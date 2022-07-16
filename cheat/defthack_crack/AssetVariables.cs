using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000B RID: 11
public static class AssetVariables
{
	// Token: 0x04000021 RID: 33
	public static AssetBundle ABundle;

	// Token: 0x04000022 RID: 34
	public static Dictionary<string, Material> Materials = new Dictionary<string, Material>();

	// Token: 0x04000023 RID: 35
	public static Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

	// Token: 0x04000024 RID: 36
	public static Dictionary<string, AudioClip> Audio = new Dictionary<string, AudioClip>();

	// Token: 0x04000025 RID: 37
	public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

	// Token: 0x04000026 RID: 38
	public static Dictionary<string, Texture> Textures_ = new Dictionary<string, Texture>();

	// Token: 0x04000027 RID: 39
	public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();

	// Token: 0x04000028 RID: 40
	public static Dictionary<string, GUISkin> Skin = new Dictionary<string, GUISkin>();
}
