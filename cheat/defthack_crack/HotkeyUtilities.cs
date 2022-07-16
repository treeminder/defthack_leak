using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000037 RID: 55
public static class HotkeyUtilities
{
	// Token: 0x060001AD RID: 429 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
	[Initializer]
	public static void Initialize()
	{
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "АИМБОТ" : "AIMBOT", MenuComponent._isRus ? "Аимбот On/Off" : "Aimbot On/Off", "_ToggleAimbot", new KeyCode[]
		{
			KeyCode.Keypad3
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "АИМБОТ" : "AIMBOT", MenuComponent._isRus ? "Аимбот по кнопке On/Off" : "Aimbot by button On/Off", "_AimbotOnKey", new KeyCode[]
		{
			KeyCode.Keypad4
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "АИМБОТ" : "AIMBOT", MenuComponent._isRus ? "Кнопка Аимбота" : "Aimbot button", "_AimbotKey", new KeyCode[]
		{
			KeyCode.F
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "ОРУЖИЕ" : "WEAPON", MenuComponent._isRus ? "Триггербот On/Off" : "Triggerbot On/Off", "_ToggleTriggerbot", new KeyCode[]
		{
			KeyCode.Keypad5
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "ОРУЖИЕ" : "WEAPON", MenuComponent._isRus ? "Без отдачи On/Off" : "No recoil On/Off", "_ToggleNoRecoil", new KeyCode[]
		{
			KeyCode.Keypad6
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "ОРУЖИЕ" : "WEAPON", MenuComponent._isRus ? "Без разброса On/Off" : "No scatter On/Off", "_ToggleNoSpread", new KeyCode[]
		{
			KeyCode.Keypad7
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "ОРУЖИЕ" : "WEAPON", MenuComponent._isRus ? "Без увода On/Off" : "Without withdrawal On/Off", "_ToggleNoSway", new KeyCode[]
		{
			KeyCode.Keypad8
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Полёт машины On/Off" : "Flight of the car On/Off", "_VFToggle", new KeyCode[]
		{
			KeyCode.Slash
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Вверх" : "Up", "_VFStrafeUp", new KeyCode[]
		{
			KeyCode.RightControl
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Вниз" : "Way down", "_VFStrafeDown", new KeyCode[]
		{
			KeyCode.LeftControl
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Влево" : "To the left", "_VFStrafeLeft", new KeyCode[]
		{
			KeyCode.LeftBracket
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Вправо" : "To the right", "_VFStrafeRight", new KeyCode[]
		{
			KeyCode.RightBracket
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Движение вперёд" : "Forward movement", "_VFMoveForward", new KeyCode[]
		{
			KeyCode.W
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Движение назад" : "Backward movement", "_VFMoveBackward", new KeyCode[]
		{
			KeyCode.S
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Поворот налево" : "Left turn", "_VFRotateLeft", new KeyCode[]
		{
			KeyCode.A
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Поворот направо" : "Right turn", "_VFRotateRight", new KeyCode[]
		{
			KeyCode.D
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Поворот вверх" : "Turn up", "_VFRotateUp", new KeyCode[]
		{
			KeyCode.Space
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Поворот вниз" : "Turn down", "_VFRotateDown", new KeyCode[]
		{
			KeyCode.LeftShift
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Вращаться влево" : "Rotate left", "_VFRollLeft", new KeyCode[]
		{
			KeyCode.Q
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт Машины" : "Flight Vehicle", MenuComponent._isRus ? "Вращаться вправо" : "Rotate right", "_VFRollRight", new KeyCode[]
		{
			KeyCode.E
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт игрока" : "Flight Vehicle", MenuComponent._isRus ? "Вверх" : "Up", "_FlyUp", new KeyCode[]
		{
			KeyCode.Space
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт игрока" : "Flight Vehicle", MenuComponent._isRus ? "Вниз" : "Way down", "_FlyDown", new KeyCode[]
		{
			KeyCode.LeftControl
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт игрока" : "Flight Vehicle", MenuComponent._isRus ? "Влево" : "To the left", "_FlyLeft", new KeyCode[]
		{
			KeyCode.A
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт игрока" : "Flight Vehicle", MenuComponent._isRus ? "Вправо" : "To the right", "_FlyRight", new KeyCode[]
		{
			KeyCode.D
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт игрока" : "Flight Vehicle", MenuComponent._isRus ? "Движение вперёд" : "Forward movement", "_FlyForward", new KeyCode[]
		{
			KeyCode.W
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Полёт игрока" : "Flight Vehicle", MenuComponent._isRus ? "Движение назад" : "Backward movement", "_FlyBackward", new KeyCode[]
		{
			KeyCode.S
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Прочее" : "Other", MenuComponent._isRus ? "Паник-кей" : "Panic Key", "_PanicButton", new KeyCode[]
		{
			KeyCode.Keypad0
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Прочее" : "Other", MenuComponent._isRus ? "Свободная камера On/Off" : "Free camera On/Off", "_ToggleFreecam", new KeyCode[]
		{
			KeyCode.Keypad2
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Прочее" : "Other", MenuComponent._isRus ? "Выбор игрока" : "Target selection", "_SelectPlayer", new KeyCode[]
		{
			KeyCode.LeftAlt
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Прочее" : "Other", MenuComponent._isRus ? "Мгновенное отключение" : "Instant disconnect", "_InstantDisconnect", new KeyCode[]
		{
			KeyCode.F5
		});
		HotkeyUtilities.AddHotkey(MenuComponent._isRus ? "Прочее" : "Other", MenuComponent._isRus ? "Авто подбор вещей" : "Auto selection of things", "_AutoPickUp", new KeyCode[]
		{
			KeyCode.Mouse2
		});
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0002CC38 File Offset: 0x0002AE38
	public static void AddHotkey(string Group, string Name, string Identifier, params KeyCode[] DefaultKeys)
	{
		if (!HotkeyOptions.HotkeyDict.ContainsKey(Group))
		{
			HotkeyOptions.HotkeyDict.Add(Group, new Dictionary<string, Hotkey>());
		}
		Dictionary<string, Hotkey> dictionary = HotkeyOptions.HotkeyDict[Group];
		if (dictionary.ContainsKey(Identifier))
		{
			return;
		}
		Hotkey value = new Hotkey
		{
			Name = Name,
			Keys = DefaultKeys
		};
		dictionary.Add(Identifier, value);
		HotkeyOptions.UnorganizedHotkeys.Add(Identifier, value);
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0002CCA0 File Offset: 0x0002AEA0
	public static bool IsHotkeyDown(string Identifier)
	{
		return HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Any(new Func<KeyCode, bool>(Input.GetKeyDown)) && HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.All(new Func<KeyCode, bool>(Input.GetKey));
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00025609 File Offset: 0x00023809
	public static bool IsHotkeyHeld(string Identifier)
	{
		return HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.All(new Func<KeyCode, bool>(Input.GetKey));
	}
}
