using System;
using UnityEngine;

// Token: 0x02000372 RID: 882
internal class TreeShake : MonoBehaviour
{
	// Token: 0x06001CA8 RID: 7336 RVA: 0x000FDCD8 File Offset: 0x000FBED8
	private void Start()
	{
		Shader.SetGlobalColor("_Wind", this.Wind);
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x000FDCF0 File Offset: 0x000FBEF0
	private void Update()
	{
		Color color = this.Wind * Mathf.Sin(Time.realtimeSinceStartup * this.WindFrequency);
		color.a = this.Wind.w;
		Shader.SetGlobalColor("_Wind", color);
	}

	// Token: 0x04002184 RID: 8580
	public Vector4 Wind = new Vector4(0.08f, 0.03f, 0.05f, 0.2f);

	// Token: 0x04002185 RID: 8581
	public float WindFrequency = 0.75f;
}
