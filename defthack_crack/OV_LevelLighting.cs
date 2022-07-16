using System;
using System.Reflection;
using SDG.Unturned;

// Token: 0x02000073 RID: 115
public static class OV_LevelLighting
{
	// Token: 0x06000351 RID: 849 RVA: 0x00025CEA File Offset: 0x00023EEA
	[OnSpy]
	public static void Disable()
	{
		if (DrawUtilities.ShouldRun())
		{
			OV_LevelLighting.WasEnabled = MiscOptions.ShowPlayersOnMap;
			MiscOptions.ShowPlayersOnMap = false;
			OV_LevelLighting.OV_updateLighting();
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00025D08 File Offset: 0x00023F08
	[OffSpy]
	public static void Enable()
	{
		if (DrawUtilities.ShouldRun())
		{
			MiscOptions.ShowPlayersOnMap = OV_LevelLighting.WasEnabled;
			OV_LevelLighting.OV_updateLighting();
		}
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00025D20 File Offset: 0x00023F20
	[Initializer]
	public static void Init()
	{
		OV_LevelLighting.Time = typeof(LevelLighting).GetField("_time", BindingFlags.Static | BindingFlags.NonPublic);
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00031670 File Offset: 0x0002F870
	[Override(typeof(LevelLighting), "updateLighting", BindingFlags.Static | BindingFlags.Public, 0)]
	public static void OV_updateLighting()
	{
		float time = LevelLighting.time;
		if (DrawUtilities.ShouldRun() && MiscOptions.SetTimeEnabled && !PlayerCoroutines.IsSpying)
		{
			OV_LevelLighting.Time.SetValue(null, MiscOptions.Time);
			OverrideUtilities.CallOriginal(null, new object[0]);
			OV_LevelLighting.Time.SetValue(null, time);
			return;
		}
		OverrideUtilities.CallOriginal(null, new object[0]);
	}

	// Token: 0x040001D7 RID: 471
	public static FieldInfo Time;

	// Token: 0x040001D8 RID: 472
	public static bool WasEnabled;
}
