using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public static class SkinsTab
{
	// Token: 0x06000506 RID: 1286 RVA: 0x000356DC File Offset: 0x000338DC
	public static void Tab()
	{
		Prefab.MenuArea(new Rect(150f, 55f, 481f, 414f), MenuComponent._isRus ? "СКИНЫ" : "SKINS", delegate()
		{
		});
	}
}
