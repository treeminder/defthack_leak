using System;
using System.Threading;
using SDG.Unturned;

// Token: 0x020000A4 RID: 164
public static class SpammerThread
{
	// Token: 0x0600050A RID: 1290 RVA: 0x00026452 File Offset: 0x00024652
	[Thread]
	public static void Spammer()
	{
		for (;;)
		{
			Thread.Sleep(MiscOptions.SpammerDelay);
			if (MiscOptions.SpammerEnabled)
			{
				ChatManager.sendChat(EChatMode.GLOBAL, MiscOptions.SpamText);
			}
		}
	}
}
