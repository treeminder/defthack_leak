using System;
using System.Reflection;
using HighlightingSystem;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class OV_PlayerInteract
{
	// Token: 0x0600037A RID: 890 RVA: 0x000318FC File Offset: 0x0002FAFC
	[Initializer]
	public static void Init()
	{
		OV_PlayerInteract.FocusField = typeof(PlayerInteract).GetField("focus", ReflectionVariables.publicStatic);
		OV_PlayerInteract.TargetField = typeof(PlayerInteract).GetField("target", ReflectionVariables.publicStatic);
		OV_PlayerInteract.InteractableField = typeof(PlayerInteract).GetField("_interactable", ReflectionVariables.publicStatic);
		OV_PlayerInteract.Interactable2Field = typeof(PlayerInteract).GetField("_interactable2", ReflectionVariables.publicStatic);
		OV_PlayerInteract.PurchaseAssetField = typeof(PlayerInteract).GetField("purchaseAsset", ReflectionVariables.publicStatic);
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x0600037B RID: 891 RVA: 0x00025DF1 File Offset: 0x00023FF1
	// (set) Token: 0x0600037C RID: 892 RVA: 0x00025E03 File Offset: 0x00024003
	public static Transform focus
	{
		get
		{
			return (Transform)OV_PlayerInteract.FocusField.GetValue(null);
		}
		set
		{
			OV_PlayerInteract.FocusField.SetValue(null, value);
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600037D RID: 893 RVA: 0x00025E11 File Offset: 0x00024011
	// (set) Token: 0x0600037E RID: 894 RVA: 0x00025E23 File Offset: 0x00024023
	public static Transform target
	{
		get
		{
			return (Transform)OV_PlayerInteract.TargetField.GetValue(null);
		}
		set
		{
			OV_PlayerInteract.TargetField.SetValue(null, value);
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x0600037F RID: 895 RVA: 0x00025E31 File Offset: 0x00024031
	// (set) Token: 0x06000380 RID: 896 RVA: 0x00025E43 File Offset: 0x00024043
	public static Interactable interactable
	{
		get
		{
			return (Interactable)OV_PlayerInteract.InteractableField.GetValue(null);
		}
		set
		{
			OV_PlayerInteract.InteractableField.SetValue(null, value);
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000381 RID: 897 RVA: 0x00025E51 File Offset: 0x00024051
	// (set) Token: 0x06000382 RID: 898 RVA: 0x00025E63 File Offset: 0x00024063
	public static Interactable2 interactable2
	{
		get
		{
			return (Interactable2)OV_PlayerInteract.Interactable2Field.GetValue(null);
		}
		set
		{
			OV_PlayerInteract.Interactable2Field.SetValue(null, value);
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06000383 RID: 899 RVA: 0x00025E71 File Offset: 0x00024071
	// (set) Token: 0x06000384 RID: 900 RVA: 0x00025E83 File Offset: 0x00024083
	public static ItemAsset purchaseAsset
	{
		get
		{
			return (ItemAsset)OV_PlayerInteract.PurchaseAssetField.GetValue(null);
		}
		set
		{
			OV_PlayerInteract.PurchaseAssetField.SetValue(null, value);
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000385 RID: 901 RVA: 0x00025E91 File Offset: 0x00024091
	public float salvageTime
	{
		get
		{
			if (MiscOptions.CustomSalvageTime)
			{
				return MiscOptions.SalvageTime;
			}
			if (!OptimizationVariables.MainPlayer.channel.owner.isAdmin)
			{
				return 8f;
			}
			return 1f;
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00025EC1 File Offset: 0x000240C1
	public void hotkey(byte button)
	{
		VehicleManager.swapVehicle(button);
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00025EC9 File Offset: 0x000240C9
	public void onPurchaseUpdated(PurchaseNode node)
	{
		if (node == null)
		{
			OV_PlayerInteract.purchaseAsset = null;
			return;
		}
		OV_PlayerInteract.purchaseAsset = (ItemAsset)Assets.find(EAssetType.ITEM, node.id);
	}

	// Token: 0x06000388 RID: 904 RVA: 0x000319A0 File Offset: 0x0002FBA0
	public static void highlight(Transform target, Color color)
	{
		if (!target.CompareTag("Player") || target.CompareTag("Enemy") || target.CompareTag("Zombie") || target.CompareTag("Animal") || target.CompareTag("Agent"))
		{
			Highlighter highlighter = target.GetComponent<Highlighter>();
			if (highlighter == null)
			{
				highlighter = target.gameObject.AddComponent<Highlighter>();
			}
			highlighter.ConstantOn(color, 0.25f);
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00031A18 File Offset: 0x0002FC18
	[OnSpy]
	public static void OnSpied()
	{
		Transform transform = OptimizationVariables.MainCam.transform;
		if (transform != null)
		{
			PhysicsUtility.raycast(new Ray(transform.position, transform.forward), out OV_PlayerInteract.hit, (float)((OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.THIRD) ? 6 : 4), RayMasks.PLAYER_INTERACT, QueryTriggerInteraction.UseGlobal);
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00031A74 File Offset: 0x0002FC74
	[Override(typeof(PlayerInteract), "Update", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
	public void OV_Update()
	{
		if (!DrawUtilities.ShouldRun())
		{
			return;
		}
		if (OptimizationVariables.MainPlayer.stance.stance != EPlayerStance.DRIVING && OptimizationVariables.MainPlayer.stance.stance != EPlayerStance.SITTING && !OptimizationVariables.MainPlayer.life.isDead && !OptimizationVariables.MainPlayer.workzone.isBuilding)
		{
			if (Time.realtimeSinceStartup - OV_PlayerInteract.lastInteract > 0.1f)
			{
				int num = 0;
				if (InteractionOptions.InteractThroughWalls && !PlayerCoroutines.IsSpying)
				{
					if (!InteractionOptions.NoHitBarricades)
					{
						num |= 134217728;
					}
					if (!InteractionOptions.NoHitItems)
					{
						num |= 8192;
					}
					if (!InteractionOptions.NoHitResources)
					{
						num |= 16384;
					}
					if (!InteractionOptions.NoHitStructures)
					{
						num |= 268435456;
					}
					if (!InteractionOptions.NoHitVehicles)
					{
						num |= 67108864;
					}
					if (!InteractionOptions.NoHitEnvironment)
					{
						num |= 1671168;
					}
				}
				else
				{
					num = RayMasks.PLAYER_INTERACT;
				}
				OV_PlayerInteract.lastInteract = Time.realtimeSinceStartup;
				float num2 = (!InteractionOptions.InteractThroughWalls || PlayerCoroutines.IsSpying) ? 4f : 20f;
				PhysicsUtility.raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out OV_PlayerInteract.hit, (OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.THIRD) ? (num2 + 2f) : num2, num, QueryTriggerInteraction.UseGlobal);
			}
			Transform transform = (!(OV_PlayerInteract.hit.collider != null)) ? null : OV_PlayerInteract.hit.collider.transform;
			if (transform != OV_PlayerInteract.focus)
			{
				if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
				{
					InteractableDoorHinge componentInParent = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
					if (componentInParent != null)
					{
						HighlighterTool.unhighlight(componentInParent.door.transform);
					}
					else
					{
						HighlighterTool.unhighlight(PlayerInteract.interactable.transform);
					}
				}
				OV_PlayerInteract.focus = null;
				OV_PlayerInteract.target = null;
				OV_PlayerInteract.interactable = null;
				OV_PlayerInteract.interactable2 = null;
				if (transform != null)
				{
					OV_PlayerInteract.focus = transform;
					OV_PlayerInteract.interactable = OV_PlayerInteract.focus.GetComponentInParent<Interactable>();
					OV_PlayerInteract.interactable2 = OV_PlayerInteract.focus.GetComponentInParent<Interactable2>();
					if (PlayerInteract.interactable != null)
					{
						OV_PlayerInteract.target = PlayerInteract.interactable.transform.FindChildRecursive("Target");
						if (PlayerInteract.interactable.checkInteractable())
						{
							if (PlayerUI.window.isEnabled)
							{
								if (PlayerInteract.interactable.checkUseable())
								{
									Color green;
									if (!PlayerInteract.interactable.checkHighlight(out green))
									{
										green = Color.green;
									}
									InteractableDoorHinge componentInParent2 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
									if (!(componentInParent2 != null))
									{
										HighlighterTool.highlight(PlayerInteract.interactable.transform, green);
									}
									else
									{
										HighlighterTool.highlight(componentInParent2.door.transform, green);
									}
								}
								else
								{
									Color red = Color.red;
									InteractableDoorHinge componentInParent3 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
									if (componentInParent3 != null)
									{
										HighlighterTool.highlight(componentInParent3.door.transform, red);
									}
									else
									{
										HighlighterTool.highlight(PlayerInteract.interactable.transform, red);
									}
								}
							}
						}
						else
						{
							OV_PlayerInteract.target = null;
							OV_PlayerInteract.interactable = null;
						}
					}
				}
			}
		}
		else
		{
			if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
			{
				InteractableDoorHinge componentInParent4 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
				if (!(componentInParent4 != null))
				{
					HighlighterTool.unhighlight(PlayerInteract.interactable.transform);
				}
				else
				{
					HighlighterTool.unhighlight(componentInParent4.door.transform);
				}
			}
			OV_PlayerInteract.focus = null;
			OV_PlayerInteract.target = null;
			OV_PlayerInteract.interactable = null;
			OV_PlayerInteract.interactable2 = null;
		}
		if (!OptimizationVariables.MainPlayer.life.isDead)
		{
			if (PlayerInteract.interactable != null)
			{
				EPlayerMessage message;
				string text;
				Color color;
				if (PlayerInteract.interactable.checkHint(out message, out text, out color) && !PlayerUI.window.showCursor)
				{
					if (PlayerInteract.interactable.CompareTag("Item"))
					{
						PlayerUI.hint((!(OV_PlayerInteract.target != null)) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, message, text, color, new object[]
						{
							((InteractableItem)PlayerInteract.interactable).item,
							((InteractableItem)PlayerInteract.interactable).asset
						});
					}
					else
					{
						PlayerUI.hint((!(OV_PlayerInteract.target != null)) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, message, text, color, new object[0]);
					}
				}
			}
			else if (OV_PlayerInteract.purchaseAsset != null && OptimizationVariables.MainPlayer.movement.purchaseNode != null && !PlayerUI.window.showCursor)
			{
				PlayerUI.hint(null, EPlayerMessage.PURCHASE, string.Empty, Color.white, new object[]
				{
					OV_PlayerInteract.purchaseAsset.itemName,
					OptimizationVariables.MainPlayer.movement.purchaseNode.cost
				});
			}
			else if (OV_PlayerInteract.focus != null && OV_PlayerInteract.focus.CompareTag("Enemy"))
			{
				Player player = DamageTool.getPlayer(OV_PlayerInteract.focus);
				if (player != null && player != Player.player && !PlayerUI.window.showCursor)
				{
					PlayerUI.hint(null, EPlayerMessage.ENEMY, string.Empty, Color.white, new object[]
					{
						player.channel.owner
					});
				}
			}
			EPlayerMessage message2;
			float data;
			if (PlayerInteract.interactable2 != null && PlayerInteract.interactable2.checkHint(out message2, out data) && !PlayerUI.window.showCursor)
			{
				PlayerUI.hint2(message2, (!OV_PlayerInteract.isHoldingKey) ? 0f : ((Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown) / this.salvageTime), data);
			}
			if (OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SITTING)
			{
				if (!Input.GetKey(KeyCode.LeftShift))
				{
					if (Input.GetKeyDown(KeyCode.F1))
					{
						this.hotkey(0);
					}
					if (Input.GetKeyDown(KeyCode.F2))
					{
						this.hotkey(1);
					}
					if (Input.GetKeyDown(KeyCode.F3))
					{
						this.hotkey(2);
					}
					if (Input.GetKeyDown(KeyCode.F4))
					{
						this.hotkey(3);
					}
					if (Input.GetKeyDown(KeyCode.F5))
					{
						this.hotkey(4);
					}
					if (Input.GetKeyDown(KeyCode.F6))
					{
						this.hotkey(5);
					}
					if (Input.GetKeyDown(KeyCode.F7))
					{
						this.hotkey(6);
					}
					if (Input.GetKeyDown(KeyCode.F8))
					{
						this.hotkey(7);
					}
					if (Input.GetKeyDown(KeyCode.F9))
					{
						this.hotkey(8);
					}
					if (Input.GetKeyDown(KeyCode.F10))
					{
						this.hotkey(9);
					}
				}
			}
			if (Input.GetKeyDown(ControlsSettings.interact))
			{
				OV_PlayerInteract.lastKeyDown = Time.realtimeSinceStartup;
				OV_PlayerInteract.isHoldingKey = true;
			}
			if (Input.GetKeyDown(ControlsSettings.inspect) && ControlsSettings.inspect != ControlsSettings.interact && OptimizationVariables.MainPlayer.equipment.canInspect)
			{
				OptimizationVariables.MainPlayer.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
			if (OV_PlayerInteract.isHoldingKey)
			{
				if (!Input.GetKeyUp(ControlsSettings.interact))
				{
					if (Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown > this.salvageTime)
					{
						OV_PlayerInteract.isHoldingKey = false;
						if (!PlayerUI.window.showCursor && PlayerInteract.interactable2 != null)
						{
							PlayerInteract.interactable2.use();
						}
					}
				}
				else
				{
					OV_PlayerInteract.isHoldingKey = false;
					if (PlayerUI.window.showCursor)
					{
						if (OptimizationVariables.MainPlayer.inventory.isStoring && OptimizationVariables.MainPlayer.inventory.shouldInteractCloseStorage)
						{
							PlayerDashboardUI.close();
							PlayerLifeUI.open();
							return;
						}
						if (PlayerBarricadeSignUI.active)
						{
							PlayerBarricadeSignUI.close();
							PlayerLifeUI.open();
							return;
						}
						if ((bool)typeof(PlayerBarricadeStereoUI).GetField("active", BindingFlags.Instance | BindingFlags.Public).GetValue(null))
						{
							typeof(PlayerBarricadeStereoUI).GetMethod("close", BindingFlags.Instance | BindingFlags.Public).Invoke(null, null);
							PlayerLifeUI.open();
							return;
						}
						if (PlayerBarricadeLibraryUI.active)
						{
							PlayerBarricadeLibraryUI.close();
							PlayerLifeUI.open();
							return;
						}
						if (PlayerBarricadeMannequinUI.active)
						{
							PlayerBarricadeMannequinUI.close();
							PlayerLifeUI.open();
							return;
						}
						if (PlayerNPCDialogueUI.active)
						{
							if (PlayerNPCDialogueUI.dialogueAnimating)
							{
								PlayerNPCDialogueUI.skipText();
								return;
							}
							if (!PlayerNPCDialogueUI.dialogueHasNextPage)
							{
								PlayerNPCDialogueUI.close();
								PlayerLifeUI.open();
								return;
							}
							PlayerNPCDialogueUI.nextPage();
							return;
						}
						else
						{
							if (PlayerNPCQuestUI.active)
							{
								PlayerNPCQuestUI.closeNicely();
								return;
							}
							if (PlayerNPCVendorUI.active)
							{
								PlayerNPCVendorUI.closeNicely();
								return;
							}
						}
					}
					else
					{
						if (OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SITTING)
						{
							VehicleManager.exitVehicle();
							return;
						}
						if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
						{
							if (PlayerInteract.interactable.checkUseable())
							{
								PlayerInteract.interactable.use();
								return;
							}
						}
						else if (OV_PlayerInteract.purchaseAsset != null)
						{
							if (OptimizationVariables.MainPlayer.skills.experience >= OptimizationVariables.MainPlayer.movement.purchaseNode.cost)
							{
								OptimizationVariables.MainPlayer.skills.sendPurchase(OptimizationVariables.MainPlayer.movement.purchaseNode);
								return;
							}
						}
						else if (ControlsSettings.inspect == ControlsSettings.interact)
						{
							if (OptimizationVariables.MainPlayer.equipment.canInspect)
							{
								OptimizationVariables.MainPlayer.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
								return;
							}
						}
					}
				}
			}
			return;
		}
	}

	// Token: 0x040001F2 RID: 498
	public static FieldInfo FocusField;

	// Token: 0x040001F3 RID: 499
	public static FieldInfo TargetField;

	// Token: 0x040001F4 RID: 500
	public static FieldInfo InteractableField;

	// Token: 0x040001F5 RID: 501
	public static FieldInfo Interactable2Field;

	// Token: 0x040001F6 RID: 502
	public static FieldInfo PurchaseAssetField;

	// Token: 0x040001F7 RID: 503
	public static bool isHoldingKey;

	// Token: 0x040001F8 RID: 504
	public static float lastInteract;

	// Token: 0x040001F9 RID: 505
	public static float lastKeyDown;

	// Token: 0x040001FA RID: 506
	public static RaycastHit hit;
}
