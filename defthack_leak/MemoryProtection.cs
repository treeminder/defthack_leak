using System;

// Token: 0x0200004B RID: 75
[Flags]
public enum MemoryProtection : uint
{
	// Token: 0x04000115 RID: 277
	EXECUTE = 16U,
	// Token: 0x04000116 RID: 278
	EXECUTE_READ = 32U,
	// Token: 0x04000117 RID: 279
	EXECUTE_READWRITE = 64U,
	// Token: 0x04000118 RID: 280
	EXECUTE_WRITECOPY = 128U,
	// Token: 0x04000119 RID: 281
	NOACCESS = 1U,
	// Token: 0x0400011A RID: 282
	READONLY = 2U,
	// Token: 0x0400011B RID: 283
	READWRITE = 4U,
	// Token: 0x0400011C RID: 284
	WRITECOPY = 8U,
	// Token: 0x0400011D RID: 285
	GUARD_Modifierflag = 256U,
	// Token: 0x0400011E RID: 286
	NOCACHE_Modifierflag = 512U,
	// Token: 0x0400011F RID: 287
	WRITECOMBINE_Modifierflag = 1024U
}
