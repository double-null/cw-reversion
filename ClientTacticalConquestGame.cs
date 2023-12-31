using System;
using UnityEngine;

// Token: 0x020001AE RID: 430
internal class ClientTacticalConquestGame : BaseClientGame
{
	// Token: 0x06000EAA RID: 3754 RVA: 0x000AA54C File Offset: 0x000A874C
	public override void OnConnected()
	{
		ClientTacticalConquestGame._game = this;
		base.OnConnected();
		this.InitTacticalPoint();
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x000AA560 File Offset: 0x000A8760
	public override void OnDisconnect()
	{
		base.OnDisconnect();
		this.TacticalPoints = null;
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x000AA570 File Offset: 0x000A8770
	public static void InitTP()
	{
		if (ClientTacticalConquestGame._game != null)
		{
			ClientTacticalConquestGame._game.InitTacticalPoint();
		}
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x000AA58C File Offset: 0x000A878C
	private void InitTacticalPoint()
	{
		this.TacticalPoints = (TacticalPoint[])UnityEngine.Object.FindSceneObjectsOfType(typeof(TacticalPoint));
		this.SortTP(this.TacticalPoints);
		foreach (TacticalPoint tacticalPoint in this.TacticalPoints)
		{
			tacticalPoint.SetAsClientEntity();
		}
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x000AA5E4 File Offset: 0x000A87E4
	private void SortTP(TacticalPoint[] ob)
	{
		for (int i = 0; i < ob.Length; i++)
		{
			for (int j = 0; j < ob.Length; j++)
			{
				if (ob[i].NumberOfPoint < ob[j].NumberOfPoint)
				{
					TacticalPoint tacticalPoint = ob[i];
					ob[i] = ob[j];
					ob[j] = tacticalPoint;
				}
			}
		}
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x000AA644 File Offset: 0x000A8844
	public void GetTCSound(string name, PoolItem poolItem)
	{
		if (poolItem != null)
		{
			foreach (AudioClip audioClip in SingletoneForm<SoundFactory>.Instance.TacticalConquestSounds)
			{
				if (audioClip.name.Contains(name))
				{
					Audio.PlayTyped(poolItem, SoundType.radio, audioClip, true, 0f, 1000000f);
					return;
				}
			}
		}
		Debug.LogWarning("Missing sound: " + name);
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x000AA6BC File Offset: 0x000A88BC
	public override bool IsTeamGame
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x000AA6C0 File Offset: 0x000A88C0
	public override void OnMatchEnd()
	{
		base.LocalPlayer.InPoint = -1;
		base.OnMatchEnd();
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x000AA6D4 File Offset: 0x000A88D4
	public override void Deserialize(eNetworkStream stream)
	{
		base.Deserialize(stream);
		int state = 0;
		stream.Serialize(ref state);
		this.state = (MatchState)state;
		float num = 0f;
		stream.Serialize(ref num);
		this.nextEventTime = Time.realtimeSinceStartup + num;
		stream.Serialize(ref this.bearWinCount);
		stream.Serialize(ref this.usecWinCount);
		short num2 = 0;
		stream.Serialize(ref num2);
		try
		{
			for (int i = 0; i < (int)num2; i++)
			{
				if (this.TacticalPoints == null || i >= this.TacticalPoints.Length)
				{
					this.ZaglushkaTactiaclPoint(stream);
				}
				else
				{
					this.TacticalPoints[i].Deserialize(stream);
				}
			}
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x000AA7AC File Offset: 0x000A89AC
	protected void ZaglushkaTactiaclPoint(eNetworkStream stream)
	{
		short num = 0;
		short num2 = 0;
		int num3 = 0;
		float num4 = 0f;
		stream.Serialize(ref num);
		stream.Serialize(ref num2);
		stream.Serialize(ref num4);
		stream.Serialize(ref num3);
	}

	// Token: 0x04000F35 RID: 3893
	private static ClientTacticalConquestGame _game;

	// Token: 0x04000F36 RID: 3894
	public TacticalPoint[] TacticalPoints;
}
