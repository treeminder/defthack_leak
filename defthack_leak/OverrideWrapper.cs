using System;
using System.Reflection;
using System.Runtime.CompilerServices;

// Token: 0x0200006E RID: 110
public class OverrideWrapper
{
	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000323 RID: 803 RVA: 0x00025BED File Offset: 0x00023DED
	// (set) Token: 0x06000324 RID: 804 RVA: 0x00025BF5 File Offset: 0x00023DF5
	public MethodInfo Original { get; set; }

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000325 RID: 805 RVA: 0x00025BFE File Offset: 0x00023DFE
	// (set) Token: 0x06000326 RID: 806 RVA: 0x00025C06 File Offset: 0x00023E06
	public MethodInfo Modified { get; set; }

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000327 RID: 807 RVA: 0x00025C0F File Offset: 0x00023E0F
	// (set) Token: 0x06000328 RID: 808 RVA: 0x00025C17 File Offset: 0x00023E17
	public IntPtr PtrOriginal { get; private set; }

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000329 RID: 809 RVA: 0x00025C20 File Offset: 0x00023E20
	// (set) Token: 0x0600032A RID: 810 RVA: 0x00025C28 File Offset: 0x00023E28
	public IntPtr PtrModified { get; private set; }

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600032B RID: 811 RVA: 0x00025C31 File Offset: 0x00023E31
	// (set) Token: 0x0600032C RID: 812 RVA: 0x00025C39 File Offset: 0x00023E39
	public OverrideUtilities.OffsetBackup OffsetBackup { get; private set; }

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600032D RID: 813 RVA: 0x00025C42 File Offset: 0x00023E42
	// (set) Token: 0x0600032E RID: 814 RVA: 0x00025C4A File Offset: 0x00023E4A
	public OverrideAttribute Attribute { get; set; }

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600032F RID: 815 RVA: 0x00025C53 File Offset: 0x00023E53
	// (set) Token: 0x06000330 RID: 816 RVA: 0x00025C5B File Offset: 0x00023E5B
	public bool Detoured { get; private set; }

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000331 RID: 817 RVA: 0x00025C64 File Offset: 0x00023E64
	public object Instance { get; }

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000332 RID: 818 RVA: 0x00025C6C File Offset: 0x00023E6C
	// (set) Token: 0x06000333 RID: 819 RVA: 0x00025C74 File Offset: 0x00023E74
	public bool Local { get; private set; }

	// Token: 0x06000334 RID: 820 RVA: 0x0003137C File Offset: 0x0002F57C
	public OverrideWrapper(MethodInfo original, MethodInfo modified, OverrideAttribute attribute, object instance = null)
	{
		this.Original = original;
		this.Modified = modified;
		this.Instance = instance;
		this.Attribute = attribute;
		this.Local = (this.Modified.DeclaringType.Assembly == Assembly.GetExecutingAssembly());
		RuntimeHelpers.PrepareMethod(original.MethodHandle);
		RuntimeHelpers.PrepareMethod(modified.MethodHandle);
		this.PtrOriginal = this.Original.MethodHandle.GetFunctionPointer();
		this.PtrModified = this.Modified.MethodHandle.GetFunctionPointer();
		this.OffsetBackup = new OverrideUtilities.OffsetBackup(this.PtrOriginal);
		this.Detoured = false;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0003142C File Offset: 0x0002F62C
	public bool Override()
	{
		bool result;
		if (this.Detoured)
		{
			result = true;
		}
		else
		{
			bool flag = OverrideUtilities.OverrideFunction(this.PtrOriginal, this.PtrModified);
			if (flag)
			{
				this.Detoured = true;
			}
			result = flag;
		}
		return result;
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00031464 File Offset: 0x0002F664
	public bool Revert()
	{
		bool result;
		if (!this.Detoured)
		{
			result = false;
		}
		else
		{
			bool flag = OverrideUtilities.RevertOverride(this.OffsetBackup);
			if (flag)
			{
				this.Detoured = false;
			}
			result = flag;
		}
		return result;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00025C7D File Offset: 0x00023E7D
	public object CallOriginal(object[] args, object instance = null)
	{
		this.Revert();
		object result = this.Original.Invoke(instance ?? this.Instance, args);
		this.Override();
		return result;
	}
}
