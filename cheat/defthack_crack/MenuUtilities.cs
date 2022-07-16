using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class MenuUtilities
{
	// Token: 0x06000240 RID: 576 RVA: 0x000257BF File Offset: 0x000239BF
	public MenuUtilities()
	{
		MenuUtilities.TexClear.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
		MenuUtilities.TexClear.Apply();
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0002E994 File Offset: 0x0002CB94
	public static void FixGUIStyleColor(GUIStyle style)
	{
		style.normal.background = MenuUtilities.TexClear;
		style.onNormal.background = MenuUtilities.TexClear;
		style.hover.background = MenuUtilities.TexClear;
		style.onHover.background = MenuUtilities.TexClear;
		style.active.background = MenuUtilities.TexClear;
		style.onActive.background = MenuUtilities.TexClear;
		style.focused.background = MenuUtilities.TexClear;
		style.onFocused.background = MenuUtilities.TexClear;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x000257F6 File Offset: 0x000239F6
	public static Rect Inline(Rect rect, int border = 1)
	{
		return new Rect(rect.x + (float)border, rect.y + (float)border, rect.width - (float)(border * 2), rect.height - (float)(border * 2));
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00025829 File Offset: 0x00023A29
	public static Rect AbsRect(Vector2 pos1, Vector2 pos2)
	{
		return MenuUtilities.AbsRect(pos1.x, pos1.y, pos2.x, pos2.y);
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0002EA24 File Offset: 0x0002CC24
	public static Rect AbsRect(float x1, float y1, float x2, float y2)
	{
		float width = y2 - y1;
		float height = x2 - x1;
		return new Rect(x1, y1, width, height);
	}

	// Token: 0x0400013D RID: 317
	public static Texture2D TexClear = new Texture2D(1, 1);
}
