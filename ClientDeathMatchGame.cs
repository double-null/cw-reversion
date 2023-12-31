using System;
using UnityEngine;

// Token: 0x020001AA RID: 426
[AddComponentMenu("Scripts/Game/ClientDeathMatchGame")]
internal class ClientDeathMatchGame : BaseClientGame
{
	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000E5D RID: 3677 RVA: 0x000A76DC File Offset: 0x000A58DC
	public override bool IsTeamGame
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x000A76E0 File Offset: 0x000A58E0
	public override void Deserialize(eNetworkStream stream)
	{
		base.Deserialize(stream);
		int state = 0;
		stream.Serialize(ref state);
		this.state = (MatchState)state;
		float num = 0f;
		stream.Serialize(ref num);
		this.nextEventTime = Time.realtimeSinceStartup + num;
	}
}
