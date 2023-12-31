using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AdditiveReplacer : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x0000210C File Offset: 0x0000030C
	private void Start()
	{
		foreach (AdditiveReplacer.ShaderReplacer shaderReplacer in this.Shaders)
		{
			if (this._shaders.ContainsKey(shaderReplacer.Name))
			{
				Debug.Log("ContainsKey" + shaderReplacer.Name);
			}
			this._shaders.Add(shaderReplacer.Name, shaderReplacer.Shader);
		}
		foreach (AdditiveReplacer.MaterialReplacer materialReplacer in this.Materials)
		{
			this._materials.Add(materialReplacer.Name, materialReplacer);
			this._materials.Add(materialReplacer.Name + " (Instance)", materialReplacer);
		}
		this.ReplaceShaders();
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000021D8 File Offset: 0x000003D8
	[ContextMenu("ReplaceShaders")]
	private void ReplaceShaders()
	{
		LevelSettings levelSettings = (LevelSettings)UnityEngine.Object.FindObjectOfType(typeof(LevelSettings));
		GameObject gameObject = levelSettings.gameObject;
		if (this.TerrainMaterial != null)
		{
			Terrain componentInChildren = gameObject.GetComponentInChildren<Terrain>();
			if (componentInChildren != null)
			{
				componentInChildren.materialTemplate = this.TerrainMaterial;
			}
		}
		Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			Material[] materials = renderer.materials;
			for (int j = 0; j < materials.Length; j++)
			{
				AdditiveReplacer.MaterialReplacer materialReplacer;
				Shader shader;
				if (this._materials.TryGetValue(materials[j].name, out materialReplacer))
				{
					materials[j] = materialReplacer.Replays(materials[j]);
				}
				else if (this._shaders.TryGetValue(materials[j].shader.name, out shader))
				{
					materials[j].shader = shader;
				}
			}
			renderer.materials = materials;
		}
	}

	// Token: 0x04000001 RID: 1
	public AdditiveReplacer.ShaderReplacer[] Shaders;

	// Token: 0x04000002 RID: 2
	private Dictionary<string, Shader> _shaders = new Dictionary<string, Shader>();

	// Token: 0x04000003 RID: 3
	public AdditiveReplacer.MaterialReplacer[] Materials;

	// Token: 0x04000004 RID: 4
	private Dictionary<string, AdditiveReplacer.MaterialReplacer> _materials = new Dictionary<string, AdditiveReplacer.MaterialReplacer>();

	// Token: 0x04000005 RID: 5
	public Material TerrainMaterial;

	// Token: 0x02000003 RID: 3
	[Serializable]
	public class ShaderReplacer
	{
		// Token: 0x04000006 RID: 6
		public Shader Shader;

		// Token: 0x04000007 RID: 7
		public string Name;
	}

	// Token: 0x02000004 RID: 4
	[Serializable]
	public class MaterialReplacer
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000022F8 File Offset: 0x000004F8
		public Material Replays(Material mat)
		{
			if (!this._initialized)
			{
				foreach (string propertyName in this.InheritPropertys)
				{
					this.Material.SetTexture(propertyName, mat.GetTexture(propertyName));
				}
				this._initialized = true;
			}
			return this.Material;
		}

		// Token: 0x04000008 RID: 8
		public Material Material;

		// Token: 0x04000009 RID: 9
		public string Name;

		// Token: 0x0400000A RID: 10
		public string[] InheritPropertys;

		// Token: 0x0400000B RID: 11
		private bool _initialized;
	}
}
