using System;
using System.Collections;
using System.Collections.Generic;
using TacticalPointNamespace;
using UnityEngine;

// Token: 0x020002CF RID: 719
internal class TacticalPoint : MonoBehaviour, cwNetworkSerializable
{
	// Token: 0x170002CC RID: 716
	// (get) Token: 0x0600139C RID: 5020 RVA: 0x000D3788 File Offset: 0x000D1988
	public bool InAction
	{
		get
		{
			return this.captureProgress > 0f && this.captureProgress < 1f;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x0600139D RID: 5021 RVA: 0x000D37B8 File Offset: 0x000D19B8
	public int UsecBoost
	{
		get
		{
			return this._usecBoost;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x0600139E RID: 5022 RVA: 0x000D37C0 File Offset: 0x000D19C0
	public int BearBoost
	{
		get
		{
			return this._bearBoost;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x0600139F RID: 5023 RVA: 0x000D37C8 File Offset: 0x000D19C8
	public int BearIN
	{
		get
		{
			return this.bearIn;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x060013A0 RID: 5024 RVA: 0x000D37D0 File Offset: 0x000D19D0
	public int UsecIN
	{
		get
		{
			return this.usecIn;
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x060013A1 RID: 5025 RVA: 0x000D37D8 File Offset: 0x000D19D8
	public bool IsBasePoint
	{
		get
		{
			return this.pointState == TacticalPointState.bear_homeBase || this.pointState == TacticalPointState.usec_homeBase;
		}
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x000D37F4 File Offset: 0x000D19F4
	public bool IsHomebase(BaseNetPlayer player)
	{
		if (player.IsBear)
		{
			return this.pointState == TacticalPointState.bear_homeBase;
		}
		return this.pointState == TacticalPointState.usec_homeBase;
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x000D3824 File Offset: 0x000D1A24
	public int isEnemy(BaseNetPlayer player)
	{
		bool flag = false;
		if (player != null)
		{
			flag = player.IsBear;
		}
		if (this.pointState == TacticalPointState.neutral)
		{
			return 0;
		}
		if ((this.pointState == TacticalPointState.bear_homeBase || this.pointState == TacticalPointState.bear_captured) && flag)
		{
			return 1;
		}
		if ((this.pointState == TacticalPointState.usec_homeBase || this.pointState == TacticalPointState.usec_captured) && !flag)
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x000D3898 File Offset: 0x000D1A98
	public bool InEnemyPoint(BaseNetPlayer player)
	{
		bool flag = false;
		if (player != null)
		{
			flag = player.IsBear;
		}
		return ((this.pointState == TacticalPointState.bear_homeBase || this.pointState == TacticalPointState.bear_captured) && !flag) || ((this.pointState == TacticalPointState.usec_homeBase || this.pointState == TacticalPointState.usec_captured) && flag);
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x060013A5 RID: 5029 RVA: 0x000D3900 File Offset: 0x000D1B00
	public string Name
	{
		get
		{
			return Language.PointName[(int)this.pointName];
		}
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000D3910 File Offset: 0x000D1B10
	public Vector3 Pos
	{
		get
		{
			return this.position;
		}
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x000D3918 File Offset: 0x000D1B18
	private void Awake()
	{
		this.Neutral = new Neutral(this);
		this.BearCaptured = new BearCaptured(this);
		this.UsecCaptured = new UsecCaptured(this);
		this.BearHomebase = new BearHomeBase(this);
		this.UsecHomeBase = new UsecHomeBase(this);
		this.currentState = this.Neutral;
		this.position = base.gameObject.transform.position;
		this._serverUpdate = false;
		this._clientUpdate = false;
		this.earnExpNeutralizeData.Clear();
		this.earnExpCaptureData.Clear();
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000D39A8 File Offset: 0x000D1BA8
	public void SetAsServerEntity()
	{
		this._serverUpdate = true;
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x000D39B4 File Offset: 0x000D1BB4
	public void SetAsClientEntity()
	{
		this._clientUpdate = true;
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x000D39C0 File Offset: 0x000D1BC0
	public void SetState(ITacticalPointState state)
	{
		this.currentState = state;
		this.currentState.OnSet();
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x000D39D4 File Offset: 0x000D1BD4
	public void OnInit()
	{
		foreach (SpawnPoint point in this.spawnPoints)
		{
			this.spawns.Add(new Spawn(point));
		}
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x000D3A44 File Offset: 0x000D1C44
	public void Clear()
	{
		this.usecs.Clear();
		this.bears.Clear();
		this.earnExpNeutralizeData.Clear();
		this.earnExpCaptureData.Clear();
		this._updateTime = 0f;
		this.progressTime = 0f;
		this.captureProgress = 0f;
		if (this.pointState == TacticalPointState.neutral)
		{
			this.currentState = this.Neutral;
		}
		if (this.pointState == TacticalPointState.bear_homeBase)
		{
			this.currentState = this.BearHomebase;
		}
		if (this.pointState == TacticalPointState.usec_homeBase)
		{
			this.currentState = this.UsecHomeBase;
		}
		if (this.pointState == TacticalPointState.bear_captured || this.pointState == TacticalPointState.usec_captured)
		{
			this.currentState = this.Neutral;
		}
		this.currentState.OnSet();
		this.clientLastState = TacticalPointState.neutral;
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x000D3B20 File Offset: 0x000D1D20
	public void AddExpForCapture()
	{
		if (this.pointState == TacticalPointState.usec_captured)
		{
			foreach (KeyValuePair<Collider, ServerNetPlayer> keyValuePair in this.usecs)
			{
				if (!this.earnExpCaptureData.Contains(keyValuePair.Value))
				{
					if (keyValuePair.Value.UnlockClanSkill(Cl_Skills.cl_tc))
					{
						keyValuePair.Value.Exp(CVars.g_capturexp * 1.1f, 0f, true);
					}
					else
					{
						keyValuePair.Value.Exp(CVars.g_capturexp, 0f, true);
					}
					this.earnExpCaptureData.Add(keyValuePair.Value);
				}
			}
		}
		if (this.pointState == TacticalPointState.bear_captured)
		{
			foreach (KeyValuePair<Collider, ServerNetPlayer> keyValuePair2 in this.bears)
			{
				if (!this.earnExpCaptureData.Contains(keyValuePair2.Value))
				{
					if (keyValuePair2.Value.UnlockClanSkill(Cl_Skills.cl_tc))
					{
						keyValuePair2.Value.Exp(CVars.g_capturexp * 1.1f, 0f, true);
					}
					else
					{
						keyValuePair2.Value.Exp(CVars.g_capturexp, 0f, true);
					}
					this.earnExpCaptureData.Add(keyValuePair2.Value);
				}
			}
		}
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x000D3CE0 File Offset: 0x000D1EE0
	public void AddExpForNeutralize(bool bear)
	{
		if (!bear)
		{
			foreach (KeyValuePair<Collider, ServerNetPlayer> keyValuePair in this.usecs)
			{
				if (!this.earnExpCaptureData.Contains(keyValuePair.Value))
				{
					if (keyValuePair.Value.UnlockClanSkill(Cl_Skills.cl_tc))
					{
						keyValuePair.Value.Exp(CVars.g_neutralizexp * 1.1f, 0f, true);
					}
					else
					{
						keyValuePair.Value.Exp(CVars.g_neutralizexp, 0f, true);
					}
					this.earnExpNeutralizeData.Add(keyValuePair.Value);
				}
			}
		}
		if (bear)
		{
			foreach (KeyValuePair<Collider, ServerNetPlayer> keyValuePair2 in this.bears)
			{
				if (!this.earnExpNeutralizeData.Contains(keyValuePair2.Value))
				{
					if (keyValuePair2.Value.UnlockClanSkill(Cl_Skills.cl_tc))
					{
						keyValuePair2.Value.Exp(CVars.g_neutralizexp * 1.1f, 0f, true);
					}
					else
					{
						keyValuePair2.Value.Exp(CVars.g_neutralizexp, 0f, true);
					}
					this.earnExpNeutralizeData.Add(keyValuePair2.Value);
				}
			}
		}
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x000D3E94 File Offset: 0x000D2094
	private void SoundPointCapture(bool isBear)
	{
		if (!this.playOnce)
		{
			PoolItem component = base.GetComponent<PoolItem>();
			if (Peer.ClientGame.LocalPlayer.IsAlive)
			{
				component = Peer.ClientGame.LocalPlayer.MainCamera.GetComponent<PoolItem>();
			}
			if (Peer.ClientGame.LocalPlayer.IsAlive && Peer.ClientGame.LocalPlayer.IsBear == isBear)
			{
				Audio.PlayTyped(component, SoundType.radio, SingletoneForm<SoundFactory>.Instance.TacticalConquestSounds[0], false, 0f, 1000000f);
			}
		}
		this.playOnce = true;
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x000D3F2C File Offset: 0x000D212C
	private void SoundPointCaptured()
	{
		PoolItem component = base.GetComponent<PoolItem>();
		if (Peer.ClientGame.LocalPlayer.IsAlive)
		{
			component = Peer.ClientGame.LocalPlayer.MainCamera.GetComponent<PoolItem>();
		}
		if (Peer.ClientGame.LocalPlayer.IsAlive)
		{
			Audio.PlayTyped(component, SoundType.radio, SingletoneForm<SoundFactory>.Instance.TacticalConquestSounds[4], false, 0f, 1000000f);
		}
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x000D3F9C File Offset: 0x000D219C
	private void SoundPointLose(bool isBear)
	{
		if (!this.playOnce)
		{
			PoolItem component = base.GetComponent<PoolItem>();
			if (Peer.ClientGame.LocalPlayer.IsAlive)
			{
				component = Peer.ClientGame.LocalPlayer.MainCamera.GetComponent<PoolItem>();
			}
			if (Peer.ClientGame.LocalPlayer.IsAlive && Peer.ClientGame.LocalPlayer.IsBear != isBear)
			{
				Audio.PlayTyped(component, SoundType.radio, SingletoneForm<SoundFactory>.Instance.TacticalConquestSounds[2], false, 0f, 1000000f);
			}
		}
		this.playOnce = true;
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x000D4034 File Offset: 0x000D2234
	private void SoundLoopPointCapturing(bool isBear)
	{
		PoolItem component = base.GetComponent<PoolItem>();
		if (Peer.ClientGame.LocalPlayer.IsAlive)
		{
			component = Peer.ClientGame.LocalPlayer.MainCamera.GetComponent<PoolItem>();
		}
		if (Peer.ClientGame.LocalPlayer.IsAlive && Peer.ClientGame.LocalPlayer.IsBear == isBear)
		{
			Audio.PlayTyped(component, SoundType.radio, SingletoneForm<SoundFactory>.Instance.TacticalConquestSounds[1], false, 0f, 1000000f);
		}
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x000D40B8 File Offset: 0x000D22B8
	public void Serialize(eNetworkStream stream)
	{
		short num = (short)this.bearIn;
		short num2 = (short)this.usecIn;
		stream.Serialize(ref num);
		stream.Serialize(ref num2);
		stream.Serialize(ref this.captureProgress);
		int num3 = (int)this.pointState;
		stream.Serialize(ref num3);
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x000D4100 File Offset: 0x000D2300
	public void Deserialize(eNetworkStream stream)
	{
		short num = 0;
		short num2 = 0;
		stream.Serialize(ref num);
		this.bearIn = (int)num;
		stream.Serialize(ref num2);
		this.usecIn = (int)num2;
		stream.Serialize(ref this.captureProgress);
		int num3 = 0;
		stream.Serialize(ref num3);
		this.lastState = this.pointState;
		this.pointState = (TacticalPointState)num3;
	}

	// Token: 0x060013B5 RID: 5045 RVA: 0x000D4158 File Offset: 0x000D2358
	private void Start()
	{
		this.Clear();
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x000D4160 File Offset: 0x000D2360
	private void Update()
	{
		if (this._serverUpdate)
		{
			if (this.progressTime < Time.time)
			{
				this.progressTime = Time.time + this.progressDelataTime;
				if (Peer.ServerGame.MatchState == MatchState.round_going && TacticalPoint.canUpdate)
				{
					this.currentState.Update();
				}
			}
			if (this._updateTime < Time.time)
			{
				this._updateTime = Time.time + 1f;
				IDictionaryEnumerator dictionaryEnumerator = this.usecs.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					Collider collider = (Collider)dictionaryEnumerator.Key;
					if (!collider.enabled)
					{
						if (this._usecCaptureBoostList.Contains((ServerNetPlayer)dictionaryEnumerator.Value))
						{
							this._usecCaptureBoostList.Remove((ServerNetPlayer)dictionaryEnumerator.Value);
						}
						if (this._usecSingleCaptureList.Contains((ServerNetPlayer)dictionaryEnumerator.Value))
						{
							this._usecSingleCaptureList.Remove((ServerNetPlayer)dictionaryEnumerator.Value);
						}
						this.usecs.Remove(collider);
						dictionaryEnumerator = this.usecs.GetEnumerator();
					}
				}
				IDictionaryEnumerator dictionaryEnumerator2 = this.bears.GetEnumerator();
				while (dictionaryEnumerator2.MoveNext())
				{
					Collider collider2 = (Collider)dictionaryEnumerator2.Key;
					if (!collider2.enabled)
					{
						if (this._bearCaptureBoostList.Contains((ServerNetPlayer)dictionaryEnumerator2.Value))
						{
							this._bearCaptureBoostList.Remove((ServerNetPlayer)dictionaryEnumerator2.Value);
						}
						if (this._bearSingleCaptureList.Contains((ServerNetPlayer)dictionaryEnumerator2.Value))
						{
							this._usecSingleCaptureList.Remove((ServerNetPlayer)dictionaryEnumerator2.Value);
						}
						this.bears.Remove(collider2);
						dictionaryEnumerator2 = this.bears.GetEnumerator();
					}
				}
				this.usecIn = this.usecs.Count;
				this.bearIn = this.bears.Count;
				this._usecBoost = ((this._usecCaptureBoostList.Count <= 0) ? 1 : 2);
				this._bearBoost = ((this._bearCaptureBoostList.Count <= 0) ? 1 : 2);
				this.UsecSingleCapture = (this._usecSingleCaptureList.Count > 0);
				this.BearSingleCapture = (this._bearSingleCaptureList.Count > 0);
			}
		}
		if (!this._clientUpdate || this._clientUpdateTime >= Time.time)
		{
			return;
		}
		this._clientUpdateTime = Time.time + 1f;
		if (this.captureProgress == 0f || this.captureProgress >= 1f)
		{
			this.playOnce = false;
		}
		if (this.clientLastState != this.pointState && this.captureProgress > 0f && this.captureProgress < 1f)
		{
			TacticalPoint.OnChangeState(this);
		}
		if (this.clientLastState == this.pointState && (this.usecIn > this.bearIn || this.bearIn > this.usecIn))
		{
			TacticalPoint.OnProgress(this);
		}
		if (this.pointState == TacticalPointState.neutral && this.captureProgress > 0f)
		{
			this.SoundPointCapture(this.bearIn > this.usecIn);
			this.SoundLoopPointCapturing(this.bearIn > this.usecIn);
		}
		if (this.pointState != TacticalPointState.neutral && this.captureProgress > 0f && this.captureProgress < 1f)
		{
			this.SoundPointLose(this.bearIn > this.usecIn);
		}
		if (this.clientLastState != this.pointState && this.pointState != TacticalPointState.neutral && this.captureProgress >= 1f)
		{
			TacticalPoint.OnCaptured(this);
			this.SoundPointCaptured();
			this.SoundPointCapture(this.bearIn > this.usecIn);
		}
		if (this.clientLastState != this.pointState && this.pointState == TacticalPointState.neutral)
		{
			TacticalPoint.OnNeutral(this);
		}
		this.clientLastState = this.pointState;
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x000D45BC File Offset: 0x000D27BC
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		if (this._serverUpdate)
		{
			ServerNetPlayer component = other.transform.parent.GetComponent<ServerNetPlayer>();
			if (component != null)
			{
				if (component.IsBear && !this.bears.ContainsKey(other))
				{
					this.bears.Add(other, component);
					if (component.PlayerClass == PlayerClass.gunsmith)
					{
						if (!this._bearCaptureBoostList.Contains(component) && component.UnlockClanSkill(Cl_Skills.cl_weap2))
						{
							this._bearCaptureBoostList.Add(component);
						}
						if (!this._bearSingleCaptureList.Contains(component) && component.UnlockClanSkill(Cl_Skills.cl_weap3))
						{
							this._bearSingleCaptureList.Add(component);
						}
					}
				}
				else if (!component.IsBear && !this.usecs.ContainsKey(other))
				{
					this.usecs.Add(other, component);
					if (component.PlayerClass == PlayerClass.gunsmith)
					{
						if (!this._usecCaptureBoostList.Contains(component) && component.UnlockClanSkill(Cl_Skills.cl_weap2))
						{
							this._usecCaptureBoostList.Add(component);
						}
						if (!this._usecSingleCaptureList.Contains(component) && component.UnlockClanSkill(Cl_Skills.cl_weap3))
						{
							this._usecSingleCaptureList.Add(component);
						}
					}
				}
			}
		}
		if (!this._clientUpdate || this.IsBasePoint)
		{
			return;
		}
		ClientNetPlayer component2 = other.transform.parent.GetComponent<ClientNetPlayer>();
		if (component2 != null)
		{
			Peer.ClientGame.LocalPlayer.InPoint = this.NumberOfPoint;
		}
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x000D4760 File Offset: 0x000D2960
	private void OnTriggerExit(Collider other)
	{
		if (this._serverUpdate)
		{
			ServerNetPlayer component = other.transform.parent.GetComponent<ServerNetPlayer>();
			if (component != null)
			{
				if (this.bears.ContainsKey(other))
				{
					if (this._bearCaptureBoostList.Contains(component))
					{
						this._bearCaptureBoostList.Remove(component);
					}
					if (this._bearSingleCaptureList.Contains(component))
					{
						this._bearSingleCaptureList.Remove(component);
					}
					this.bears.Remove(other);
				}
				else if (this.usecs.ContainsKey(other))
				{
					if (this._usecCaptureBoostList.Contains(component))
					{
						this._usecCaptureBoostList.Remove(component);
					}
					if (this._usecSingleCaptureList.Contains(component))
					{
						this._usecSingleCaptureList.Remove(component);
					}
					this.usecs.Remove(other);
				}
			}
		}
		if (!this._clientUpdate)
		{
			return;
		}
		ClientNetPlayer component2 = other.transform.parent.GetComponent<ClientNetPlayer>();
		if (component2 != null)
		{
			Peer.ClientGame.LocalPlayer.InPoint = -1;
		}
	}

	// Token: 0x0400184C RID: 6220
	private const float UpdateDeltaTime = 1f;

	// Token: 0x0400184D RID: 6221
	public ITacticalPointState currentState;

	// Token: 0x0400184E RID: 6222
	public Neutral Neutral;

	// Token: 0x0400184F RID: 6223
	public BearCaptured BearCaptured;

	// Token: 0x04001850 RID: 6224
	public UsecCaptured UsecCaptured;

	// Token: 0x04001851 RID: 6225
	public BearHomeBase BearHomebase;

	// Token: 0x04001852 RID: 6226
	public UsecHomeBase UsecHomeBase;

	// Token: 0x04001853 RID: 6227
	private Vector3 position = default(Vector3);

	// Token: 0x04001854 RID: 6228
	private float capturedTime;

	// Token: 0x04001855 RID: 6229
	private float addPointTime;

	// Token: 0x04001856 RID: 6230
	public int playersNeeded;

	// Token: 0x04001857 RID: 6231
	public float secondsForNextPointAdd;

	// Token: 0x04001858 RID: 6232
	public int NumberOfPoint;

	// Token: 0x04001859 RID: 6233
	public TacticalPointName pointName;

	// Token: 0x0400185A RID: 6234
	public bool SpawnAtBase;

	// Token: 0x0400185B RID: 6235
	private bool playOnce;

	// Token: 0x0400185C RID: 6236
	[HideInInspector]
	public float blinkTimer;

	// Token: 0x0400185D RID: 6237
	[HideInInspector]
	public TacticalPointState lastState = TacticalPointState.neutral;

	// Token: 0x0400185E RID: 6238
	public TacticalPointState pointState = TacticalPointState.neutral;

	// Token: 0x0400185F RID: 6239
	public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

	// Token: 0x04001860 RID: 6240
	[HideInInspector]
	public TacticalPointState nextPointState = TacticalPointState.neutral;

	// Token: 0x04001861 RID: 6241
	protected eTimer soundTimer = new eTimer();

	// Token: 0x04001862 RID: 6242
	private Dictionary<Collider, ServerNetPlayer> usecs = new Dictionary<Collider, ServerNetPlayer>();

	// Token: 0x04001863 RID: 6243
	private Dictionary<Collider, ServerNetPlayer> bears = new Dictionary<Collider, ServerNetPlayer>();

	// Token: 0x04001864 RID: 6244
	private List<ServerNetPlayer> earnExpNeutralizeData = new List<ServerNetPlayer>();

	// Token: 0x04001865 RID: 6245
	private List<ServerNetPlayer> earnExpCaptureData = new List<ServerNetPlayer>();

	// Token: 0x04001866 RID: 6246
	private List<ServerNetPlayer> _bearCaptureBoostList = new List<ServerNetPlayer>();

	// Token: 0x04001867 RID: 6247
	private List<ServerNetPlayer> _usecCaptureBoostList = new List<ServerNetPlayer>();

	// Token: 0x04001868 RID: 6248
	private int _usecBoost = 1;

	// Token: 0x04001869 RID: 6249
	private int _bearBoost = 1;

	// Token: 0x0400186A RID: 6250
	private List<ServerNetPlayer> _bearSingleCaptureList = new List<ServerNetPlayer>();

	// Token: 0x0400186B RID: 6251
	private List<ServerNetPlayer> _usecSingleCaptureList = new List<ServerNetPlayer>();

	// Token: 0x0400186C RID: 6252
	public bool UsecSingleCapture;

	// Token: 0x0400186D RID: 6253
	public bool BearSingleCapture;

	// Token: 0x0400186E RID: 6254
	private bool _serverUpdate;

	// Token: 0x0400186F RID: 6255
	private bool _clientUpdate;

	// Token: 0x04001870 RID: 6256
	private float _updateTime;

	// Token: 0x04001871 RID: 6257
	private float _clientUpdateTime;

	// Token: 0x04001872 RID: 6258
	private TacticalPointState clientLastState = TacticalPointState.neutral;

	// Token: 0x04001873 RID: 6259
	public static bool canUpdate = true;

	// Token: 0x04001874 RID: 6260
	[SerializeField]
	public int bearIn;

	// Token: 0x04001875 RID: 6261
	public int usecIn;

	// Token: 0x04001876 RID: 6262
	public float captureProgress;

	// Token: 0x04001877 RID: 6263
	private float progressTime;

	// Token: 0x04001878 RID: 6264
	private float progressDelataTime = 0.1f;

	// Token: 0x04001879 RID: 6265
	public float captureStep = 0.01f;

	// Token: 0x0400187A RID: 6266
	public static TacticalPoint.OnEvent OnCaptured = delegate(TacticalPoint point)
	{
	};

	// Token: 0x0400187B RID: 6267
	public static TacticalPoint.OnEvent OnProgress = delegate(TacticalPoint point)
	{
	};

	// Token: 0x0400187C RID: 6268
	public static TacticalPoint.OnEvent OnNeutral = delegate(TacticalPoint point)
	{
	};

	// Token: 0x0400187D RID: 6269
	public static TacticalPoint.OnEvent OnChangeState = delegate(TacticalPoint point)
	{
	};

	// Token: 0x0400187E RID: 6270
	[HideInInspector]
	public List<Spawn> spawns = new List<Spawn>();

	// Token: 0x020003B1 RID: 945
	// (Invoke) Token: 0x06001E2C RID: 7724
	public delegate void OnEvent(TacticalPoint point);
}
