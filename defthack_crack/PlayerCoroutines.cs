using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SDG.NetPak;
using SDG.NetTransport;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000084 RID: 132
public static class PlayerCoroutines
{
	// Token: 0x060003F8 RID: 1016 RVA: 0x00025FED File Offset: 0x000241ED
	[Obsolete]
	public static IEnumerator TakeScreenshot()
	{
		Player mainPlayer = OptimizationVariables.MainPlayer;
		SteamChannel channel = mainPlayer.channel;
		switch (MiscOptions.AntiSpyMethod)
		{
		case 0:
		{
			if (Time.realtimeSinceStartup - PlayerCoroutines.LastSpy < 0.5f || PlayerCoroutines.IsSpying)
			{
				yield break;
			}
			PlayerCoroutines.IsSpying = true;
			PlayerCoroutines.LastSpy = Time.realtimeSinceStartup;
			if (!MiscOptions.PanicMode)
			{
				PlayerCoroutines.DisableAllVisuals();
			}
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
			{
				name = "Screenshot_Raw",
				hideFlags = HideFlags.HideAndDontSave
			};
			texture2D.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0, false);
			Texture2D texture2D2 = new Texture2D(640, 480, TextureFormat.RGB24, false)
			{
				name = "Screenshot_Final",
				hideFlags = HideFlags.HideAndDontSave
			};
			Color[] pixels = texture2D.GetPixels();
			Color[] array = new Color[texture2D2.width * texture2D2.height];
			float num = (float)texture2D.width / (float)texture2D2.width;
			float num2 = (float)texture2D.height / (float)texture2D2.height;
			for (int i = 0; i < texture2D2.height; i++)
			{
				int num3 = (int)((float)i * num2) * texture2D.width;
				int num4 = i * texture2D2.width;
				for (int j = 0; j < texture2D2.width; j++)
				{
					int num5 = (int)((float)j * num);
					array[num4 + j] = pixels[num3 + num5];
				}
			}
			texture2D2.SetPixels(array);
			byte[] data = texture2D2.EncodeToJPG(33);
			if (data.Length < 30000)
			{
				if (!Provider.isServer)
				{
					ServerInstanceMethod.Get(typeof(Player), "ReceiveScreenshotRelay").Invoke(Player.player.GetNetId(), ENetReliability.Reliable, delegate(NetPakWriter writer)
					{
						ushort num16 = (ushort)data.Length;
						writer.WriteUInt16(num16);
						writer.WriteBytes(data, (int)num16);
					});
				}
				else
				{
					PlayerCoroutines._HandleScreenshotData(data, channel);
				}
			}
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			PlayerCoroutines.IsSpying = false;
			if (!MiscOptions.PanicMode)
			{
				PlayerCoroutines.EnableAllVisuals();
			}
			break;
		}
		case 1:
		{
			System.Random random = new System.Random();
			string[] files = Directory.GetFiles(MiscOptions.AntiSpyPath);
			byte[] data2 = File.ReadAllBytes(files[random.Next(files.Length)]);
			Texture2D texture2D3 = new Texture2D(2, 2);
			texture2D3.LoadImage(data2);
			Texture2D texture2D4 = new Texture2D(640, 480, TextureFormat.RGB24, false)
			{
				name = "Screenshot_Final",
				hideFlags = HideFlags.HideAndDontSave
			};
			Color[] pixels2 = texture2D3.GetPixels();
			Color[] array2 = new Color[texture2D4.width * texture2D4.height];
			float num6 = (float)texture2D3.width / (float)texture2D4.width;
			float num7 = (float)texture2D3.height / (float)texture2D4.height;
			for (int k = 0; k < texture2D4.height; k++)
			{
				int num8 = (int)((float)k * num7) * texture2D3.width;
				int num9 = k * texture2D4.width;
				for (int l = 0; l < texture2D4.width; l++)
				{
					int num10 = (int)((float)l * num6);
					array2[num9 + l] = pixels2[num8 + num10];
				}
			}
			texture2D4.SetPixels(array2);
			byte[] data = texture2D4.EncodeToJPG(33);
			if (data.Length < 30000)
			{
				if (Provider.isServer)
				{
					PlayerCoroutines._HandleScreenshotData(data, channel);
				}
				else
				{
					ServerInstanceMethod.Get(typeof(Player), "ReceiveScreenshotRelay").Invoke(Player.player.GetNetId(), ENetReliability.Reliable, delegate(NetPakWriter writer)
					{
						ushort num16 = (ushort)data.Length;
						writer.WriteUInt16(num16);
						writer.WriteBytes(data, (int)num16);
					});
				}
			}
			break;
		}
		case 3:
		{
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			Texture2D texture2D5 = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
			{
				name = "Screenshot_Raw",
				hideFlags = HideFlags.HideAndDontSave
			};
			texture2D5.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0, false);
			Texture2D texture2D6 = new Texture2D(640, 480, TextureFormat.RGB24, false)
			{
				name = "Screenshot_Final",
				hideFlags = HideFlags.HideAndDontSave
			};
			Color[] pixels3 = texture2D5.GetPixels();
			Color[] array3 = new Color[texture2D6.width * texture2D6.height];
			float num11 = (float)texture2D5.width / (float)texture2D6.width;
			float num12 = (float)texture2D5.height / (float)texture2D6.height;
			for (int m = 0; m < texture2D6.height; m++)
			{
				int num13 = (int)((float)m * num12) * texture2D5.width;
				int num14 = m * texture2D6.width;
				for (int n = 0; n < texture2D6.width; n++)
				{
					int num15 = (int)((float)n * num11);
					array3[num14 + n] = pixels3[num13 + num15];
				}
			}
			texture2D6.SetPixels(array3);
			byte[] data = texture2D6.EncodeToJPG(33);
			if (data.Length < 30000)
			{
				if (Provider.isServer)
				{
					PlayerCoroutines._HandleScreenshotData(data, channel);
				}
				else
				{
					ServerInstanceMethod.Get(typeof(Player), "ReceiveScreenshotRelay").Invoke(Player.player.GetNetId(), ENetReliability.Reliable, delegate(NetPakWriter writer)
					{
						ushort num16 = (ushort)data.Length;
						writer.WriteUInt16(num16);
						writer.WriteBytes(data, (int)num16);
					});
				}
			}
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			break;
		}
		}
		if (MiscOptions.AlertOnSpy)
		{
			OptimizationVariables.MainPlayer.StartCoroutine(PlayerCoroutines.ScreenShotMessageCoroutine());
		}
		yield break;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00032ED0 File Offset: 0x000310D0
	public static void _HandleScreenshotData(byte[] data, SteamChannel channel)
	{
		if (Dedicator.isDedicated)
		{
			ReadWrite.writeBytes(string.Concat(new string[]
			{
				ReadWrite.PATH,
				ServerSavedata.directory,
				"/",
				Provider.serverID,
				"/Spy.jpg"
			}), false, false, data);
			ReadWrite.writeBytes(string.Concat(new object[]
			{
				ReadWrite.PATH,
				ServerSavedata.directory,
				"/",
				Provider.serverID,
				"/Spy/",
				channel.owner.playerID.steamID.m_SteamID,
				".jpg"
			}), false, false, data);
			if (Player.player.onPlayerSpyReady != null)
			{
				Player.player.onPlayerSpyReady(channel.owner.playerID.steamID, data);
			}
			PlayerSpyReady playerSpyReady = new Queue<PlayerSpyReady>().Dequeue();
			if (playerSpyReady != null)
			{
				playerSpyReady(channel.owner.playerID.steamID, data);
				return;
			}
		}
		else
		{
			ReadWrite.writeBytes("/Spy.jpg", false, true, data);
			if (Player.onSpyReady != null)
			{
				Player.onSpyReady(channel.owner.playerID.steamID, data);
			}
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00025FF5 File Offset: 0x000241F5
	public static IEnumerator ScreenShotMessageCoroutine()
	{
		float started = Time.realtimeSinceStartup;
		bool flag2;
		do
		{
			yield return new WaitForEndOfFrame();
			if (!PlayerCoroutines.IsSpying)
			{
				PlayerUI.hint(null, EPlayerMessage.INTERACT, "Amdin Sana Spy Atıyo Loo", Color.red, new object[0]);
			}
			flag2 = (Time.realtimeSinceStartup - started > 3f);
		}
		while (!flag2);
		yield break;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00033004 File Offset: 0x00031204
	public static void DisableAllVisuals()
	{
		SpyManager.InvokePre();
		ItemGunAsset itemGunAsset;
		if (DrawUtilities.ShouldRun() && (itemGunAsset = (OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset)) != null)
		{
			PlayerUI.updateCrosshair((OptimizationVariables.MainPlayer.equipment.useable as UseableGun).isAiming ? WeaponComponent.AssetBackups[itemGunAsset.id][5] : WeaponComponent.AssetBackups[itemGunAsset.id][6]);
		}
		if (LevelLighting.seaLevel == 0f)
		{
			LevelLighting.seaLevel = MiscOptions.Altitude;
		}
		SpyManager.DestroyComponents();
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00025FFD File Offset: 0x000241FD
	public static void EnableAllVisuals()
	{
		SpyManager.AddComponents();
		SpyManager.InvokePost();
	}

	// Token: 0x04000200 RID: 512
	public static float LastSpy;

	// Token: 0x04000201 RID: 513
	public static bool IsSpying;

	// Token: 0x04000202 RID: 514
	public static Player SpecPlayer;
}
