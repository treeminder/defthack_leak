using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000041 RID: 65
public static class ItemUtilities
{
	// Token: 0x060001D0 RID: 464 RVA: 0x0002D580 File Offset: 0x0002B780
	public static bool Whitelisted(ItemAsset asset, ItemOptionList OptionList)
	{
		return (OptionList.ItemfilterCustom && OptionList.AddedItems.Contains(asset.id)) || (OptionList.ItemfilterGun && asset is ItemGunAsset) || (OptionList.ItemfilterGunMeel && asset is ItemMeleeAsset) || (OptionList.ItemfilterAmmo && asset is ItemMagazineAsset) || (OptionList.ItemfilterMedical && asset is ItemMedicalAsset) || (OptionList.ItemfilterFoodAndWater && (asset is ItemFoodAsset || asset is ItemWaterAsset)) || (OptionList.ItemfilterBackpack && asset is ItemBackpackAsset) || (OptionList.ItemfilterCharges && asset is ItemChargeAsset) || (OptionList.ItemfilterFuel && asset is ItemFuelAsset) || (OptionList.ItemfilterClothing && asset is ItemClothingAsset);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0002D648 File Offset: 0x0002B848
	public static void DrawItemButton(ItemAsset asset, HashSet<ushort> AddedItems)
	{
		string text = asset.itemName;
		if (asset.itemName.Length > 60)
		{
			text = asset.itemName.Substring(0, 60) + "..";
		}
		if (GUILayout.Button(text, new GUILayoutOption[]
		{
			GUILayout.Width(515f)
		}))
		{
			if (AddedItems.Contains(asset.id))
			{
				AddedItems.Remove(asset.id);
			}
			else
			{
				AddedItems.Add(asset.id);
			}
		}
		GUILayout.Space(3f);
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
	public static void DrawFilterTab(ItemOptionList OptionList)
	{
		System.Action two = null;
		System.Action tri = null;
		System.Action one = null;
		Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), MenuComponent._isRus ? "ФИЛЬТР ПРЕДМЕТОВ" : "OBJECT FILTER", delegate
		{
			Prefab.Toggle(MenuComponent._isRus ? "Оружие" : "Weapon", ref OptionList.ItemfilterGun, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Оружие ближнего боя" : "Melee weapon", ref OptionList.ItemfilterGunMeel, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Боеприпасы" : "Ammunition", ref OptionList.ItemfilterAmmo, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Медикаменты" : "Medication", ref OptionList.ItemfilterMedical, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Рюкзаки" : "Backpacks", ref OptionList.ItemfilterBackpack, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Charges" : "Charges", ref OptionList.ItemfilterCharges, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Топливо" : "Fuel", ref OptionList.ItemfilterFuel, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Одежда" : "clothing", ref OptionList.ItemfilterClothing, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Провизия" : "Provisions", ref OptionList.ItemfilterFoodAndWater, 17);
			Prefab.Toggle(MenuComponent._isRus ? "Настройка фильтра" : "Filter setting", ref OptionList.ItemfilterCustom, 17);
			if (OptionList.ItemfilterCustom)
			{
				GUILayout.Space(5f);
				string header = MenuComponent._isRus ? "Кастомизация фильтра" : "Filter customization";
				System.Action code;
				if ((code = one) == null)
				{
					code = (one = delegate()
					{
						GUILayout.BeginHorizontal(new GUILayoutOption[0]);
						GUILayout.Space(55f);
						OptionList.searchstring = Prefab.TextField(OptionList.searchstring, MenuComponent._isRus ? "Поиск:" : "Search:");
						GUILayout.Space(5f);
						if (GUILayout.Button(MenuComponent._isRus ? "Обновить" : "Refresh", new GUILayoutOption[0]))
						{
							ItemsComponent.RefreshItems();
						}
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						Rect rect = new Rect(70f, 50f, 540f, 190f);
						ItemOptionList optionList = OptionList;
						System.Action code2;
						if ((code2 = two) == null)
						{
							code2 = (two = delegate()
							{
								GUILayout.Space(5f);
								foreach (ItemAsset itemAsset in ItemsComponent.items)
								{
									if (itemAsset.itemName.ToLower().Contains(OptionList.searchstring.ToLower()) && !OptionList.AddedItems.Contains(itemAsset.id))
									{
										ItemUtilities.DrawItemButton(itemAsset, OptionList.AddedItems);
									}
								}
								GUILayout.Space(2f);
							});
						}
						Prefab.ScrollView(rect, ref optionList.additemscroll, code2);
						Rect rect2 = new Rect(70f, 245f, 540f, 191f);
						ItemOptionList optionList2 = OptionList;
						System.Action code3;
						if ((code3 = tri) == null)
						{
							code3 = (tri = delegate()
							{
								GUILayout.Space(5f);
								for (int i = 0; i < ItemsComponent.items.Count; i++)
								{
									ItemAsset itemAsset = ItemsComponent.items[i];
									bool flag = false;
									if (itemAsset.itemName.ToLower().Contains(OptionList.searchstring.ToLower()))
									{
										flag = true;
									}
									if (!OptionList.AddedItems.Contains(itemAsset.id))
									{
										flag = false;
									}
									if (flag)
									{
										ItemUtilities.DrawItemButton(itemAsset, OptionList.AddedItems);
									}
								}
								GUILayout.Space(2f);
							});
						}
						Prefab.ScrollView(rect2, ref optionList2.removeitemscroll, code3);
					});
				}
				Prefab.SectionTab(new Rect(0f, 0f, 640f, 480f), header, code);
			}
		});
	}
}
