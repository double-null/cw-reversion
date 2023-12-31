using System;
using Edelweiss.DecalSystem;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class DS_SkinnedDecals : SkinnedDecals
{
	// Token: 0x060000FA RID: 250 RVA: 0x0000B8DC File Offset: 0x00009ADC
	protected override SkinnedDecalsMeshRenderer AddSkinnedDecalsMeshRendererComponentToGameObject(GameObject a_GameObject)
	{
		return a_GameObject.AddComponent<DS_SkinnedDecalsMeshRenderer>();
	}
}
