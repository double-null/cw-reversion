using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000234 RID: 564
[AddComponentMenu("Scripts/Game/Events/Server/ServerLoad")]
internal class ServerLoad : ServerEvent
{
	// Token: 0x0600117A RID: 4474 RVA: 0x000C2754 File Offset: 0x000C0954
	public override void Initialize(PoolableBehaviour mono, params object[] args)
	{
		this.player = (mono as ServerNetPlayer);
		this.player.CanSpawn = false;
		this.player.playerInfo.buffs |= Buffs.db_load;
		global::Console.print(string.Concat(new object[]
		{
			"NetPlayer ",
			this.player.UserInfo.userID,
			" Load (myID ",
			this.player.MyID,
			" targetID ",
			this.player.TargetID,
			")"
		}));
		HtmlLayer.RequestCompressed("?action=idload&user_id=" + this.player.UserInfo.userID.ToString(), new RequestFinished(this.OnLoad), new RequestFailed(this.OnLoadFail), string.Empty, string.Empty);
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x000C2844 File Offset: 0x000C0A44
	protected void ReadProfile(Dictionary<string, object> data)
	{
		this.player.UserInfo.weaponsStates = new WeaponInfo[Main.UserInfo.weaponsStates.Length];
		for (int i = 0; i < this.player.UserInfo.weaponsStates.Length; i++)
		{
			this.player.UserInfo.weaponsStates[i] = new WeaponInfo();
			this.player.UserInfo.weaponsStates[i].Init(true, i);
		}
		this.player.UserInfo.Read(data, true);
		this.player.playerInfo.level = this.player.UserInfo.currentLevel;
		this.player.playerInfo.Nick = this.player.UserInfo.nick;
		this.player.playerInfo.NickColor = this.player.UserInfo.nickColor;
		this.player.playerInfo.IsSuspected = this.player.UserInfo.isSuspected;
		this.player.playerInfo.clanTag = this.player.UserInfo.clanTag;
		this.player.playerInfo.skillsInfos = this.player.UserInfo.skillArray;
		this.player.playerInfo.playerClass = this.player.UserInfo.playerClass;
		this.player.playerInfo.clanSkillsInfos = this.player.UserInfo.clanSkillArray;
		this.player.playerInfo.isModerator = (this.player.UserInfo.Permission == EPermission.Moder);
		if (this.player.playerInfo.Nick == "Player")
		{
			PlayerInfo playerInfo = this.player.playerInfo;
			playerInfo.Nick += this.player.UserInfo.userID.ToString();
		}
		if (this.player.playerInfo.Nick == "Guest")
		{
			PlayerInfo playerInfo2 = this.player.playerInfo;
			playerInfo2.Nick += this.player.playerInfo.playerID.ToString();
		}
		this.player.Stats = new Stats(true);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x000C2AAC File Offset: 0x000C0CAC
	protected void OnLoad(string text, string url)
	{
		this.OnLoad(text);
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x000C2AB8 File Offset: 0x000C0CB8
	[Obfuscation(Exclude = true)]
	protected void OnLoad(string text)
	{
		try
		{
			Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
			if ((int)dictionary["result"] != 0)
			{
				this.OnLoadFail(new Exception("DataServer Error Bad Result"), "?action=idload");
				return;
			}
			this.ReadProfile(dictionary);
			if (this.player.UserInfo.banned != 0)
			{
				base.OnFail(new Exception(Language.ServerLoadOnFail0));
				return;
			}
			if (this.player.UserInfo.Permission <= EPermission.Tester && (Main.HostInfo.Name == "UnityTestServer" || Main.HostInfo.Name.Contains("Developer")))
			{
				return;
			}
			if (this.player.UserInfo.Permission != EPermission.Moder)
			{
				if (this.player.UserInfo.userID == IDUtil.GuestID && Main.IsTargetDesignation)
				{
					base.OnFail(new Exception(Language.ServerLoadOnFail2));
					return;
				}
				if (!Peer.Info.MyLevelRange(this.player.playerInfo.level, false) && this.player.UserInfo.Permission <= EPermission.Tester)
				{
					if (!Peer.HardcoreMode)
					{
						base.OnFail(new Exception(Language.ServerLoadOnFail4));
						return;
					}
					if (!this.player.UserInfo.skillsInfos[52].Unlocked)
					{
						base.OnFail(new Exception(Language.ServerLoadOnFail4));
						return;
					}
				}
			}
		}
		catch (Exception ex)
		{
			if (Application.isEditor || Peer.Dedicated)
			{
				global::Console.print(ex.ToString());
			}
			this.OnLoadFail(new Exception("DataServer Error " + ex), "?action=idload");
			return;
		}
		this.LoadEvents(text);
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x000C2CD0 File Offset: 0x000C0ED0
	protected void OnLoadFail(Exception e)
	{
		if (this.player)
		{
			this.player.Disconnect();
		}
		Main.AddDatabaseRequestCallBack<InitUser>(delegate
		{
		}, delegate
		{
		}, new object[0]);
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x000C2D40 File Offset: 0x000C0F40
	protected void OnLoadFail(Exception e, string url)
	{
		this.OnLoadFail(e);
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x000C2D4C File Offset: 0x000C0F4C
	protected virtual void LoadEvents(string text)
	{
		if (CVars.n_httpDebug)
		{
			global::Console.print("NetPlayer " + this.player.UserInfo.userID + " Load Finished");
		}
		this.player.playerInfo.buffs -= 256;
		if (!this.player.playerInfo.isModerator)
		{
			Peer.ServerGame.EventMessage(this.player.playerInfo.Nick, ChatInfo.network_message, Language.PlayerConnected);
		}
		Peer.ServerGame.AddPlayer(this.player);
		this.player.GetComponent<PoolItem>().CancelInvoke("Despawn");
		eNetwork.RPC("OpenConnectionFinished", this.player.NetworkPlayer, new object[]
		{
			this.player.UserID,
			this.player.TargetID,
			this.player.MyID,
			this.player.ID,
			this.player.Group,
			Peer.ServerGame.ServerTime,
			SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--snow")
		});
		this.player.CanSpawn = true;
		base.OnResponse(text);
	}

	// Token: 0x0400111D RID: 4381
	public static bool ErrorTest;
}
