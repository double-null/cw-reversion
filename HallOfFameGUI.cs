using System;
using System.Collections.Generic;
using System.Reflection;
using HallOfFameNamespace;
using UnityEngine;

// Token: 0x02000153 RID: 339
[Obfuscation(Exclude = true, ApplyToMembers = true)]
internal class HallOfFameGUI : Form
{
	// Token: 0x17000110 RID: 272
	// (get) Token: 0x0600084F RID: 2127 RVA: 0x0004A604 File Offset: 0x00048804
	public override int Width
	{
		get
		{
			return this.gui.Width;
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000850 RID: 2128 RVA: 0x0004A614 File Offset: 0x00048814
	public override int Height
	{
		get
		{
			return this.gui.Height;
		}
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0004A624 File Offset: 0x00048824
	public void Parse(Dictionary<string, object>[] data, int y, int m, bool lA, bool rA)
	{
		this.year = y;
		this.month = m;
		this.leftArrow = lA;
		this.rightArrow = rA;
		if (this.storage.InStorage(y, m))
		{
			this.units = this.storage.GetStore(y, m);
		}
		else
		{
			this.ParseDict(this.units, data);
			this.storage.Add(this.units, y, m);
		}
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0004A69C File Offset: 0x0004889C
	private void ParseDict(List<HallOfFameUnit> HOFunits, Dictionary<string, object>[] data)
	{
		HOFunits.Clear();
		int i = 0;
		int num = 0;
		int num2 = 0;
		while (i < data.Length)
		{
			HallOfFameUnit hallOfFameUnit = new HallOfFameUnit();
			hallOfFameUnit.Parse(data[i]);
			if (hallOfFameUnit.HeaderInfo == null && i < Language.HallOfFameHeader.Length)
			{
				hallOfFameUnit.HeaderInfo = Language.HallOfFameHeader[i];
			}
			hallOfFameUnit.SetPos(20 + num * 265, 110 + 125 * num2);
			num2++;
			if ((i + 1) % 4 == 0)
			{
				num2 = 0;
				num++;
			}
			this.SetDefault(hallOfFameUnit, i);
			HOFunits.Add(hallOfFameUnit);
			i++;
		}
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0004A738 File Offset: 0x00048938
	private void SetDefault(HallOfFameUnit unit, int i)
	{
		if (i < Language.HallOfFameHeader.Length)
		{
			unit.HeaderInfo = Language.HallOfFameHeader[i];
		}
		if (i < this.sameIcon.Length)
		{
			unit.sameImg = this.sameIcon[i];
		}
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0004A77C File Offset: 0x0004897C
	[Obfuscation(Exclude = true)]
	public void ShowHallOfFameGUI(object obj)
	{
		Main.AddDatabaseRequest<HallOfFameRequest>(new object[]
		{
			this
		});
		this.Show(0.5f, 0f);
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0004A7A0 File Offset: 0x000489A0
	[Obfuscation(Exclude = true)]
	public void HideHallOfFameGUI(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0004A7B0 File Offset: 0x000489B0
	private void HideTab()
	{
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0004A7B4 File Offset: 0x000489B4
	private void ExitWindowButton()
	{
		if (this.gui.Button(new Vector2((float)(this.Width - this.gui.server_window[3].width), 0f), this.gui.server_window[3], this.gui.server_window[4], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.HideHallOfFameGUI(null);
		}
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0004A840 File Offset: 0x00048A40
	private void DrawMainPicture(Texture2D picture, float pictureAlpha)
	{
		this.gui.color = Colors.alpha(this.gui.color, pictureAlpha * base.visibility);
		this.gui.PictureCentered(new Vector2((float)(this.Width / 2), (float)(this.Height / 2)), picture, new Vector2(1f, 1f));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0004A8C4 File Offset: 0x00048AC4
	private bool KrutilkaOnLoading()
	{
		float angle = 180f * Time.realtimeSinceStartup * 1.5f;
		Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
		this.gui.RotateGUI(angle, new Vector2(rect.center.x, rect.center.y));
		this.gui.Picture(new Vector2(rect.center.x - (float)(this.gui.settings_window[9].width / 2), rect.center.y - (float)(this.gui.settings_window[9].height / 2)), this.gui.settings_window[9]);
		this.gui.RotateGUI(0f, Vector2.zero);
		return false;
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0004A9E0 File Offset: 0x00048BE0
	public override void MainInitialize()
	{
		this.isRendering = true;
		base.MainInitialize();
		HallOfFameUnit.TabArrows = this.TabArrows;
		HallOfFameUnit.infoBtn = this.infoBtn;
		HallOfFameUnit.voteBtn = this.voteBtn;
		HallOfFameUnit.gui = this.gui;
		HallOfFameUnit.halloffame_back = this.halloffame_back;
		HallOfFameUnit.avatarSample = this.avatarSample;
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0004AA3C File Offset: 0x00048C3C
	private void DrawingTab()
	{
		this.gui.TextField(new Rect(210f, 5f, 450f, 25f), "ЛУЧШИЕ ИГРОКИ " + HOFDataToStr.GetData(this.month, this.year).ToUpper(), 14, "#ffffff_Micra", TextAnchor.UpperLeft, false, false);
		if (this.leftArrow)
		{
			if (this.gui.Button(new Vector2(115f, 0f), this.TabArrows[0], this.TabArrows[1], this.TabArrows[0], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				Main.AddDatabaseRequest<HallOfFameRequest>(new object[]
				{
					this,
					1
				});
			}
			this.gui.TextField(new Rect(100f, 33f, 100f, 25f), HOFDataToStr.GetData(this.month - 1, this.year), 9, "#a1a1a1_Tahoma", TextAnchor.UpperCenter, false, false);
		}
		if (this.rightArrow)
		{
			if (this.gui.Button(new Vector2(630f, 0f), this.TabArrows[2], this.TabArrows[3], this.TabArrows[2], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				Main.AddDatabaseRequest<HallOfFameRequest>(new object[]
				{
					this,
					2
				});
			}
			this.gui.TextField(new Rect(610f, 33f, 100f, 25f), HOFDataToStr.GetData(this.month + 1, this.year), 9, "#a1a1a1_Tahoma", TextAnchor.UpperCenter, false, false);
		}
		this.gui.TextField(new Rect(260f, 33f, 270f, 25f), "Зачисление в зал славы происходит  в конце каждого месяца", 9, "#a1a1a1_Tahoma", TextAnchor.UpperCenter, false, false);
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0004AC58 File Offset: 0x00048E58
	private void DrawUnits()
	{
		for (int i = 0; i < this.units.Count; i++)
		{
			this.units[i].Show();
		}
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0004AC94 File Offset: 0x00048E94
	public override void OnInitialized()
	{
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0004AC98 File Offset: 0x00048E98
	public override void OnDestroy()
	{
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0004AC9C File Offset: 0x00048E9C
	public override void Clear()
	{
		base.Clear();
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0004ACA4 File Offset: 0x00048EA4
	public override void Register()
	{
		EventFactory.Register("ShowHallOfFameGUI", this);
		EventFactory.Register("HideHallOfFameGUI", this);
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0004ACBC File Offset: 0x00048EBC
	public override void InterfaceGUI()
	{
		this.gui.color = Colors.alpha(Color.white, 0.9f * base.visibility);
		this.gui.PictureSized(new Vector2(0f, 0f), this.gui.black, new Vector2((float)Screen.width, (float)Screen.height));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		Rect rect = new Rect((float)((Screen.width - this.gui.Width) / 2), (float)((Screen.height - this.gui.Height) / 2), (float)this.Width, (float)this.Height);
		this.gui.BeginGroup(rect, this.windowID != this.gui.FocusedWindow);
		this.ExitWindowButton();
		this.gui.TextField(new Rect(0f, 0f, 100f, 20f), "ЗАЛ СЛАВЫ", 16, "#ffffff", TextAnchor.UpperCenter, false, false);
		this.DrawingTab();
		this.DrawUnits();
		this.gui.EndGroup();
	}

	// Token: 0x0400096A RID: 2410
	private bool leftArrow = true;

	// Token: 0x0400096B RID: 2411
	private bool rightArrow;

	// Token: 0x0400096C RID: 2412
	private HallOfFameStore storage = new HallOfFameStore();

	// Token: 0x0400096D RID: 2413
	private List<HallOfFameUnit> units = new List<HallOfFameUnit>();

	// Token: 0x0400096E RID: 2414
	private int month;

	// Token: 0x0400096F RID: 2415
	private int year;

	// Token: 0x04000970 RID: 2416
	public Texture2D halloffame_back;

	// Token: 0x04000971 RID: 2417
	public Texture2D[] TabArrows;

	// Token: 0x04000972 RID: 2418
	public Texture2D[] voteBtn;

	// Token: 0x04000973 RID: 2419
	public Texture2D[] infoBtn;

	// Token: 0x04000974 RID: 2420
	public Texture2D[] sameIcon;

	// Token: 0x04000975 RID: 2421
	public Texture2D avatarSample;
}
