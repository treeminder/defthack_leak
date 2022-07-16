using System;
using System.Collections.Generic;

// Token: 0x0200003E RID: 62
public class ItemOptionList
{
	// Token: 0x040000E6 RID: 230
	public HashSet<ushort> AddedItems = new HashSet<ushort>();

	// Token: 0x040000E7 RID: 231
	public bool ItemfilterGun = true;

	// Token: 0x040000E8 RID: 232
	public bool ItemfilterGunMeel = true;

	// Token: 0x040000E9 RID: 233
	public bool ItemfilterAmmo = true;

	// Token: 0x040000EA RID: 234
	public bool ItemfilterMedical = true;

	// Token: 0x040000EB RID: 235
	public bool ItemfilterBackpack = true;

	// Token: 0x040000EC RID: 236
	public bool ItemfilterCharges = true;

	// Token: 0x040000ED RID: 237
	public bool ItemfilterFuel = true;

	// Token: 0x040000EE RID: 238
	public bool ItemfilterClothing = true;

	// Token: 0x040000EF RID: 239
	public bool ItemfilterFoodAndWater = true;

	// Token: 0x040000F0 RID: 240
	public bool ItemfilterCustom;

	// Token: 0x040000F1 RID: 241
	public string searchstring = "";

	// Token: 0x040000F2 RID: 242
	public SerializableVector2 additemscroll = new SerializableVector2(0f, 0f);

	// Token: 0x040000F3 RID: 243
	public SerializableVector2 removeitemscroll = new SerializableVector2(0f, 0f);
}
