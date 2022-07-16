using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x0200004C RID: 76
[Component]
[SpyComponent]
public class MenuComponent : MonoBehaviour
{
	// Token: 0x0600020E RID: 526 RVA: 0x0002E27C File Offset: 0x0002C47C
	
	//too lazy will fix later
	public void Update()
	{
		/*if (!MenuComponent.changedHwid)
		{
			foreach (byte[] array in LocalHwid.GetHwids())
			{
				for (int j = 0; j < 20; j++)
				{
					array[j] = (byte)UnityEngine.Random.Range(0, 256);
				}
			}
			byte[][] hwids;
			MenuComponent.hwidfield.SetValue(null, hwids);
			MenuComponent.changedHwid = true; 
		}*/ 
		HotkeyUtilities.Initialize();
		if (!HotkeyOptions.UnorganizedHotkeys.ContainsKey("_Menu"))
		{
			HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Прочее" : "Other", MenuComponent._isRus ? "Активация меню" : "Menu activation", "_MenuComponent", new KeyCode[]
			{
				KeyCode.F1
			});
		}
		if ((HotkeyOptions.UnorganizedHotkeys["_MenuComponent"].Keys.Length == 0 && Input.GetKeyDown(MenuComponent.MenuKey)) || HotkeyUtilities.IsHotkeyDown("_MenuComponent"))
		{
			MenuComponent.IsInMenu = !MenuComponent.IsInMenu;
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0002E368 File Offset: 0x0002C568
	public void OnGUI()
	{
		GUI.skin = MenuComponent.skin;
		if (MenuComponent.IsInMenu)
		{
			GUI.depth = -1;
			MenuComponent._windowRect = GUI.Window(0, MenuComponent._windowRect, new GUI.WindowFunction(this.DoMenu), "");
			GUI.depth = -2;
			this._cursor.x = Input.mousePosition.x;
			this._cursor.y = (float)Screen.height - Input.mousePosition.y;
			GUI.DrawTexture(this._cursor, MenuComponent._cursorTexture);
			Cursor.lockState = CursorLockMode.None;
			if (PlayerUI.window != null)
			{
				PlayerUI.window.showCursor = true;
			}
			if (MenuComponent._sectiontabVis)
			{
				MenuComponent._windowRect = GUI.Window(0, MenuComponent._windowRect, DoMenu, "");
			}
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0002E44C File Offset: 0x0002C64C
	public void DoMenu(int id)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("DeftHack", MenuComponent.skin.GetStyle("BigLabel"), new GUILayoutOption[0]);
		MenuComponent._style = GUIStyle.none;
		MenuComponent._style.padding = new RectOffset(0, 0, -4, -4);
		if (MenuComponent._isRus)
		{
			if (GUILayout.Button(MenuComponent.rus, MenuComponent._style, new GUILayoutOption[]
			{
				GUILayout.Width(30f),
				GUILayout.Height(22f)
			}))
			{
				MenuComponent._isRus = !MenuComponent._isRus;
			}
		}
		else if (GUILayout.Button(MenuComponent.usa, MenuComponent._style, new GUILayoutOption[]
		{
			GUILayout.Width(30f),
			GUILayout.Height(22f)
		}))
		{
			MenuComponent._isRus = !MenuComponent._isRus;
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(15f);
		GUILayout.BeginVertical(new GUILayoutOption[]
		{
			GUILayout.Width(120f),
			GUILayout.Height(370f)
		});
		if (GUILayout.Button(MenuComponent._isRus ? "ВХ" : "Visuals", (MenuComponent._selectTab == 1) ? MenuComponent.skin.GetStyle("TabButtons_active") : MenuComponent.skin.GetStyle("TabButtons"), new GUILayoutOption[0]))
		{
			MenuComponent._selectTab = 1;
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(MenuComponent._isRus ? "АИМБОТ" : "AimBot", (MenuComponent._selectTab == 2) ? MenuComponent.skin.GetStyle("TabButtons_active") : MenuComponent.skin.GetStyle("TabButtons"), new GUILayoutOption[0]))
		{
			MenuComponent._selectTab = 2;
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(MenuComponent._isRus ? "ОРУЖИЕ" : "Weapons", (MenuComponent._selectTab == 3) ? MenuComponent.skin.GetStyle("TabButtons_active") : MenuComponent.skin.GetStyle("TabButtons"), new GUILayoutOption[0]))
		{
			MenuComponent._selectTab = 3;
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(MenuComponent._isRus ? "ПРОЧЕЕ" : "Others", (MenuComponent._selectTab == 4) ? MenuComponent.skin.GetStyle("TabButtons_active") : MenuComponent.skin.GetStyle("TabButtons"), new GUILayoutOption[0]))
		{
			MenuComponent._selectTab = 4;
		}
		GUILayout.Space(5f);
		if (GUILayout.Button(MenuComponent._isRus ? "ОПЦИИ" : "Settings", (MenuComponent._selectTab == 5) ? MenuComponent.skin.GetStyle("TabButtons_active") : MenuComponent.skin.GetStyle("TabButtons"), new GUILayoutOption[0]))
		{
			MenuComponent._selectTab = 5;
		}
		GUILayout.Space(5f);
		GUILayout.EndVertical();
		Prefab.MenuArea(new Rect(18f, 245f, 125f, 91f), "КОНФИГ", delegate()
		{
			GUILayout.Label(MenuComponent._isRus ? "КОНФИГ" : "Config", new GUILayoutOption[0]);
			if (GUILayout.Button(MenuComponent._isRus ? "Сохранить" : "Save", new GUILayoutOption[]
			{
				GUILayout.Width(90f)
			}))
			{
				ConfigManager.SaveConfig(ConfigManager.CollectConfig());
			}
			GUILayout.Space(5f);
			if (GUILayout.Button(MenuComponent._isRus ? "Загрузить" : "Load", new GUILayoutOption[]
			{
				GUILayout.Width(90f)
			}))
			{
				ConfigManager.Init();
			}
		});
		switch (MenuComponent._selectTab)
		{
		case 1:
			VisualsTab.Tab();
			break;
		case 2:
			AimbotTab.Tab();
			break;
		case 3:
			WeaponsTab.Tab();
			break;
		case 4:
			MiscTabs.Tab();
			break;
		case 5:
			OptionsTab.Tab();
			break;
		}
		GUI.DragWindow();
	}

	// Token: 0x04000120 RID: 288
	public static bool IsInMenu;

	// Token: 0x04000121 RID: 289
	public static KeyCode MenuKey = KeyCode.F1;

	// Token: 0x04000122 RID: 290
	public Rect _cursor = new Rect(0f, 0f, 35f, 35f);

	// Token: 0x04000123 RID: 291
	public static Texture _cursorTexture;

	// Token: 0x04000124 RID: 292
	private static Rect _windowRect = new Rect(0f, 0f, 640f, 480f);

	// Token: 0x04000125 RID: 293
	[Save]
	public static bool _isRus = true;

	// Token: 0x04000126 RID: 294
	private static GUIStyle _style;

	// Token: 0x04000127 RID: 295
	private static int _selectTab = 1;

	// Token: 0x04000128 RID: 296
	public static GUISkin skin;

	// Token: 0x04000129 RID: 297
	public static Texture rus;

	// Token: 0x0400012A RID: 298
	public static Texture usa;

	// Token: 0x0400012B RID: 299
	public static bool _sectiontabVis = false;

	// Token: 0x0400012C RID: 300
	public static System.Action _sectiontabAction;

	// Token: 0x0400012D RID: 301
	private static readonly FieldInfo hwidfield = typeof(LocalHwid).GetField("cachedHwids", BindingFlags.Static | BindingFlags.NonPublic);

	// Token: 0x0400012E RID: 302
	public static bool changedHwid = false;

	// Token: 0x0400012F RID: 303
	public static bool check = false;
}
