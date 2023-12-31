using System;
using UnityEngine;

// Token: 0x020001AF RID: 431
[AddComponentMenu("Scripts/Game/ClientTargetDesignationGame")]
internal class ClientTargetDesignationGame : BaseClientGame
{
	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x000AA7F0 File Offset: 0x000A89F0
	public override bool IsTeamGame
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x000AA7F4 File Offset: 0x000A89F4
	public override bool IsRounedGame
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x000AA7F8 File Offset: 0x000A89F8
	public override void Deserialize(eNetworkStream stream)
	{
		base.Deserialize(stream);
		int state = 0;
		stream.Serialize(ref state);
		this.state = (MatchState)state;
		float num = 0f;
		stream.Serialize(ref num);
		this.nextEventTime = Time.realtimeSinceStartup + num;
		stream.Serialize(ref this.usecWinCount);
		stream.Serialize(ref this.bearWinCount);
	}
}
