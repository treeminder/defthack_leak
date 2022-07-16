using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public static class VectorUtilities
{
	// Token: 0x0600057D RID: 1405 RVA: 0x000265A7 File Offset: 0x000247A7
	public static double GetDistance(Vector3 point)
	{
		return VectorUtilities.GetDistance(OptimizationVariables.MainCam.transform.position, point);
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00036B1C File Offset: 0x00034D1C
	public static double GetDistance(Vector3 start, Vector3 point)
	{
		Vector3 vector;
		vector.x = start.x - point.x;
		vector.y = start.y - point.y;
		vector.z = start.z - point.z;
		return Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x000265BE File Offset: 0x000247BE
	public static double GetMagnitude(Vector3 vector)
	{
		return Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x000265EF File Offset: 0x000247EF
	public static Vector3 Normalize(Vector3 vector)
	{
		return vector / (float)VectorUtilities.GetMagnitude(vector);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00036B94 File Offset: 0x00034D94
	public static double GetAngleDelta(Vector3 mainPos, Vector3 forward, Vector3 target)
	{
		Vector3 lhs = VectorUtilities.Normalize(target - mainPos);
		return Math.Atan2(VectorUtilities.GetMagnitude(Vector3.Cross(lhs, forward)), (double)Vector3.Dot(lhs, forward)) * 57.29577951308232;
	}
}
