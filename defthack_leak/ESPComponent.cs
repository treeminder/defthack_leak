using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000022 RID: 34
[Component]
[SpyComponent]
public class ESPComponent : MonoBehaviour
{
	// Token: 0x060000F9 RID: 249 RVA: 0x0002965C File Offset: 0x0002785C
	[Initializer]
	public static void Initialize()
	{
		for (int i = 0; i < ESPOptions.VisualOptions.Length; i++)
		{
			ESPTarget esptarget = (ESPTarget)i;
			Color32 color = Color.red;
			ColorUtilities.addColor(new ColorVariable(string.Format("_{0}", esptarget), string.Format(MenuComponent._isRus ? "ВХ-{0}" : "WH-{0}", esptarget), color, false));
			ColorUtilities.addColor(new ColorVariable(string.Format("_{0}_Outline", esptarget), string.Format(MenuComponent._isRus ? "ВХ-{0}(Контур)" : "WH-{0}(Outline)", esptarget), Color.black, false));
			ColorUtilities.addColor(new ColorVariable(string.Format("_{0}_Glow", esptarget), string.Format(MenuComponent._isRus ? "ВХ-{0}(ОБВОДКА)" : "WH-{0}(Glow)", esptarget), Color.yellow, false));
		}
		ColorUtilities.addColor(new ColorVariable("_ESPFriendly", MenuComponent._isRus ? "Друзья" : "Friends", Color.green, false));
		ColorUtilities.addColor(new ColorVariable("_ChamsFriendVisible", MenuComponent._isRus ? "Чамсы-Видимый друг" : "Chams-Visible friend", Color.green, false));
		ColorUtilities.addColor(new ColorVariable("_ChamsFriendInisible", MenuComponent._isRus ? "Чамсы-Невидимый друг" : "Chams-Invisible Friend", Color.blue, false));
		ColorUtilities.addColor(new ColorVariable("_ChamsEnemyVisible", MenuComponent._isRus ? "Чамсы-Видимый враг" : "Chams-Visible enemy", new Color32(byte.MaxValue, 165, 0, byte.MaxValue), false));
		ColorUtilities.addColor(new ColorVariable("_ChamsEnemyInvisible", MenuComponent._isRus ? "Чамсы-Невидимый враг" : "Chams-Invisible enemy", Color.red, false));
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0002531A File Offset: 0x0002351A
	public void Start()
	{
		CoroutineComponent.ESPCoroutine = base.StartCoroutine(ESPCoroutines.UpdateObjectList());
		CoroutineComponent.ChamsCoroutine = base.StartCoroutine(ESPCoroutines.DoChams());
		ESPComponent.MainCamera = OptimizationVariables.MainCam;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0002983C File Offset: 0x00027A3C
	public void Update()
	{
		WeatherAssetBase activeWeatherAsset = LevelLighting.GetActiveWeatherAsset();
		if (activeWeatherAsset != null && ((MiscOptions.NoRain && activeWeatherAsset.GUID == WeatherAssetBase.DEFAULT_RAIN.GUID) || (MiscOptions.NoSnow && activeWeatherAsset.GUID == WeatherAssetBase.DEFAULT_SNOW.GUID)))
		{
			MethodBase methodBase = ESPComponent.setActiveWeatherAsset;
			object obj = null;
			object[] array = new object[3];
			array[1] = 0f;
			methodBase.Invoke(obj, array);
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x000298B0 File Offset: 0x00027AB0
	public void OnGUI()
	{
		if (Event.current.type == EventType.Repaint && ESPOptions.Enabled && DrawUtilities.ShouldRun())
		{
			GUI.depth = 1;
			if (ESPComponent.MainCamera == null)
			{
				ESPComponent.MainCamera = OptimizationVariables.MainCam;
			}
			Vector3 position = OptimizationVariables.MainPlayer.transform.position;
			Vector3 position2 = OptimizationVariables.MainPlayer.look.aim.position;
			Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
			for (int i = 0; i < ESPVariables.Objects.Count; i++)
			{
				ESPObject espobject = ESPVariables.Objects[i];
				ESPVisual espvisual = ESPOptions.VisualOptions[(int)espobject.Target];
				GameObject gobject = espobject.GObject;
				if (!espvisual.Enabled)
				{
					Highlighter component = gobject.GetComponent<Highlighter>();
					if (component != null && component != TrajectoryComponent.Highlighted)
					{
						component.ConstantOffImmediate();
					}
				}
				else if (espobject.Target != ESPTarget.Предметы || !ESPOptions.FilterItems || ItemUtilities.Whitelisted(((InteractableItem)espobject.Object).asset, ItemOptions.ItemESPOptions))
				{
					Color color = ColorUtilities.getColor(string.Format("_{0}", espobject.Target));
					LabelLocation location = espvisual.Location;
					if (!(gobject == null))
					{
						Vector3 position3 = gobject.transform.position;
						double distance = VectorUtilities.GetDistance(position3, position);
						if (distance >= 0.5 && (distance <= (double)espvisual.Distance || espvisual.InfiniteDistance))
						{
							Vector3 vector = ESPComponent.MainCamera.WorldToScreenPoint(position3);
							if (vector.z > 0f)
							{
								Vector3 localScale = gobject.transform.localScale;
								ESPTarget target = espobject.Target;
								Bounds bounds;
								if (target > ESPTarget.Зомби)
								{
									if (target != ESPTarget.Транспорт)
									{
										bounds = gobject.GetComponent<Collider>().bounds;
									}
									else
									{
										bounds = gobject.transform.Find("Model_0").GetComponent<MeshRenderer>().bounds;
										Transform transform = gobject.transform.Find("Model_1");
										if (transform != null)
										{
											bounds.Encapsulate(transform.GetComponent<MeshRenderer>().bounds);
										}
									}
								}
								else
								{
									bounds = new Bounds(new Vector3(position3.x, position3.y + 1f, position3.z), new Vector3(localScale.x * 2f, localScale.y * 3f, localScale.z * 2f));
								}
								int textSize = DrawUtilities.GetTextSize(espvisual, distance);
								double num = Math.Round(distance);
								string text = string.Format("<size={0}>", textSize);
								string text2 = string.Format("<size={0}>", textSize);
								switch (espobject.Target)
								{
									case ESPTarget.Игроки:
										{
											Player player = (Player)espobject.Object;
											if (player.life.isDead)
											{
												goto IL_13B8;
											}
											if (espvisual.ShowName)
											{
												text2 = text2 + ESPComponent.GetSteamPlayer(player).playerID.characterName + "\n";
											}
											if (RaycastUtilities.TargetedPlayer == player && RaycastOptions.EnablePlayerSelection)
											{
												text2 += "[Hedef]\n";
											}
											if (ESPOptions.ShowPlayerWeapon)
											{
												text2 = text2 + ((player.equipment.asset != null) ? player.equipment.asset.itemName : "Fists") + "\n";
											}
											if (ESPOptions.ShowPlayerVehicle)
											{
												text2 += ((player.movement.getVehicle() != null) ? (player.movement.getVehicle().asset.name + "\n") : "No Vehicle\n");
											}
											bounds.size /= 2f;
											bounds.size = new Vector3(bounds.size.x, bounds.size.y * 1.25f, bounds.size.z);
											if (FriendUtilities.IsFriendly(player) && ESPOptions.UsePlayerGroup)
											{
												color = ColorUtilities.getColor("_ESPFriendly");
											}
											break;
										}
									case ESPTarget.Зомби:
										if (((Zombie)espobject.Object).isDead)
										{
											goto IL_13B8;
										}
										if (espvisual.ShowName)
										{
											text2 += "Zombi\n";
										}
										break;
									case ESPTarget.Предметы:
										{
											InteractableItem interactableItem = (InteractableItem)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 = text2 + interactableItem.asset.itemName + "\n";
											}
											break;
										}
									case ESPTarget.Турели:
										{
											InteractableSentry interactableSentry = (InteractableSentry)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "Turret\n";
												text += "Turret\n";
											}
											if (ESPOptions.ShowSentryItem)
											{
												text = text + ESPComponent.SentryName(interactableSentry.displayItem, false) + "\n";
												text2 = text2 + ESPComponent.SentryName(interactableSentry.displayItem, true) + "\n";
											}
											break;
										}
									case ESPTarget.Кровати:
										{
											InteractableBed bed = (InteractableBed)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "Yatak\n";
												text += "Yatak\n";
											}
											if (ESPOptions.ShowClaimed)
											{
												text2 = text2 + ESPComponent.GetOwned(bed, true) + "\n";
												text = text + ESPComponent.GetOwned(bed, false) + "\n";
											}
											break;
										}
									case ESPTarget.КлеймФлаги:
										if (espvisual.ShowName)
										{
											text2 += "Bayrak\n";
										}
										break;
									case ESPTarget.Транспорт:
										{
											InteractableVehicle interactableVehicle = (InteractableVehicle)espobject.Object;
											if (interactableVehicle.health == 0 || (ESPOptions.FilterVehicleLocked && interactableVehicle.isLocked))
											{
												goto IL_13B8;
											}
											ushort num2;
											ushort num3;
											interactableVehicle.getDisplayFuel(out num2, out num3);
											float num4 = Mathf.Round(100f * ((float)interactableVehicle.health / (float)interactableVehicle.asset.health));
											float num5 = Mathf.Round(100f * ((float)num2 / (float)num3));
											if (espvisual.ShowName)
											{
												text2 = text2 + interactableVehicle.asset.name + "\n";
												text = text + interactableVehicle.asset.name + "\n";
											}
											if (ESPOptions.ShowVehicleHealth)
											{
												text2 += string.Format("Canı: {0}%\n", num4);
												text += string.Format("Canı: {0}%\n", num4);
											}
											if (ESPOptions.ShowVehicleFuel)
											{
												text2 += string.Format("Benzini: {0}%\n", num5);
												text += string.Format("Benzini: {0}%\n", num5);
											}
											if (ESPOptions.ShowVehicleLocked)
											{
												text2 = text2 + ESPComponent.GetLocked(interactableVehicle, true) + "\n";
												text = text + ESPComponent.GetLocked(interactableVehicle, false) + "\n";
											}
											break;
										}
									case ESPTarget.Ящики:
										{
											InteractableStorage interactableStorage = (InteractableStorage)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "Dolap\n";
												text2 = text2 + "ID: " + interactableStorage.name + "\n";
												if (interactableStorage.name == "1374")
												{
													text2 += "Аирдроп\n";
												}
												else if (interactableStorage.name == "366")
												{
													text2 += "ИМЯ: Кленовый Ящик\n";
												}
												else if (interactableStorage.name == "1202")
												{
													text2 += "ИМЯ: Кленовая Стойка для оружия\n";
												}
												else if (interactableStorage.name == "1205")
												{
													text2 += "ИМЯ: Кленовый Стенд\n";
												}
												else if (interactableStorage.name == "1245")
												{
													text2 += "ИМЯ: Кленовый кухонный шкаф\n";
												}
												else if (interactableStorage.name == "1251")
												{
													text2 += "ИМЯ: Кленовая Кухонная раковина\n";
												}
												else if (interactableStorage.name == "1278")
												{
													text2 += "ИМЯ: Кленовый шкаф\n";
												}
												else if (interactableStorage.name == "1410")
												{
													text2 += "ИМЯ: Кленовая подставка для трофеев\n";
												}
												else if (interactableStorage.name == "1410")
												{
													text2 += "ИМЯ: Кленовая подставка для трофеев\n";
												}
												else if (interactableStorage.name == "367")
												{
													text2 += "ИМЯ: Берёзовый ящик\n";
												}
												else if (interactableStorage.name == "1203")
												{
													text2 += "ИМЯ: Берёзовая стойка для оружия\n";
												}
												else if (interactableStorage.name == "1206")
												{
													text2 += "ИМЯ: Берёзовый стенд\n";
												}
												else if (interactableStorage.name == "1246")
												{
													text2 += "ИМЯ: Берёзовый кухонный шкаф\n";
												}
												else if (interactableStorage.name == "1252")
												{
													text2 += "ИМЯ: Берёзовая раковина\n";
												}
												else if (interactableStorage.name == "1411")
												{
													text2 += "ИМЯ: Берёзовая подставка для трофеев\n";
												}
												else if (interactableStorage.name == "368")
												{
													text2 += "ИМЯ: Сосновый ящик\n";
												}
												else if (interactableStorage.name == "1204")
												{
													text2 += "ИМЯ: Сосновая стойка для оружия\n";
												}
												else if (interactableStorage.name == "1207")
												{
													text2 += "ИМЯ: Сосновый стенд\n";
												}
												else if (interactableStorage.name == "1247")
												{
													text2 += "ИМЯ: Сосновый кухонный шкаф\n";
												}
												else if (interactableStorage.name == "1253")
												{
													text2 += "ИМЯ: Сосновая раковина\n";
												}
												else if (interactableStorage.name == "1280")
												{
													text2 += "ИМЯ: Сосновый шкаф\n";
												}
												else if (interactableStorage.name == "1412")
												{
													text2 += "ИМЯ: Сосновая подставка для трофеев\n";
												}
												else if (interactableStorage.name == "328")
												{
													text2 += "ИМЯ: Сейф\n";
												}
												else if (interactableStorage.name == "1220")
												{
													text2 += "ИМЯ: Железная стойка для оружия\n";
												}
												else if (interactableStorage.name == "1221")
												{
													text2 += "ИМЯ: Железный стенд\n";
												}
												else if (interactableStorage.name == "1248")
												{
													text2 += "ИМЯ: Железный кухонный шкаф\n";
												}
												else if (interactableStorage.name == "1254")
												{
													text2 += "ИМЯ: Железная раковина\n";
												}
												else if (interactableStorage.name == "1281")
												{
													text2 += "ИМЯ: Железный шкаф\n";
												}
												else if (interactableStorage.name == "1413")
												{
													text2 += "ИМЯ: Железная подставка для трофеев\n";
												}
											}
											break;
										}
									case ESPTarget.Генераторы:
										{
											if (espvisual.ShowName)
											{
												text2 += "Jeneratör\n";
											}
											InteractableGenerator interactableGenerator = (InteractableGenerator)espobject.Object;
											float num6 = Mathf.Round(100f * ((float)interactableGenerator.fuel / (float)interactableGenerator.capacity));
											if (ESPOptions.ShowGeneratorFuel)
											{
												text2 += string.Format("Benzin: {0}%\n", num6);
												text += string.Format("Benzin: {0}%\n", num6);
											}
											if (ESPOptions.ShowGeneratorPowered)
											{
												text2 = text2 + ESPComponent.GetPowered(interactableGenerator, true) + "\n";
												text = text + ESPComponent.GetPowered(interactableGenerator, false) + "\n";
											}
											break;
										}
									case ESPTarget.Животные:
										{
											Animal animal = (Animal)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "Hayvan\n";
												text2 = text2 + animal.asset.animalName + "\n";
											}
											break;
										}
									case ESPTarget.Ловшуки:
										{
											InteractableTrap interactableTrap = (InteractableTrap)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "Tuzak\n";
											}
											break;
										}
									case ESPTarget.Аирдропы:
										{
											Carepackage carepackage = (Carepackage)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "Airdrop\n";
											}
											break;
										}
									case ESPTarget.Двери:
										if (espvisual.ShowName)
										{
											text2 += "Kapı\n";
										}
										break;
									case ESPTarget.Ягоды:
										if (espvisual.ShowName)
										{
											text2 += "Ягода\n";
										}
										break;
									case ESPTarget.Растения:
										{
											InteractableFarm interactableFarm = (InteractableFarm)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "Растение\n";
												if (interactableFarm.name == "330")
												{
													text2 += "Саженец моркови\n";
												}
												else if (interactableFarm.name == "336")
												{
													text2 += "Саженец кукурузы\n";
												}
												else if (interactableFarm.name == "339")
												{
													text2 += "Саженец капусты\n";
												}
												else if (interactableFarm.name == "341")
												{
													text2 += "Саженец помидора\n";
												}
												else if (interactableFarm.name == "343")
												{
													text2 += "Саженец картошки\n";
												}
												else if (interactableFarm.name == "345")
												{
													text2 += "Саженец пшеницы\n";
												}
												else if (interactableFarm.name == "1104")
												{
													text2 += "Саженец янтаря\n";
												}
												else if (interactableFarm.name == "1105")
												{
													text2 += "Саженец синих ягод\n";
												}
												else if (interactableFarm.name == "1106")
												{
													text2 += "Саженец зелёных ягод\n";
												}
												else if (interactableFarm.name == "1107")
												{
													text2 += "Саженец лиловых ягод\n";
												}
												else if (interactableFarm.name == "1108")
												{
													text2 += "Саженец алых ягод\n";
												}
												else if (interactableFarm.name == "1109")
												{
													text2 += "Саженец бирюзовых ягод\n";
												}
												else if (interactableFarm.name == "1110")
												{
													text2 += "Саженец жёлтых ягод\n";
												}
											}
											break;
										}
									case ESPTarget.C4:
										{
											InteractableCharge interactableCharge = (InteractableCharge)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "C4\n";
												if (interactableCharge.name == "1241")
												{
													text2 += "Breaching Charge\n";
												}
												else if (interactableCharge.name == "1393")
												{
													text2 += "Precision Charge\n";
												}
											}
											break;
										}
									case ESPTarget.Fire:
										if (espvisual.ShowName)
										{
											text2 += "Источник огня\n";
										}
										break;
									case ESPTarget.Лампы:
										if (espvisual.ShowName)
										{
											text2 += "Лампа\n";
										}
										break;
									case ESPTarget.Топливо:
										if (espvisual.ShowName)
										{
											text2 += "Benzin Kutusu\n";
										}
										break;
									case ESPTarget.Генератор_безопасной_зоны:
										{
											InteractableSafezone interactableSafezone = (InteractableSafezone)espobject.Object;
											if (espvisual.ShowName)
											{
												text2 += "Генератор сейф зоны\n";
											}
											break;
										}
									case ESPTarget.Генератор_Воздуха:
										if (espvisual.ShowName)
										{
											text2 += "Oksijen Jeneratörü\n";
										}
										break;
									case ESPTarget.NPC:
										if (espvisual.ShowName)
										{
											text2 += "NPC\n";
										}
										break;
								}
								if (text == string.Format("<size={0}>", textSize))
								{
									text = null;
								}
								if (espvisual.ShowDistance)
								{
									text2 += string.Format("{0}m\n", num);
									if (text != null)
									{
										text += string.Format("{0}m\n", num);
									}
								}
								if (espvisual.ShowAngle)
								{
									double num7 = Math.Round(VectorUtilities.GetAngleDelta(position2, forward, position3), 2);
									text2 += string.Format("Угол: {0}°\n", num7);
									if (text != null)
									{
										text += string.Format("{0}°\n", num7);
									}
								}
								text2 += "</size>";
								if (text != null)
								{
									text += "</size>";
								}
								Vector3[] boxVectors = DrawUtilities.GetBoxVectors(bounds);
								Vector2[] rectangleLines = DrawUtilities.GetRectangleLines(ESPComponent.MainCamera, bounds, color);
								Vector3 v2 = DrawUtilities.Get2DW2SVector(ESPComponent.MainCamera, rectangleLines, location);
								bool flag;
								if (MirrorCameraOptions.Enabled)
								{
									flag = rectangleLines.Any((Vector2 v) => MirrorCameraComponent.viewport.Contains(v));
								}
								else
								{
									flag = false;
								}
								if (flag)
								{
									Highlighter component2 = gobject.GetComponent<Highlighter>();
									if (component2 != null)
									{
										component2.ConstantOffImmediate();
									}
								}
								else
								{
									if (espvisual.Boxes)
									{
										if (espvisual.TwoDimensional)
										{
											DrawUtilities.PrepareRectangleLines(rectangleLines, color);
										}
										else
										{
											DrawUtilities.PrepareBoxLines(boxVectors, color);
											v2 = DrawUtilities.Get3DW2SVector(ESPComponent.MainCamera, bounds, location);
										}
									}
									if (espvisual.Glow)
									{
										Highlighter highlighter = gobject.GetComponent<Highlighter>() ?? gobject.AddComponent<Highlighter>();
										highlighter.occluder = true;
										highlighter.overlay = true;
										highlighter.ConstantOnImmediate(ColorUtilities.getColor(string.Format("_{0}_Glow", espobject.Target)));
										ESPComponent.Highlighters.Add(highlighter);
									}
									else
									{
										Highlighter component3 = gobject.GetComponent<Highlighter>();
										if (component3 != null && component3 != TrajectoryComponent.Highlighted)
										{
											component3.ConstantOffImmediate();
										}
									}
									DrawUtilities.DrawLabel(ESPComponent.ESPFont, location, v2, text2, espvisual.CustomTextColor ? ColorUtilities.getColor(string.Format("_{0}_Text", espobject.Target)) : color, ColorUtilities.getColor(string.Format("_{0}_Outline", espobject.Target)), espvisual.BorderStrength, text, 12);
									if (espvisual.LineToObject)
									{
										ESPVariables.DrawBuffer2.Enqueue(new ESPBox2
										{
											Color = color,
											Vertices = new Vector2[]
											{
												new Vector2((float)(Screen.width / 2), (float)Screen.height),
												new Vector2(vector.x, (float)Screen.height - vector.y)
											}
										});
									}
								}
							}
						}
					}
				}
			IL_13B8:;
			}
			ESPComponent.GLMat.SetPass(0);
			GL.PushMatrix();
			GL.LoadProjectionMatrix(ESPComponent.MainCamera.projectionMatrix);
			GL.modelview = ESPComponent.MainCamera.worldToCameraMatrix;
			GL.Begin(1);
			for (int j = 0; j < ESPVariables.DrawBuffer.Count; j++)
			{
				ESPBox espbox = ESPVariables.DrawBuffer.Dequeue();
				GL.Color(espbox.Color);
				Vector3[] vertices = espbox.Vertices;
				for (int k = 0; k < vertices.Length; k++)
				{
					GL.Vertex(vertices[k]);
				}
			}
			GL.End();
			GL.PopMatrix();
			GL.PushMatrix();
			GL.Begin(1);
			for (int l = 0; l < ESPVariables.DrawBuffer2.Count; l++)
			{
				ESPBox2 espbox2 = ESPVariables.DrawBuffer2.Dequeue();
				GL.Color(espbox2.Color);
				Vector2[] vertices2 = espbox2.Vertices;
				bool flag2 = true;
				for (int m = 0; m < vertices2.Length; m++)
				{
					if (m < vertices2.Length - 1)
					{
						Vector2 b = vertices2[m];
						if (Vector2.Distance(vertices2[m + 1], b) > (float)(Screen.width / 2))
						{
							flag2 = false;
							break;
						}
					}
				}
				if (flag2)
				{
					for (int n = 0; n < vertices2.Length; n++)
					{
						GL.Vertex3(vertices2[n].x, vertices2[n].y, 0f);
					}
				}
			}
			GL.End();
			GL.PopMatrix();
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0002AD68 File Offset: 0x00028F68
	[OnSpy]
	public static void DisableHighlighters()
	{
		foreach (Highlighter highlighter in ESPComponent.Highlighters)
		{
			highlighter.occluder = false;
			highlighter.overlay = false;
			highlighter.ConstantOffImmediate();
		}
		ESPComponent.Highlighters.Clear();
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00025346 File Offset: 0x00023546
	public static string SentryName(Item DisplayItem, bool color)
	{
		if (DisplayItem != null)
		{
			return Assets.find(EAssetType.ITEM, DisplayItem.id).name;
		}
		if (color)
		{
			return "<color=#ff0000ff>Нет предмета</color>";
		}
		return "Нет предмета";
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0002536B File Offset: 0x0002356B
	public static string GetLocked(InteractableVehicle Vehicle, bool color)
	{
		if (!Vehicle.isLocked)
		{
			if (!color)
			{
				return "Открыто";
			}
			return "<color=#00ff00ff>Открыто</color>";
		}
		else
		{
			if (color)
			{
				return "<color=#ff0000ff>Закрыто</color>";
			}
			return "Закрыто";
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00025392 File Offset: 0x00023592
	public static string GetPowered(InteractableGenerator Generator, bool color)
	{
		if (!Generator.isPowered)
		{
			if (color)
			{
				return "<color=#ff0000ff>Не работает</color>";
			}
			return "Не работает";
		}
		else
		{
			if (color)
			{
				return "<color=#00ff00ff>Работает</color>";
			}
			return "Работает";
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000253B9 File Offset: 0x000235B9
	public static string GetOwned(InteractableBed bed, bool color)
	{
		if (!bed.isClaimed)
		{
			if (!color)
			{
				return "Свободна";
			}
			return "<color=#ff0000ff>Свободна</color>";
		}
		else
		{
			if (color)
			{
				return "<color=$00ff00ff>Занята</color>";
			}
			return "Занята";
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0002ADD0 File Offset: 0x00028FD0
	public static SteamPlayer GetSteamPlayer(Player player)
	{
		using (List<SteamPlayer>.Enumerator enumerator = Provider.clients.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SteamPlayer steamPlayer = enumerator.Current;
				if (steamPlayer.player == player)
				{
					return steamPlayer;
				}
			}
			goto IL_43;
		}
		SteamPlayer result;
		return result;
		IL_43:
		return null;
	}



	// Token: 0x04000056 RID: 86
	public static MethodInfo setActiveWeatherAsset = typeof(LevelLighting).GetMethod("SetActiveWeatherAsset", BindingFlags.Static | BindingFlags.NonPublic);

	// Token: 0x04000057 RID: 87
	public static Material GLMat;

	// Token: 0x04000058 RID: 88
	public static Font ESPFont;

	// Token: 0x04000059 RID: 89
	public static List<Highlighter> Highlighters = new List<Highlighter>();

	// Token: 0x0400005A RID: 90
	public static Camera MainCamera;
}
