using System;
using System.Globalization;
using System.Linq;
using UnityEngine;

// Token: 0x02000010 RID: 16
internal class ColorUtilities
{
	// Token: 0x0600005D RID: 93 RVA: 0x00025048 File Offset: 0x00023248
	public static void addColor(ColorVariable ColorVariable)
	{
		if (!ColorOptions.DefaultColorDict.ContainsKey(ColorVariable.identity))
		{
			ColorOptions.DefaultColorDict.Add(ColorVariable.identity, ColorVariable);
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00027B3C File Offset: 0x00025D3C
	public static ColorVariable getColor(string identifier)
	{
		ColorVariable colorVariable;
		ColorVariable result;
		if (ColorOptions.ColorDict.TryGetValue(identifier, out colorVariable))
		{
			result = colorVariable;
		}
		else
		{
			result = ColorOptions.errorColor;
		}
		return result;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00027B64 File Offset: 0x00025D64
	public static string getHex(string identifier)
	{
		ColorVariable color;
		string result;
		if (ColorOptions.ColorDict.TryGetValue(identifier, out color))
		{
			result = ColorUtilities.ColorToHex(color);
		}
		else
		{
			result = ColorUtilities.ColorToHex(ColorOptions.errorColor);
		}
		return result;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00027BA0 File Offset: 0x00025DA0
	public static void setColor(string identifier, Color32 color)
	{
		ColorVariable colorVariable;
		if (ColorOptions.ColorDict.TryGetValue(identifier, out colorVariable))
		{
			colorVariable.color = color.ToSerializableColor();
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0002506D File Offset: 0x0002326D
	public static string ColorToHex(Color32 color)
	{
		return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + "FF";
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000250AC File Offset: 0x000232AC
	public static ColorVariable[] getArray()
	{
		return ColorOptions.ColorDict.Values.ToArray<ColorVariable>();
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00027BC8 File Offset: 0x00025DC8
	public static Color32 HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
		return new Color32(r, g, b, byte.MaxValue);
	}
}
