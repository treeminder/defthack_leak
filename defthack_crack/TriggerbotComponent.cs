using System;
using System.Collections;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x020000B1 RID: 177
[Component]
public class TriggerbotComponent : MonoBehaviour
{
	// Token: 0x0600055D RID: 1373 RVA: 0x0002651F File Offset: 0x0002471F
	[Initializer]
	public static void Init()
	{
		TriggerbotComponent.CurrentFiremode = typeof(UseableGun).GetField("firemode", BindingFlags.Instance | BindingFlags.NonPublic);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0002653C File Offset: 0x0002473C
	public void Start()
	{
		base.StartCoroutine(TriggerbotComponent.CheckTrigger());
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0002654A File Offset: 0x0002474A
	public static IEnumerator CheckTrigger()
	{
		for (; ; )
		{
			yield return new WaitForSeconds(0.1f);
			if (!TriggerbotOptions.Enabled || !DrawUtilities.ShouldRun() || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SPRINT || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.CLIMB || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING)
			{
				TriggerbotOptions.IsFiring = false;
			}
			else
			{
				PlayerLook look = OptimizationVariables.MainPlayer.look;
				Useable useable2 = OptimizationVariables.MainPlayer.equipment.useable;
				UseableGun useableGun;
				if (useable2 == null)
				{
					TriggerbotOptions.IsFiring = false;
				}
				else if ((useableGun = (useable2 as UseableGun)) != null)
				{
					UseableGun gun = useableGun;
					ItemGunAsset PAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
					RaycastInfo ri = RaycastUtilities.GenerateOriginalRaycast(new Ray(look.aim.position, look.aim.forward), PAsset.range, RayMasks.DAMAGE_CLIENT);
					if (AimbotCoroutines.LockedObject != null && AimbotCoroutines.IsAiming)
					{
						Ray r = OV_UseableGun.GetAimRay(look.aim.position, AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull"));
						ri = RaycastUtilities.GenerateOriginalRaycast(new Ray(r.origin, r.direction), PAsset.range, RayMasks.DAMAGE_CLIENT);
						r = default(Ray);
						r = default(Ray);
					}
					bool Valid = ri.player == null;
					if (RaycastOptions.Enabled)
					{
						Valid = RaycastUtilities.GenerateRaycast(out ri);
					}
					if (Valid)
					{
						TriggerbotOptions.IsFiring = false;
					}
					else if ((EFiremode)TriggerbotComponent.CurrentFiremode.GetValue(gun) == EFiremode.AUTO)
					{
						TriggerbotOptions.IsFiring = true;
					}
					else
					{
						TriggerbotOptions.IsFiring = !TriggerbotOptions.IsFiring;
						ri = null;
					}
				}
				else if (useable2 as UseableMelee != null)
				{
					ItemMeleeAsset MAsset = (ItemMeleeAsset)OptimizationVariables.MainPlayer.equipment.asset;
					RaycastInfo ri2 = RaycastUtilities.GenerateOriginalRaycast(new Ray(look.aim.position, look.aim.forward), MAsset.range, RayMasks.DAMAGE_CLIENT);
					if (AimbotCoroutines.LockedObject != null && AimbotCoroutines.IsAiming)
					{
						Ray r2 = OV_UseableGun.GetAimRay(look.aim.position, AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull"));
						ri2 = RaycastUtilities.GenerateOriginalRaycast(new Ray(r2.origin, r2.direction), MAsset.range, RayMasks.DAMAGE_CLIENT);
						r2 = default(Ray);
						r2 = default(Ray);
					}
					bool Valid2 = ri2.player != null;
					if (RaycastOptions.Enabled)
					{
						Valid2 = RaycastUtilities.GenerateRaycast(out ri2);
					}
					if (!Valid2)
					{
						TriggerbotOptions.IsFiring = false;
					}
					else if (MAsset.isRepeated)
					{
						TriggerbotOptions.IsFiring = true;
					}
					else
					{
						TriggerbotOptions.IsFiring = !TriggerbotOptions.IsFiring;
						ri2 = null;
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x04000299 RID: 665
	public static FieldInfo CurrentFiremode;
}
