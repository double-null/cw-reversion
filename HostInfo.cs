using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029B RID: 667
[Serializable]
internal class HostInfo : global::HostData, Convertible
{
	// Token: 0x1700028A RID: 650
	// (get) Token: 0x0600129B RID: 4763 RVA: 0x000CB584 File Offset: 0x000C9784
	public bool IsProtected
	{
		get
		{
			return this._platform == Realms.Standalone;
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x0600129C RID: 4764 RVA: 0x000CB590 File Offset: 0x000C9790
	// (set) Token: 0x0600129D RID: 4765 RVA: 0x000CB598 File Offset: 0x000C9798
	public int RealPing { get; private set; }

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x0600129E RID: 4766 RVA: 0x000CB5A4 File Offset: 0x000C97A4
	// (set) Token: 0x0600129F RID: 4767 RVA: 0x000CB5AC File Offset: 0x000C97AC
	public bool SkipInQuickGame
	{
		get
		{
			return this._skipInQuickGame;
		}
		set
		{
			this._skipInQuickGame = value;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000CB5B8 File Offset: 0x000C97B8
	// (set) Token: 0x060012A1 RID: 4769 RVA: 0x000CB5C0 File Offset: 0x000C97C0
	public bool TestVip
	{
		get
		{
			return this._testVip;
		}
		set
		{
			this._testVip = value;
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000CB5CC File Offset: 0x000C97CC
	public float ExpCoef
	{
		get
		{
			if (Globals.I.maps.Length <= this._mapIndex || this._mapIndex < 0)
			{
				return 1f;
			}
			return Globals.I.maps[this._mapIndex].GameModes[this._gameMode].GetExpCoefByRealm(this._platform);
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x060012A3 RID: 4771 RVA: 0x000CB630 File Offset: 0x000C9830
	// (set) Token: 0x060012A4 RID: 4772 RVA: 0x000CB638 File Offset: 0x000C9838
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			this._name = value;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x060012A5 RID: 4773 RVA: 0x000CB644 File Offset: 0x000C9844
	// (set) Token: 0x060012A6 RID: 4774 RVA: 0x000CB64C File Offset: 0x000C984C
	public int PlayerCount
	{
		get
		{
			return this._playerCount;
		}
		set
		{
			this._playerCount = value;
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x060012A7 RID: 4775 RVA: 0x000CB658 File Offset: 0x000C9858
	// (set) Token: 0x060012A8 RID: 4776 RVA: 0x000CB660 File Offset: 0x000C9860
	public int SpectactorCount
	{
		get
		{
			return this._spectactorCount;
		}
		set
		{
			this._spectactorCount = value;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x060012A9 RID: 4777 RVA: 0x000CB66C File Offset: 0x000C986C
	// (set) Token: 0x060012AA RID: 4778 RVA: 0x000CB674 File Offset: 0x000C9874
	public int LoadPlayerCount
	{
		get
		{
			return this._loadPlayerCount;
		}
		set
		{
			this._loadPlayerCount = value;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x060012AB RID: 4779 RVA: 0x000CB680 File Offset: 0x000C9880
	public int ConnectionsCoint
	{
		get
		{
			return this._playerCount + this._spectactorCount + this._loadPlayerCount;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x060012AC RID: 4780 RVA: 0x000CB698 File Offset: 0x000C9898
	public bool IsFullByConnections
	{
		get
		{
			return this.ConnectionsCoint >= this._maxPlayers;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x060012AD RID: 4781 RVA: 0x000CB6AC File Offset: 0x000C98AC
	// (set) Token: 0x060012AE RID: 4782 RVA: 0x000CB6B4 File Offset: 0x000C98B4
	public int MaxPlayers
	{
		get
		{
			return this._maxPlayers;
		}
		set
		{
			if (value > 30)
			{
				value = 30;
			}
			this._maxPlayers = value;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x060012AF RID: 4783 RVA: 0x000CB6CC File Offset: 0x000C98CC
	public bool IsFull
	{
		get
		{
			return this._playerCount >= this._maxPlayers;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x060012B0 RID: 4784 RVA: 0x000CB6E0 File Offset: 0x000C98E0
	// (set) Token: 0x060012B1 RID: 4785 RVA: 0x000CB6E8 File Offset: 0x000C98E8
	public int MinLevel
	{
		get
		{
			return this._minLevel;
		}
		set
		{
			this._minLevel = value;
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x060012B2 RID: 4786 RVA: 0x000CB6F4 File Offset: 0x000C98F4
	// (set) Token: 0x060012B3 RID: 4787 RVA: 0x000CB6FC File Offset: 0x000C98FC
	public int MaxLevel
	{
		get
		{
			return this._maxLevel;
		}
		set
		{
			this._maxLevel = value;
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x060012B4 RID: 4788 RVA: 0x000CB708 File Offset: 0x000C9908
	// (set) Token: 0x060012B5 RID: 4789 RVA: 0x000CB710 File Offset: 0x000C9910
	public int MapIndex
	{
		get
		{
			return this._mapIndex;
		}
		set
		{
			this._mapIndex = value;
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000CB71C File Offset: 0x000C991C
	// (set) Token: 0x060012B7 RID: 4791 RVA: 0x000CB734 File Offset: 0x000C9934
	public string MapName
	{
		get
		{
			return Globals.I.maps[this._mapIndex].Name;
		}
		set
		{
			for (int i = 0; i < Globals.I.maps.Length; i++)
			{
				if (Globals.I.maps[i].Name == value)
				{
					this._mapIndex = i;
				}
			}
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000CB784 File Offset: 0x000C9984
	// (set) Token: 0x060012B9 RID: 4793 RVA: 0x000CB78C File Offset: 0x000C998C
	public GameMode GameMode
	{
		get
		{
			return this._gameMode;
		}
		set
		{
			this._gameMode = value;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x060012BA RID: 4794 RVA: 0x000CB798 File Offset: 0x000C9998
	// (set) Token: 0x060012BB RID: 4795 RVA: 0x000CB7A0 File Offset: 0x000C99A0
	public bool Ranked
	{
		get
		{
			return this._ranked;
		}
		set
		{
			this._ranked = value;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x060012BC RID: 4796 RVA: 0x000CB7AC File Offset: 0x000C99AC
	// (set) Token: 0x060012BD RID: 4797 RVA: 0x000CB7B4 File Offset: 0x000C99B4
	public bool Hardcore
	{
		get
		{
			return this._hardcore;
		}
		set
		{
			this._hardcore = value;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x060012BE RID: 4798 RVA: 0x000CB7C0 File Offset: 0x000C99C0
	public bool Friends
	{
		get
		{
			return this.FriendsList.Count > 0;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x060012BF RID: 4799 RVA: 0x000CB7D0 File Offset: 0x000C99D0
	public string FriendNick
	{
		get
		{
			return (this.FriendsList.Count <= 0) ? string.Empty : this.FriendsList[0];
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x060012C0 RID: 4800 RVA: 0x000CB7FC File Offset: 0x000C99FC
	public int FriendsCount
	{
		get
		{
			return this.FriendsList.Count;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000CB80C File Offset: 0x000C9A0C
	public int Ping
	{
		get
		{
			return this.ping.Ping;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x060012C2 RID: 4802 RVA: 0x000CB81C File Offset: 0x000C9A1C
	// (set) Token: 0x060012C3 RID: 4803 RVA: 0x000CB824 File Offset: 0x000C9A24
	public bool ForceNAT
	{
		get
		{
			return this._forceNat;
		}
		set
		{
			this._forceNat = value;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x060012C4 RID: 4804 RVA: 0x000CB830 File Offset: 0x000C9A30
	// (set) Token: 0x060012C5 RID: 4805 RVA: 0x000CB838 File Offset: 0x000C9A38
	public bool IsHidden
	{
		get
		{
			return this._hidden;
		}
		set
		{
			this._hidden = value;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x060012C6 RID: 4806 RVA: 0x000CB844 File Offset: 0x000C9A44
	// (set) Token: 0x060012C7 RID: 4807 RVA: 0x000CB84C File Offset: 0x000C9A4C
	public bool PasswordProtected
	{
		get
		{
			return this._password;
		}
		set
		{
			this._password = value;
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000CB858 File Offset: 0x000C9A58
	// (set) Token: 0x060012C9 RID: 4809 RVA: 0x000CB860 File Offset: 0x000C9A60
	public string Debug
	{
		get
		{
			return this._debug;
		}
		set
		{
			this._debug = value;
		}
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x000CB86C File Offset: 0x000C9A6C
	public new void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "name", ref this._name, isWrite);
		JSON.ReadWrite(dict, "playerCount", ref this._playerCount, isWrite);
		JSON.ReadWrite(dict, "spectatorCount", ref this._spectactorCount, isWrite);
		JSON.ReadWrite(dict, "loadPlayerCount", ref this._loadPlayerCount, isWrite);
		JSON.ReadWrite(dict, "maxPlayers", ref this._maxPlayers, isWrite);
		JSON.ReadWrite(dict, "minLevel", ref this._minLevel, isWrite);
		JSON.ReadWrite(dict, "maxLevel", ref this._maxLevel, isWrite);
		JSON.ReadWrite(dict, "mapIndex", ref this._mapIndex, isWrite);
		JSON.ReadWriteEnum<GameMode>(dict, "gameMode", ref this._gameMode, isWrite);
		JSON.ReadWrite(dict, "ranked", ref this._ranked, isWrite);
		JSON.ReadWrite(dict, "hardcore", ref this._hardcore, isWrite);
		JSON.ReadWrite(dict, "skip", ref this._skipInQuickGame, isWrite);
		JSON.ReadWrite(dict, "testVip", ref this._testVip, isWrite);
		JSON.ReadWrite(dict, "hidden", ref this._hidden, isWrite);
		JSON.ReadWrite(dict, "forceNAT", ref this._forceNat, isWrite);
		JSON.ReadWrite(dict, "password", ref this._password, isWrite);
		JSON.ReadWrite(dict, "debug", ref this._debug, isWrite);
		JSON.ReadWrite(dict, "ip", ref this.ip, isWrite);
		JSON.ReadWrite(dict, "port", ref this.port, isWrite);
		if (isWrite)
		{
			int num = Utility.IntPlatform();
			JSON.ReadWrite(dict, "platform", ref num, true);
		}
		else
		{
			JSON.ReadWriteEnum<Realms>(dict, "platform", ref this._platform, false);
		}
		if (dict.ContainsKey("friends"))
		{
			foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)dict["friends"]))
			{
				this.FriendsList.Add(keyValuePair.Value.ToString());
			}
		}
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x000CBA84 File Offset: 0x000C9C84
	public bool MyLevelRange(int level, bool isHardcore = false)
	{
		return (level >= this._minLevel || isHardcore) && level <= this._maxLevel;
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000CBAAC File Offset: 0x000C9CAC
	public string OnlineInfo()
	{
		Dictionary<string, object> dict = new Dictionary<string, object>();
		this.Convert(dict, true);
		base.Convert(dict, true);
		return ArrayUtility.ToJSON<string, object>(dict);
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x000CBAD8 File Offset: 0x000C9CD8
	public IEnumerator GetPing()
	{
		if (this == null)
		{
			yield return null;
		}
		if (!this._ipList.Contains(this.ip))
		{
			this._ipList.Add(this.ip);
		}
		this._etr = this._ipList.GetEnumerator();
		while (this._etr.MoveNext())
		{
			Ping p = new Ping(this.ip);
			while (p.isDone)
			{
				yield return null;
			}
			this.RealPing = p.time;
			yield return new WaitForEndOfFrame();
		}
		if (this._currentTime + 1f < Time.realtimeSinceStartup)
		{
			this._etr.Reset();
			this._currentTime = Time.realtimeSinceStartup;
		}
		yield break;
	}

	// Token: 0x0400156A RID: 5482
	private string _name = "New Game";

	// Token: 0x0400156B RID: 5483
	private string _debug = string.Empty;

	// Token: 0x0400156C RID: 5484
	private float _currentTime;

	// Token: 0x0400156D RID: 5485
	private bool _ranked = true;

	// Token: 0x0400156E RID: 5486
	private bool _hardcore;

	// Token: 0x0400156F RID: 5487
	private bool _hidden;

	// Token: 0x04001570 RID: 5488
	private bool _forceNat;

	// Token: 0x04001571 RID: 5489
	private bool _password;

	// Token: 0x04001572 RID: 5490
	private bool _skipInQuickGame;

	// Token: 0x04001573 RID: 5491
	private bool _testVip;

	// Token: 0x04001574 RID: 5492
	private int _friendsCount;

	// Token: 0x04001575 RID: 5493
	private int _playerCount;

	// Token: 0x04001576 RID: 5494
	private int _spectactorCount;

	// Token: 0x04001577 RID: 5495
	private int _loadPlayerCount;

	// Token: 0x04001578 RID: 5496
	private int _maxPlayers = 30;

	// Token: 0x04001579 RID: 5497
	private int _minLevel;

	// Token: 0x0400157A RID: 5498
	private int _maxLevel = 70;

	// Token: 0x0400157B RID: 5499
	private int _mapIndex;

	// Token: 0x0400157C RID: 5500
	private IEnumerator _etr;

	// Token: 0x0400157D RID: 5501
	private GameMode _gameMode = GameMode.dontKnow;

	// Token: 0x0400157E RID: 5502
	private Realms _platform = Realms.WrongRealm;

	// Token: 0x0400157F RID: 5503
	private List<string> _ipList = new List<string>();

	// Token: 0x04001580 RID: 5504
	public List<string> FriendsList = new List<string>();

	// Token: 0x04001581 RID: 5505
	public PingSample ping;

	// Token: 0x04001582 RID: 5506
	public string ExternalIp = string.Empty;
}
