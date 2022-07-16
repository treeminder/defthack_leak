using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class ESPObject
{
	// Token: 0x06000176 RID: 374 RVA: 0x000254C8 File Offset: 0x000236C8
	public ESPObject(ESPTarget t, object o, GameObject go)
	{
		this.Target = t;
		this.Object = o;
		this.GObject = go;
	}

	// Token: 0x04000069 RID: 105
	public ESPTarget Target;

	// Token: 0x0400006A RID: 106
	public object Object;

	// Token: 0x0400006B RID: 107
	public GameObject GObject;
}
