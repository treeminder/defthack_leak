using System;
using System.Collections.Generic;

// Token: 0x0200009F RID: 159
public class SkinOptionList
{
	// Token: 0x06000505 RID: 1285 RVA: 0x00026425 File Offset: 0x00024625
	public SkinOptionList(SkinType Type)
	{
		this.Type = Type;
	}

	// Token: 0x04000265 RID: 613
	public SkinType Type = SkinType.Weapons;

	// Token: 0x04000266 RID: 614
	public HashSet<Skin> Skins = new HashSet<Skin>();
}
