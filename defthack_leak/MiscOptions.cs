using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

// Token: 0x02000057 RID: 87
public static class MiscOptions
{
	// Token: 0x0400015B RID: 347
	public static Vector3 pos;

	// Token: 0x0400015C RID: 348
	public static bool PanicMode = false;

	// Token: 0x0400015D RID: 349
	[Save]
	public static bool hang = false;

	// Token: 0x0400015E RID: 350
	[Save]
	public static bool PunchSilentAim = false;

	// Token: 0x0400015F RID: 351
	[Save]
	public static bool EnVehicle = false;

	// Token: 0x04000160 RID: 352
	[Save]
	public static bool BuildinObstacles = false;

	// Token: 0x04000161 RID: 353
	[Save]
	public static bool NoFlash = false;

	// Token: 0x04000162 RID: 354
	[Save]
	public static bool PunchAura = false;

	// Token: 0x04000163 RID: 355
	[Save]
	public static bool NoSnow = false;

	// Token: 0x04000164 RID: 356
	[Save]
	public static bool IsRussian = true;

	// Token: 0x04000165 RID: 357
	[Save]
	public static bool IsEnglish = false;

	// Token: 0x04000166 RID: 358
	[Save]
	public static bool NoRain = false;

	// Token: 0x04000167 RID: 359
	[Save]
	public static bool banbypass = false;

	// Token: 0x04000168 RID: 360
	[Save]
	public static bool NoFlinch = false;

	// Token: 0x04000169 RID: 361
	[Save]
	public static bool NoGrayscale = false;

	// Token: 0x0400016A RID: 362
	[Save]
	public static bool CustomSalvageTime = false;

	// Token: 0x0400016B RID: 363
	[Save]
	public static float SalvageTime = 1f;

	// Token: 0x0400016C RID: 364
	[Save]
	public static bool SetTimeEnabled = false;

	// Token: 0x0400016D RID: 365
	[Save]
	public static float Time = 0f;

	// Token: 0x0400016E RID: 366
	[Save]
	public static bool SlowFall = false;

	// Token: 0x0400016F RID: 367
	[Save]
	public static bool AirStick = false;

	// Token: 0x04000170 RID: 368
	[Save]
	public static bool Compass = false;

	// Token: 0x04000171 RID: 369
	[Save]
	public static bool GPS = false;

	// Token: 0x04000172 RID: 370
	[Save]
	public static bool Bones = false;

	// Token: 0x04000173 RID: 371
	[Save]
	public static bool ShowPlayersOnMap = false;

	// Token: 0x04000174 RID: 372
	[Save]
	public static bool NightVision = false;

	// Token: 0x04000175 RID: 373
	public static bool WasNightVision = false;

	// Token: 0x04000176 RID: 374
	[Save]
	public static string SpamText = "https://vk.com/defthack";

	// Token: 0x04000177 RID: 375
	[Save]
	public static string NickName = "";

	// Token: 0x04000178 RID: 376
	[Save]
	public static bool SpammerEnabled = false;

	// Token: 0x04000179 RID: 377
	[Save]
	public static int SpammerDelay = 0;

	// Token: 0x0400017A RID: 378
	[Save]
	public static bool VehicleFly = false;

	// Token: 0x0400017B RID: 379
	[Save]
	public static bool VehicleUseMaxSpeed = false;

	// Token: 0x0400017C RID: 380
	[Save]
	public static float SpeedMultiplier = 1f;

	// Token: 0x0400017D RID: 381
	[Save]
	public static bool ExtendMeleeRange = false;

	// Token: 0x0400017E RID: 382
	[Save]
	public static float MeleeRangeExtension = 7.5f;

	// Token: 0x0400017F RID: 383
	public static bool NoMovementVerification = false;

	// Token: 0x04000180 RID: 384
	[Save]
	public static bool AlwaysCheckMovementVerification = false;

	// Token: 0x04000181 RID: 385
	public static Player SpectatedPlayer;

	// Token: 0x04000182 RID: 386
	[Save]
	public static bool PlayerFlight = false;

	// Token: 0x04000183 RID: 387
	[Save]
	public static float FlightSpeedMultiplier = 1f;

	// Token: 0x04000184 RID: 388
	public static bool Freecam = false;

	// Token: 0x04000185 RID: 389
	[Save]
	public static HashSet<ulong> Friends = new HashSet<ulong>();

	// Token: 0x04000186 RID: 390
	[Save]
	public static int SCrashMethod = 1;

	// Token: 0x04000187 RID: 391
	[Save]
	public static int AntiSpyMethod = 0;

	// Token: 0x04000188 RID: 392
	[Save]
	public static string AntiSpyPath = "";

	// Token: 0x04000189 RID: 393
	[Save]
	public static bool AlertOnSpy = true;

	// Token: 0x0400018A RID: 394
	[Save]
	public static bool EnableDistanceCrash = false;

	// Token: 0x0400018B RID: 395
	[Save]
	public static float CrashDistance = 100f;

	// Token: 0x0400018C RID: 396
	[Save]
	public static bool CrashByName = false;

	// Token: 0x0400018D RID: 397
	[Save]
	public static List<string> CrashWords = new List<string>();

	// Token: 0x0400018E RID: 398
	[Save]
	public static List<string> CrashIDs = new List<string>();

	// Token: 0x0400018F RID: 399
	[Save]
	public static bool NearbyItemRaycast = false;

	// Token: 0x04000190 RID: 400
	[Save]
	public static bool IncreaseNearbyItemDistance = false;

	// Token: 0x04000191 RID: 401
	[Save]
	public static float NearbyItemDistance = 15f;

	// Token: 0x04000192 RID: 402
	public static bool epos;

	// Token: 0x04000193 RID: 403
	public static float Altitude;
}
