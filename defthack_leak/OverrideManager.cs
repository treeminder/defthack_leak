using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class OverrideManager
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060000A7 RID: 167 RVA: 0x000251FC File Offset: 0x000233FC
	public Dictionary<OverrideAttribute, OverrideWrapper> Overrides
	{
		get
		{
			return OverrideManager._overrides;
		}
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0002825C File Offset: 0x0002645C
	public void OffHook()
	{
		foreach (OverrideWrapper overrideWrapper in this.Overrides.Values)
		{
			overrideWrapper.Revert();
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x000282B4 File Offset: 0x000264B4
	public void LoadOverride(MethodInfo method)
	{
		try
		{
			OverrideAttribute attribute = (OverrideAttribute)Attribute.GetCustomAttribute(method, typeof(OverrideAttribute));
			if (this.Overrides.Count((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Key.Method == attribute.Method) <= 0)
			{
				OverrideWrapper overrideWrapper = new OverrideWrapper(attribute.Method, method, attribute, null);
				overrideWrapper.Override();
				this.Overrides.Add(attribute, overrideWrapper);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("0x0j0f234\n" + ex.Message + "\n" + method.Name);
		}
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00028364 File Offset: 0x00026564
	public void InitHook()
	{
		Type[] types = Assembly.GetExecutingAssembly().GetTypes();
		for (int i = 0; i < types.Length; i++)
		{
			foreach (MethodInfo methodInfo in types[i].GetMethods())
			{
				if (methodInfo.Name == "OV_GetKey" && methodInfo.IsDefined(typeof(OverrideAttribute), false))
				{
					this.LoadOverride(methodInfo);
				}
			}
		}
	}

	// Token: 0x04000047 RID: 71
	public static Dictionary<OverrideAttribute, OverrideWrapper> _overrides = new Dictionary<OverrideAttribute, OverrideWrapper>();
}
