using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000217 RID: 535
[AddComponentMenu("Scripts/Game/Events/FocusController")]
internal class FocusController : DatabaseEvent
{
	// Token: 0x1700025F RID: 607
	// (get) Token: 0x060010EC RID: 4332 RVA: 0x000BD390 File Offset: 0x000BB590
	public bool isBlured
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x000BD394 File Offset: 0x000BB594
	[Obfuscation(Exclude = true)]
	public void OnBlur(string cmd)
	{
		Audio.Disable();
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x000BD39C File Offset: 0x000BB59C
	[Obfuscation(Exclude = true)]
	public void OnFocus(string cmd)
	{
		Audio.Enable();
	}
}
