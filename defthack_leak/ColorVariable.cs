using System;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class ColorVariable
{
	// Token: 0x06000066 RID: 102 RVA: 0x000250BD File Offset: 0x000232BD
	[JsonConstructor]
	public ColorVariable(string identity, string name, Color32 color, Color32 origColor, bool disableAlpha)
	{
		this.identity = identity;
		this.name = name;
		this.color = color;
		this.origColor = origColor;
		this.disableAlpha = disableAlpha;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000250F4 File Offset: 0x000232F4
	public ColorVariable(string identity, string name, Color32 color, bool disableAlpha = true)
	{
		this.identity = identity;
		this.name = name;
		this.color = color;
		this.origColor = color;
		this.disableAlpha = disableAlpha;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00027C1C File Offset: 0x00025E1C
	public ColorVariable(ColorVariable option)
	{
		this.identity = option.identity;
		this.name = option.name;
		this.color = option.color;
		this.origColor = option.origColor;
		this.disableAlpha = option.disableAlpha;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x0002512A File Offset: 0x0002332A
	public static implicit operator Color(ColorVariable color)
	{
		return color.color.ToColor();
	}

	// Token: 0x0600006A RID: 106 RVA: 0x0002513C File Offset: 0x0002333C
	public static implicit operator Color32(ColorVariable color)
	{
		return color.color;
	}

	// Token: 0x04000035 RID: 53
	public SerializableColor color;

	// Token: 0x04000036 RID: 54
	public SerializableColor origColor;

	// Token: 0x04000037 RID: 55
	public string name;

	// Token: 0x04000038 RID: 56
	public string identity;

	// Token: 0x04000039 RID: 57
	public bool disableAlpha;
}
