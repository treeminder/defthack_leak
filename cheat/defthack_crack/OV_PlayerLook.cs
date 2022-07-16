using System;
using System.Reflection;
using SDG.Unturned;

// Token: 0x0200007C RID: 124
public class OV_PlayerLook
{
	// Token: 0x060003A9 RID: 937 RVA: 0x00025F53 File Offset: 0x00024153
	[Override(typeof(PlayerLook), "onDamaged", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
	public static void OV_onDamaged(byte damage)
	{
		if (!MiscOptions.NoFlinch)
		{
			OverrideUtilities.CallOriginal(null, new object[]
			{
				damage
			});
		}
	}
}
