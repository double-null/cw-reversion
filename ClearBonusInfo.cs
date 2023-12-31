using System;
using UnityEngine;

// Token: 0x02000211 RID: 529
[AddComponentMenu("Scripts/Game/Events/ClearBonusInfo")]
internal class ClearBonusInfo : DatabaseEvent
{
	// Token: 0x060010D4 RID: 4308 RVA: 0x000BCBAC File Offset: 0x000BADAC
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("?action=clearNewLevel", null, null, string.Empty, string.Empty);
	}
}
