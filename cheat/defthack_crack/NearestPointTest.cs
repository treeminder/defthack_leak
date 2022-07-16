using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
public static class NearestPointTest
{
	// Token: 0x060002E4 RID: 740 RVA: 0x00030A88 File Offset: 0x0002EC88
	public static Vector3 NearestPointOnMesh(Vector3 pt, Vector3[] verts, int[] tri, VertTriList vt)
	{
		int num = -1;
		float num2 = 100000000f;
		for (int i = 0; i < verts.Length; i++)
		{
			float sqrMagnitude = (verts[i] - pt).sqrMagnitude;
			if (sqrMagnitude < num2)
			{
				num = i;
				num2 = sqrMagnitude;
			}
		}
		Vector3 result;
		if (num != -1)
		{
			int[] array = vt[num];
			Vector3 vector = Vector3.zero;
			num2 = 100000000f;
			for (int j = 0; j < array.Length; j++)
			{
				int num3 = array[j] * 3;
				Vector3 vector2 = verts[tri[num3]];
				Vector3 vector3 = verts[tri[num3 + 1]];
				Vector3 vector4 = verts[tri[num3 + 2]];
				Vector3 vector5 = NearestPointTest.NearestPointOnTri(pt, vector2, vector3, vector4);
				float sqrMagnitude2 = (pt - vector5).sqrMagnitude;
				if (sqrMagnitude2 < num2)
				{
					vector = vector5;
					num2 = sqrMagnitude2;
				}
			}
			result = vector;
		}
		else
		{
			result = Vector3.zero;
		}
		return result;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00030B68 File Offset: 0x0002ED68
	public static Vector3 NearestPointOnTri(Vector3 pt, Vector3 a, Vector3 b, Vector3 c)
	{
		Vector3 lhs = b - a;
		Vector3 vector = c - a;
		Vector3 lhs2 = c - b;
		float magnitude = lhs.magnitude;
		float magnitude2 = vector.magnitude;
		float magnitude3 = lhs2.magnitude;
		Vector3 vector2 = pt - a;
		Vector3 rhs = pt - b;
		Vector3 vector3 = pt - c;
		Vector3 vector4 = lhs / magnitude;
		Vector3 normalized = Vector3.Cross(lhs, vector).normalized;
		Vector3 rhs2 = Vector3.Cross(normalized, vector4);
		Vector3 lhs3 = Vector3.Cross(lhs, vector2);
		Vector3 lhs4 = Vector3.Cross(vector, -vector3);
		Vector3 lhs5 = Vector3.Cross(lhs2, rhs);
		bool flag = Vector3.Dot(lhs3, normalized) > 0f;
		bool flag2 = Vector3.Dot(lhs4, normalized) > 0f;
		bool flag3 = Vector3.Dot(lhs5, normalized) > 0f;
		Vector3 result;
		if (flag && flag2 && flag3)
		{
			float d = Vector3.Dot(vector2, vector4);
			float d2 = Vector3.Dot(vector2, rhs2);
			result = a + vector4 * d + rhs2 * d2;
		}
		else
		{
			Vector3 lhs6 = vector4;
			Vector3 normalized2 = vector.normalized;
			Vector3 normalized3 = lhs2.normalized;
			float d3 = Mathf.Clamp(Vector3.Dot(lhs6, vector2), 0f, magnitude);
			float d4 = Mathf.Clamp(Vector3.Dot(normalized2, vector2), 0f, magnitude2);
			float d5 = Mathf.Clamp(Vector3.Dot(normalized3, rhs), 0f, magnitude3);
			Vector3 vector5 = a + d3 * lhs6;
			Vector3 vector6 = a + d4 * normalized2;
			Vector3 vector7 = b + d5 * normalized3;
			float sqrMagnitude = (pt - vector5).sqrMagnitude;
			float sqrMagnitude2 = (pt - vector6).sqrMagnitude;
			float sqrMagnitude3 = (pt - vector7).sqrMagnitude;
			result = ((sqrMagnitude < sqrMagnitude2) ? ((sqrMagnitude < sqrMagnitude3) ? vector5 : vector7) : ((sqrMagnitude2 < sqrMagnitude3) ? vector6 : vector7));
		}
		return result;
	}

	// Token: 0x040001A7 RID: 423
	public static Vector3 a;

	// Token: 0x040001A8 RID: 424
	public static Vector3 b;

	// Token: 0x040001A9 RID: 425
	public static Vector3 c;

	// Token: 0x040001AA RID: 426
	public static Vector3 pt;
}
