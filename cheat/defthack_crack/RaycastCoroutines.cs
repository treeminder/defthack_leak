using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class RaycastCoroutines
{
	// Token: 0x060004B6 RID: 1206 RVA: 0x0002621E File Offset: 0x0002441E

public static IEnumerator UpdateObjects()
		{
			for (; ; )
			{
				if (!DrawUtilities.ShouldRun())
				{
					RaycastUtilities.Objects = new GameObject[0];
					yield return new WaitForSeconds(1f);
				}
				else
				{
					try
					{
						ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
						float num = (itemGunAsset != null) ? itemGunAsset.range : 15.5f;
						num += 10f;
						GameObject[] array = (from c in Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, num)
											  select c.gameObject).ToArray<GameObject>();
						switch (RaycastOptions.Target)
						{
							case TargetPriority.Players:
								{
									RaycastCoroutines.CachedPlayers.Clear();
									GameObject[] array2 = array;
									int num2;
									for (int i = 0; i < array2.Length; i = num2 + 1)
									{
										Player player = DamageTool.getPlayer(array2[i].transform);
										if (!(player == null) && !RaycastCoroutines.CachedPlayers.Contains(player) && !(player == OptimizationVariables.MainPlayer) && !player.life.isDead)
										{
											RaycastCoroutines.CachedPlayers.Add(player);
										}
										num2 = i;
									}
									RaycastUtilities.Objects = (from c in RaycastCoroutines.CachedPlayers
																select c.gameObject).ToArray<GameObject>();
									break;
								}
							case TargetPriority.Zombies:
								RaycastUtilities.Objects = (from g in array
															where g.GetComponent<Zombie>() != null
															select g).ToArray<GameObject>();
								break;
							case TargetPriority.Sentries:
								RaycastUtilities.Objects = (from g in array
															where g.GetComponent<InteractableSentry>() != null
															select g).ToArray<GameObject>();
								break;
							case TargetPriority.Beds:
								RaycastUtilities.Objects = (from g in array
															where g.GetComponent<InteractableBed>() != null
															select g).ToArray<GameObject>();
								break;
							case TargetPriority.ClaimFlags:
								RaycastUtilities.Objects = (from g in array
															where g.GetComponent<InteractableClaim>() != null
															select g).ToArray<GameObject>();
								break;
							case TargetPriority.Storage:
								RaycastUtilities.Objects = (from g in array
															where g.GetComponent<InteractableStorage>() != null
															select g).ToArray<GameObject>();
								break;
							case TargetPriority.Vehicles:
								RaycastUtilities.Objects = (from g in array
															where g.GetComponent<InteractableVehicle>() != null
															select g).ToArray<GameObject>();
								break;
						}
					}
					catch (Exception)
					{
					}
					yield return new WaitForSeconds(2f);
				}
			}
			yield break;
		}
	public static List<Player> CachedPlayers = new List<Player>();
}