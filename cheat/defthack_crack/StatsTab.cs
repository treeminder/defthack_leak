using System;
using SDG.Provider;
using SDG.Unturned;
using UnityEngine;

// Token: 0x020000AA RID: 170
public static class StatsTab
{
	// Token: 0x06000523 RID: 1315 RVA: 0x00035920 File Offset: 0x00033B20
	public static void Tab()
	{
		Prefab.ScrollView(new Rect(0f, 0f, 250f, 380f), ref StatsTab.ScrollPos, delegate()
		{
			for (int i = 0; i < StatsTab.StatLabels.Length; i++)
			{
				if (GUILayout.Button(StatsTab.StatLabels[i], new GUILayoutOption[]
				{
					GUILayout.Width(230f)
				}))
				{
					StatsTab.Selected = i;
				}
				GUILayout.Space(3f);
			}
			GUILayout.Label(MenuComponent._isRus ? "Получение достижений" : "Obtaining achievements", new GUILayoutOption[0]);
			for (int j = 0; j < StatsTab.Achiv.Length; j++)
			{
				if (GUILayout.Button(StatsTab.nameAchiv[j], new GUILayoutOption[]
				{
					GUILayout.Width(230f)
				}))
				{
					Provider.provider.achievementsService.setAchievement(StatsTab.Achiv[j]);
				}
				GUILayout.Space(2f);
			}
			if (GUILayout.Button(MenuComponent._isRus ? "Получить все достижения" : "Get all achievements", new GUILayoutOption[]
			{
				GUILayout.Width(230f)
			}))
			{
				for (int k = 0; k < StatsTab.Achiv.Length; k++)
				{
					Provider.provider.achievementsService.setAchievement(StatsTab.Achiv[k]);
				}
			}
		});
		Prefab.MenuArea(new Rect(260f, 0f, 196f, 220f), MenuComponent._isRus ? "Модиффикатор" : "Modifier", delegate()
		{
			if (StatsTab.Selected != 0)
			{
				string text = StatsTab.StatLabels[StatsTab.Selected];
				int num;
				Provider.provider.statisticsService.userStatisticsService.getStatistic(StatsTab.StatNames[StatsTab.Selected], out num);
				GUILayout.Label(text, new GUILayoutOption[0]);
				GUILayout.Space(4f);
				GUILayout.Label(string.Format(MenuComponent._isRus ? "Текущий: {0}" : "Current: {0}", num), new GUILayoutOption[0]);
				GUILayout.Space(3f);
				StatsTab.Amount = Prefab.TextField(StatsTab.Amount, MenuComponent._isRus ? "Изменить на: " : "Modify:");
				GUILayout.Space(2f);
				int num2;
				if (int.TryParse(StatsTab.Amount, out num2) && GUILayout.Button(MenuComponent._isRus ? "Принять" : "Accept", new GUILayoutOption[0]))
				{
					for (int i = 1; i <= num2; i++)
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic(StatsTab.StatNames[StatsTab.Selected], num + i);
					}
				}
			}
		});
		Prefab.MenuArea(new Rect(260f, 140f, 196f, 174f), MenuComponent._isRus ? "Счётчик убийств" : "Kill counter", delegate()
		{
			GUILayout.Label(MenuComponent._isRus ? "Возьмите в руки оружие\nсо счётчиком убийств!" : "Take up a weapon\nwith a kill counter!", new GUILayoutOption[0]);
			GUILayout.Space(5f);
			if (GUILayout.Button(MenuComponent._isRus ? "Найти оружие" : "Find a weapon", new GUILayoutOption[0]) && Player.player)
			{
				ItemAsset asset = Player.player.equipment.asset;
				if (asset != null)
				{
					EStatTrackerType estatTrackerType;
					int num;
					Player.player.equipment.getUseableStatTrackerValue(out estatTrackerType, out num);
					StatsTab.name = asset.itemName.ToString();
					StatsTab.id = asset.id.ToString();
					StatsTab.count = num;
					StatsTab.labels = (MenuComponent._isRus ? "Оружие:" : "Weapon:");
				}
			}
			Prefab.TextField(StatsTab.name, MenuComponent._isRus ? "Оружие: " : "Weapon: ", 110);
			StatsTab.count = Prefab.TextField(StatsTab.count, MenuComponent._isRus ? "Убийств:" : "Murders:", 6, 0, 2147483646);
			ushort itemID;
			if (GUILayout.Button(MenuComponent._isRus ? "Изменить" : "Изменить", new GUILayoutOption[0]) && ushort.TryParse(StatsTab.id, out itemID))
			{
				MiscComponent.incrementStatTrackerValue(itemID, StatsTab.count);
			}
		});
	}

	// Token: 0x04000278 RID: 632
	public static string id;

	// Token: 0x04000279 RID: 633
	public static int count;

	// Token: 0x0400027A RID: 634
	public static string name = MenuComponent._isRus ? "Не выбрано" : "Not chosen";

	// Token: 0x0400027B RID: 635
	public static string labels;

	// Token: 0x0400027C RID: 636
	public static int Selected = 0;

	// Token: 0x0400027D RID: 637
	public static Vector2 ScrollPos;

	// Token: 0x0400027E RID: 638
	public static string Amount = "";

	// Token: 0x0400027F RID: 639
	public static string[] StatLabels = new string[]
	{
		"None",
		"Normal Zombie Kills",
		"Player Kills",
		"Items Found",
		"Resources Found",
		"Experience Found",
		"Mega Zombie Kills",
		"Player Deaths",
		"Animal Kills",
		"Blueprints Found",
		"Fishies Found",
		"Plants Taken",
		"Accuracy",
		"Headshots",
		"Foot Traveled",
		"Vehicle Traveled",
		"Arena Wins",
		"Buildables Taken",
		"Throwables Found"
	};

	// Token: 0x04000280 RID: 640
	public static string[] StatNames = new string[]
	{
		"None",
		"Kills_Zombies_Normal",
		"Kills_Players",
		"Found_Items",
		"Found_Resources",
		"Found_Experience",
		"Kills_Zombies_Mega",
		"Deaths_Players",
		"Kills_Animals",
		"Found_Crafts",
		"Found_Fishes",
		"Found_Plants",
		"Accuracy_Shot",
		"Headshots",
		"Travel_Foot",
		"Travel_Vehicle",
		"Arena_Wins",
		"Found_Buildables",
		"Found_Throwables"
	};

	// Token: 0x04000281 RID: 641
	public static string[] nameAchiv = new string[]
	{
		"Welcome to PEI",
		"A Bridge Too Far",
		"Mastermind",
		"Offense",
		"Defense",
		"Support",
		"Experienced + Schooled",
		"Hoarder + Scavenger",
		"Outdoors + Camper",
		"Psychopath + Murderer",
		"Survivor",
		"Berries",
		"Accident Prone",
		"Behind the Wheel",
		"Welcome to the Yukon",
		"Welcome to Washington",
		"Fishing",
		"Crafting",
		"Farming",
		"Headshot",
		"Sharpshooter",
		"Hiking",
		"Roadtrip",
		"Champion",
		"Fortified",
		"Welcome to Russia",
		"Villain",
		"Helping Hand",
		"Unturned",
		"Forged + Hardened",
		"Graduation",
		"Soulcrystal",
		"Paragon",
		"Mk. II",
		"Ensign",
		"Lieutenant",
		"Major",
		"Welcome to Hawaii",
		"Extinguished",
		"Secrets of Neuschwanstein",
		"Welcome to Germany",
		"Welcome to Kuwait",
		"Squeek",
		"Feline Friends",
		"Dinosaur Juice",
		"Home Decor",
		"Welcome to Ireland",
		"Always Watching",
		"Familiar Faces"
	};

	// Token: 0x04000282 RID: 642
	public static string[] Achiv = new string[]
	{
		"pei",
		"bridge",
		"mastermind",
		"offense",
		"defense",
		"support",
		"experienced",
		"hoarder",
		"outdoors",
		"psychopath",
		"survivor",
		"berries",
		"accident_prone",
		"wheel",
		"yukon",
		"washington",
		"fishing",
		"crafting",
		"farming",
		"headshot",
		"sharpshooter",
		"hiking",
		"roadtrip",
		"champion",
		"fortified",
		"russia",
		"villain",
		"Quest",
		"unturned",
		"forged",
		"Educated",
		"soulcrystal",
		"paragon",
		"mk2",
		"ensign",
		"lieutenant",
		"major",
		"hawaii",
		"Boss_Magma",
		"Zweihander",
		"Peaks",
		"Kuwait_Visited",
		"Kuwait_Squeek",
		"Kuwait_FelineFriends",
		"Kuwait_DinosaurJuice",
		"Kuwait_HomeDecor",
		"Ireland_Visited",
		"Kuwait_AlwaysWatching",
		"Kuwait_FamiliarFaces"
	};
}
