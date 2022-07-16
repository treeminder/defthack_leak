using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public static class AimbotTab
{
	// Token: 0x06000043 RID: 67 RVA: 0x00026E60 File Offset: 0x00025060
	public static void Tab()
	{
		Prefab.MenuArea(new Rect(150f, 55f, 481f, 414f), "Aim", delegate()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(230f)
			});
			GUILayout.Space(2f);
			Prefab.Toggle(ref RaycastOptions.Enabled, MenuComponent._isRus ? "РейджАим" : "Silent/Rage Aim", 17);
			GUILayout.Space(10f);
			if (RaycastOptions.Enabled)
			{
				Prefab.Toggle(ref SphereOptions.SpherePrediction, MenuComponent._isRus ? "Авто радиус сферы" : "Auto sphere radius", 17);
				GUILayout.Space(5f);
				if (!SphereOptions.SpherePrediction)
				{
					GUILayout.Label((MenuComponent._isRus ? "Радиус сферы: " : "Sphere radius: ") + Math.Round((double)SphereOptions.SphereRadius, 2).ToString() + (MenuComponent._isRus ? "m" : "M"), new GUILayoutOption[0]);
					SphereOptions.SphereRadius = GUILayout.HorizontalSlider(SphereOptions.SphereRadius, 0f, 16f, new GUILayoutOption[0]);
				}
				GUILayout.Space(5f);
				GUIContent[] array = new GUIContent[]
				{
					new GUIContent(MenuComponent._isRus ? "Игроки" : "Players"),
					new GUIContent(MenuComponent._isRus ? "Зомби" : "Zombie"),
					new GUIContent(MenuComponent._isRus ? "Турели" : "Turrets"),
					new GUIContent(MenuComponent._isRus ? "Кровати" : "Beds"),
					new GUIContent(MenuComponent._isRus ? "Клейм флаг" : "Claim Flags"),
					new GUIContent(MenuComponent._isRus ? "Ящики" : "Storages"),
					new GUIContent(MenuComponent._isRus ? "Транспорт" : "Vehicles")
				};
				Prefab.Toggle(ref RaycastOptions.NoShootthroughthewalls, MenuComponent._isRus ? "Не стрелять через стены" : "Don't shoot through walls", 17);
				GUILayout.Space(2f);
				Prefab.Toggle(ref RaycastOptions.SilentAimUseFOV, MenuComponent._isRus ? "Использовать FOV" : "Use FOV", 17);
				if (RaycastOptions.SilentAimUseFOV)
				{
					Prefab.Toggle(ref RaycastOptions.ShowSilentAimUseFOV, MenuComponent._isRus ? "Отображать FOV" : "Display FOV", 17);
					GUILayout.Space(2f);
					GUILayout.Label("FOV: " + RaycastOptions.SilentAimFOV.ToString(), new GUILayoutOption[0]);
					RaycastOptions.SilentAimFOV = GUILayout.HorizontalSlider(RaycastOptions.SilentAimFOV, 1f, 180f, new GUILayoutOption[0]);
					if (RaycastOptions.SilentAimFOV == 1f)
					{
						RaycastOptions.SilentAimFOV = 2f;
					}
				}
				else
				{
					RaycastOptions.ShowSilentAimUseFOV = false;
				}
				RaycastOptions.Target = (TargetPriority)Prefab.List(MenuComponent._isRus ? 180 : 140, new GUIContent((MenuComponent._isRus ? "Цель: " : "Target: ") + array[(int)RaycastOptions.Target].text), 6, (int)RaycastOptions.Target);
				GUILayout.Space(5f);
			}
			GUILayout.EndVertical();
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			Prefab.Toggle(ref AimbotOptions.Enabled, MenuComponent._isRus ? "АИМ" : "AimBot", 17);
			if (AimbotOptions.Enabled)
			{
				Prefab.Toggle(ref AimbotOptions.Smooth, MenuComponent._isRus ? "Плавность" : "Smoothness", 17);
				Prefab.Toggle(ref AimbotOptions.OnKey, MenuComponent._isRus ? "По кнопке(F)" : "By button (F)", 17);
				GUILayout.Space(3f);
				if (AimbotOptions.Smooth)
				{
					GUILayout.Label((MenuComponent._isRus ? "Скорость аима: " : "Aim speed: ") + AimbotOptions.AimSpeed.ToString(), new GUILayoutOption[0]);
					AimbotOptions.AimSpeed = GUILayout.HorizontalSlider(AimbotOptions.AimSpeed, 1f, AimbotOptions.MaxSpeed, new GUILayoutOption[0]);
				}
				if (AimbotOptions.TargetMode == TargetMode.FOV)
				{
					Prefab.Toggle(ref AimbotOptions.UseFovAim, MenuComponent._isRus ? "Использовать FOV" : "Use FOV", 17);
					if (AimbotOptions.UseFovAim)
					{
						Prefab.Toggle(ref RaycastOptions.ShowAimUseFOV, MenuComponent._isRus ? "Отображать FOV" : "Display FOV", 17);
						AimbotOptions.TargetMode = TargetMode.FOV;
						GUILayout.Label("FOV: " + AimbotOptions.FOV.ToString(), new GUILayoutOption[0]);
						AimbotOptions.FOV = GUILayout.HorizontalSlider(AimbotOptions.FOV, 1f, 180f, new GUILayoutOption[0]);
						if (AimbotOptions.FOV == 1f)
						{
							AimbotOptions.FOV = 3f;
						}
					}
					else
					{
						RaycastOptions.ShowAimUseFOV = false;
					}
				}
				else if (AimbotOptions.TargetMode == TargetMode.Distance)
				{
					GUILayout.Label((MenuComponent._isRus ? "Дистанция: " : "Distance: ") + AimbotOptions.Distance.ToString(), new GUILayoutOption[0]);
					AimbotOptions.Distance = GUILayout.HorizontalSlider(AimbotOptions.Distance, 50f, 1000f, new GUILayoutOption[0]);
				}
				GUIContent[] array2 = new GUIContent[]
				{
					new GUIContent(MenuComponent._isRus ? "Дистанция" : "Distance"),
					new GUIContent("FOV")
				};
				AimbotOptions.TargetMode = (TargetMode)Prefab.List(180, new GUIContent((MenuComponent._isRus ? "Наводится: " : "Target Mode: ") + array2[(int)AimbotOptions.TargetMode].text), 1, (int)AimbotOptions.TargetMode);
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		});
	}

	// Token: 0x04000016 RID: 22
	[Save]
	public static bool experimental;
}
