using System;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class SerializableColor
{
	// Token: 0x060004F6 RID: 1270 RVA: 0x000262ED File Offset: 0x000244ED
	public SerializableColor(int nr, int ng, int nb, int na)
	{
		this.r = nr;
		this.g = ng;
		this.b = nb;
		this.a = na;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00026312 File Offset: 0x00024512
	public SerializableColor(int nr, int ng, int nb)
	{
		this.r = nr;
		this.g = ng;
		this.b = nb;
		this.a = 255;
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0002633A File Offset: 0x0002453A
	public static implicit operator Color32(SerializableColor color)
	{
		return color.ToColor();
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00026342 File Offset: 0x00024542
	public static implicit operator Color(SerializableColor color)
	{
		return color.ToColor();
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0002634F File Offset: 0x0002454F
	public static implicit operator SerializableColor(Color32 color)
	{
		return color.ToSerializableColor();
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x00026357 File Offset: 0x00024557
	public Color32 ToColor()
	{
		return new Color32((byte)this.r, (byte)this.g, (byte)this.b, (byte)this.a);
	}

	// Token: 0x04000252 RID: 594
	public int r;

	// Token: 0x04000253 RID: 595
	public int g;

	// Token: 0x04000254 RID: 596
	public int b;

	// Token: 0x04000255 RID: 597
	public int a;
}
