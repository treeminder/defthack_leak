using System;
using System.Collections.Generic;
using System.Reflection;
using SDG.Unturned;

// Token: 0x02000077 RID: 119
public class OV_PlayerDashboardInventoryUI
{

	// Token: 0x040001DA RID: 474
	public static Items areaItems = new Items(PlayerInventory.AREA);

	// Token: 0x040001DB RID: 475
	public static List<InteractableItem> itemsInRadius = new List<InteractableItem>();

	// Token: 0x040001DC RID: 476
	public static List<RegionCoordinate> regionsInRadius = new List<RegionCoordinate>(4);

	// Token: 0x040001DD RID: 477
	public static FieldInfo itemsfield = typeof(PlayerDashboardInventoryUI).GetField("items", BindingFlags.Static | BindingFlags.NonPublic);

	// Token: 0x040001DE RID: 478
	public static MethodInfo updateBoxAreasfield = typeof(PlayerDashboardInventoryUI).GetMethod("updateBoxAreas", BindingFlags.Static | BindingFlags.NonPublic);
}
