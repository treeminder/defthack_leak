using SDG.Unturned;
using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000090 RID: 144
[DisallowMultipleComponent]
public class RaycastComponent : MonoBehaviour
{
	// Token: 0x06000497 RID: 1175 RVA: 0x000261B6 File Offset: 0x000243B6
	public void Awake()
	{
		base.StartCoroutine(this.RedoSphere());
		base.StartCoroutine(this.CalcSphere());
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x000261D2 File Offset: 0x000243D2
	IEnumerator CalcSphere()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);

			if (Sphere)
			{
				Rigidbody rb = gameObject.GetComponent<Rigidbody>();

				if (rb)
				{
					float sizeBias = 1 - Provider.ping * rb.velocity.magnitude * 2;
					Sphere.transform.localScale = new Vector3(sizeBias, sizeBias, sizeBias);
				}
			}
		}
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x000261E1 File Offset: 0x000243E1
	public IEnumerator RedoSphere()
	{
		for (; ; )
		{
			UnityEngine.Object sphere = this.Sphere;
			this.Sphere = IcoSphere.Create("HitSphere", SphereOptions.SpherePrediction ? 15.5f : SphereOptions.SphereRadius, (float)SphereOptions.RecursionLevel);
			this.Sphere.layer = 24;
			this.Sphere.transform.parent = base.transform;
			this.Sphere.transform.localPosition = Vector3.zero;
			UnityEngine.Object.Destroy(sphere);
			yield return new WaitForSeconds(1f);
		}
		yield break;
	}

	// Token: 0x04000224 RID: 548
	public GameObject Sphere;
}
