using System;
using UnityEngine;

// Token: 0x020002B3 RID: 691
[AddComponentMenu("Scripts/Game/Foundation/SpawnPoint")]
public class SpawnPoint : MonoBehaviour
{
	// Token: 0x0600136D RID: 4973 RVA: 0x000D0B4C File Offset: 0x000CED4C
	private void Awake()
	{
		if (base.renderer != null)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x000D0B6C File Offset: 0x000CED6C
	private void OnDrawGizmos()
	{
		if (this.isDeathMatch)
		{
			Gizmos.DrawIcon(base.transform.position, "SP_green.png");
		}
		else if (this.isTeamEllimination)
		{
			Gizmos.DrawIcon(base.transform.position, "SP_gold.png");
		}
		else if (this.isTeam)
		{
			Gizmos.DrawIcon(base.transform.position, (!this.isBear) ? "SP_blue.png" : "SP_red.png");
		}
	}

	// Token: 0x0400168B RID: 5771
	public bool isBear;

	// Token: 0x0400168C RID: 5772
	public bool isTeam;

	// Token: 0x0400168D RID: 5773
	public bool isDeathMatch;

	// Token: 0x0400168E RID: 5774
	public bool isTeamEllimination;
}
