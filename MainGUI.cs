using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Assets.Scripts.Camouflage;
using CWGUInamespace.Tutor;
using LeagueGUI;
using namespaceMainGUI;
using UnityEngine;

// Token: 0x02000159 RID: 345
[AddComponentMenu("Scripts/GUI/MainGUI")]
internal class MainGUI : Form
{
	// Token: 0x17000114 RID: 276
	// (get) Token: 0x0600088F RID: 2191 RVA: 0x0004D888 File Offset: 0x0004BA88
	private UserInfo Info
	{
		get
		{
			return Main.UserInfo;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000890 RID: 2192 RVA: 0x0004D890 File Offset: 0x0004BA90
	public SuitInfo currentSuit
	{
		get
		{
			return this.Info.suits[this.CurrentSuitIndex];
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0004D8A4 File Offset: 0x0004BAA4
	private void OnApplicationQuit()
	{
		if (!LeagueWindow.I.topFrame.Searching || SearchFrame.Accepted)
		{
			return;
		}
		ClientLeagueSystem.SendCancelRequest();
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0004D8D8 File Offset: 0x0004BAD8
	private MainGUI.Settings GetSettings(string textColor)
	{
		MainGUI.Settings settings;
		if (!MainGUI.setts.TryGetValue(textColor, out settings))
		{
			settings = new MainGUI.Settings();
			settings.Font = this.fontDNC57;
			if (textColor.Length > 7)
			{
				char c = textColor[8];
				switch (c)
				{
				case 'A':
					settings.Font = null;
					break;
				default:
					if (c != 'M')
					{
						if (c == 'T')
						{
							settings.Font = this.fontTahoma;
						}
					}
					else
					{
						settings.Font = this.fontMicra;
					}
					break;
				case 'D':
					settings.Font = this.fontDNC57;
					break;
				}
			}
			float num = (float)Convert.ToInt32(textColor.Substring(1, 2), 16);
			float num2 = (float)Convert.ToInt32(textColor.Substring(3, 2), 16);
			float num3 = (float)Convert.ToInt32(textColor.Substring(5, 2), 16);
			settings.Color = new Color(num / 256f, num2 / 256f, num3 / 256f);
			MainGUI.setts.Add(textColor, settings);
		}
		return settings;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0004D9F0 File Offset: 0x0004BBF0
	public ButtonState ButtonSelected(ButtonState bstate, Vector2 pos, Texture2D idle = null, Texture2D over = null, Texture2D selected = null, string text = "", int textSize = 24, string textColor = "#ffffff", TextAnchor anchor = TextAnchor.MiddleCenter)
	{
		return this.Button(pos, idle, over, selected, text, textSize, textColor, anchor, new ButtonState?(bstate), null, null, null);
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0004DA24 File Offset: 0x0004BC24
	private ButtonState IncrementedHeightButton(ref Vector2 pos, Texture2D idle = null, Texture2D over = null, Texture2D selected = null, string text = "", int textSize = 24, string textColor = "#ffffff", TextAnchor anchor = TextAnchor.MiddleCenter, ButtonState? state = null, Vector2? scale = null, AudioClip hoverSound = null, AudioClip clickSound = null, int step = 24)
	{
		ButtonState result = this.Button(pos, idle, over, selected, text, textSize, textColor, anchor, state, scale, hoverSound, clickSound);
		pos.y += (float)step;
		return result;
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0004DA64 File Offset: 0x0004BC64
	public ButtonState Button(Vector2 pos, Texture2D idle = null, Texture2D over = null, Texture2D selected = null, string text = "", int textSize = 24, string textColor = "#ffffff", TextAnchor anchor = TextAnchor.MiddleCenter, ButtonState? state = null, Vector2? scale = null, AudioClip hoverSound = null, AudioClip clickSound = null)
	{
		GUIStyle button = this.skin.button;
		button.fontSize = textSize;
		MainGUI.Settings settings = this.GetSettings(textColor);
		button.font = settings.Font;
		button.alignment = anchor;
		button.normal.textColor = settings.Color;
		button.hover.textColor = settings.Color;
		if (over != null && this.disabled)
		{
			over = idle;
		}
		if (selected != null && this.disabled)
		{
			selected = idle;
		}
		if (selected != null && state != null && state.Value.Selected)
		{
			button.normal.background = selected;
			button.hover.background = selected;
			button.active.background = selected;
		}
		else
		{
			button.normal.background = idle;
			button.hover.background = over;
			button.active.background = selected;
		}
		Vector2 size = Vector2.one * 64f;
		if (over != null)
		{
			size = new Vector2((float)over.width, (float)over.height);
		}
		if (scale != null)
		{
			size = new Vector2(size.x * scale.Value.x, size.y * scale.Value.y);
		}
		if (this.disabled)
		{
			if (button.normal.background)
			{
				this.PictureSized(pos, button.normal.background, size);
			}
			if (text != string.Empty)
			{
				this.TextLabel(new Rect(pos.x, pos.y, size.x, size.y), text, textSize, textColor, anchor, true);
			}
			return new ButtonState(false, false, false);
		}
		ButtonState result = default(ButtonState);
		if (state != null)
		{
			result = state.Value;
		}
		result.Clicked = false;
		result.Hover = this.gui.inRect(new Rect(pos.x, pos.y, size.x, size.y), this.upper, this.cursorPosition);
		bool flag = this.gui.inRect(new Rect(pos.x, pos.y, size.x, size.y), this.upper, this.cursorPositionPrev);
		if (result.Hover)
		{
			if (GUI.Button(new Rect(pos.x, pos.y, size.x, size.y), text, button))
			{
				Audio.Play(Vector3.zero, clickSound ?? this.clickSoundPrefab, -1f, -1f);
				result.Clicked = true;
				if (selected != null && state != null)
				{
					result.Selected = !result.Selected;
				}
			}
			else if (!flag)
			{
				Audio.Play(Vector3.zero, hoverSound ?? this.hoverSoundPrefab, -1f, -1f);
			}
			return result;
		}
		if (button.normal.background)
		{
			this.PictureSized(pos, button.normal.background, size);
		}
		if (text != string.Empty)
		{
			this.TextLabel(new Rect(pos.x, pos.y, size.x, size.y), text, textSize, textColor, anchor, true);
		}
		return new ButtonState(false, false, false);
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0004DE3C File Offset: 0x0004C03C
	public ButtonState TextButton(Rect rect, string text = "", int textSize = 24, string textColor = "#ffffff", string textColorOver = "#ffffff", TextAnchor anchor = TextAnchor.MiddleCenter, ButtonState? state = null, Vector2? scale = null, AudioClip hoverSound = null, AudioClip clickSound = null)
	{
		GUIStyle button = this.skin.button;
		button.fontSize = textSize;
		button.font = this.fontDNC57;
		MainGUI.Settings settings = this.GetSettings(textColor);
		button.font = settings.Font;
		button.alignment = anchor;
		button.normal.textColor = settings.Color;
		if (this.disabled)
		{
			button.hover.textColor = button.normal.textColor;
		}
		else
		{
			button.hover.textColor = settings.Color;
		}
		button.normal.background = this.invisible;
		button.hover.background = this.invisible;
		button.active.background = this.invisible;
		if (scale != null)
		{
			rect = new Rect(rect.x, rect.y, rect.x * scale.Value.x, rect.y * scale.Value.y);
		}
		if (this.disabled)
		{
			if (text != string.Empty)
			{
				GUI.Label(rect, text, button);
			}
			return new ButtonState(false, false, false);
		}
		ButtonState result = default(ButtonState);
		if (state != null)
		{
			result = state.Value;
		}
		result.Clicked = false;
		result.Hover = this.gui.inRect(rect, this.upper, this.cursorPosition);
		bool flag = this.gui.inRect(rect, this.upper, this.cursorPositionPrev);
		if (result.Hover)
		{
			if (GUI.Button(rect, text, button))
			{
				if (clickSound == null)
				{
					Audio.Play(Vector3.zero, this.clickSoundPrefab, -1f, -1f);
				}
				else
				{
					Audio.Play(Vector3.zero, clickSound, -1f, -1f);
				}
				result.Clicked = true;
				if (state != null)
				{
					result.Selected = !result.Selected;
				}
			}
			else if (result.Hover && !flag)
			{
				if (hoverSound == null)
				{
					Audio.Play(Vector3.zero, this.hoverSoundPrefab, -1f, -1f);
				}
				else
				{
					Audio.Play(Vector3.zero, hoverSound, -1f, -1f);
				}
			}
			return result;
		}
		if (text != string.Empty)
		{
			GUI.Label(rect, text, button);
		}
		return new ButtonState(false, false, false);
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0004E0D8 File Offset: 0x0004C2D8
	public bool CheckBox(Vector2 pos, bool state, Rect? rect = null, string text = "", int textSize = 16, string textColor = "#FFFFFF", TextAnchor alignment = TextAnchor.UpperLeft)
	{
		if (state)
		{
			if (this.Button(pos, this.server_window[18], this.server_window[19], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				state = !state;
			}
		}
		else if (this.Button(pos, this.server_window[1], this.server_window[2], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			state = !state;
		}
		if (rect != null)
		{
			this.BeginGroup(new Rect(pos.x, pos.y, rect.Value.width, rect.Value.height));
			this.TextLabel(rect.Value, text, textSize, textColor, alignment, true);
			this.EndGroup();
		}
		return state;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x0004E1F0 File Offset: 0x0004C3F0
	public bool HardcoreToggle(Vector2 pos, bool state, ref bool levelCheckBox, ref bool fullCheckBox)
	{
		if (state)
		{
			if (this.Button(pos, this.server_window[27], this.server_window[28], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				state = !state;
				levelCheckBox = true;
				fullCheckBox = false;
			}
		}
		else if (this.Button(pos, this.server_window[25], this.server_window[26], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			state = !state;
			levelCheckBox = false;
			fullCheckBox = true;
		}
		return state;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0004E2B8 File Offset: 0x0004C4B8
	public bool HardcoreToggle(Vector2 pos, bool state)
	{
		if (state)
		{
			if (this.Button(pos, this.server_window[27], this.server_window[28], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				state = !state;
			}
		}
		else if (this.Button(pos, this.server_window[25], this.server_window[26], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			state = !state;
		}
		return state;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0004E370 File Offset: 0x0004C570
	public bool ProtectedToggle(Vector2 pos, bool state, ref bool levelCheckBox, ref bool fullCheckBox)
	{
		if (state)
		{
			if (this.Button(pos, this.server_window[41], this.server_window[42], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				state = !state;
				levelCheckBox = true;
				fullCheckBox = false;
			}
		}
		else if (this.Button(pos, this.server_window[39], this.server_window[40], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			state = !state;
			levelCheckBox = false;
			fullCheckBox = true;
		}
		return state;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0004E438 File Offset: 0x0004C638
	public bool FriendsToggle(Vector2 pos, bool state, ref bool levelCheckBox, ref bool fullCheckBox)
	{
		if (state)
		{
			if (this.Button(pos, this.server_window[35], this.server_window[36], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				state = !state;
				levelCheckBox = true;
				fullCheckBox = false;
			}
		}
		else if (this.Button(pos, this.server_window[33], this.server_window[34], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			state = !state;
			levelCheckBox = false;
			fullCheckBox = true;
		}
		return state;
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0004E500 File Offset: 0x0004C700
	public bool ClanToggle(Vector2 pos, bool state)
	{
		if (state)
		{
			if (this.Button(pos, this.server_window[31], this.server_window[32], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				state = !state;
			}
		}
		else if (this.Button(pos, this.server_window[29], this.server_window[30], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			state = !state;
		}
		return state;
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0004E5B8 File Offset: 0x0004C7B8
	public void PictureCentered(Vector2 pos, Texture2D picture, Vector2 scale)
	{
		Vector2 vector = new Vector2((float)picture.width * scale.x, (float)picture.height * scale.y);
		GUI.DrawTexture(new Rect(pos.x - vector.x / 2f, pos.y - vector.y / 2f, vector.x, vector.y), picture, ScaleMode.ScaleToFit, true);
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0004E630 File Offset: 0x0004C830
	public void PictureCentereNoScale(Vector2 pos, Texture picture, Vector2 scale, bool additive = false)
	{
		if (!CVars.i_useAdditiveShader)
		{
			additive = false;
		}
		if (additive)
		{
			Vector2 vector = new Vector2((float)picture.width * scale.x, (float)picture.height * scale.y);
			Graphics.DrawTexture(new Rect(pos.x - vector.x / 2f, pos.y - vector.y / 2f, vector.x, vector.y), picture, new Rect(0f, 0f, 1f, 1f), 0, 0, 0, 0, this.color / 2f, this.additive);
		}
		else
		{
			Vector2 vector2 = new Vector2((float)picture.width * scale.x, (float)picture.height * scale.y);
			GUI.DrawTexture(new Rect(pos.x - vector2.x / 2f, pos.y - vector2.y / 2f, vector2.x, vector2.y), picture);
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0004E75C File Offset: 0x0004C95C
	public void RadarPicture(Vector2 pos, Texture picture, Vector2 scale)
	{
		Vector3[] array = new Vector3[]
		{
			new Vector3(pos.x, pos.y, CameraListener.Camera.nearClipPlane),
			new Vector3(pos.x, pos.y + (float)picture.height, CameraListener.Camera.nearClipPlane),
			new Vector3(pos.x + (float)picture.width, pos.y + (float)picture.height, CameraListener.Camera.nearClipPlane),
			new Vector3(pos.x + (float)picture.width, pos.y, CameraListener.Camera.nearClipPlane)
		};
		for (int i = 0; i < 4; i++)
		{
			array[i] = CameraListener.Camera.ViewportToWorldPoint(array[i]);
		}
		Mesh mesh = new Mesh();
		mesh.vertices = array;
		this.additive.SetPass(0);
		Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0004E890 File Offset: 0x0004CA90
	public void PictureScreenSpace(Vector2 pos, Texture2D picture, Vector2 scale)
	{
		Vector2 vector = new Vector2((float)picture.width * scale.x, (float)picture.height * scale.y);
		GUI.DrawTexture(new Rect(pos.x - vector.x / 2f, pos.y - vector.y / 2f, vector.x, vector.y), picture, ScaleMode.ScaleToFit, true);
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0004E908 File Offset: 0x0004CB08
	public bool HoverPicture(Vector2 pos, Texture2D idle, Texture2D hover)
	{
		bool result = false;
		if (this.gui.inRect(new Rect(pos.x, pos.y, (float)idle.width, (float)idle.height), this.upper, this.cursorPosition))
		{
			idle = hover;
			result = true;
		}
		GUI.DrawTexture(new Rect(pos.x, pos.y, (float)idle.width, (float)idle.height), idle, ScaleMode.ScaleToFit, true);
		return result;
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x0004E984 File Offset: 0x0004CB84
	public void Picture(Vector2 pos, Texture picture, Vector2 scale)
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, (float)picture.width * scale.x, (float)picture.height * scale.y), picture, ScaleMode.StretchToFill, true);
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0004E9CC File Offset: 0x0004CBCC
	public void Picture(Vector2 pos, Texture picture)
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, (float)picture.width, (float)picture.height), picture, ScaleMode.StretchToFill, true);
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0004EA04 File Offset: 0x0004CC04
	public void PictureSized(Vector2 pos, Texture picture, Vector2 size)
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), picture, ScaleMode.StretchToFill, true);
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0004EA30 File Offset: 0x0004CC30
	public void PictureSizedNoBlend(Vector2 pos, Texture picture, Vector2 size, bool blend = false)
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), picture, ScaleMode.StretchToFill, blend);
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0004EA68 File Offset: 0x0004CC68
	public void PictureCentereNoScaleNoBlend(Vector2 pos, Texture2D picture, Vector2 scale)
	{
		Vector2 vector = new Vector2((float)picture.width * scale.x, (float)picture.height * scale.y);
		GUI.DrawTexture(new Rect(pos.x - vector.x / 2f, pos.y - vector.y / 2f, vector.x, vector.y), picture, ScaleMode.StretchToFill, false);
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0004EAE0 File Offset: 0x0004CCE0
	public void DrawCurve(string Name, Rect rect, List<Vector2> points, Texture2D tex)
	{
		if (points.Count == 0)
		{
			return;
		}
		this.gui.color = Colors.alpha(Color.white, 0.75f);
		this.gui.BeginGroup(new Rect(rect.x, rect.y, 8096f, 8096f));
		this.PictureSized(Vector2.zero, this.black, new Vector2(rect.width + 64f, rect.height + 64f));
		Vector2 vector = CVars.h_v3infinity;
		Vector2 lhs = CVars.h_v3infinity;
		if (points.Count > 1)
		{
			vector = points[0];
			lhs = points[0];
			for (int i = 0; i < points.Count; i++)
			{
				if (vector.x >= points[i].x)
				{
					vector.x = points[i].x;
				}
				if (vector.y >= points[i].y)
				{
					vector.y = points[i].y;
				}
				if (lhs.x <= points[i].x)
				{
					lhs.x = points[i].x;
				}
				if (lhs.y <= points[i].y)
				{
					lhs.y = points[i].y;
				}
			}
			if (vector.y == lhs.y)
			{
				vector.y -= 50f;
				lhs.y += 50f;
			}
			if (vector.x == lhs.x)
			{
				vector.x -= 50f;
				lhs.x += 50f;
			}
			for (int j = 0; j < points.Count - 1; j++)
			{
				Vector2 vector2 = points[j] - vector;
				vector2.x = vector2.x / Mathf.Abs(vector.x - lhs.x) * rect.width;
				vector2.y = vector2.y / Mathf.Abs(vector.y - lhs.y) * rect.height;
				vector2.y = rect.height - vector2.y;
				Vector2 vector3 = points[j + 1] - vector;
				vector3.x = vector3.x / Mathf.Abs(vector.x - lhs.x) * rect.width;
				vector3.y = vector3.y / Mathf.Abs(vector.y - lhs.y) * rect.height;
				vector3.y = rect.height - vector3.y;
				vector2 += new Vector2(32f, 32f);
				vector3 += new Vector2(32f, 32f);
				Utility.DrawLine(vector2, vector3, tex, 2);
				if (j != 0 && points[j].y != points[j - 1].y)
				{
					this.TextLabel(new Rect(vector2.x - 64f, vector2.y - 64f, 128f, 128f), points[j].y.ToString(), 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
				}
				if (j == points.Count - 2)
				{
					this.TextLabel(new Rect(vector3.x - 64f, vector3.y - 64f, 128f, 128f), points[j + 1].y.ToString(), 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
				}
			}
		}
		if (vector == CVars.h_v2infinity)
		{
			vector.y = -50f;
			lhs.y = 50f;
		}
		if (lhs == CVars.h_v2infinity)
		{
			vector.x = -50f;
			lhs.x = 50f;
		}
		this.TextLabel(new Rect(-48f, -32f, 128f, 128f), lhs.y.ToString(), 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
		this.TextLabel(new Rect(-48f, rect.height - 32f, 128f, 128f), vector.y.ToString(), 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
		this.TextLabel(new Rect(-16f, rect.height - 32f + 16f, 128f, 128f), vector.x.ToString(), 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
		this.TextLabel(new Rect(-32f + rect.width, rect.height - 32f + 16f, 128f, 128f), lhs.x.ToString(), 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
		this.gui.EndGroup();
		this.TextLabel(new Rect(rect.x - 32f + rect.width / 2f, rect.y - 32f - 16f, 128f, 128f), Name, 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0004F0BC File Offset: 0x0004D2BC
	public string TextFieldfloat(Rect rect, float i, int textSize = 16, string textColor = "#FFFFFF", TextAnchor alignment = TextAnchor.UpperLeft, bool editable = false, bool selectable = false)
	{
		return this.TextField(rect, i.ToString(), textSize, textColor, alignment, editable, selectable);
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0004F0E0 File Offset: 0x0004D2E0
	public string TextFieldFloat(Rect rect, Float i, int textSize = 16, string textColor = "#FFFFFF", TextAnchor alignment = TextAnchor.UpperLeft, bool editable = false, bool selectable = false)
	{
		return this.TextField(rect, i.ToString(), textSize, textColor, alignment, editable, selectable);
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0004F104 File Offset: 0x0004D304
	public string TextFieldint(Rect rect, int i, int textSize = 16, string textColor = "#FFFFFF", TextAnchor alignment = TextAnchor.UpperLeft, bool editable = false, bool selectable = false)
	{
		return this.TextField(rect, i.ToString(), textSize, textColor, alignment, editable, selectable);
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0004F128 File Offset: 0x0004D328
	public string TextFieldInt(Rect rect, Int i, int textSize = 16, string textColor = "#FFFFFF", TextAnchor alignment = TextAnchor.UpperLeft, bool editable = false, bool selectable = false)
	{
		return this.TextField(rect, i.ToString(), textSize, textColor, alignment, editable, selectable);
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0004F14C File Offset: 0x0004D34C
	public string TextField(Rect rect, Int i, int textSize = 16, string textColor = "#FFFFFF", TextAnchor alignment = TextAnchor.UpperLeft, bool editable = false, bool selectable = false)
	{
		return this.TextField(rect, i.ToString(), textSize, textColor, alignment, editable, selectable);
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0004F170 File Offset: 0x0004D370
	public string TextField(Rect rect, string text, int textSize = 16, string textColor = "#FFFFFF", TextAnchor alignment = TextAnchor.UpperLeft, bool editable = false, bool selectable = false)
	{
		if (text.Length > 2 && text[0] == '[' && text[1] == '#' && text.Length > 9)
		{
			textColor = text.Substring(1, 7);
			text = text.Remove(0, 9);
		}
		textColor = textColor.Trim();
		if (textColor.Length < 7)
		{
			textColor = "#FFFFFF";
		}
		GUIStyle label = this.skin.label;
		label.fontSize = textSize;
		label.font = this.fontDNC57;
		if (textColor.Length > 7)
		{
			if (textColor[8] == 'D')
			{
				label.font = this.fontDNC57;
			}
			if (textColor[8] == 'T')
			{
				label.font = this.fontTahoma;
			}
			if (textColor[8] == 'M')
			{
				label.font = this.fontMicra;
			}
			if (textColor[8] == 'A')
			{
				label.font = null;
			}
		}
		label.alignment = alignment;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (Regex.IsMatch(textColor.Substring(1, 6), "^[0-9,A-F,a-f]+$"))
		{
			num = Convert.ToInt32(textColor.Substring(1, 2), 16);
			num2 = Convert.ToInt32(textColor.Substring(3, 2), 16);
			num3 = Convert.ToInt32(textColor.Substring(5, 2), 16);
		}
		label.normal.textColor = new Color((float)num / 256f, (float)num2 / 256f, (float)num3 / 256f);
		if (editable && !this.disabled)
		{
			if (!selectable)
			{
				Color selectionColor = this.skin.settings.selectionColor;
				this.skin.settings.selectionColor = new Color(0f, 0f, 0f, 0f);
				text = GUI.TextArea(rect, text, this.textMaxSize, label);
				this.skin.settings.selectionColor = selectionColor;
			}
			else
			{
				text = GUI.TextArea(rect, text, this.textMaxSize, label);
			}
		}
		if ((!editable && !selectable) || this.disabled)
		{
			GUI.Label(rect, text, label);
		}
		return text;
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0004F3A8 File Offset: 0x0004D5A8
	public string PasswordTextField(Rect scretRect, string text, int textSize = 16, TextAnchor alignment = TextAnchor.UpperLeft)
	{
		GUIStyle label = this.skin.label;
		label.fontSize = textSize;
		label.font = this.fontDNC57;
		label.alignment = alignment;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (Regex.IsMatch(this._passwordTextColor.Substring(1, 6), "^[0-9,A-F,a-f]+$"))
		{
			num = Convert.ToInt32(this._passwordTextColor.Substring(1, 2), 16);
			num2 = Convert.ToInt32(this._passwordTextColor.Substring(3, 2), 16);
			num3 = Convert.ToInt32(this._passwordTextColor.Substring(5, 2), 16);
		}
		label.normal.textColor = new Color((float)num / 256f, (float)num2 / 256f, (float)num3 / 256f);
		if (!this.disabled)
		{
			text = GUI.PasswordField(scretRect, text, '*', this.textMaxSize, label);
		}
		return text;
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0004F484 File Offset: 0x0004D684
	public void TextLabel(Rect rect, object content, int textSize = 16, string textColor = "#FFFFFF", TextAnchor alignment = TextAnchor.UpperLeft, bool wordWrap = true)
	{
		GUIStyle label = GUI.skin.label;
		label.fontSize = textSize;
		label.font = this.fontDNC57;
		if (textColor.Length < 7)
		{
			textColor = "#FFFFFF";
		}
		MainGUI.Settings settings = this.GetSettings(textColor);
		label.font = settings.Font;
		label.alignment = alignment;
		label.wordWrap = wordWrap;
		label.clipping = TextClipping.Overflow;
		label.normal.textColor = settings.Color;
		GUI.Label(rect, content.ToString(), label);
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0004F510 File Offset: 0x0004D710
	public void ProgressBar(Vector2 pos, float width, float proc, Texture2D texture, float bonusProc = 0f, bool invert = false, bool RedGreen = true)
	{
		if (proc > 1f)
		{
			proc = 1f;
		}
		if (proc < 0f)
		{
			proc = 0f;
		}
		GUI.DrawTexture(new Rect(pos.x, pos.y, width * proc, (float)texture.height), texture, ScaleMode.StretchToFill, true);
		if (bonusProc > 0f)
		{
			if (RedGreen)
			{
				if (invert)
				{
					texture = this.red;
				}
				else
				{
					texture = this.green;
				}
			}
			GUI.DrawTexture(new Rect(pos.x + width * proc, pos.y, Mathf.Min(width * bonusProc, width - width * proc), (float)texture.height), texture, ScaleMode.StretchToFill, true);
		}
		if (bonusProc < 0f)
		{
			if (RedGreen)
			{
				if (invert)
				{
					texture = this.red;
				}
				else
				{
					texture = this.green;
				}
			}
			if (width * proc < Mathf.Abs(width * bonusProc))
			{
				bonusProc = proc * Mathf.Sign(bonusProc);
			}
			GUI.DrawTexture(new Rect(pos.x + width * proc + width * bonusProc, pos.y, width * Mathf.Abs(bonusProc), (float)(texture.height - 1)), texture, ScaleMode.StretchToFill, true);
		}
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0004F650 File Offset: 0x0004D850
	private void TextFieldWeaponPopup(Rect rect, float modVal, float skillVal, float baseVal, bool invert = false, bool cap = false, bool shiftBase = false)
	{
		if (baseVal < 1f)
		{
			baseVal = 1f;
		}
		if (cap && baseVal > 100f)
		{
			baseVal = 100f;
		}
		modVal = Mathf.Round(modVal);
		skillVal = Mathf.Round(skillVal);
		baseVal = Mathf.Round(baseVal);
		float num = baseVal + modVal + skillVal;
		if (num <= 0f)
		{
			num = 1f;
		}
		this.TextLabel(new Rect(rect.x - (float)((!shiftBase) ? 20 : 13), rect.y, rect.width, rect.height), baseVal, 9, "#808080_Tahoma", TextAnchor.UpperCenter, true);
		if (modVal == 0f && skillVal == 0f)
		{
			this.TextLabel(rect, baseVal, 9, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, true);
			return;
		}
		if (!invert)
		{
			if (modVal > 0f && skillVal > 0f)
			{
				if (modVal > skillVal)
				{
					this.TextLabel(rect, num, 9, "#6daf07_Tahoma", TextAnchor.UpperCenter, true);
				}
				else
				{
					this.TextLabel(rect, num, 9, "#62aeea_Tahoma", TextAnchor.UpperCenter, true);
				}
			}
			else if (modVal > 0f && skillVal < 0f)
			{
				if (modVal + skillVal > 0f)
				{
					this.TextLabel(rect, num, 9, "#6daf07_Tahoma", TextAnchor.UpperCenter, true);
				}
				else
				{
					this.TextLabel(rect, num, 9, "#ad0505_Tahoma", TextAnchor.UpperCenter, true);
				}
			}
			else if (modVal < 0f && skillVal > 0f)
			{
				if (modVal + skillVal > 0f)
				{
					this.TextLabel(rect, num, 9, "#62aeea_Tahoma", TextAnchor.UpperCenter, true);
				}
				else
				{
					this.TextLabel(rect, num, 9, "#ad0505_Tahoma", TextAnchor.UpperCenter, true);
				}
			}
			else if ((modVal < 0f && skillVal == 0f) || (skillVal < 0f && modVal == 0f))
			{
				this.TextLabel(rect, num, 9, "#ad0505_Tahoma", TextAnchor.UpperCenter, true);
			}
			else if (modVal > 0f && skillVal == 0f)
			{
				this.TextLabel(rect, num, 9, "#6daf07_Tahoma", TextAnchor.UpperCenter, true);
			}
			else if (skillVal > 0f && modVal == 0f)
			{
				this.TextLabel(rect, num, 9, "#62aeea_Tahoma", TextAnchor.UpperCenter, true);
			}
		}
		else if (modVal < 0f && skillVal < 0f)
		{
			if (modVal < skillVal)
			{
				this.TextLabel(rect, num, 9, "#6daf07_Tahoma", TextAnchor.UpperCenter, true);
			}
			else
			{
				this.TextLabel(rect, num, 9, "#62aeea_Tahoma", TextAnchor.UpperCenter, true);
			}
		}
		else if (modVal < 0f && skillVal > 0f)
		{
			if (modVal + skillVal > 0f)
			{
				this.TextLabel(rect, num, 9, "#6daf07_Tahoma", TextAnchor.UpperCenter, true);
			}
			else
			{
				this.TextLabel(rect, num, 9, "#ad0505_Tahoma", TextAnchor.UpperCenter, true);
			}
		}
		else if (modVal > 0f && skillVal < 0f)
		{
			if (modVal + skillVal < 0f)
			{
				this.TextLabel(rect, num, 9, "#62aeea_Tahoma", TextAnchor.UpperCenter, true);
			}
			else
			{
				this.TextLabel(rect, num, 9, "#ad0505_Tahoma", TextAnchor.UpperCenter, true);
			}
		}
		else if ((modVal > 0f && skillVal == 0f) || (skillVal > 0f && modVal == 0f))
		{
			this.TextLabel(rect, num, 9, "#ad0505_Tahoma", TextAnchor.UpperCenter, true);
		}
		else if (modVal < 0f && skillVal == 0f)
		{
			this.TextLabel(rect, num, 9, "#6daf07_Tahoma", TextAnchor.UpperCenter, true);
		}
		else if (skillVal < 0f && modVal == 0f)
		{
			this.TextLabel(rect, num, 9, "#62aeea_Tahoma", TextAnchor.UpperCenter, true);
		}
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0004FA90 File Offset: 0x0004DC90
	private void ProgressBarLine(ref Rect rect, float xMin, float value, Texture2D texture)
	{
		if (rect.width < value)
		{
			value = rect.width;
		}
		if (rect.xMin + value < xMin)
		{
			value = -Mathf.Abs(rect.xMin - xMin);
		}
		this.PictureSized(new Vector2(rect.xMin, rect.yMin), texture, new Vector2(value, rect.height));
		rect.Set(rect.xMin + value, rect.yMin, rect.width - value, rect.height);
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0004FB18 File Offset: 0x0004DD18
	private void ProgressBarLineInvert(ref Rect rect, float value, Texture2D texture)
	{
		if (value < 0f)
		{
			this.gui.color = Colors.alpha(this.gui.color, this.weapHelpAlpha.visibility * 0.3f);
		}
		value = Mathf.Abs(value);
		if (rect.width < value)
		{
			value = rect.width;
		}
		this.PictureSized(new Vector2(rect.xMax, rect.yMin), texture, new Vector2(-value, rect.height));
		rect.Set(rect.xMin, rect.yMin, rect.width - value, rect.height);
		this.gui.color = Colors.alpha(this.gui.color, this.weapHelpAlpha.visibility);
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0004FBE4 File Offset: 0x0004DDE4
	public void ProgressBarWeaponParam(ref Rect rect, float BaseProc, float ModProcBonus, float SkillProcBonus, bool invert = false, float durability = 1f)
	{
		if (BaseProc > 100f)
		{
			int num = (int)BaseProc / 100;
			ModProcBonus /= (float)num;
		}
		float num2 = BaseProc * (1f - durability);
		float num3;
		if (ModProcBonus >= 0f && SkillProcBonus >= 0f)
		{
			num3 = rect.width * (num2 + BaseProc * durability + ModProcBonus + SkillProcBonus) / 100f;
		}
		else if (ModProcBonus >= 0f && SkillProcBonus < 0f)
		{
			num3 = rect.width * (num2 + BaseProc * durability + ModProcBonus) / 100f;
		}
		else if (ModProcBonus < 0f && SkillProcBonus > 0f)
		{
			num3 = rect.width * (num2 + BaseProc * durability + SkillProcBonus) / 100f;
		}
		else if (ModProcBonus < 0f && SkillProcBonus < 0f)
		{
			num3 = rect.width * (num2 + BaseProc * durability) / 100f;
		}
		else if (ModProcBonus == 0f && SkillProcBonus == 0f)
		{
			num3 = rect.width * (num2 + BaseProc * durability) / 100f;
		}
		else
		{
			num3 = rect.width * (num2 + BaseProc * durability) / 100f;
		}
		float width = rect.width;
		if (num3 < 0f)
		{
			num3 *= -1f;
		}
		if (num3 > rect.width)
		{
			num3 = rect.width;
		}
		rect.Set(rect.xMin, rect.yMin, num3, rect.height);
		if (durability < 0.999f)
		{
			this.ProgressBarLineInvert(ref rect, -width * num2 / 100f, this.red);
		}
		if (ModProcBonus < 0f)
		{
			if (!invert)
			{
				this.ProgressBarLineInvert(ref rect, width * ModProcBonus / 100f, this.red);
			}
			else
			{
				this.ProgressBarLineInvert(ref rect, width * ModProcBonus / 100f, this.green);
			}
		}
		else if (!invert)
		{
			this.ProgressBarLineInvert(ref rect, width * ModProcBonus / 100f, this.green);
		}
		else
		{
			this.ProgressBarLineInvert(ref rect, width * ModProcBonus / 100f, this.green);
		}
		this.ProgressBarLineInvert(ref rect, width * SkillProcBonus / 100f, this.blue_progressbar);
		this.ProgressBarLineInvert(ref rect, width * BaseProc * durability / 100f, this.char_progressbar);
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0004FE3C File Offset: 0x0004E03C
	public void ProgressDoubleTextured(Vector2 pos, float width, float proc, Texture2D bCenter, Texture2D bRight, Texture2D center, Texture2D right)
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, width - (float)right.width, (float)center.height), bCenter, ScaleMode.StretchToFill, true);
		GUI.DrawTexture(new Rect(pos.x + (width - (float)right.width), pos.y, (float)right.width, (float)right.height), bRight, ScaleMode.StretchToFill, true);
		GUI.DrawTexture(new Rect(pos.x, pos.y, width * proc * (width - 12f) / width, (float)center.height), center, ScaleMode.StretchToFill, true);
		if (proc != 0f)
		{
			GUI.DrawTexture(new Rect(pos.x + width * proc * (width - 12f) / width, pos.y, (float)right.width, (float)right.height), right, ScaleMode.StretchToFill, true);
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0004FF24 File Offset: 0x0004E124
	public void ProgressDualTextured(Vector2 pos, float width, float proc, Texture2D back, Texture2D fore)
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, width, (float)fore.height), back);
		if (proc != 0f)
		{
			GUI.DrawTexture(new Rect(pos.x, pos.y, width * proc, (float)fore.height), fore);
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0004FF84 File Offset: 0x0004E184
	public void ProgressDualTexturedTCMode(Vector2 pos, float width, float proc, Texture2D back, Texture2D fore, bool rightBar = true)
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, width, (float)fore.height), back);
		if (rightBar)
		{
			GUI.DrawTexture(new Rect(pos.x + 100f, pos.y, width * proc / 2f, (float)fore.height), fore);
		}
		else
		{
			GUI.DrawTexture(new Rect(pos.x + (100f - proc * 100f), pos.y, width * proc / 2f, (float)fore.height), fore);
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0005002C File Offset: 0x0004E22C
	public float FloatSlider(Vector2 pos, float value, float min, float max, bool showText = false, bool hide = false, bool showMax = false)
	{
		this.Picture(pos, this.settings_window[3]);
		if (showText)
		{
			this.TextFieldint(new Rect(pos.x - 60f, pos.y - 7f, 50f, 50f), (int)value, 15, "#ffffff", TextAnchor.UpperRight, false, false);
		}
		if (showMax)
		{
			this.TextFieldint(new Rect(pos.x + (float)this.settings_window[3].width + 10f, pos.y - 7f, 50f, 50f), (int)max, 15, "#ffffff", TextAnchor.UpperLeft, false, false);
		}
		if (hide)
		{
			return value;
		}
		if (this.disabled)
		{
			return value;
		}
		return GUI.HorizontalSlider(new Rect(pos.x - 5f, pos.y, (float)(this.settings_window[3].width + 10), (float)this.settings_window[3].height), value, min, max);
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00050138 File Offset: 0x0004E338
	public float FloatSlider0dot00(Vector2 pos, float value, float min, float max, bool showText = false)
	{
		this.Picture(pos, this.settings_window[3]);
		if (showText)
		{
			this.TextFieldfloat(new Rect(pos.x - 60f, pos.y - 7f, 50f, 50f), (float)((int)(value * 100f)) / 100f, 15, "#ffffff", TextAnchor.UpperRight, false, false);
		}
		return GUI.HorizontalSlider(new Rect(pos.x - 5f, pos.y, (float)(this.settings_window[3].width + 10), (float)this.settings_window[3].height), value, min, max);
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x000501E8 File Offset: 0x0004E3E8
	public void TextFieldBacgr(Rect textRect, string str, int fontsize, string colortext, TextAnchor alignment = TextAnchor.UpperCenter, float BackgrAlpha = 1f)
	{
		Font font = this.gui.fontDNC57;
		if (colortext.Length >= 8)
		{
			if (colortext[8] == 'T')
			{
				font = this.gui.fontTahoma;
			}
			if (colortext[8] == 'M')
			{
				font = this.gui.fontMicra;
			}
		}
		if (BackgrAlpha > 1f)
		{
			BackgrAlpha = 1f;
		}
		this.gui.color = Colors.alpha(this.gui.color, BackgrAlpha * 0.4f);
		this.gui.PictureSized(new Vector2((textRect.xMax + textRect.xMin - this.gui.CalcWidth(str, font, fontsize)) / 2f, textRect.yMin + 2f), this.gui.black, new Vector2(this.gui.CalcWidth(str, font, fontsize) + 2f, this.gui.CalcHeight(str, textRect.width, font, fontsize) - 5f));
		this.gui.color = Colors.alpha(this.gui.color, BackgrAlpha);
		this.gui.TextLabel(textRect, str, fontsize, colortext, alignment, true);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0005032C File Offset: 0x0004E52C
	public void CompositeText(ref Rect rect, string str, int fontsize, string colr, TextAnchor alignment = TextAnchor.UpperCenter, float BackgrAlpha = 2f)
	{
		if (BackgrAlpha > 1f)
		{
			BackgrAlpha = base.visibility;
		}
		float width = this.gui.CalcWidth(str, this.gui.fontDNC57, fontsize);
		rect.Set(rect.xMin, rect.yMin, width, rect.height);
		this.TextFieldBacgr(rect, str, fontsize, colr, alignment, BackgrAlpha);
		rect.Set(rect.xMax + 2f, rect.y, rect.width, rect.height);
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x000503B8 File Offset: 0x0004E5B8
	public void CompositeTextClean(ref Rect rect, string str, int fontsize, string colr, TextAnchor alignment = TextAnchor.UpperCenter)
	{
		float width = this.gui.CalcWidth(str, this.gui.fontDNC57, fontsize);
		rect.Set(rect.xMin, rect.yMin, width, rect.height);
		this.gui.TextLabel(rect, str, fontsize, colr, alignment, true);
		rect.Set(rect.xMax + 2f, rect.y, rect.width, rect.height);
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00050434 File Offset: 0x0004E634
	public void WorkWithFocus(MonoEvented[] forms)
	{
		for (int i = forms.Length - 1; i >= 0; i--)
		{
			if (!(forms[i] is SpectactorGUI))
			{
				if (forms[i] is Form && forms[i].isRendering && (forms[i] as Form).FocusVisible && !forms[i].isGameHandler)
				{
					this.FocusWindow(forms[i].WindowID);
					break;
				}
			}
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x000504B8 File Offset: 0x0004E6B8
	public void FocusWindow(int id)
	{
		this.focusedWindow = id;
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x000504C4 File Offset: 0x0004E6C4
	public void UnFocusWindow(int id)
	{
		if (this.focusedWindow == id)
		{
			this.focusedWindow = -1;
		}
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x000504DC File Offset: 0x0004E6DC
	public Rect Window(int id, Rect rect, GUI.WindowFunction func)
	{
		func(id);
		return rect;
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000504E8 File Offset: 0x0004E6E8
	public int FocusedWindow
	{
		get
		{
			return this.focusedWindow;
		}
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x000504F0 File Offset: 0x0004E6F0
	public void BeginGroup(Rect rect, bool isDisable)
	{
		this.disabled = isDisable;
		this.lastUpper.Add(new Vector2(rect.x, rect.y));
		this.upper += this.lastUpper[this.lastUpper.Count - 1];
		this.lastColors.Add(GUI.color);
		GUI.BeginGroup(rect);
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00050564 File Offset: 0x0004E764
	public void BeginGroup(Rect rect)
	{
		this.lastUpper.Add(new Vector2(rect.x, rect.y));
		this.upper += this.lastUpper[this.lastUpper.Count - 1];
		this.lastColors.Add(GUI.color);
		GUI.BeginGroup(rect);
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x000505D0 File Offset: 0x0004E7D0
	public void EndGroup()
	{
		GUI.EndGroup();
		this.upper -= this.lastUpper[this.lastUpper.Count - 1];
		this.lastUpper.RemoveAt(this.lastUpper.Count - 1);
		this.accumColor = this.lastColors[this.lastColors.Count - 1];
		this.lastColors.RemoveAt(this.lastColors.Count - 1);
		GUI.color = this.accumColor;
		this.accumColor = Color.white;
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00050670 File Offset: 0x0004E870
	public Vector2 BeginScrollView(Rect rect, Vector2 pos, Rect rect2, float maxY = 3.4028235E+38f)
	{
		this.lastUpper.Add(new Vector2(rect.x + pos.x, rect.y - pos.y));
		this.upper += this.lastUpper[this.lastUpper.Count - 1];
		if (this.disabled)
		{
			GUI.BeginScrollView(rect, pos, rect2);
		}
		else if (pos.y < 0f)
		{
			pos.y = 0f;
			GUI.BeginScrollView(rect, pos, rect2);
		}
		else if (pos.y > maxY)
		{
			pos.y = maxY;
			GUI.BeginScrollView(rect, pos, rect2);
		}
		else
		{
			pos = GUI.BeginScrollView(rect, pos, rect2);
			if (pos.y < 0f)
			{
				pos.y = 0f;
			}
			else if (pos.y > maxY)
			{
				pos.y = maxY;
			}
		}
		return pos;
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00050784 File Offset: 0x0004E984
	public void EndScrollView()
	{
		if (this.disabled)
		{
			GUI.EndGroup();
		}
		else
		{
			GUI.EndScrollView();
		}
		this.upper -= this.lastUpper[this.lastUpper.Count - 1];
		this.lastUpper.RemoveAt(this.lastUpper.Count - 1);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x000507EC File Offset: 0x0004E9EC
	public bool GetKeyDown(KeyCode code)
	{
		return Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode == code;
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00050828 File Offset: 0x0004EA28
	public bool GetKey(KeyCode code)
	{
		return Event.current.isKey && Event.current.keyCode == code;
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00050854 File Offset: 0x0004EA54
	public bool GetKeyUp(KeyCode code)
	{
		return Event.current.isKey && Event.current.type == EventType.KeyUp && Event.current.keyCode == code;
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00050890 File Offset: 0x0004EA90
	public Vector2 WorldToScreen(Vector3 pos)
	{
		Vector3 vector = CameraListener.Camera.WorldToScreenPoint(pos);
		vector.y = (float)Screen.height - vector.y;
		if (vector.z < 0f)
		{
			vector.x = (float)Screen.width - vector.x;
			vector.y = (float)Screen.width - vector.y;
		}
		if (vector.x > (float)Screen.width)
		{
			vector.x = (float)Screen.width;
		}
		if (vector.x < 0f)
		{
			vector.x = 0f;
		}
		if (vector.y > (float)Screen.height)
		{
			vector.y = (float)Screen.height;
		}
		if (vector.y < 0f)
		{
			vector.y = 0f;
		}
		return new Vector2(vector.x, vector.y);
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00050984 File Offset: 0x0004EB84
	public bool inRect(Rect rect, Vector2 upper, Vector2 mouseCursorRelative)
	{
		float num = rect.x + upper.x;
		float num2 = rect.y + upper.y;
		float num3 = num + rect.width;
		float num4 = num2 + rect.height;
		return mouseCursorRelative.x >= num && mouseCursorRelative.x <= num3 && mouseCursorRelative.y >= num2 && mouseCursorRelative.y <= num4;
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x060008CC RID: 2252 RVA: 0x000509FC File Offset: 0x0004EBFC
	// (set) Token: 0x060008CD RID: 2253 RVA: 0x00050A04 File Offset: 0x0004EC04
	public Color color
	{
		get
		{
			return this.accumColor;
		}
		set
		{
			GUI.color = this.accumColor * value;
		}
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00050A18 File Offset: 0x0004EC18
	public float CalcHeight(string text, float width, Font font, int fontSize)
	{
		GUI.skin.textArea.font = font;
		GUI.skin.textArea.fontSize = fontSize;
		this.cacheGUIContent.text = text;
		return GUI.skin.textArea.CalcHeight(this.cacheGUIContent, width);
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00050A68 File Offset: 0x0004EC68
	public float CalcWidth(string text, Font font, int fontSize)
	{
		if (string.IsNullOrEmpty(text))
		{
			return 0f;
		}
		if (text.Length > 9 && text[0] == '[' && text[1] == '#')
		{
			text = text.Remove(0, 9);
		}
		GUI.skin.textArea.font = font;
		GUI.skin.textArea.fontSize = fontSize;
		this.cacheGUIContent.text = text;
		float num;
		float result;
		GUI.skin.textArea.CalcMinMaxWidth(this.cacheGUIContent, out num, out result);
		return result;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00050B00 File Offset: 0x0004ED00
	public string FormatedUserName(string FirstName, string LastName)
	{
		string result = string.Empty;
		if (FirstName.Length != 0 && LastName.Length != 0)
		{
			result = FirstName.Substring(0, 1) + ". " + LastName;
		}
		else
		{
			result = FirstName + " " + LastName;
		}
		return result;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00050B50 File Offset: 0x0004ED50
	public void RotateGUI(float angle, Vector2 center)
	{
		if (angle == 0f)
		{
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
		}
		else
		{
			GUIUtility.RotateAroundPivot(angle, center);
		}
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00050B90 File Offset: 0x0004ED90
	public int Dropdown(Vector2 pos, string[] names, int index, ref Vector2 scrollPos)
	{
		if (index > -1)
		{
			if (this.Button(pos, this.mainMenuButtons[0], this.mainMenuButtons[1], this.mainMenuButtons[1], names[index], 17, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				index = -1;
			}
			this.Picture(new Vector2(pos.x + 165f, pos.y + 8f), this.settings_window[2]);
		}
		else if (index == -1)
		{
			bool flag = this.disabled;
			this.disabled = false;
			this.Picture(new Vector2(pos.x - 5f, pos.y), this.settings_window[4]);
			if (scrollPos.y > (float)(names.Length * (this.kitMenuButtons[0].height + 5) - 112))
			{
				scrollPos.y = (float)(names.Length * (this.kitMenuButtons[0].height + 5) - 112);
			}
			if (names.Length > 4)
			{
				this.Picture(new Vector2(pos.x + 200f, pos.y + (float)this.kitMenuButtons[0].height + 3f - 24f), this.settings_window[7]);
				scrollPos = this.BeginScrollView(new Rect(pos.x - 5f, pos.y + 6f, (float)(this.settings_window[4].width - 5), (float)(this.settings_window[4].height - 11)), scrollPos * 1.25f, new Rect(0f, 0f, (float)(this.settings_window[4].width - 25), (float)(names.Length * (this.kitMenuButtons[0].height + 5) + 15)), float.MaxValue);
			}
			else
			{
				this.BeginGroup(new Rect(pos.x - 5f, pos.y, (float)(this.settings_window[4].width - 5), (float)(this.settings_window[4].height - 8)));
			}
			for (int i = 0; i < names.Length; i++)
			{
				if (this.Button(new Vector2(4f, (float)(0 + (this.kitMenuButtons[0].height + 2) * i + 3)), null, this.kitMenuButtons[1], this.kitMenuButtons[2], names[i], 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					index = i;
				}
			}
			if (names.Length > 4)
			{
				this.EndScrollView();
			}
			else
			{
				this.EndGroup();
			}
			if (!this.gui.inRect(new Rect(pos.x - 5f, pos.y, (float)(this.settings_window[4].width - 5), (float)(this.settings_window[4].height - 8)), this.upper, this.cursorPosition) && index == -1 && Input.GetMouseButtonDown(0))
			{
				index = 0;
			}
			this.disabled = flag;
		}
		return index;
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x00050ED4 File Offset: 0x0004F0D4
	public void SecondsToStringHMS(int seconds, ref string str)
	{
		if (str.Length < 9)
		{
			str = "000:00:00";
		}
		int num = (int)((float)seconds / 3600f);
		int num2 = (int)((float)(seconds - num * 3600) / 60f);
		int num3 = seconds - num * 3600 - num2 * 60;
		str = string.Format("{0}:{1}:{2}", num, num2, num3);
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00050F40 File Offset: 0x0004F140
	public string SecondsTostringDDHHMMSS(int seconds)
	{
		int num = seconds / 86400;
		seconds %= 86400;
		int num2 = seconds / 3600;
		seconds %= 3600;
		int num3 = seconds / 60;
		seconds %= 60;
		return string.Concat(new string[]
		{
			(num <= 0) ? string.Empty : (num + Language.D + " "),
			num2.ToString("D2"),
			Language.H,
			" ",
			num3.ToString("D2"),
			Language.M,
			" ",
			seconds.ToString("D2"),
			Language.S
		});
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00051004 File Offset: 0x0004F204
	public string SecondsToStringHHHMMSS(int seconds)
	{
		int num = seconds / 3600;
		seconds %= 3600;
		int num2 = seconds / 60;
		seconds %= 60;
		return string.Concat(new string[]
		{
			num.ToString("D2"),
			":",
			num2.ToString("D2"),
			":",
			seconds.ToString("D2")
		});
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00051078 File Offset: 0x0004F278
	public string SecondsToStringMS(int seconds)
	{
		int num = seconds / 60;
		seconds %= 60;
		return num.ToString("D2") + ":" + seconds.ToString("D2");
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x000510B4 File Offset: 0x0004F2B4
	public string SecondsToStringMSS(int seconds)
	{
		int num = seconds / 60;
		seconds %= 60;
		return num.ToString("D1") + ":" + seconds.ToString("D2");
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x000510F0 File Offset: 0x0004F2F0
	public bool VoteWidget(Vector2 pos, int vote_for, Float repa, int posIdx = -1)
	{
		Rect rect = new Rect(pos.x + 60f, pos.y + 2f, 23f, 23f);
		GUIStyle guistyle = new GUIStyle();
		guistyle.normal.background = this._carrierGui.ratingRecord[8];
		guistyle.hover.background = this._carrierGui.ratingRecord[9];
		bool result = false;
		bool flag = false;
		this.gui.Picture(new Vector2(pos.x, pos.y), this._carrierGui.ratingRecord[4]);
		this.gui.TextLabel(new Rect(pos.x - 40f, pos.y + 2f, 100f, 23f), Helpers.KFormat((int)repa.Value), 16, "#ffffff", TextAnchor.MiddleRight, true);
		Rect onHoverRect = new Rect(pos.x + 24f, pos.y, 36f, 18f);
		if (repa.Value >= 1000f && onHoverRect.Contains(Event.current.mousePosition))
		{
			float yOffset = (float)((posIdx != 0) ? 2 : 38);
			Helpers.Hint(onHoverRect, Helpers.SeparateNumericString(repa.Value.ToString("F0")), CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, yOffset);
		}
		if (vote_for != Main.UserInfo.userID && Main.UserInfo.votes > 0)
		{
			for (int i = 0; i < Main.UserInfo.voteInfo.Length; i++)
			{
				if (Main.UserInfo.voteInfo[i] == vote_for)
				{
					flag = true;
				}
			}
			if (!flag && !Vote.isVoting)
			{
				if (GUI.Button(rect, string.Empty, guistyle))
				{
					result = true;
					global::Console.print(string.Concat(new object[]
					{
						"Voting positive for ",
						vote_for,
						"posIdx=",
						posIdx
					}));
					Main.AddDatabaseRequest<Vote>(new object[]
					{
						repa,
						vote_for,
						1,
						posIdx
					});
					Vote.repaToChange = repa;
				}
				if (rect.Contains(Event.current.mousePosition))
				{
					float yOffset = (float)((posIdx != 0) ? 0 : 37);
					Helpers.Hint(rect, Language.Votes + Main.UserInfo.votes, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, yOffset);
				}
			}
		}
		return result;
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x060008D9 RID: 2265 RVA: 0x000513AC File Offset: 0x0004F5AC
	public Vector2 cursorPosition
	{
		get
		{
			return new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x060008DA RID: 2266 RVA: 0x000513E0 File Offset: 0x0004F5E0
	public Vector2 cursorPositionPrev
	{
		get
		{
			return new Vector2(this.prevMousePos.x, (float)Screen.height - this.prevMousePos.y);
		}
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00051410 File Offset: 0x0004F610
	public void ProcessingIndicator(Vector2 center)
	{
		Rect position = new Rect(center.x - (float)this.KrutilkaSmall.width * 0.5f, center.y - (float)this.KrutilkaSmall.height * 0.5f, (float)this.KrutilkaSmall.width, (float)this.KrutilkaSmall.height);
		GUIUtility.RotateAroundPivot(180f * Time.time, position.center);
		GUI.DrawTexture(position, this.KrutilkaSmall);
		GUIUtility.RotateAroundPivot(-180f * Time.time, position.center);
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x000514AC File Offset: 0x0004F6AC
	private void ShowHint(string description, string reward, Texture2D texture, int x = 0, int y = 0)
	{
		Color color = this.color;
		float num = 0f;
		float num2 = 0f;
		if (description.Length > 0)
		{
			num = this.gui.CalcWidth(description, this.gui.fontTahoma, 9);
			num2 = this.gui.CalcHeight(description, num, this.gui.fontTahoma, 9);
			this.color = new Color(1f, 1f, 1f, 0.95f);
			this.gui.PictureSized(new Vector2((float)x, (float)(y + 2)), this.gui.black, new Vector2(num, (num2 - 3f) * 3f));
			this.color = color;
			this.gui.TextLabel(new Rect((float)x, (float)y, num, num2), description, 9, "#FFFFFF_Tahoma", TextAnchor.MiddleLeft, true);
		}
		if (reward.Length > 0)
		{
			reward = Helpers.SeparateNumericString(reward);
			float num3 = this.CalcWidth(Language.Reward + ": " + reward, this.fontTahoma, 9) + (float)texture.width;
			this.gui.TextLabel(new Rect((float)x + (num - num3) / 2f, (float)y + num2 + 5f, num, num2), Language.Reward + ": " + reward, 9, "#FFFFFF_Tahoma", TextAnchor.MiddleLeft, true);
			GUI.DrawTexture(new Rect((float)x + (num - num3) / 2f + this.CalcWidth(Language.Reward + ": " + reward, this.fontTahoma, 9) - 4f, (float)y + num2 + 2f, (float)texture.width, (float)texture.height), texture);
		}
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00051660 File Offset: 0x0004F860
	[Obfuscation(Exclude = true)]
	private void ShowMainGUI(object obj)
	{
		this.Show(0.5f, 0.15f);
		this.backgroundAlpha.Show(0.15f, 0f);
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00051688 File Offset: 0x0004F888
	[Obfuscation(Exclude = true)]
	private void HideMainGUI(object obj)
	{
		this.backgroundAlpha.Hide(1f, 0f);
		this.Hide(0.35f);
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x000516B8 File Offset: 0x0004F8B8
	private void ShowWeaponInfo()
	{
		if (this.newWeapState == null && !this.weapHelpAlpha.Hiding)
		{
			this.weapHelpAlpha.Hide(0.15f, 0f);
		}
		else
		{
			this.weapState = this.newWeapState;
		}
		if (this.disabled)
		{
			return;
		}
		if (this.weapState == null)
		{
			return;
		}
		if (this.disableSelection)
		{
			if (!this.onHoverWeaponInSet)
			{
				return;
			}
			if (this.currentSuit.primaryIndex == 127 && this.currentSuit.secondaryIndex == 127)
			{
				return;
			}
			if (this.currentSuit.primaryIndex != 127 && this.currentSuit.secondaryIndex != 127 && this.weapState != this.Info.weaponsStates[this.currentSuit.primaryIndex] && this.weapState != this.Info.weaponsStates[this.currentSuit.secondaryIndex])
			{
				return;
			}
			if (this.currentSuit.primaryIndex != 127 && this.currentSuit.secondaryIndex == 127 && this.weapState != this.Info.weaponsStates[this.currentSuit.primaryIndex])
			{
				return;
			}
			if (this.currentSuit.primaryIndex == 127 && this.currentSuit.secondaryIndex != 127 && this.weapState != this.Info.weaponsStates[this.currentSuit.secondaryIndex])
			{
				return;
			}
		}
		float num = ((float)this.weapState.CurrentWeapon.durability - this.weapState.repair_info) / (float)this.weapState.CurrentWeapon.durability;
		if (this.weapState.isUndestructable)
		{
			num = 1f;
		}
		if (this.wtask_mode_weapstate)
		{
			this.color = new Color(1f, 1f, 1f, this.weapHelpAlpha.visibility);
			if (this.weapHelpMousePos.x >= (float)(Screen.width / 2) && this.weapHelpMousePos.y >= (float)(Screen.height / 2))
			{
				this.BeginGroup(new Rect(this.weapHelpMousePos.x - (float)this.wtask_popup.width, (float)Screen.height - this.weapHelpMousePos.y, (float)this.wtask_popup.width, (float)this.wtask_popup.height));
			}
			else if (this.weapHelpMousePos.x <= (float)(Screen.width / 2) && this.weapHelpMousePos.y >= (float)(Screen.height / 2))
			{
				this.BeginGroup(new Rect(this.weapHelpMousePos.x, (float)Screen.height - this.weapHelpMousePos.y, (float)this.wtask_popup.width, (float)this.wtask_popup.height));
			}
			else if (this.weapHelpMousePos.x >= (float)(Screen.width / 2) && this.weapHelpMousePos.y <= (float)(Screen.height / 2))
			{
				this.BeginGroup(new Rect(this.weapHelpMousePos.x - (float)this.wtask_popup.width, (float)Screen.height - this.weapHelpMousePos.y - (float)this.wtask_popup.height, (float)this.wtask_popup.width, (float)this.wtask_popup.height));
			}
			else
			{
				this.BeginGroup(new Rect(this.weapHelpMousePos.x, (float)Screen.height - this.weapHelpMousePos.y - (float)this.wtask_popup.height, (float)this.wtask_popup.width, (float)this.wtask_popup.height));
			}
			this.Picture(new Vector2(0f, 0f), this.wtask_popup);
			this.gui.TextLabel(new Rect(132f, 43f, 80f, 18f), Language.Accuracy, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(132f, 59f, 80f, 18f), Language.Impact, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(132f, 75f, 80f, 18f), Language.Mobility, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(132f, 91f, 80f, 18f), Language.Damage, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(132f, 106f, 80f, 18f), Language.Penetration, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.TextLabel(new Rect(10f, 13f, (float)this.weapon_info.width, 50f), this.weapState.CurrentWeapon.WtaskGearName, 16, "#000000", TextAnchor.UpperCenter, true);
			this.TextLabel(new Rect(10f, 12f, (float)this.weapon_info.width, 50f), this.weapState.CurrentWeapon.WtaskGearName, 16, "#FFFFFF", TextAnchor.UpperCenter, true);
			this.TextLabel(new Rect(110f, 130f, 210f, 190f), this.weapState.CurrentWeapon.WtaskGearDesc, 9, "#c1c1c1_Tahoma", TextAnchor.UpperLeft, true);
			this.TextLabel(new Rect(0f, 216f, (float)this.wtask_popup.width, 20f), Language.Objective + ":", 17, "#ffffff", TextAnchor.MiddleCenter, true);
			this.TextLabel(new Rect(0f, 240f, (float)this.wtask_popup.width, 20f), this.weapState.CurrentWeapon.WtaskDescription, 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
			this.Picture(new Vector2(18f, 126f), this.wtask_gear[this.weapState.CurrentWeapon.WtaskGearID]);
			float num2 = (!Utility.IsModableWeapon(this.weapState.CurrentWeapon.type)) ? this.weapState.CurrentWeapon.UiModAccuracy : this.weapState.CurrentWeapon.ModAccuracyProcBonus;
			if (Math.Abs(num2) > 0f)
			{
				this.TextLabel(new Rect(183f, 40f, 50f, 20f), num2.ToString("+#;-#;0"), 9, (num2 >= 0f) ? "#96ff00_Tahoma" : "#d60000_Tahoma", TextAnchor.MiddleRight, true);
			}
			else
			{
				this.TextLabel(new Rect(183f, 40f, 50f, 20f), "---", 9, "#989898_Tahoma", TextAnchor.MiddleRight, true);
			}
			num2 = ((!Utility.IsModableWeapon(this.weapState.CurrentWeapon.type)) ? this.weapState.CurrentWeapon.UiModRecoil : this.weapState.CurrentWeapon.ModRecoilProcBonus);
			if (Math.Abs(num2) > 0f)
			{
				this.TextLabel(new Rect(183f, 57f, 50f, 20f), num2.ToString("+#;-#;0"), 9, (num2 >= 0f) ? "#96ff00_Tahoma" : "#d60000_Tahoma", TextAnchor.MiddleRight, true);
			}
			else
			{
				this.TextLabel(new Rect(183f, 57f, 50f, 20f), "---", 9, "#989898_Tahoma", TextAnchor.MiddleRight, true);
			}
			num2 = ((!Utility.IsModableWeapon(this.weapState.CurrentWeapon.type)) ? this.weapState.CurrentWeapon.UiModMobility : this.weapState.CurrentWeapon.ModMobilityProcBonus);
			if (Math.Abs(num2) > 0f)
			{
				this.TextLabel(new Rect(183f, 73f, 50f, 20f), num2.ToString("+#;-#;0"), 9, (num2 >= 0f) ? "#96ff00_Tahoma" : "#d60000_Tahoma", TextAnchor.MiddleRight, true);
			}
			else
			{
				this.TextLabel(new Rect(183f, 73f, 50f, 20f), "---", 9, "#989898_Tahoma", TextAnchor.MiddleRight, true);
			}
			num2 = ((!Utility.IsModableWeapon(this.weapState.CurrentWeapon.type)) ? this.weapState.CurrentWeapon.UiModDamage : this.weapState.CurrentWeapon.ModDamageProcBonus);
			if (Math.Abs(num2) > 0f)
			{
				this.TextLabel(new Rect(183f, 89f, 50f, 20f), num2.ToString("+#;-#;0"), 9, (num2 >= 0f) ? "#96ff00_Tahoma" : "#d60000_Tahoma", TextAnchor.MiddleRight, true);
			}
			else
			{
				this.TextLabel(new Rect(183f, 89f, 50f, 20f), "---", 9, "#989898_Tahoma", TextAnchor.MiddleRight, true);
			}
			num2 = ((!Utility.IsModableWeapon(this.weapState.CurrentWeapon.type)) ? this.weapState.CurrentWeapon.UiModPenetration : this.weapState.CurrentWeapon.ModPierceProcBonus);
			if (Math.Abs(num2) > 0f)
			{
				this.TextLabel(new Rect(183f, 104f, 50f, 20f), num2.ToString("+#;-#;0"), 9, (num2 >= 0f) ? "#96ff00_Tahoma" : "#d60000_Tahoma", TextAnchor.MiddleRight, true);
			}
			else
			{
				this.TextLabel(new Rect(183f, 104f, 50f, 20f), "---", 9, "#989898_Tahoma", TextAnchor.MiddleRight, true);
			}
			if (this.weapState.wtaskUnlocked && this.weapState.Unlocked)
			{
				this.ProgressDoubleTextured(new Vector2(20f, 275f), 300f, 1f, this.wtask_icon[1], this.wtask_icon[2], this.wtask_icon[4], this.wtask_icon[5]);
				this.TextLabel(new Rect(1f, 270f, (float)this.wtask_popup.width, 20f), Language.Completed + "!", 14, "#000000_Micra", TextAnchor.MiddleCenter, true);
				this.TextLabel(new Rect(0f, 270f, (float)this.wtask_popup.width, 20f), Language.Completed + "!", 14, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
			}
			else
			{
				this.ProgressDoubleTextured(new Vector2(20f, 275f), 300f, this.weapState.wtaskProgress, this.wtask_icon[1], this.wtask_icon[2], this.wtask_icon[4], this.wtask_icon[5]);
				this.TextLabel(new Rect(0f, 271f, (float)this.wtask_popup.width, 20f), this.weapState.wtaskCurrent.ToString("F0") + "/" + this.weapState.CurrentWeapon.wtask.count, 14, "#000000_Micra", TextAnchor.MiddleCenter, true);
				this.TextLabel(new Rect(0f, 270f, (float)this.wtask_popup.width, 20f), this.weapState.wtaskCurrent.ToString("F0") + "/" + this.weapState.CurrentWeapon.wtask.count, 14, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
			}
			this.EndGroup();
			this.color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			this.color = new Color(1f, 1f, 1f, this.weapHelpAlpha.visibility);
			if (this.weapHelpMousePos.x >= (float)(Screen.width / 2) && this.weapHelpMousePos.y >= (float)(Screen.height / 2))
			{
				this.BeginGroup(new Rect(this.weapHelpMousePos.x - (float)this.weapon_info.width, (float)Screen.height - this.weapHelpMousePos.y, (float)this.weapon_info.width, (float)this.weapon_info.height));
			}
			else if (this.weapHelpMousePos.x < (float)(Screen.width / 2) && this.weapHelpMousePos.y > (float)(Screen.height / 2))
			{
				this.BeginGroup(new Rect(this.weapHelpMousePos.x, (float)Screen.height - this.weapHelpMousePos.y, (float)this.weapon_info.width, (float)this.weapon_info.height));
			}
			else if (this.weapHelpMousePos.x > (float)(Screen.width / 2) && this.weapHelpMousePos.y < (float)(Screen.height / 2))
			{
				this.BeginGroup(new Rect(this.weapHelpMousePos.x - (float)this.weapon_info.width, (float)Screen.height - this.weapHelpMousePos.y - (float)this.weapon_info.height, (float)this.weapon_info.width, (float)this.weapon_info.height));
			}
			else
			{
				this.BeginGroup(new Rect(this.weapHelpMousePos.x, (float)Screen.height - this.weapHelpMousePos.y - (float)this.weapon_info.height, (float)this.weapon_info.width, (float)this.weapon_info.height));
			}
			if (this.weapState.Unlocked && (this.Info.suits[this.Info.suitNameIndex].GetWtask((int)this.weapState.CurrentWeapon.type) || Utility.IsModableWeapon((int)this.weapState.CurrentWeapon.type)))
			{
				bool flag = Utility.IsModableWeapon((int)this.weapState.CurrentWeapon.type);
				this.Picture(new Vector2(0f, 0f), this.weapon_info);
				this.TextLabel(new Rect(25f, 12f, (float)this.weapon_info.width, 50f), (!flag) ? this.weapState.CurrentWeapon.WtaskName : this.weapState.CurrentWeapon.Name, 16, "#000000", TextAnchor.UpperLeft, true);
				this.TextLabel(new Rect(25f, 11f, (float)this.weapon_info.width, 50f), (!flag) ? this.weapState.CurrentWeapon.WtaskName : this.weapState.CurrentWeapon.Name, 16, "#FFFFFF", TextAnchor.UpperLeft, true);
				this.tmpRect.Set(115f, 49f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseAccuracyProc, this.weapState.CurrentWeapon.ModAccuracyProcBonus, this.weapState.CurrentWeapon.SkillAccuracyProcBonus, false, num);
				this.tmpRect.Set(115f, 65f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseRecoilProc, this.weapState.CurrentWeapon.ModRecoilProcBonus, this.weapState.CurrentWeapon.SkillRecoilProcBonus, true, 1f);
				this.tmpRect.Set(115f, 81f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseDamageProc, this.weapState.CurrentWeapon.ModDamageProcBonus, this.weapState.CurrentWeapon.SkillDamageProcBonus, false, 1f);
				this.tmpRect.Set(115f, 97f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseFirerateProc, this.weapState.CurrentWeapon.ModFirerateProcBonus, this.weapState.CurrentWeapon.SkillFirerateProcBonus, false, 1f);
				this.tmpRect.Set(115f, 113f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseMobilityProc, this.weapState.CurrentWeapon.ModMobilityProcBonus, this.weapState.CurrentWeapon.SkillMobilityProcBonus, false, 1f);
				this.tmpRect.Set(115f, 129f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseReloadSpeedProc, this.weapState.CurrentWeapon.ModReloadSpeedProcBonus, this.weapState.CurrentWeapon.SkillReloadSpeedProcBonus, false, 1f);
				this.tmpRect.Set(115f, 177f, 70f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BasePierceProc, this.weapState.CurrentWeapon.ModPierceProcBonus, this.weapState.CurrentWeapon.SkillPierceProcBonus, false, 1f);
				this.TextLabel(new Rect(20f, 205f, 220f, 190f), (!flag) ? this.weapState.CurrentWeapon.WtaskInfo : this.weapState.CurrentWeapon.Info, 9, "#c1c1c1_Tahoma", TextAnchor.UpperLeft, true);
				this.TextLabel(new Rect(245f, 260f, 80f, 30f), (!this.weapState.CurrentWeapon.isPremium || this.weapState.CurrentWeapon.forceEnableWtask) ? (this.weapState.wtaskProgress100 + "%") : Language.No, 20, "#62aeea", TextAnchor.UpperCenter, true);
				this.TextFieldWeaponPopup(new Rect(240f, 44f, 138f, 30f), this.weapState.CurrentWeapon.ModAccuracyProcBonus, this.weapState.CurrentWeapon.SkillAccuracyProcBonus * num, this.weapState.CurrentWeapon.BaseAccuracyProc * num, false, true, false);
				this.TextFieldWeaponPopup(new Rect(240f, 60f, 138f, 30f), this.weapState.CurrentWeapon.ModRecoilProcBonus, this.weapState.CurrentWeapon.SkillRecoilProcBonus, this.weapState.CurrentWeapon.BaseRecoilProc, true, false, false);
				this.TextFieldWeaponPopup(new Rect(240f, 76f, 138f, 30f), this.weapState.CurrentWeapon.ModDamageProcBonus, this.weapState.CurrentWeapon.SkillDamageProcBonus, this.weapState.CurrentWeapon.BaseDamageProc, false, false, false);
				this.TextFieldWeaponPopup(new Rect(240f, 92f, 138f, 30f), this.weapState.CurrentWeapon.ModFirerateProcBonus, this.weapState.CurrentWeapon.SkillFirerateProcBonus, this.weapState.CurrentWeapon.BaseFirerateProc, false, false, false);
				this.TextFieldWeaponPopup(new Rect(240f, 108f, 138f, 30f), this.weapState.CurrentWeapon.ModMobilityProcBonus, this.weapState.CurrentWeapon.SkillMobilityProcBonus, this.weapState.CurrentWeapon.BaseMobilityProc, false, false, false);
				this.TextFieldWeaponPopup(new Rect(240f, 124f, 138f, 30f), this.weapState.CurrentWeapon.ModReloadSpeedProcBonus, this.weapState.CurrentWeapon.SkillReloadSpeedProcBonus, this.weapState.CurrentWeapon.BaseReloadSpeedProc, false, false, false);
				this.TextFieldWeaponPopup(new Rect(137f, 172f, 138f, 30f), this.weapState.CurrentWeapon.ModPierceProcBonus, this.weapState.CurrentWeapon.SkillPierceProcBonus, this.weapState.CurrentWeapon.BasePierceProc, false, false, true);
				this.TextFieldfloat(new Rect(201f, 148f, 30f, 20f), Mathf.Round(this.weapState.CurrentWeapon.BaseDamageProc + this.weapState.CurrentWeapon.SkillDamageProcBonus + this.weapState.CurrentWeapon.ModDamageProcBonus), 8, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, false, false);
				this.TextFieldfloat(new Rect(201f, 166f, 30f, 20f), Mathf.Round(this.weapState.CurrentWeapon.distanceReducerE2 * (this.weapState.CurrentWeapon.BaseDamageProc + this.weapState.CurrentWeapon.SkillDamageProcBonus + this.weapState.CurrentWeapon.ModDamageProcBonus)), 8, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, false, false);
				this.TextFieldfloat(new Rect(227f, 180f, 30f, 20f), Mathf.Round(this.weapState.CurrentWeapon.DamageReduceDistanceMin), 8, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, false, false);
				this.TextFieldfloat(new Rect(263f, 180f, 30f, 20f), Mathf.Round(this.weapState.CurrentWeapon.DamageReduceDistanceMax), 8, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, false, false);
			}
			else
			{
				this.Picture(new Vector2(0f, 0f), this.weapon_info);
				this.TextLabel(new Rect(25f, 12f, (float)this.weapon_info.width, 50f), this.weapState.CurrentWeapon.Name, 16, "#000000", TextAnchor.UpperLeft, true);
				this.TextLabel(new Rect(25f, 11f, (float)this.weapon_info.width, 50f), this.weapState.CurrentWeapon.Name, 16, "#FFFFFF", TextAnchor.UpperLeft, true);
				this.tmpRect.Set(115f, 49f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseAccuracyProc, 0f, this.weapState.CurrentWeapon.SkillAccuracyProcBonus, false, num);
				this.tmpRect.Set(115f, 65f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseRecoilProc, 0f, this.weapState.CurrentWeapon.SkillRecoilProcBonus, true, 1f);
				this.tmpRect.Set(115f, 81f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseDamageProc, 0f, this.weapState.CurrentWeapon.SkillDamageProcBonus, false, 1f);
				this.tmpRect.Set(115f, 97f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseFirerateProc, 0f, this.weapState.CurrentWeapon.SkillFirerateProcBonus, false, 1f);
				this.tmpRect.Set(115f, 113f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseMobilityProc, 0f, this.weapState.CurrentWeapon.SkillMobilityProcBonus, false, 1f);
				this.tmpRect.Set(115f, 129f, 165f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BaseReloadSpeedProc, 0f, this.weapState.CurrentWeapon.SkillReloadSpeedProcBonus, false, 1f);
				this.tmpRect.Set(115f, 177f, 70f, 7f);
				this.ProgressBarWeaponParam(ref this.tmpRect, this.weapState.CurrentWeapon.BasePierceProc, 0f, this.weapState.CurrentWeapon.SkillPierceProcBonus, false, 1f);
				this.TextLabel(new Rect(20f, 205f, 220f, 190f), this.weapState.CurrentWeapon.Info, 9, "#c1c1c1_Tahoma", TextAnchor.UpperLeft, true);
				this.TextLabel(new Rect(245f, 260f, 80f, 30f), (!this.weapState.CurrentWeapon.isPremium || this.weapState.CurrentWeapon.forceEnableWtask) ? (this.weapState.wtaskProgress100 + "%") : Language.No, 20, "#62aeea", TextAnchor.UpperCenter, true);
				this.TextFieldWeaponPopup(new Rect(240f, 44f, 138f, 30f), 0f, this.weapState.CurrentWeapon.SkillAccuracyProcBonus, this.weapState.CurrentWeapon.BaseAccuracyProc * num, false, true, false);
				this.TextFieldWeaponPopup(new Rect(240f, 60f, 138f, 30f), 0f, this.weapState.CurrentWeapon.SkillRecoilProcBonus, this.weapState.CurrentWeapon.BaseRecoilProc, true, false, false);
				this.TextFieldWeaponPopup(new Rect(240f, 76f, 138f, 30f), 0f, this.weapState.CurrentWeapon.SkillDamageProcBonus, this.weapState.CurrentWeapon.BaseDamageProc, false, false, false);
				this.TextFieldWeaponPopup(new Rect(240f, 92f, 138f, 30f), 0f, this.weapState.CurrentWeapon.SkillFirerateProcBonus, this.weapState.CurrentWeapon.BaseFirerateProc, false, false, false);
				this.TextFieldWeaponPopup(new Rect(240f, 108f, 138f, 30f), 0f, this.weapState.CurrentWeapon.SkillMobilityProcBonus, this.weapState.CurrentWeapon.BaseMobilityProc, false, false, false);
				this.TextFieldWeaponPopup(new Rect(240f, 124f, 138f, 30f), 0f, this.weapState.CurrentWeapon.SkillReloadSpeedProcBonus, this.weapState.CurrentWeapon.BaseReloadSpeedProc, false, false, false);
				this.TextFieldWeaponPopup(new Rect(137f, 172f, 138f, 30f), 0f, this.weapState.CurrentWeapon.SkillPierceProcBonus, this.weapState.CurrentWeapon.BasePierceProc, false, false, true);
				this.TextFieldfloat(new Rect(201f, 148f, 30f, 20f), Mathf.Round(this.weapState.CurrentWeapon.BaseDamageProc + this.weapState.CurrentWeapon.SkillDamageProcBonus), 8, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, false, false);
				this.TextFieldfloat(new Rect(201f, 166f, 30f, 20f), Mathf.Round(this.weapState.CurrentWeapon.distanceReducerE2 * (this.weapState.CurrentWeapon.BaseDamageProc + this.weapState.CurrentWeapon.SkillDamageProcBonus)), 8, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, false, false);
				this.TextFieldfloat(new Rect(227f, 180f, 30f, 20f), Mathf.Round(this.weapState.CurrentWeapon.DamageReduceDistanceMin), 8, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, false, false);
				this.TextFieldfloat(new Rect(263f, 180f, 30f, 20f), Mathf.Round(this.weapState.CurrentWeapon.DamageReduceDistanceMax), 8, "#c1c1c1_Tahoma", TextAnchor.UpperCenter, false, false);
			}
			int num3 = this.weapState.CurrentWeapon.bagSize / this.weapState.CurrentWeapon.magSize + 1;
			this.TextLabel(new Rect(113f, 140f, 200f, 20f), string.Concat(new object[]
			{
				num3,
				Language.magazPo,
				this.weapState.CurrentWeapon.magSize,
				Language.patr
			}), 9, "#ffffff_Tahoma", TextAnchor.UpperLeft, true);
			this.TextLabel(new Rect(39f, 44f, 85f, 18f), Language.Accuracy, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.TextLabel(new Rect(39f, 60f, 85f, 18f), Language.Impact, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(39f, 76f, 85f, 18f), Language.Damage, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(39f, 92f, 85f, 18f), Language.FireRate, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(39f, 108f, 85f, 18f), Language.Mobility, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(39f, 124f, 85f, 18f), Language.ReloadRate, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(39f, 140f, 85f, 18f), Language.Ammunition, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(39f, 155f, 85f, 18f), Language.Cartridge, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(39f, 171f, 85f, 18f), Language.Penetration, 10, "#ffffff_Tahoma", TextAnchor.MiddleLeft, true);
			if (!this.weapState.CurrentWeapon.isPremium)
			{
				if (this.weapState.repair_info > 0f)
				{
					this.Picture(new Vector2(97f, 46f), this.repair[7]);
				}
				this.Picture(new Vector2(306f, 18f), this.repair[7]);
				if (this.weapState.isUndestructable)
				{
					this.TextLabel(new Rect(252f, 14f, 50f, 20f), "∞/∞ \u00a0", 14, "#999999", TextAnchor.MiddleRight, true);
				}
				else
				{
					string content = this.weapState.CurrentWeapon.durability - (int)this.weapState.repair_info + "/" + this.weapState.CurrentWeapon.durability;
					this.TextLabel(new Rect(252f, 14f, 50f, 20f), content, 14, "#999999", TextAnchor.MiddleRight, true);
				}
				this.TextLabel(new Rect(113f, 156f, 138f, 30f), this.weapState.CurrentWeapon.StringAmmo, 9, "#ffffff_Tahoma", TextAnchor.UpperLeft, true);
			}
			this.TextLabel(new Rect(113f, 156f, 138f, 30f), this.weapState.CurrentWeapon.StringAmmo, 9, "#ffffff_Tahoma", TextAnchor.UpperLeft, true);
			this.EndGroup();
			this.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0005380C File Offset: 0x00051A0C
	private bool LeagueAvailableByLevel
	{
		get
		{
			return Main.UserInfo.Permission >= EPermission.Admin || (CVars.LeagueLevel >= 0 && Main.UserInfo.PlayerLevel >= CVars.LeagueLevel);
		}
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00053850 File Offset: 0x00051A50
	private bool MainGUIMainButtons()
	{
		this.BeginGroup(new Rect(-9f, -10f, 800f, 600f));
		Vector2 vector = new Vector2(28f, 62f);
		if (Main.IsGameLoaded)
		{
			if (this.IncrementedHeightButton(ref vector, this.mainMenuButtons[0], this.mainMenuButtons[1], null, Language.ReturnToTheGame, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked)
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.DownloadAdditionalGameData, Language.CWMainLoading, Language.DownloadAdditionalGameDataDesc, PopupState.progress, false, true, string.Empty, "AdditionalWeapon"));
				Loader.DownloadAdditionalGameData(this.currentSuit, false);
			}
		}
		else
		{
			this.Picture(new Vector2(35f, 69f), this.fastgame);
			if (this.IncrementedHeightButton(ref vector, this.mainMenuButtons[11], this.mainMenuButtons[12], null, Language.QuickPlay, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked)
			{
				this._alreadyQuit = false;
				if (Application.isEditor && !Input.GetKey(KeyCode.RightShift))
				{
					Peer.Info = new HostInfo
					{
						MinLevel = 0,
						MaxLevel = 70
					};
					Main.HostInfo.MapIndex = (int)CVars.fastmap;
					Main.HostInfo.MapName = Globals.I.maps[Main.HostInfo.MapIndex].Name;
					Peer.Info.GameMode = Globals.I.maps[Main.HostInfo.MapIndex].Modes[0];
					if (Input.GetKey(KeyCode.LeftControl) && Globals.I.maps[Main.HostInfo.MapIndex].Modes.Contains(GameMode.TeamElimination))
					{
						Peer.Info.GameMode = GameMode.TeamElimination;
					}
					if (Input.GetKey(KeyCode.LeftShift) && Globals.I.maps[Main.HostInfo.MapIndex].Modes.Contains(GameMode.TargetDesignation))
					{
						Peer.Info.GameMode = GameMode.TargetDesignation;
					}
					if (Input.GetKey(KeyCode.RightControl) && Globals.I.maps[Main.HostInfo.MapIndex].Modes.Contains(GameMode.TacticalConquest))
					{
						Peer.Info.GameMode = GameMode.TacticalConquest;
					}
					Main.HostInfo.Name = "Developer Test Server " + (int)(UnityEngine.Random.value * 13f);
					Peer.Info.ForceNAT = true;
					Peer.IsHost = true;
					Peer.CreateServer();
				}
				else
				{
					if (Peer.games.Count == 0 || this.lastTimeServerListUpdate < Time.realtimeSinceStartup)
					{
						this.lastTimeServerListUpdate = Time.realtimeSinceStartup + 10f;
						Peer.ForceUpdateServers();
					}
					EventFactory.Call("ShowPopup", new Popup(WindowsID.QuickGameGUI, Language.QuickPlay, string.Empty, PopupState.quickGame, false, true, string.Empty, string.Empty));
				}
			}
			if (this.IncrementedHeightButton(ref vector, this.mainMenuButtons[0], this.mainMenuButtons[1], null, Language.SearchGames, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked)
			{
				this._alreadyQuit = false;
				EventFactory.Call("ShowSearchGames", null);
			}
		}
		if (this.IncrementedHeightButton(ref vector, this.mainMenuButtons[0], this.mainMenuButtons[1], null, Language.Settings, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked)
		{
			EventFactory.Call("ShowSettings", null);
		}
		if (this.IncrementedHeightButton(ref vector, this.mainMenuButtons[0], this.mainMenuButtons[1], null, Language.Career, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked)
		{
			EventFactory.Call("InitCarrier", Main.UserInfo);
			EventFactory.Call("ShowCarrier", null);
		}
		if (this.IncrementedHeightButton(ref vector, this.mainMenuButtons[0], this.mainMenuButtons[1], null, Language.Help, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked)
		{
			EventFactory.Call("ShowMainHelpGUI", null);
			this.tutor.tutorialAborted = false;
		}
		if (CVars.IsStandaloneRealm && CVars.LeagueLevel >= 0 && !Main.IsGameLoaded)
		{
			Texture2D texture2D = this.mainMenuButtons[0];
			Texture2D over = this.mainMenuButtons[1];
			if (!this.LeagueAvailableByLevel)
			{
				texture2D = this.mainMenuButtons[3];
				over = this.mainMenuButtons[3];
			}
			if (this.IncrementedHeightButton(ref vector, texture2D, over, null, Language.LeagueUpper, 15, (!this.LeagueAvailableByLevel) ? "#FF0000" : "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked && this.LeagueAvailableByLevel)
			{
				this._alreadyQuit = false;
				EventFactory.Call("ShowPopup", new Popup(WindowsID.LeagueNotification, Language.League, string.Empty, delegate()
				{
					if (!LeagueContentDownloader.I.LeagueContentLoaded)
					{
						Main.AddDatabaseRequestCallBack<LoadLeagueInfo>(delegate
						{
							LeagueContentDownloader.I.DownloadContent();
							EventFactory.Call("ShowPopup", new Popup(WindowsID.LeagueLoading, Language.League, Language.LeagueLoading, delegate()
							{
								this.League.Start();
							}, PopupState.leagueLoading, false, true, new object[0]));
						}, delegate
						{
						}, new object[0]);
					}
					else
					{
						this.League.Start();
					}
				}, PopupState.leagueNotification, false, true, new object[0]));
			}
			if (!this.LeagueAvailableByLevel)
			{
				GUI.DrawTexture(new Rect(150f, 180f, (float)this.locked.width, (float)this.locked.height), this.locked);
				string hint = string.Concat(new object[]
				{
					Language.LeagueAvailableHintStart,
					" ",
					CVars.LeagueLevel,
					" ",
					Language.LeagueAvailableHintEnd
				});
				Helpers.Hint(new Rect(28f, 182f, (float)texture2D.width, (float)texture2D.height), hint, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 25f, 40f);
			}
			else
			{
				GUI.DrawTexture(new Rect(75f, 188f, (float)this.Crown.width, (float)this.Crown.height), this.Crown);
			}
		}
		if (Main.IsGameLoaded && !this._alreadyQuit && this.IncrementedHeightButton(ref vector, this.mainMenuButtons[0], this.mainMenuButtons[1], null, Language.ExitFromTheServer, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked)
		{
			this._alreadyQuit = true;
			SingletoneForm<Peer>.Instance.Quit();
			Peer.ClientGame.LocalPlayer.Quit();
			return true;
		}
		if (this.IncrementedHeightButton(ref vector, this.mainMenuButtons[0], this.mainMenuButtons[1], null, Language.Exit, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null, 24).Clicked)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.id0, Language.Exit, string.Empty, PopupState.StandaloneQuit, false, true, string.Empty, string.Empty));
		}
		this.EndGroup();
		return false;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00053FC0 File Offset: 0x000521C0
	private void ShowTutorial()
	{
		for (int i = 0; i < PopupGUI.thisObject.popups.Count; i++)
		{
			if (PopupGUI.thisObject.popups[i].popupState != PopupState.quickGame)
			{
				return;
			}
		}
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, 22f), this.black);
		CWGUI.p.standartDNC5714.fontSize = 16;
		GUI.Label(new Rect(0f, 1f, this.CalcWidth(Language.TutorHeader1, this.fontDNC57, 16), 20f), Language.TutorHeader1, CWGUI.p.standartDNC5714);
		GUI.Label(new Rect((float)(Screen.width - 80), 1f, this.CalcWidth(Language.TutorHeader3, this.fontDNC57, 16), 20f), Language.TutorHeader3, CWGUI.p.standartDNC5714);
		CWGUI.p.standartDNC5714.normal.textColor = Color.gray;
		GUI.Label(new Rect(((float)Screen.width - this.CalcWidth(Language.TutorHeader2, this.fontDNC57, 16)) * 0.5f, 1f, this.CalcWidth(Language.TutorHeader2, this.fontDNC57, 16), 20f), Language.TutorHeader2, CWGUI.p.standartDNC5714);
		CWGUI.p.standartDNC5714.normal.textColor = Color.white;
		CWGUI.p.standartDNC5714.fontSize = 14;
		if (GUI.Button(new Rect((float)(Screen.width - 30), 0f, 30f, 22f), string.Empty, CWGUI.p.closeButtonSmall) || (this.tutor.HintCounter == 13 && PopupGUI.thisObject.Hiding))
		{
			this.showedTutor = true;
			this.tutorialComplete = false;
			this.tutor.tutorialAborted = true;
			HelpersGUI.I.Hide(0.35f);
		}
		this.BeginGroup(this.Rect);
		for (int j = 0; j < this.tutor.Hints.Count - 1; j++)
		{
			if ((int)this.tutor.HintCounter > this.tutor.Hints.Count)
			{
				break;
			}
			this.tutor.Show(this.tutor.Hints[(int)this.tutor.HintCounter]);
		}
		if ((int)this.tutor.HintCounter < this.tutor.Hints.Count && Input.GetMouseButtonDown(0))
		{
			if (this.tutor.BorderPosition[(int)this.tutor.HintCounter].Contains(Event.current.mousePosition))
			{
				Audio.Play(Vector3.zero, this.clickSoundPrefab, -1f, -1f);
				if (this.tutor.HintCounter == 5)
				{
					EventFactory.Call("ShowBuyPopup", new object[]
					{
						BuyTypes.GUN,
						this.Info.weaponsStates[3],
						false,
						true
					});
				}
				if (this.tutor.HintCounter == 10)
				{
					Main.AddDatabaseRequest<SaveProfile>(new object[0]);
					this.tutor.SelfClosing = true;
				}
				if (this.tutor.HintCounter == 12)
				{
					Peer.ForceUpdateServers();
					EventFactory.Call("ShowPopup", new Popup(WindowsID.QuickGameGUI, Language.QuickPlay, string.Empty, PopupState.quickGame, false, true, string.Empty, string.Empty));
				}
				if (this.tutor.HintCounter == 13)
				{
					this.showedTutor = true;
					this.tutorialComplete = true;
				}
			}
			if (this.tutor.HintCounter == 7)
			{
				if (this.tutor.BorderPosition[7].Contains(Event.current.mousePosition))
				{
					this.Info.suits[this.Info.suitNameIndex].primaryIndex = 15;
					Audio.Play(Vector3.zero, this.gunEquipClickSoundPrefab, -1f, -1f);
				}
				if (this.tutor.BorderPosition[5].Contains(Event.current.mousePosition))
				{
					this.Info.suits[this.Info.suitNameIndex].primaryIndex = 3;
					Audio.Play(Vector3.zero, this.gunEquipClickSoundPrefab, -1f, -1f);
				}
			}
		}
		if (this.tutor.HintCounter == 4)
		{
			this.tutor.SelfClosing = false;
			if ((this.tutor.BorderPosition[4].Contains(Event.current.mousePosition) && Input.GetMouseButton(0)) || Input.GetKeyDown(this.Info.settings.binds.fullscreen))
			{
				Utility.SetResolution(Main.UserInfo.settings.resolution.width, Main.UserInfo.settings.resolution.height, true);
				this.tutor.Hide();
			}
		}
		if (this.tutor.HintCounter == 8)
		{
			this.tutorialWtaskIcon = MainGUI.Instance.wtask_pp[0];
			if (this.tutor.BorderPosition[8].Contains(Event.current.mousePosition))
			{
				this.tutorialWtaskIcon = MainGUI.Instance.wtask_pp[1];
				if (Input.GetMouseButton(0))
				{
					this.Info.suits[this.Info.suitNameIndex].SetWtask(0, !this.Info.suits[this.Info.suitNameIndex].GetWtask(0));
					this.tutorialWtaskIcon = MainGUI.Instance.wtask_pp[2];
					Audio.Play(Vector3.zero, this.modEquipClickSoundPrefab, -1f, -1f);
				}
			}
		}
		if (this.tutor.HintCounter == 12)
		{
			this.tutor.SelfClosing = false;
		}
		if (this.tutorialWtaskIcon != null)
		{
			GUI.DrawTexture(new Rect(216f, 94f, (float)this.tutorialWtaskIcon.width, (float)this.tutorialWtaskIcon.height), this.tutorialWtaskIcon);
		}
		if (this.tutor.HintCounter == 7)
		{
			this.tutor.OnGUI(this.tutor.BorderPosition[(int)(this.tutor.HintCounter - 2)], TutorContainer.HintPosition.emptyHint);
		}
		if ((int)this.tutor.HintCounter < this.tutor.Hints.Count - 1)
		{
			this.tutor.OnGUI(this.tutor.BorderPosition[(int)this.tutor.HintCounter], this.tutor.Position[(int)this.tutor.HintCounter]);
		}
		this.EndGroup();
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00054700 File Offset: 0x00052900
	private void ShowHelpTutorial()
	{
		this.BeginGroup(this.Rect);
		this.tutor.Show(this.tutor.Hints[this.tutor.Hints.Count - 1]);
		this.tutor.OnGUI(this.tutor.BorderPosition[this.tutor.Hints.Count - 1], this.tutor.Position[this.tutor.Hints.Count - 1]);
		this.EndGroup();
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0005479C File Offset: 0x0005299C
	public override void OnRoundStart()
	{
		Loader.DownloadAdditionalGameData(this.currentSuit, true);
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x000547AC File Offset: 0x000529AC
	private void WeaponGUI(Vector2 pos, WeaponInfo state, int textureIndex, Texture2D[] weapon_button, Texture2D[] weapon_wtask_in, bool setEnabled = true, bool isChoosenWeapon = false)
	{
		if (!Main.UserInfo.weaponsStates[textureIndex].CurrentWeapon.isVisible && !Main.UserInfo.weaponsStates[textureIndex].Unlocked)
		{
			return;
		}
		this.BeginGroup(new Rect(pos.x, pos.y, (float)this.weapon_back[0].width, (float)this.weapon_back[0].height));
		Rect buttonRect;
		if (state.CurrentWeapon.weaponUseType == WeaponUseType.Primary)
		{
			buttonRect = new Rect(pos.x, pos.y, (float)this.weapon_back[0].width, (float)this.weapon_back[0].height);
		}
		else
		{
			buttonRect = new Rect(pos.x, pos.y, (float)this.weapon_back[5].width, (float)this.weapon_back[5].height);
		}
		this.DrawWeaponBackground(state);
		this.EndGroup();
		if ((!setEnabled && !state.Unlocked && textureIndex < this.weapon_locked.Length) || (this.wtask_mode_on && (state.CurrentWeapon.isPremium || state.CurrentWeapon.isDonate)))
		{
			Color color = this.color;
			this.color = Colors.alpha(Color.red, 1f * base.visibility);
			this.Picture(new Vector2(pos.x + (float)this.GetB_Xpos(state, this.weapon_locked[textureIndex]), pos.y + (float)this.GetB_Ypos(state, this.weapon_locked[textureIndex])), this.weapon_locked[textureIndex]);
			this.color = Colors.alpha(Color.red, base.visibility);
			this.color = color;
			return;
		}
		bool flag = state.repair_info == (float)state.CurrentWeapon.durability;
		if (!this.wtask_mode_on && this.weapon_wtask_inc.Length > textureIndex)
		{
			bool flag2 = this.gui.inRect(new Rect(pos.x, pos.y, (float)this.GetWeaponBackrWidth(state), (float)this.GetWeaponBackrHeight(state)), this.upper, this.cursorPosition);
			if (flag2)
			{
				this.globalHover = true;
				this.weapHelpAlpha.Show(0.3f, 0f);
				this.weapHelpMousePos = Input.mousePosition;
				this.wtask_mode_weapstate = false;
				this.newWeapState = state;
			}
			if (!this.wtask_mode_on && Globals.I.masteringTable.Length > 0 && state.Unlocked && state.GetWeapon.IsModable && (!Main.IsGameLoaded || !isChoosenWeapon))
			{
				this.DrawMasteringButton(state, buttonRect);
			}
			this.BeginGroup(new Rect(pos.x, pos.y, (float)this.GetWeaponBackrWidth(state), (float)this.weapon_back[0].height));
			ButtonState buttonState = default(ButtonState);
			if ((CVars.i_showWV && flag2 && !this.disableSelection && !this.disabled && this.weapon_unlocked.Length > textureIndex) || (!Main.IsGameLoaded && isChoosenWeapon))
			{
				bool flag3 = Time.time - this._timeSinceMasteringOpened >= 1f;
				if (state.CurrentWeapon.isPremium || state.CurrentWeapon.isSocial || state.CurrentWeapon.isDonate || state.CurrentWeapon.type == Weapons.pm)
				{
					buttonState = this.Button(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), 0f), this.WeaponView[0], this.WeaponView[1], this.WeaponView[0], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
				}
				else
				{
					buttonState = this.Button(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), (float)(4 + this.repair[0].height)), this.WeaponView[0], this.WeaponView[1], this.WeaponView[0], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
				}
				if (buttonState.Clicked && flag3)
				{
					this.IsWeaponViewerClicked = true;
					EventFactory.Call("ShowWeaponViewerGUI", new object[]
					{
						state.CurrentWeapon.type,
						this.currentSuit.GetWtask((int)state.CurrentWeapon.type)
					});
				}
			}
			ButtonState buttonState2 = default(ButtonState);
			if (state.Unlocked && !state.isUndestructable && !this.disabled && this.weapon_unlocked.Length > textureIndex && state.CurrentWeapon.skillReq < 0)
			{
				float num = ((float)state.CurrentWeapon.durability - state.repair_info) / (float)state.CurrentWeapon.durability;
				int num2 = (state.repair_info == 0f) ? 6 : 0;
				int num3 = (state.repair_info == 0f) ? 6 : 1;
				if ((flag2 && !this.disableSelection) || (isChoosenWeapon && !Main.IsGameLoaded))
				{
					buttonState2 = this.Button(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), 2f), this.repair[num2], this.repair[num3], this.repair[num3], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
					if (buttonState2.Clicked)
					{
						EventFactory.Call("ShowBuyPopup", new object[]
						{
							BuyTypes.PISTOL,
							state,
							true
						});
					}
					this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 65), 1f), (num <= 0.2f) ? this.repair[3] : this.repair[2]);
					this.ProgressBar(new Vector2((float)(this.GetWeaponBackrWidth(state) - 63), 3f), (float)(this.repair[2].width - 4), num, (num <= 0.2f) ? this.repair[5] : this.repair[4], 0f, false, true);
				}
				if (num < 0.2f && this.weapon_unlocked.Length > textureIndex)
				{
					this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 65), 1f), (num <= 0.2f) ? this.repair[3] : this.repair[2]);
					this.ProgressBar(new Vector2((float)(this.GetWeaponBackrWidth(state) - 63), 3f), (float)(this.repair[2].width - 4), num, (num <= 0.2f) ? this.repair[5] : this.repair[4], 0f, false, true);
				}
			}
			if (!this.disableSelection && !state.CurrentWeapon.IsModable && state.wtaskUnlocked && state.Unlocked && !(state.CurrentWeapon.isPremium ^ state.CurrentWeapon.forceEnableWtask) && Globals.I.weapons.Length > textureIndex)
			{
				AudioClip clickSound = this.modEquipClickSoundPrefab;
				if (this.Info.suits[this.Info.suitNameIndex].GetWtask(textureIndex))
				{
					if (this.Button(new Vector2(1f, (float)(this.GetWeaponBackrHeight(state) - weapon_wtask_in[0].height - 1)), weapon_wtask_in[2], weapon_wtask_in[2], weapon_wtask_in[2], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, clickSound).Clicked && state.Unlocked && !this.disableSelection)
					{
						this.Info.suits[this.Info.suitNameIndex].SetWtask(textureIndex, !this.Info.suits[this.Info.suitNameIndex].GetWtask(textureIndex));
					}
				}
				else if (this.Button(new Vector2(1f, (float)(this.GetWeaponBackrHeight(state) - weapon_wtask_in[0].height - 1)), weapon_wtask_in[0], (!state.Unlocked) ? weapon_wtask_in[0] : weapon_wtask_in[1], (!state.Unlocked) ? weapon_wtask_in[0] : weapon_wtask_in[2], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, clickSound).Clicked && state.Unlocked && !this.disableSelection)
				{
					this.Info.suits[this.Info.suitNameIndex].SetWtask(textureIndex, !this.Info.suits[this.Info.suitNameIndex].GetWtask(textureIndex));
				}
			}
			int num4 = (state.CurrentWeapon.weaponUseType != WeaponUseType.Primary) ? this.Info.suits[this.Info.suitNameIndex].secondaryIndex : this.Info.suits[this.Info.suitNameIndex].primaryIndex;
			if (!this.disableSelection)
			{
				if (textureIndex == num4)
				{
					state.buttonState = this.Button(new Vector2(0f, 0f), this.GetWeaponBTexture(state, 0), this.GetWeaponBHoverTexture(state, textureIndex), (!state.Unlocked) ? null : this.GetWeaponBTexture(state, 1), string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
					if (state.buttonState.Clicked && !this.disableSelection && state.CurrentWeapon.weaponUseType == WeaponUseType.Primary && this.Info.suits[this.Info.suitNameIndex].primaryIndex == textureIndex)
					{
						this.Info.suits[this.Info.suitNameIndex].primaryIndex = 127;
					}
				}
				else
				{
					AudioClip audioClip = this.pistolEquipClickSoundPrefab;
					if (weapon_button != this.pistol_button)
					{
						audioClip = this.gunEquipClickSoundPrefab;
					}
					state.buttonState = this.Button(new Vector2(0f, 0f), null, this.GetWeaponBHoverTexture(state, textureIndex), (!state.Unlocked) ? null : this.GetWeaponBTexture(state, 0), string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, (!state.Unlocked) ? null : ((!flag) ? audioClip : this.error_sound));
					if (state.buttonState.Clicked && state.Unlocked && !flag && !this.disableSelection)
					{
						if (state.CurrentWeapon.weaponUseType == WeaponUseType.Primary)
						{
							this.Info.suits[this.Info.suitNameIndex].primaryIndex = textureIndex;
						}
						else
						{
							this.Info.suits[this.Info.suitNameIndex].secondaryIndex = textureIndex;
						}
					}
				}
			}
			if (state.buttonState.Clicked && !state.Unlocked && !this.disableSelection && state.CurrentWeapon.skillReq < 0)
			{
				if (state.CurrentWeapon.isSocial)
				{
					EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.GatherTheTeam, string.Empty, PopupState.invite, false, true, string.Empty, string.Empty));
				}
				else if (!state.CurrentWeapon.isDonate)
				{
					BuyTypes buyTypes = (state.CurrentWeapon.weaponUseType != WeaponUseType.Primary) ? BuyTypes.PISTOL : BuyTypes.GUN;
					EventFactory.Call("ShowBuyPopup", new object[]
					{
						buyTypes,
						state,
						false
					});
				}
			}
			if (state.Unlocked)
			{
				if (flag)
				{
					this.BeginGroup(new Rect(0f, 0f, (float)weapon_button[0].width, (float)weapon_button[0].height));
					this.color = Color.red * base.visibility;
				}
				if (this.Info.suits[this.Info.suitNameIndex].GetWtask(textureIndex))
				{
					this.Picture(new Vector2((float)this.GetB_Xpos(state, this.weapon_wtask[textureIndex]), (float)this.GetB_Ypos(state, this.weapon_unlocked[textureIndex])), this.weapon_wtask[textureIndex]);
				}
				else
				{
					this.Picture(new Vector2((float)this.GetB_Xpos(state, this.weapon_unlocked[textureIndex]), (float)this.GetB_Ypos(state, this.weapon_unlocked[textureIndex])), this.weapon_unlocked[textureIndex]);
				}
				if (flag)
				{
					this.EndGroup();
				}
				if (!state.CurrentWeapon.isPremium && !state.CurrentWeapon.isSocial && !this.disabled && !state.isUndestructable)
				{
					if ((buttonState2.Hover && !this.disableSelection) || (isChoosenWeapon && !Main.IsGameLoaded))
					{
						this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), 2f), (!buttonState2.Hover) ? this.repair[0] : this.repair[1]);
					}
					if ((flag2 && !this.disableSelection) || isChoosenWeapon)
					{
						if (state.repair_info == 0f && !Main.IsGameLoaded)
						{
							this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), 2f), this.repair[6]);
						}
						float num5 = ((float)state.CurrentWeapon.durability - state.repair_info) / (float)state.CurrentWeapon.durability;
						this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 65), 1f), (num5 <= 0.2f) ? this.repair[3] : this.repair[2]);
						this.ProgressBar(new Vector2((float)(this.GetWeaponBackrWidth(state) - 63), 3f), (float)(this.repair[2].width - 4), num5, (num5 <= 0.2f) ? this.repair[5] : this.repair[4], 0f, false, true);
					}
				}
				if (state.repair_info == -77f && !this.disabled && (flag2 || isChoosenWeapon))
				{
					this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), 2f), this.repair[8]);
				}
				if (!this.disableSelection)
				{
					int num6 = state.rentEnd - HtmlLayer.serverUtc;
					if (num6 < 0 && state.rentEnd != -1)
					{
						state.Unlocked = false;
						if (this.Info.suits[this.Info.suitNameIndex].secondaryIndex == textureIndex)
						{
							if (state.CurrentWeapon.weaponUseType == WeaponUseType.Secondary)
							{
								this.Info.suits[this.Info.suitNameIndex].secondaryIndex = 127;
							}
							else
							{
								this.Info.suits[this.Info.suitNameIndex].primaryIndex = 127;
							}
						}
					}
					if (state.rentEnd != -1)
					{
						int num7;
						int num8;
						if (state.CurrentWeapon.weaponUseType == WeaponUseType.Secondary)
						{
							num7 = (this.GetWeaponBackrHeight(state) - (int)this.gui.CalcHeight("00:00:00", 70f, this.gui.fontDNC57, 14)) / 2;
							num8 = (this.GetWeaponBackrWidth(state) - (this._carrierGui.stats[1].width + (int)this.gui.CalcWidth("00:00:00", this.gui.fontDNC57, 14))) / 2;
						}
						else
						{
							num7 = (this.GetWeaponBackrHeight(state) - (int)this.gui.CalcHeight("00:00:00", 50f, this.gui.fontDNC57, 14)) / 2 + 20;
							num8 = 0;
							if (state.wtaskUnlocked)
							{
								num8 = 30;
							}
							if (Utility.IsModableWeapon((int)state.CurrentWeapon.type))
							{
								num8 = 0;
								num7 -= 25;
							}
						}
						if (state.CurrentWeapon.weaponUseType == WeaponUseType.Secondary)
						{
							this.Picture(new Vector2((float)(num8 + (int)this.gui.CalcWidth("00:00:00", this.gui.fontDNC57, 14)), (float)(num7 - 4)), this._carrierGui.stats[1]);
							this.TextLabel(new Rect((float)num8, (float)num7, 50f, 20f), this.SecondsToStringHHHMMSS(num6), 14, "#ffffff", TextAnchor.UpperRight, true);
						}
						else
						{
							this.Picture(new Vector2((float)(num8 - 4), (float)(num7 - 4)), this._carrierGui.stats[1]);
							this.TextLabel(new Rect((float)(num8 + this._carrierGui.stats[1].width - 10), (float)num7, 60f, 20f), this.SecondsToStringHHHMMSS(num6), 14, "#ffffff", TextAnchor.UpperLeft, true);
						}
					}
				}
			}
			else
			{
				if (this.weapon_locked.Length > textureIndex)
				{
					this.Picture(new Vector2((float)this.GetB_Xpos(state, this.weapon_locked[textureIndex]), (float)this.GetB_Ypos(state, this.weapon_locked[textureIndex])), this.weapon_locked[textureIndex]);
				}
				if (state.CurrentWeapon.skillReq < 0)
				{
					int num9 = weapon_button[0].width / 2 - 40;
					if (state.CurrentWeapon.isPremium)
					{
						float num10 = this.CalcWidth(state.CurrentWeapon.GetRentPrice(0) + "-" + state.CurrentWeapon.GetRentPrice(state.CurrentWeapon.rentPrice.Length - 1), this.fontDNC57, 16);
						this.TextLabel(new Rect(0f, -4f, (float)weapon_button[0].width, 60f), state.CurrentWeapon.GetRentPrice(0) + "-" + state.CurrentWeapon.GetRentPrice(state.CurrentWeapon.rentPrice.Length - 1), 16, "#ffa200", TextAnchor.MiddleCenter, true);
						this.Picture(new Vector2((num10 + (float)weapon_button[0].width) / 2f - 2f, 15f), this.gldIcon);
						this.Picture(new Vector2((float)(-1 + num9), 11f), this.locked);
					}
					else if (state.CurrentWeapon.isSocial && this.weapon_locked.Length > textureIndex)
					{
						this.BeginGroup(new Rect((float)(this.GetWeaponBackrWidth(state) / 3), -5f, 90f, (float)this.weapon_locked[textureIndex].height));
						this.Picture(new Vector2(-4f, 13f), this.locked);
						this.Picture(new Vector2(69f, 22f), this._carrierGui.ratingRecord[16]);
						this.TextLabel(new Rect(8f, -3f, 30f, 60f), (Main.UserInfo.friendsAdded >= 10) ? Main.UserInfo.friendsAdded.ToString() : ("0" + Main.UserInfo.friendsAdded.ToString()), 24, "#d40000", TextAnchor.MiddleRight, true);
						this.TextLabel(new Rect(39f, 0f, 30f, 60f), "/" + state.CurrentWeapon.friendsRequired, 16, "#ffffff", TextAnchor.MiddleLeft, true);
						this.TextLabel(new Rect(0f, 15f, 90f, 60f), Language.FriendsInvited, 12, "#999999", TextAnchor.MiddleLeft, true);
						this.EndGroup();
					}
					else if (state.CurrentWeapon.isDonate)
					{
						this.Picture(new Vector2(-3f, 39f), this.locked);
					}
					else
					{
						this.Picture(new Vector2((float)(10 + num9), 10f), this.locked);
						int num11 = state.CurrentWeapon.price;
						if (Main.UserInfo.skillUnlocked(Skills.car_weap))
						{
							num11 = (int)Mathf.Round((float)num11 * 0.8f);
						}
						this.TextLabel(new Rect((float)(29 + num9), 0f, 50f, 50f), num11, 16, (!(this.Info.CR < num11)) ? "#FFFFFF" : "#d40000", TextAnchor.MiddleLeft, true);
					}
				}
				if (Main.UserInfo.discount_id == textureIndex)
				{
					this.Picture(new Vector2(-8f, 37f), this.discountbar);
					this.TextLabel(new Rect(2f, 42f, 100f, 20f), Language.discount, 16, "#c3751a", TextAnchor.MiddleLeft, true);
					this.TextLabel(new Rect(-18f, 42f, 100f, 20f), Main.UserInfo.discount.ToString() + "%", 16, "#c3751a", TextAnchor.MiddleRight, true);
				}
			}
			if (CVars.i_showWV && flag2 && !this.disabled && !this.disableSelection && this.weapon_unlocked.Length > textureIndex && state.CurrentWeapon.skillReq < 0)
			{
				if (!state.CurrentWeapon.isPremium && !state.CurrentWeapon.isSocial && !state.CurrentWeapon.isDonate && state.CurrentWeapon.type != Weapons.pm)
				{
					this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), (float)(4 + this.repair[0].height)), (!buttonState.Hover) ? this.WeaponView[0] : this.WeaponView[1]);
				}
				else
				{
					this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), 0f), (!buttonState.Hover) ? this.WeaponView[0] : this.WeaponView[1]);
				}
			}
			if (CVars.i_showWV && flag2 && !state.isUndestructable && !this.disabled && !this.disableSelection && state.repair_info != 0f && this.weapon_locked.Length > textureIndex)
			{
				this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 17), 2f), (!buttonState2.Hover) ? this.repair[0] : this.repair[1]);
			}
			this.EndGroup();
		}
		else if (this.weapon_wtask_inc.Length > textureIndex)
		{
			bool flag4 = this.gui.inRect(new Rect(pos.x, pos.y, (float)this.GetWeaponBackrWidth(state), (float)this.GetWeaponBackrHeight(state)), this.upper, this.cursorPosition);
			if (flag4)
			{
				this.weapHelpAlpha.Show(0.3f, 0f);
				this.weapHelpMousePos = Input.mousePosition;
				this.wtask_mode_weapstate = true;
				this.newWeapState = state;
			}
			state.buttonState = this.Button(pos, weapon_button[2], weapon_button[0], weapon_button[1], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			this.BeginGroup(new Rect(pos.x, pos.y, (float)this.GetWeaponBackrWidth(state), (float)this.GetWeaponBackrHeight(state)));
			if (this.weapon_wtask.Length > textureIndex)
			{
				if (state.wtaskUnlocked)
				{
					this.Picture(new Vector2((float)this.GetB_Xpos(state, this.weapon_wtask[textureIndex]), (float)this.GetB_Ypos(state, this.weapon_wtask[textureIndex])), this.weapon_wtask[textureIndex]);
					this.ProgressDoubleTextured(new Vector2(3f, (float)(this.GetWeaponBackrHeight(state) - this.wtask_icon[1].height - 2)), (float)(this.GetWeaponBackrWidth(state) - 5), 1f, this.wtask_icon[1], this.wtask_icon[2], this.wtask_icon[4], this.wtask_icon[5]);
					this.TextLabel(new Rect(2f, (float)(this.GetWeaponBackrHeight(state) - this.wtask_icon[1].height - 2 - 20), 50f, 50f), "100%", 15, "#62aeea", TextAnchor.UpperLeft, true);
				}
				else
				{
					this.Picture(new Vector2((float)this.GetB_Xpos(state, this.weapon_wtask_inc[textureIndex]), (float)this.GetB_Ypos(state, this.weapon_wtask[textureIndex])), this.weapon_wtask_inc[textureIndex]);
					if (state.Unlocked && !state.CurrentWeapon.isPremium && !this.disabled && !this.disableSelection)
					{
						if (state.buttonState.Hover)
						{
							this.Picture(new Vector2((float)(this.GetWeaponBackrWidth(state) - 30), 1f), this.wtaskBuy[0]);
						}
						if (state.buttonState.Clicked)
						{
							BuyWtask.unlockWtaskCached = textureIndex;
							EventFactory.Call("ShowPopup", new Popup(WindowsID.KitUnlock, Language.ThePurchaseOfModification + Main.UserInfo.weaponsStates[textureIndex].CurrentWeapon.WtaskName, "qq", PopupState.wtaskUnlock, false, true, string.Empty, string.Empty));
						}
					}
					this.ProgressDoubleTextured(new Vector2(3f, (float)(this.GetWeaponBackrHeight(state) - this.wtask_icon[1].height - 2)), (float)(this.GetWeaponBackrWidth(state) - 5), state.wtaskProgress, this.wtask_icon[1], this.wtask_icon[2], this.wtask_icon[4], this.wtask_icon[5]);
					this.TextLabel(new Rect(2f, (float)(this.GetWeaponBackrHeight(state) - this.wtask_icon[1].height - 2 - 20), 50f, 50f), state.wtaskProgress100 + "%", 15, "#FFFFFF", TextAnchor.UpperLeft, true);
				}
			}
			this.EndGroup();
		}
		if (!this.wtask_mode_on && Globals.I.masteringTable.Length > 0 && state.Unlocked)
		{
			this.DrawMasteringPictures(state, textureIndex, buttonRect);
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00056358 File Offset: 0x00054558
	public void DrawMasteringPictures(WeaponInfo state, int textureIndex, Rect buttonRect)
	{
		if (state.GetWeapon.IsModable)
		{
			if (state.GetWeapon.IsPrimary)
			{
				this.DrawWeaponExp(buttonRect, (int)state.CurrentWeapon.type);
			}
			this.DrawMainMenuButtonPicture(buttonRect);
			if (Main.UserInfo.Mastering.WeaponsStats != null && Main.UserInfo.Mastering.WeaponsStats.ContainsKey((int)state.GetWeapon.type) && state.GetWeapon.IsPrimary)
			{
				this.DrawMainMenuModSlots(buttonRect, textureIndex);
			}
		}
		else
		{
			this.DrawUnmodableMastering(state, textureIndex, buttonRect);
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x000563FC File Offset: 0x000545FC
	private void DrawMainMenuModSlots(Rect buttonRect, int weaponIndex)
	{
		Texture2D texture2D = this.masteringTextures[5];
		if (MasteringSuitsInfo.Instance.Suits == null)
		{
			return;
		}
		if (!MasteringSuitsInfo.Instance.Suits.ContainsKey(this.CurrentSuitIndex))
		{
			return;
		}
		if (!MasteringSuitsInfo.Instance.Suits[this.CurrentSuitIndex].CurrentWeaponsMods.ContainsKey(weaponIndex))
		{
			return;
		}
		Dictionary<ModType, int> mods = MasteringSuitsInfo.Instance.Suits[this.CurrentSuitIndex].CurrentWeaponsMods[weaponIndex].Mods;
		int num = 0;
		foreach (KeyValuePair<ModType, int> keyValuePair in mods)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
			if (modById == null || modById.SmallIcon == null)
			{
				break;
			}
			if (!modById.IsCamo)
			{
				GUI.DrawTexture(new Rect(buttonRect.xMin + (float)(num * (texture2D.width - 2)), buttonRect.yMax - (float)texture2D.height, (float)texture2D.width, (float)texture2D.height), texture2D);
				GUI.DrawTexture(new Rect(buttonRect.xMin + (float)(num * (texture2D.width - 2)) + 6f, buttonRect.yMax - (float)modById.SmallIcon.height - 5f, (float)modById.SmallIcon.width, (float)modById.SmallIcon.height), modById.SmallIcon);
				num++;
			}
		}
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000565BC File Offset: 0x000547BC
	private void DrawWeaponExp(Rect rect, int weaponId)
	{
		if (Main.UserInfo.Mastering.WeaponsStats == null || !Main.UserInfo.Mastering.WeaponsStats.ContainsKey(weaponId))
		{
			return;
		}
		Texture2D texture2D = this.masteringTextures[1];
		int weaponExp = Main.UserInfo.Mastering.WeaponsStats[weaponId].WeaponExp;
		GUI.DrawTexture(new Rect(rect.x + 160f, rect.y + 51f, (float)texture2D.width, (float)texture2D.height), texture2D);
		this.gui.TextLabel(new Rect(rect.x + 160f, rect.y + 49f, (float)(texture2D.width - 30), 13f), Helpers.KFormat(weaponExp), 9, "#b6b6b6_Tahoma", TextAnchor.MiddleRight, true);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00056698 File Offset: 0x00054898
	private void DrawMasteringButton(WeaponInfo state, Rect buttonRect)
	{
		Texture2D texture2D = this.masteringTextures[3];
		Vector2 pos = new Vector2(buttonRect.xMax - (float)texture2D.width + 5f, buttonRect.yMax - (float)texture2D.height + 5f);
		this._masteringButtonState = this.gui.Button(pos, null, texture2D, null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
		if (this._masteringButtonState.Clicked && !this.MasteringBtnClicked && !this._masteringGui.Showing && !this.IsWeaponViewerClicked && !Main.IsGameLoaded)
		{
			this._timeSinceMasteringOpened = Time.time;
			this.MasteringBtnClicked = true;
			if (Main.UserInfo.Mastering.WeaponsStats.ContainsKey((int)state.GetWeapon.type))
			{
				int type = (int)state.CurrentWeapon.type;
				if (state.wtaskUnlocked && !Main.UserInfo.Mastering.WeaponsStats[type].MetaUnlocked(0))
				{
					Main.AddDatabaseRequestCallBack<MasteringSetWtaskInfo>(delegate
					{
						this._masteringGui.ShowMasteringGUI(state);
					}, delegate
					{
					}, new object[]
					{
						type
					});
				}
				else
				{
					this._masteringGui.ShowMasteringGUI(state);
				}
			}
			else
			{
				Main.AddDatabaseRequestCallBack<MasteringSetWeaponInfo>(delegate
				{
					this._masteringGui.ShowMasteringGUI(state);
				}, delegate
				{
				}, new object[]
				{
					state.CurrentWeapon.type
				});
			}
		}
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0005688C File Offset: 0x00054A8C
	private void DrawMainMenuButtonPicture(Rect buttonRect)
	{
		Texture2D texture2D = this.masteringTextures[3];
		Texture2D texture2D2 = this.masteringTextures[4];
		Texture2D texture2D3 = this.masteringTextures[6];
		if (this._masteringButtonState.Hover)
		{
			Vector2 pos = new Vector2(buttonRect.xMax - (float)texture2D2.width + 12f, buttonRect.yMax - (float)texture2D2.height + 12f);
			this.gui.Picture(pos, texture2D2);
		}
		else
		{
			Vector2 pos2 = new Vector2(buttonRect.xMax - (float)texture2D.width + 5f, buttonRect.yMax - (float)texture2D.height + 5f);
			this.gui.Picture(pos2, texture2D);
		}
		GUI.DrawTexture(new Rect(buttonRect.xMax - (float)texture2D.width + 13f, buttonRect.yMax - (float)texture2D.height + 15f, (float)texture2D3.width, (float)texture2D3.height), texture2D3);
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0005698C File Offset: 0x00054B8C
	private void DrawUnmodableMastering(WeaponInfo state, int textureIndex, Rect buttonRect)
	{
		if (Main.UserInfo.suits[Main.UserInfo.suitNameIndex].primaryIndex == textureIndex || Main.UserInfo.suits[Main.UserInfo.suitNameIndex].secondaryIndex == textureIndex || state.buttonState.Hover)
		{
			this.DrawActiveMastering(state, textureIndex, buttonRect);
		}
		else if (!Main.IsGameLoaded)
		{
			this.DrawUnactiveMastering(textureIndex, buttonRect);
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00056A14 File Offset: 0x00054C14
	private void DrawUnactiveMastering(int textureIndex, Rect buttonRect)
	{
		Texture2D texture2D = this.masteringTextures[2];
		this.gui.color = new Color(1f, 1f, 1f, 0.3f);
		GUI.DrawTexture(new Rect(buttonRect.xMax - (float)texture2D.width, buttonRect.yMax - (float)texture2D.height, (float)texture2D.width, (float)texture2D.height), texture2D);
		this.gui.TextLabel(new Rect(buttonRect.xMax - (float)texture2D.width - 8f, buttonRect.yMax - (float)texture2D.height, (float)texture2D.width, (float)texture2D.height), Main.UserInfo.getWeaponLevel(Main.UserInfo.userStats.weaponKills[textureIndex]).ToString(), 10, "#ff9314_Micra", TextAnchor.MiddleRight, true);
		this.gui.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00056B18 File Offset: 0x00054D18
	private void DrawActiveMastering(WeaponInfo state, int textureIndex, Rect buttonRect)
	{
		Texture2D texture2D = this.masteringTextures[2];
		Texture2D texture2D2 = this.masteringTextures[1];
		int num = 0;
		if (textureIndex < Main.UserInfo.userStats.weaponKills.Length)
		{
			num = Main.UserInfo.userStats.weaponKills[textureIndex];
		}
		int num2 = Globals.I.masteringTable[Main.UserInfo.getWeaponLevel(Main.UserInfo.userStats.weaponKills[textureIndex]) + 1];
		GUI.DrawTexture(new Rect(buttonRect.xMax - (float)texture2D.width, buttonRect.yMax - (float)texture2D.height, (float)texture2D.width, (float)texture2D.height), texture2D);
		this.gui.TextLabel(new Rect(buttonRect.xMax - (float)texture2D.width - 8f, buttonRect.yMax - (float)texture2D.height, (float)texture2D.width, (float)texture2D.height), Main.UserInfo.getWeaponLevel(Main.UserInfo.userStats.weaponKills[textureIndex]).ToString(), 10, "#ff9314_Micra", TextAnchor.MiddleRight, true);
		float num3;
		if (num2 < 1000)
		{
			num3 = 23f;
		}
		else if (num2 < 10000)
		{
			num3 = 28f;
		}
		else if (num2 < 100000)
		{
			num3 = 33f;
		}
		else
		{
			num3 = 40f;
		}
		float num4 = 0f;
		if (state.CurrentWeapon.weaponUseType == WeaponUseType.Secondary)
		{
			num4 = 9f;
		}
		GUI.DrawTexture(new Rect(buttonRect.xMax - (float)texture2D2.width - (float)((num4 != 0f) ? 3 : (texture2D.width - 3)), buttonRect.yMax - (float)texture2D2.height + num4, (float)texture2D2.width, (float)texture2D2.height), texture2D2);
		this.gui.TextLabel(new Rect(buttonRect.xMax - (float)texture2D2.width - (float)((num4 != 0f) ? 8 : texture2D.width), buttonRect.yMax - 12f + num4, (float)texture2D2.width - num3, 13f), num.ToString(), 9, "#b6b6b6_Tahoma", TextAnchor.MiddleRight, true);
		this.gui.TextLabel(new Rect(buttonRect.xMax - (float)texture2D2.width - (float)((num4 != 0f) ? 8 : texture2D.width), buttonRect.yMax - 12f + num4, (float)texture2D2.width, 13f), "/ " + num2.ToString(), 9, "#b6b6b6_Tahoma", TextAnchor.MiddleRight, true);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00056DCC File Offset: 0x00054FCC
	private ContractInfo GetContract(int i)
	{
		switch (i)
		{
		case 0:
			return Main.UserInfo.contractsInfo.CurrentEasy;
		case 1:
			return Main.UserInfo.contractsInfo.CurrentNormal;
		case 2:
			return Main.UserInfo.contractsInfo.CurrentHard;
		default:
			return Main.UserInfo.contractsInfo.CurrentEasy;
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00056E34 File Offset: 0x00055034
	private int GetContractProgres(int i)
	{
		int result = 0;
		switch (i)
		{
		case 0:
			if (Main.UserInfo.contractsInfo.CurrentEasy.task_counter > 0)
			{
				result = Main.UserInfo.contractsInfo.CurrentEasyCount * 100 / Main.UserInfo.contractsInfo.CurrentEasy.task_counter;
			}
			break;
		case 1:
			if (Main.UserInfo.contractsInfo.CurrentNormal.task_counter > 0)
			{
				result = Main.UserInfo.contractsInfo.CurrentNormalCount * 100 / Main.UserInfo.contractsInfo.CurrentNormal.task_counter;
			}
			break;
		case 2:
			if (Main.UserInfo.contractsInfo.CurrentHard.task_counter > 0)
			{
				result = Main.UserInfo.contractsInfo.CurrentHardCount * 100 / Main.UserInfo.contractsInfo.CurrentHard.task_counter;
			}
			break;
		}
		return result;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00056F38 File Offset: 0x00055138
	private int GetContractNumber(int i)
	{
		switch (i)
		{
		case 0:
			return Main.UserInfo.contractsInfo.CurrentEasyIndex;
		case 1:
			return Main.UserInfo.contractsInfo.CurrentNormalIndex;
		case 2:
			return Main.UserInfo.contractsInfo.CurrentHardIndex;
		default:
			return 0;
		}
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00056F90 File Offset: 0x00055190
	private void MainGUIContractsInfo()
	{
		this.ContractsHint = -1;
		this.gui.color = Colors.alpha(this.gui.color, 0.3f * base.visibility);
		GUI.DrawTexture(new Rect(27f, 282f, 198f, 50f), this.black);
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		this.TextLabel(new Rect(30f, 280f, 150f, 20f), Language.ProgressDailyContracts, 9, "#a2a39d_Tahoma", TextAnchor.UpperCenter, true);
		this.TextLabel(new Rect(180f, 280f, 50f, 20f), this.gui.SecondsToStringHHHMMSS((Main.UserInfo.contractsInfo.DeltaTime <= 0) ? 0 : Main.UserInfo.contractsInfo.DeltaTime), 9, "#d1d8e4_Tahoma", TextAnchor.UpperCenter, true);
		ContractInfo contractInfo = null;
		int i = 0;
		int num = 0;
		while (i < this._carrierGui.ContractStarsImgs.Length)
		{
			this.gui.Picture(new Vector2((float)(50 + num), 295f), this._carrierGui.ContractStarsImgs[i]);
			ButtonState buttonState = this.gui.TextButton(new Rect((float)(50 + num), 295f, (float)this._carrierGui.ContractStarsImgs[0].width, (float)this._carrierGui.ContractStarsImgs[0].height), string.Empty, 24, "#ffffff", "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			if (buttonState.Hover)
			{
				this.ContractsHint = i;
				contractInfo = this.GetContract(i);
			}
			if (buttonState.Clicked)
			{
				EventFactory.Call("InitCarrier", Main.UserInfo);
				EventFactory.Call("ShowCarrier", null);
				this._carrierGui.SetCarrerState(CarrierState.CONTRACTS);
			}
			int contractProgres = this.GetContractProgres(i);
			this.TextLabel(new Rect((float)(num - 10), 315f, 150f, 20f), contractProgres + " %", 9, "#d1d8e4_Tahoma", TextAnchor.UpperCenter, true);
			i++;
			num = (this._carrierGui.ContractStarsImgs[0].width + 30) * i;
		}
		if (this.ContractsHint != -1 && this.showedTutor && this.tutorialComplete)
		{
			this.contractDescription = this.GetContractNumber(this.ContractsHint).ToString("F0") + ". " + contractInfo.description;
			if (contractInfo.prizeCR != 0)
			{
				this.contractReward = contractInfo.prizeCR.ToString("F0");
				this.contractRewardType = this.crIcon;
			}
			else if (contractInfo.prizeGP != 0)
			{
				this.contractReward = contractInfo.prizeGP.ToString("F0");
				this.contractRewardType = this.gldIcon;
			}
			else
			{
				this.contractReward = contractInfo.prizeSP.ToString("F0");
				this.contractRewardType = this.sp_small;
			}
			this.gui.ShowHint(this.contractDescription, this.contractReward, this.contractRewardType, 30 + (this._carrierGui.ContractStarsImgs[0].width + 30) * this.ContractsHint, 250);
		}
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00057314 File Offset: 0x00055514
	private void Advertisement()
	{
		if (SplashGUI.I.Visible)
		{
			return;
		}
		if (this._adTex == null)
		{
			this._adTex = AdDownloader.I.AdTexture;
		}
		if (this._adTex == null)
		{
			return;
		}
		this._adRect.Set((float)((Screen.width - this._adTex.width) / 2 - 5), (float)((!Globals.I.showPremiumBox) ? 616 : 730), (float)this._adTex.width, (float)this._adTex.height);
		if (this._adStyle == null)
		{
			this._adStyle = new GUIStyle();
			this._adStyle.normal.background = AdDownloader.I.AdTexture;
		}
		if (GUI.Button(this._adRect, string.Empty, this._adStyle))
		{
			EventFactory.Call("ShowBankGUI", null);
		}
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00057414 File Offset: 0x00055614
	private Texture2D GetWeaponBHoverTexture(WeaponInfo state, int index)
	{
		if (this.Info.suits[this.Info.suitNameIndex].secondaryIndex == index || this.Info.suits[this.Info.suitNameIndex].primaryIndex == index)
		{
			return this.GetWeaponBTexture(state, 0);
		}
		return this.GetWeaponBTexture(state, 1);
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00057480 File Offset: 0x00055680
	private Texture2D GetWeaponBTexture(WeaponInfo state, int at = 0)
	{
		if (state.CurrentWeapon.weaponUseType == WeaponUseType.Primary)
		{
			if (at == 1 && this.weapon_back.Length > 8)
			{
				return this.weapon_back[8];
			}
			if (at == 2 && this.weapon_back.Length > 1)
			{
				return this.weapon_back[1];
			}
			return this.weapon_back[0];
		}
		else
		{
			if (at == 1 && this.weapon_back.Length > 9)
			{
				return this.weapon_back[9];
			}
			if (at == 2 && this.weapon_back.Length > 5)
			{
				return this.weapon_back[5];
			}
			return this.weapon_back[4];
		}
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00057528 File Offset: 0x00055728
	private int GetWeaponBackrHeight(WeaponInfo state)
	{
		return (state.CurrentWeapon.weaponUseType != WeaponUseType.Primary) ? this.weapon_back[4].height : this.weapon_back[0].height;
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0005755C File Offset: 0x0005575C
	private int GetWeaponBackrWidth(WeaponInfo state)
	{
		return (state.CurrentWeapon.weaponUseType != WeaponUseType.Primary) ? this.weapon_back[4].width : this.weapon_back[0].width;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00057590 File Offset: 0x00055790
	private int GetB_Xpos(WeaponInfo state, Texture2D texure)
	{
		return (Mathf.Abs(this.GetWeaponBackrWidth(state)) - texure.width) / 2;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x000575A8 File Offset: 0x000557A8
	private int GetB_Ypos(WeaponInfo state, Texture2D texure)
	{
		return (Mathf.Abs(this.GetWeaponBackrHeight(state)) - texure.height) / 2;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x000575C0 File Offset: 0x000557C0
	private void DrawWeaponBackground(WeaponInfo state)
	{
		if (state.CurrentWeapon.weaponUseType == WeaponUseType.Primary)
		{
			if (state.CurrentWeapon.isPremium)
			{
				if (!this.wtask_mode_on)
				{
					this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[3]);
				}
				else
				{
					this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[2]);
				}
			}
			else if (!this.isSetUnlock(state.CurrentWeapon.armoryBlock) && !state.Unlocked)
			{
				this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[2]);
			}
			else if (state.Unlocked)
			{
				this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[1]);
			}
		}
		else if (state.CurrentWeapon.isPremium)
		{
			if (!this.wtask_mode_on)
			{
				this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[7]);
			}
			else
			{
				this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[6]);
			}
		}
		else if (state.CurrentWeapon.isDonate)
		{
			this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[10]);
		}
		else if (!this.isSetUnlock(state.CurrentWeapon.armoryBlock) && !state.Unlocked)
		{
			this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[6]);
		}
		else if (state.Unlocked)
		{
			this.gui.Picture(new Vector2(0f, 0f), this.weapon_back[5]);
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x000577D8 File Offset: 0x000559D8
	private void DrawTaskProgress()
	{
		if (this.SelectedSet == this.GetUniqTab())
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		this.BeginGroup(new Rect(210f, 475f, 775f, 600f));
		for (int i = 0; i < 101; i++)
		{
			if (Main.UserInfo.weaponsStates.Length > i && Main.UserInfo.weaponsStates[i].CurrentWeapon.armoryBlock == this.SelectedSet && !Main.UserInfo.weaponsStates[i].CurrentWeapon.isPremium)
			{
				num += Main.UserInfo.weaponsStates[i].wtaskProgress100;
				num2++;
			}
		}
		if (num2 == 0)
		{
			num2 = 1;
		}
		this.gui.TextLabel(new Rect(30f, 6f, 50f, 20f), num / num2 + "%", 14, "#768187", TextAnchor.MiddleLeft, true);
		this.gui.Picture(new Vector2(5f, 5f), this.wtask_pp[2]);
		this.EndGroup();
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00057904 File Offset: 0x00055B04
	private void DrawChoosingSet()
	{
		bool flag = this.disableSelection;
		this.gui.TextLabel(new Rect(220f, 505f, 200f, 50f), Language.SelectedSet, 15, Colors.standartBlueGary, TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(570f, 505f, 200f, 50f), Language.CharacterCamouflage, 15, Colors.standartBlueGary, TextAnchor.UpperLeft, true);
		this.BeginGroup(new Rect(215f, 525f, 780f, 600f));
		this.disableSelection = true;
		if (this.currentSuit.secondaryIndex < this.Info.weaponsStates.Length && this.currentSuit.secondaryIndex != 127)
		{
			WeaponInfo weaponInfo = this.Info.weaponsStates[this.currentSuit.secondaryIndex];
			this.WeaponGUI(new Vector2(8f, 8f), weaponInfo, this.currentSuit.secondaryIndex, this.pistol_button, this.wtask_pp, true, true);
			int weaponBackrWidth = this.GetWeaponBackrWidth(weaponInfo);
			int weaponBackrHeight = this.GetWeaponBackrHeight(weaponInfo);
			this.secondary = this.gui.TextButton(new Rect(8f, 8f, (float)weaponBackrWidth, (float)weaponBackrHeight), string.Empty, 24, "#ffffff", "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			if (this.secondary.Clicked)
			{
				this.SelectedSet = weaponInfo.CurrentWeapon.armoryBlock;
			}
		}
		if (this.currentSuit.primaryIndex < this.Info.weaponsStates.Length && this.currentSuit.primaryIndex != 127 && this.Info.weaponsStates[this.currentSuit.secondaryIndex].Unlocked)
		{
			int num = 23 + this.GetWeaponBackrWidth(this.Info.weaponsStates[this.currentSuit.secondaryIndex]);
			WeaponInfo weaponInfo2 = this.Info.weaponsStates[this.currentSuit.primaryIndex];
			this.WeaponGUI(new Vector2((float)num, 8f), weaponInfo2, this.currentSuit.primaryIndex, this.rifle_button, this.wtask_rifle, true, true);
			int weaponBackrWidth2 = this.GetWeaponBackrWidth(weaponInfo2);
			int weaponBackrHeight2 = this.GetWeaponBackrHeight(weaponInfo2);
			this.primary = this.gui.TextButton(new Rect((float)num, 8f, (float)weaponBackrWidth2, (float)weaponBackrHeight2), string.Empty, 24, "#ffffff", "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			if (this.primary.Clicked)
			{
				this.SelectedSet = weaponInfo2.CurrentWeapon.armoryBlock;
			}
		}
		if (ModsStorage.Instance().CharacterCamouflages != null && Main.UserInfo.Mastering != null)
		{
			CamouflageInfo camouflageInfo = ModsStorage.Instance().CharacterCamouflages[Main.UserInfo.Mastering.Camouflages[Main.UserInfo.suitNameIndex]];
			MasteringMod modById = ModsStorage.Instance().GetModById(Main.UserInfo.Mastering.Camouflages[Main.UserInfo.suitNameIndex]);
			this.Picture(new Vector2(354f, 8f), this.weapon_back[5]);
			int num2 = 355 + (this.weapon_back[5].width - this.CamoBack.width) / 2;
			int num3 = 10 + (this.weapon_back[5].height - this.CamoBack.height) / 2;
			this.Picture(new Vector2((float)num2, (float)num3), this.CamoBack);
			if (modById != null && modById.BigIcon != null)
			{
				Rect position = new Rect((float)(num2 + (this.CamoBack.width - modById.BigIcon.width) / 2), (float)(num3 - 1 + (this.CamoBack.height - modById.BigIcon.height) / 2), (float)modById.BigIcon.width, (float)modById.BigIcon.height);
				GUI.DrawTexture(position, modById.BigIcon);
			}
			else if (camouflageInfo.Id > 0)
			{
				this.ProcessingIndicator(new Vector2((float)(num2 + this.CamoBack.width / 2), (float)(num3 + this.CamoBack.height / 2)));
			}
			this.TextLabel(new Rect(445f, 4f, 128f, 16f), camouflageInfo.ShortName, 14, "#FFFFFF_D", TextAnchor.UpperLeft, true);
			this.TextLabel(new Rect(445f, 20f, 96f, 32f), camouflageInfo.FullName, 9, "#7C8087_T", TextAnchor.UpperLeft, true);
			bool enabled = GUI.enabled;
			GUI.enabled = !Main.IsGameLoaded;
			ButtonState buttonState = this.Button(new Vector2(438f, 47f), this.wtask_button[0], this.wtask_button[1], this.wtask_button[2], Language.Select, 15, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			GUI.enabled = enabled;
			if (buttonState.Clicked)
			{
				this.CamouflageGui.Open();
			}
		}
		if ((this.secondary.Hover || this.primary.Hover) && this.showedTutor && this.tutorialComplete)
		{
			this.onHoverWeaponInSet = true;
		}
		else
		{
			this.onHoverWeaponInSet = false;
		}
		this.disableSelection = flag;
		this.EndGroup();
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00057EC8 File Offset: 0x000560C8
	private void DrawTabWeaponSet()
	{
		int num = this.TabSetButton[0].width + 2;
		this.BeginGroup(new Rect(211f, 9f, 570f, 80f));
		for (int i = 0; i < 7; i++)
		{
			int num2;
			if (i == this.GetUniqTab())
			{
				num2 = 2;
			}
			else
			{
				num2 = 0;
			}
			if (this.Button(new Vector2((float)(i * num), 0f), this.GetTextureForTAB(i, 0 + num2), this.GetTextureForTAB(i, 1 + num2), this.GetTextureForTAB(i, 1 + num2), string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.SelectedSet = i;
				if (!this.isSetUnlock(i) && this.SelectedSet != this.GetUniqTab())
				{
					this.drawPackages = false;
					this.isClicked = false;
				}
			}
			if (this.SelectedSet == i)
			{
				this.Picture(new Vector2((float)(i * num), (float)(this.GetTextureForTAB(i, 1 + num2).height * 2 / 3)), this.TabSetButton[4]);
				this.Picture(new Vector2((float)(i * num), 0f), this.GetTextureForTAB(i, 1 + num2));
			}
			if (i == this.GetUniqTab())
			{
				this.TextLabel(new Rect((float)(i * num), -9f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), Language.SetWeapons, 9, "#000000_Tahoma", TextAnchor.MiddleCenter, true);
				this.TextLabel(new Rect((float)(i * num - 1), 4f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), "SPEC", 10, "#000000_Micra", TextAnchor.MiddleCenter, true);
				this.TextLabel(new Rect((float)(i * num - 2), 3f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), "SPEC", 10, "#f79320_Micra", TextAnchor.MiddleCenter, true);
			}
			else if (this.isSetUnlock(i) || i * 10 <= Main.UserInfo.currentLevel)
			{
				this.TextLabel(new Rect((float)(i * num), -9f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), Language.SetWeapons, 9, "#000000_Tahoma", TextAnchor.MiddleCenter, true);
				this.TextLabel(new Rect((float)(i * num - 1), 4f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), "0" + (i + 1), 10, "#a2a39d_Micra", TextAnchor.MiddleCenter, true);
				this.TextLabel(new Rect((float)(i * num - 2), 3f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), "0" + (i + 1), 10, "#000000_Micra", TextAnchor.MiddleCenter, true);
			}
			else
			{
				this.TextLabel(new Rect((float)(i * num), -9f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), Language.SetWeapons, 9, "#b20000_Tahoma", TextAnchor.MiddleCenter, true);
				this.TextLabel(new Rect((float)(i * num - 3), 2f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), "0" + (i + 1), 10, "#000000_Micra", TextAnchor.MiddleCenter, true);
				this.TextLabel(new Rect((float)(i * num - 2), 3f, (float)this.TabSetButton[0].width, (float)this.TabSetButton[0].height), "0" + (i + 1), 10, "#b20000_Micra", TextAnchor.MiddleCenter, true);
			}
			if (i != this.GetUniqTab() && !this.isSetUnlock(i))
			{
				this.gui.Picture(new Vector2((float)(i * num + 52), 10f), this.locked);
			}
		}
		this.EndGroup();
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x000582FC File Offset: 0x000564FC
	private Texture2D GetTextureForTAB(int i, int selected = 0)
	{
		if (this.isSetUnlock(i))
		{
			if (selected == 0)
			{
				return this.TabSetButton[0];
			}
			if (selected == 1)
			{
				return this.TabSetButton[1];
			}
			if (selected == 2)
			{
				return this.TabSetButton[2];
			}
			if (selected == 3)
			{
				return this.TabSetButton[3];
			}
			return this.TabSetButton[5];
		}
		else
		{
			if (selected == 2)
			{
				return this.TabSetButton[2];
			}
			if (selected == 3)
			{
				return this.TabSetButton[3];
			}
			return this.TabSetButton[5];
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x0005838C File Offset: 0x0005658C
	private bool isSetUnlock(int index)
	{
		bool result;
		switch (index)
		{
		case 0:
			result = Main.UserInfo.Set1Unlocked;
			break;
		case 1:
			result = Main.UserInfo.Set2Unlocked;
			break;
		case 2:
			result = Main.UserInfo.Set3Unlocked;
			break;
		case 3:
			result = Main.UserInfo.Set4Unlocked;
			break;
		case 4:
			result = Main.UserInfo.Set5Unlocked;
			break;
		case 5:
			result = Main.UserInfo.Set6Unlocked;
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00058428 File Offset: 0x00056628
	private void DrawWeaponSets()
	{
		bool flag = this.disableSelection;
		if (Main.IsGameLoaded)
		{
			this.disableSelection = true;
		}
		this.gui.BeginGroup(new Rect(216f, 55f, 565f, 410f));
		for (int i = 0; i < 101; i++)
		{
			if (this.Info.weaponsStates.Length > i && this.Info.weaponsStates[i].CurrentWeapon.armoryBlock == this.SelectedSet && this.Info.weaponsStates[i].CurrentWeapon.GridPosition != null)
			{
				int num = this.Info.weaponsStates[i].CurrentWeapon.GridPosition[0];
				int num2 = this.Info.weaponsStates[i].CurrentWeapon.GridPosition[1];
				if (this.Info.weaponsStates[i].CurrentWeapon.weaponUseType == WeaponUseType.Secondary)
				{
					this.WeaponGUI(new Vector2((float)(num * (this.pistol_button[0].width + 5)), (float)(num2 * (this.weapon_back[0].height + 7))), this.Info.weaponsStates[i], i, this.pistol_button, this.wtask_pp, this.isSetUnlock(this.SelectedSet) || this.SelectedSet == this.GetUniqTab(), false);
				}
				if (this.Info.weaponsStates[i].CurrentWeapon.weaponUseType == WeaponUseType.Primary)
				{
					this.WeaponGUI(new Vector2((float)(this.pistol_button[0].width + 10 + num * (this.rifle_button[0].width + 5)), (float)(num2 * (this.weapon_back[0].height + 7))), this.Info.weaponsStates[i], i, this.rifle_button, this.wtask_rifle, this.isSetUnlock(this.SelectedSet) || this.SelectedSet == this.GetUniqTab(), false);
				}
			}
		}
		this.gui.EndGroup();
		this.disableSelection = flag;
		if (Main.IsGameLoaded)
		{
			this.gui.BeginGroup(new Rect(215f, 45f, 565f, 430f));
			Color color = this.color;
			this.color = Colors.alpha(this.color, 0.6f * base.visibility);
			this.PictureSized(new Vector2(0f, 0f), this.gui.black, new Vector2(565f, 430f));
			this.color = Colors.alpha(color, base.visibility);
			this.Picture(new Vector2(70f, 100f), this.blockedSets);
			GUI.Label(new Rect(70f, 140f, (float)this.blockedSets.width, 22f), Language.MainGUIFreeChooseWeapon, CWGUI.p.standartDNC5714);
			GUI.Label(new Rect(70f, 154f, (float)this.blockedSets.width, 30f), Language.MainGUIBlocked, CWGUI.p.standartKorataki18);
			GUI.Label(new Rect(70f, 175f, (float)this.blockedSets.width, 22f), Language.MainGUIChooseSavedSets, CWGUI.p.standartDNC5714);
			this.gui.EndGroup();
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00058798 File Offset: 0x00056998
	private void DrawWtask()
	{
		this.BeginGroup(new Rect(267f, 80f, 525f, 425f));
		int num = this.SelectedSet * 10;
		if (this.SelectedSet == this.GetUniqTab())
		{
			if (this.Button(new Vector2((float)((this.SelectedSet == this.GetUniqTab()) ? 385 : 255), 398f), (!this.isClicked) ? this.boxbutton.idle : this.boxbutton.clicked, (!this.isClicked) ? this.boxbutton.over : this.boxbutton.clicked, this.boxbutton.clicked, Language.Packages + "   -", 15, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.isClicked = !this.isClicked;
				this.drawPackages = !this.drawPackages;
			}
			this.Picture(new Vector2((float)((this.SelectedSet == this.GetUniqTab()) ? 471 : 341), 404f), this.packages_icon[0]);
		}
		else if (this.isSetUnlock(this.SelectedSet) || num <= Main.UserInfo.currentLevel)
		{
			if (this.Button(new Vector2((float)((this.SelectedSet == this.GetUniqTab()) ? 385 : 255), 398f), (!this.isClicked) ? this.boxbutton.idle : this.boxbutton.clicked, (!this.isClicked) ? this.boxbutton.over : this.boxbutton.clicked, this.boxbutton.clicked, Language.Packages + "   -", 15, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.isClicked = !this.isClicked;
				this.drawPackages = !this.drawPackages;
			}
			this.Picture(new Vector2((float)((this.SelectedSet == this.GetUniqTab()) ? 471 : 341), 404f), this.packages_icon[0]);
			if (this.Info.suits[this.Info.suitNameIndex].WtaskModeStates.Length > this.SelectedSet)
			{
				if (this.Button(new Vector2(385f, 398f), this.wtask_button[(!this.Info.suits[this.Info.suitNameIndex].WtaskModeStates[this.SelectedSet]) ? 0 : 2], this.wtask_button[1], this.wtask_button[2], Language.Task + "   -", 15, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					this.Info.suits[this.Info.suitNameIndex].WtaskModeStates[this.SelectedSet] = !this.Info.suits[this.Info.suitNameIndex].WtaskModeStates[this.SelectedSet];
					this.wtask_mode_on = !this.wtask_mode_on;
				}
				this.Picture(new Vector2(469f, 401f), this.mainMenuButtons[8]);
			}
		}
		else
		{
			this.Picture(new Vector2(0f, 398f), this.mainMenuButtons[10]);
			this.TextLabel(new Rect(9f, 400f, 400f, 20f), Language.LockedRequired + num.ToString() + Language.Level, 15, "#000000", TextAnchor.UpperLeft, true);
			this.TextLabel(new Rect(9f, 399f, 400f, 20f), Language.LockedRequired + num.ToString() + Language.Level, 15, "#ffffff", TextAnchor.UpperLeft, true);
			if (!this.disableSelection)
			{
				ButtonState buttonState = this.Button(new Vector2(385f, 398f), this.wtask_button[0], this.wtask_button[1], this.wtask_button[2], Language.EarlyAccessCaps + "   -", 15, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
				this.Picture(new Vector2(486f, 399f), this.gldIcon);
				if (buttonState.Clicked)
				{
					if (!this.isSetUnlock(this.SelectedSet - 1))
					{
						EventFactory.Call("ShowPopup", new Popup(WindowsID.SetUnlock, Language.EarlyAccess, Language.TextToOpenLastSet, PopupState.information, true, true, string.Empty, string.Empty));
					}
					else
					{
						BuySet.setIndexCached = this.SelectedSet;
						EventFactory.Call("ShowPopup", new Popup(WindowsID.SetUnlock, Language.UnlockingTheSet, "qq", PopupState.setUnlock, false, true, string.Empty, string.Empty));
					}
				}
			}
		}
		this.EndGroup();
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00058D24 File Offset: 0x00056F24
	private void DrawPackages()
	{
		this.gui.BeginGroup(new Rect(216f, 57f, 555f, 450f));
		this.package.OnGUI();
		this.gui.EndGroup();
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00058D6C File Offset: 0x00056F6C
	private int GetUniqTab()
	{
		return 6;
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000903 RID: 2307 RVA: 0x00058D70 File Offset: 0x00056F70
	public override int Width
	{
		get
		{
			return 800;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000904 RID: 2308 RVA: 0x00058D78 File Offset: 0x00056F78
	public override int Height
	{
		get
		{
			return 600;
		}
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00058D80 File Offset: 0x00056F80
	public override void MainInitialize()
	{
		MainGUI.Instance = this;
		this.package.gui = this;
		this.tutor.OnStart();
		this.isUpdating = true;
		this.isRendering = true;
		this.tutorialComplete = true;
		this._carrierGui = base.GetComponent<CarrierGUI>();
		this._masteringGui = base.GetComponent<MasteringGUI>();
		this.CamouflageGui = base.GetComponent<CamouflageGUI>();
		this.weaponViewer.width = 760;
		this.weaponViewer.height = 380;
		this.CharacterViewer.width = 315;
		this.CharacterViewer.height = 562;
		base.MainInitialize();
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00058E2C File Offset: 0x0005702C
	public override void Clear()
	{
		base.Clear();
		this.textMaxSize = 16;
		this.isDoubleClick = false;
		this.upper = Vector2.zero;
		this.wtask_mode_on = false;
		this.wtask_mode_weapstate = false;
		this.weapState = null;
		this.newWeapState = null;
		this.weapHelpAlpha = new Alpha();
		this.weapHelpMousePos = Vector2.zero;
		this.disabled = false;
		this.lastUpper = new List<Vector2>();
		this.accumColor = Color.white;
		this.lastColors = new List<Color>();
		this.dblGNAME = new GraphicValue();
		this.editText = false;
		this.prevMousePos = Vector3.zero;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00058ED0 File Offset: 0x000570D0
	[Obfuscation(Exclude = true)]
	public void MainGUIShowTutor(object obj)
	{
		this.showedTutor = false;
		this.tutorialComplete = false;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00058EE0 File Offset: 0x000570E0
	public override void Register()
	{
		EventFactory.Register("ShowMainGUI", this);
		EventFactory.Register("HideMainGUI", this);
		EventFactory.Register("MainGUIShowTutor", this);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00058F04 File Offset: 0x00057104
	public override void MasterGUI()
	{
		GUI.skin = this.skin;
		Forms.PreGUI();
		if (base.Visible)
		{
			Forms.InterfaceGUI();
			if (!SplashGUI.I.Visible && !this.showedTutor)
			{
				this.ShowTutorial();
			}
			if (this.tutor.tutorialAborted)
			{
				this.ShowHelpTutorial();
			}
		}
		else if (CVars.IsStandaloneRealm && !Main.StandaloneLogined)
		{
			Forms.PopupInterfaceGUI();
		}
		else if (Main.IsGameLoaded)
		{
			Forms.GameGUI();
		}
		Forms.LateGUI();
		this.prevMousePos = Input.mousePosition;
		this.lastUpper.Clear();
		this.lastColors.Clear();
		this.upper = Vector2.zero;
		this.accumColor = Color.white;
		GUI.color = Color.white;
		GUI.skin = null;
		this.newWeapState = null;
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00058FEC File Offset: 0x000571EC
	public override void InterfaceGUI()
	{
		if (this.League.Enabled)
		{
			this.League.OnGUI();
			return;
		}
		this.color = new Color(1f, 1f, 1f, this.backgroundAlpha.visibility);
		GUI.DrawTexture(new Rect(-10f, -10f, (float)(Screen.width + 20), (float)(Screen.height + 20)), this.black);
		this.color = new Color(1f, 1f, 1f, (!base.Hiding) ? base.visibility : (base.visibility / 3f));
		Texture2D texture2D = (!CVars.IsStandaloneRealm) ? this.background : this.StandaloneBackground;
		float top = (!Globals.I.AdEnabled) ? ((float)((Screen.height - texture2D.height) / 2) + this.rect.y) : -202f;
		GUI.DrawTexture(new Rect((float)((Screen.width - texture2D.width) / 2) + this.rect.x, top, (float)texture2D.width, (float)texture2D.height), texture2D);
		if (!this.showedTutor || !this.tutorialComplete)
		{
			GUI.enabled = false;
		}
		if (Globals.I.showPremiumBox)
		{
			this.premiumPackage.OnGUI();
		}
		if (Globals.I.AdEnabled)
		{
			this.Advertisement();
		}
		this.BeginGroup(this.Rect, this.windowID != this.FocusedWindow);
		if (this.drawPackages)
		{
			this.DrawPackages();
		}
		else
		{
			this.DrawWeaponSets();
		}
		this.DrawTabWeaponSet();
		this.DrawChoosingSet();
		if (CVars.realm == "ok")
		{
			this.BeginGroup(new Rect(0f, 0f, (float)this.Width, (float)this.Height));
		}
		else
		{
			this.BeginGroup(new Rect(-15f, 0f, (float)this.Width, (float)this.Height));
		}
		if (!CVars.IsStandaloneRealm)
		{
			Helpers.Hint(new Rect(18f, 7f, (float)this.FullScreenButton[0].width, (float)this.FullScreenButton[0].height), Language.GoFullscreen, this.LabelWhite14MC, Helpers.HintAlignment.Left, 45f, 10f);
			if (this.Button(new Vector2(18f, 7f), this.FullScreenButton[0], this.FullScreenButton[1], this.FullScreenButton[1], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				if (Screen.fullScreen)
				{
					Utility.SetResolution(800, 600, false);
				}
				else
				{
					Utility.SetResolution(Main.UserInfo.settings.resolution.width, Main.UserInfo.settings.resolution.height, true);
				}
			}
		}
		if (this.MainGUIMainButtons())
		{
			return;
		}
		this.BeginGroup(new Rect(-10f, 4f, 600f, 500f));
		this.ProgressBar(new Vector2(81f, 256f), 135f, (this.Info.currentXP.Value - this.Info.minXP(this.Info.currentXP.Value)) / (this.Info.maxXP(this.Info.currentXP.Value) - this.Info.minXP(this.Info.currentXP.Value)), this.xp_progressbar, 0f, false, true);
		this.Picture(new Vector2(31f, 208f), this.rank_icon[this.Info.currentLevel]);
		if (this.Info.nick == Language.ProfileNotLoaded)
		{
			this.TextLabel(new Rect(60f, 203f, 230f, 130f), this.Info.nick, 18, "#CC0000", TextAnchor.UpperLeft, true);
		}
		else
		{
			this.TextLabel(new Rect(60f, 203f, 130f, 130f), this.Info.nick, 20, this.Info.nickColor, TextAnchor.UpperLeft, true);
		}
		this.TextLabel(new Rect(60f, 228f, 130f, 130f), this.rank_text[this.Info.currentLevel], 14, "#b1b1b1", TextAnchor.UpperLeft, true);
		if (this.Info.SP > 0)
		{
			this.gui.color = Colors.alpha(this.gui.color, base.visibility * this.gui.color.a * Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 2f)));
			this.Picture(new Vector2(157f, 186f), this.mainmenu_skillbut[2]);
			this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		}
		if (this.Button(new Vector2(178f, 205f), this.mainmenu_skillbut[0], this.mainmenu_skillbut[1], null, string.Empty, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("InitCarrier", Main.UserInfo);
			EventFactory.Call("ShowCarrier", null);
			this._carrierGui.SetCarrerState(CarrierState.SKILLS);
		}
		this.TextLabel(new Rect(178f, 233f, 46f, 20f), Language.CarrSkills.ToLower(), 9, "#e3f4fd_T", TextAnchor.MiddleCenter, true);
		string str = Helpers.SeparateNumericString(Mathf.CeilToInt(this.Info.currentXP.Value).ToString());
		string str2 = Helpers.SeparateNumericString(Mathf.CeilToInt(this.Info.maxXP(this.Info.currentXP.Value)).ToString());
		this.TextLabel(new Rect(70f, 253f, 165f, 300f), str + "/" + str2, 19, "#000000", TextAnchor.UpperCenter, true);
		this.TextLabel(new Rect(70f, 253f, 165f, 300f), str + "/" + str2, 19, "#FFFFFF", TextAnchor.UpperCenter, true);
		this.MainGUIContractsInfo();
		this.gui.color = Colors.alpha(this.gui.color, 0.3f * base.visibility);
		GUI.DrawTexture(new Rect(27f, 346f, 198f, 67f), this.black);
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		this.TextLabel(new Rect(135f, 348f, 50f, 22f), this.Info.SP.ToString(), 20, "#ffffff", TextAnchor.MiddleRight, true);
		this.TextLabel(new Rect(135f, 369f, 50f, 22f), Main.UserInfo.Mastering.MasteringPoints.ToString(), 20, "#ffffff", TextAnchor.MiddleRight, true);
		this.TextLabel(new Rect(58f, 348f, 100f, 22f), Helpers.SeparateNumericString(this.Info.CR.ToString()), 20, "#ffffff", TextAnchor.MiddleLeft, true);
		this.TextLabel(new Rect(58f, 369f, 100f, 22f), Helpers.SeparateNumericString(this.Info.GP.ToString()), 20, "#ffa200", TextAnchor.MiddleLeft, true);
		Texture2D texture2D2 = this.masteringTextures[6];
		Rect rect = new Rect(190f, 372f, (float)texture2D2.width, (float)texture2D2.height);
		if (this.Button(new Vector2(190f, 372f), texture2D2, texture2D2, texture2D2, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked || this.Button(new Vector2(189f, 347f), this.sp_small, this.sp_small, this.sp_small, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked || this.Button(new Vector2(30f, 347f), this.crIcon, this.crIcon, this.crIcon, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked || this.Button(new Vector2(31f, 369f), this.gldIcon, this.gldIcon, this.gldIcon, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("ShowBankGUI", null);
		}
		if (rect.Contains(Event.current.mousePosition))
		{
			Texture2D texture2D3 = this.repair[2];
			int num = 112;
			GUI.BeginGroup(new Rect(Event.current.mousePosition.x + 2f, Event.current.mousePosition.y - 18f, (float)num, 16f));
			Color color = this.color;
			this.color = new Color(1f, 1f, 1f, 0.9f);
			GUI.DrawTexture(new Rect(0f, 0f, (float)num, 16f), this.black);
			this.color = color;
			GUI.DrawTexture(new Rect((float)(num - texture2D3.width) * 0.5f, (float)(16 - texture2D3.height) * 0.5f, (float)texture2D3.width, (float)texture2D3.height), texture2D3);
			float width = (float)(texture2D3.width - 2) * this.Info.PercentForNextMpLevel - 2f;
			GUI.DrawTexture(new Rect((float)(num - texture2D3.width) * 0.5f + 2f, (float)(16 - texture2D3.height) * 0.5f + 2f, width, 2f), this.orange);
			string text = Helpers.SeparateNumericString(this.Info.CurrentMpExp.ToString());
			TextAnchor alignment = CWGUI.p.MastModDescContentStyle.alignment;
			CWGUI.p.MastModDescContentStyle.alignment = TextAnchor.MiddleRight;
			GUI.Label(new Rect(0f, 2f, 30f, 10f), text, CWGUI.p.MastModDescContentStyle);
			text = Helpers.SeparateNumericString(Globals.I.MasteringExpToNextPoint.ToString());
			CWGUI.p.MastModDescContentStyle.alignment = alignment;
			GUI.Label(new Rect((float)(num - texture2D3.width) * 0.5f + (float)texture2D3.width + 2f, 2f, 30f, 10f), text, CWGUI.p.MastModDescContentStyle);
			GUI.EndGroup();
		}
		if (this.Button(new Vector2(27f, 389f), this.mainMenuButtons[11], this.mainMenuButtons[12], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("ShowBankGUI", null);
		}
		this.TextLabel(new Rect((float)((Language.CurrentLanguage != ELanguage.EN) ? 60 : 70), 390f, 200f, 20f), Language.FillUpBalance, 16, "#FFFFFF", TextAnchor.MiddleLeft, true);
		this.TextLabel(new Rect(165f, 390f, 20f, 20f), "+", 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
		this.gui.color = Colors.alpha(this.gui.color, base.visibility * this.gui.color.a * Mathf.Abs(Mathf.Cos(Time.realtimeSinceStartup * 4f)));
		GUI.DrawTexture(new Rect(28f, 388f, (float)this.YellowGlow.width, (float)this.YellowGlow.height), this.YellowGlow);
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		GUI.DrawTexture(new Rect(181f, 389f, (float)this.crIcon.width, (float)this.crIcon.height), this.crIcon);
		GUI.DrawTexture(new Rect(191f, 390f, (float)this.gldIcon.width, (float)this.gldIcon.height), this.gldIcon);
		this.EndGroup();
		if ((Globals.I.RouletteInfo.Enabled || Main.UserInfo.Permission >= EPermission.Admin) && this.isSetUnlock(this.SelectedSet))
		{
			if (Main.IsGameLoaded || Main.UserInfo.currentLevel < 10)
			{
				GUI.enabled = false;
			}
			if (this.Button(new Vector2(227f, 478f), this.wtask_button[0], this.wtask_button[1], this.wtask_button[2], Language.Roulette.ToUpper(), 15, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				EventFactory.Call("ShowRoulette", null);
			}
			GUI.enabled = true;
			if (Main.UserInfo.currentLevel < 10)
			{
				Rect onHoverRect = new Rect(227f, 478f, (float)this.wtask_button[0].width, (float)this.wtask_button[0].height);
				if (onHoverRect.Contains(Event.current.mousePosition))
				{
					Helpers.Hint(onHoverRect, Language.ClansExpSliderHint, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
				}
				GUI.DrawTexture(new Rect(320f, 476f, (float)this.locked.width, (float)this.locked.height), this.locked);
			}
			else
			{
				GUI.DrawTexture(new Rect(320f, 480f, (float)this.RouletteIcon.width, (float)this.RouletteIcon.height), this.RouletteIcon);
			}
			if (!this.drawPackages && Main.UserInfo.currentLevel >= 10)
			{
				GUI.DrawTexture(new Rect(300f, 458f, (float)this.discountbar.width, (float)this.discountbar.height), this.discountbar);
				GUI.Label(new Rect(300f, 458f, (float)this.discountbar.width, (float)this.discountbar.height), Language.RouletteTries.ToUpper() + ": " + Main.UserInfo.Attempts, this.RouletteStyle);
			}
		}
		this.BeginGroup(new Rect(-9f, 9f, 230f, 600f));
		this.gui.TextLabel(new Rect(55f, 413f, 170f, 50f), Language.SetOfEquipment, 15, Colors.standartBlueGary, TextAnchor.UpperLeft, true);
		for (int i = 0; i < this.Info.suits.Length; i++)
		{
			if (this.Info.suits[i].Unlocked)
			{
				if (i == this.CurrentSuitIndex)
				{
					if (!Main.IsGameLoaded && this.Button(new Vector2(30f, (float)(435 + 25 * i)), this.edit_button[3], this.edit_button[2], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
					{
						Main.AddDatabaseRequest<SaveProfile>(new object[0]);
						this.editText = false;
					}
					if (this.editText)
					{
						if (!this.disabled)
						{
							GUI.FocusControl("TextField1");
							GUI.SetNextControlName("TextField1");
						}
						bool flag = this.gui.inRect(new Rect(28f, (float)(435 + 25 * i), (float)this.kitMenuButtons[0].width, (float)this.kitMenuButtons[0].height), this.upper, this.cursorPosition);
						if (this.Button(new Vector2(188f, (float)(435 + 25 * i)), this.edit_button[1], this.edit_button[1], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked || Input.GetKeyDown(KeyCode.Return))
						{
							this.editText = false;
							if (!string.IsNullOrEmpty(this.Info.suits[i].SuitName) && this.Info.suits[i].SuitName[this.Info.suits[i].SuitName.Length - 1] == ' ')
							{
								this.Info.suits[i].SuitName = this.Info.suits[i].SuitName.Substring(0, this.Info.suits[i].SuitName.Length - 1);
							}
							if (string.IsNullOrEmpty(this.Info.suits[i].SuitName))
							{
								this.Info.suits[i].SuitName = "SET" + (i + 1);
							}
						}
						if (!flag && Input.GetMouseButtonDown(0))
						{
							this.editText = false;
						}
						if (!this.editText)
						{
							this.Button(new Vector2(28f, (float)(435 + 25 * i)), this.kitMenuButtons[0], this.kitMenuButtons[1], this.kitMenuButtons[2], string.Empty, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null);
						}
						else
						{
							this.Picture(new Vector2(28f, (float)(435 + 25 * i)), this.kitMenuButtons[0]);
						}
						string str3 = this.TextField(new Rect(28f, (float)(435 + 25 * i), (float)this.kitMenuButtons[0].width, (float)this.kitMenuButtons[0].height), this.Info.suits[i].SuitName, 15, "#adb0af", TextAnchor.LowerCenter, true, true);
						this.Info.suits[i].SuitName = Helpers.WeaponKitNameRegEx(str3);
					}
					else
					{
						if (this.Button(new Vector2(188f, (float)(435 + 25 * i)), this.edit_button[0], this.edit_button[1], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked && !Main.IsGameLoaded)
						{
							this.editText = true;
						}
						this.Button(new Vector2(188f, (float)(435 + 25 * i)), this.edit_button[0], this.edit_button[0], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
						this.Button(new Vector2(28f, (float)(435 + 25 * i)), this.kitMenuButtons[2], this.kitMenuButtons[2], this.kitMenuButtons[2], this.Info.suits[i].SuitName, 15, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
					}
				}
				else
				{
					ButtonState buttonState = this.Button(new Vector2(188f, (float)(435 + 25 * i)), this.edit_button[0], this.edit_button[1], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
					if (buttonState.Clicked && !Main.IsGameLoaded)
					{
						this.Info.suitNameIndex = i;
						this.editText = true;
						buttonState.Selected = true;
						Dictionary<int, WeaponMods> currentWeaponsMods = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods;
						foreach (KeyValuePair<int, WeaponMods> keyValuePair in currentWeaponsMods)
						{
							Main.UserInfo.weaponsStates[keyValuePair.Key].CurrentWeapon.ApplyModsEffect(keyValuePair.Value.Mods);
						}
					}
					else if (buttonState.Hover)
					{
						this.Button(new Vector2(28f, (float)(435 + 25 * i)), this.kitMenuButtons[0], this.kitMenuButtons[0], this.kitMenuButtons[0], this.Info.suits[i].SuitName, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null);
					}
					else if (buttonState.normal && this.Button(new Vector2(28f, (float)(435 + 25 * i)), this.kitMenuButtons[0], this.kitMenuButtons[1], this.kitMenuButtons[2], this.Info.suits[i].SuitName, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
					{
						this.CurrentSuitIndex = i;
						base.StartCoroutine(this.ApplyMods(i));
					}
				}
			}
			else
			{
				Color color2 = this.color;
				if (Main.IsGameLoaded)
				{
					this.color = Colors.alpha(this.color, 0.4f * base.visibility);
				}
				if (this.Button(new Vector2(28f, (float)(435 + 25 * i)), this.mainMenuButtons[3], this.mainMenuButtons[3], this.mainMenuButtons[3], Language.Set + i, 15, "#ff0000", TextAnchor.MiddleCenter, null, null, null, null).Clicked && !Main.IsGameLoaded)
				{
					BuyKit.KitIndexCached = i;
					EventFactory.Call("ShowPopup", new Popup(WindowsID.KitUnlock, Language.UnlockingTheSet, "qq", PopupState.kitUnlock, false, true, string.Empty, string.Empty));
				}
				this.Picture(new Vector2(35f, (float)(432 + 25 * i)), this.locked);
				this.Picture(new Vector2(193f, (float)(436 + 25 * i)), this.gldIcon);
				this.TextLabel(new Rect(91f, (float)(437 + 25 * i), 100f, 20f), Globals.I.weaponSetPrices[i], 15, "#fbc421", TextAnchor.MiddleRight, true);
				this.color = Colors.alpha(color2, base.visibility);
			}
		}
		if (CVars.realm != "ag" && !CVars.IsStandaloneRealm)
		{
			if (this.gui.Button(new Vector2(35f, 557f), this.mainMenuButtons[13], this.mainMenuButtons[14], this.mainMenuButtons[13], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked && CVars.realm != "standalone")
			{
				Application.ExternalCall("Invite", new object[0]);
			}
			this.gui.TextLabel(new Rect(35f, 557f, (float)(this.mainMenuButtons[13].width - this.mainMenuButtons[13].width / 4), (float)this.mainMenuButtons[13].height), Language.InviteFriends, 17, "#aab8be", TextAnchor.MiddleCenter, true);
			this.gui.TextLabel(new Rect(35f, 556f, (float)(this.mainMenuButtons[13].width - this.mainMenuButtons[13].width / 4), (float)this.mainMenuButtons[13].height), Language.InviteFriends, 17, "#182029", TextAnchor.MiddleCenter, true);
		}
		this.EndGroup();
		if (Main.IsGameLoaded || !this.showedTutor || !this.tutorialComplete)
		{
			this.disableSelection = true;
		}
		else
		{
			this.disableSelection = false;
		}
		this.EndGroup();
		this.DrawWtask();
		this.EndGroup();
		this.ShowWeaponInfo();
		GUI.enabled = true;
		this.color = new Color(1f, 1f, 1f, base.visibility);
		this.TextLabel(new Rect((float)(Screen.width - 804), (float)(Screen.height - 25), 800f, 25f), CVars.Version, 14, "#FFFFFF", TextAnchor.LowerRight, true);
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x0600090B RID: 2315 RVA: 0x0005AAC8 File Offset: 0x00058CC8
	// (set) Token: 0x0600090C RID: 2316 RVA: 0x0005AB00 File Offset: 0x00058D00
	public int CurrentSuitIndex
	{
		get
		{
			if (this._currentSuitIndex < 0)
			{
				this._currentSuitIndex = this.Info.suitNameIndex;
			}
			return this._currentSuitIndex;
		}
		private set
		{
			this._currentSuitIndex = value;
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0005AB0C File Offset: 0x00058D0C
	private IEnumerator ApplyMods(int i)
	{
		while (Main.IsGameLoaded && Peer.ClientGame.LocalPlayer.IsAlive)
		{
			yield return new WaitForEndOfFrame();
		}
		this.editText = false;
		this.Info.suitNameIndex = i;
		Dictionary<int, WeaponMods> weapons = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods;
		foreach (KeyValuePair<int, WeaponMods> weapon in weapons)
		{
			Main.UserInfo.weaponsStates[weapon.Key].CurrentWeapon.ApplyModsEffect(weapon.Value.Mods);
		}
		if (Main.IsGameLoaded)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.DownloadAdditionalGameData, Language.CWMainLoading, Language.DownloadAdditionalGameDataDesc, PopupState.progress, false, true, string.Empty, "AdditionalWeapon"));
			Loader.DownloadAdditionalGameData(this.currentSuit, true);
		}
		yield break;
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0005AB38 File Offset: 0x00058D38
	public override void OnUpdate()
	{
		if (this.League.Enabled)
		{
			this.League.OnUpdate();
		}
		if (this.windowID == this.FocusedWindow)
		{
			this.package.OnUpdate();
		}
		if (Main.UserInfo.Permission >= EPermission.Admin && Input.GetKeyDown(KeyCode.Keypad1))
		{
			Debug.Break();
		}
		if (Main.IsGameLoaded)
		{
			Screen.lockCursor = (!Forms.keyboardLock && Forms.mouseLock);
		}
		if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(Main.Binds.menu)) && Main.IsGameLoaded && !Main.IsGameLoading && !Forms.chatLock && SingletoneForm<Loader>.Instance.IsGameLoadedAndClicked)
		{
			if (base.Visible)
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.DownloadAdditionalGameData, Language.CWMainLoading, Language.DownloadAdditionalGameDataDesc, PopupState.progress, false, true, string.Empty, "AdditionalWeapon"));
				Loader.DownloadAdditionalGameData(this.currentSuit, false);
			}
			else
			{
				EventFactory.Call("ShowInterface", null);
			}
		}
		if (Input.GetKeyDown(Main.Binds.fullscreen))
		{
			if (Screen.fullScreen)
			{
				Utility.SetResolution(800, 600, false);
			}
			else
			{
				Utility.SetResolution(Main.UserInfo.settings.resolution.width, Main.UserInfo.settings.resolution.height, true);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			if (this.dblGNAME.Get() == 1f)
			{
				this.isDoubleClick = true;
			}
			Vector2[] v = new Vector2[]
			{
				new Vector2(0f, 1f),
				new Vector2(0.45f, 1f),
				new Vector2(0.5f, 0f),
				new Vector2(float.MaxValue, 0f)
			};
			this.dblGNAME.Init(v);
		}
		else if (this.dblGNAME.Get() != 1f)
		{
			this.isDoubleClick = false;
		}
	}

	// Token: 0x040009A2 RID: 2466
	private const float MasteringOpenedCooldown = 1f;

	// Token: 0x040009A3 RID: 2467
	public static MainGUI Instance = null;

	// Token: 0x040009A4 RID: 2468
	public LeagueWindow League = new LeagueWindow();

	// Token: 0x040009A5 RID: 2469
	public TutorContainer tutor = new TutorContainer();

	// Token: 0x040009A6 RID: 2470
	public Package package = new Package();

	// Token: 0x040009A7 RID: 2471
	public PremiumPackage premiumPackage = new PremiumPackage();

	// Token: 0x040009A8 RID: 2472
	public ButtonTextures boxbutton = new ButtonTextures();

	// Token: 0x040009A9 RID: 2473
	private bool showedTutor = true;

	// Token: 0x040009AA RID: 2474
	public bool tutorialComplete = true;

	// Token: 0x040009AB RID: 2475
	private Texture2D tutorialWtaskIcon;

	// Token: 0x040009AC RID: 2476
	private Alpha backgroundAlpha = new Alpha();

	// Token: 0x040009AD RID: 2477
	public Texture2D binoculars;

	// Token: 0x040009AE RID: 2478
	public Texture2D binocularsMarker;

	// Token: 0x040009AF RID: 2479
	public Texture2D PassViewOn;

	// Token: 0x040009B0 RID: 2480
	public Texture2D PassViewOff;

	// Token: 0x040009B1 RID: 2481
	public Texture2D SwitchLanguageSub;

	// Token: 0x040009B2 RID: 2482
	public Texture2D SwitchLanguageIdle;

	// Token: 0x040009B3 RID: 2483
	public Texture2D SwitchLanguageOver;

	// Token: 0x040009B4 RID: 2484
	public Texture2D SwitchLanguagePressed;

	// Token: 0x040009B5 RID: 2485
	public Material additive;

	// Token: 0x040009B6 RID: 2486
	public Material radar;

	// Token: 0x040009B7 RID: 2487
	public Font fontDNC57;

	// Token: 0x040009B8 RID: 2488
	public Font fontMicra;

	// Token: 0x040009B9 RID: 2489
	public Font fontTahoma;

	// Token: 0x040009BA RID: 2490
	public GUISkin skin;

	// Token: 0x040009BB RID: 2491
	public RenderTexture weaponViewer;

	// Token: 0x040009BC RID: 2492
	public RenderTexture masteringWeaponViewer;

	// Token: 0x040009BD RID: 2493
	public RenderTexture CharacterViewer;

	// Token: 0x040009BE RID: 2494
	public Texture2D Crown;

	// Token: 0x040009BF RID: 2495
	public Texture2D cr_BIG;

	// Token: 0x040009C0 RID: 2496
	public Texture2D sp_small;

	// Token: 0x040009C1 RID: 2497
	public Texture2D wtask_popup;

	// Token: 0x040009C2 RID: 2498
	public Texture2D red;

	// Token: 0x040009C3 RID: 2499
	public Texture2D green;

	// Token: 0x040009C4 RID: 2500
	public Texture2D black;

	// Token: 0x040009C5 RID: 2501
	public Texture2D white;

	// Token: 0x040009C6 RID: 2502
	public Texture2D blue;

	// Token: 0x040009C7 RID: 2503
	public Texture2D yellow;

	// Token: 0x040009C8 RID: 2504
	public Texture2D orange;

	// Token: 0x040009C9 RID: 2505
	public Texture2D locked;

	// Token: 0x040009CA RID: 2506
	public Texture2D crIcon;

	// Token: 0x040009CB RID: 2507
	public Texture2D gldIcon;

	// Token: 0x040009CC RID: 2508
	public Texture2D bgIcon;

	// Token: 0x040009CD RID: 2509
	public Texture2D spIcon_med;

	// Token: 0x040009CE RID: 2510
	public Texture2D invisible;

	// Token: 0x040009CF RID: 2511
	public Texture2D weapon_info;

	// Token: 0x040009D0 RID: 2512
	public Texture2D games_bg;

	// Token: 0x040009D1 RID: 2513
	public Texture2D xp_progressbar;

	// Token: 0x040009D2 RID: 2514
	public Texture2D char_progressbar;

	// Token: 0x040009D3 RID: 2515
	public Texture2D blue_progressbar;

	// Token: 0x040009D4 RID: 2516
	public Texture2D background;

	// Token: 0x040009D5 RID: 2517
	public Texture2D StandaloneBackground;

	// Token: 0x040009D6 RID: 2518
	public Texture2D weapon_set_background;

	// Token: 0x040009D7 RID: 2519
	public Texture2D weapon_set_background_locked;

	// Token: 0x040009D8 RID: 2520
	public Texture2D fastgame;

	// Token: 0x040009D9 RID: 2521
	public Texture2D[] TabSetButton;

	// Token: 0x040009DA RID: 2522
	public Texture2D[] mainmenu_skillbut;

	// Token: 0x040009DB RID: 2523
	public string[] rank_text;

	// Token: 0x040009DC RID: 2524
	public Texture2D[] premuim_buttons;

	// Token: 0x040009DD RID: 2525
	public Texture2D[] mainMenuButtons;

	// Token: 0x040009DE RID: 2526
	public Texture2D[] kitMenuButtons;

	// Token: 0x040009DF RID: 2527
	public Texture2D[] edit_button;

	// Token: 0x040009E0 RID: 2528
	public Texture2D[] weapon_sets_locked;

	// Token: 0x040009E1 RID: 2529
	public Texture2D[] rank_icon;

	// Token: 0x040009E2 RID: 2530
	public Texture2D[] packages_icon;

	// Token: 0x040009E3 RID: 2531
	public Texture2D[] wtask_icon;

	// Token: 0x040009E4 RID: 2532
	public Texture2D[] wtask_pp;

	// Token: 0x040009E5 RID: 2533
	public Texture2D[] wtask_button;

	// Token: 0x040009E6 RID: 2534
	public Texture2D[] pistol_button;

	// Token: 0x040009E7 RID: 2535
	public Texture2D[] pp_button;

	// Token: 0x040009E8 RID: 2536
	public Texture2D[] rifle_button;

	// Token: 0x040009E9 RID: 2537
	public Texture2D[] wtask_rifle;

	// Token: 0x040009EA RID: 2538
	public Texture2D[] wtask_gear;

	// Token: 0x040009EB RID: 2539
	public Texture2D[] server_window;

	// Token: 0x040009EC RID: 2540
	public Texture2D[] settings_window;

	// Token: 0x040009ED RID: 2541
	public Texture2D[] repair;

	// Token: 0x040009EE RID: 2542
	public Texture2D[] WeaponView;

	// Token: 0x040009EF RID: 2543
	public AudioClip hoverSoundPrefab;

	// Token: 0x040009F0 RID: 2544
	public AudioClip clickSoundPrefab;

	// Token: 0x040009F1 RID: 2545
	public AudioClip error_sound;

	// Token: 0x040009F2 RID: 2546
	public AudioClip repair_sound;

	// Token: 0x040009F3 RID: 2547
	public AudioClip buy_sound;

	// Token: 0x040009F4 RID: 2548
	public AudioClip modEquipClickSoundPrefab;

	// Token: 0x040009F5 RID: 2549
	public AudioClip pistolEquipClickSoundPrefab;

	// Token: 0x040009F6 RID: 2550
	public AudioClip gunEquipClickSoundPrefab;

	// Token: 0x040009F7 RID: 2551
	public Texture2D[] weapon_locked;

	// Token: 0x040009F8 RID: 2552
	public Texture2D[] weapon_unlocked;

	// Token: 0x040009F9 RID: 2553
	public Texture2D[] weapon_wtask;

	// Token: 0x040009FA RID: 2554
	public Texture2D[] weapon_wtask_inc;

	// Token: 0x040009FB RID: 2555
	public Texture2D[] weapon_back;

	// Token: 0x040009FC RID: 2556
	public Texture2D server_hint;

	// Token: 0x040009FD RID: 2557
	public Texture2D blockedSets;

	// Token: 0x040009FE RID: 2558
	public Texture2D[] weaponFrames;

	// Token: 0x040009FF RID: 2559
	public Texture2D[] wtaskBuy;

	// Token: 0x04000A00 RID: 2560
	public Texture2D[] FullScreenButton;

	// Token: 0x04000A01 RID: 2561
	public Texture2D discountbar;

	// Token: 0x04000A02 RID: 2562
	public Texture2D sprint;

	// Token: 0x04000A03 RID: 2563
	public Texture2D KrutilkaSmall;

	// Token: 0x04000A04 RID: 2564
	public Texture2D CamoBack;

	// Token: 0x04000A05 RID: 2565
	public Texture2D slide1;

	// Token: 0x04000A06 RID: 2566
	public Texture2D slide2;

	// Token: 0x04000A07 RID: 2567
	public Material mat;

	// Token: 0x04000A08 RID: 2568
	public Texture2D[] radioChat;

	// Token: 0x04000A09 RID: 2569
	public Texture2D[] closeSmallButton;

	// Token: 0x04000A0A RID: 2570
	public Texture2D RouletteIcon;

	// Token: 0x04000A0B RID: 2571
	public GUIStyle RouletteStyle;

	// Token: 0x04000A0C RID: 2572
	public GUIStyle LabelWhite14MC;

	// Token: 0x04000A0D RID: 2573
	public GUIStyle RepairButton;

	// Token: 0x04000A0E RID: 2574
	public Texture2D YellowGlow;

	// Token: 0x04000A0F RID: 2575
	[HideInInspector]
	public bool disableSelection;

	// Token: 0x04000A10 RID: 2576
	public Texture2D[] masteringTextures;

	// Token: 0x04000A11 RID: 2577
	private CarrierGUI _carrierGui;

	// Token: 0x04000A12 RID: 2578
	private MasteringGUI _masteringGui;

	// Token: 0x04000A13 RID: 2579
	internal CamouflageGUI CamouflageGui;

	// Token: 0x04000A14 RID: 2580
	public Texture2D[] SocialNetIcons;

	// Token: 0x04000A15 RID: 2581
	private float lastTimeServerListUpdate;

	// Token: 0x04000A16 RID: 2582
	private bool drawPackages;

	// Token: 0x04000A17 RID: 2583
	private bool isClicked;

	// Token: 0x04000A18 RID: 2584
	private int ContractsHint = -1;

	// Token: 0x04000A19 RID: 2585
	private string contractReward = string.Empty;

	// Token: 0x04000A1A RID: 2586
	private string contractDescription = string.Empty;

	// Token: 0x04000A1B RID: 2587
	private Texture2D contractRewardType;

	// Token: 0x04000A1C RID: 2588
	[HideInInspector]
	public bool globalHover;

	// Token: 0x04000A1D RID: 2589
	private ButtonState secondary;

	// Token: 0x04000A1E RID: 2590
	private ButtonState primary;

	// Token: 0x04000A1F RID: 2591
	[HideInInspector]
	public bool onHoverWeaponInSet;

	// Token: 0x04000A20 RID: 2592
	[HideInInspector]
	public int textMaxSize = 16;

	// Token: 0x04000A21 RID: 2593
	[HideInInspector]
	public bool isDoubleClick;

	// Token: 0x04000A22 RID: 2594
	[HideInInspector]
	public Vector2 upper = Vector2.zero;

	// Token: 0x04000A23 RID: 2595
	private bool wtask_mode_on;

	// Token: 0x04000A24 RID: 2596
	[HideInInspector]
	public bool wtask_mode_weapstate;

	// Token: 0x04000A25 RID: 2597
	private WeaponInfo weapState;

	// Token: 0x04000A26 RID: 2598
	[HideInInspector]
	public WeaponInfo newWeapState;

	// Token: 0x04000A27 RID: 2599
	public int SelectedSet;

	// Token: 0x04000A28 RID: 2600
	private Rect tmpRect = new Rect(115f, 49f, 50f, 7f);

	// Token: 0x04000A29 RID: 2601
	[HideInInspector]
	public Alpha weapHelpAlpha = new Alpha();

	// Token: 0x04000A2A RID: 2602
	[HideInInspector]
	public Vector2 weapHelpMousePos = Vector2.zero;

	// Token: 0x04000A2B RID: 2603
	private bool disabled;

	// Token: 0x04000A2C RID: 2604
	private List<Vector2> lastUpper = new List<Vector2>();

	// Token: 0x04000A2D RID: 2605
	private Color accumColor = Color.white;

	// Token: 0x04000A2E RID: 2606
	private List<Color> lastColors = new List<Color>();

	// Token: 0x04000A2F RID: 2607
	private GraphicValue dblGNAME = new GraphicValue();

	// Token: 0x04000A30 RID: 2608
	private bool editText;

	// Token: 0x04000A31 RID: 2609
	private Vector3 prevMousePos = Vector3.zero;

	// Token: 0x04000A32 RID: 2610
	private static Dictionary<string, MainGUI.Settings> setts = new Dictionary<string, MainGUI.Settings>();

	// Token: 0x04000A33 RID: 2611
	private string _passwordTextColor = "#FFFFFF";

	// Token: 0x04000A34 RID: 2612
	public GUIStyle testStyle = new GUIStyle();

	// Token: 0x04000A35 RID: 2613
	private int focusedWindow = -1;

	// Token: 0x04000A36 RID: 2614
	private GUIContent cacheGUIContent = new GUIContent();

	// Token: 0x04000A37 RID: 2615
	private bool _alreadyQuit;

	// Token: 0x04000A38 RID: 2616
	public bool IsWeaponViewerClicked;

	// Token: 0x04000A39 RID: 2617
	private float _timeSinceMasteringOpened;

	// Token: 0x04000A3A RID: 2618
	private ButtonState _masteringButtonState;

	// Token: 0x04000A3B RID: 2619
	public bool MasteringBtnClicked;

	// Token: 0x04000A3C RID: 2620
	private Rect _adRect;

	// Token: 0x04000A3D RID: 2621
	private GUIStyle _adStyle;

	// Token: 0x04000A3E RID: 2622
	private Texture2D _adTex;

	// Token: 0x04000A3F RID: 2623
	private int _currentSuitIndex = -1;

	// Token: 0x0200015A RID: 346
	private class Settings
	{
		// Token: 0x04000A43 RID: 2627
		public Color Color;

		// Token: 0x04000A44 RID: 2628
		public Font Font;
	}
}
