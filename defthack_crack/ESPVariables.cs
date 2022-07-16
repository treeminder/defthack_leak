using System;
using System.Collections.Generic;

// Token: 0x0200002E RID: 46
public class ESPVariables
{
	// Token: 0x040000B0 RID: 176
	public static List<ESPObject> Objects = new List<ESPObject>();

	// Token: 0x040000B1 RID: 177
	public static Queue<ESPBox> DrawBuffer = new Queue<ESPBox>();

	// Token: 0x040000B2 RID: 178
	public static Queue<ESPBox2> DrawBuffer2 = new Queue<ESPBox2>();
}
