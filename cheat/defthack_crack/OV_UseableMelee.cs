using System;
using System.Reflection;
using SDG.Unturned;

// Token: 0x02000082 RID: 130
public class OV_UseableMelee
{
	// Token: 0x060003EA RID: 1002 RVA: 0x00032D18 File Offset: 0x00030F18
	[Override(typeof(UseableMelee), "fire", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
	public static void OV_fire()
	{
		OV_DamageTool.OVType = OverrideType.None;
		if (RaycastOptions.Enabled && MiscOptions.ExtendMeleeRange)
		{
			OV_DamageTool.OVType = OverrideType.SilentAimMelee;
		}
		else if (!RaycastOptions.Enabled)
		{
			if (MiscOptions.ExtendMeleeRange)
			{
				OV_DamageTool.OVType = OverrideType.Extended;
			}
		}
		else
		{
			OV_DamageTool.OVType = OverrideType.SilentAim;
		}
		OverrideUtilities.CallOriginal(OptimizationVariables.MainPlayer.equipment.useable, new object[0]);
		OV_DamageTool.OVType = OverrideType.None;
	}
}
