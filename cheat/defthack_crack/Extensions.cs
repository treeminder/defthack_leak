using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000030 RID: 48
public static class Extensions
{
	// Token: 0x06000182 RID: 386 RVA: 0x00025514 File Offset: 0x00023714
	public static Color Invert(this Color32 color)
	{
		return new Color((float)(byte.MaxValue - color.r), (float)(byte.MaxValue - color.g), (float)(byte.MaxValue - color.b));
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00025542 File Offset: 0x00023742
	public static SerializableColor ToSerializableColor(this Color32 c)
	{
		return new SerializableColor((int)c.r, (int)c.g, (int)c.b, (int)c.a);
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00025561 File Offset: 0x00023761
	public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
	{
		return source.Skip(Math.Max(0, source.Count<T>() - N));
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0002BF20 File Offset: 0x0002A120
	public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> target)
	{
		foreach (T item in target)
		{
			source.Add(item);
		}
	}
}
