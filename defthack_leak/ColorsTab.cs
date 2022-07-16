using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200000D RID: 13
public static class ColorsTab
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600004F RID: 79 RVA: 0x00024FF7 File Offset: 0x000231F7
	// (set) Token: 0x06000050 RID: 80 RVA: 0x00024FFE File Offset: 0x000231FE
	public static Color LastColorPreview1
	{
		get
		{
			return ColorsTab.LastColorPreview;
		}
		set
		{
			ColorsTab.LastColorPreview = value;
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000274C8 File Offset: 0x000256C8
	public static void Tab()
	{
		if (ColorOptions.selectedOption == null)
		{
			ColorOptions.previewselected = ColorOptions.preview;
		}
		Prefab.ScrollView(new Rect(0f, 0f, 250f, 385f), ref ColorsTab.ColorScroll, delegate()
		{
			GUILayout.Space(10f);
			List<KeyValuePair<string, ColorVariable>> list = ColorOptions.ColorDict.ToList<KeyValuePair<string, ColorVariable>>();
			list.Sort((KeyValuePair<string, ColorVariable> pair1, KeyValuePair<string, ColorVariable> pair2) => string.Compare(pair1.Value.name, pair2.Value.name, StringComparison.Ordinal));
			for (int i = 0; i < list.Count; i++)
			{
				ColorVariable color = ColorUtilities.getColor(list[i].Value.identity);
				if (Prefab.ColorButton(205f, color, 25f, new GUILayoutOption[0]))
				{
					ColorOptions.selectedOption = color.identity;
					ColorOptions.previewselected = new ColorVariable(color);
					ColorsTab.LastColorPreview1 = color.color;
				}
				GUILayout.Space(3f);
			}
			if (GUILayout.Button(MenuComponent._isRus ? "Восстановить по умолчанию" : "Restore to default", new GUILayoutOption[0]))
			{
				for (int j = 0; j < list.Count; j++)
				{
					ColorVariable color2 = ColorUtilities.getColor(list[j].Value.identity);
					color2.color = color2.origColor;
					ColorOptions.selectedOption = null;
					ColorsTab.LastColorPreview1 = ColorOptions.preview.color;
				}
			}
			GUILayout.Space(10f);
		});
		Rect previewRect = new Rect(255f, 0f, 211f, 120f);
		Prefab.MenuArea(previewRect, MenuComponent._isRus ? "Предпросмотр" : "Preview", delegate()
		{
			Rect rect = MenuUtilities.Inline(new Rect(5f, 20f, previewRect.width - 10f, previewRect.height - 25f), 2);
			Drawing.DrawRect(new Rect(rect.x, rect.y, rect.width / 2f, rect.height), ColorsTab.LastColorPreview1, null);
			Drawing.DrawRect(new Rect(rect.x + rect.width / 2f, rect.y, rect.width / 2f, rect.height), ColorOptions.previewselected, null);
		});
		Prefab.MenuArea(new Rect(previewRect.x, previewRect.y + previewRect.height + 5f, previewRect.width, 436f - previewRect.height - 5f), ColorOptions.previewselected.name, delegate()
		{
			GUILayout.BeginArea(new Rect(10f, 20f, previewRect.width - 10f, 205f));
			ColorOptions.previewselected.color.r = (int)((byte)Prefab.TextField(ColorOptions.previewselected.color.r, "R: ", 30, 0, 255));
			ColorOptions.previewselected.color.r = (int)((byte)Mathf.Round(GUILayout.HorizontalSlider((float)ColorOptions.previewselected.color.r, 0f, 255f, new GUILayoutOption[0])));
			GUILayout.FlexibleSpace();
			ColorOptions.previewselected.color.g = (int)((byte)Prefab.TextField(ColorOptions.previewselected.color.g, "G: ", 30, 0, 255));
			ColorOptions.previewselected.color.g = (int)((byte)Mathf.Round(GUILayout.HorizontalSlider((float)ColorOptions.previewselected.color.g, 0f, 255f, new GUILayoutOption[0])));
			GUILayout.FlexibleSpace();
			ColorOptions.previewselected.color.b = (int)((byte)Prefab.TextField(ColorOptions.previewselected.color.b, "B: ", 30, 0, 255));
			ColorOptions.previewselected.color.b = (int)((byte)Mathf.Round(GUILayout.HorizontalSlider((float)ColorOptions.previewselected.color.b, 0f, 255f, new GUILayoutOption[0])));
			GUILayout.FlexibleSpace();
			if (!ColorOptions.previewselected.disableAlpha)
			{
				ColorOptions.previewselected.color.a = (int)((byte)Prefab.TextField(ColorOptions.previewselected.color.a, "A: ", 30, 0, 255));
				ColorOptions.previewselected.color.a = (int)((byte)Mathf.Round(GUILayout.HorizontalSlider((float)ColorOptions.previewselected.color.a, 0f, 255f, new GUILayoutOption[0])));
			}
			else
			{
				Prefab.TextField(ColorOptions.previewselected.color.a, "A: ", 30, 0, 255);
				GUILayout.HorizontalSlider((float)ColorOptions.previewselected.color.a, 0f, 255f, new GUILayoutOption[0]);
			}
			GUILayout.Space(100f);
			GUILayout.EndArea();
			GUILayout.Space(160f);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(MenuComponent._isRus ? "Отменить" : "Cancel", new GUILayoutOption[0]))
			{
				ColorOptions.selectedOption = null;
				ColorsTab.LastColorPreview1 = ColorOptions.preview.color;
			}
			GUILayout.Space(3f);
			if (GUILayout.Button(MenuComponent._isRus ? "Восстановить" : "Restore", new GUILayoutOption[0]))
			{
				ColorUtilities.setColor(ColorOptions.previewselected.identity, ColorOptions.previewselected.origColor);
				ColorOptions.previewselected.color = ColorOptions.previewselected.origColor;
			}
			GUILayout.Space(3f);
			if (GUILayout.Button(MenuComponent._isRus ? "Применить" : "Apply", new GUILayoutOption[0]))
			{
				ColorUtilities.setColor(ColorOptions.previewselected.identity, ColorOptions.previewselected.color);
				ColorsTab.LastColorPreview1 = ColorOptions.previewselected.color;
			}
			GUILayout.Space(30f);
		});
	}

	// Token: 0x0400002F RID: 47
	public static Vector2 ColorScroll;

	// Token: 0x04000030 RID: 48
	public static Color LastColorPreview = ColorOptions.preview.color;
}
