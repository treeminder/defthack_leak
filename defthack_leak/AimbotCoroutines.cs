using System;
using System.Collections;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000003 RID: 3
public static class AimbotCoroutines
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00024E8A File Offset: 0x0002308A
	// (set) Token: 0x06000005 RID: 5 RVA: 0x00024E9B File Offset: 0x0002309B
	public static float Pitch
	{
		get
		{
			return OptimizationVariables.MainPlayer.look.pitch;
		}
		set
		{
			AimbotCoroutines.PitchInfo.SetValue(OptimizationVariables.MainPlayer.look, value);
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000006 RID: 6 RVA: 0x00024EB7 File Offset: 0x000230B7
	// (set) Token: 0x06000007 RID: 7 RVA: 0x00024EC8 File Offset: 0x000230C8
	public static float Yaw
	{
		get
		{
			return OptimizationVariables.MainPlayer.look.yaw;
		}
		set
		{
			AimbotCoroutines.YawInfo.SetValue(OptimizationVariables.MainPlayer.look, value);
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00024EE4 File Offset: 0x000230E4
	[Initializer]
	public static void Init()
	{
		AimbotCoroutines.PitchInfo = typeof(PlayerLook).GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic);
		AimbotCoroutines.YawInfo = typeof(PlayerLook).GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00024F1C File Offset: 0x0002311C
	public static IEnumerator SetLockedObject()
	{
		{
			for (; ; )
			{
				if (!DrawUtilities.ShouldRun() || !AimbotOptions.Enabled)
				{
					yield return new WaitForSeconds(0.1f);
				}
				else
				{
					Player p = null;
					Vector3 aimPos = OptimizationVariables.MainPlayer.look.aim.position;
					Vector3 aimForward = OptimizationVariables.MainPlayer.look.aim.forward;
					SteamPlayer[] players = Provider.clients.ToArray();
					int num;
					for (int i = 0; i < players.Length; i = num + 1)
					{
						TargetMode targetMode = AimbotOptions.TargetMode;
						SteamPlayer cPlayer = players[i];
						if (cPlayer != null && !(cPlayer.player == OptimizationVariables.MainPlayer) && !(cPlayer.player.life == null) && !cPlayer.player.life.isDead && !FriendUtilities.IsFriendly(cPlayer.player))
						{
							if (targetMode > TargetMode.Distance)
							{
								if (targetMode == TargetMode.FOV && VectorUtilities.GetAngleDelta(aimPos, aimForward, players[i].player.transform.position) < (double)AimbotOptions.FOV)
								{
									if (p == null)
									{
										p = players[i].player;
									}
									else if (VectorUtilities.GetAngleDelta(aimPos, aimForward, players[i].player.transform.position) < VectorUtilities.GetAngleDelta(aimPos, aimForward, p.transform.position))
									{
										p = players[i].player;
									}
								}
							}
							else if ((double)AimbotOptions.Distance > VectorUtilities.GetDistance(players[i].player.transform.position))
							{
								p = players[i].player;
							}
						}
						num = i;
					}
					if (!AimbotCoroutines.IsAiming)
					{
						AimbotCoroutines.LockedObject = ((p != null) ? p.gameObject : null);
					}
					yield return new WaitForEndOfFrame();
					aimPos = default(Vector3);
					aimForward = default(Vector3);
					aimPos = default(Vector3);
					aimForward = default(Vector3);
				}
			}
			yield break;
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00024F24 File Offset: 0x00023124
	public static IEnumerator AimToObject()
	{
		for (; ; )
		{
			if (!DrawUtilities.ShouldRun() || !AimbotOptions.Enabled)
			{
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				if (AimbotCoroutines.LockedObject != null && AimbotCoroutines.LockedObject.transform != null && ESPComponent.MainCamera != null)
				{
					if (HotkeyUtilities.IsHotkeyHeld("_AimbotKey") || !AimbotOptions.OnKey)
					{
						AimbotCoroutines.IsAiming = true;
						if (AimbotOptions.Smooth)
						{
							AimbotCoroutines.SmoothAim(AimbotCoroutines.LockedObject);
						}
						else
						{
							AimbotCoroutines.Aim(AimbotCoroutines.LockedObject);
						}
					}
					else
					{
						AimbotCoroutines.IsAiming = false;
					}
				}
				else
				{
					AimbotCoroutines.IsAiming = false;
				}
				yield return new WaitForEndOfFrame();
			}
		}
		yield break;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000267BC File Offset: 0x000249BC
	public static void Aim(GameObject obj)
	{
		Camera mainCam = OptimizationVariables.MainCam;
		Vector3 aimPosition = AimbotCoroutines.GetAimPosition(obj.transform, "Skull");
		if (!(aimPosition == AimbotCoroutines.PiVector))
		{
			OptimizationVariables.MainPlayer.transform.LookAt(aimPosition);
			OptimizationVariables.MainPlayer.transform.eulerAngles = new Vector3(0f, OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y, 0f);
			mainCam.transform.LookAt(aimPosition);
			float num = mainCam.transform.localRotation.eulerAngles.x;
			if (num <= 90f && num <= 270f)
			{
				num = mainCam.transform.localRotation.eulerAngles.x + 90f;
			}
			else if (num >= 270f && num <= 360f)
			{
				num = mainCam.transform.localRotation.eulerAngles.x - 270f;
			}
			AimbotCoroutines.Pitch = num;
			AimbotCoroutines.Yaw = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000268E8 File Offset: 0x00024AE8
	public static void SmoothAim(GameObject obj)
	{
		Camera mainCam = OptimizationVariables.MainCam;
		Vector3 aimPosition = AimbotCoroutines.GetAimPosition(obj.transform, "Skull");
		if (!(aimPosition == AimbotCoroutines.PiVector))
		{
			OptimizationVariables.MainPlayer.transform.rotation = Quaternion.Slerp(OptimizationVariables.MainPlayer.transform.rotation, Quaternion.LookRotation(aimPosition - OptimizationVariables.MainPlayer.transform.position), Time.deltaTime * AimbotOptions.AimSpeed);
			OptimizationVariables.MainPlayer.transform.eulerAngles = new Vector3(0f, OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y, 0f);
			mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, Quaternion.LookRotation(aimPosition - mainCam.transform.position), Time.deltaTime * AimbotOptions.AimSpeed);
			float num = mainCam.transform.localRotation.eulerAngles.x;
			if (num <= 90f && num <= 270f)
			{
				num = mainCam.transform.localRotation.eulerAngles.x + 90f;
			}
			else if (num >= 270f && num <= 360f)
			{
				num = mainCam.transform.localRotation.eulerAngles.x - 270f;
			}
			AimbotCoroutines.Pitch = num;
			AimbotCoroutines.Yaw = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00024F2C File Offset: 0x0002312C
	public static Vector2 CalcAngle(GameObject obj)
	{
		ESPComponent.MainCamera.WorldToScreenPoint(AimbotCoroutines.GetAimPosition(obj.transform, "Skull"));
		return Vector2.zero;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00024F4E File Offset: 0x0002314E
	public static void AimMouseTo(float x, float y)
	{
		AimbotCoroutines.Yaw = x;
		AimbotCoroutines.Pitch = y;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00026A7C File Offset: 0x00024C7C
	public static Vector3 GetAimPosition(Transform parent, string name)
	{
		Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
		Vector3 piVector;
		if (componentsInChildren == null)
		{
			piVector = AimbotCoroutines.PiVector;
		}
		else
		{
			foreach (Transform transform in componentsInChildren)
			{
				if (transform.name.Trim() == name)
				{
					return transform.position + new Vector3(0f, 0.4f, 0f);
				}
			}
			piVector = AimbotCoroutines.PiVector;
		}
		return piVector;
	}

	// Token: 0x04000001 RID: 1
	public static Vector3 PiVector = new Vector3(0f, 3.1415927f, 0f);

	// Token: 0x04000002 RID: 2
	public static GameObject LockedObject;

	// Token: 0x04000003 RID: 3
	public static bool IsAiming = false;

	// Token: 0x04000004 RID: 4
	public static FieldInfo PitchInfo;

	// Token: 0x04000005 RID: 5
	public static FieldInfo YawInfo;
}
