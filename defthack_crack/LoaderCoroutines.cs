using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

// Token: 0x02000044 RID: 68
public static class LoaderCoroutines
{

	public static string AssetPath = Application.dataPath + "/thanking.assets";

	public static IEnumerator LoadAssets()
		{
			yield return new WaitForSeconds(1f);
			byte[] Loader = File.ReadAllBytes(LoaderCoroutines.AssetPath);
			Console.WriteLine(LoaderCoroutines.AssetPath);
			if (File.Exists(LoaderCoroutines.AssetPath))
			{
				AssetBundle bundle = AssetBundle.LoadFromMemory(Loader);
				AssetVariables.ABundle = bundle;
				foreach (Shader s in bundle.LoadAllAssets<Shader>())
				{
					AssetVariables.Materials.Add(s.name, new Material(s)
					{
						hideFlags = HideFlags.HideAndDontSave
					});
				}
				foreach (Shader s2 in bundle.LoadAllAssets<Shader>())
				{
					AssetVariables.Shaders.Add(s2.name, s2);
				}
				foreach (Font f in bundle.LoadAllAssets<Font>())
				{
					AssetVariables.Fonts.Add(f.name, f);
				}
				foreach (AudioClip ac in bundle.LoadAllAssets<AudioClip>())
				{
					AssetVariables.Audio.Add(ac.name, ac);
				}
			foreach (Texture texture in bundle.LoadAllAssets<Texture>())
			{
				if (!(texture.name == "cursour"))
				{
					if (texture.name == "rus" || texture.name == "usa")
					{
						AssetVariables.Textures_.Add(texture.name, texture);
					}
				}
				else
				{
					AssetVariables.Textures_.Add(texture.name, texture);
				}
			}
			MenuComponent.rus = AssetVariables.Textures["rus"];
			MenuComponent.usa = AssetVariables.Textures["usa"];
			MenuComponent._cursorTexture = AssetVariables.Textures["cursour"];
			MenuComponent.skin = AssetVariables.Skin["DeftSkin"];
			ESPComponent.GLMat = AssetVariables.Materials["ESP"];
			ESPComponent.ESPFont = AssetVariables.Fonts["Roboto-Light"];
			ESPCoroutines.Normal = Shader.Find("Standard/Clothes");
			ESPCoroutines.LitChams = AssetVariables.Shaders["chamsLit"];
			ESPCoroutines.UnlitChams = AssetVariables.Shaders["chamsUnlit"];
			LoaderCoroutines.IsLoaded = true;
			ConfigManager.Init();
		}
			else
			{
				yield return null;
			}
			yield break;
		}

	// Token: 0x04000108 RID: 264
	public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();

	// Token: 0x04000109 RID: 265
	public static bool IsLoaded;
}
