using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Token: 0x02000017 RID: 23
public class ConfigManager
{
	// Token: 0x0600007C RID: 124 RVA: 0x00025168 File Offset: 0x00023368
	public static void Init()
	{
		ConfigManager.LoadConfig(ConfigManager.GetConfig());
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00027E3C File Offset: 0x0002603C
	public static Dictionary<string, object> CollectConfig()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>
		{
			{
				"Version",
				ConfigManager.ConfigVersion
			}
		};
		foreach (Type type in (from T in Assembly.GetExecutingAssembly().GetTypes()
		where T.IsClass
		select T).ToArray<Type>())
		{
			foreach (FieldInfo fieldInfo in (from F in type.GetFields()
			where F.IsDefined(typeof(SaveAttribute), false)
			select F).ToArray<FieldInfo>())
			{
				dictionary.Add(type.Name + "_" + fieldInfo.Name, fieldInfo.GetValue(null));
			}
		}
		return dictionary;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00027F18 File Offset: 0x00026118
	public static Dictionary<string, object> GetConfig()
	{
		if (!File.Exists(ConfigManager.ConfigPath))
		{
			ConfigManager.SaveConfig(ConfigManager.CollectConfig());
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		try
		{
			dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(ConfigManager.ConfigPath), new JsonSerializerSettings
			{
				Formatting = Formatting.Indented
			});
		}
		catch
		{
			dictionary = ConfigManager.CollectConfig();
			ConfigManager.SaveConfig(dictionary);
		}
		return dictionary;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00025174 File Offset: 0x00023374
	public static void SaveConfig(Dictionary<string, object> Config)
	{
		File.WriteAllText(ConfigManager.ConfigPath, JsonConvert.SerializeObject(Config, Formatting.Indented));
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00027F84 File Offset: 0x00026184
	public static void LoadConfig(Dictionary<string, object> Config)
	{
		foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
		{
			foreach (FieldInfo fieldInfo in from f in type.GetFields()
			where Attribute.IsDefined(f, typeof(SaveAttribute))
			select f)
			{
				string key = type.Name + "_" + fieldInfo.Name;
				Type fieldType = fieldInfo.FieldType;
				object value = fieldInfo.GetValue(null);
				if (!Config.ContainsKey(key))
				{
					Config.Add(key, value);
				}
				try
				{
					if (Config[key].GetType() == typeof(JArray))
					{
						Config[key] = ((JArray)Config[key]).ToObject(fieldInfo.FieldType);
					}
					if (Config[key].GetType() == typeof(JObject))
					{
						Config[key] = ((JObject)Config[key]).ToObject(fieldInfo.FieldType);
					}
					fieldInfo.SetValue(null, fieldInfo.FieldType.IsEnum ? Enum.ToObject(fieldInfo.FieldType, Config[key]) : Convert.ChangeType(Config[key], fieldInfo.FieldType));
				}
				catch
				{
					Config[key] = value;
				}
			}
		}
		foreach (KeyValuePair<string, ColorVariable> keyValuePair in ColorOptions.DefaultColorDict)
		{
			if (!ColorOptions.ColorDict.ContainsKey(keyValuePair.Key))
			{
				ColorOptions.ColorDict.Add(keyValuePair.Key, new ColorVariable(keyValuePair.Value));
			}
		}
		using (List<KeyValuePair<string, Hotkey>>.Enumerator enumerator3 = HotkeyOptions.UnorganizedHotkeys.ToList<KeyValuePair<string, Hotkey>>().GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				KeyValuePair<string, Hotkey> str = enumerator3.Current;
				if (HotkeyOptions.HotkeyDict.All((KeyValuePair<string, Dictionary<string, Hotkey>> kvp) => !kvp.Value.ContainsKey(str.Key)))
				{
					HotkeyOptions.UnorganizedHotkeys.Remove(str.Key);
				}
			}
		}
		ConfigManager.SaveConfig(Config);
	}

	// Token: 0x04000040 RID: 64
	public static string ConfigPath = Environment.CurrentDirectory + "\\Localization\\English\\Server\\ServerCommandAnimals.dat";

	// Token: 0x04000041 RID: 65
	public static string ConfigVersion = "1.0.3";
}
