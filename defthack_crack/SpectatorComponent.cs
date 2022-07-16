using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
[Component]
public class SpectatorComponent : MonoBehaviour
{
	// Token: 0x0600050D RID: 1293 RVA: 0x0003573C File Offset: 0x0003393C
	public void FixedUpdate()
	{
		if (DrawUtilities.ShouldRun())
		{
			if (MiscOptions.SpectatedPlayer != null && !PlayerCoroutines.IsSpying)
			{
				OptimizationVariables.MainPlayer.look.isOrbiting = true;
				OptimizationVariables.MainPlayer.look.orbitPosition = MiscOptions.SpectatedPlayer.transform.position - OptimizationVariables.MainPlayer.transform.position;
				OptimizationVariables.MainPlayer.look.orbitPosition += new Vector3(0f, 3f, 0f);
				return;
			}
			OptimizationVariables.MainPlayer.look.isOrbiting = MiscOptions.Freecam;
		}
	}
}
