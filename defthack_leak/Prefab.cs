using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200008C RID: 140
public static class Prefab
{
	// Token: 0x06000464 RID: 1124 RVA: 0x00033F10 File Offset: 0x00032110
	public static bool ColorButton(float width, ColorVariable color, float height = 25f, params GUILayoutOption[] options)
	{
		List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
		list.Add(GUILayout.Height(height));
		list.Add(GUILayout.Width(width));
		Rect rect = GUILayoutUtility.GetRect(width, height, list.ToArray());
		Drawing.DrawRect(rect, Color.black, null);
		Rect rect2 = new Rect(rect.x + 4f, rect.y + 4f, rect.height - 8f, rect.height - 8f);
		bool result = GUI.Button(MenuUtilities.Inline(rect, 1), color.name ?? "");
		Drawing.DrawRect(rect2, Color.black, null);
		Drawing.DrawRect(MenuUtilities.Inline(rect2, 1), Color.gray, null);
		Drawing.DrawRect(MenuUtilities.Inline(rect2, 2), color.color, null);
		return result;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00033FE4 File Offset: 0x000321E4
	public static void MenuArea(Rect area, string header, Action code)
	{
		Rect rect = new Rect(area.x, area.y + 5f, area.width, area.height - 5f);
		Rect rect2 = Prefab.Inline(rect, 1);
		Rect position = Prefab.Inline(rect2, 1);
		Drawing.DrawRect(rect, Color.clear, null);
		Drawing.DrawRect(rect2, Color.clear, null);
		Drawing.DrawRect(position, Color.clear, null);
		Vector2 vector = new GUIStyle().CalcSize(new GUIContent(header));
		Drawing.DrawRect(new Rect(area.x + 15f, area.y, vector.x + 4f, vector.y), Color.clear, null);
		GUILayout.BeginArea(area);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		try
		{
			code();
		}
		catch (Exception)
		{
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x000340E0 File Offset: 0x000322E0
	public static void MenuArea(Rect area, string header, Action code, Color32 color)
	{
		Rect rect = new Rect(area.x, area.y + 5f, area.width, area.height - 5f);
		Rect rect2 = Prefab.Inline(rect, 1);
		Rect position = Prefab.Inline(rect2, 1);
		Drawing.DrawRect(rect, color, null);
		Drawing.DrawRect(rect2, Color.clear, null);
		Drawing.DrawRect(position, Color.clear, null);
		Vector2 vector = new GUIStyle().CalcSize(new GUIContent(header));
		Drawing.DrawRect(new Rect(area.x + 15f, area.y, vector.x + 4f, vector.y), Color.clear, null);
		GUILayout.BeginArea(area);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		try
		{
			code();
		}
		catch (Exception)
		{
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x000257F6 File Offset: 0x000239F6
	public static Rect Inline(Rect rect, int border = 1)
	{
		return new Rect(rect.x + (float)border, rect.y + (float)border, rect.width - (float)(border * 2), rect.height - (float)(border * 2));
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x000341DC File Offset: 0x000323DC
	public static void ScrollView(Rect rect, ref Vector2 scrollpos, Action code)
	{
		GUILayout.BeginArea(rect);
		scrollpos = GUILayout.BeginScrollView(scrollpos, false, true, new GUILayoutOption[0]);
		try
		{
			code();
		}
		catch (Exception)
		{
		}
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00034230 File Offset: 0x00032430
	public static void ScrollView(Rect rect, ref SerializableVector2 scrollpos, Action code)
	{
		GUILayout.BeginArea(rect);
		scrollpos = GUILayout.BeginScrollView(scrollpos.ToVector2(), false, true, new GUILayoutOption[0]);
		try
		{
			code();
		}
		catch (Exception)
		{
		}
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00034284 File Offset: 0x00032484
	public static void SectionTab(Rect rect, string header, Action code)
	{
		if (GUILayout.Button(header, MenuComponent.skin.GetStyle("TabButtons"), new GUILayoutOption[0]))
		{
			MenuComponent._sectiontabVis = true;
			if (MenuComponent._sectiontabVis)
			{
				MenuComponent._sectiontabAction = delegate()
				{
					Prefab.MenuArea(new Rect(0f, 0f, 640f, 480f), header, delegate()
					{
						GUILayout.BeginArea(new Rect(10f, 10f, 630f, 470f));
						GUILayout.Label(header, MenuComponent.skin.GetStyle("BigLabel"), new GUILayoutOption[0]);
						code();
						GUILayout.EndArea();
						GUILayout.BeginArea(new Rect(10f, 440f, 100f, 50f));
						Prefab.ToggleButton(ref MenuComponent._sectiontabVis, "НАЗАД", MenuComponent.skin.GetStyle("TabButtons"));
						GUILayout.EndArea();
					}, new Color32(1, 10, 25, byte.MaxValue));
				};
			}
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0002607A File Offset: 0x0002427A
	public static void ToggleButton(ref bool toggle, string head, GUIStyle gUIStyle)
	{
		if (GUILayout.Button(head, gUIStyle, new GUILayoutOption[0]))
		{
			toggle = !toggle;
		}
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00026092 File Offset: 0x00024292
	public static bool Toggle(ref bool value, string head, int fakeint = 0)
	{
		value = GUILayout.Toggle(value, head, new GUILayoutOption[0]);
		return value;
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x000260A6 File Offset: 0x000242A6
	public static bool Toggle(string head, ref bool value, int fakeint = 0)
	{
		value = GUILayout.Toggle(value, head, new GUILayoutOption[0]);
		return value;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x000342E8 File Offset: 0x000324E8
	public static int List(int width, GUIContent uIContent, int max, int target)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (GUILayout.Button("<=", new GUILayoutOption[0]))
		{
			if (target == 0)
			{
				target = max;
			}
			else
			{
				target--;
			}
		}
		GUILayout.Label(uIContent, MenuComponent.skin.GetStyle("list_label"), new GUILayoutOption[]
		{
			GUILayout.Width((float)width)
		});
		if (GUILayout.Button("=>", new GUILayoutOption[0]))
		{
			if (target != max)
			{
				target++;
			}
			else
			{
				target = 0;
			}
		}
		GUILayout.EndHorizontal();
		return target;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0003436C File Offset: 0x0003256C
	public static string TextField(string text, string label, int width)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label(label, new GUILayoutOption[0]);
		float y = new GUIStyle().CalcSize(new GUIContent("asdf")).y;
		Rect rect = GUILayoutUtility.GetRect((float)width, y + 10f);
		text = GUI.TextField(new Rect(rect.x + 4f, rect.y + 2f, rect.width, rect.height), text);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		return text;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x000260BA File Offset: 0x000242BA
	public static string TextField(string text, string label)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label(label, new GUILayoutOption[0]);
		text = GUILayout.TextField(text.ToString(), new GUILayoutOption[]
		{
			GUILayout.Width(50f)
		});
		GUILayout.EndHorizontal();
		return text;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x000343FC File Offset: 0x000325FC
	public static int TextField(int text, string label, int maxL, int min = 0, int max = 255)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label(label, new GUILayoutOption[0]);
		int num = int.Parse(GUILayout.TextField(text.ToString(), maxL, new GUILayoutOption[]
		{
			GUILayout.Width((float)(maxL * 10 + 1))
		}));
		if (num >= min && num <= max)
		{
			text = num;
		}
		GUILayout.EndHorizontal();
		return text;
	}
}
