using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class SerializableVector2
{
	// Token: 0x060004FF RID: 1279 RVA: 0x000263B8 File Offset: 0x000245B8
	public SerializableVector2(float nx, float ny)
	{
		this.x = nx;
		this.y = ny;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x000263CE File Offset: 0x000245CE
	public Vector2 ToVector2()
	{
		return new Vector2(this.x, this.y);
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000263E1 File Offset: 0x000245E1
	public static implicit operator Vector2(SerializableVector2 vector)
	{
		return vector.ToVector2();
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x000263E9 File Offset: 0x000245E9
	public static implicit operator SerializableVector2(Vector2 vector)
	{
		return new SerializableVector2(vector.x, vector.y);
	}

	// Token: 0x04000259 RID: 601
	public float x;

	// Token: 0x0400025A RID: 602
	public float y;
}
