using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020003A3 RID: 931
internal class Utility
{
	// Token: 0x06001DA0 RID: 7584 RVA: 0x00103710 File Offset: 0x00101910
	public static string GetWeaponPrefabName(string baseName, WeaponPrefabType prefabType = WeaponPrefabType.Weapon)
	{
		string text;
		if (CVars.EncryptContent)
		{
			if (CVars.UseUnityCache)
			{
				text = "_cache_" + baseName;
			}
			else
			{
				text = "_newWWW_" + baseName;
			}
		}
		else
		{
			text = baseName;
		}
		if (Utility.IsModableWeapon(baseName))
		{
			text += "_cs";
		}
		if (prefabType == WeaponPrefabType.Hands)
		{
			text += "-hands";
		}
		if (prefabType == WeaponPrefabType.Lod)
		{
			text += "-lod";
		}
		return text;
	}

	// Token: 0x06001DA1 RID: 7585 RVA: 0x00103794 File Offset: 0x00101994
	public static string GetWeaponPrefabName(int weaponIndex, WeaponPrefabType prefabType = WeaponPrefabType.Weapon)
	{
		string text;
		if (CVars.EncryptContent)
		{
			if (CVars.UseUnityCache)
			{
				text = "_cache_" + EnumNames.Weapons(weaponIndex);
			}
			else
			{
				text = "_newWWW_" + EnumNames.Weapons(weaponIndex);
			}
		}
		else
		{
			text = EnumNames.Weapons(weaponIndex);
		}
		if (Utility.IsModableWeapon(weaponIndex))
		{
			text += "_cs";
		}
		if (prefabType == WeaponPrefabType.Hands)
		{
			text += "-hands";
		}
		if (prefabType == WeaponPrefabType.Lod)
		{
			text += "-lod";
		}
		return text;
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x00103828 File Offset: 0x00101A28
	public static string GetWeaponPrefabPath(string baseName, WeaponPrefabType prefabType = WeaponPrefabType.Weapon)
	{
		string text = "Entities/";
		if (Utility.IsModableWeapon(baseName))
		{
			text += "mastering/";
		}
		if (prefabType == WeaponPrefabType.Weapon)
		{
			text += "weapons/";
		}
		if (prefabType == WeaponPrefabType.Hands)
		{
			text += "weapons-hands/";
		}
		if (prefabType == WeaponPrefabType.Lod)
		{
			text += "weapons-lods/";
		}
		if (CVars.EncryptContent)
		{
			if (CVars.UseUnityCache)
			{
				text += "_cache_/";
			}
			else
			{
				text += "_newWWW_/";
			}
		}
		return text;
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x001038BC File Offset: 0x00101ABC
	public static string GetModPrefabPath(WeaponPrefabType prefabType = WeaponPrefabType.Weapon)
	{
		string text = "Entities/mastering/";
		if (prefabType == WeaponPrefabType.Weapon)
		{
			text += "mods/";
		}
		if (prefabType == WeaponPrefabType.Lod)
		{
			text += "mods-lods/";
		}
		if (CVars.EncryptContent)
		{
			if (CVars.UseUnityCache)
			{
				text += "_cache_/";
			}
			else
			{
				text += "_newWWW_/";
			}
		}
		return text;
	}

	// Token: 0x06001DA4 RID: 7588 RVA: 0x00103928 File Offset: 0x00101B28
	public static string GetModPrefabName(MasteringMod mod, WeaponPrefabType prefabType = WeaponPrefabType.Weapon)
	{
		string text = string.Empty;
		if (CVars.EncryptContent)
		{
			if (CVars.UseUnityCache)
			{
				text += "_cache_";
			}
			else
			{
				text += "_newWWW_";
			}
		}
		if (mod.Type == ModType.optic)
		{
			text += "optic_";
		}
		if (mod.Type == ModType.silencer)
		{
			text += "sil_";
		}
		if (mod.Type == ModType.tactical)
		{
			text += "tac_";
		}
		text += mod.EngShortName;
		if (prefabType == WeaponPrefabType.Lod)
		{
			text += "-lod";
		}
		return text;
	}

	// Token: 0x06001DA5 RID: 7589 RVA: 0x001039D4 File Offset: 0x00101BD4
	public static bool IsModableWeapon(int weaponIndex)
	{
		bool flag = Enum.IsDefined(typeof(ModableWeapons), weaponIndex);
		return WeaponModsStorage.Instance().Weapons != null && WeaponModsStorage.Instance().Weapons.ContainsKey(weaponIndex);
	}

	// Token: 0x06001DA6 RID: 7590 RVA: 0x00103A1C File Offset: 0x00101C1C
	public static bool IsModableWeapon(Weapons weapon)
	{
		bool flag = Enum.IsDefined(typeof(ModableWeapons), (int)weapon);
		return WeaponModsStorage.Instance().Weapons != null && WeaponModsStorage.Instance().Weapons.ContainsKey((int)weapon);
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x00103A64 File Offset: 0x00101C64
	public static bool IsModableWeapon(string weaponName)
	{
		if (weaponName.EndsWith("_cs-lod"))
		{
			weaponName = weaponName.Substring(0, weaponName.Length - "_cs-lod".Length);
		}
		bool flag = Enum.IsDefined(typeof(ModableWeapons), weaponName + "_cs");
		return WeaponModsStorage.Instance().Weapons != null && WeaponModsStorage.Instance().Weapons.ContainsKey((int)Enum.Parse(typeof(Weapons), weaponName));
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x00103AF0 File Offset: 0x00101CF0
	public static string ModsToString(Dictionary<ModType, int> dict)
	{
		string text = string.Empty;
		if (dict != null)
		{
			foreach (KeyValuePair<ModType, int> keyValuePair in dict)
			{
				text = text + keyValuePair.Value + " ";
			}
		}
		return text;
	}

	// Token: 0x06001DA9 RID: 7593 RVA: 0x00103B70 File Offset: 0x00101D70
	public static Dictionary<ModType, int> StringToMods(string modsStr)
	{
		Dictionary<ModType, int> dictionary = new Dictionary<ModType, int>();
		if (modsStr != null)
		{
			modsStr = modsStr.Trim();
			if (modsStr != string.Empty)
			{
				foreach (string text in modsStr.Split(new char[]
				{
					' '
				}))
				{
					int num = 0;
					if (int.TryParse(text, out num))
					{
						ModsStorage modsStorage = ModsStorage.Instance();
						ModType type = modsStorage.GetModById(num).Type;
						dictionary.Add(type, num);
					}
					else
					{
						Debug.Log("Cant parse " + text);
					}
				}
			}
		}
		return dictionary;
	}

	// Token: 0x06001DAA RID: 7594 RVA: 0x00103C14 File Offset: 0x00101E14
	public static byte[] CaptureScreenshot()
	{
		Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0);
		return texture2D.EncodeToPNG();
	}

	// Token: 0x06001DAB RID: 7595 RVA: 0x00103C60 File Offset: 0x00101E60
	public static Texture2D CaptureCustomScreenshot(int width, int height)
	{
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGB24, true, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		int num = Screen.width / 800;
		Texture2D texture2D2 = new Texture2D(width >> num, height >> num, TextureFormat.RGB24, false, false);
		texture2D2.SetPixels32(texture2D.GetPixels32(num));
		texture2D2.Apply();
		return texture2D2;
	}

	// Token: 0x06001DAC RID: 7596 RVA: 0x00103CC8 File Offset: 0x00101EC8
	public static byte[] CaptureScreenshot(int width, int height)
	{
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGB24, false, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		return texture2D.EncodeToPNG();
	}

	// Token: 0x06001DAD RID: 7597 RVA: 0x00103D04 File Offset: 0x00101F04
	public static PlayerClass convertFromWeaponSpecific(WeaponSpecific weaponSpecific)
	{
		if (weaponSpecific == WeaponSpecific.scout)
		{
			return PlayerClass.scout;
		}
		if (weaponSpecific == WeaponSpecific.storm_trooper)
		{
			return PlayerClass.storm_trooper;
		}
		if (weaponSpecific == WeaponSpecific.sniper)
		{
			return PlayerClass.sniper;
		}
		if (weaponSpecific == WeaponSpecific.destroyer)
		{
			return PlayerClass.gunsmith;
		}
		if (weaponSpecific == WeaponSpecific.gunsmith)
		{
			return PlayerClass.destroyer;
		}
		if (weaponSpecific == WeaponSpecific.careerist)
		{
			return PlayerClass.careerist;
		}
		if (weaponSpecific == (WeaponSpecific)128)
		{
			return PlayerClass.sniper;
		}
		return PlayerClass.none;
	}

	// Token: 0x06001DAE RID: 7598 RVA: 0x00103D58 File Offset: 0x00101F58
	public static float Period(float period, float k = 1f)
	{
		float num = Time.realtimeSinceStartup * k;
		return num - (float)((int)(num / period)) * period;
	}

	// Token: 0x06001DAF RID: 7599 RVA: 0x00103D78 File Offset: 0x00101F78
	public static Vector3 ProjectPointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
	{
		Vector3 rhs = point - lineStart;
		Vector3 vector = lineEnd - lineStart;
		float magnitude = vector.magnitude;
		Vector3 vector2 = vector;
		if (magnitude > 1E-06f)
		{
			vector2 /= magnitude;
		}
		float d = Mathf.Clamp(Vector3.Dot(vector2, rhs), 0f, magnitude);
		return lineStart + vector2 * d;
	}

	// Token: 0x06001DB0 RID: 7600 RVA: 0x00103DD4 File Offset: 0x00101FD4
	public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
	{
		return Vector3.Magnitude(Utility.ProjectPointLine(point, lineStart, lineEnd) - point);
	}

	// Token: 0x06001DB1 RID: 7601 RVA: 0x00103DEC File Offset: 0x00101FEC
	public static float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(2f * CVars.g_jumpHeight * Physics.gravity.magnitude);
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x00103E18 File Offset: 0x00102018
	public static float ClampAngle(float ang, float min, float max)
	{
		if (ang < -360f)
		{
			ang += 360f;
		}
		if (ang > 360f)
		{
			ang -= 360f;
		}
		return Mathf.Clamp(ang, min, max);
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x00103E58 File Offset: 0x00102058
	public static Transform FindHierarchy(Transform current, string name)
	{
		if (current.name == name)
		{
			return current;
		}
		for (int i = 0; i < current.GetChildCount(); i++)
		{
			Transform transform = Utility.FindHierarchy(current.GetChild(i), name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x06001DB4 RID: 7604 RVA: 0x00103EAC File Offset: 0x001020AC
	public static void SetLayerRecursively(GameObject current, int layer)
	{
		for (int i = 0; i < current.transform.GetChildCount(); i++)
		{
			Utility.SetLayerRecursively(current.transform.GetChild(i).gameObject, layer);
		}
		current.layer = layer;
	}

	// Token: 0x06001DB5 RID: 7605 RVA: 0x00103EF4 File Offset: 0x001020F4
	public static void SetRenderRecursively(GameObject current, string name, bool visible)
	{
		for (int i = 0; i < current.transform.GetChildCount(); i++)
		{
			Utility.SetRenderRecursively(current.transform.GetChild(i).gameObject, name, visible);
		}
		if (current.renderer && current.name == name)
		{
			current.renderer.enabled = visible;
		}
	}

	// Token: 0x06001DB6 RID: 7606 RVA: 0x00103F64 File Offset: 0x00102164
	public static void DrawLine(Vector3 position, Quaternion rotation, Vector3 p1, Vector3 p2, Color color, float duration)
	{
		p1 = position + rotation * p1;
		p2 = position + rotation * p2;
		Debug.DrawLine(p1, p2, color, duration);
	}

	// Token: 0x06001DB7 RID: 7607 RVA: 0x00103F9C File Offset: 0x0010219C
	public static void DrawLine(Vector3 p1, Vector3 p2, Color color, float duration)
	{
		Debug.DrawLine(p1, p2, color, duration);
	}

	// Token: 0x06001DB8 RID: 7608 RVA: 0x00103FA8 File Offset: 0x001021A8
	public static void DrawSphere(Vector3 position, Quaternion rotation, float radius, Color color, float duration = 0f)
	{
		float num = 20f;
		for (float num2 = 0f; num2 <= 360f; num2 += num)
		{
			float num3 = 1f;
			float f = num2 * 0.017453292f;
			float f2 = (num2 - num) * 0.017453292f;
			Vector3 a = new Vector3(Mathf.Cos(f) * num3, 0f, Mathf.Sin(f) * num3);
			Vector3 a2 = new Vector3(Mathf.Cos(f2) * num3, 0f, Mathf.Sin(f2) * num3);
			Utility.DrawLine(position, rotation, a * radius, a2 * radius, color, duration);
			a = new Vector3(0f, Mathf.Cos(f) * num3, Mathf.Sin(f) * num3);
			a2 = new Vector3(0f, Mathf.Cos(f2) * num3, Mathf.Sin(f2) * num3);
			Utility.DrawLine(position, rotation, a * radius, a2 * radius, color, duration);
			a = new Vector3(Mathf.Cos(f) * num3, Mathf.Sin(f) * num3, 0f);
			a2 = new Vector3(Mathf.Cos(f2) * num3, Mathf.Sin(f2) * num3, 0f);
			Utility.DrawLine(position, rotation, a * radius, a2 * radius, color, duration);
		}
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x001040EC File Offset: 0x001022EC
	public static void DrawCapsule(CapsuleCollider capsule, Vector3 position, Quaternion rotation, Color color, float duration = 0f)
	{
		Vector3 vector = Vector3.up;
		if (capsule.direction == 0)
		{
			vector = Vector3.left;
		}
		if (capsule.direction == 1)
		{
			vector = Vector3.up;
		}
		if (capsule.direction == 2)
		{
			vector = Vector3.forward;
		}
		Utility.DrawSphere(rotation * (vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) + capsule.center) + position, rotation, capsule.radius, color, duration);
		Utility.DrawSphere(rotation * (-vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) + capsule.center) + position, rotation, capsule.radius, color, duration);
		Vector3 p = vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) + Vector3.Cross(vector, Vector3.up) * capsule.radius;
		Vector3 p2 = -vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) + Vector3.Cross(vector, Vector3.up) * capsule.radius;
		Utility.DrawLine(position + rotation * capsule.center, rotation, p, p2, color, duration);
		p = vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) - Vector3.Cross(vector, Vector3.up) * capsule.radius;
		p2 = -vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) - Vector3.Cross(vector, Vector3.up) * capsule.radius;
		Utility.DrawLine(position + rotation * capsule.center, rotation, p, p2, color, duration);
		p = vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) + Vector3.Cross(vector, Vector3.forward) * capsule.radius;
		p2 = -vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) + Vector3.Cross(vector, Vector3.forward) * capsule.radius;
		Utility.DrawLine(position + rotation * capsule.center, rotation, p, p2, color, duration);
		p = vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) - Vector3.Cross(vector, Vector3.forward) * capsule.radius;
		p2 = -vector * Mathf.Max(capsule.height / 2f - capsule.radius, 0f) - Vector3.Cross(vector, Vector3.forward) * capsule.radius;
		Utility.DrawLine(position + rotation * capsule.center, rotation, p, p2, color, duration);
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x0010444C File Offset: 0x0010264C
	public static void ShowDisconnectReason(string title, string desc)
	{
		if (!PopupGUI.thisObject.IsShowingQuickGame)
		{
			EventFactory.Call("ShowInterface", null);
			EventFactory.Call("HidePopup", new Popup(WindowsID.Connection, Language.Connection, Language.ConnectionFailed, PopupState.information, false, true, string.Empty, string.Empty));
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Connection, title, desc, PopupState.information, true, true, string.Empty, string.Empty));
		}
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x001044BC File Offset: 0x001026BC
	public static void ChangeParent(Transform target, Transform parent)
	{
		Vector3 localPosition = target.localPosition;
		Quaternion localRotation = target.localRotation;
		Vector3 localScale = target.localScale;
		target.parent = parent;
		target.localPosition = localPosition;
		target.localRotation = localRotation;
		target.localScale = localScale;
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x001044FC File Offset: 0x001026FC
	public static void ChangeParentWorld(Transform target, Transform parent)
	{
		Vector3 position = target.position;
		Quaternion rotation = target.rotation;
		target.parent = parent;
		target.position = position;
		target.rotation = rotation;
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x0010452C File Offset: 0x0010272C
	public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Texture2D texture, int thickness)
	{
		Vector2 vector = lineEnd - lineStart;
		float num = 57.29578f * Mathf.Atan(vector.y / vector.x);
		if (vector.x < 0f)
		{
			num += 180f;
		}
		if (float.IsNaN(num))
		{
			return;
		}
		if (float.IsNaN(lineStart.x))
		{
			return;
		}
		if (float.IsNaN(lineStart.y))
		{
			return;
		}
		if (float.IsNaN(lineEnd.x))
		{
			return;
		}
		if (float.IsNaN(lineEnd.y))
		{
			return;
		}
		if (thickness < 1)
		{
			thickness = 1;
		}
		int num2 = (int)Mathf.Ceil((float)(thickness / 2));
		GUIUtility.RotateAroundPivot(num, lineStart);
		GUI.DrawTexture(new Rect(lineStart.x, lineStart.y - (float)num2, vector.magnitude, (float)thickness), texture);
		GUIUtility.RotateAroundPivot(-num, lineStart);
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x00104614 File Offset: 0x00102814
	public static PlayerClass TranslatePlayerClass(int plClass)
	{
		switch (plClass)
		{
		case 0:
			return PlayerClass.scout;
		case 1:
			return PlayerClass.storm_trooper;
		case 2:
			return PlayerClass.destroyer;
		case 3:
			return PlayerClass.sniper;
		case 4:
			return PlayerClass.gunsmith;
		case 5:
			return PlayerClass.careerist;
		default:
			return PlayerClass.none;
		}
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x00104654 File Offset: 0x00102854
	public static int TranslatePlayerClass(PlayerClass plClass)
	{
		switch (plClass)
		{
		case PlayerClass.storm_trooper:
			return 1;
		case PlayerClass.scout:
			return 0;
		case PlayerClass.sniper:
			return 3;
		case PlayerClass.destroyer:
			return 2;
		case PlayerClass.gunsmith:
			return 4;
		case PlayerClass.careerist:
			return 5;
		default:
			return 0;
		}
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x00104698 File Offset: 0x00102898
	public static string PrintHierarchy(Transform transform, int depth)
	{
		string text = transform.name;
		Component[] components = transform.GetComponents<Component>();
		for (int i = 0; i < components.Length; i++)
		{
			string text2 = components[i].GetType().ToString();
			if (!text2.Contains("UnityEngine") || text2.Contains("Network"))
			{
				if (components[i] is NetworkView)
				{
					string text3 = text;
					text = string.Concat(new object[]
					{
						text3,
						" <",
						text2,
						" ",
						(components[i] as NetworkView).viewID,
						">"
					});
				}
				else
				{
					text = text + " <" + text2 + ">";
				}
			}
		}
		for (int j = 0; j < transform.GetChildCount(); j++)
		{
			text = text + "\n" + new string(' ', depth) + Utility.PrintHierarchy(transform.GetChild(j), depth + 1);
		}
		return text;
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x001047A4 File Offset: 0x001029A4
	public static byte[] BinarySerializeObject<T>(T objectToSerialize)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryFormatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
		MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, objectToSerialize);
		return memoryStream.ToArray();
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x001047D8 File Offset: 0x001029D8
	public static T BinaryDeserializeObject<T>(byte[] bytes)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryFormatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
		MemoryStream serializationStream = new MemoryStream(bytes);
		return (T)((object)binaryFormatter.Deserialize(serializationStream));
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x00104808 File Offset: 0x00102A08
	public static bool ValidColor(string str)
	{
		if (str.Length <= 0)
		{
			return false;
		}
		if (str.Length != 6)
		{
			return false;
		}
		Regex regex = new Regex("[^a-fA-F0-9]");
		return !regex.IsMatch(str);
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x00104848 File Offset: 0x00102A48
	public static int IntPlatform()
	{
		string realm = CVars.realm;
		switch (realm)
		{
		case "standalone":
			return 0;
		case "vk":
			return 1;
		case "kg":
		case "ag":
		case "mc":
		case "fb":
			return 2;
		case "ok":
			return 3;
		case "mailru":
			return 4;
		case "omega":
			return -1;
		}
		return -2;
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x00104928 File Offset: 0x00102B28
	public static void SetResolution(int width, int height, bool fullScreen)
	{
		Screen.SetResolution(width, height, fullScreen);
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x00104934 File Offset: 0x00102B34
	public static void SetResolution(Resolution resolution)
	{
		Screen.SetResolution(resolution.width, resolution.height, true);
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x0010494C File Offset: 0x00102B4C
	public static void FixResolution()
	{
		if (Screen.width > 1920 || Screen.height > 1080)
		{
			Utility.SetResolution(1920, 1920, Screen.fullScreen);
		}
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x0010498C File Offset: 0x00102B8C
	public static void DeleteDirectory(string targetDir)
	{
		try
		{
			string[] files = Directory.GetFiles(targetDir);
			string[] directories = Directory.GetDirectories(targetDir);
			foreach (string path in files)
			{
				File.SetAttributes(path, FileAttributes.Normal);
				File.Delete(path);
			}
			foreach (string targetDir2 in directories)
			{
				Utility.DeleteDirectory(targetDir2);
			}
			Directory.Delete(targetDir, false);
		}
		catch (Exception exception)
		{
			Debug.LogError("Cant delete " + targetDir);
			Debug.LogException(exception);
		}
	}

	// Token: 0x0400224F RID: 8783
	public const int MAX_AVAILABLE_RESOLUTION_WIDTH = 1920;

	// Token: 0x04002250 RID: 8784
	public const int MAX_AVAILABLE_RESOLUTION_HEIGHT = 1080;
}
