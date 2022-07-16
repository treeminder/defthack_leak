using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SDG.Provider;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000054 RID: 84
[Component]
public class MiscComponent : MonoBehaviour
{
	// Token: 0x0600026B RID: 619 RVA: 0x0002EDA0 File Offset: 0x0002CFA0
	[Initializer]
	public static void Initialize()
	{
		HotkeyComponent.ActionDict.Add("_VFToggle", delegate
		{
			MiscOptions.VehicleFly = !MiscOptions.VehicleFly;
		});
		HotkeyComponent.ActionDict.Add("_ToggleAimbot", delegate
		{
			AimbotOptions.Enabled = !AimbotOptions.Enabled;
		});
		HotkeyComponent.ActionDict.Add("_AimbotOnKey", delegate
		{
			AimbotOptions.OnKey = !AimbotOptions.OnKey;
		});
		HotkeyComponent.ActionDict.Add("_ToggleFreecam", delegate
		{
			MiscOptions.Freecam = !MiscOptions.Freecam;
		});
		HotkeyComponent.ActionDict.Add("_PanicButton", delegate
		{
			MiscOptions.PanicMode = !MiscOptions.PanicMode;
			if (MiscOptions.PanicMode)
			{
				PlayerCoroutines.DisableAllVisuals();
				return;
			}
			PlayerCoroutines.EnableAllVisuals();
		});
		HotkeyComponent.ActionDict.Add("_SelectPlayer", delegate
		{
			Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
			Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
			if (RaycastOptions.EnablePlayerSelection)
			{
				foreach (GameObject gameObject in RaycastUtilities.Objects)
				{
					Player component = gameObject.GetComponent<Player>();
					if (component != null && VectorUtilities.GetAngleDelta(position, forward, gameObject.transform.position) < (double)RaycastOptions.SelectedFOV)
					{
						RaycastUtilities.TargetedPlayer = component;
						return;
					}
				}
			}
		});
		HotkeyComponent.ActionDict.Add("_InstantDisconnect", delegate
		{
			Provider.disconnect();
		});
		HotkeyComponent.ActionDict.Add("_AutoPickUp", delegate
		{
			ItemOptions.AutoItemPickup = !ItemOptions.AutoItemPickup;
		});
	}

	// Token: 0x0600026C RID: 620 RVA: 0x000258E2 File Offset: 0x00023AE2
	[OnSpy]
	public static void Disable()
	{
		if (MiscOptions.WasNightVision)
		{
			MiscComponent.NightvisionBeforeSpy = true;
			MiscOptions.NightVision = false;
		}
		if (MiscOptions.Freecam)
		{
			MiscComponent.FreecamBeforeSpy = true;
			MiscOptions.Freecam = false;
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0002590A File Offset: 0x00023B0A
	[OffSpy]
	public static void Enable()
	{
		if (MiscComponent.NightvisionBeforeSpy)
		{
			MiscComponent.NightvisionBeforeSpy = false;
			MiscOptions.NightVision = true;
		}
		if (MiscComponent.FreecamBeforeSpy)
		{
			MiscComponent.FreecamBeforeSpy = false;
			MiscOptions.Freecam = true;
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00025932 File Offset: 0x00023B32
	public void Start()
	{
		MiscComponent.Instance = this;
		Provider.onClientConnected = (Provider.ClientConnected)Delegate.Combine(Provider.onClientConnected, new Provider.ClientConnected(delegate()
		{
			if (MiscOptions.AlwaysCheckMovementVerification)
			{
				MiscComponent.CheckMovementVerification();
				return;
			}
			MiscOptions.NoMovementVerification = false;
		}));
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0002EF20 File Offset: 0x0002D120
	public void Update()
	{
		if (MiscOptions.SpammerDelay < 500)
		{
			MiscOptions.SpammerDelay = 500;
		}
		if (Camera.main != null && OptimizationVariables.MainCam == null)
		{
			OptimizationVariables.MainCam = Camera.main;
		}
		if (OptimizationVariables.MainPlayer && DrawUtilities.ShouldRun())
		{
			int num;
			Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num);
			if (WeaponOptions.OofOnDeath)
			{
				if (num != this.currentKills)
				{
					if (this.currentKills != -1)
					{
						OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetVariables.Audio["oof"], 3f);
					}
					this.currentKills = num;
				}
			}
			else
			{
				this.currentKills = num;
			}
			if (!MiscOptions.NightVision)
			{
				if (MiscOptions.WasNightVision)
				{
					LevelLighting.vision = ELightingVision.NONE;
					LevelLighting.nightvisionColor = new Color(0f, 0f, 0f, 0f);
					LevelLighting.nightvisionFogIntensity = 0f;
					LevelLighting.updateLighting();
					LevelLighting.updateLocal();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision = false;
				}
			}
			else
			{
				LevelLighting.vision = ELightingVision.MILITARY;
				LevelLighting.nightvisionColor = new Color(0.078f, 0.471f, 0.314f, 1f);
				LevelLighting.nightvisionFogIntensity = 0.25f;
				LevelLighting.updateLighting();
				LevelLighting.updateLocal();
				PlayerLifeUI.updateGrayscale();
				MiscOptions.WasNightVision = true;
			}
			if (OptimizationVariables.MainPlayer.life.isDead)
			{
				MiscComponent.LastDeath = OptimizationVariables.MainPlayer.transform.position;
			}
			if (MiscOptions.NoFlash && MiscOptions.NoFlash && ((Color)typeof(PlayerUI).GetField("stunAlpha", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).a > 0f)
			{
				typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, 0);
			}
		}
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0002596D File Offset: 0x00023B6D
	public void FixedUpdate()
	{
		if (OptimizationVariables.MainPlayer)
		{
			MiscComponent.VehicleFlight();
			MiscComponent.PlayerFlight();
		}
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0002F108 File Offset: 0x0002D308
	public static void PlayerFlight()
	{
		Player mainPlayer = OptimizationVariables.MainPlayer;
		if (!MiscOptions.PlayerFlight)
		{
			ItemCloudAsset itemCloudAsset = mainPlayer.equipment.asset as ItemCloudAsset;
			mainPlayer.movement.itemGravityMultiplier = ((itemCloudAsset != null) ? itemCloudAsset.gravity : 1f);
			return;
		}
		mainPlayer.movement.itemGravityMultiplier = 0f;
		float flightSpeedMultiplier = MiscOptions.FlightSpeedMultiplier;
		if (HotkeyUtilities.IsHotkeyHeld("_FlyUp"))
		{
			mainPlayer.transform.position += mainPlayer.transform.up / 5f * flightSpeedMultiplier;
		}
		if (HotkeyUtilities.IsHotkeyHeld("_FlyDown"))
		{
			mainPlayer.transform.position -= mainPlayer.transform.up / 5f * flightSpeedMultiplier;
		}
		if (HotkeyUtilities.IsHotkeyHeld("_FlyLeft"))
		{
			mainPlayer.transform.position -= mainPlayer.transform.right / 5f * flightSpeedMultiplier;
		}
		if (HotkeyUtilities.IsHotkeyHeld("_FlyRight"))
		{
			mainPlayer.transform.position += mainPlayer.transform.right / 5f * flightSpeedMultiplier;
		}
		if (HotkeyUtilities.IsHotkeyHeld("_FlyForward"))
		{
			mainPlayer.transform.position += mainPlayer.transform.forward / 5f * flightSpeedMultiplier;
		}
		if (HotkeyUtilities.IsHotkeyHeld("_FlyBackward"))
		{
			mainPlayer.transform.position -= mainPlayer.transform.forward / 5f * flightSpeedMultiplier;
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0002F2D4 File Offset: 0x0002D4D4
	public static void VehicleFlight()
	{
		InteractableVehicle vehicle = OptimizationVariables.MainPlayer.movement.getVehicle();
		if (!(vehicle == null))
		{
			Rigidbody component = vehicle.GetComponent<Rigidbody>();
			if (!(component == null))
			{
				if (!MiscOptions.VehicleFly)
				{
					component.useGravity = true;
					component.isKinematic = false;
				}
				else
				{
					float num = MiscOptions.VehicleUseMaxSpeed ? (vehicle.asset.speedMax * Time.fixedDeltaTime) : (MiscOptions.SpeedMultiplier / 3f);
					component.useGravity = false;
					component.isKinematic = true;
					Transform transform = vehicle.transform;
					if (HotkeyUtilities.IsHotkeyHeld("_VFStrafeUp"))
					{
						transform.position += new Vector3(0f, num * 0.65f, 0f);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFStrafeDown"))
					{
						transform.position -= new Vector3(0f, num * 0.65f, 0f);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFStrafeLeft"))
					{
						component.MovePosition(transform.position - transform.right * num);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFStrafeRight"))
					{
						component.MovePosition(transform.position + transform.right * num);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFMoveForward"))
					{
						component.MovePosition(transform.position + transform.forward * num);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFMoveBackward"))
					{
						component.MovePosition(transform.position - transform.forward * num);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFRotateRight"))
					{
						transform.Rotate(0f, 1f, 0f);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFRotateLeft"))
					{
						transform.Rotate(0f, -1f, 0f);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFRollLeft"))
					{
						transform.Rotate(0f, 0f, 2f);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFRollRight"))
					{
						transform.Rotate(0f, 0f, -2f);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFRotateUp"))
					{
						vehicle.transform.Rotate(-2f, 0f, 0f);
					}
					if (HotkeyUtilities.IsHotkeyHeld("_VFRotateDown"))
					{
						vehicle.transform.Rotate(2f, 0f, 0f);
						return;
					}
				}
			}
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00025985 File Offset: 0x00023B85
	public static void CheckMovementVerification()
	{
		MiscComponent.Instance.StartCoroutine(MiscComponent.CheckVerification(OptimizationVariables.MainPlayer.transform.position));
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0002F540 File Offset: 0x0002D740
	public static void incrementStatTrackerValue(ushort itemID, int newValue)
	{
		if (Player.player == null)
		{
			return;
		}
		SteamPlayer owner = Player.player.channel.owner;
		if (owner == null)
		{
			return;
		}
		int num;
		if (!owner.getItemSkinItemDefID(itemID, out num))
		{
			return;
		}
		string tags;
		string dynamic_props;
		if (!owner.getTagsAndDynamicPropsForItem(num, out tags, out dynamic_props))
		{
			return;
		}
		DynamicEconDetails dynamicEconDetails = new DynamicEconDetails(tags, dynamic_props);
		EStatTrackerType type;
		int num2;
		if (!dynamicEconDetails.getStatTrackerValue(out type, out num2))
		{
			return;
		}
		if (!owner.modifiedItems.Contains(itemID))
		{
			owner.modifiedItems.Add(itemID);
		}
		int i = 0;
		while (i < owner.skinItems.Length)
		{
			if (owner.skinItems[i] != num)
			{
				i++;
			}
			else
			{
				if (i < owner.skinDynamicProps.Length)
				{
					owner.skinDynamicProps[i] = dynamicEconDetails.getPredictedDynamicPropsJsonForStatTracker(type, newValue);
					return;
				}
				return;
			}
		}
	}

	// Token: 0x06000275 RID: 629 RVA: 0x000259A6 File Offset: 0x00023BA6
	public static IEnumerator CheckVerification(Vector3 LastPos)
	{
		if (Time.realtimeSinceStartup - MiscComponent.LastMovementCheck >= 0.8f)
		{
			MiscComponent.LastMovementCheck = Time.realtimeSinceStartup;
			OptimizationVariables.MainPlayer.transform.position = new Vector3(0f, -1337f, 0f);
			yield return new WaitForSeconds(3f);
			if (VectorUtilities.GetDistance(OptimizationVariables.MainPlayer.transform.position, LastPos) >= 10.0)
			{
				MiscOptions.NoMovementVerification = true;
				OptimizationVariables.MainPlayer.transform.position = LastPos + new Vector3(0f, 5f, 0f);
			}
			else
			{
				MiscOptions.NoMovementVerification = false;
			}
			yield break;
		}
		yield break;
		yield break;
	}

	// Token: 0x04000143 RID: 323
	public static Vector3 LastDeath;

	// Token: 0x04000144 RID: 324
	public static MiscComponent Instance;

	// Token: 0x04000145 RID: 325
	public static float LastMovementCheck;

	// Token: 0x04000146 RID: 326
	public static bool FreecamBeforeSpy;

	// Token: 0x04000147 RID: 327
	public static bool NightvisionBeforeSpy;

	// Token: 0x04000148 RID: 328
	public static List<PlayerInputPacket> ClientsidePackets;

	// Token: 0x04000149 RID: 329
	public static FieldInfo Primary = typeof(PlayerEquipment).GetField("_primary", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x0400014A RID: 330
	public static FieldInfo Sequence = typeof(PlayerInput).GetField("sequence", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x0400014B RID: 331
	public static FieldInfo CPField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x0400014C RID: 332
	public int currentKills = -1;

	// Token: 0x0400014D RID: 333
	public bool _isBroken;
}
