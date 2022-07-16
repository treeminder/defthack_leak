using System;
using System.Threading;
using SDG.Unturned;
using SosiHui;
using UnityEngine;

// Token: 0x0200005C RID: 92
public static class MoreMiscTab
{
	// Token: 0x060002DD RID: 733 RVA: 0x00030610 File Offset: 0x0002E810
	public static void Tab()
	{
		Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), MenuComponent._isRus ? "Опции" : "Options", delegate()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(230f)
			});
			Prefab.Toggle(ref ItemOptions.AutoItemPickup, MenuComponent._isRus ? "Авто подбор вещей" : "Auto pick up item", 17);
			GUILayout.Label((MenuComponent._isRus ? "Задержка: " : "Delay: ") + ItemOptions.ItemPickupDelay.ToString() + (MenuComponent._isRus ? "мс" : "ms"), new GUILayoutOption[0]);
			ItemOptions.ItemPickupDelay = (int)GUILayout.HorizontalSlider((float)ItemOptions.ItemPickupDelay, 0f, 3000f, new GUILayoutOption[0]);
			ItemUtilities.DrawFilterTab(ItemOptions.ItemFilterOptions);
			GUIContent[] array = new GUIContent[]
			{
				new GUIContent(MenuComponent._isRus ? "Чистый экран" : "Clear screen"),
				new GUIContent(MenuComponent._isRus ? "Рандом картинка" : "Random picture"),
				new GUIContent(MenuComponent._isRus ? "Без картинки" : "No picture"),
				new GUIContent(MenuComponent._isRus ? "Без antispy" : "No antispy")
			};
			GUILayout.Label(MenuComponent._isRus ? "Antispy метод:" : "Antispy method:", new GUILayoutOption[0]);
			MiscOptions.AntiSpyMethod = Prefab.List(180, new GUIContent(array[MiscOptions.AntiSpyMethod].text), array.Length - 1, MiscOptions.AntiSpyMethod);
			if (MiscOptions.AntiSpyMethod == 1)
			{
				GUILayout.Label(MenuComponent._isRus ? "Anti/spy папка:" : "Anti/spy folder:", new GUILayoutOption[0]);
				MiscOptions.AntiSpyPath = Prefab.TextField(MiscOptions.AntiSpyPath, "", 250);
			}
			Prefab.Toggle(ref MiscOptions.AlertOnSpy, MenuComponent._isRus ? "Предупреждать при /spy" : "Warn on /spy", 17);
			if (GUILayout.Button(MenuComponent._isRus ? "Мгновенный дисконнект" : "Instant disconnect", new GUILayoutOption[]
			{
				GUILayout.Width(170f)
			}))
			{
				Provider.disconnect();
			}
			if (GUILayout.Button(MenuComponent._isRus ? "Убрать воду" : "Remove water", new GUILayoutOption[]
			{
				GUILayout.Width(170f)
			}))
			{
				if (MiscOptions.Altitude == 0f)
				{
					MiscOptions.Altitude = LevelLighting.seaLevel;
				}
				LevelLighting.seaLevel = ((LevelLighting.seaLevel == 0f) ? MiscOptions.Altitude : 0f);
			}
			if (Provider.cameraMode != ECameraMode.BOTH && GUILayout.Button(MenuComponent._isRus ? "Включить 3-е лицо" : "Turn on 3rd person", new GUILayoutOption[]
			{
				GUILayout.Width(170f)
			}))
			{
				Provider.cameraMode = ECameraMode.BOTH;
			}
			if (Provider.cameraMode == ECameraMode.BOTH && GUILayout.Button(MenuComponent._isRus ? "Выключить 3-е лицо" : "Turn off 3rd person", new GUILayoutOption[]
			{
				GUILayout.Width(170f)
			}))
			{
				Provider.cameraMode = ECameraMode.VEHICLE;
			}
			if (GUILayout.Button(MenuComponent._isRus ? "Отключить чит" : "Disable cheat", new GUILayoutOption[]
			{
				GUILayout.Width(170f)
			}))
			{
				PlayerCoroutines.DisableAllVisuals();
				new OverrideManager().OffHook();
				UnityEngine.Object.DestroyImmediate(BinaryOperationBinder.HookObject);
			}
			GUILayout.EndVertical();
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.Label(MenuComponent._isRus ? "Сменить ник:\r\n(авт.перезаход на сервер)" : "Change nickname:\r\n(auto switch to server)", new GUILayoutOption[0]);
			MiscOptions.NickName = Prefab.TextField(MiscOptions.NickName, MenuComponent._isRus ? "Ник: " : "Nick: ", 130);
			if (GUILayout.Button(MenuComponent._isRus ? "Применить" : "Apply", new GUILayoutOption[0]))
			{
				if (!Provider.isConnected)
				{
					if (!Provider.isConnected)
					{
						Characters.rename(MiscOptions.NickName);
						Characters.renick(MiscOptions.NickName);
						Characters.apply();
					}
				}
				else
				{
					Characters.rename(MiscOptions.NickName);
					Characters.renick(MiscOptions.NickName);
					Characters.apply();
					SteamConnectionInfo info = new SteamConnectionInfo(Provider.currentServerInfo.ip, Provider.currentServerInfo.queryPort, "");
					Provider.disconnect();
					Thread.Sleep(50);
					MenuPlayConnectUI.connect(info, true);
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		});
	}
}
