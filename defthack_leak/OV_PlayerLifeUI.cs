using System;
using System.Reflection;
using SDG.Unturned;

// Token: 0x0200007B RID: 123
public static class OV_PlayerLifeUI
{
	// Token: 0x060003A4 RID: 932 RVA: 0x00025EEB File Offset: 0x000240EB
	[Override(typeof(PlayerLifeUI), "hasCompassInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
	public static bool OV_hasCompassInInventory()
	{
		return MiscOptions.Compass || (bool)OverrideUtilities.CallOriginal(null, new object[0]);
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00025F07 File Offset: 0x00024107
	[Override(typeof(PlayerLifeUI), "updateGrayscale", BindingFlags.Static | BindingFlags.Public, 0)]
	public static void OV_updateGrayscale()
	{
		if (!MiscOptions.NoGrayscale)
		{
			OverrideUtilities.CallOriginal(null, new object[0]);
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00025F1D File Offset: 0x0002411D
	[OnSpy]
	public static void Disable()
	{
		if (DrawUtilities.ShouldRun())
		{
			OV_PlayerLifeUI.WasCompassEnabled = MiscOptions.Compass;
			MiscOptions.Compass = false;
			PlayerLifeUI.updateCompass();
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00025F3B File Offset: 0x0002413B
	[OffSpy]
	public static void Enable()
	{
		if (DrawUtilities.ShouldRun())
		{
			MiscOptions.Compass = OV_PlayerLifeUI.WasCompassEnabled;
			PlayerLifeUI.updateCompass();
		}
	}

	// Token: 0x040001FB RID: 507
	public static bool WasCompassEnabled;
}
