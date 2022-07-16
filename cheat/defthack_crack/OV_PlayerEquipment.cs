using System;
using System.Reflection;
using SDG.Unturned;

// Token: 0x02000078 RID: 120
public class OV_PlayerEquipment
{
	// Token: 0x06000365 RID: 869 RVA: 0x00025D92 File Offset: 0x00023F92
	[Override(typeof(PlayerEquipment), "punch", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
	public void OV_punch(EPlayerPunch p)
	{
		if (MiscOptions.PunchSilentAim)
		{
			OV_DamageTool.OVType = OverrideType.PlayerHit;
		}
		OverrideUtilities.CallOriginal(OptimizationVariables.MainPlayer.equipment, new object[]
		{
			p
		});
		OV_DamageTool.OVType = OverrideType.None;
	}

	// Token: 0x040001DF RID: 479
	public static bool WasPunching;

	// Token: 0x040001E0 RID: 480
	public static uint CurrSim;
}
