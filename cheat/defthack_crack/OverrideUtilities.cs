using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

// Token: 0x02000068 RID: 104
public static class OverrideUtilities
{
	// Token: 0x06000304 RID: 772 RVA: 0x00030FE8 File Offset: 0x0002F1E8
	public static object CallOriginalFunc(MethodInfo method, object instance = null, params object[] args)
	{
		OverrideManager overrideManager = new OverrideManager();
		if (overrideManager.Overrides.All((KeyValuePair<OverrideAttribute, OverrideWrapper> o) => o.Value.Original != method))
		{
			throw new Exception("The Override specified was not found!");
		}
		return overrideManager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value.CallOriginal(args, instance);
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00031050 File Offset: 0x0002F250
	public static object CallOriginal(object instance = null, params object[] args)
	{
		OverrideManager overrideManager = new OverrideManager();
		StackTrace stackTrace = new StackTrace(false);
		if (stackTrace.FrameCount < 1)
		{
			throw new Exception("Invalid trace back to the original method! Please provide the methodinfo instead!");
		}
		MethodBase method = stackTrace.GetFrame(1).GetMethod();
		MethodInfo original = null;
		if (!Attribute.IsDefined(method, typeof(OverrideAttribute)))
		{
			method = stackTrace.GetFrame(2).GetMethod();
		}
		OverrideAttribute overrideAttribute = (OverrideAttribute)Attribute.GetCustomAttribute(method, typeof(OverrideAttribute));
		if (overrideAttribute == null)
		{
			throw new Exception("This method can only be called from an overwritten method!");
		}
		if (!overrideAttribute.MethodFound)
		{
			throw new Exception("The original method was never found!");
		}
		original = overrideAttribute.Method;
		if (overrideManager.Overrides.All((KeyValuePair<OverrideAttribute, OverrideWrapper> o) => o.Value.Original != original))
		{
			throw new Exception("The Override specified was not found!");
		}
		return overrideManager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == original).Value.CallOriginal(args, instance);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00031144 File Offset: 0x0002F344
	public static bool EnableOverride(MethodInfo method)
	{
		OverrideWrapper value = new OverrideManager().Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
		return value != null && value.Override();
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00031190 File Offset: 0x0002F390
	public static bool DisableOverride(MethodInfo method)
	{
		OverrideWrapper value = new OverrideManager().Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
		return value != null && value.Revert();
	}

	// Token: 0x06000308 RID: 776 RVA: 0x000311DC File Offset: 0x0002F3DC
	public unsafe static bool OverrideFunction(IntPtr ptrOriginal, IntPtr ptrModified)
	{
		bool result;
		try
		{
			int size = IntPtr.Size;
			if (size != 4)
			{
				if (size != 8)
				{
					return false;
				}
				byte* ptr = (byte*)ptrOriginal.ToPointer();
				*ptr = 72;
				ptr[1] = 184;
				*(long*)(ptr + 2) = ptrModified.ToInt64();
				ptr[10] = byte.MaxValue;
				ptr[11] = 224;
			}
			else
			{
				byte* ptr2 = (byte*)ptrOriginal.ToPointer();
				*ptr2 = 104;
				*(int*)(ptr2 + 1) = ptrModified.ToInt32();
				ptr2[5] = 195;
			}
			result = true;
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00031270 File Offset: 0x0002F470
	public unsafe static bool RevertOverride(OverrideUtilities.OffsetBackup backup)
	{
		bool result;
		try
		{
			byte* ptr = (byte*)backup.Method.ToPointer();
			*ptr = backup.A;
			ptr[1] = backup.B;
			ptr[10] = backup.C;
			ptr[11] = backup.D;
			ptr[12] = backup.E;
			if (IntPtr.Size != 4)
			{
				*(long*)(ptr + 2) = (long)backup.F64;
			}
			else
			{
				*(int*)(ptr + 1) = (int)backup.F32;
				ptr[5] = backup.G;
			}
			result = true;
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x02000069 RID: 105
	public class OffsetBackup
	{
		// Token: 0x06000312 RID: 786 RVA: 0x00031300 File Offset: 0x0002F500
		public unsafe OffsetBackup(IntPtr method)
		{
			this.Method = method;
			byte* ptr = (byte*)method.ToPointer();
			this.A = *ptr;
			this.B = ptr[1];
			this.C = ptr[10];
			this.D = ptr[11];
			this.E = ptr[12];
			if (IntPtr.Size == 4)
			{
				this.F32 = *(uint*)(ptr + 1);
				this.G = ptr[5];
				return;
			}
			this.F64 = (ulong)(*(long*)(ptr + 2));
		}

		// Token: 0x040001BF RID: 447
		public IntPtr Method;

		// Token: 0x040001C0 RID: 448
		public byte A;

		// Token: 0x040001C1 RID: 449
		public byte B;

		// Token: 0x040001C2 RID: 450
		public byte C;

		// Token: 0x040001C3 RID: 451
		public byte D;

		// Token: 0x040001C4 RID: 452
		public byte E;

		// Token: 0x040001C5 RID: 453
		public byte G;

		// Token: 0x040001C6 RID: 454
		public ulong F64;

		// Token: 0x040001C7 RID: 455
		public uint F32;
	}
}
