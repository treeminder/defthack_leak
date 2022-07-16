using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class VertTriList
{
	// Token: 0x17000035 RID: 53
	public int[] this[int index]
	{
		get
		{
			return this.list[index];
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00026608 File Offset: 0x00024808
	public VertTriList(int[] tri, int numVerts)
	{
		this.Init(tri, numVerts);
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00026618 File Offset: 0x00024818
	public VertTriList(Mesh mesh)
	{
		this.Init(mesh.triangles, mesh.vertexCount);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00036BD4 File Offset: 0x00034DD4
	public void Init(int[] tri, int numVerts)
	{
		int[] array = new int[numVerts];
		for (int i = 0; i < tri.Length; i++)
		{
			array[tri[i]]++;
		}
		this.list = new int[numVerts][];
		for (int j = 0; j < array.Length; j++)
		{
			this.list[j] = new int[array[j]];
		}
		for (int k = 0; k < tri.Length; k++)
		{
			int num = tri[k];
			int[] array2 = this.list[num];
			int[] array3 = array;
			int num2 = num;
			int num3 = array3[num2] - 1;
			array3[num2] = num3;
			array2[num3] = k / 3;
		}
	}

	// Token: 0x040002A0 RID: 672
	public int[][] list;
}
