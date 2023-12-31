using System;
using UnityEngine;

// Token: 0x0200025A RID: 602
[Serializable]
internal class EntityState : cwTime, cwNetworkSerializable, ReusableClass<EntityState>
{
	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06001277 RID: 4727 RVA: 0x000CA0CC File Offset: 0x000C82CC
	// (set) Token: 0x06001278 RID: 4728 RVA: 0x000CA0D4 File Offset: 0x000C82D4
	public float Time
	{
		get
		{
			return this.time;
		}
		set
		{
			this.time = value;
		}
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x000CA0E0 File Offset: 0x000C82E0
	public void Serialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.ID);
		int num = (int)this.type;
		stream.Serialize(ref num);
		if (this.type == EntityType.placement)
		{
			stream.Serialize(ref this.pos);
			stream.Serialize(ref this.euler);
			stream.Serialize(ref this.marker);
			stream.Serialize(ref this.isBear);
			stream.Serialize(ref this.playerID);
			char c = (char)this.placementState;
			stream.Serialize(ref c);
			stream.Serialize(ref this.plaecementProgress);
			stream.Serialize(ref this.sonarTime);
			stream.Serialize<PlacementType>(ref this.PointType);
		}
		if (this.type == EntityType.tactical_point)
		{
			int num2 = (int)this.tacticalPointState;
			stream.Serialize(ref this.TacticalPointNum);
			stream.Serialize(ref this.captureProgress);
			stream.Serialize(ref num2);
			stream.Serialize(ref this.usecCount);
			stream.Serialize(ref this.bearCount);
			stream.Serialize(ref this.playersNeeded);
		}
		if (this.type == EntityType.grenade || this.type == EntityType.mortar || this.type == EntityType.sonar || this.type == EntityType.beacon)
		{
			stream.Serialize(ref this.pos);
			stream.Serialize(ref this.euler);
			stream.Serialize(ref this.marker);
			stream.Serialize(ref this.isBear);
			stream.Serialize(ref this.playerID);
			stream.Serialize(ref this.sonarTime);
		}
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x000CA254 File Offset: 0x000C8454
	public void Deserialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.ID);
		int num = 0;
		stream.Serialize(ref num);
		this.type = (EntityType)num;
		if (this.type == EntityType.placement)
		{
			stream.Serialize(ref this.pos);
			stream.Serialize(ref this.euler);
			stream.Serialize(ref this.marker);
			stream.Serialize(ref this.isBear);
			stream.Serialize(ref this.playerID);
			char c = '\0';
			stream.Serialize(ref c);
			this.placementState = (PlacementState)c;
			stream.Serialize(ref this.plaecementProgress);
			stream.Serialize(ref this.sonarTime);
			stream.Serialize<PlacementType>(ref this.PointType);
		}
		if (this.type == EntityType.tactical_point)
		{
			int num2 = 0;
			stream.Serialize(ref this.TacticalPointNum);
			stream.Serialize(ref this.captureProgress);
			stream.Serialize(ref num2);
			this.tacticalPointState = (TacticalPointState)num2;
			stream.Serialize(ref this.usecCount);
			stream.Serialize(ref this.bearCount);
			stream.Serialize(ref this.playersNeeded);
		}
		if (this.type == EntityType.grenade || this.type == EntityType.mortar || this.type == EntityType.sonar || this.type == EntityType.beacon)
		{
			stream.Serialize(ref this.pos);
			stream.Serialize(ref this.euler);
			stream.Serialize(ref this.marker);
			stream.Serialize(ref this.isBear);
			stream.Serialize(ref this.playerID);
			stream.Serialize(ref this.sonarTime);
		}
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x000CA3CC File Offset: 0x000C85CC
	public void Clear()
	{
		this.ID = IDUtil.NoID;
		this.type = EntityType.none;
		this.pos = Vector3.zero;
		this.euler = Vector3.zero;
		this.marker = false;
		this.isBear = false;
		this.placementState = PlacementState.none;
		this.plaecementProgress = 0f;
		this.playerID = IDUtil.NoID;
		this.sonarTime = 0;
		this.PointType = PlacementType.end;
		this.tacticalPointState = TacticalPointState.neutral;
		this.captureProgress = 0f;
		this.time = 0f;
		this.usecCount = 0;
		this.bearCount = 0;
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x000CA468 File Offset: 0x000C8668
	public void Clone(EntityState i)
	{
		this.ID = i.ID;
		this.type = i.type;
		this.pos = i.pos;
		this.euler = i.euler;
		this.marker = i.marker;
		this.isBear = i.isBear;
		this.placementState = i.placementState;
		this.plaecementProgress = i.plaecementProgress;
		this.playerID = i.playerID;
		this.sonarTime = i.sonarTime;
		this.PointType = i.PointType;
		this.time = i.time;
	}

	// Token: 0x0400122D RID: 4653
	public int ID = IDUtil.NoID;

	// Token: 0x0400122E RID: 4654
	public EntityType type = EntityType.none;

	// Token: 0x0400122F RID: 4655
	public Vector3 pos = Vector3.zero;

	// Token: 0x04001230 RID: 4656
	public Vector3 euler = Vector3.zero;

	// Token: 0x04001231 RID: 4657
	public bool marker;

	// Token: 0x04001232 RID: 4658
	public bool isBear;

	// Token: 0x04001233 RID: 4659
	public PlacementState placementState;

	// Token: 0x04001234 RID: 4660
	public float plaecementProgress;

	// Token: 0x04001235 RID: 4661
	public int playerID = IDUtil.NoID;

	// Token: 0x04001236 RID: 4662
	public int sonarTime;

	// Token: 0x04001237 RID: 4663
	public PlacementType PointType = PlacementType.end;

	// Token: 0x04001238 RID: 4664
	public int usecCount;

	// Token: 0x04001239 RID: 4665
	public int bearCount;

	// Token: 0x0400123A RID: 4666
	public int playersNeeded;

	// Token: 0x0400123B RID: 4667
	public TacticalPointState tacticalPointState = TacticalPointState.neutral;

	// Token: 0x0400123C RID: 4668
	public int TacticalPointNum;

	// Token: 0x0400123D RID: 4669
	public float captureProgress;

	// Token: 0x0400123E RID: 4670
	public float secondsForNextPointAdd;

	// Token: 0x0400123F RID: 4671
	private float time;
}
