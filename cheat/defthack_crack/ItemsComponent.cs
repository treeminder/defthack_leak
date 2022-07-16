using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000040 RID: 64
[Component]
public class ItemsComponent : MonoBehaviour
{
	// Token: 0x060001C8 RID: 456 RVA: 0x0002D518 File Offset: 0x0002B718
	public static void RefreshItems()
	{
		ItemsComponent.items.Clear();
		for (ushort num = 0; num < 65535; num += 1)
		{
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
			if (!string.IsNullOrEmpty((itemAsset != null) ? itemAsset.itemName : null) && !ItemsComponent.items.Contains(itemAsset))
			{
				ItemsComponent.items.Add(itemAsset);
			}
		}
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0002568E File Offset: 0x0002388E
	public void Start()
	{
		CoroutineComponent.ItemPickupCoroutine = base.StartCoroutine(ItemCoroutines.PickupItems());
	}

	// Token: 0x040000F9 RID: 249
	public static List<ItemAsset> items = new List<ItemAsset>();
}
