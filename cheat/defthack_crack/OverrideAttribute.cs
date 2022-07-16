using System;
using System.Linq;
using System.Reflection;

// Token: 0x02000065 RID: 101
[AttributeUsage(AttributeTargets.Method)]
public class OverrideAttribute : Attribute
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060002F4 RID: 756 RVA: 0x00025AEF File Offset: 0x00023CEF
	// (set) Token: 0x060002F5 RID: 757 RVA: 0x00025AF7 File Offset: 0x00023CF7
	public Type Class { get; private set; }

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060002F6 RID: 758 RVA: 0x00025B00 File Offset: 0x00023D00
	// (set) Token: 0x060002F7 RID: 759 RVA: 0x00025B08 File Offset: 0x00023D08
	public string MethodName { get; private set; }

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060002F8 RID: 760 RVA: 0x00025B11 File Offset: 0x00023D11
	// (set) Token: 0x060002F9 RID: 761 RVA: 0x00025B19 File Offset: 0x00023D19
	public MethodInfo Method { get; private set; }

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x060002FA RID: 762 RVA: 0x00025B22 File Offset: 0x00023D22
	// (set) Token: 0x060002FB RID: 763 RVA: 0x00025B2A File Offset: 0x00023D2A
	public BindingFlags Flags { get; private set; }

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x060002FC RID: 764 RVA: 0x00025B33 File Offset: 0x00023D33
	// (set) Token: 0x060002FD RID: 765 RVA: 0x00025B3B File Offset: 0x00023D3B
	public bool MethodFound { get; private set; }

	// Token: 0x060002FE RID: 766 RVA: 0x00030F60 File Offset: 0x0002F160
	public OverrideAttribute(Type tClass, string method, BindingFlags flags, int index = 0)
	{
		this.Class = tClass;
		this.MethodName = method;
		this.Flags = flags;
		try
		{
			this.Method = (from a in this.Class.GetMethods(flags)
			where a.Name == method
			select a).ToArray<MethodInfo>()[index];
			this.MethodFound = true;
		}
		catch (Exception)
		{
			this.MethodFound = false;
		}
	}
}
