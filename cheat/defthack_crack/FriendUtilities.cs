using System;
using SDG.Unturned;

// Token: 0x02000031 RID: 49
public static class FriendUtilities
{
	// Token: 0x06000189 RID: 393 RVA: 0x0002BF6C File Offset: 0x0002A16C
	public static bool IsFriendly(Player player)
	{
		return (player.quests.isMemberOfSameGroupAs(OptimizationVariables.MainPlayer) && ESPOptions.UsePlayerGroup) || MiscOptions.Friends.Contains(player.channel.owner.playerID.steamID.m_SteamID);
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
	public static void AddFriend(Player Friend)
	{
		ulong steamID = Friend.channel.owner.playerID.steamID.m_SteamID;
		if (!MiscOptions.Friends.Contains(steamID))
		{
			MiscOptions.Friends.Add(steamID);
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0002BFFC File Offset: 0x0002A1FC
	public static void RemoveFriend(Player Friend)
	{
		ulong steamID = Friend.channel.owner.playerID.steamID.m_SteamID;
		if (MiscOptions.Friends.Contains(steamID))
		{
			MiscOptions.Friends.Remove(steamID);
		}
	}
}
