using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class DropDown
{
	// Token: 0x060000F4 RID: 244 RVA: 0x000252ED File Offset: 0x000234ED
	public DropDown()
	{
		this.IsEnabled = false;
		this.ListIndex = 0;
		this.ScrollView = Vector2.zero;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00029624 File Offset: 0x00027824
	public static DropDown Get(string identifier)
	{
		DropDown dropDown;
		DropDown result;
		if (DropDown.DropDownManager.TryGetValue(identifier, out dropDown))
		{
			result = dropDown;
		}
		else
		{
			dropDown = new DropDown();
			DropDown.DropDownManager.Add(identifier, dropDown);
			result = dropDown;
		}
		return result;
	}

	// Token: 0x0400004E RID: 78
	public static Dictionary<string, DropDown> DropDownManager = new Dictionary<string, DropDown>();

	// Token: 0x0400004F RID: 79
	public bool IsEnabled;

	// Token: 0x04000050 RID: 80
	public int ListIndex;

	// Token: 0x04000051 RID: 81
	public Vector2 ScrollView;
}
