using System;
using SDG.Unturned;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[Component]
public class VanishPlayerComponent : MonoBehaviour
{
	// Token: 0x06000577 RID: 1399 RVA: 0x00026569 File Offset: 0x00024769
	[OnSpy]
	public static void Disable()
	{
		VanishPlayerComponent.WasEnabled = ESPOptions.ShowVanishPlayers;
		ESPOptions.ShowVanishPlayers = false;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0002657B File Offset: 0x0002477B
	[OffSpy]
	public static void Enable()
	{
		ESPOptions.ShowVanishPlayers = VanishPlayerComponent.WasEnabled;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0003695C File Offset: 0x00034B5C
	public void OnGUI()
	{
		if (ESPOptions.ShowVanishPlayers)
		{
			GUI.color = new Color(1f, 1f, 1f, 0f);
			VanishPlayerComponent.vew = GUILayout.Window(350, VanishPlayerComponent.vew, new GUI.WindowFunction(this.PlayersMenu), "Игроки в вашине", new GUILayoutOption[0]);
			GUI.color = Color.white;
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x000369C4 File Offset: 0x00034BC4
	public void PlayersMenu(int windowID)
	{
		Drawing.DrawRect(new Rect(0f, 0f, VanishPlayerComponent.vew.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
		Drawing.DrawRect(new Rect(0f, 20f, VanishPlayerComponent.vew.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
		Drawing.DrawRect(new Rect(0f, 25f, VanishPlayerComponent.vew.width, VanishPlayerComponent.vew.height + 25f), new Color32(64, 64, 64, byte.MaxValue), null);
		GUILayout.Space(-19f);
		GUILayout.Label("Vanish Players", new GUILayoutOption[0]);
		foreach (SteamPlayer steamPlayer in Provider.clients)
		{
			if (Vector3.Distance(steamPlayer.player.transform.position, Vector3.zero) < 10f)
			{
				GUILayout.Label(steamPlayer.playerID.characterName, new GUILayoutOption[0]);
			}
		}
		GUI.DragWindow();
	}

	// Token: 0x0400029E RID: 670
	public static bool WasEnabled;

	// Token: 0x0400029F RID: 671
	public static Rect vew = new Rect(1075f, 10f, 200f, 300f);
}
