using System;
using System.Reflection;
using SDG.Unturned;

// Token: 0x0200007E RID: 126
public class OV_PlayerUI
{
	// Token: 0x060003B0 RID: 944 RVA: 0x000323D4 File Offset: 0x000305D4
	[Override(typeof(PlayerUI), "updateCrosshair", BindingFlags.Static | BindingFlags.Public, 0)]
	public static void OV_updateCrosshair(float spread)
	{
		if (Provider.modeConfigData.Gameplay.Crosshair)
		{
			PlayerLifeUI.crosshairLeftImage.positionOffset_X = (int)(-spread * 400f) - 4;
			PlayerLifeUI.crosshairLeftImage.positionOffset_Y = -4;
			PlayerLifeUI.crosshairRightImage.positionOffset_X = (int)(spread * 400f) - 4;
			PlayerLifeUI.crosshairRightImage.positionOffset_Y = -4;
			PlayerLifeUI.crosshairUpImage.positionOffset_X = -4;
			PlayerLifeUI.crosshairUpImage.positionOffset_Y = (int)(-spread * 400f) - 4;
			PlayerLifeUI.crosshairDownImage.positionOffset_X = -4;
			PlayerLifeUI.crosshairDownImage.positionOffset_Y = (int)(spread * 400f) - 4;
		}
	}
}
