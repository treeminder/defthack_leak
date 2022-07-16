using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class OV_PlayerInput
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000368 RID: 872 RVA: 0x00025DC6 File Offset: 0x00023FC6
	public static List<PlayerInputPacket> ClientsidePackets
	{
		get
		{
			if (DrawUtilities.ShouldRun() && OV_PlayerInput.Run)
			{
				return (List<PlayerInputPacket>)OV_PlayerInput.ClientsidePacketsField.GetValue(OptimizationVariables.MainPlayer.input);
			}
			return null;
		}
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00031744 File Offset: 0x0002F944
	public static void OV_askAck(PlayerInput instance, CSteamID steamId, int ack)
	{
		if (!(steamId != Provider.server))
		{
			for (int i = OV_PlayerInput.Packets.Count - 1; i >= 0; i--)
			{
				if ((ulong)OV_PlayerInput.Packets[i].clientSimulationFrameNumber <= (ulong)((long)ack))
				{
					OV_PlayerInput.Packets.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0003179C File Offset: 0x0002F99C
	public static void OV_FixedUpdate()
	{
		Player mainPlayer = OptimizationVariables.MainPlayer;
		if (MiscOptions.PunchSilentAim)
		{
			OV_DamageTool.OVType = OverrideType.PlayerHit;
		}
		DamageTool.raycast(new Ray(mainPlayer.look.aim.position, mainPlayer.look.aim.forward), 6f, RayMasks.DAMAGE_SERVER);
		OverrideUtilities.CallOriginal(null, new object[0]);
		List<PlayerInputPacket> clientsidePackets = OV_PlayerInput.ClientsidePackets;
		OV_PlayerInput.LastPacket = ((clientsidePackets != null) ? clientsidePackets.Last<PlayerInputPacket>() : null);
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00031818 File Offset: 0x0002FA18
	[Override(typeof(PlayerInput), "InitializePlayer", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
	public static void OV_InitializePlayer(PlayerInput instance)
	{
		if (instance.player != Player.player)
		{
			OverrideUtilities.CallOriginal(instance, new object[0]);
			return;
		}
		OptimizationVariables.MainPlayer = Player.player;
		OV_PlayerInput.Rate = 4;
		OV_PlayerInput.Count = 0;
		OV_PlayerInput.Buffer = 0;
		OV_PlayerInput.Packets.Clear();
		OV_PlayerInput.LastPacket = null;
		OV_PlayerInput.SequenceDiff = 0;
		OV_PlayerInput.ClientSequence = 0;
		OverrideUtilities.CallOriginal(instance, new object[0]);
	}

	// Token: 0x040001E1 RID: 481
	public static FieldInfo ClientsidePacketsField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x040001E2 RID: 482
	public static PlayerInputPacket LastPacket;

	// Token: 0x040001E3 RID: 483
	public static float Yaw;

	// Token: 0x040001E4 RID: 484
	public static float Pitch;

	// Token: 0x040001E5 RID: 485
	public static int Count;

	// Token: 0x040001E6 RID: 486
	public static int Buffer;

	// Token: 0x040001E7 RID: 487
	public static int Choked;

	// Token: 0x040001E8 RID: 488
	public static uint Clock = 1U;

	// Token: 0x040001E9 RID: 489
	public static int Rate;

	// Token: 0x040001EA RID: 490
	public static int ClientSequence = 1;

	// Token: 0x040001EB RID: 491
	public static int SequenceDiff;

	// Token: 0x040001EC RID: 492
	public static List<PlayerInputPacket> Packets = new List<PlayerInputPacket>();

	// Token: 0x040001ED RID: 493
	public static Queue<PlayerInputPacket> WaitingPackets = new Queue<PlayerInputPacket>();

	// Token: 0x040001EE RID: 494
	public static float LastReal;

	// Token: 0x040001EF RID: 495
	public static bool Run;

	// Token: 0x040001F0 RID: 496
	public static FieldInfo SimField = typeof(PlayerInput).GetField("_simulation", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x040001F1 RID: 497
	public static Vector3 lastSentPositon = Vector3.zero;
}
