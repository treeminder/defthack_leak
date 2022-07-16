using System;

// Token: 0x0200008F RID: 143
public static class RadarOptions
{
	// Token: 0x0400021C RID: 540
	[Save]
	public static bool Enabled = false;

	// Token: 0x0400021D RID: 541
	[Save]
	public static bool TrackPlayer = false;

	// Token: 0x0400021E RID: 542
	[Save]
	public static bool ShowPlayers = false;

	// Token: 0x0400021F RID: 543
	[Save]
	public static bool ShowVehicles = false;

	// Token: 0x04000220 RID: 544
	[Save]
	public static bool ShowVehiclesUnlocked = false;

	// Token: 0x04000221 RID: 545
	[Save]
	public static bool ShowDeathPosition = false;

	// Token: 0x04000222 RID: 546
	public static float RadarZoom = 1f;

	// Token: 0x04000223 RID: 547
	[Save]
	public static float RadarSize = 300f;
}
