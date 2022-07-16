using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000071 RID: 113
public static class OV_Input
{
	// Token: 0x06000346 RID: 838 RVA: 0x00025CDA File Offset: 0x00023EDA
	[OnSpy]
	public static void OnSpied()
	{
		OV_Input.InputEnabled = false;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00025CE2 File Offset: 0x00023EE2
	[OffSpy]
	public static void OnEndSpy()
	{
		OV_Input.InputEnabled = true;
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0003155C File Offset: 0x0002F75C
	[Override(typeof(Input), "GetKey", BindingFlags.Static | BindingFlags.Public, 0)]
	public static bool OV_GetKey(KeyCode key)
	{
		bool result;
		if (DrawUtilities.ShouldRun() && OV_Input.InputEnabled)
		{
			bool flag;
			if (key == ControlsSettings.primary)
			{
				if (TriggerbotOptions.IsFiring)
				{
					flag = true;
					goto IL_74;
				}
			}
			if (key != ControlsSettings.left)
			{
				if (key != ControlsSettings.right)
				{
					if (key != ControlsSettings.up)
					{
						if (key != ControlsSettings.down)
						{
							goto IL_54;
						}
					}
				}
			}
			if (MiscOptions.SpectatedPlayer != null)
			{
				flag = false;
				goto IL_74;
			}
			IL_54:
			flag = (bool)OverrideUtilities.CallOriginal(null, new object[]
			{
				key
			});
			IL_74:
			result = flag;
		}
		else
		{
			result = (bool)OverrideUtilities.CallOriginal(null, new object[]
			{
				key
			});
		}
		return result;
	}

	// Token: 0x040001D6 RID: 470
	public static bool InputEnabled = true;
}
