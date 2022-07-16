using System;
using System.Linq;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

// Token: 0x0200008A RID: 138
public static class PlayersTab
{
	// Token: 0x06000433 RID: 1075 RVA: 0x0003384C File Offset: 0x00031A4C
	public static SteamPlayer GetSteamPlayer(Player player)
	{
		foreach (SteamPlayer steamPlayer in Provider.clients)
		{
			if (steamPlayer.player == player)
			{
				return steamPlayer;
			}
		}
		return null;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x000338B0 File Offset: 0x00031AB0
	public static void Tab()
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Space(5f);
		PlayersTab.SearchString = Prefab.TextField(PlayersTab.SearchString, MenuComponent._isRus ? "Поиск: " : "Search: ", MenuComponent._isRus ? 375 : 375);
		GUILayout.EndHorizontal();
		Prefab.ScrollView(new Rect(0f, 27f, 466f, 186f), ref PlayersTab.PlayersScroll, delegate()
		{
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				Player player = Provider.clients[i].player;
				if (!(player == OptimizationVariables.MainPlayer) && !(player == null))
				{
					if (!(PlayersTab.SearchString != "") || player.name.IndexOf(PlayersTab.SearchString, StringComparison.OrdinalIgnoreCase) != -1)
					{
						bool flag = FriendUtilities.IsFriendly(player);
						bool flag2 = MiscOptions.SpectatedPlayer == player;
						bool flag3 = false;
						bool flag4 = player == PlayersTab.SelectedPlayer;
						string text = flag ? "<color=#00ff00ff>" : "";
						if (GUILayout.Button(string.Concat(new string[]
						{
							flag4 ? "<b>" : "",
							flag2 ? (MenuComponent._isRus ? "<color=#0000ffff>[НАБЛЮДЕНИЕ]</color> " : "<color=#0000ffff>[OBSERVATION]</color> ") : "",
							text,
							player.name,
							(flag || flag3) ? "</color>" : "",
							flag4 ? "</b>" : ""
						}), new GUILayoutOption[0]))
						{
							PlayersTab.SelectedPlayer = player;
						}
						GUILayout.Space(2f);
					}
				}
			}
		});
		Prefab.MenuArea(new Rect(0f, 220f, 190f, 175f), MenuComponent._isRus ? "ОПЦИИ" : "OPTIONS", delegate()
		{
			if (!(PlayersTab.SelectedPlayer == null))
			{
				CSteamID steamID = PlayersTab.SelectedPlayer.channel.owner.playerID.steamID;
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.BeginVertical(new GUILayoutOption[0]);
				if (!FriendUtilities.IsFriendly(PlayersTab.SelectedPlayer))
				{
					if (GUILayout.Button(MenuComponent._isRus ? "Добавить в друзья" : "Add as Friend", new GUILayoutOption[]
					{
						GUILayout.Width(190f)
					}))
					{
						FriendUtilities.AddFriend(PlayersTab.SelectedPlayer);
					}
				}
				else if (GUILayout.Button(MenuComponent._isRus ? "Убрать из друзей" : "Remove from friends", new GUILayoutOption[]
				{
					GUILayout.Width(190f)
				}))
				{
					FriendUtilities.RemoveFriend(PlayersTab.SelectedPlayer);
				}
				if (GUILayout.Button(MenuComponent._isRus ? "Наблюдаль" : "Observing", new GUILayoutOption[]
				{
					GUILayout.Width(190f)
				}))
				{
					MiscOptions.SpectatedPlayer = PlayersTab.SelectedPlayer;
				}
				if (MiscOptions.SpectatedPlayer != null && MiscOptions.SpectatedPlayer == PlayersTab.SelectedPlayer && GUILayout.Button(MenuComponent._isRus ? "Не наблюдать" : "Do not observe", new GUILayoutOption[]
				{
					GUILayout.Width(190f)
				}))
				{
					MiscOptions.SpectatedPlayer = null;
				}
				if (MiscOptions.NoMovementVerification && GUILayout.Button(MenuComponent._isRus ? "Телепортироваться" : "Teleport", new GUILayoutOption[]
				{
					GUILayout.Width(190f)
				}))
				{
					OptimizationVariables.MainPlayer.transform.position = PlayersTab.SelectedPlayer.transform.position;
				}
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
		});
		Prefab.MenuArea(new Rect(196f, 220f, 270f, 175f), MenuComponent._isRus ? "Информация" : "Information", delegate()
		{
			if (!(PlayersTab.SelectedPlayer == null))
			{
				string str = Convert.ToString(Convert.ToInt32(Provider.clients.Count((SteamPlayer c) => c.player != PlayersTab.SelectedPlayer && c.player.quests.isMemberOfSameGroupAs(PlayersTab.SelectedPlayer))) + 1);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.BeginVertical(new GUILayoutOption[0]);
				GUILayout.Label("SteamID:", new GUILayoutOption[0]);
				GUILayout.TextField(PlayersTab.SelectedPlayer.channel.owner.playerID.steamID.ToString(), new GUILayoutOption[0]);
				GUILayout.Space(2f);
				GUILayout.TextField((MenuComponent._isRus ? "Локация: " : "Location: ") + LocationUtilities.GetClosestLocation(PlayersTab.SelectedPlayer.transform.position).name, new GUILayoutOption[0]);
				GUILayout.TextField((MenuComponent._isRus ? "Координаты X,Y,Z:\r\n" : "X,Y,Z coordinates:\r\n") + PlayersTab.SelectedPlayer.transform.position.ToString(), new GUILayoutOption[0]);
				GUILayout.Space(-8f);
				GUILayout.Label((MenuComponent._isRus ? "Оружие: " : "Weapon: ") + ((PlayersTab.SelectedPlayer.equipment.asset != null) ? PlayersTab.SelectedPlayer.equipment.asset.itemName : "Fists"), new GUILayoutOption[0]);
				GUILayout.Space(-10f);
				GUILayout.Label((MenuComponent._isRus ? "Транспорт: " : "Transport: ") + ((PlayersTab.SelectedPlayer.movement.getVehicle() != null) ? PlayersTab.SelectedPlayer.movement.getVehicle().asset.name : "No Vehicle"), new GUILayoutOption[0]);
				GUILayout.Space(-10f);
				GUILayout.Label((MenuComponent._isRus ? "Кол-во в группе: " : "Number in the group: ") + str, new GUILayoutOption[0]);
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
		});
	}

	// Token: 0x0400020E RID: 526
	public static Vector2 PlayersScroll;

	// Token: 0x0400020F RID: 527
	public static Player SelectedPlayer;

	// Token: 0x04000210 RID: 528
	public static string SearchString = "";
}
