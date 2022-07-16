using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

// Token: 0x0200004A RID: 74
public static class MathUtilities
{
	// Token: 0x06000207 RID: 519 RVA: 0x00025718 File Offset: 0x00023918
	[Initializer]
	public static void GenerateRandom()
	{
		MathUtilities.Random = new System.Random();
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0002DFD8 File Offset: 0x0002C1D8
	public static bool Intersect(Vector3 p0, Vector3 p1, Vector3 oVector, Vector3 bCenter, out Vector3 intersection)
	{
		intersection = Vector3.zero;
		Vector3 vector = p1 - p0;
		float num = Vector3.Dot(vector, oVector);
		bool result;
		if (num == 0f)
		{
			result = false;
		}
		else
		{
			float d = -(Vector3.Dot(p0 - bCenter, oVector) / num);
			intersection = p0 + d * vector;
			result = true;
		}
		return result;
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0002E038 File Offset: 0x0002C238
	public static Vector3 GetOrthogonalVector(Vector3 vCenter, Vector3 vPoint)
	{
		Vector3 a = vCenter - vPoint;
		double distance = VectorUtilities.GetDistance(vCenter, vPoint);
		return a / (float)distance;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0002E05C File Offset: 0x0002C25C
	public static Vector3[] GetRectanglePoints(Vector3 playerPos, Vector3[] bCorners, Bounds bound)
	{
		Vector3 orthogonalVector = MathUtilities.GetOrthogonalVector(bound.center, playerPos);
		List<Vector3> list = new List<Vector3>();
		Vector3[] array = new Vector3[]
		{
			bCorners[0],
			bCorners[1],
			bCorners[1],
			bCorners[3],
			bCorners[3],
			bCorners[2],
			bCorners[2],
			bCorners[0],
			bCorners[4],
			bCorners[5],
			bCorners[5],
			bCorners[7],
			bCorners[7],
			bCorners[6],
			bCorners[6],
			bCorners[4],
			bCorners[0],
			bCorners[4],
			bCorners[1],
			bCorners[5],
			bCorners[2],
			bCorners[6],
			bCorners[3],
			bCorners[7]
		};
		for (int i = 0; i < 24; i += 2)
		{
			Vector3 p = array[i];
			Vector3 p2 = array[i + 1];
			Vector3 item;
			if (MathUtilities.Intersect(p, p2, orthogonalVector, bound.center, out item))
			{
				list.Add(item);
			}
		}
		Bounds bounds = new Bounds(bound.center, bound.size * 1.2f);
		for (int j = list.Count - 1; j > -1; j--)
		{
			if (!bounds.Contains(list[j]))
			{
				list.RemoveAt(j);
			}
		}
		return list.ToArray();
	}

	// Token: 0x04000112 RID: 274
	private static readonly WebClient web = new WebClient();

	// Token: 0x04000113 RID: 275
	public static System.Random Random;
}
