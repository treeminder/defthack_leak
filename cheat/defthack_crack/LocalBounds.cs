using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class LocalBounds
{
	// Token: 0x060001F8 RID: 504 RVA: 0x000256D7 File Offset: 0x000238D7
	public LocalBounds(Vector3 po, Vector3 e)
	{
		this.PosOffset = po;
		this.Extents = e;
	}

	// Token: 0x0400010C RID: 268
	public Vector3 PosOffset;

	// Token: 0x0400010D RID: 269
	public Vector3 Extents;
}
