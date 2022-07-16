using System;
using SosiHui;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public static class VisualsTab
{
	// Token: 0x0600058C RID: 1420 RVA: 0x00036C64 File Offset: 0x00034E64
	public static void Tab()
	{
		Prefab.MenuArea(new Rect(150f, 55f, 481f, 414f), "Visuals", delegate()
		{
			Prefab.ScrollView(new Rect(0f, 0f, 225f, 430f), ref VisualsTab.VisualscrollPosition, delegate()
			{
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ИГРОКИ" : "Players", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Игроки);
					if (ESPOptions.VisualOptions[0].Enabled)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						Prefab.Toggle(ref ESPOptions.ShowPlayerWeapon, MenuComponent._isRus ? "Показывать оружие" : "Show weapon", 17);
						Prefab.Toggle(ref ESPOptions.ShowPlayerVehicle, MenuComponent._isRus ? "Показывать транспорт" : "Show transport", 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ЗОМБИ" : "Zombie", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Зомби);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ТРАНСПОРТ" : "Vehicle", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Транспорт);
					if (ESPOptions.VisualOptions[6].Enabled)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						Prefab.Toggle(ref ESPOptions.ShowVehicleFuel, MenuComponent._isRus ? "Кол-во топлива" : "Fuel quantity", 17);
						Prefab.Toggle(ref ESPOptions.ShowVehicleHealth, MenuComponent._isRus ? "Кол-во прочности" : "Amount of durability", 17);
						Prefab.Toggle(ref ESPOptions.ShowVehicleLocked, MenuComponent._isRus ? "Показывать закрытые" : "Show closed", 17);
						Prefab.Toggle(ref ESPOptions.FilterVehicleLocked, MenuComponent._isRus ? "Фильтровать закрытые" : "Filter closed", 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ПРЕДМЕТЫ" : "Items", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Предметы);
					if (ESPOptions.VisualOptions[2].Enabled)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						Prefab.Toggle(ref ESPOptions.FilterItems, MenuComponent._isRus ? "Фильтр предметов" : "Item filter", 17);
						if (ESPOptions.FilterItems)
						{
							GUILayout.Space(5f);
							ItemUtilities.DrawFilterTab(ItemOptions.ItemESPOptions);
						}
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ЯЩИКИ" : "Storages", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Ящики);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "КРОВАТИ" : "Beds", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Кровати);
					if (ESPOptions.VisualOptions[4].Enabled)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						Prefab.Toggle(ref ESPOptions.ShowClaimed, MenuComponent._isRus ? "Показать занятые" : "Show busy", 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ГЕНЕРАТОРЫ" : "Generators", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Генераторы);
					if (ESPOptions.VisualOptions[8].Enabled)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						Prefab.Toggle(ref ESPOptions.ShowGeneratorFuel, MenuComponent._isRus ? "Кол-во топлива" : "Fuel quantity", 17);
						Prefab.Toggle(ref ESPOptions.ShowGeneratorPowered, MenuComponent._isRus ? "Статус работы" : "Job status", 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ТУРЕЛИ" : "Turrets", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Турели);
					if (ESPOptions.VisualOptions[3].Enabled)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						Prefab.Toggle(ref ESPOptions.ShowSentryItem, MenuComponent._isRus ? "Показывать оружие" : "Show weapon", 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "КЛЕЙМ ФЛАГИ" : "Claim Flags", delegate
				{
					VisualsTab.BasicControls(ESPTarget.КлеймФлаги);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ЖИВОТНЫЕ" : "Animals", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Животные);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ЛОВУШКИ" : "Traps", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Ловшуки);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ДВЕРИ" : "Doors", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Двери);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "АИРДРОПЫ" : "AirDrops", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Аирдропы);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ЯГОДЫ" : "Berries", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Ягоды);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "РАСТЕНИЯ" : "Plants", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Растения);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ВЗРЫВЧАТКА" : "Explosives", delegate
				{
					VisualsTab.BasicControls(ESPTarget.C4);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ИСТОЧНИК ОГНЯ" : "Sources of Fire", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Fire);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ЛАМПЫ" : "Lamps", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Лампы);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ТОПЛИВО" : "Fuel Barrels", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Топливо);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ГЕН. СЗ" : "Gen. SafeZone", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Генератор_безопасной_зоны);
				});
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ГЕН. ВОЗДУХА" : "Gen. Air", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Генератор_Воздуха);
				});
				GUILayout.Space(10f);
			});
			Prefab.MenuArea(new Rect(240f, 0f, 236f, 180f), MenuComponent._isRus ? "ДРУГОЕ" : "Other", delegate()
			{
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "Радар" : "Radar", delegate
				{
					Prefab.Toggle(ref RadarOptions.Enabled, MenuComponent._isRus ? "Радар" : "Radar", 17);
					if (RadarOptions.Enabled)
					{
						Prefab.Toggle(ref RadarOptions.TrackPlayer, MenuComponent._isRus ? "Центрирование игрока" : "Centering the player", 17);
						Prefab.Toggle(ref RadarOptions.ShowPlayers, MenuComponent._isRus ? "Показывать игроков" : "Show players", 17);
						Prefab.Toggle(ref RadarOptions.ShowVehicles, MenuComponent._isRus ? "Показывать машины" : "Show cars", 17);
						if (RadarOptions.ShowVehicles)
						{
							Prefab.Toggle(ref RadarOptions.ShowVehiclesUnlocked, MenuComponent._isRus ? "Только открытые" : "Open only", 17);
						}
						GUILayout.Space(5f);
						GUILayout.Label((MenuComponent._isRus ? "Зум радара: " : "Zoom radar: ") + Mathf.Round(RadarOptions.RadarZoom).ToString(), new GUILayoutOption[0]);
						RadarOptions.RadarZoom = (float)((int)GUILayout.HorizontalSlider(RadarOptions.RadarZoom, 0f, 10f, new GUILayoutOption[0]));
						if (GUILayout.Button(MenuComponent._isRus ? "По умолчанию" : "Default", new GUILayoutOption[0]))
						{
							RadarOptions.RadarZoom = 1f;
						}
						GUILayout.Space(5f);
						GUILayout.Label((MenuComponent._isRus ? "Размер радара: " : "Radar size: ") + Mathf.RoundToInt(RadarOptions.RadarSize).ToString(), new GUILayoutOption[0]);
						RadarOptions.RadarSize = (float)((int)GUILayout.HorizontalSlider(RadarOptions.RadarSize, 50f, 1000f, new GUILayoutOption[0]));
					}
				});
				Prefab.Toggle(ref ESPOptions.ShowVanishPlayers, MenuComponent._isRus ? "Игроки в ванише" : "Vanish players", 17);
				Prefab.Toggle(ref ESPOptions.ShowCoordinates, MenuComponent._isRus ? "Показывать координаты" : "Show coordinates", 17);
				Prefab.Toggle(ref MirrorCameraOptions.Enabled, MenuComponent._isRus ? "Камера заднего вида" : "Rear View Camera", 17);
				GUILayout.Space(5f);
				if (MirrorCameraOptions.Enabled && GUILayout.Button(MenuComponent._isRus ? "Вернуть" : "Return", new GUILayoutOption[0]))
				{
					MirrorCameraComponent.FixCam();
				}
			});
			Prefab.MenuArea(new Rect(240f, 170f, 236f, 250f), MenuComponent._isRus ? "Переключатели" : "Switches", delegate()
			{
				if (Prefab.Toggle(ref ESPOptions.Enabled, MenuComponent._isRus ? "ВХ" : "WallHack", 17) && !ESPOptions.Enabled)
				{
					for (int i = 0; i < ESPOptions.VisualOptions.Length; i++)
					{
						ESPOptions.VisualOptions[i].Glow = false;
					}
					BinaryOperationBinder.HookObject.GetComponent<ESPComponent>().OnGUI();
				}
				Prefab.Toggle(ref ESPOptions.ChamsEnabled, MenuComponent._isRus ? "Чамсы" : "Chams", 17);
				if (ESPOptions.ChamsEnabled)
				{
					Prefab.Toggle(ref ESPOptions.ChamsFlat, MenuComponent._isRus ? "Плоские чамсы" : "Flat chams", 17);
				}
				Prefab.Toggle(ref MiscOptions.NoRain, MenuComponent._isRus ? "Без дождя" : "No rain", 17);
				Prefab.Toggle(ref MiscOptions.NoSnow, MenuComponent._isRus ? "Без снега" : "No snow", 17);
				Prefab.Toggle(ref MiscOptions.NightVision, MenuComponent._isRus ? "ПНВ" : "NV device", 17);
				Prefab.Toggle(ref MiscOptions.Compass, MenuComponent._isRus ? "Компасс" : "Compass", 17);
				Prefab.Toggle(ref MiscOptions.GPS, MenuComponent._isRus ? "Карта(GPS)" : "Map (GPS)", 17);
			});
		});
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00036CB4 File Offset: 0x00034EB4
	public static void BasicControls(ESPTarget esptarget)
	{
		ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
		Prefab.Toggle(ref espvisual.Enabled, MenuComponent._isRus ? "Активировать" : "Activate", 0);
		if (espvisual.Enabled)
		{
			GUILayout.Space(-7f);
			Prefab.Toggle(ref espvisual.Labels, MenuComponent._isRus ? "Надписи" : "Labels", 17);
			if (espvisual.Labels)
			{
				GUILayout.Space(-7f);
				Prefab.Toggle(ref espvisual.ShowName, MenuComponent._isRus ? "Показывать имя" : "Show name", 17);
				GUILayout.Space(-7f);
				Prefab.Toggle(ref espvisual.ShowDistance, MenuComponent._isRus ? "Показывать дистанцию" : "Show distance", 17);
				GUILayout.Space(-7f);
				Prefab.Toggle(ref espvisual.ShowAngle, MenuComponent._isRus ? "Показывать угол" : "Show angle", 17);
			}
			GUILayout.Space(-7f);
			Prefab.Toggle(ref espvisual.Boxes, "Вох", 17);
			if (espvisual.Boxes)
			{
				GUILayout.Space(-7f);
				Prefab.Toggle(ref espvisual.TwoDimensional, "2D Вох", 17);
			}
			GUILayout.Space(-7f);
			Prefab.Toggle(ref espvisual.Glow, MenuComponent._isRus ? "Обводка" : "Glow(Outline)", 17);
			GUILayout.Space(-7f);
			Prefab.Toggle(ref espvisual.LineToObject, MenuComponent._isRus ? "Линия до объекта" : "Line to object", 17);
			GUILayout.Space(-7f);
			Prefab.Toggle(ref espvisual.TextScaling, MenuComponent._isRus ? "Масштаб текста" : "Scale of text", 17);
			if (espvisual.TextScaling)
			{
				GUILayout.Space(-7f);
				espvisual.MinTextSize = Prefab.TextField(espvisual.MinTextSize, MenuComponent._isRus ? "Минимальный размер текста: " : "Minimum text size: ", 3, 0, 255);
				GUILayout.Space(-7f);
				espvisual.MaxTextSize = Prefab.TextField(espvisual.MaxTextSize, MenuComponent._isRus ? "Максимальный размер текста: " : "Maximum text size: ", 3, 0, 255);
				GUILayout.Space(-7f);
				GUILayout.Label((MenuComponent._isRus ? "Масштабирование текста по расстоянию: " : "Scaling text by distance: ") + Mathf.RoundToInt(espvisual.MinTextSizeDistance).ToString(), new GUILayoutOption[0]);
				espvisual.MinTextSizeDistance = GUILayout.HorizontalSlider(espvisual.MinTextSizeDistance, 0f, 1000f, new GUILayoutOption[0]);
			}
			else
			{
				GUILayout.Space(-7f);
				espvisual.FixedTextSize = Prefab.TextField(espvisual.FixedTextSize, MenuComponent._isRus ? "Фиксированный размер текста: " : "Fixed text size: ", 3, 0, 255);
			}
			GUILayout.Space(-7f);
			Prefab.Toggle(ref espvisual.InfiniteDistance, MenuComponent._isRus ? "Дистанция на всю карту" : "Infinity Distance", 17);
			if (!espvisual.InfiniteDistance)
			{
				GUILayout.Label((MenuComponent._isRus ? "ESP Расстояние: " : "ESP Distance:") + Mathf.RoundToInt(espvisual.Distance).ToString(), new GUILayoutOption[0]);
				espvisual.Distance = GUILayout.HorizontalSlider(espvisual.Distance, 0f, 4000f, new GUILayoutOption[0]);
				GUILayout.Space(3f);
			}
			GUILayout.Space(-7f);
			Prefab.Toggle(ref espvisual.UseObjectCap, MenuComponent._isRus ? "Лимит объектов" : "Object limit", 17);
			if (espvisual.UseObjectCap)
			{
				GUILayout.Space(-7f);
				espvisual.ObjectCap = Prefab.TextField(espvisual.ObjectCap, "Object cap:", 3, 0, 255);
			}
			GUILayout.Space(-7f);
			espvisual.BorderStrength = Prefab.TextField(espvisual.BorderStrength, "Border Strength:", 3, 0, 255);
		}
	}

	// Token: 0x040002A1 RID: 673
	public static Vector2 VisualscrollPosition;
}
