using System;
using System.Collections.Generic;
using SDG.Unturned;

// Token: 0x02000096 RID: 150
public static class RaycastOptions
{
	// Token: 0x04000237 RID: 567
	[Save]
	public static bool Enabled = false;

	// Token: 0x04000238 RID: 568
	[Save]
	public static bool NoShootthroughthewalls = false;

	// Token: 0x04000239 RID: 569
	[Save]
	public static bool AlwaysHitHead = false;

	// Token: 0x0400023A RID: 570
	[Save]
	public static bool UseRandomLimb = false;

	// Token: 0x0400023B RID: 571
	[Save]
	public static bool UseCustomLimb = false;

	// Token: 0x0400023C RID: 572
	[Save]
	public static bool UseTargetMaterial = false;

	// Token: 0x0400023D RID: 573
	[Save]
	public static bool UseModifiedVector = false;

	// Token: 0x0400023E RID: 574
	[Save]
	public static bool EnablePlayerSelection = false;

	// Token: 0x0400023F RID: 575
	[Save]
	public static bool OnlyShootAtSelectedPlayer = false;

	// Token: 0x04000240 RID: 576
	[Save]
	public static float SelectedFOV = 10f;

	// Token: 0x04000241 RID: 577
	[Save]
	public static bool SilentAimUseFOV = false;

	// Token: 0x04000242 RID: 578
	[Save]
	public static bool ShowSilentAimUseFOV = false;

	// Token: 0x04000243 RID: 579
	[Save]
	public static bool ShowAimUseFOV = false;

	// Token: 0x04000244 RID: 580
	[Save]
	public static float SilentAimFOV = 10f;

	// Token: 0x04000245 RID: 581
	[Save]
	public static HashSet<TargetPriority> Targets = new HashSet<TargetPriority>();

	// Token: 0x04000246 RID: 582
	[Save]
	public static TargetPriority Target = TargetPriority.Players;

	// Token: 0x04000247 RID: 583
	[Save]
	public static EPhysicsMaterial TargetMaterial = EPhysicsMaterial.ALIEN_DYNAMIC;

	// Token: 0x04000248 RID: 584
	[Save]
	public static ELimb TargetLimb = ELimb.SKULL;

	// Token: 0x04000249 RID: 585
	[Save]
	public static SerializableVector TargetRagdoll = new SerializableVector(0f, 10f, 0f);
}
