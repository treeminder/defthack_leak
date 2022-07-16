using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000035 RID: 53
public static class HotkeyTab
{
	// Token: 0x0600019B RID: 411 RVA: 0x0002C148 File Offset: 0x0002A348
	public static void Tab()
	{
		Prefab.ScrollView(new Rect(0f, 0f, 466f, 385f), ref HotkeyTab.HotkeyScroll, delegate()
		{
			foreach (KeyValuePair<string, Dictionary<string, Hotkey>> keyValuePair in HotkeyOptions.HotkeyDict)
			{
				if (HotkeyTab.IsFirst)
				{
					HotkeyTab.IsFirst = false;
					HotkeyTab.DrawSpacer(keyValuePair.Key, true);
				}
				else
				{
					HotkeyTab.DrawSpacer(keyValuePair.Key, false);
				}
				foreach (KeyValuePair<string, Hotkey> keyValuePair2 in keyValuePair.Value)
				{
					HotkeyTab.DrawButton(keyValuePair2.Value.Name, keyValuePair2.Key);
				}
			}
		});
	}

	// Token: 0x0600019C RID: 412 RVA: 0x000255D0 File Offset: 0x000237D0
	public static void DrawSpacer(string Text, bool First)
	{
		if (!First)
		{
			GUILayout.Space(10f);
		}
		GUILayout.Label(Text, new GUILayoutOption[0]);
		GUILayout.Space(8f);
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0002C198 File Offset: 0x0002A398
	public static void DrawButton(string Option, string Identifier)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label(Option, new GUILayoutOption[0]);
		if (!(HotkeyTab.ClickedOption == Identifier))
		{
			string text;
			if (HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Length != 0)
			{
				text = string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
				{
					KeyCode keyCode = k;
					return keyCode.ToString();
				}).ToArray<string>());
			}
			else
			{
				text = (MenuComponent._isRus ? "Не назначена" : "Not assigned");
			}
			if (GUILayout.Button(text, new GUILayoutOption[]
			{
				GUILayout.Width(90f)
			}))
			{
				HotkeyComponent.Clear();
				HotkeyTab.ClickedOption = Identifier;
				HotkeyComponent.NeedsKeys = true;
			}
		}
		else
		{
			if (GUILayout.Button(MenuComponent._isRus ? "Убрать" : "Put away", new GUILayoutOption[]
			{
				GUILayout.Width(90f)
			}))
			{
				HotkeyComponent.Clear();
				HotkeyOptions.UnorganizedHotkeys[Identifier].Keys = new KeyCode[0];
				HotkeyTab.ClickedOption = "";
			}
			if (!HotkeyComponent.StopKeys)
			{
				string text2;
				if (HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Length != 0)
				{
					text2 = string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
					{
						KeyCode keyCode = k;
						return keyCode.ToString();
					}).ToArray<string>());
				}
				else
				{
					text2 = (MenuComponent._isRus ? "Не назначена" : "Not assigned");
				}
				GUILayout.Button(text2, new GUILayoutOption[]
				{
					GUILayout.Width(90f)
				});
			}
			else
			{
				HotkeyOptions.UnorganizedHotkeys[Identifier].Keys = HotkeyComponent.CurrentKeys.ToArray();
				HotkeyComponent.Clear();
				GUILayout.Button(string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
				{
					KeyCode keyCode = k;
					return keyCode.ToString();
				}).ToArray<string>()), new GUILayoutOption[]
				{
					GUILayout.Width(90f)
				});
				HotkeyTab.ClickedOption = "";
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(2f);
	}

	// Token: 0x040000D2 RID: 210
	public static Vector2 HotkeyScroll;

	// Token: 0x040000D3 RID: 211
	public static string ClickedOption;

	// Token: 0x040000D4 RID: 212
	public static bool IsFirst = true;
}
