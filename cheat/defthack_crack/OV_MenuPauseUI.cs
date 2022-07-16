using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000074 RID: 116
public static class OV_MenuPauseUI
{
	// Token: 0x06000359 RID: 857 RVA: 0x00025D3D File Offset: 0x00023F3D
	[Override(typeof(MenuPauseUI), "onClickedQuitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
	public static void OV_onClickedQuitButton(ISleekButton button)
	{
		Application.Quit();
	}
}
