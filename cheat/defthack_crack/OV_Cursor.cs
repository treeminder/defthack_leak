using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200006F RID: 111
public static class OV_Cursor
{
	// Token: 0x0600033F RID: 831 RVA: 0x00025CA4 File Offset: 0x00023EA4
	[Override(typeof(Cursor), "set_lockState", BindingFlags.Static | BindingFlags.Public, 0)]
	public static void OV_set_lockState(CursorLockMode rMode)
	{
		if (!MenuComponent.IsInMenu || PlayerCoroutines.IsSpying || (rMode != CursorLockMode.Confined && rMode != CursorLockMode.Locked))
		{
			OverrideUtilities.CallOriginal(null, new object[]
			{
				rMode
			});
		}
	}
}
