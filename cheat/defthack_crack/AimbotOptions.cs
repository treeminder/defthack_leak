using System;
using SDG.Unturned;

// Token: 0x02000006 RID: 6
public static class AimbotOptions
{
	// Token: 0x0400000A RID: 10
	[Save]
	public static bool Enabled = false;

	// Token: 0x0400000B RID: 11
	[Save]
	public static bool UseGunDistance = false;

	// Token: 0x0400000C RID: 12
	[Save]
	public static bool Smooth = false;

	// Token: 0x0400000D RID: 13
	[Save]
	public static bool OnKey = false;

	// Token: 0x0400000E RID: 14
	[Save]
	public static bool UseFovAim = true;

	// Token: 0x0400000F RID: 15
	public static float MaxSpeed = 20f;

	// Token: 0x04000010 RID: 16
	[Save]
	public static float AimSpeed = 5f;

	// Token: 0x04000011 RID: 17
	[Save]
	public static float Distance = 300f;

	// Token: 0x04000012 RID: 18
	[Save]
	public static float FOV = 15f;

	// Token: 0x04000013 RID: 19
	[Save]
	public static ELimb TargetLimb = ELimb.SKULL;

	// Token: 0x04000014 RID: 20
	[Save]
	public static TargetMode TargetMode = TargetMode.Distance;

	// Token: 0x04000015 RID: 21
	[Save]
	public static bool NoAimbotDrop = false;
}
