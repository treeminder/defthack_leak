using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class OV_Player : MonoBehaviour
{
	// Token: 0x0600035C RID: 860 RVA: 0x00025D84 File Offset: 0x00023F84
	[Override(typeof(Player), "ReceiveTakeScreenshot", BindingFlags.Instance | BindingFlags.Public, 0)]
	public void OV_askScreenshot()
	{
		base.StartCoroutine(PlayerCoroutines.TakeScreenshot());
	}
}
