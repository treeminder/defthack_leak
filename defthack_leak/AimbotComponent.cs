using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
[Component]
public class AimbotComponent : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00024E54 File Offset: 0x00023054
	public void Start()
	{
		CoroutineComponent.LockCoroutine = base.StartCoroutine(AimbotCoroutines.SetLockedObject());
		CoroutineComponent.AimbotCoroutine = base.StartCoroutine(AimbotCoroutines.AimToObject());
		base.StartCoroutine(RaycastCoroutines.UpdateObjects());
	}
}
