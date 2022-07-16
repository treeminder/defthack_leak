using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public static class OV_Physics
{
	// Token: 0x0600035B RID: 859 RVA: 0x00025D44 File Offset: 0x00023F44
	public static bool OV_Linecast(Vector3 start, Vector3 end, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return !OV_Physics.ForceReturnFalse && (bool)OverrideUtilities.CallOriginal(null, new object[]
		{
			start,
			end,
			layerMask,
			queryTriggerInteraction
		});
	}

	// Token: 0x040001D9 RID: 473
	public static bool ForceReturnFalse;
}
