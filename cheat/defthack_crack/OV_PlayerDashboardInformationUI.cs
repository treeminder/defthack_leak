using System;
using System.Reflection;
using SDG.Unturned;

// Token: 0x0200000A RID: 10
public static class OV_PlayerDashboardInformationUI
{
	// Token: 0x0600004C RID: 76 RVA: 0x00024FC8 File Offset: 0x000231C8
	[Override(typeof(PlayerDashboardInformationUI), "searchForMapsInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
	public static void OV_searchForMapsInInventory(ref bool enableChart, ref bool enableMap)
	{
		if (MiscOptions.GPS)
		{
			enableMap = true;
			enableChart = true;
			return;
		}
		OverrideUtilities.CallOriginal(null, new object[]
		{
			true,
			true
		});
	}
}
