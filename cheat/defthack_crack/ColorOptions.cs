using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000C RID: 12
public static class ColorOptions
{
	// Token: 0x04000029 RID: 41
	[Save]
	public static Dictionary<string, ColorVariable> ColorDict = new Dictionary<string, ColorVariable>();

	// Token: 0x0400002A RID: 42
	public static Dictionary<string, ColorVariable> DefaultColorDict = new Dictionary<string, ColorVariable>();

	// Token: 0x0400002B RID: 43
	public static ColorVariable errorColor = new ColorVariable("errorColor", "#ERROR.NOTFOUND", Color.magenta, true);

	// Token: 0x0400002C RID: 44
	public static ColorVariable preview = new ColorVariable("preview", "No Color Selected", Color.white, true);

	// Token: 0x0400002D RID: 45
	public static ColorVariable previewselected;

	// Token: 0x0400002E RID: 46
	public static string selectedOption;
}
