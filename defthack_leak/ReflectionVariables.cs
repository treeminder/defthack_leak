using System;
using System.Reflection;

// Token: 0x02000098 RID: 152
public static class ReflectionVariables
{
	// Token: 0x0400024D RID: 589
	public static BindingFlags PublicInstance = BindingFlags.Instance | BindingFlags.Public;

	// Token: 0x0400024E RID: 590
	public static BindingFlags publicInstance = BindingFlags.Instance | BindingFlags.NonPublic;

	// Token: 0x0400024F RID: 591
	public static BindingFlags PublicStatic = BindingFlags.Static | BindingFlags.Public;

	// Token: 0x04000250 RID: 592
	public static BindingFlags publicStatic = BindingFlags.Static | BindingFlags.NonPublic;

	// Token: 0x04000251 RID: 593
	public static BindingFlags Everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
}
