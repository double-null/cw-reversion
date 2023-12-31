using System;
using System.Collections.Generic;
using System.Reflection;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x020002A8 RID: 680
[Serializable]
internal class PlayerInfo : cwNetworkSerializable, Convertible, ReusableClass<PlayerInfo>
{
	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06001324 RID: 4900 RVA: 0x000CE884 File Offset: 0x000CCA84
	// (set) Token: 0x06001325 RID: 4901 RVA: 0x000CE8DC File Offset: 0x000CCADC
	public float Health
	{
		get
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (!callingAssembly.Equals(Assembly.GetExecutingAssembly()))
			{
				SingletoneComponent<Main>.Instance.StartCoroutine(SingletoneComponent<Main>.Instance.TakeAndSendScreenshot("gayCheck4", Main.UserInfo.userID));
			}
			return this._health;
		}
		set
		{
			this._health = value;
		}
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06001326 RID: 4902 RVA: 0x000CE8EC File Offset: 0x000CCAEC
	// (set) Token: 0x06001327 RID: 4903 RVA: 0x000CE8FC File Offset: 0x000CCAFC
	public string Nick
	{
		get
		{
			return this._nick;
		}
		set
		{
			this._nick = value;
		}
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x000CE90C File Offset: 0x000CCB0C
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"Warning!\n UserID: ",
			this.userID,
			" Nick: ",
			this._nick,
			" Level: ",
			this.level
		});
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x000CE968 File Offset: 0x000CCB68
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		string value = this._nick;
		JSON.ReadWrite(dict, "nick", ref value, isWrite);
		this._nick = value;
		JSON.ReadWrite(dict, "perk_states", ref this.NickColor, isWrite);
		if (!this.NickColor.StartsWith("#"))
		{
			this.NickColor = "#" + this.NickColor;
		}
		JSON.ReadWrite(dict, "clanTag", ref this.clanTag, isWrite);
		JSON.ReadWrite(dict, "isHost", ref this.isHost, isWrite);
		JSON.ReadWrite(dict, "userID", ref this.userID, isWrite);
		JSON.ReadWrite(dict, "playerID", ref this.playerID, isWrite);
		JSON.ReadWriteEnum<PlayerType>(dict, "playerType", ref this.playerType, isWrite);
		JSON.ReadWriteEnum<PlayerClass>(dict, "playerClass", ref this.playerClass, isWrite);
		JSON.ReadWrite(dict, "ping", ref this.ping, isWrite);
		JSON.ReadWrite(dict, "clientHalfPing", ref this.clientHalfPing, isWrite);
		JSON.ReadWrite(dict, "packetLatency", ref this.packetLatency, isWrite);
		JSON.ReadWrite(dict, "dead", ref this.dead, isWrite);
		float value2 = this._health;
		JSON.ReadWrite(dict, "health", ref value2, isWrite);
		this._health = value2;
		JSON.ReadWrite(dict, "armor", ref this.armor, isWrite);
		JSON.ReadWrite(dict, "suspectRatio", ref this.IsSuspected, isWrite);
		JSON.ReadWrite(dict, "killCount", ref this.killCount, isWrite);
		JSON.ReadWrite(dict, "deathCount", ref this.deathCount, isWrite);
		JSON.ReadWrite(dict, "level", ref this.level, isWrite);
		JSON.ReadWrite(dict, "points", ref this.points, isWrite);
		JSON.ReadWriteEnum<Buffs>(dict, "buffs", ref this.buffs, isWrite);
		JSON.ReadWrite(dict, "loading", ref this.loading, isWrite);
		JSON.ReadWrite(dict, "isModerator", ref this.isModerator, isWrite);
		JSON.ReadWrite(dict, "hasMortar", ref this.hasMortar, isWrite);
		JSON.ReadWrite(dict, "hasSonar", ref this.hasSonar, isWrite);
		JSON.ReadWrite(dict, "number", ref this.number, isWrite);
		JSON.ReadWrite(dict, "skillsInfos", ref this.skillsInfos, isWrite);
		JSON.ReadWrite(dict, "clan_skills", ref this.clanSkillsInfos, isWrite);
		JSON.ReadWrite(dict, "Camouflages", ref this.Camouflages, isWrite);
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x000CEBC0 File Offset: 0x000CCDC0
	public void Serialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.playerID);
		this.cPlayerType = (char)this.playerType;
		stream.Serialize(ref this.cPlayerType);
		BoolToInt boolToInt = new BoolToInt(0);
		boolToInt.Push(this.dead);
		boolToInt.Push(this.hasMortar);
		boolToInt.Push(this.hasSonar);
		boolToInt.Push(this.isModerator);
		stream.Serialize(ref boolToInt.Compressed);
		string value = this._nick;
		stream.Serialize(UpdateType.FullUpdate, ref value);
		this._nick = value;
		stream.Serialize(UpdateType.FullUpdate, ref this.NickColor);
		stream.Serialize(UpdateType.FullUpdate, ref this.clanTag);
		stream.Serialize<PlayerClass>(UpdateType.FullUpdate, ref this.playerClass);
		stream.Serialize(UpdateType.FullUpdate, ref this.isHost);
		stream.Serialize(UpdateType.FullUpdate, ref this.userID);
		stream.Serialize(UpdateType.FullUpdate, ref this.skillsInfos);
		stream.Serialize(UpdateType.FullUpdate, ref this.clanSkillsInfos);
		this.clevel = (char)this.level;
		stream.Serialize(ref this.clevel);
		this.cPing = (char)this.ping;
		stream.Serialize(ref this.cPing);
		if (this.playerType != PlayerType.spectactor)
		{
			this.sHealth = (short)this._health;
			stream.Serialize(ref this.sHealth);
			this.sarmor = (short)this.armor;
			stream.Serialize(ref this.sarmor);
			this.sKillCount = (short)this.killCount;
			stream.Serialize(ref this.sKillCount);
			this.sdeathCount = (short)this.deathCount;
			stream.Serialize(ref this.sdeathCount);
			this.cachePoints = this.points;
			stream.Serialize(ref this.cachePoints);
			stream.Serialize<Buffs>(ref this.buffs);
			stream.Serialize(ref this.number);
		}
		else
		{
			stream.Serialize(ref this.loading);
		}
		stream.Serialize(UpdateType.FullUpdate, ref this.IsSuspected);
		stream.Serialize(UpdateType.FullUpdate, ref this.Camouflages);
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x000CEDC0 File Offset: 0x000CCFC0
	public void Deserialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.playerID);
		stream.Serialize(ref this.cPlayerType);
		this.playerType = (PlayerType)this.cPlayerType;
		BoolToInt boolToInt = new BoolToInt(4);
		stream.Serialize(ref boolToInt.Compressed);
		this.isModerator = boolToInt.Pop();
		this.hasSonar = boolToInt.Pop();
		this.hasMortar = boolToInt.Pop();
		this.dead = boolToInt.Pop();
		string value = this._nick;
		stream.Serialize(UpdateType.FullUpdate, ref value);
		this._nick = value;
		stream.Serialize(UpdateType.FullUpdate, ref this.NickColor);
		stream.Serialize(UpdateType.FullUpdate, ref this.clanTag);
		stream.Serialize<PlayerClass>(UpdateType.FullUpdate, ref this.playerClass);
		stream.Serialize(UpdateType.FullUpdate, ref this.isHost);
		stream.Serialize(UpdateType.FullUpdate, ref this.userID);
		stream.Serialize(UpdateType.FullUpdate, ref this.skillsInfos);
		stream.Serialize(UpdateType.FullUpdate, ref this.clanSkillsInfos);
		stream.Serialize(ref this.clevel);
		this.level = (int)this.clevel;
		stream.Serialize(ref this.cPing);
		this.ping = (int)this.cPing;
		if (this.playerType != PlayerType.spectactor)
		{
			stream.Serialize(ref this.sHealth);
			this._health = (float)this.sHealth;
			stream.Serialize(ref this.sarmor);
			this.armor = (float)this.sarmor;
			stream.Serialize(ref this.sKillCount);
			this.killCount = (int)this.sKillCount;
			stream.Serialize(ref this.sdeathCount);
			this.deathCount = (int)this.sdeathCount;
			stream.Serialize(ref this.cachePoints);
			this.points = this.cachePoints;
			stream.Serialize<Buffs>(ref this.buffs);
			stream.Serialize(ref this.number);
		}
		else
		{
			stream.Serialize(ref this.loading);
		}
		try
		{
			stream.Serialize(UpdateType.FullUpdate, ref this.IsSuspected);
			stream.Serialize(UpdateType.FullUpdate, ref this.Camouflages);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x000CEFDC File Offset: 0x000CD1DC
	public void Clear()
	{
		this._nick = "unknown";
		this.NickColor = "#FFFFFF";
		this.clanTag = string.Empty;
		this.isHost = false;
		this.userID = IDUtil.NoID;
		this.playerID = IDUtil.NoID;
		this.playerType = PlayerType.spectactor;
		this.playerClass = PlayerClass.none;
		this.ping = 0;
		this.clientHalfPing = 0f;
		this.packetLatency = 0f;
		this.dead = true;
		this._health = CVars.g_baseHealthAmount;
		this.armor = 0f;
		this.IsSuspected = false;
		this.killCount = 0;
		this.deathCount = 0;
		this.level = 0;
		this.points = 0;
		this.buffs = Buffs.none;
		this.loading = 0;
		this.hasMortar = false;
		this.hasSonar = false;
		this.number = IDUtil.NoID;
		this.skillsInfos = new bool[0];
		this.clanSkillsInfos = new bool[0];
		this.Camouflages = string.Empty;
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x000CF0F0 File Offset: 0x000CD2F0
	public void Clone(PlayerInfo i)
	{
		this._nick = i._nick;
		this.NickColor = i.NickColor;
		this.isModerator = i.isModerator;
		this.clanTag = i.clanTag;
		this.playerID = i.playerID;
		this.userID = i.userID;
		this.playerClass = i.playerClass;
		this.ping = i.ping;
		this.clientHalfPing = i.clientHalfPing;
		this.packetLatency = i.packetLatency;
		this.temporaryBuff = i.temporaryBuff;
		this.dead = i.dead;
		this._health = i._health;
		this.armor = i.armor;
		this.IsSuspected = i.IsSuspected;
		this.killCount = i.killCount;
		this.deathCount = i.deathCount;
		this.level = i.level;
		this.points = i.points;
		this.playerType = i.playerType;
		this.isHost = i.isHost;
		this.loading = i.loading;
		this.buffs = i.buffs;
		this.hasMortar = i.hasMortar;
		this.hasSonar = i.hasSonar;
		this.number = i.number;
		this.skillsInfos = i.skillsInfos;
		this.clanSkillsInfos = i.clanSkillsInfos;
		this.Camouflages = i.Camouflages;
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000CF25C File Offset: 0x000CD45C
	public void Update(PlayerInfo i)
	{
		this._nick = i._nick;
		this.NickColor = i.NickColor;
		this.clanTag = i.clanTag;
		this.playerClass = i.playerClass;
		this.isHost = i.isHost;
		this.userID = i.userID;
		this.skillsInfos = i.skillsInfos;
		this.clanSkillsInfos = i.clanSkillsInfos;
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000CF2CC File Offset: 0x000CD4CC
	public bool skillUnlocked(Skills skill)
	{
		if (this.skillsInfos.Length < 1)
		{
			return false;
		}
		if (Peer.HardcoreMode)
		{
			return Peer.Info != null && (skill == Skills.car_block || skill == Skills.att3 || skill == Skills.marks || skill == Skills.uniq_p || skill == Skills.uniq_pp || skill == Skills.spec_mp5 || skill == Skills.uniq_rifle || skill == Skills.uniq_shot || skill == Skills.uniq_mg || skill == Skills.uniq_sni);
		}
		return this.skillsInfos[(int)skill];
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x000CF360 File Offset: 0x000CD560
	public bool clanSkillUnlocked(Cl_Skills clanSkill)
	{
		return !Peer.HardcoreMode && this.clanSkillsInfos != null && this.clanSkillsInfos.Length >= 1 && this.clanSkillsInfos[(int)clanSkill];
	}

	// Token: 0x0400160C RID: 5644
	private ObscuredString _nick = "unknown";

	// Token: 0x0400160D RID: 5645
	public string NickColor = "#FFFFFF";

	// Token: 0x0400160E RID: 5646
	public string clanTag = string.Empty;

	// Token: 0x0400160F RID: 5647
	public bool isHost;

	// Token: 0x04001610 RID: 5648
	public ObscuredInt userID = IDUtil.NoID;

	// Token: 0x04001611 RID: 5649
	public ObscuredInt playerID = IDUtil.NoID;

	// Token: 0x04001612 RID: 5650
	public PlayerType playerType = PlayerType.spectactor;

	// Token: 0x04001613 RID: 5651
	public PlayerClass playerClass;

	// Token: 0x04001614 RID: 5652
	public int ping;

	// Token: 0x04001615 RID: 5653
	public float clientHalfPing;

	// Token: 0x04001616 RID: 5654
	public float packetLatency;

	// Token: 0x04001617 RID: 5655
	public bool IsSuspected;

	// Token: 0x04001618 RID: 5656
	public string Camouflages;

	// Token: 0x04001619 RID: 5657
	private ObscuredFloat _health = CVars.g_baseHealthAmount;

	// Token: 0x0400161A RID: 5658
	public float armor;

	// Token: 0x0400161B RID: 5659
	public int killCount;

	// Token: 0x0400161C RID: 5660
	public int deathCount;

	// Token: 0x0400161D RID: 5661
	public int level;

	// Token: 0x0400161E RID: 5662
	public int points;

	// Token: 0x0400161F RID: 5663
	public Buffs buffs = Buffs.none;

	// Token: 0x04001620 RID: 5664
	public Buffs temporaryBuff = Buffs.none;

	// Token: 0x04001621 RID: 5665
	public int loading;

	// Token: 0x04001622 RID: 5666
	public bool dead = true;

	// Token: 0x04001623 RID: 5667
	public bool hasMortar;

	// Token: 0x04001624 RID: 5668
	public bool hasSonar;

	// Token: 0x04001625 RID: 5669
	public bool isModerator;

	// Token: 0x04001626 RID: 5670
	public int number = IDUtil.NoID;

	// Token: 0x04001627 RID: 5671
	[HideInInspector]
	public bool[] skillsInfos = new bool[0];

	// Token: 0x04001628 RID: 5672
	[HideInInspector]
	public bool[] clanSkillsInfos = new bool[0];

	// Token: 0x04001629 RID: 5673
	private char cPlayerType;

	// Token: 0x0400162A RID: 5674
	private char clevel;

	// Token: 0x0400162B RID: 5675
	private char cPing;

	// Token: 0x0400162C RID: 5676
	private short sdeathCount;

	// Token: 0x0400162D RID: 5677
	private short sKillCount;

	// Token: 0x0400162E RID: 5678
	private short sHealth;

	// Token: 0x0400162F RID: 5679
	private short sarmor;

	// Token: 0x04001630 RID: 5680
	private int cachePoints;
}
