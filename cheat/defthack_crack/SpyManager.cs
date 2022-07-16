using System;
using System.Collections.Generic;
using System.Reflection;
using SosiHui;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class SpyManager
{
	// Token: 0x060000BB RID: 187 RVA: 0x000283D4 File Offset: 0x000265D4
	public static void InvokePre()
	{
		foreach (MethodInfo methodInfo in SpyManager.PreSpy)
		{
			methodInfo.Invoke(null, null);
		}
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00028424 File Offset: 0x00026624
	public static void InvokePost()
	{
		foreach (MethodInfo methodInfo in SpyManager.PostSpy)
		{
			methodInfo.Invoke(null, null);
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00028474 File Offset: 0x00026674
	public static void DestroyComponents()
	{
		foreach (Type type in SpyManager.Components)
		{
			UnityEngine.Object.Destroy(BinaryOperationBinder.HookObject.GetComponent(type));
		}
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000284CC File Offset: 0x000266CC
	public static void AddComponents()
	{
		foreach (Type componentType in SpyManager.Components)
		{
			BinaryOperationBinder.HookObject.AddComponent(componentType);
		}
	}

	// Token: 0x04000049 RID: 73
	public static IEnumerable<MethodInfo> PreSpy;

	// Token: 0x0400004A RID: 74
	public static IEnumerable<Type> Components;

	// Token: 0x0400004B RID: 75
	public static IEnumerable<MethodInfo> PostSpy;
}
