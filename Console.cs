using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ConsoleGUI;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x0200006C RID: 108
[AddComponentMenu("Scripts/Engine/Console")]
public class Console : SingletoneForm<global::Console>
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
	public static List<LogEntry> Entries
	{
		get
		{
			return SingletoneForm<global::Console>.instance.entries;
		}
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
	[Obfuscation(Exclude = true)]
	private void ChangeWind1()
	{
		this.slide1RandVelocity = new Vector2(UnityEngine.Random.Range(-1f, 1f) * 0.05f, UnityEngine.Random.Range(-1f, 1f) * 0.001f);
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000F1FC File Offset: 0x0000D3FC
	[Obfuscation(Exclude = true)]
	private void ChangeWind2()
	{
		this.slide2RandVelocity = new Vector2(UnityEngine.Random.Range(-1f, 1f) * 0.01f, UnityEngine.Random.Range(-1f, 1f) * 0.005f);
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000F234 File Offset: 0x0000D434
	public static string GetLOG()
	{
		StringBuilder stringBuilder = new StringBuilder();
		try
		{
			for (int i = 0; i < SingletoneForm<global::Console>.Instance.entries.Count; i++)
			{
				stringBuilder.AppendLine(SingletoneForm<global::Console>.Instance.entries[i].text);
			}
		}
		catch (Exception ex)
		{
			return "LOG ERROR";
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
	public static void exception(Exception e)
	{
		if (!Main.UseLogCallback)
		{
			if (CVars.d_showerror)
			{
				Debug.LogError(e.ToString());
			}
		}
		else
		{
			SingletoneForm<global::Console>.Instance.LogCallback(string.Empty, e.ToString(), LogType.Exception);
			if (Main.UserInfo.Permission >= EPermission.Moder)
			{
				SingletoneForm<global::Console>.Instance.Show(0.5f, 0f);
			}
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000F32C File Offset: 0x0000D52C
	public static void webalert(object str, Color? color = null)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), color);
		Application.ExternalEval(str.ToString());
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000F358 File Offset: 0x0000D558
	public static void WriteLine(string str, Color? color = null)
	{
		SingletoneForm<global::Console>.Instance.printI(str, color);
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000F368 File Offset: 0x0000D568
	public static void print(Exception e)
	{
		global::Console.exception(e);
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000F370 File Offset: 0x0000D570
	public static void print(string str)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), null);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000F398 File Offset: 0x0000D598
	public static void print(int str)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), null);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
	public static void print(float str)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), null);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
	public static void print(double str)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), null);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000F410 File Offset: 0x0000D610
	public static void print(char str)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), null);
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000F438 File Offset: 0x0000D638
	public static void print(byte str)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), null);
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000F460 File Offset: 0x0000D660
	public static void print(string str, Color color)
	{
		if (!Peer.Dedicated)
		{
			SingletoneForm<global::Console>.Instance.printI(str.ToString(), new Color?(color));
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000F490 File Offset: 0x0000D690
	public static void print(int str, Color color)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), new Color?(color));
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000F4AC File Offset: 0x0000D6AC
	public static void print(float str, Color color)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), new Color?(color));
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
	public static void print(double str, Color color)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), new Color?(color));
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000F4E4 File Offset: 0x0000D6E4
	public static void print(char str, Color color)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), new Color?(color));
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000F500 File Offset: 0x0000D700
	public static void print(byte str, Color color)
	{
		SingletoneForm<global::Console>.Instance.printI(str.ToString(), new Color?(color));
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000F51C File Offset: 0x0000D71C
	private void printI(string str, Color? color = null)
	{
		this.printI_base(str, color, LogType.Log);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000F528 File Offset: 0x0000D728
	private void printI_base(string str, Color? color = null, LogType type = LogType.Log)
	{
		if (color == null)
		{
			color = new Color?(Color.white);
		}
		LogEntry logEntry = new LogEntry();
		logEntry.header = str.Split(new char[]
		{
			'\n'
		})[0];
		logEntry.text = str;
		logEntry.color = color.Value;
		logEntry.opened = false;
		if (type == LogType.Error || type == LogType.Exception)
		{
			logEntry.IsErrorEntry = true;
			logEntry.LastTime = "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
		}
		else
		{
			logEntry.LastTime = string.Empty;
		}
		for (int i = 0; i < this.entries.Count; i++)
		{
			if (this.entries[i].header == logEntry.header && this.entries[i].text == logEntry.text)
			{
				if (logEntry.IsErrorEntry)
				{
					this.entries[i].LastTime = "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
				}
				this.entries[i].count++;
				return;
			}
		}
		logEntry.OnScreeenResolutionChanged();
		this.entries.Add(logEntry);
		if (this.entries.Count > 128)
		{
			this.entries.RemoveAt(0);
		}
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
	private void LogCallback(string text, string stackTrace, LogType type)
	{
		if (type == LogType.Error)
		{
			this.printI_base(string.Concat(new string[]
			{
				"[",
				type.ToString(),
				"]: ",
				text,
				"\n\n",
				stackTrace
			}), new Color?(Color.red), type);
		}
		else if (type == LogType.Exception)
		{
			this.printI_base(string.Concat(new string[]
			{
				"[",
				type.ToString(),
				"]: ",
				text,
				"\n\n",
				stackTrace
			}), new Color?(new Color(1f, 0.4f, 0.4f)), type);
		}
		else if (type == LogType.Assert)
		{
			this.printI_base(string.Concat(new string[]
			{
				"[",
				type.ToString(),
				"]: ",
				text,
				"\n\n",
				stackTrace
			}), new Color?(Color.gray), type);
		}
		if (type == LogType.Warning && CVars.n_httpDebug)
		{
			this.printI_base("[" + type.ToString() + "]: " + text, new Color?(Color.yellow), type);
		}
		if (type == LogType.Log && CVars.n_httpDebug)
		{
			this.printI_base(text, new Color?(Color.white), type);
		}
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000F838 File Offset: 0x0000DA38
	private void ParseLine(string line)
	{
		string[] array = line.Split(new char[]
		{
			' ',
			'='
		});
		string text = array[0];
		string text2 = string.Empty;
		string[] array2 = new string[array.Length - 1];
		for (int i = 1; i < array.Length; i++)
		{
			array2[i - 1] = array[i];
		}
		if (array.Length > 1)
		{
			text2 = array[1];
		}
		string text3 = text;
		switch (text3)
		{
		case "match":
		case "round":
		case "start":
		case "stop":
		case "restart":
		case "server_targetframerate":
		case "boots":
		case "frog":
		case "mortar":
		case "sonar":
		case "grenade":
		case "health":
		case "armor":
		case "bearwin":
		case "usecwin":
		case "kick":
		case "close":
		case "nw_info":
		case "admin_pain":
		case "nw_h":
			if (Peer.ClientGame && Main.UserInfo.Permission >= EPermission.Admin)
			{
				global::Console.print(line, Colors.consoleServerCmds);
				Peer.ClientGame.LocalPlayer.AdminCommand(line);
			}
			return;
		case "ss":
			if (Peer.ClientGame && Main.UserInfo.Permission >= EPermission.Moder)
			{
				global::Console.print(line, Colors.consoleServerCmds);
				Peer.ClientGame.LocalPlayer.AdminCommand(line);
			}
			return;
		case "ban":
			if (Main.UserInfo.Permission >= EPermission.Moder)
			{
				if (Main.UserInfo.Permission >= EPermission.Admin)
				{
					global::Console.print(line, Colors.consoleServerCmds);
				}
				int num2 = Convert.ToInt32(array[1]);
				int num3 = Convert.ToInt32(array[2]);
				string text4 = this.cmd.Replace(array[0], string.Empty).Replace(array[1], string.Empty).Replace(array[2], string.Empty).Remove(0, 2);
				Main.AddDatabaseRequest<Ban>(new object[]
				{
					num2,
					num3,
					text4
				});
				Peer.ClientGame.LocalPlayer.AdminCommand(line);
			}
			return;
		case "sban":
			if (Main.UserInfo.Permission >= EPermission.Admin)
			{
				global::Console.print(line, Colors.consoleServerCmds);
				Main.AddDatabaseRequest<SBan>(new object[]
				{
					Convert.ToInt32(array[1]),
					Convert.ToInt32(array[2]),
					this.cmd.Replace(array[0], string.Empty).Replace(array[1], string.Empty).Replace(array[2], string.Empty).Remove(0, 2)
				});
			}
			return;
		case "clear":
			this.entries.Clear();
			return;
		}
		FieldInfo[] fields = typeof(CVars).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		for (int j = 0; j < fields.Length; j++)
		{
			if (text.CompareTo(fields[j].Name) == 0)
			{
				object[] customAttributes = fields[j].GetCustomAttributes(typeof(CVar), true);
				if (customAttributes.Length != 0 && customAttributes[0] is CVar)
				{
					if ((customAttributes[0] as CVar).Permission <= Main.UserInfo.Permission)
					{
						if (text2 == string.Empty)
						{
							global::Console.print(line + " " + fields[j].GetValue(null), Colors.consoleServerCmds);
						}
						else
						{
							global::Console.print(line, Colors.consoleServerCmds);
							if (fields[j].GetValue(null) is int || fields[j].GetValue(null) is ObscuredInt)
							{
								fields[j].SetValue(null, Convert.ToInt32(text2));
							}
							if (fields[j].GetValue(null) is float)
							{
								fields[j].SetValue(null, Convert.ToSingle(text2));
							}
							if (fields[j].GetValue(null) is bool)
							{
								fields[j].SetValue(null, Convert.ToBoolean(text2));
							}
							if (fields[j].GetValue(null) is string)
							{
								fields[j].SetValue(null, text2);
							}
							if (fields[j].GetValue(null).GetType() == typeof(Enum))
							{
								fields[j].SetValue(null, Convert.ToInt32(text2));
							}
						}
						return;
					}
				}
			}
		}
		MethodInfo[] methods = typeof(CVars).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		for (int k = 0; k < methods.Length; k++)
		{
			if (text.CompareTo(methods[k].Name) == 0)
			{
				object[] customAttributes2 = methods[k].GetCustomAttributes(typeof(CVarFunction), true);
				if (customAttributes2.Length != 0 && customAttributes2[0] is CVarFunction)
				{
					if ((customAttributes2[0] as CVarFunction).Permission <= Main.UserInfo.Permission)
					{
						global::Console.print(line, Colors.consoleServerCmds);
						CVars.line = line;
						CVars.val = text2;
						methods[k].Invoke(null, new object[]
						{
							array2
						});
						return;
					}
				}
			}
		}
		Action<string[]> action;
		if (Main.UserInfo.Permission >= EPermission.Admin && this._commands.TryGetValue(text, out action))
		{
			action(array2);
			return;
		}
		global::Console.print("[Warning] unknown command: " + line);
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
	public static void AddCommand(string commandName, Action<string[]> action)
	{
		global::Console instance = SingletoneForm<global::Console>.Instance;
		commandName = commandName.ToLower();
		if (instance._commands.ContainsKey(commandName))
		{
			instance._commands[commandName] = action;
			return;
		}
		instance._commands.Add(commandName, action);
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060001DB RID: 475 RVA: 0x0000FF08 File Offset: 0x0000E108
	public override Rect Rect
	{
		get
		{
			return new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000FF28 File Offset: 0x0000E128
	protected override void Awake()
	{
		if (Main.UseLogCallback)
		{
			Application.RegisterLogCallback(new Application.LogCallback(this.LogCallback));
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000FF48 File Offset: 0x0000E148
	public override void Clear()
	{
		this.entries.Clear();
		this.cmd = string.Empty;
		this.index = -1;
		this.setFocus = false;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000FF7C File Offset: 0x0000E17C
	public override void MainInitialize()
	{
		this.isUpdating = true;
		this.isRendering = true;
		base.MainInitialize();
		base.InvokeRepeating("ChangeWind1", 0f, 5f);
		base.InvokeRepeating("ChangeWind2", 0f, 10f);
		if (!this.mat && this.gui)
		{
			this.mat = new Material(this.gui.mat);
		}
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00010000 File Offset: 0x0000E200
	public override void OnDestroy()
	{
		if (this.mat)
		{
			UnityEngine.Object.DestroyImmediate(this.mat);
			this.mat = null;
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00010030 File Offset: 0x0000E230
	public override void PreGUI()
	{
		if (Forms.chatLock)
		{
			return;
		}
		if (this.gui.GetKeyDown(KeyCode.BackQuote) || this.gui.GetKeyDown(KeyCode.F8))
		{
			if (!base.Visible)
			{
				this.setFocus = true;
				this.Show(0.5f, 0f);
			}
			else
			{
				this.Hide(0.35f);
			}
		}
		if (global::Console.hideme)
		{
			this.Hide(0.35f);
			global::Console.hideme = false;
		}
		if (Input.GetMouseButtonDown(0))
		{
			Rect rect = new Rect(0f, (float)(Screen.height / 2), (float)Screen.width, 30f);
			if (!rect.Contains(Input.mousePosition))
			{
				this.setFocus = false;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			Rect rect2 = new Rect(0f, (float)(Screen.height / 2), (float)Screen.width, 30f);
			if (rect2.Contains(Input.mousePosition))
			{
				this.setFocus = true;
			}
		}
		if (this.gui.GetKeyDown(KeyCode.Return))
		{
			if (this._commandLog.Count > this._commandLogLenght)
			{
				this._commandLog.RemoveAt(0);
			}
			this.index = this._commandLog.Count + 1;
			this._commandLog.Add(this.cmd);
			try
			{
				this.ParseLine(this.cmd);
			}
			catch (Exception e)
			{
				global::Console.exception(e);
			}
			this.cmd = string.Empty;
			this.scrollPos.y = (float)this.pos;
		}
		if (this.gui.GetKeyDown(KeyCode.UpArrow))
		{
			this.index--;
			if (this.index < 0)
			{
				this.index = 0;
			}
			if (this._commandLog.Count > this.index && this.index >= 0)
			{
				this.cmd = this._commandLog[this.index];
			}
			else
			{
				this.cmd = string.Empty;
			}
		}
		if (this.gui.GetKeyDown(KeyCode.DownArrow) && this.index != -1)
		{
			this.index++;
			if (this.index < 0)
			{
				this.index = 0;
			}
			if (this._commandLog.Count > this.index && this.index >= 0)
			{
				this.cmd = this._commandLog[this.index];
			}
			if (this.index > this._commandLog.Count)
			{
				this.index = this._commandLog.Count;
			}
		}
		if (this.gui.GetKeyDown(KeyCode.Escape))
		{
			this.index = this._commandLog.Count;
			this.cmd = string.Empty;
			GUI.FocusControl("Console Edit");
			GUI.SetNextControlName("Console Edit");
		}
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00010354 File Offset: 0x0000E554
	public override void LateGUI()
	{
		if (Main.UserInfo.banned != 0)
		{
			return;
		}
		if (Event.current.type == EventType.Repaint)
		{
			Color color = this.mat.color;
			this.mat.color = Colors.alpha(Color.black, base.visibility);
			this.slide1Velocity = (this.slide1Velocity + this.slide1RandVelocity).normalized * Mathf.Min(this.slide1RandVelocity.magnitude, this.slide1Velocity.magnitude);
			this.slide1Position += this.slide1Velocity * Time.deltaTime * 4f;
			this.mat.mainTextureOffset = this.slide1Position;
			this.mat.mainTextureScale = new Vector2(2f * (float)Screen.width / 800f * 0.75f * 2f, (float)Screen.height / 600f * 0.75f * 2f);
			Graphics.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)(Screen.height / 2)), this.gui.slide1, new Rect(0f, 0f, 1f, 1f), 0, 0, 0, 0, Colors.alpha(Color.black, base.visibility), this.mat);
			this.slide2Velocity = (this.slide2Velocity + this.slide2RandVelocity).normalized * Mathf.Min(this.slide2RandVelocity.magnitude, this.slide2Velocity.magnitude);
			this.slide2Position += this.slide2Velocity * Time.deltaTime * 10f;
			this.mat.mainTextureOffset = this.slide2Position;
			this.mat.mainTextureScale = new Vector2(2f * (float)Screen.width / 800f * 0.75f * 2f, (float)Screen.height / 600f * 0.75f * 2f);
			Graphics.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)(Screen.height / 2)), this.gui.slide2, new Rect(0f, 0f, 1f, 1f), 0, 0, 0, 0, Colors.alpha(Color.black, base.visibility), this.mat);
			this.mat.mainTextureOffset = Vector2.zero;
			this.mat.mainTextureScale = Vector2.one;
			this.mat.color = color;
		}
		this.gui.color = Colors.alpha(Color.white, base.visibility);
		GUI.color = Colors.alpha(Color.white, 1f);
		this.gui.color = Colors.alpha(Color.white, base.visibility * 0.15f);
		GUI.DrawTexture(new Rect(0f, (float)(Screen.height / 2 - 30), (float)Screen.width, 30f), this.gui.blue);
		this.gui.color = Colors.alpha(Color.white, base.visibility);
		this.gui.textMaxSize = 1024;
		GUI.SetNextControlName("Console Edit");
		this.cmd = GUI.TextField(new Rect(10f, (float)(Screen.height / 2 - 30), (float)(Screen.width - 20), 30f), this.cmd, 200, this.TextStyle).Replace("`", string.Empty).Replace("\n", string.Empty);
		if (this.setFocus)
		{
			GUI.FocusControl("Console Edit");
			GUI.SetNextControlName("Console Edit");
		}
		this.gui.textMaxSize = 16;
		this.gui.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height / 2f), false);
		this.scrollPos = this.gui.BeginScrollView(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height / 2f - 30f), this.scrollPos, new Rect(0f, 0f, (float)Screen.width - 50f, (float)(this.pos + 80)), float.MaxValue);
		this.gui.textMaxSize = int.MaxValue;
		this.pos = 0;
		bool flag = false;
		if (this._screenWidth != Screen.width)
		{
			this._screenWidth = Screen.width;
			flag = true;
		}
		for (int i = 0; i < this.entries.Count; i++)
		{
			if (flag)
			{
				this.entries[i].OnScreeenResolutionChanged();
			}
			this.gui.color = Colors.alpha(this.entries[i].color, base.visibility);
			float openedHeight = this.entries[i].OpenedHeight;
			this.gui.TextField(new Rect(5f, (float)(5 + this.pos), (float)Screen.width - 25f, openedHeight), this.entries[i].LogText, 9, "#FFFFFF_Tahoma", TextAnchor.UpperLeft, true, true);
			this.gui.color = Colors.alpha(Color.white, base.visibility * 0.25f);
			this.pos += (int)openedHeight;
		}
		this.gui.textMaxSize = 16;
		this.gui.EndScrollView();
		string text = string.Concat(new object[]
		{
			"UserID: ",
			Main.UserInfo.userID,
			"\t FPS: ",
			global::Console.fps.Count.ToString()
		});
		if (CVars.d_rate)
		{
			text = string.Concat(new string[]
			{
				global::Console.fps.Count.ToString(),
				"(updates)/",
				Application.targetFrameRate.ToString(),
				"(TFR)   ",
				this.fixes.Count.ToString(),
				"(fixed updates)/",
				CVars.g_tickrate.ToString(),
				"(TR)"
			});
			GUI.Label(new Rect((float)(Screen.width - 355), (float)(Screen.height / 2 - 28), 350f, 20f), text);
		}
		else
		{
			GUI.Label(new Rect((float)(Screen.width - 190), (float)(Screen.height / 2 - 28), 180f, 20f), text, this.FpsStyle);
		}
		this.gui.EndGroup();
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00010A54 File Offset: 0x0000EC54
	public static void HideMe()
	{
		global::Console.hideme = true;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x00010A5C File Offset: 0x0000EC5C
	public override void OnUpdate()
	{
		global::Console.fps.Advance();
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00010A68 File Offset: 0x0000EC68
	public override void OnFixedUpdate()
	{
	}

	// Token: 0x04000242 RID: 578
	private Material mat;

	// Token: 0x04000243 RID: 579
	private List<string> _commandLog = new List<string>();

	// Token: 0x04000244 RID: 580
	private int _commandLogLenght = 10;

	// Token: 0x04000245 RID: 581
	private List<LogEntry> entries = new List<LogEntry>();

	// Token: 0x04000246 RID: 582
	private string cmd = string.Empty;

	// Token: 0x04000247 RID: 583
	private int index = -1;

	// Token: 0x04000248 RID: 584
	[SerializeField]
	public ConsoleScrollList consoleList = new ConsoleScrollList();

	// Token: 0x04000249 RID: 585
	[SerializeField]
	public GUIStyle HeadersStyle = new GUIStyle();

	// Token: 0x0400024A RID: 586
	[SerializeField]
	public GUIStyle TextStyle = new GUIStyle();

	// Token: 0x0400024B RID: 587
	[SerializeField]
	public GUIStyle FpsStyle = new GUIStyle();

	// Token: 0x0400024C RID: 588
	public static FpsCounter fps = new FpsCounter();

	// Token: 0x0400024D RID: 589
	private FpsCounter fixes = new FpsCounter();

	// Token: 0x0400024E RID: 590
	private bool setFocus;

	// Token: 0x0400024F RID: 591
	private int _screenWidth;

	// Token: 0x04000250 RID: 592
	private Dictionary<string, Action<string[]>> _commands = new Dictionary<string, Action<string[]>>();

	// Token: 0x04000251 RID: 593
	private int pos;

	// Token: 0x04000252 RID: 594
	private Vector2 scrollPos = Vector2.zero;

	// Token: 0x04000253 RID: 595
	private Vector2 slide1RandVelocity = Vector2.zero;

	// Token: 0x04000254 RID: 596
	private Vector2 slide2RandVelocity = Vector2.zero;

	// Token: 0x04000255 RID: 597
	private Vector2 slide1Velocity = Vector2.one;

	// Token: 0x04000256 RID: 598
	private Vector2 slide2Velocity = Vector2.one;

	// Token: 0x04000257 RID: 599
	private Vector2 slide1Position = Vector2.zero;

	// Token: 0x04000258 RID: 600
	private Vector2 slide2Position = Vector2.zero;

	// Token: 0x04000259 RID: 601
	protected static bool hideme = false;
}
