using System;
using System.Collections.Generic;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000072 RID: 114
public static class OV_ItemManager
{
	// Token: 0x06000350 RID: 848 RVA: 0x000315FC File Offset: 0x0002F7FC
	[Override(typeof(ItemManager), "getItemsInRadius", BindingFlags.Static | BindingFlags.Public, 0)]
	public static void OV_getItemsInRadius(Vector3 center, float sqrRadius, List<RegionCoordinate> search, List<InteractableItem> result)
	{
		if (MiscOptions.IncreaseNearbyItemDistance)
		{
			OverrideUtilities.CallOriginal(null, new object[]
			{
				center,
				Mathf.Pow(20f, 2f),
				search,
				result
			});
			return;
		}
		OverrideUtilities.CallOriginal(null, new object[]
		{
			center,
			sqrRadius,
			search,
			result
		});
	}
}
