using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class SerializableVector
{
	// Token: 0x060004FC RID: 1276 RVA: 0x0002637A File Offset: 0x0002457A
	public SerializableVector(float nx, float ny, float nz)
	{
		this.x = nx;
		this.y = ny;
		this.z = nz;
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00026397 File Offset: 0x00024597
	public Vector3 ToVector()
	{
		return new Vector3(this.x, this.y, this.z);
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x000263B0 File Offset: 0x000245B0
	public static implicit operator Vector3(SerializableVector vector)
	{
		return vector.ToVector();
	}

	// Token: 0x04000256 RID: 598
	public float x;

	// Token: 0x04000257 RID: 599
	public float y;

	// Token: 0x04000258 RID: 600
	public float z;
}
