using System;
using Edelweiss.DecalSystem;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class DS_Decals : Decals
{
	// Token: 0x060000F5 RID: 245 RVA: 0x0000B8A4 File Offset: 0x00009AA4
	protected override DecalsMeshRenderer AddDecalsMeshRendererComponentToGameObject(GameObject a_GameObject)
	{
		return a_GameObject.AddComponent<DS_DecalsMeshRenderer>();
	}
}
