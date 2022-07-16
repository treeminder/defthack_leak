using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000058 RID: 88
public static class MiscTab
{
	// Token: 0x060002B6 RID: 694 RVA: 0x0002F954 File Offset: 0x0002DB54
	public static void g()
	{
		bool flag = !MiscTab.lb;
		int num = 210;
		MiscTab.lb = flag;
		int[] array = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			21,
			22,
			23,
			24,
			26,
			27,
			28,
			29,
			30
		};
		int num2 = 0;
		for (;;)
		{
			int num3 = num2;
			int num4 = array.Length;
			num = ((num & -209) | 4);
			if (num3 >= num4)
			{
				break;
			}
			for (;;)
			{
				int layer = array[num2];
				Physics.IgnoreLayerCollision(26, layer, MiscTab.lb);
				int num5 = MiscTab.o;
				if ((8013 ^ num5 + num5 - (num5 * 168 + num5 * 344)) != 0)
				{
					num2++;
					int num6 = MiscTab.i;
					if (~num6 != (int)((uint)(num6 * -1081081856 / 1183637 | num6 << 17) >> 17))
					{
						break;
					}
				}
			}
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0002FA00 File Offset: 0x0002DC00
	public static void Unc(bool bool_0)
	{
		InteractableVehicle vehicle = Player.player.movement.getVehicle();
		if (vehicle != null)
		{
			vehicle.GetComponent<Rigidbody>().useGravity = !bool_0;
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0002FA38 File Offset: 0x0002DC38
	public static void Mcz()
	{
		MiscTab.nnX++;
		if (MiscTab.nnX > 3)
		{
			MiscTab.nnX = 0;
		}
		switch (MiscTab.nnX)
		{
		case 0:
			MiscTab.Unc(false);
			MiscTab.znI = EEngine.BOAT;
			return;
		case 1:
			MiscTab.Unc(true);
			MiscTab.znI = EEngine.HELICOPTER;
			return;
		case 2:
			MiscTab.Unc(false);
			MiscTab.znI = EEngine.CAR;
			return;
		case 3:
			MiscTab.Unc(true);
			MiscTab.znI = EEngine.PLANE;
			return;
		default:
			return;
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
	public static void Wn5()
	{
		InteractableVehicle vehicle = Player.player.movement.getVehicle();
		FieldInfo field = vehicle.asset.GetType().GetField("_engine", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (field == null)
		{
			return;
		}
		field.SetValue(vehicle.asset, MiscTab.znI);
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00025A6F File Offset: 0x00023C6F
	public static string a(bool bool_0)
	{
		return ": " + (bool_0 ? (MenuComponent._isRus ? "Вкл" : "ON") : (MenuComponent._isRus ? "Выкл" : "OFF"));
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0002FB08 File Offset: 0x0002DD08
	public static void Tab()
	{
		Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "", delegate()
		{
			if (MiscTab.cb == EEngine.CAR)
			{
				MiscTab.car = (MenuComponent._isRus ? "Машина" : "Car");
			}
			else if (MiscTab.cb != EEngine.PLANE)
			{
				if (MiscTab.cb != EEngine.HELICOPTER)
				{
					if (MiscTab.cb == EEngine.BLIMP)
					{
						MiscTab.car = (MenuComponent._isRus ? "Дирижабль" : "Airship");
					}
					else if (MiscTab.cb == EEngine.TRAIN)
					{
						MiscTab.car = (MenuComponent._isRus ? "Поезд" : "Train");
					}
				}
				else
				{
					MiscTab.car = (MenuComponent._isRus ? "Вертолёт" : "Helicopter");
				}
			}
			else
			{
				MiscTab.car = (MenuComponent._isRus ? "Самолёт" : "Plane");
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(230f)
			});
			Prefab.Toggle(ref MiscOptions.EnVehicle, MenuComponent._isRus ? "Для транспорта" : "For transport", 17);
			GUILayout.Space(-7f);
			if (MiscOptions.EnVehicle)
			{
				Prefab.Toggle(ref MiscOptions.VehicleFly, MenuComponent._isRus ? "Полёт транспорта" : "Transport flight", 17);
				GUILayout.Space(-7f);
				if (MiscOptions.VehicleFly)
				{
					Prefab.Toggle(ref MiscOptions.VehicleUseMaxSpeed, MenuComponent._isRus ? "Максимальная скорость" : "Maximum speed", 17);
					GUILayout.Space(-7f);
					if (!MiscOptions.VehicleUseMaxSpeed)
					{
						GUILayout.Label((MenuComponent._isRus ? "Множитель скорости: " : "Speed ​​multiplier: ") + MiscOptions.SpeedMultiplier.ToString() + "x", new GUILayoutOption[0]);
						MiscOptions.SpeedMultiplier = (float)Math.Round((double)GUILayout.HorizontalSlider(MiscOptions.SpeedMultiplier, 0f, 10f, new GUILayoutOption[0]), 2);
					}
				}
				GUILayout.Space(5f);
				if (GUILayout.Button(MenuComponent._isRus ? "Заправить машину" : "Fill the car", new GUILayoutOption[0]))
				{
					InteractableVehicle vehicle = Player.player.movement.getVehicle();
					if (vehicle != null)
					{
						vehicle.askFillFuel(2000);
					}
				}
				if (GUILayout.Button((MenuComponent._isRus ? "Проезд сквозь объекты" : "Off Colision") + MiscTab.a(MiscTab.lb), new GUILayoutOption[0]))
				{
					MiscTab.g();
				}
			}
			Prefab.Toggle(ref MiscOptions.CustomSalvageTime, MenuComponent._isRus ? "Быстрое снятия строений" : "Custom Salvage Time", 17);
			GUILayout.Space(-7f);
			Prefab.Toggle(ref MiscOptions.BuildinObstacles, MenuComponent._isRus ? "Постройка в препядствиях" : "Obstacle building", 17);
			GUILayout.Space(-7f);
			Prefab.Toggle(ref MiscOptions.SetTimeEnabled, MenuComponent._isRus ? "Время суток" : "Times of Day", 17);
			GUILayout.Space(-7f);
			if (MiscOptions.NoMovementVerification)
			{
				Prefab.Toggle(ref MiscOptions.PlayerFlight, MenuComponent._isRus ? "Полёт игрока" : "Player flight", 17);
				GUILayout.Space(-7f);
				if (MiscOptions.PlayerFlight)
				{
					GUILayout.Label((MenuComponent._isRus ? "Множитель скорости: " : "Speed ​​multiplier: ") + MiscOptions.FlightSpeedMultiplier.ToString() + "x", new GUILayoutOption[0]);
					MiscOptions.FlightSpeedMultiplier = (float)Math.Round((double)GUILayout.HorizontalSlider(MiscOptions.FlightSpeedMultiplier, 0f, 10f, new GUILayoutOption[0]), 2);
				}
			}
			Prefab.Toggle(ref MiscOptions.PunchSilentAim, MenuComponent._isRus ? "Дальность удара" : "Impact range", 17);
			GUILayout.Space(-7f);
			if (!MiscOptions.PunchSilentAim)
			{
				MiscOptions.ExtendMeleeRange = false;
			}
			else
			{
				MiscOptions.ExtendMeleeRange = true;
			}
			GUILayout.EndVertical();
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			if (Provider.isConnected && OptimizationVariables.MainPlayer != null)
			{
				if (!OptimizationVariables.MainPlayer.look.isOrbiting)
				{
					OptimizationVariables.MainPlayer.look.orbitPosition = Vector3.zero;
				}
				Prefab.Toggle(ref MiscOptions.Freecam, MenuComponent._isRus ? "Свободная камера" : "Free camera", 17);
				GUILayout.Space(-7f);
				if (OptimizationVariables.MainPlayer.look.isOrbiting)
				{
					GUILayout.Space(5f);
					if (GUILayout.Button(MenuComponent._isRus ? "Вернуть камеру" : "Return camera", new GUILayoutOption[0]))
					{
						OptimizationVariables.MainPlayer.look.orbitPosition = Vector3.zero;
					}
					GUILayout.Space(-7f);
				}
			}
			Prefab.Toggle(ref MiscOptions.AlwaysCheckMovementVerification, MenuComponent._isRus ? "Автопроверка движения" : "Auto motion check", 17);
			GUILayout.Space(-7f);
			if (Provider.isConnected)
			{
				GUILayout.Space(5f);
				if (GUILayout.Button(MenuComponent._isRus ? "Проверить движение" : "Check movement", new GUILayoutOption[0]))
				{
					MiscComponent.CheckMovementVerification();
				}
				GUILayout.Space(-7f);
			}
			if (MiscOptions.ExtendMeleeRange)
			{
				GUILayout.Label((MenuComponent._isRus ? "Расстояние удара: " : "Impact distance: ") + MiscOptions.MeleeRangeExtension.ToString(), new GUILayoutOption[0]);
				MiscOptions.MeleeRangeExtension = (float)Math.Round((double)GUILayout.HorizontalSlider(MiscOptions.MeleeRangeExtension, 0f, 7.5f, new GUILayoutOption[0]), 1);
			}
			if (MiscOptions.SetTimeEnabled)
			{
				GUILayout.Label(MenuComponent._isRus ? "ТЕКУЩЕЕ ВРЕМЯ" : "CURRENT TIME", new GUILayoutOption[0]);
				GUILayout.Label((MenuComponent._isRus ? "Время: " : "Time: ") + MiscOptions.Time.ToString(), new GUILayoutOption[0]);
				MiscOptions.Time = (float)Math.Round((double)GUILayout.HorizontalSlider(MiscOptions.Time, 0f, 0.9f, new GUILayoutOption[0]), 2);
			}
			if (MiscOptions.CustomSalvageTime)
			{
				GUILayout.Label(MenuComponent._isRus ? "ВРЕМЯ СНЯТИЯ ПОСТРОЕК" : "Salvage Time", new GUILayoutOption[0]);
				GUILayout.Label((MenuComponent._isRus ? "Время снятия: " : "Salvage Time: ") + MiscOptions.SalvageTime.ToString() + (MenuComponent._isRus ? " секунда" : " second"), new GUILayoutOption[0]);
				MiscOptions.SalvageTime = (float)Math.Round((double)GUILayout.HorizontalSlider(MiscOptions.SalvageTime, 0f, 10f, new GUILayoutOption[0]));
				if (MiscOptions.SalvageTime == 0f)
				{
					MiscOptions.SalvageTime = 1f;
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		});
		Prefab.MenuArea(new Rect(17f, 291f, 215f, 135f), MenuComponent._isRus ? "СПАМЕР" : "SPAMER", delegate()
		{
			Prefab.Toggle(ref MiscOptions.SpammerEnabled, MenuComponent._isRus ? "Включить" : "Turn on", 17);
			MiscOptions.SpamText = Prefab.TextField(MiscOptions.SpamText, MenuComponent._isRus ? "Текст: " : "Text: ", 150);
			GUILayout.Space(-2f);
			GUILayout.Label((MenuComponent._isRus ? "Задержка: " : "Delay: ") + MiscOptions.SpammerDelay.ToString() + "ms", new GUILayoutOption[0]);
			MiscOptions.SpammerDelay = (int)GUILayout.HorizontalSlider((float)MiscOptions.SpammerDelay, 0f, 10000f, new GUILayoutOption[0]);
		});
		Prefab.MenuArea(new Rect(235f, 271f, 221f, 155f), MenuComponent._isRus ? "Взаимодейвия" : "Interactions", delegate()
		{
			Prefab.Toggle(ref InteractionOptions.InteractThroughWalls, MenuComponent._isRus ? "Взаимодейвие через:" : "Interaction through:", 17);
			GUILayout.Space(-7f);
			if (InteractionOptions.InteractThroughWalls)
			{
				Prefab.Toggle(ref InteractionOptions.NoHitStructures, MenuComponent._isRus ? "Стены/Полы/т.д." : "Walls/Floors/etc.", 17);
				GUILayout.Space(-7f);
				Prefab.Toggle(ref InteractionOptions.NoHitBarricades, MenuComponent._isRus ? "Сейфы/Двери/т.д." : "Safes/Doors/etc.", 17);
				GUILayout.Space(-7f);
				Prefab.Toggle(ref InteractionOptions.NoHitItems, MenuComponent._isRus ? "Предметы" : "Items", 17);
				GUILayout.Space(-7f);
				Prefab.Toggle(ref InteractionOptions.NoHitVehicles, MenuComponent._isRus ? "Транспорт" : "Vehicle", 17);
				GUILayout.Space(-7f);
				Prefab.Toggle(ref InteractionOptions.NoHitEnvironment, MenuComponent._isRus ? "Землю/Здания" : "Land/Buildings", 17);
			}
		});
	}

	// Token: 0x04000194 RID: 404
	public static int i;

	// Token: 0x04000195 RID: 405
	public static int o;

	// Token: 0x04000196 RID: 406
	public static bool lb;

	// Token: 0x04000197 RID: 407
	public static int db;

	// Token: 0x04000198 RID: 408
	public static int nnX;

	// Token: 0x04000199 RID: 409
	public static EEngine znI;

	// Token: 0x0400019A RID: 410
	public static string car;

	// Token: 0x0400019B RID: 411
	public static EEngine cb;

	// Token: 0x0400019C RID: 412
	public static Vector2 MiscScrollPosition;
}
