using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x0200037D RID: 893
internal class Helpers
{
	// Token: 0x06001CE4 RID: 7396 RVA: 0x000FF440 File Offset: 0x000FD640
	public static string SeparateNumericString(string str)
	{
		string text = string.Empty;
		int num = 0;
		if (str.Length % 3 == 0)
		{
			num = 3;
		}
		else if (str.Length % 3 == 2)
		{
			num = 2;
		}
		else if (str.Length % 3 == 1)
		{
			num = 1;
		}
		int i = 0;
		while (i < str.Length)
		{
			if (i == 0 && str.Length % 3 != 0)
			{
				text = text + str.Substring(i, num) + " ";
				i += num;
			}
			else
			{
				text = text + str.Substring(i, 3) + " ";
				i += 3;
			}
		}
		return text;
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x000FF4EC File Offset: 0x000FD6EC
	public static string KFormat(int value)
	{
		int i = value;
		string text = string.Empty;
		while (i > 1000)
		{
			i /= 1000;
			text += "k";
		}
		return i + text;
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x000FF534 File Offset: 0x000FD734
	public static string GetTag(string tag)
	{
		if (string.IsNullOrEmpty(tag))
		{
			return string.Empty;
		}
		if (tag.Length == 0)
		{
			return string.Empty;
		}
		if (tag.Length < 10)
		{
			return string.Empty;
		}
		if (tag[0] == '[')
		{
			return tag.Substring(9);
		}
		return tag;
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x000FF590 File Offset: 0x000FD790
	public static Color GetTagColor(string tag)
	{
		try
		{
			string text = tag.Substring(2, 6);
			float num = (float)Convert.ToInt32(text.Substring(0, 2), 16);
			float num2 = (float)Convert.ToInt32(text.Substring(2, 2), 16);
			float num3 = (float)Convert.ToInt32(text.Substring(4, 2), 16);
			return new Color(num / 256f, num2 / 256f, num3 / 256f, 1f);
		}
		catch (Exception exception)
		{
			Debug.Log("Something wrong in Helpers.GetTagColor().");
			Debug.LogException(exception);
		}
		return default(Color);
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x000FF648 File Offset: 0x000FD848
	public static string GetTagHexColor(string tag)
	{
		try
		{
			if (tag.Length < 9)
			{
				return "#FFFFFF";
			}
			string str = tag.Substring(2, 6);
			return "#" + str;
		}
		catch (Exception exception)
		{
			Debug.Log("Something wrong in Helpers.GetTagColor().");
			Debug.LogException(exception);
		}
		return "#FFFFFF";
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x000FF6C8 File Offset: 0x000FD8C8
	public static Color HexToColor(string hexColor)
	{
		float num = (float)Convert.ToInt32(hexColor.Substring(0, 2), 16);
		float num2 = (float)Convert.ToInt32(hexColor.Substring(2, 2), 16);
		float num3 = (float)Convert.ToInt32(hexColor.Substring(4, 2), 16);
		return new Color(num / 256f, num2 / 256f, num3 / 256f, 1f);
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x000FF728 File Offset: 0x000FD928
	public static string ColoredTag(string tag)
	{
		if (string.IsNullOrEmpty(tag))
		{
			return string.Empty;
		}
		if (!tag.Contains("[#"))
		{
			return tag;
		}
		string text = tag.Substring(1, 7);
		return string.Concat(new string[]
		{
			"<color=",
			text,
			">",
			tag.Substring(9),
			"</color>"
		});
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x000FF798 File Offset: 0x000FD998
	public static string ColoredLog(object obj, string color = "#00FF00")
	{
		return string.Concat(new object[]
		{
			"<b><color=",
			color,
			">This is Den's log:</color> ",
			obj,
			"</b>"
		});
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x000FF7C8 File Offset: 0x000FD9C8
	public static void Hint(Rect onHoverRect, string hint, GUIStyle style, Helpers.HintAlignment alignment = Helpers.HintAlignment.Left, float xOffset = 0f, float yOffset = 0f)
	{
		if (!onHoverRect.Contains(Event.current.mousePosition))
		{
			return;
		}
		Rect position = default(Rect);
		float num = (MainGUI.Instance.CalcWidth(hint, style.font, style.fontSize) >= 300f) ? 300f : MainGUI.Instance.CalcWidth(hint, style.font, style.fontSize);
		float num2 = (MainGUI.Instance.CalcWidth(hint, style.font, style.fontSize) >= 300f) ? ((float)(style.fontSize + 2) * Mathf.Ceil(MainGUI.Instance.CalcWidth(hint, style.font, style.fontSize) / 300f + 1f)) : ((float)(style.fontSize + 2));
		if (alignment == Helpers.HintAlignment.Left)
		{
			position.Set(onHoverRect.x + xOffset, onHoverRect.y - num2 + yOffset, num, num2);
		}
		else
		{
			position.Set(onHoverRect.xMax - num + xOffset, onHoverRect.y - num2 + yOffset, num, num2);
		}
		GUI.DrawTexture(position, MainGUI.Instance.black);
		style.wordWrap = true;
		GUI.Label(position, hint, style);
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x000FF904 File Offset: 0x000FDB04
	public static void HintList(Rect onHoverRect, List<string> list, GUIStyle style, bool left = false, float xOffset = 0f, float yOffset = 0f)
	{
		if (!onHoverRect.Contains(Event.current.mousePosition))
		{
			return;
		}
		if (list == null || list.Count == 0)
		{
			return;
		}
		bool flag = Event.current.mousePosition.y > (float)(Screen.height / 2 - 100);
		float num = MainGUI.Instance.CalcWidth(list[0], style.font, style.fontSize);
		foreach (string text in list)
		{
			if (MainGUI.Instance.CalcWidth(text, style.font, style.fontSize) > num)
			{
				num = MainGUI.Instance.CalcWidth(text, style.font, style.fontSize);
			}
		}
		float num2 = (float)(list.Count * (style.fontSize + 2));
		float num3;
		if (Event.current.mousePosition.x > (float)(Screen.width / 2) || left)
		{
			num3 = onHoverRect.x - num;
		}
		else
		{
			num3 = onHoverRect.xMax;
		}
		float num4 = (!flag) ? onHoverRect.yMax : onHoverRect.yMin;
		Rect position = new Rect(num3 + xOffset, num4 + yOffset - ((!flag) ? 0f : num2), num, num2);
		GUI.DrawTexture(position, MainGUI.Instance.black);
		style.wordWrap = true;
		for (int i = 0; i < list.Count; i++)
		{
			float num5 = (float)(i * (2 + style.fontSize));
			GUI.Label(new Rect(num3 + xOffset + 2f, num4 + yOffset + ((!flag) ? num5 : (num5 - num2)), num, num2), list[i], style);
		}
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x000FFB08 File Offset: 0x000FDD08
	public static string ColoredText(object text, string color = "#FFFFFF")
	{
		return string.Concat(new object[]
		{
			"<color=",
			color,
			">",
			text,
			"</color>"
		});
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x000FFB38 File Offset: 0x000FDD38
	public static string HexOnly(string str)
	{
		string pattern = "[^a-fA-F0-9]";
		string empty = string.Empty;
		Regex regex = new Regex(pattern);
		return regex.Replace(str, empty);
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x000FFB68 File Offset: 0x000FDD68
	public static string FixedNickname(string editableName, string currentName)
	{
		if (editableName == currentName)
		{
			return currentName;
		}
		Regex regex = new Regex("[^a-zA-Zà-ÿÀ-ß0-9]");
		if (Helpers.WordLanguage(editableName) == Helpers.Languages.En)
		{
			regex = new Regex("[^a-zA-Z0-9]");
		}
		if (Helpers.WordLanguage(editableName) == Helpers.Languages.Ru)
		{
			regex = new Regex("[^à-ÿÀ-ß0-9]");
		}
		return regex.Replace(editableName, string.Empty);
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x000FFBCC File Offset: 0x000FDDCC
	public static string WeaponKitNameRegEx(string str)
	{
		Regex regex = new Regex("[^a-zA-Z0-9_ ]");
		string input = regex.Replace(str, string.Empty);
		regex = new Regex("[ ]{2,}", RegexOptions.None);
		input = regex.Replace(input, " ");
		regex = new Regex("[_]{2,}", RegexOptions.None);
		return regex.Replace(input, "_");
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x000FFC2C File Offset: 0x000FDE2C
	private static Helpers.Languages WordLanguage(string s)
	{
		foreach (char c in s)
		{
			if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
			{
				return Helpers.Languages.En;
			}
			if ((c >= 'à' && c <= 'ÿ') || (c >= 'À' && c <= 'ß'))
			{
				return Helpers.Languages.Ru;
			}
		}
		return Helpers.Languages.Any;
	}

	// Token: 0x0200037E RID: 894
	public enum HintAlignment
	{
		// Token: 0x040021A2 RID: 8610
		Left,
		// Token: 0x040021A3 RID: 8611
		Rigth
	}

	// Token: 0x0200037F RID: 895
	private enum Languages
	{
		// Token: 0x040021A5 RID: 8613
		Any,
		// Token: 0x040021A6 RID: 8614
		En,
		// Token: 0x040021A7 RID: 8615
		Ru
	}
}
