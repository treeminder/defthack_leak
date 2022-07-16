using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public static class MiscTabs
{
	// Token: 0x060002D2 RID: 722 RVA: 0x00030418 File Offset: 0x0002E618
	public static void Tab()
	{
		Prefab.MenuArea(new Rect(150f, 55f, 481f, 414f), MenuComponent._isRus ? "ПРОЧЕЕ" : "OTHER", delegate()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			if (GUILayout.Button(MenuComponent._isRus ? "ПРОЧЕЕ" : "Other", (MiscTabs._selectMiscTab == 1) ? MenuComponent.skin.GetStyle("ToolBarButtonsActive") : MenuComponent.skin.GetStyle("ToolBarButtons"), new GUILayoutOption[0]))
			{
				MiscTabs._selectMiscTab = 1;
			}
			if (GUILayout.Button(MenuComponent._isRus ? "СТАТИСТИКА" : "Statistics", (MiscTabs._selectMiscTab == 2) ? MenuComponent.skin.GetStyle("ToolBarButtonsActive") : MenuComponent.skin.GetStyle("ToolBarButtons"), new GUILayoutOption[0]))
			{
				MiscTabs._selectMiscTab = 2;
			}
			if (GUILayout.Button(MenuComponent._isRus ? "ИГРОКИ" : "Players", (MiscTabs._selectMiscTab == 3) ? MenuComponent.skin.GetStyle("ToolBarButtonsActive") : MenuComponent.skin.GetStyle("ToolBarButtons"), new GUILayoutOption[0]))
			{
				MiscTabs._selectMiscTab = 3;
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			Prefab.MenuArea(new Rect(5f, 30f, 481f, 414f), MenuComponent._isRus ? "Прочее" : "Other", delegate()
			{
				switch (MiscTabs._selectMiscTab)
				{
				case 1:
					MiscTab.Tab();
					return;
				case 2:
					StatsTab.Tab();
					return;
				case 3:
					PlayersTab.Tab();
					return;
				default:
					return;
				}
			});
		});
	}

	// Token: 0x040001A1 RID: 417
	private static int _selectMiscTab = 1;
}
