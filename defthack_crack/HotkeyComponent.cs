using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000033 RID: 51
[Component]
public class HotkeyComponent : MonoBehaviour
{
	// Token: 0x06000192 RID: 402 RVA: 0x0002C040 File Offset: 0x0002A240
	public void Update()
	{
		if (HotkeyComponent.NeedsKeys)
		{
			List<KeyCode> currentKeys = HotkeyComponent.CurrentKeys.ToList<KeyCode>();
			HotkeyComponent.CurrentKeys.Clear();
			foreach (KeyCode keyCode in HotkeyComponent.Keys)
			{
				if (Input.GetKey(keyCode))
				{
					HotkeyComponent.CurrentKeys.Add(keyCode);
				}
			}
			if (HotkeyComponent.CurrentKeys.Count < HotkeyComponent.CurrentKeyCount && HotkeyComponent.CurrentKeyCount > 0)
			{
				HotkeyComponent.CurrentKeys = currentKeys;
				HotkeyComponent.StopKeys = true;
			}
			HotkeyComponent.CurrentKeyCount = HotkeyComponent.CurrentKeys.Count;
		}
		if (!MenuComponent.IsInMenu)
		{
			foreach (KeyValuePair<string, Action> keyValuePair in HotkeyComponent.ActionDict)
			{
				if ((!MiscOptions.PanicMode || keyValuePair.Key == "_PanicButton") && HotkeyUtilities.IsHotkeyDown(keyValuePair.Key))
				{
					keyValuePair.Value();
				}
			}
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00025577 File Offset: 0x00023777
	public static void Clear()
	{
		HotkeyComponent.NeedsKeys = false;
		HotkeyComponent.StopKeys = false;
		HotkeyComponent.CurrentKeyCount = 0;
		HotkeyComponent.CurrentKeys = new List<KeyCode>();
	}

	// Token: 0x040000CA RID: 202
	public static bool NeedsKeys;

	// Token: 0x040000CB RID: 203
	public static bool StopKeys;

	// Token: 0x040000CC RID: 204
	public static int CurrentKeyCount;

	// Token: 0x040000CD RID: 205
	public static List<KeyCode> CurrentKeys;

	// Token: 0x040000CE RID: 206
	public static Dictionary<string, Action> ActionDict = new Dictionary<string, Action>();

	// Token: 0x040000CF RID: 207
	public static KeyCode[] Keys = (KeyCode[])Enum.GetValues(typeof(KeyCode));
}
