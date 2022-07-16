using System;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000047 RID: 71
public static class LocationUtilities
{
	// Token: 0x060001F9 RID: 505 RVA: 0x0002DEA0 File Offset: 0x0002C0A0
	public static LocationNode GetClosestLocation(Vector3 pos)
	{
		double num = 1337420.0;
		LocationNode result = null;
		foreach (LocationNode locationNode in (from n in LevelNodes.nodes
		where n.type == ENodeType.LOCATION
		select (LocationNode)n).ToArray<LocationNode>())
		{
			double distance = VectorUtilities.GetDistance(pos, locationNode.point);
			if (distance < num)
			{
				num = distance;
				result = locationNode;
			}
		}
		return result;
	}
}
