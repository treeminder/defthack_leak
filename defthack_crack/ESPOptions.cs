using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200002A RID: 42
public static class ESPOptions
{
	// Token: 0x0400006C RID: 108
	[Save]
	public static bool Enabled = true;

	// Token: 0x0400006D RID: 109
	[Save]
	public static bool ChamsEnabled = false;

	// Token: 0x0400006E RID: 110
	[Save]
	public static bool ChamsFlat = false;

	// Token: 0x0400006F RID: 111
	[Save]
	public static bool ShowVanishPlayers = false;

	// Token: 0x04000070 RID: 112
	[Save]
	public static bool ShowToolTipWindow = false;

	// Token: 0x04000071 RID: 113
	[Save]
	public static bool ShowCoordinates = false;

	// Token: 0x04000072 RID: 114
	[Save]
	public static ESPVisual[] VisualOptions = Enumerable.Repeat<ESPVisual>(new ESPVisual
	{
		Enabled = false,
		Labels = false,
		Boxes = false,
		ShowName = false,
		ShowDistance = false,
		ShowAngle = false,
		TwoDimensional = false,
		Glow = false,
		InfiniteDistance = false,
		LineToObject = false,
		TextScaling = false,
		UseObjectCap = false,
		CustomTextColor = false,
		Distance = 250f,
		Location = LabelLocation.BottomMiddle,
		FixedTextSize = 11,
		MinTextSize = 8,
		MaxTextSize = 11,
		MinTextSizeDistance = 800f,
		BorderStrength = 2,
		ObjectCap = 24
	}, Enum.GetValues(typeof(ESPTarget)).Length).ToArray<ESPVisual>();

	// Token: 0x04000073 RID: 115
	[Save]
	public static Dictionary<ESPTarget, int> PriorityTable = Enum.GetValues(typeof(ESPTarget)).Cast<ESPTarget>().ToDictionary((ESPTarget x) => x, (ESPTarget x) => (int)x);

	// Token: 0x04000074 RID: 116
	[Save]
	public static bool ShowPlayerWeapon = false;

	// Token: 0x04000075 RID: 117
	[Save]
	public static bool ShowPlayerVehicle = false;

	// Token: 0x04000076 RID: 118
	[Save]
	public static bool UsePlayerGroup = false;

	// Token: 0x04000078 RID: 120
	[Save]
	public static bool FilterItems = false;

	// Token: 0x04000079 RID: 121
	[Save]
	public static bool ShowVehicleFuel;

	// Token: 0x0400007A RID: 122
	[Save]
	public static bool ShowVehicleHealth;

	// Token: 0x0400007B RID: 123
	[Save]
	public static bool ShowVehicleLocked;

	// Token: 0x0400007C RID: 124
	[Save]
	public static bool FilterVehicleLocked;

	// Token: 0x0400007D RID: 125
	[Save]
	public static bool ShowSentryItem;

	// Token: 0x0400007E RID: 126
	[Save]
	public static bool ShowClaimed;

	// Token: 0x0400007F RID: 127
	[Save]
	public static bool ShowGeneratorFuel;

	// Token: 0x04000080 RID: 128
	[Save]
	public static bool ShowGeneratorPowered;
}
