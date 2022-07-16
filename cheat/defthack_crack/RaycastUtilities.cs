using System;
using System.Collections.Generic;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000097 RID: 151
public static class RaycastUtilities
{
	// Token: 0x060004D8 RID: 1240 RVA: 0x00035108 File Offset: 0x00033308
	public static bool NoShootthroughthewalls(Transform transform)
	{
		Vector3 direction = AimbotCoroutines.GetAimPosition(transform, "Skull") - Player.player.look.aim.position;
		RaycastHit raycastHit;
		return PhysicsUtility.raycast(new Ray(Player.player.look.aim.position, direction), out raycastHit, direction.magnitude, RayMasks.DAMAGE_CLIENT, QueryTriggerInteraction.UseGlobal) && raycastHit.transform.IsChildOf(transform);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0003517C File Offset: 0x0003337C
	public static RaycastInfo GenerateOriginalRaycast(Ray ray, float range, int mask)
	{
		RaycastHit hit;
		PhysicsUtility.raycast(ray, out hit, range, mask, QueryTriggerInteraction.UseGlobal);
		RaycastInfo raycastInfo = new RaycastInfo(hit);
		raycastInfo.direction = ray.direction;
		raycastInfo.limb = ELimb.SPINE;
		if (raycastInfo.transform != null)
		{
			if (!raycastInfo.transform.CompareTag("Barricade"))
			{
				if (raycastInfo.transform.CompareTag("Structure"))
				{
					raycastInfo.transform = DamageTool.getStructureRootTransform(raycastInfo.transform);
				}
				else if (!raycastInfo.transform.CompareTag("Resource"))
				{
					if (raycastInfo.transform.CompareTag("Enemy"))
					{
						raycastInfo.player = DamageTool.getPlayer(raycastInfo.transform);
						raycastInfo.limb = RaycastUtilities.GetLimb(raycastInfo);
					}
					else if (raycastInfo.transform.CompareTag("Zombie"))
					{
						raycastInfo.zombie = DamageTool.getZombie(raycastInfo.transform);
						raycastInfo.limb = RaycastUtilities.GetLimb(raycastInfo);
					}
					else if (!raycastInfo.transform.CompareTag("Animal"))
					{
						if (raycastInfo.transform.CompareTag("Vehicle"))
						{
							raycastInfo.vehicle = DamageTool.getVehicle(raycastInfo.transform);
						}
					}
					else
					{
						raycastInfo.animal = DamageTool.getAnimal(raycastInfo.transform);
						raycastInfo.limb = RaycastUtilities.GetLimb(raycastInfo);
					}
				}
				else
				{
					raycastInfo.transform = DamageTool.getResourceRootTransform(raycastInfo.transform);
				}
			}
			else
			{
				raycastInfo.transform = DamageTool.getBarricadeRootTransform(raycastInfo.transform);
			}
			if (raycastInfo.zombie != null && raycastInfo.zombie.isRadioactive)
			{
				raycastInfo.materialName = "Alien_Dynamic";
				raycastInfo.material = EPhysicsMaterial.ALIEN_DYNAMIC;
			}
			else
			{
				raycastInfo.materialName = PhysicsTool.GetMaterialName(hit);
				raycastInfo.material = PhysicsTool.GetLegacyMaterialByName(raycastInfo.materialName);
			}
		}
		return raycastInfo;
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00035344 File Offset: 0x00033544
	public static ELimb GetLimb(RaycastInfo _T)
	{
		if (RaycastOptions.AlwaysHitHead)
		{
			return ELimb.SKULL;
		}
		if (RaycastOptions.UseRandomLimb)
		{
			ELimb[] array = (ELimb[])Enum.GetValues(typeof(ELimb));
			return array[MathUtilities.Random.Next(0, array.Length)];
		}
		if (!RaycastOptions.UseCustomLimb)
		{
			return DamageTool.getLimb(_T.transform);
		}
		return RaycastOptions.TargetLimb;
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x000353A0 File Offset: 0x000335A0
	public static bool GenerateRaycast(out RaycastInfo info)
	{
		ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
		float num = (itemGunAsset != null) ? itemGunAsset.range : 15.5f;
		info = RaycastUtilities.GenerateOriginalRaycast(new Ray(OptimizationVariables.MainPlayer.look.aim.position, OptimizationVariables.MainPlayer.look.aim.forward), num, RayMasks.DAMAGE_CLIENT);
		if (RaycastOptions.EnablePlayerSelection && RaycastUtilities.TargetedPlayer != null)
		{
			GameObject gameObject = RaycastUtilities.TargetedPlayer.gameObject;
			bool flag = true;
			Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
			if (Vector3.Distance(position, gameObject.transform.position) > num)
			{
				flag = false;
			}
			Vector3 point;
			if (!SphereUtilities.GetRaycast(gameObject, position, out point))
			{
				flag = false;
			}
			if (flag)
			{
				info = RaycastUtilities.GenerateRaycast(gameObject, point, info.collider);
				return true;
			}
			if (RaycastOptions.OnlyShootAtSelectedPlayer)
			{
				return false;
			}
		}
		GameObject @object;
		Vector3 point2;
		if (!RaycastUtilities.GetTargetObject(RaycastUtilities.Objects, out @object, out point2, (double)num))
		{
			return false;
		}
		info = RaycastUtilities.GenerateRaycast(@object, point2, info.collider);
		return true;
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x000354B0 File Offset: 0x000336B0
	public static RaycastInfo GenerateRaycast(GameObject Object, Vector3 Point, Collider col)
	{
		ELimb limb = RaycastOptions.TargetLimb;
		if (RaycastOptions.UseRandomLimb)
		{
			ELimb[] array = (ELimb[])Enum.GetValues(typeof(ELimb));
			limb = array[MathUtilities.Random.Next(0, array.Length)];
		}
		EPhysicsMaterial material = (col == null) ? EPhysicsMaterial.NONE : DamageTool.getMaterial(Point, Object.transform, col);
		return new RaycastInfo(Object.transform)
		{
			point = Point,
			direction = OptimizationVariables.MainPlayer.look.aim.forward,
			limb = limb,
			material = material,
			player = Object.GetComponent<Player>(),
			zombie = Object.GetComponent<Zombie>(),
			vehicle = Object.GetComponent<InteractableVehicle>()
		};
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00035568 File Offset: 0x00033768
	public static bool GetTargetObject(GameObject[] Objects, out GameObject Object, out Vector3 Point, double Range)
	{
		double num = Range + 1.0;
		double num2 = 180.0;
		Object = null;
		Point = Vector3.zero;
		Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
		Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
		foreach (GameObject gameObject in Objects)
		{
			if (!(gameObject == null))
			{
				Vector3 position2 = gameObject.transform.position;
				Player component = gameObject.GetComponent<Player>();
				if (!component || (!component.life.isDead && !FriendUtilities.IsFriendly(component) && (!RaycastOptions.NoShootthroughthewalls || RaycastUtilities.NoShootthroughthewalls(gameObject.transform))))
				{
					Zombie component2 = gameObject.GetComponent<Zombie>();
					if (!component2 || !component2.isDead)
					{
						if (!(gameObject.GetComponent<RaycastComponent>() == null))
						{
							double distance = VectorUtilities.GetDistance(position, position2);
							if (distance <= Range)
							{
								if (!RaycastOptions.SilentAimUseFOV)
								{
									if (distance > num)
									{
										goto IL_14B;
									}
								}
								else
								{
									double angleDelta = VectorUtilities.GetAngleDelta(position, forward, position2);
									if (angleDelta > (double)RaycastOptions.SilentAimFOV || angleDelta > num2)
									{
										goto IL_14B;
									}
									num2 = angleDelta;
								}
								Vector3 vector;
								if (SphereUtilities.GetRaycast(gameObject, position, out vector))
								{
									Object = gameObject;
									num = distance;
									Point = vector;
								}
							}
						}
						else
						{
							gameObject.AddComponent<RaycastComponent>();
						}
					}
				}
			}
			IL_14B:;
		}
		return Object != null;
	}

	// Token: 0x0400024A RID: 586
	public static GameObject[] Objects = new GameObject[0];

	// Token: 0x0400024B RID: 587
	public static List<GameObject> AttachedObjects = new List<GameObject>();

	// Token: 0x0400024C RID: 588
	public static Player TargetedPlayer;
}
