using System;

// Token: 0x020000BC RID: 188
public static class WeaponOptions
{
	// Token: 0x040002CC RID: 716
	[Save]
	public static bool ShowWeaponInfo = false;

	// Token: 0x040002CD RID: 717
	[Save]
	public static bool CustomCrosshair = false;

	// Token: 0x040002CE RID: 718
	[Save]
	public static SerializableColor CrosshairColor = new SerializableColor(255, 0, 0);

	// Token: 0x040002CF RID: 719
	[Save]
	public static bool NoRecoil = false;

	// Token: 0x040002D0 RID: 720
	[Save]
	public static bool NoSpread = false;

	// Token: 0x040002D1 RID: 721
	[Save]
	public static bool NoSway = false;

	// Token: 0x040002D2 RID: 722
	[Save]
	public static bool NoDrop = false;

	// Token: 0x040002D3 RID: 723
	[Save]
	public static bool OofOnDeath = false;

	// Token: 0x040002D4 RID: 724
	[Save]
	public static bool AutoReload = false;

	// Token: 0x040002D5 RID: 725
	[Save]
	public static bool Tracers = false;

	// Token: 0x040002D6 RID: 726
	[Save]
	public static bool EnableBulletDropPrediction = false;

	// Token: 0x040002D7 RID: 727
	[Save]
	public static bool HighlightBulletDropPredictionTarget = false;

	// Token: 0x040002D8 RID: 728
	[Save]
	public static bool Zoom;

	// Token: 0x040002D9 RID: 729
	[Save]
	public static float ZoomValue = 16f;
}
