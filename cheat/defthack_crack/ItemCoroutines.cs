using SDG.Unturned;
using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003C RID: 60
public static class ItemCoroutines
{
	// Token: 0x060001B8 RID: 440 RVA: 0x00025643 File Offset: 0x00023843
	public static IEnumerator PickupItems()
	{
		for (; ; )
		{
			if (!DrawUtilities.ShouldRun() || !ItemOptions.AutoItemPickup)
			{
				yield return new WaitForSeconds(0.5f);
			}
			else
			{
				Collider[] array = Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, 19f, 8192);
				int num;
				for (int i = 0; i < array.Length; i = num + 1)
				{
					Collider col = array[i];
					if (!(col == null) && !(col.GetComponent<InteractableItem>() == null) && col.GetComponent<InteractableItem>().asset != null)
					{
						InteractableItem item = col.GetComponent<InteractableItem>();
						if (ItemUtilities.Whitelisted(item.asset, ItemOptions.ItemFilterOptions))
						{
							item.use();
						}
					}
					num = i;
				}
				yield return new WaitForSeconds((float)(ItemOptions.ItemPickupDelay / 1000));
			}
		}
		yield break;
	}
}
