using System;
using System.Collections.Generic;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class OV_UseableGun
{
	// Token: 0x060003C0 RID: 960 RVA: 0x00025FAD File Offset: 0x000241AD
	[Initializer]
	public static void Load()
	{
		OV_UseableGun.BulletsField = typeof(UseableGun).GetField("bullets", ReflectionVariables.publicInstance);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x000325C8 File Offset: 0x000307C8
	public static bool IsRaycastInvalid(RaycastInfo info)
	{
		return info.player == null && info.zombie == null && info.animal == null && info.vehicle == null && info.transform == null;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x0003261C File Offset: 0x0003081C
	[Override(typeof(UseableGun), "ballistics", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
	public void OV_ballistics()
	{
		Useable useable = OptimizationVariables.MainPlayer.equipment.useable;
		if (Provider.isServer)
		{
			OverrideUtilities.CallOriginal(useable, new object[0]);
			return;
		}
		if (Time.realtimeSinceStartup - PlayerLifeUI.hitmarkers[0].lastHit > PlayerUI.HIT_TIME)
		{
			PlayerLifeUI.hitmarkers[0].image.isVisible = false;
		}
		ItemGunAsset itemGunAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
		PlayerLook look = OptimizationVariables.MainPlayer.look;
		if (!(itemGunAsset.projectile != null))
		{
			List<BulletInfo> list = (List<BulletInfo>)OV_UseableGun.BulletsField.GetValue(useable);
			if (list.Count != 0)
			{
				RaycastInfo raycastInfo = null;
				if (RaycastOptions.Enabled)
				{
					RaycastUtilities.GenerateRaycast(out raycastInfo);
				}
				if (Provider.modeConfigData.Gameplay.Ballistics)
				{
					if (raycastInfo == null)
					{
						if (AimbotOptions.NoAimbotDrop && (AimbotCoroutines.IsAiming && AimbotCoroutines.LockedObject != null))
						{
							Vector3 aimPosition = AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull");
							Ray aimRay = OV_UseableGun.GetAimRay(look.aim.position, aimPosition);
							float maxDistance = (float)VectorUtilities.GetDistance(look.aim.position, aimPosition);
							RaycastHit raycastHit;
							if (!Physics.Raycast(aimRay, out raycastHit, maxDistance, RayMasks.DAMAGE_SERVER))
							{
								raycastInfo = RaycastUtilities.GenerateOriginalRaycast(aimRay, itemGunAsset.range, 1024);
							}
						}
						if (WeaponOptions.NoDrop && raycastInfo == null)
						{
							for (int i = 0; i < list.Count; i++)
							{
								BulletInfo bulletInfo = list[i];
								RaycastInfo info = DamageTool.raycast(new Ray(bulletInfo.pos, bulletInfo.dir), itemGunAsset.ballisticTravel, RayMasks.DAMAGE_CLIENT);
								if (OV_UseableGun.IsRaycastInvalid(info))
								{
									bulletInfo.pos += bulletInfo.dir * itemGunAsset.ballisticTravel;
								}
								else
								{
									EPlayerHit newHit = OV_UseableGun.CalcHitMarker(itemGunAsset, ref info);
									PlayerUI.hitmark(0, Vector3.zero, false, newHit);
									OptimizationVariables.MainPlayer.input.sendRaycast(info, ERaycastInfoUsage.Gun);
									bulletInfo.steps = 254;
								}
							}
							for (int j = list.Count - 1; j >= 0; j--)
							{
								BulletInfo bulletInfo2 = list[j];
								bulletInfo2.steps += 1;
								if (bulletInfo2.steps >= itemGunAsset.ballisticSteps)
								{
									list.RemoveAt(j);
								}
							}
							return;
						}
						if (raycastInfo == null)
						{
							OverrideUtilities.CallOriginal(useable, new object[0]);
							return;
						}
					}
					for (int k = 0; k < list.Count; k++)
					{
						BulletInfo bulletInfo3 = list[k];
						double distance = VectorUtilities.GetDistance(OptimizationVariables.MainPlayer.transform.position, raycastInfo.point);
						if ((double)((float)bulletInfo3.steps * itemGunAsset.ballisticTravel) >= distance)
						{
							EPlayerHit newHit2 = OV_UseableGun.CalcHitMarker(itemGunAsset, ref raycastInfo);
							PlayerUI.hitmark(0, Vector3.zero, false, newHit2);
							OptimizationVariables.MainPlayer.input.sendRaycast(raycastInfo, ERaycastInfoUsage.Gun);
							bulletInfo3.steps = 254;
						}
					}
					for (int l = list.Count - 1; l >= 0; l--)
					{
						BulletInfo bulletInfo4 = list[l];
						bulletInfo4.steps += 1;
						if (bulletInfo4.steps >= itemGunAsset.ballisticSteps)
						{
							list.RemoveAt(l);
						}
					}
					return;
				}
				if (raycastInfo != null)
				{
					for (int m = 0; m < list.Count; m++)
					{
						EPlayerHit newHit3 = OV_UseableGun.CalcHitMarker(itemGunAsset, ref raycastInfo);
						PlayerUI.hitmark(0, Vector3.zero, false, newHit3);
						OptimizationVariables.MainPlayer.input.sendRaycast(raycastInfo, ERaycastInfoUsage.Gun);
					}
					list.Clear();
					return;
				}
				OverrideUtilities.CallOriginal(useable, new object[0]);
			}
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x000329D8 File Offset: 0x00030BD8
	public static EPlayerHit CalcHitMarker(ItemGunAsset PAsset, ref RaycastInfo ri)
	{
		EPlayerHit eplayerHit = EPlayerHit.NONE;
		EPlayerHit result;
		if (ri == null || PAsset == null)
		{
			result = eplayerHit;
		}
		else
		{
			if (ri.animal || ri.player || ri.zombie)
			{
				eplayerHit = EPlayerHit.ENTITIY;
				if (ri.limb == ELimb.SKULL)
				{
					eplayerHit = EPlayerHit.CRITICAL;
				}
			}
			else if (!ri.transform)
			{
				if (ri.vehicle && !ri.vehicle.isDead && PAsset.vehicleDamage > 1f && (ri.vehicle.asset != null && (ri.vehicle.asset.isVulnerable || PAsset.isInvulnerable)) && eplayerHit == EPlayerHit.NONE)
				{
					eplayerHit = EPlayerHit.BUILD;
				}
			}
			else if (ri.transform.CompareTag("Barricade") && PAsset.barricadeDamage > 1f)
			{
				InteractableDoorHinge component = ri.transform.GetComponent<InteractableDoorHinge>();
				if (component != null)
				{
					ri.transform = component.transform.parent.parent;
				}
				ushort id;
				if (!ushort.TryParse(ri.transform.name, out id))
				{
					return eplayerHit;
				}
				ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id);
				if (itemBarricadeAsset == null || (!itemBarricadeAsset.isVulnerable && !PAsset.isInvulnerable))
				{
					return eplayerHit;
				}
				if (eplayerHit == EPlayerHit.NONE)
				{
					eplayerHit = EPlayerHit.BUILD;
				}
			}
			else if (ri.transform.CompareTag("Structure") && PAsset.structureDamage > 1f)
			{
				ushort id2;
				if (!ushort.TryParse(ri.transform.name, out id2))
				{
					return eplayerHit;
				}
				ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id2);
				if (itemStructureAsset == null || (!itemStructureAsset.isVulnerable && !PAsset.isInvulnerable))
				{
					return eplayerHit;
				}
				if (eplayerHit == EPlayerHit.NONE)
				{
					eplayerHit = EPlayerHit.BUILD;
				}
			}
			else if (ri.transform.CompareTag("Resource") && PAsset.resourceDamage > 1f)
			{
				byte x;
				byte y;
				ushort index;
				if (!ResourceManager.tryGetRegion(ri.transform, out x, out y, out index))
				{
					return eplayerHit;
				}
				ResourceSpawnpoint resourceSpawnpoint = ResourceManager.getResourceSpawnpoint(x, y, index);
				if (resourceSpawnpoint == null || resourceSpawnpoint.isDead || !PAsset.hasBladeID(resourceSpawnpoint.asset.bladeID))
				{
					return eplayerHit;
				}
				if (eplayerHit == EPlayerHit.NONE)
				{
					eplayerHit = EPlayerHit.BUILD;
				}
			}
			else if (PAsset.objectDamage > 1f)
			{
				InteractableObjectRubble component2 = ri.transform.GetComponent<InteractableObjectRubble>();
				if (component2 == null)
				{
					return eplayerHit;
				}
				ri.section = component2.getSection(ri.collider.transform);
				if (component2.isSectionDead(ri.section) || (!component2.asset.rubbleIsVulnerable && !PAsset.isInvulnerable))
				{
					return eplayerHit;
				}
				if (eplayerHit == EPlayerHit.NONE)
				{
					eplayerHit = EPlayerHit.BUILD;
				}
			}
			result = eplayerHit;
		}
		return result;
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00032CF4 File Offset: 0x00030EF4
	public static Ray GetAimRay(Vector3 origin, Vector3 pos)
	{
		Vector3 direction = VectorUtilities.Normalize(pos - origin);
		return new Ray(pos, direction);
	}

	// Token: 0x040001FE RID: 510
	public static FieldInfo BulletsField;
}
