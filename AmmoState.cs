using System;
using System.Collections.Generic;

// Token: 0x02000242 RID: 578
[Serializable]
internal class AmmoState : cwNetworkSerializable, Convertible, ReusableClass<AmmoState>
{
	// Token: 0x060011BF RID: 4543 RVA: 0x000C5200 File Offset: 0x000C3400
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWriteEnum<WeaponEquipedState>(dict, "equiped", ref this.equiped, isWrite);
		JSON.ReadWriteEnum<WeaponEquipedState>(dict, "preSupportEquiped", ref this.preSupportEquiped, isWrite);
		JSON.ReadWrite(dict, "primaryIndex", ref this.primaryIndex, isWrite);
		JSON.ReadWrite(dict, "primaryMod", ref this.primaryMod, isWrite);
		JSON.ReadWrite<WeaponState>(dict, "primaryState", ref this.primaryState, isWrite);
		JSON.ReadWrite(dict, "secondaryIndex", ref this.secondaryIndex, isWrite);
		JSON.ReadWrite(dict, "secondaryMod", ref this.secondaryMod, isWrite);
		JSON.ReadWrite<WeaponState>(dict, "secondaryState", ref this.secondaryState, isWrite);
		JSON.ReadWrite(dict, "RandomSeed", ref this.randomSeed, isWrite);
		JSON.ReadWrite(dict, "grenadeCount", ref this.grenadeCount, isWrite);
		JSON.ReadWrite(dict, "isAim", ref this.isAim, isWrite);
		JSON.ReadWrite(dict, "supportReady", ref this.supportReady, isWrite);
		JSON.ReadWrite(dict, "ServerReloadStart", ref this.ServerReloadStart, isWrite);
		JSON.ReadWrite(dict, "WeaponKit", ref this.WeaponKit, isWrite);
		JSON.ReadWrite<InvokerState>(dict, "G", ref this.G, isWrite);
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x000C531C File Offset: 0x000C351C
	public void Serialize(eNetworkStream stream)
	{
		BoolToInt boolToInt = new BoolToInt(0);
		char c = (char)this.equiped;
		stream.Serialize(ref c);
		this._sSecondaryIndex = (short)this.secondaryIndex;
		stream.Serialize(ref this._sSecondaryIndex);
		boolToInt.Push(this.secondaryMod);
		boolToInt.Push(this.primaryMod);
		boolToInt.Push(this.isAim);
		boolToInt.Push(this.supportReady);
		boolToInt.Push(this.ServerReloadStart);
		stream.Serialize(ref boolToInt.Compressed);
		if (this.secondaryIndex != 127)
		{
			this.secondaryState.Serialize(stream);
		}
		this._sPrimaryIndex = (short)this.primaryIndex;
		stream.Serialize(ref this._sPrimaryIndex);
		if (this.primaryIndex != 127)
		{
			this.primaryState.Serialize(stream);
		}
		stream.Serialize(ref this.randomSeed);
		this._sGrenadeCount = (short)this.grenadeCount;
		stream.Serialize(ref this._sGrenadeCount);
		this._sWeaponKit = (short)this.WeaponKit;
		stream.Serialize(ref this._sWeaponKit);
		if (stream.isFullUpdate)
		{
			this.G.Serialize(stream);
		}
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x000C5450 File Offset: 0x000C3650
	public void Deserialize(eNetworkStream stream)
	{
		BoolToInt boolToInt = new BoolToInt(5);
		char c = (char)this.equiped;
		stream.Serialize(ref c);
		this.equiped = (WeaponEquipedState)c;
		stream.Serialize(ref this._sSecondaryIndex);
		this.secondaryIndex = (int)this._sSecondaryIndex;
		stream.Serialize(ref boolToInt.Compressed);
		this.ServerReloadStart = boolToInt.Pop();
		this.supportReady = boolToInt.Pop();
		this.isAim = boolToInt.Pop();
		this.primaryMod = boolToInt.Pop();
		this.secondaryMod = boolToInt.Pop();
		if (this.secondaryIndex != 127)
		{
			this.secondaryState.Clear();
			this.secondaryState.Deserialize(stream);
		}
		stream.Serialize(ref this._sPrimaryIndex);
		this.primaryIndex = (int)this._sPrimaryIndex;
		if (this.primaryIndex != 127)
		{
			this.primaryState.Clear();
			this.primaryState.Deserialize(stream);
		}
		stream.Serialize(ref this.randomSeed);
		stream.Serialize(ref this._sGrenadeCount);
		this.grenadeCount = (int)this._sGrenadeCount;
		stream.Serialize(ref this._sWeaponKit);
		this.WeaponKit = (int)this._sWeaponKit;
		if (stream.isFullUpdate)
		{
			this.G.Deserialize(stream);
		}
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x000C5598 File Offset: 0x000C3798
	public void Clear()
	{
		this.equiped = WeaponEquipedState.none;
		this.preSupportEquiped = WeaponEquipedState.none;
		this.primaryIndex = 127;
		this.primaryMod = false;
		this.primaryState.Clear();
		this.secondaryIndex = 127;
		this.secondaryMod = false;
		this.secondaryState.Clear();
		this.randomSeed = 0;
		this.grenadeCount = 0;
		this.isAim = false;
		this.supportReady = false;
		this.ServerReloadStart = false;
		this.BlockFireByServer = false;
		this.WeaponKit = 0;
		this.G.Clear();
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x000C5624 File Offset: 0x000C3824
	public void Clone(AmmoState i)
	{
		this.equiped = i.equiped;
		this.preSupportEquiped = i.preSupportEquiped;
		this.primaryIndex = i.primaryIndex;
		this.primaryMod = i.primaryMod;
		this.primaryState.Clone(i.primaryState);
		this.secondaryIndex = i.secondaryIndex;
		this.secondaryMod = i.secondaryMod;
		this.secondaryState.Clone(i.secondaryState);
		this.randomSeed = i.randomSeed;
		this.grenadeCount = i.grenadeCount;
		this.isAim = i.isAim;
		this.supportReady = i.supportReady;
		this.ServerReloadStart = i.ServerReloadStart;
		this.WeaponKit = i.WeaponKit;
		this.G.Clone(i.G);
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x000C56F4 File Offset: 0x000C38F4
	public void Restore(AmmoState i)
	{
		this.primaryState.Clone(i.primaryState);
		this.secondaryState.Clone(i.secondaryState);
		this.randomSeed = i.randomSeed;
		this.grenadeCount = i.grenadeCount;
		this.WeaponKit = i.WeaponKit;
		this.G.Clone(i.G);
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x000C5758 File Offset: 0x000C3958
	public string OnChange(AmmoState i)
	{
		if (i == null)
		{
			return string.Empty;
		}
		bool flag = true;
		if (Peer.ClientGame.LocalPlayer.Ammo != null)
		{
			if (i.primaryState != null && i.primaryState.clips != this.primaryState.clips && Peer.ClientGame.LocalPlayer.Ammo.cPrimary != null)
			{
				Peer.ClientGame.LocalPlayer.Ammo.cPrimary.state.clips = i.primaryState.clips;
				Peer.ClientGame.LocalPlayer.Ammo.cPrimary.state.bagSize = i.primaryState.bagSize;
			}
			if (i.secondaryState != null && i.secondaryState.clips != this.secondaryState.clips)
			{
				Peer.ClientGame.LocalPlayer.Ammo.cSecondary.state.clips = i.secondaryState.clips;
				Peer.ClientGame.LocalPlayer.Ammo.cSecondary.state.bagSize = i.secondaryState.bagSize;
			}
			if (this.randomSeed != i.randomSeed)
			{
				Peer.ClientGame.LocalPlayer.Ammo.state.randomSeed = i.randomSeed;
			}
			if (Peer.ClientGame.LocalPlayer.Ammo.state.ServerReloadStart != i.ServerReloadStart && Globals.I.ReloadScheme == ReloadScheme.Asynchronous)
			{
				Peer.ClientGame.LocalPlayer.Ammo.state.ServerReloadStart = i.ServerReloadStart;
				if (i.ServerReloadStart)
				{
					Peer.ClientGame.LocalPlayer.Ammo.Reload();
				}
			}
			if (Globals.I.ReloadScheme != ReloadScheme.OldSynchronous)
			{
				this.BlockFireByServer = i.ServerReloadStart;
			}
		}
		if (this.grenadeCount != i.grenadeCount)
		{
			flag = true;
		}
		if (this.isAim != i.isAim)
		{
		}
		if (this.supportReady != i.supportReady)
		{
			flag = true;
		}
		if (this.primaryIndex != i.primaryIndex)
		{
			flag = true;
		}
		else if (this.primaryMod != i.primaryMod)
		{
			flag = true;
		}
		if (this.secondaryIndex != i.secondaryIndex)
		{
			flag = true;
		}
		else if (this.secondaryMod != i.secondaryMod)
		{
			flag = true;
		}
		if (flag)
		{
			Peer.ClientGame.LocalPlayer.Ammo.state.Restore(i);
		}
		return string.Empty;
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x000C5A0C File Offset: 0x000C3C0C
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"\nEquiped: ",
			this.equiped,
			" \nRandomSeed: ",
			this.randomSeed,
			" \nGrenadeCount: ",
			this.grenadeCount,
			" \nIsAim: ",
			this.isAim,
			" \nSupportReady: ",
			this.supportReady,
			"\n",
			this.secondaryIndex,
			" ",
			this.secondaryMod,
			" ",
			this.secondaryState,
			" ",
			this.primaryIndex,
			" ",
			this.primaryMod,
			" ",
			this.primaryState,
			"\n"
		});
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x000C5B24 File Offset: 0x000C3D24
	public string ToStringTest()
	{
		return string.Concat(new object[]
		{
			"Equiped: ",
			this.equiped,
			" RandomSeed: ",
			this.randomSeed,
			" GrenadeCount: ",
			this.grenadeCount,
			" IsAim: ",
			this.isAim,
			" SupportReady: ",
			this.supportReady,
			" ",
			this.secondaryIndex,
			" ",
			this.secondaryMod,
			" ",
			this.secondaryState,
			" ",
			this.primaryIndex,
			" ",
			this.primaryMod,
			" ",
			this.primaryState
		});
	}

	// Token: 0x0400114C RID: 4428
	public WeaponEquipedState equiped;

	// Token: 0x0400114D RID: 4429
	public WeaponEquipedState preSupportEquiped;

	// Token: 0x0400114E RID: 4430
	public int primaryIndex = 127;

	// Token: 0x0400114F RID: 4431
	public bool primaryMod;

	// Token: 0x04001150 RID: 4432
	public WeaponState primaryState = new WeaponState();

	// Token: 0x04001151 RID: 4433
	public int secondaryIndex = 127;

	// Token: 0x04001152 RID: 4434
	public bool secondaryMod;

	// Token: 0x04001153 RID: 4435
	public WeaponState secondaryState = new WeaponState();

	// Token: 0x04001154 RID: 4436
	public int randomSeed;

	// Token: 0x04001155 RID: 4437
	public int grenadeCount;

	// Token: 0x04001156 RID: 4438
	public bool isAim;

	// Token: 0x04001157 RID: 4439
	public bool supportReady;

	// Token: 0x04001158 RID: 4440
	public bool ServerReloadStart;

	// Token: 0x04001159 RID: 4441
	public bool BlockFireByServer;

	// Token: 0x0400115A RID: 4442
	public int WeaponKit;

	// Token: 0x0400115B RID: 4443
	public InvokerState G = new InvokerState();

	// Token: 0x0400115C RID: 4444
	private short _sPrimaryIndex;

	// Token: 0x0400115D RID: 4445
	private short _sSecondaryIndex;

	// Token: 0x0400115E RID: 4446
	private short _sGrenadeCount;

	// Token: 0x0400115F RID: 4447
	private short _sWeaponKit;
}
