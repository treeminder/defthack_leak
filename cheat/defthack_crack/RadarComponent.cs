using System;
using SDG.Unturned;
using UnityEngine;

// Token: 0x0200008E RID: 142
[Component]
public class RadarComponent : MonoBehaviour
{
	// Token: 0x0600048A RID: 1162 RVA: 0x000260F9 File Offset: 0x000242F9
	[OnSpy]
	public static void Disable()
	{
		RadarComponent.WasEnabled = RadarOptions.Enabled;
		RadarOptions.Enabled = false;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0002610B File Offset: 0x0002430B
	[OffSpy]
	public static void Enable()
	{
		RadarOptions.Enabled = RadarComponent.WasEnabled;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00034544 File Offset: 0x00032744
	public void OnGUI()
	{
		if (RadarOptions.Enabled && Provider.isConnected && !Provider.isLoading)
		{
			RadarComponent.vew.width = (RadarComponent.vew.height = RadarOptions.RadarSize + 10f);
			GUI.color = new Color(1f, 1f, 1f, 0f);
			RadarComponent.veww = GUILayout.Window(345, RadarComponent.vew, new GUI.WindowFunction(this.RadarMenu), "Radar", new GUILayoutOption[0]);
			RadarComponent.vew.x = RadarComponent.veww.x;
			RadarComponent.vew.y = RadarComponent.veww.y;
			GUI.color = Color.white;
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0003460C File Offset: 0x0003280C
	public void RadarMenu(int windowID)
	{
		Drawing.DrawRect(new Rect(0f, 0f, RadarComponent.vew.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
		Drawing.DrawRect(new Rect(0f, 20f, RadarComponent.vew.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
		Drawing.DrawRect(new Rect(0f, 25f, RadarComponent.vew.width, RadarComponent.vew.height + 25f), new Color32(64, 64, 64, byte.MaxValue), null);
		GUILayout.Space(-19f);
		GUILayout.Label("Radar", new GUILayoutOption[0]);
		Vector2 vector = new Vector2(RadarComponent.vew.width / 2f, (RadarComponent.vew.height + 25f) / 2f);
		RadarComponent.radarcenter = new Vector2(RadarComponent.vew.width / 2f, (RadarComponent.vew.height + 25f) / 2f);
		Vector2 vector2 = RadarComponent.GameToRadarPosition(Player.player.transform.position);
		if (RadarOptions.TrackPlayer)
		{
			RadarComponent.radarcenter.x = RadarComponent.radarcenter.x - vector2.x;
			RadarComponent.radarcenter.y = RadarComponent.radarcenter.y + vector2.y;
		}
		Drawing.DrawRect(new Rect(vector.x, 25f, 1f, RadarComponent.vew.height), Color.gray, null);
		Drawing.DrawRect(new Rect(0f, vector.y, RadarComponent.vew.width, 1f), Color.gray, null);
		this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector2.x, RadarComponent.radarcenter.y - vector2.y), Color.black, 4f);
		this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector2.x, RadarComponent.radarcenter.y - vector2.y), Color.white, 3f);
		if (RadarOptions.ShowVehicles)
		{
			foreach (InteractableVehicle interactableVehicle in VehicleManager.vehicles)
			{
				if (!RadarOptions.ShowVehiclesUnlocked)
				{
					Vector2 vector3 = RadarComponent.GameToRadarPosition(interactableVehicle.transform.position);
					this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector3.x, RadarComponent.radarcenter.y - vector3.y), Color.black, 3f);
					this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector3.x, RadarComponent.radarcenter.y - vector3.y), ColorUtilities.getColor("_Vehicles"), 2f);
				}
				else if (!interactableVehicle.isLocked)
				{
					Vector2 vector4 = RadarComponent.GameToRadarPosition(interactableVehicle.transform.position);
					this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector4.x, RadarComponent.radarcenter.y - vector4.y), Color.black, 3f);
					this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector4.x, RadarComponent.radarcenter.y - vector4.y), ColorUtilities.getColor("_Vehicles"), 2f);
				}
			}
		}
		if (RadarOptions.ShowPlayers)
		{
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				if (steamPlayer.player != OptimizationVariables.MainPlayer)
				{
					Vector2 vector5 = RadarComponent.GameToRadarPosition(steamPlayer.player.transform.position);
					this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector5.x, RadarComponent.radarcenter.y - vector5.y), Color.black, 3f);
					this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector5.x, RadarComponent.radarcenter.y - vector5.y), ColorUtilities.getColor("_Players"), 2f);
				}
			}
		}
		if (MiscComponent.LastDeath != new Vector3(0f, 0f, 0f))
		{
			Vector2 vector6 = RadarComponent.GameToRadarPosition(MiscComponent.LastDeath);
			this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector6.x, RadarComponent.radarcenter.y - vector6.y), Color.black, 4f);
			this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector6.x, RadarComponent.radarcenter.y - vector6.y), Color.grey, 3f);
		}
		GUI.DragWindow();
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00026117 File Offset: 0x00024317
	public void DrawRadarDot(Vector2 pos, Color color, float size = 2f)
	{
		Drawing.DrawRect(new Rect(pos.x - size, pos.y - size, size * 2f, size * 2f), color, null);
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00034B94 File Offset: 0x00032D94
	public static Vector2 GameToRadarPosition(Vector3 pos)
	{
		Vector2 result;
		result.x = pos.x / ((float)Level.size / (RadarOptions.RadarZoom * RadarOptions.RadarSize));
		result.y = pos.z / ((float)Level.size / (RadarOptions.RadarZoom * RadarOptions.RadarSize));
		return result;
	}

	// Token: 0x04000218 RID: 536
	public static Rect veww;

	// Token: 0x04000219 RID: 537
	public static Rect vew = new Rect((float)Screen.width - RadarOptions.RadarSize - 20f, 10f, RadarOptions.RadarSize + 10f, RadarOptions.RadarSize + 10f);

	// Token: 0x0400021A RID: 538
	public static Vector2 radarcenter;

	// Token: 0x0400021B RID: 539
	public static bool WasEnabled;
}
