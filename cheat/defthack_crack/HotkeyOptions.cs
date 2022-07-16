using System;
using System.Collections.Generic;

// Token: 0x02000034 RID: 52
public static class HotkeyOptions
{
	// Token: 0x040000D0 RID: 208
	[Save]
	public static Dictionary<string, Dictionary<string, Hotkey>> HotkeyDict = new Dictionary<string, Dictionary<string, Hotkey>>();

	// Token: 0x040000D1 RID: 209
	[Save]
	public static Dictionary<string, Hotkey> UnorganizedHotkeys = new Dictionary<string, Hotkey>();
}
