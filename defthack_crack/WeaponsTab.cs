using System;
using SDG.Unturned;
using UnityEngine;

// Token: 0x020000BE RID: 190
internal class WeaponsTab
{
	// Token: 0x060005F6 RID: 1526 RVA: 0x000388EC File Offset: 0x00036AEC
	public static void Tab()
	{
		Prefab.MenuArea(new Rect(150f, 55f, 481f, 414f), "Weapon", delegate()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(230f)
			});
			Prefab.Toggle(ref WeaponOptions.NoRecoil, MenuComponent._isRus ? "Нет отдачи" : "No recoil", 17);
			Prefab.Toggle(ref WeaponOptions.NoSpread, MenuComponent._isRus ? "Нет разброса" : "No spread", 17);
			Prefab.Toggle(ref WeaponOptions.NoSway, MenuComponent._isRus ? "Нет увода" : "No sway", 17);
			Prefab.Toggle(ref WeaponOptions.NoDrop, MenuComponent._isRus ? "Нет баллистики" : "No drop", 17);
			Prefab.Toggle(ref TriggerbotOptions.Enabled, MenuComponent._isRus ? "Триггербот" : "Triggerbot", 17);
			Prefab.Toggle(ref WeaponOptions.OofOnDeath, MenuComponent._isRus ? "Звук после убийства" : "Sound after murder", 17);
			Prefab.Toggle(ref WeaponOptions.ShowWeaponInfo, MenuComponent._isRus ? "Показывать инфу о оружии" : "Show weapon info", 17);
			if (!RaycastOptions.UseCustomLimb && !RaycastOptions.UseRandomLimb)
			{
				Prefab.Toggle(ref RaycastOptions.AlwaysHitHead, MenuComponent._isRus ? "Стрелять всегда в голову" : "Always Hit Head", 17);
				if (RaycastOptions.AlwaysHitHead)
				{
					RaycastOptions.UseCustomLimb = false;
					RaycastOptions.UseRandomLimb = false;
				}
			}
			if (!RaycastOptions.AlwaysHitHead)
			{
				if (!RaycastOptions.UseRandomLimb && Prefab.Toggle(ref RaycastOptions.UseCustomLimb, MenuComponent._isRus ? "Кастомная конечность" : "Custom limb", 17))
				{
					RaycastOptions.UseRandomLimb = false;
					RaycastOptions.AlwaysHitHead = false;
				}
				if (!RaycastOptions.UseCustomLimb && Prefab.Toggle(ref RaycastOptions.UseRandomLimb, MenuComponent._isRus ? "Случайная конечность" : "Random limb", 17))
				{
					RaycastOptions.UseCustomLimb = false;
					RaycastOptions.AlwaysHitHead = false;
				}
			}
			GUILayout.Space(2f);
			GUIContent[] array = new GUIContent[]
			{
				new GUIContent(MenuComponent._isRus ? "Левая Стопа" : "Left Foot"),
				new GUIContent(MenuComponent._isRus ? "Левая Нога" : "Left Leg"),
				new GUIContent(MenuComponent._isRus ? "Правая Стопа" : "Right Foot"),
				new GUIContent(MenuComponent._isRus ? "Правая Нога" : "Right Leg"),
				new GUIContent(MenuComponent._isRus ? "Левая Рука" : "Left Hand"),
				new GUIContent(MenuComponent._isRus ? "Левая Кисть" : "Left Arm"),
				new GUIContent(MenuComponent._isRus ? "Правая Рука" : "Right Hand"),
				new GUIContent(MenuComponent._isRus ? "Правая Кисть" : "Right Arm"),
				new GUIContent("Left Back"),
				new GUIContent("Right Back"),
				new GUIContent("Left Front"),
				new GUIContent("Right Front"),
				new GUIContent("Spine"),
				new GUIContent(MenuComponent._isRus ? "Голова" : "Skull")
			};
			GUILayout.Space(2f);
			if (RaycastOptions.UseCustomLimb && !RaycastOptions.UseRandomLimb)
			{
				RaycastOptions.TargetLimb = (ELimb)Prefab.List(MenuComponent._isRus ? 180 : 140, new GUIContent((MenuComponent._isRus ? "Конечность: " : "Limb: ") + array[(int)RaycastOptions.TargetLimb].text), 13, (int)RaycastOptions.TargetLimb);
			}
			GUILayout.Space(2f);
			GUILayout.EndVertical();
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(230f)
			});
			Prefab.Toggle(ref WeaponOptions.Zoom, MenuComponent._isRus ? "Кратность увеличения(зум)" : "Magnification factor (zoom)", 17);
			if (WeaponOptions.Zoom)
			{
				GUILayout.Space(2f);
				GUILayout.Label((MenuComponent._isRus ? "Зум прицела: " : "Sight zoom: ") + WeaponOptions.ZoomValue.ToString(), new GUILayoutOption[0]);
				WeaponOptions.ZoomValue = GUILayout.HorizontalSlider(WeaponOptions.ZoomValue, 2f, 30f, new GUILayoutOption[0]);
			}
			GUILayout.Space(2f);
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		});
	}
}
