using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class Drawing
{
	// Token: 0x060000C6 RID: 198 RVA: 0x0002522D File Offset: 0x0002342D
	public static void DrawRect(Rect position, Color color, GUIContent content = null)
	{
		Color backgroundColor = GUI.backgroundColor;
		GUI.backgroundColor = color;
		GUI.Box(position, content ?? GUIContent.none, Drawing.textureStyle);
		GUI.backgroundColor = backgroundColor;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00025254 File Offset: 0x00023454
	public static void LayoutBox(Color color, GUIContent content = null)
	{
		Color backgroundColor = GUI.backgroundColor;
		GUI.backgroundColor = color;
		GUILayout.Box(content ?? GUIContent.none, Drawing.textureStyle, new GUILayoutOption[0]);
		GUI.backgroundColor = backgroundColor;
	}

	// Token: 0x0400004C RID: 76
	public static Texture2D backgroundTexture = Texture2D.whiteTexture;

	// Token: 0x0400004D RID: 77
	public static GUIStyle textureStyle = new GUIStyle
	{
		normal = new GUIStyleState
		{
			background = Drawing.backgroundTexture
		}
	};
}
