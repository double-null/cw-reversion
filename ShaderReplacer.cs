using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
[AddComponentMenu("Scripts/Engine/Foundation/ShaderReplacer")]
internal class ShaderReplacer : MonoBehaviour
{
	// Token: 0x06000374 RID: 884 RVA: 0x0001927C File Offset: 0x0001747C
	public void Init()
	{
		this.shaderNames = new string[base.renderer.materials.Length];
		for (int i = 0; i < this.shaderNames.Length; i++)
		{
			this.shaderNames[i] = base.renderer.materials[i].shader.name;
		}
	}

	// Token: 0x06000375 RID: 885 RVA: 0x000192DC File Offset: 0x000174DC
	public void Replace(QualityLevelUser level)
	{
		for (int i = 0; i < this.shaderNames.Length; i++)
		{
			string text = this.shaderNames[i];
			if (!text.Contains("Transparent") && !text.Contains("Particle"))
			{
				text = "Diffuse";
				base.renderer.materials[i].shader = Shader.Find(text);
			}
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00019350 File Offset: 0x00017550
	public void Reset()
	{
		for (int i = 0; i < this.shaderNames.Length; i++)
		{
			base.renderer.materials[i].shader = Shader.Find(this.shaderNames[i]);
		}
	}

	// Token: 0x04000397 RID: 919
	private string[] shaderNames;
}
