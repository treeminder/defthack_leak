using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
public static class OptionsTab
{
	// Token: 0x060002E9 RID: 745 RVA: 0x00030D68 File Offset: 0x0002EF68
	public static void Tab()
	{
		Prefab.MenuArea(new Rect(150f, 55f, 481f, 414f), MenuComponent._isRus ? "ОПЦИИ" : "Options", delegate()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			if (GUILayout.Button(MenuComponent._isRus ? "ОПЦИИ" : "Options", (OptionsTab._selectMiscTab == 1) ? MenuComponent.skin.GetStyle("ToolBarButtonsActive") : MenuComponent.skin.GetStyle("ToolBarButtons"), new GUILayoutOption[0]))
			{
				OptionsTab._selectMiscTab = 1;
			}
			if (GUILayout.Button(MenuComponent._isRus ? "ЦВЕТА" : "Colors", (OptionsTab._selectMiscTab == 2) ? MenuComponent.skin.GetStyle("ToolBarButtonsActive") : MenuComponent.skin.GetStyle("ToolBarButtons"), new GUILayoutOption[0]))
			{
				OptionsTab._selectMiscTab = 2;
			}
			if (GUILayout.Button(MenuComponent._isRus ? "КНОПКИ" : "HotKeys", (OptionsTab._selectMiscTab == 3) ? MenuComponent.skin.GetStyle("ToolBarButtonsActive") : MenuComponent.skin.GetStyle("ToolBarButtons"), new GUILayoutOption[0]))
			{
				OptionsTab._selectMiscTab = 3;
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			Prefab.MenuArea(new Rect(5f, 30f, 481f, 414f), MenuComponent._isRus ? "ОПЦИИ" : "Options", delegate()
			{
				switch (OptionsTab._selectMiscTab)
				{
				case 1:
					MoreMiscTab.Tab();
					return;
				case 2:
					ColorsTab.Tab();
					return;
				case 3:
					HotkeyTab.Tab();
					return;
				default:
					return;
				}
			});
		});
	}

	// Token: 0x040001AF RID: 431
	private static int _selectMiscTab = 1;
}
