using System;

// Token: 0x0200003F RID: 63
public static class ItemOptions
{
	// Token: 0x040000F4 RID: 244
	[Save]
	public static bool AutoItemPickup = false;

	// Token: 0x040000F5 RID: 245
	[Save]
	public static bool AutoForagePickup = false;

	// Token: 0x040000F6 RID: 246
	[Save]
	public static int ItemPickupDelay = 1000;

	// Token: 0x040000F7 RID: 247
	[Save]
	public static ItemOptionList ItemFilterOptions = new ItemOptionList();

	// Token: 0x040000F8 RID: 248
	[Save]
	public static ItemOptionList ItemESPOptions = new ItemOptionList();
}
