using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class OV_UseableBarricade
{
	// Token: 0x060003B6 RID: 950 RVA: 0x00032478 File Offset: 0x00030678
	[Override(typeof(UseableBarricade), "checkSpace", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
	public bool OV_checkSpace()
	{
		if (!MiscOptions.BuildinObstacles || PlayerCoroutines.IsSpying)
		{
			return (bool)OverrideUtilities.CallOriginal(this, new object[0]);
		}
		OverrideUtilities.CallOriginal(this, new object[0]);
		if ((Vector3)OV_UseableBarricade.pointField.GetValue(this) != Vector3.zero && !MiscOptions.Freecam)
		{
			if (MiscOptions.epos)
			{
				OV_UseableBarricade.pointField.SetValue(this, (Vector3)OV_UseableBarricade.pointField.GetValue(this) + MiscOptions.pos);
			}
			return true;
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(OptimizationVariables.MainCam.transform.position, OptimizationVariables.MainCam.transform.forward), out raycastHit, 20f, RayMasks.DAMAGE_CLIENT))
		{
			Vector3 vector = raycastHit.point;
			if (MiscOptions.epos)
			{
				vector += MiscOptions.pos;
			}
			OV_UseableBarricade.pointField.SetValue(this, vector);
			return true;
		}
		Vector3 vector2 = OptimizationVariables.MainCam.transform.position + OptimizationVariables.MainCam.transform.forward * 7f;
		if (MiscOptions.epos)
		{
			vector2 += MiscOptions.pos;
		}
		OV_UseableBarricade.pointField.SetValue(this, vector2);
		return true;
	}

	// Token: 0x040001FD RID: 509
	public static FieldInfo pointField = typeof(UseableBarricade).GetField("point", ReflectionVariables.publicInstance);
}
