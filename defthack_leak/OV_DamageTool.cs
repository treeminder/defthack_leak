using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000070 RID: 112
public static class OV_DamageTool
{
	// Token: 0x06000340 RID: 832 RVA: 0x00031494 File Offset: 0x0002F694
	[Override(typeof(DamageTool), "raycast", BindingFlags.Static | BindingFlags.Public, 1)]
	public static RaycastInfo OV_raycast(Ray ray, float range, int mask)
	{
		switch (OV_DamageTool.OVType)
		{
		case OverrideType.Extended:
			return RaycastUtilities.GenerateOriginalRaycast(ray, MiscOptions.MeleeRangeExtension, mask);
		case OverrideType.PlayerHit:
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				if (VectorUtilities.GetDistance(Player.player.transform.position, Provider.clients[i].player.transform.position) <= 15.5)
				{
					RaycastInfo result;
					RaycastUtilities.GenerateRaycast(out result);
					return result;
				}
			}
			break;
		case OverrideType.SilentAim:
		{
			RaycastInfo result2;
			if (!RaycastUtilities.GenerateRaycast(out result2))
			{
				return RaycastUtilities.GenerateOriginalRaycast(ray, range, mask);
			}
			return result2;
		}
		case OverrideType.SilentAimMelee:
		{
			RaycastInfo result3;
			if (!RaycastUtilities.GenerateRaycast(out result3))
			{
				return RaycastUtilities.GenerateOriginalRaycast(ray, MiscOptions.MeleeRangeExtension, mask);
			}
			return result3;
		}
		}
		return RaycastUtilities.GenerateOriginalRaycast(ray, range, mask);
	}

	// Token: 0x040001D5 RID: 469
	public static OverrideType OVType;
}
