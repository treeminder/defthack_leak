using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x0200004E RID: 78
public class MenuTabOption
{
	// Token: 0x06000232 RID: 562 RVA: 0x0002E8B4 File Offset: 0x0002CAB4
	public static void Add(MenuTabOption tab)
	{
		if (!MenuTabOption.Contains(tab))
		{
			MenuTabOption.tabs[MenuTabOption.cPageIndex].Add(tab);
			tab.page = MenuTabOption.cPageIndex;
			MenuTabOption.cTabIndex++;
			if (MenuTabOption.cTabIndex % 9 == 0)
			{
				MenuTabOption.cTabIndex = 0;
				MenuTabOption.cPageIndex++;
			}
		}
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0002E910 File Offset: 0x0002CB10
	public static bool Contains(MenuTabOption tab)
	{
		bool result = false;
		foreach (MenuTabOption menuTabOption in MenuTabOption.tabs.SelectMany((List<MenuTabOption> t) => t))
		{
			if (tab.name == menuTabOption.name)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00025774 File Offset: 0x00023974
	public MenuTabOption(string name, MenuTabOption.MenuTab tab)
	{
		this.tab = tab;
		this.name = name;
	}

	// Token: 0x04000133 RID: 307
	public MenuTabOption.MenuTab tab;

	// Token: 0x04000134 RID: 308
	public string name;

	// Token: 0x04000135 RID: 309
	public bool enabled;

	// Token: 0x04000136 RID: 310
	public static MenuTabOption CurrentTab;

	// Token: 0x04000137 RID: 311
	public int page;

	// Token: 0x04000138 RID: 312
	public static int cTabIndex = 0;

	// Token: 0x04000139 RID: 313
	public static int cPageIndex = 0;

	// Token: 0x0400013A RID: 314
	public static List<MenuTabOption>[] tabs = new List<MenuTabOption>[]
	{
		new List<MenuTabOption>(),
		new List<MenuTabOption>()
	};

	// Token: 0x0200004F RID: 79
	// (Invoke) Token: 0x0600023A RID: 570
	public delegate void MenuTab();
}
