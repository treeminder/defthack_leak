using System;
using UnityEngine;

namespace SosiHui
{
	// Token: 0x020000C0 RID: 192
	public static class BinaryOperationBinder
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x00026779 File Offset: 0x00024979
		public static void DynamicObject()
		{
			BinaryOperationBinder.HookObject = new GameObject();
			UnityEngine.Object.DontDestroyOnLoad(BinaryOperationBinder.HookObject);
			ConfigManager.Init();
			AttributeManager.Init();
			AssetManager.Init();
		}

		// Token: 0x040002DE RID: 734
		public static GameObject HookObject;
	}
}
