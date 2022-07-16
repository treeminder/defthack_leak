using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[Component]
[SpyComponent]
public class WeaponComponent : MonoBehaviour
{
	// Token: 0x060005B2 RID: 1458 RVA: 0x000266C2 File Offset: 0x000248C2
	public static byte Ammo()
	{
		return (byte)WeaponComponent.AmmoInfo.GetValue(OptimizationVariables.MainPlayer.equipment.useable);
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00037FE8 File Offset: 0x000361E8
	[Initializer]
	public static void Initialize()
	{
		ColorUtilities.addColor(new ColorVariable("_WeaponInfoColor", "Оружие - Информация", new Color32(0, byte.MaxValue, 0, byte.MaxValue), true));
		ColorUtilities.addColor(new ColorVariable("_WeaponInfoBorder", "Оружие - Информация (Граница)", new Color32(0, 0, 0, byte.MaxValue), true));
		ColorUtilities.addColor(new ColorVariable("_ShowFOVAim", "Отрисовка FOV Aim", new Color32(0, byte.MaxValue, 0, byte.MaxValue), true));
		ColorUtilities.addColor(new ColorVariable("_ShowFOV", "Отрисовка FOV SilentAim", new Color32(byte.MaxValue, 0, 0, byte.MaxValue), true));
		ColorUtilities.addColor(new ColorVariable("_CoordInfoColor", "Координаты - Информация", new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
		HotkeyComponent.ActionDict.Add("_ToggleTriggerbot", delegate
		{
			TriggerbotOptions.Enabled = !TriggerbotOptions.Enabled;
		});
		HotkeyComponent.ActionDict.Add("_ToggleNoRecoil", delegate
		{
			WeaponOptions.NoRecoil = !WeaponOptions.NoRecoil;
		});
		HotkeyComponent.ActionDict.Add("_ToggleNoSpread", delegate
		{
			WeaponOptions.NoSpread = !WeaponOptions.NoSpread;
		});
		HotkeyComponent.ActionDict.Add("_ToggleNoSway", delegate
		{
			WeaponOptions.NoSway = !WeaponOptions.NoSway;
		});
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x000266E2 File Offset: 0x000248E2
	public void Start()
	{
		base.StartCoroutine(WeaponComponent.UpdateWeapon());
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00038170 File Offset: 0x00036370
	public void OnGUI()
	{
		if (WeaponComponent.MainCamera == null)
		{
			WeaponComponent.MainCamera = Camera.main;
		}
		if (WeaponOptions.NoSway && OptimizationVariables.MainPlayer != null && OptimizationVariables.MainPlayer.animator != null)
		{
			OptimizationVariables.MainPlayer.animator.viewSway = Vector3.zero;
		}
		if (Event.current.type == EventType.Repaint && DrawUtilities.ShouldRun())
		{
			if (WeaponOptions.Tracers)
			{
				ESPComponent.GLMat.SetPass(0);
				GL.PushMatrix();
				GL.LoadProjectionMatrix(WeaponComponent.MainCamera.projectionMatrix);
				GL.modelview = WeaponComponent.MainCamera.worldToCameraMatrix;
				GL.Begin(1);
				for (int i = WeaponComponent.Tracers.Count - 1; i > -1; i--)
				{
					TracerLine tracerLine = WeaponComponent.Tracers[i];
					if (!(DateTime.Now - tracerLine.CreationTime > TimeSpan.FromSeconds(5.0)))
					{
						GL.Color(tracerLine.Hit ? ColorUtilities.getColor("_BulletTracersHitColor") : ColorUtilities.getColor("_BulletTracersColor"));
						GL.Vertex(tracerLine.StartPosition);
						GL.Vertex(tracerLine.EndPosition);
					}
					else
					{
						WeaponComponent.Tracers.Remove(tracerLine);
					}
				}
				GL.End();
				GL.PopMatrix();
			}
			if (WeaponOptions.ShowWeaponInfo && OptimizationVariables.MainPlayer.equipment.asset is ItemGunAsset)
			{
				GUI.depth = 0;
				ItemGunAsset itemGunAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
				string content = string.Format("<size=15>{0}\nДальность: {1}\nУрон игрокам: {2}</size>", itemGunAsset.itemName, itemGunAsset.range, itemGunAsset.playerDamageMultiplier.damage);
				DrawUtilities.DrawLabel(ESPComponent.ESPFont, LabelLocation.MiddleLeft, new Vector2((float)(Screen.width - 20), (float)(Screen.height / 2)), content, ColorUtilities.getColor("_WeaponInfoColor"), ColorUtilities.getColor("_WeaponInfoBorder"), 1, null, 12);
			}
			if (ESPOptions.ShowCoordinates)
			{
				float x = OptimizationVariables.MainPlayer.transform.position.x;
				float y = OptimizationVariables.MainPlayer.transform.position.y;
				float z = OptimizationVariables.MainPlayer.transform.position.z;
				string content2 = string.Format("<size=10>Координаты(X,Y,Z): {0},{1},{2}</size>", Math.Round((double)x, 2).ToString(), Math.Round((double)y, 2).ToString(), Math.Round((double)z, 2).ToString());
				DrawUtilities.DrawLabel(ESPComponent.ESPFont, LabelLocation.TopRight, new Vector2((float)(Screen.width / Screen.width + 10), (float)(Screen.height / 38)), content2, ColorUtilities.getColor("_CoordInfoColor"), Color.black, 1, null, 12);
			}
			if (RaycastOptions.ShowSilentAimUseFOV && RaycastOptions.Enabled)
			{
				DrawUtilities.DrawCircle(AssetVariables.Materials["ESP"], ColorUtilities.getColor("_ShowFOV"), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), WeaponComponent.FOVRadius(RaycastOptions.SilentAimFOV));
			}
			if (RaycastOptions.ShowAimUseFOV && AimbotOptions.Enabled)
			{
				DrawUtilities.DrawCircle(AssetVariables.Materials["ESP"], ColorUtilities.getColor("_ShowFOVAim"), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), WeaponComponent.FOVRadius(AimbotOptions.FOV));
			}
		}
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x000384EC File Offset: 0x000366EC
	internal static float FOVRadius(float fov)
	{
		float fieldOfView = OptimizationVariables.MainCam.fieldOfView;
		return (float)(Math.Tan((double)fov * 0.017453292519943295 / 2.0) / Math.Tan((double)fieldOfView * 0.017453292519943295 / 2.0) * (double)Screen.height);
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x000266F0 File Offset: 0x000248F0
	public static IEnumerator UpdateWeapon()
	{
		for (; ; )
		{
			yield return new WaitForSeconds(0.1f);
			if (DrawUtilities.ShouldRun())
			{
				ItemGunAsset PAsset;
				if ((PAsset = (OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset)) != null)
				{
					if (WeaponOptions.Zoom && !PlayerCoroutines.IsSpying)
					{
						float num2 = 90f / WeaponOptions.ZoomValue;
						WeaponComponent.ZoomInfo.SetValue(Player.player.equipment.useable, num2);
						Player.player.look.scopeCamera.fieldOfView = num2;
						bool flag9;
						if (((UseableGun)Player.player.equipment.useable).isAiming)
						{
							EPlayerPerspective perspective = Player.player.look.perspective;
							flag9 = false;
						}
						else
						{
							flag9 = false;
						}
						if (flag9)
						{
							WeaponComponent.fov.SetValue(Player.player.look, num2);
						}
					}
					if (!WeaponComponent.AssetBackups.ContainsKey(PAsset.id))
					{
						float[] Backups = new float[]
						{
							PAsset.recoilAim,
							PAsset.recoilMax_x,
							PAsset.recoilMax_y,
							PAsset.recoilMin_x,
							PAsset.recoilMin_y,
							PAsset.spreadAim,
							PAsset.spreadHip
						};
						Backups[6] = PAsset.spreadHip;
						WeaponComponent.AssetBackups.Add(PAsset.id, Backups);
					}
					if (WeaponOptions.NoRecoil && !PlayerCoroutines.IsSpying)
					{
						PAsset.recoilAim = 0f;
						PAsset.recoilMax_x = 0f;
						PAsset.recoilMax_y = 0f;
						PAsset.recoilMin_x = 0f;
						PAsset.recoilMin_y = 0f;
					}
					else
					{
						PAsset.recoilAim = WeaponComponent.AssetBackups[PAsset.id][0];
						PAsset.recoilMax_x = WeaponComponent.AssetBackups[PAsset.id][1];
						PAsset.recoilMax_y = WeaponComponent.AssetBackups[PAsset.id][2];
						PAsset.recoilMin_x = WeaponComponent.AssetBackups[PAsset.id][3];
						PAsset.recoilMin_y = WeaponComponent.AssetBackups[PAsset.id][4];
					}
					if (WeaponOptions.NoSpread && !PlayerCoroutines.IsSpying)
					{
						PAsset.spreadAim = 0f;
						PAsset.spreadHip = 0f;
						PlayerUI.updateCrosshair(0f);
					}
					else
					{
						PAsset.spreadAim = WeaponComponent.AssetBackups[PAsset.id][5];
						PAsset.spreadHip = WeaponComponent.AssetBackups[PAsset.id][6];
						WeaponComponent.UpdateCrosshair.Invoke(OptimizationVariables.MainPlayer.equipment.useable, null);
					}
				}
			}
		}
		yield break;
	}





	// Token: 0x040002BD RID: 701
	public static Dictionary<ushort, float[]> AssetBackups = new Dictionary<ushort, float[]>();

	// Token: 0x040002BE RID: 702
	public static List<TracerLine> Tracers = new List<TracerLine>();

	// Token: 0x040002BF RID: 703
	public static Camera MainCamera;

	// Token: 0x040002C0 RID: 704
	public static FieldInfo ZoomInfo = typeof(UseableGun).GetField("zoom", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x040002C1 RID: 705
	public static FieldInfo AmmoInfo = typeof(UseableGun).GetField("ammo", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x040002C2 RID: 706
	public static MethodInfo UpdateCrosshair = typeof(UseableGun).GetMethod("updateCrosshair", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x040002C3 RID: 707
	public static FieldInfo attachments1 = typeof(UseableGun).GetField("firstAttachments", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x040002C4 RID: 708
	public static FieldInfo fov = typeof(PlayerLook).GetField("fov", BindingFlags.Instance | BindingFlags.NonPublic);
}
