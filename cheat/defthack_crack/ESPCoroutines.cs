using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000024 RID: 36
public static class ESPCoroutines
{
	// Token: 0x06000128 RID: 296 RVA: 0x0002542A File Offset: 0x0002362A
	public static IEnumerator DoChams()
	{
		for (; ; )
		{
			if (!DrawUtilities.ShouldRun() || ESPCoroutines.UnlitChams == null)
			{
				yield return new WaitForSeconds(1f);
			}
			else
			{
				try
				{
					if (ESPOptions.ChamsEnabled)
					{
						ESPCoroutines.EnableChams();
					}
					else
					{
						ESPCoroutines.DisableChams();
					}
				}
				catch (Exception)
				{
				}
				yield return new WaitForSeconds(5f);
			}
		}
		yield break;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0002AE34 File Offset: 0x00029034
	public static void DoChamsGameObject(GameObject pgo, Color32 front, Color32 behind)
	{
		if (!(ESPCoroutines.UnlitChams == null))
		{
			Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].material.shader != ESPCoroutines.LitChams | ESPCoroutines.UnlitChams)
				{
					Material[] materials = componentsInChildren[i].materials;
					for (int j = 0; j < materials.Length; j++)
					{
						materials[j].shader = (ESPOptions.ChamsFlat ? ESPCoroutines.UnlitChams : ESPCoroutines.LitChams);
						materials[j].SetColor("_ColorVisible", new Color32(front.r, front.g, front.b, front.a));
						materials[j].SetColor("_ColorBehind", new Color32(behind.r, behind.g, behind.b, behind.a));
					}
				}
			}
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0002AF28 File Offset: 0x00029128
	[OffSpy]
	public static void EnableChams()
	{
		if (ESPOptions.ChamsEnabled)
		{
			Color32 color = ColorUtilities.getColor("_ChamsFriendVisible");
			Color32 color2 = ColorUtilities.getColor("_ChamsFriendInvisible");
			Color32 color3 = ColorUtilities.getColor("_ChamsEnemyVisible");
			Color32 color4 = ColorUtilities.getColor("_ChamsEnemyInvisible");
			foreach (SteamPlayer steamPlayer in Provider.clients.ToArray())
			{
				Color32 front = FriendUtilities.IsFriendly(steamPlayer.player) ? color : color3;
				Color32 behind = FriendUtilities.IsFriendly(steamPlayer.player) ? color2 : color4;
				Player player = steamPlayer.player;
				if (!(player == null) && !(player == OptimizationVariables.MainPlayer) && !(player.gameObject == null) && !(player.life == null) && !player.life.isDead)
				{
					ESPCoroutines.DoChamsGameObject(player.gameObject, front, behind);
				}
			}
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0002B02C File Offset: 0x0002922C
	[OnSpy]
	public static void DisableChams()
	{
		if (!(ESPCoroutines.Normal == null))
		{
			for (int i = 0; i < Provider.clients.ToArray().Length; i++)
			{
				Player player = Provider.clients.ToArray()[i].player;
				if (!(player == null) && !(player == OptimizationVariables.MainPlayer) && !(player.life == null) && !player.life.isDead)
				{
					Renderer[] componentsInChildren = player.gameObject.GetComponentsInChildren<Renderer>();
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						Material[] materials = componentsInChildren[j].materials;
						for (int k = 0; k < materials.Length; k++)
						{
							if (materials[k].shader != ESPCoroutines.Normal)
							{
								if (!materials[k].name.Contains("Clothes"))
								{
									materials[k].shader = Shader.Find("Standard");
								}
								else
								{
									materials[k].shader = ESPCoroutines.Normal;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00025432 File Offset: 0x00023632
	public static IEnumerator UpdateObjectList()
	{
		for (; ; )
		{
			if (!DrawUtilities.ShouldRun())
			{
				yield return new WaitForSeconds(2f);
			}
			else
			{
				List<ESPObject> objects = ESPVariables.Objects;
				objects.Clear();

				List<ESPTarget> targets =
					ESPOptions.PriorityTable.Keys.OrderByDescending(k => ESPOptions.PriorityTable[k]).ToList();
				
				int num;
				for (int i = 0; i < targets.Count; i = num + 1)
				{
					ESPTarget target = targets[i];
					ESPVisual vis = ESPOptions.VisualOptions[(int)target];
					if (vis.Enabled)
					{
						Vector3 pPos = OptimizationVariables.MainPlayer.transform.position;
						switch (target)
						{
							case ESPTarget.Игроки:
								{
									SteamPlayer[] objarray = (from p in Provider.clients
															  orderby VectorUtilities.GetDistance(pPos, p.player.transform.position) descending
															  select p).ToArray<SteamPlayer>();
									if (vis.UseObjectCap)
									{
										objarray = objarray.TakeLast(vis.ObjectCap).ToArray<SteamPlayer>();
									}
									for (int j = 0; j < objarray.Length; j = num + 1)
									{
										Player plr = objarray[j].player;
										if (!plr.life.isDead && !(plr == OptimizationVariables.MainPlayer))
										{
											objects.Add(new ESPObject(target, plr, plr.gameObject));
										}
										num = j;
									}
									break;
								}
							case ESPTarget.Зомби:
								{
									Zombie[] objarr = (from obj in ZombieManager.regions.SelectMany((ZombieRegion r) => r.zombies)
													   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
													   select obj).ToArray<Zombie>();
									if (vis.UseObjectCap)
									{
										objarr = objarr.TakeLast(vis.ObjectCap).ToArray<Zombie>();
									}
									for (int k2 = 0; k2 < objarr.Length; k2 = num + 1)
									{
										Zombie obj9 = objarr[k2];
										objects.Add(new ESPObject(target, obj9, obj9.gameObject));
										num = k2;
									}
									break;
								}
							case ESPTarget.Предметы:
								{
									InteractableItem[] objarr2 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableItem>()
																  orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																  select obj).ToArray<InteractableItem>();
									if (vis.UseObjectCap)
									{
										objarr2 = objarr2.TakeLast(vis.ObjectCap).ToArray<InteractableItem>();
									}
									for (int l = 0; l < objarr2.Length; l = num + 1)
									{
										InteractableItem obj10 = objarr2[l];
										if (ItemUtilities.Whitelisted(obj10.asset, ItemOptions.ItemESPOptions) || !ESPOptions.FilterItems)
										{
											objects.Add(new ESPObject(target, obj10, obj10.gameObject));
										}
										num = l;
									}
									break;
								}
							case ESPTarget.Турели:
								{
									InteractableSentry[] objarr3 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSentry>()
																	orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																	select obj).ToArray<InteractableSentry>();
									if (vis.UseObjectCap)
									{
										objarr3 = objarr3.TakeLast(vis.ObjectCap).ToArray<InteractableSentry>();
									}
									for (int m = 0; m < objarr3.Length; m = num + 1)
									{
										InteractableSentry obj11 = objarr3[m];
										objects.Add(new ESPObject(target, obj11, obj11.gameObject));
										num = m;
									}
									break;
								}
							case ESPTarget.Кровати:
								{
									InteractableBed[] objarr4 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableBed>()
																 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																 select obj).ToArray<InteractableBed>();
									if (vis.UseObjectCap)
									{
										objarr4 = objarr4.TakeLast(vis.ObjectCap).ToArray<InteractableBed>();
									}
									for (int n = 0; n < objarr4.Length; n = num + 1)
									{
										InteractableBed obj12 = objarr4[n];
										objects.Add(new ESPObject(target, obj12, obj12.gameObject));
										num = n;
									}
									break;
								}
							case ESPTarget.КлеймФлаги:
								{
									InteractableClaim[] objarr5 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableClaim>()
																   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																   select obj).ToArray<InteractableClaim>();
									if (vis.UseObjectCap)
									{
										objarr5 = objarr5.TakeLast(vis.ObjectCap).ToArray<InteractableClaim>();
									}
									for (int j2 = 0; j2 < objarr5.Length; j2 = num + 1)
									{
										InteractableClaim obj13 = objarr5[j2];
										objects.Add(new ESPObject(target, obj13, obj13.gameObject));
										num = j2;
									}
									break;
								}
							case ESPTarget.Транспорт:
								{
									InteractableVehicle[] objarr6 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableVehicle>()
																	 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																	 select obj).ToArray<InteractableVehicle>();
									if (vis.UseObjectCap)
									{
										objarr6 = objarr6.TakeLast(vis.ObjectCap).ToArray<InteractableVehicle>();
									}
									for (int j3 = 0; j3 < objarr6.Length; j3 = num + 1)
									{
										InteractableVehicle obj14 = objarr6[j3];
										if (!obj14.isDead)
										{
											objects.Add(new ESPObject(target, obj14, obj14.gameObject));
										}
										num = j3;
									}
									break;
								}
							case ESPTarget.Ящики:
								{
									InteractableStorage[] objarr7 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableStorage>()
																	 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																	 select obj).ToArray<InteractableStorage>();
									if (vis.UseObjectCap)
									{
										objarr7 = objarr7.TakeLast(vis.ObjectCap).ToArray<InteractableStorage>();
									}
									for (int j4 = 0; j4 < objarr7.Length; j4 = num + 1)
									{
										InteractableStorage obj15 = objarr7[j4];
										objects.Add(new ESPObject(target, obj15, obj15.gameObject));
										num = j4;
									}
									break;
								}
							case ESPTarget.Генераторы:
								{
									InteractableGenerator[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableGenerator>()
																	   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																	   select obj).ToArray<InteractableGenerator>();
									if (vis.UseObjectCap)
									{
										objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableGenerator>();
									}
									for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
									{
										InteractableGenerator obj16 = objarr8[j5];
										objects.Add(new ESPObject(target, obj16, obj16.gameObject));
										num = j5;
									}
									break;
								}
							case ESPTarget.Животные:
								{
									Animal[] objarr9 = (from obj in UnityEngine.Object.FindObjectsOfType<Animal>()
														orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
														select obj).ToArray<Animal>();
									if (vis.UseObjectCap)
									{
										objarr9 = objarr9.TakeLast(vis.ObjectCap).ToArray<Animal>();
									}
									for (int j6 = 0; j6 < objarr9.Length; j6 = num + 1)
									{
										Animal obj17 = objarr9[j6];
										objects.Add(new ESPObject(target, obj17, obj17.gameObject));
										num = j6;
									}
									break;
								}
							case ESPTarget.Ловшуки:
								{
									InteractableTrap[] objarr10 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableTrap>()
																   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																   select obj).ToArray<InteractableTrap>();
									if (vis.UseObjectCap)
									{
										objarr10 = objarr10.TakeLast(vis.ObjectCap).ToArray<InteractableTrap>();
									}
									for (int j7 = 0; j7 < objarr10.Length; j7 = num + 1)
									{
										InteractableTrap obj18 = objarr10[j7];
										objects.Add(new ESPObject(target, obj18, obj18.gameObject));
										num = j7;
									}
									break;
								}
							case ESPTarget.Аирдропы:
								{
									Carepackage[] objarr11 = (from obj in UnityEngine.Object.FindObjectsOfType<Carepackage>()
															  orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
															  select obj).ToArray<Carepackage>();
									if (vis.UseObjectCap)
									{
										objarr11 = objarr11.TakeLast(vis.ObjectCap).ToArray<Carepackage>();
									}
									for (int j8 = 0; j8 < objarr11.Length; j8 = num + 1)
									{
										Carepackage obj19 = objarr11[j8];
										objects.Add(new ESPObject(target, obj19, obj19.gameObject));
										num = j8;
									}
									break;
								}
							case ESPTarget.Двери:
								{
									InteractableDoorHinge[] objarr12 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableDoorHinge>()
																		orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																		select obj).ToArray<InteractableDoorHinge>();
									if (vis.UseObjectCap)
									{
										objarr12 = objarr12.TakeLast(vis.ObjectCap).ToArray<InteractableDoorHinge>();
									}
									for (int j9 = 0; j9 < objarr12.Length; j9 = num + 1)
									{
										InteractableDoorHinge obj20 = objarr12[j9];
										objects.Add(new ESPObject(target, obj20, obj20.gameObject));
										num = j9;
									}
									break;
								}
							case ESPTarget.Ягоды:
								{
									InteractableForage[] objarr13 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableForage>()
																	 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																	 select obj).ToArray<InteractableForage>();
									if (vis.UseObjectCap)
									{
										objarr13 = objarr13.TakeLast(vis.ObjectCap).ToArray<InteractableForage>();
									}
									for (int j10 = 0; j10 < objarr13.Length; j10 = num + 1)
									{
										InteractableForage obj21 = objarr13[j10];
										objects.Add(new ESPObject(target, obj21, obj21.gameObject));
										num = j10;
									}
									break;
								}
							case ESPTarget.Растения:
								{
									InteractableFarm[] objarr14 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableFarm>()
																   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																   select obj).ToArray<InteractableFarm>();
									if (vis.UseObjectCap)
									{
										objarr14 = objarr14.TakeLast(vis.ObjectCap).ToArray<InteractableFarm>();
									}
									for (int j11 = 0; j11 < objarr14.Length; j11 = num + 1)
									{
										InteractableFarm obj22 = objarr14[j11];
										objects.Add(new ESPObject(target, obj22, obj22.gameObject));
										num = j11;
									}
									break;
								}
							case ESPTarget.C4:
								{
									InteractableCharge[] objarr15 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableCharge>()
																	 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																	 select obj).ToArray<InteractableCharge>();
									if (vis.UseObjectCap)
									{
										objarr15 = objarr15.TakeLast(vis.ObjectCap).ToArray<InteractableCharge>();
									}
									for (int j12 = 0; j12 < objarr15.Length; j12 = num + 1)
									{
										InteractableCharge obj23 = objarr15[j12];
										objects.Add(new ESPObject(target, obj23, obj23.gameObject));
										num = j12;
									}
									break;
								}
							case ESPTarget.Fire:
								{
									InteractableFire[] objarr16 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableFire>()
																   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																   select obj).ToArray<InteractableFire>();
									if (vis.UseObjectCap)
									{
										objarr16 = objarr16.TakeLast(vis.ObjectCap).ToArray<InteractableFire>();
									}
									for (int j13 = 0; j13 < objarr16.Length; j13 = num + 1)
									{
										InteractableFire obj24 = objarr16[j13];
										objects.Add(new ESPObject(target, obj24, obj24.gameObject));
										num = j13;
									}
									break;
								}
							case ESPTarget.Лампы:
								{
									InteractableSpot[] objarr17 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSpot>()
																   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																   select obj).ToArray<InteractableSpot>();
									if (vis.UseObjectCap)
									{
										objarr17 = objarr17.TakeLast(vis.ObjectCap).ToArray<InteractableSpot>();
									}
									for (int j14 = 0; j14 < objarr17.Length; j14 = num + 1)
									{
										InteractableSpot obj25 = objarr17[j14];
										objects.Add(new ESPObject(target, obj25, obj25.gameObject));
										num = j14;
									}
									break;
								}
							case ESPTarget.Топливо:
								{
									InteractableObjectResource[] objarr18 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableObjectResource>()
																			 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																			 select obj).ToArray<InteractableObjectResource>();
									if (vis.UseObjectCap)
									{
										objarr18 = objarr18.TakeLast(vis.ObjectCap).ToArray<InteractableObjectResource>();
									}
									for (int j15 = 0; j15 < objarr18.Length; j15 = num + 1)
									{
										InteractableObjectResource obj26 = objarr18[j15];
										objects.Add(new ESPObject(target, obj26, obj26.gameObject));
										num = j15;
									}
									break;
								}
							case ESPTarget.Генератор_безопасной_зоны:
								{
									InteractableSafezone[] objarr19 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSafezone>()
																	   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																	   select obj).ToArray<InteractableSafezone>();
									if (vis.UseObjectCap)
									{
										objarr19 = objarr19.TakeLast(vis.ObjectCap).ToArray<InteractableSafezone>();
									}
									for (int j16 = 0; j16 < objarr19.Length; j16 = num + 1)
									{
										InteractableSafezone obj27 = objarr19[j16];
										objects.Add(new ESPObject(target, obj27, obj27.gameObject));
										num = j16;
									}
									break;
								}
							case ESPTarget.Генератор_Воздуха:
								{
									InteractableOxygenator[] objarr20 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableOxygenator>()
																		 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																		 select obj).ToArray<InteractableOxygenator>();
									if (vis.UseObjectCap)
									{
										objarr20 = objarr20.TakeLast(vis.ObjectCap).ToArray<InteractableOxygenator>();
									}
									for (int j17 = 0; j17 < objarr20.Length; j17 = num + 1)
									{
										InteractableOxygenator obj28 = objarr20[j17];
										objects.Add(new ESPObject(target, obj28, obj28.gameObject));
										num = j17;
									}
									break;
								}
							case ESPTarget.NPC:
								{
									ResourceManager[] objarr21 = (from obj in UnityEngine.Object.FindObjectsOfType<ResourceManager>()
																  orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
																  select obj).ToArray<ResourceManager>();
									if (vis.UseObjectCap)
									{
										objarr21 = objarr21.TakeLast(vis.ObjectCap).ToArray<ResourceManager>();
									}
									for (int j18 = 0; j18 < objarr21.Length; j18 = num + 1)
									{
										ResourceManager obj29 = objarr21[j18];
										objects.Add(new ESPObject(target, obj29, obj29.gameObject));
										num = j18;
									}
									break;
								}
						}
					}
					num = i;
				}
				yield return new WaitForSeconds(5f);
			}
		}
		yield break;
	}

	// Token: 0x0400005D RID: 93
	public static InteractableObjectNPC[] Hcx;

	// Token: 0x0400005E RID: 94
	public static Shader LitChams;

	// Token: 0x0400005F RID: 95
	public static Shader UnlitChams;

	// Token: 0x04000060 RID: 96
	public static Shader Normal;
}
