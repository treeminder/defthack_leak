using System;
using System.Diagnostics;
using System.Reflection;
using SDG.Unturned;

// Token: 0x0200007D RID: 125
public static class OV_PlayerPauseUI
{
	// Token: 0x060003AB RID: 939 RVA: 0x00025A2D File Offset: 0x00023C2D
	[Override(typeof(PlayerPauseUI), "onClickedExitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
	public static void OV_onClickedExitButton(ISleekElement button)
	{
		Provider.disconnect();
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00025F72 File Offset: 0x00024172
	[Override(typeof(PlayerPauseUI), "onClickedQuitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
	public static void OV_onClickedQuitButton(ISleekElement button)
	{
		Process.GetCurrentProcess().Kill();
	}
}
