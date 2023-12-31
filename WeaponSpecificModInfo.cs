using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034C RID: 844
internal class WeaponSpecificModInfo
{
	// Token: 0x06001C19 RID: 7193 RVA: 0x000FAF38 File Offset: 0x000F9138
	public WeaponSpecificModInfo(Dictionary<string, object> modData, int metaId, int index)
	{
		this.HasParent = (modData.ContainsKey("p_meta") && modData.ContainsKey("p_col"));
		if (modData.ContainsKey("device"))
		{
			this.Device = modData["device"].ToString();
		}
		if (modData.ContainsKey("p_meta"))
		{
			this.ParentLevel = int.Parse(modData["p_meta"].ToString());
		}
		if (modData.ContainsKey("p_col"))
		{
			this.ParentIndex = int.Parse(modData["p_col"].ToString());
		}
		if (modData.ContainsKey("mp"))
		{
			this.Mp = int.Parse(modData["mp"].ToString());
		}
		if (modData.ContainsKey("aimfov"))
		{
			this.AimFov = int.Parse(modData["aimfov"].ToString());
		}
		this.Level = metaId;
		this.Index = index;
		if (modData.ContainsKey("scale"))
		{
			float num = float.Parse(modData["scale"].ToString());
			this.Scale = new Vector3(num, num, num);
		}
		float x = 0f;
		if (modData.ContainsKey("xshift"))
		{
			x = float.Parse(modData["xshift"].ToString());
		}
		float y = 0f;
		if (modData.ContainsKey("yshift"))
		{
			y = float.Parse(modData["yshift"].ToString());
		}
		float z = 0f;
		if (modData.ContainsKey("zshift"))
		{
			z = float.Parse(modData["zshift"].ToString());
		}
		this.Shift = new Vector3(x, y, z);
		float x2 = 0f;
		if (modData.ContainsKey("xdelta"))
		{
			x2 = float.Parse(modData["xdelta"].ToString());
		}
		float y2 = 0f;
		if (modData.ContainsKey("ydelta"))
		{
			y2 = float.Parse(modData["ydelta"].ToString());
		}
		float z2 = 0f;
		if (modData.ContainsKey("zdelta"))
		{
			z2 = float.Parse(modData["zdelta"].ToString());
		}
		this.AimDelta = new Vector3(x2, y2, z2);
		if (modData.ContainsKey("disabled"))
		{
			this.DisabledGameObjects = modData["disabled"].ToString().Split(new char[]
			{
				' ',
				','
			}, StringSplitOptions.RemoveEmptyEntries);
		}
	}

	// Token: 0x040020CF RID: 8399
	public int Mp;

	// Token: 0x040020D0 RID: 8400
	public int Level;

	// Token: 0x040020D1 RID: 8401
	public int Index;

	// Token: 0x040020D2 RID: 8402
	public int ParentLevel;

	// Token: 0x040020D3 RID: 8403
	public int ParentIndex;

	// Token: 0x040020D4 RID: 8404
	public bool HasParent;

	// Token: 0x040020D5 RID: 8405
	public int AimFov;

	// Token: 0x040020D6 RID: 8406
	public string Device;

	// Token: 0x040020D7 RID: 8407
	public string[] DisabledGameObjects = new string[0];

	// Token: 0x040020D8 RID: 8408
	public Vector3 Scale = Vector3.zero;

	// Token: 0x040020D9 RID: 8409
	public Vector3 Shift = Vector3.zero;

	// Token: 0x040020DA RID: 8410
	public Vector3 AimDelta = Vector3.zero;
}
