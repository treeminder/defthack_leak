using System;
using SosiHui;

// Token: 0x02000014 RID: 20
public static class AssetManager
{
	// Token: 0x0600006D RID: 109 RVA: 0x00025151 File Offset: 0x00023351
	public static void Init()
	{
		BinaryOperationBinder.HookObject.GetComponent<CoroutineComponent>().StartCoroutine(LoaderCoroutines.LoadAssets());
	}
}
