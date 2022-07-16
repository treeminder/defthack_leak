using System;
using System.Reflection;
using SDG.Unturned;

// Token: 0x0200007F RID: 127
public static class OV_Provider
{
	// Token: 0x060003B5 RID: 949 RVA: 0x00025F7E File Offset: 0x0002417E
	[Override(typeof(Provider), "legacyReceiveClient", BindingFlags.Static | BindingFlags.NonPublic, 0)]
	public static void OV_legacyReceiveClient(byte[] packet, int offset, int size)
	{
		if (!OV_Provider.IsConnected)
		{
			OV_Provider.IsConnected = true;
		}
	}

	// Token: 0x040001FC RID: 508
	public static bool IsConnected;
}
