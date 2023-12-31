using System;
using UnityEngine;

// Token: 0x0200036F RID: 879
[AddComponentMenu("Scripts/Engine/Components/CustomTerrainScript")]
internal class CustomTerrainScript : MonoBehaviour
{
	// Token: 0x06001C9C RID: 7324 RVA: 0x000FD8A4 File Offset: 0x000FBAA4
	private void Awake()
	{
		this.terrainComp = (Terrain)base.GetComponent(typeof(Terrain));
		if (this.Bump0)
		{
			Shader.SetGlobalTexture("_BumpMap0", this.Bump0);
		}
		if (this.Bump1)
		{
			Shader.SetGlobalTexture("_BumpMap1", this.Bump1);
		}
		if (this.Bump2)
		{
			Shader.SetGlobalTexture("_BumpMap2", this.Bump2);
		}
		if (this.Bump3)
		{
			Shader.SetGlobalTexture("_BumpMap3", this.Bump3);
		}
		Shader.SetGlobalVector("_Tile01", new Vector4(this.terrainComp.terrainData.splatPrototypes[0].tileSize.x, this.terrainComp.terrainData.splatPrototypes[0].tileSize.y, this.terrainComp.terrainData.splatPrototypes[1].tileSize.x, this.terrainComp.terrainData.splatPrototypes[1].tileSize.y));
		Shader.SetGlobalVector("_Tile23", new Vector4(this.terrainComp.terrainData.splatPrototypes[2].tileSize.x, this.terrainComp.terrainData.splatPrototypes[2].tileSize.y, this.terrainComp.terrainData.splatPrototypes[3].tileSize.x, this.terrainComp.terrainData.splatPrototypes[3].tileSize.y));
		Shader.SetGlobalVector("_Terrain", new Vector4(this.terrainComp.terrainData.size.x, this.terrainComp.terrainData.size.z, 0f, 0f));
	}

	// Token: 0x04002175 RID: 8565
	public Texture2D Bump0;

	// Token: 0x04002176 RID: 8566
	public Texture2D Bump1;

	// Token: 0x04002177 RID: 8567
	public Texture2D Bump2;

	// Token: 0x04002178 RID: 8568
	public Texture2D Bump3;

	// Token: 0x04002179 RID: 8569
	private Terrain terrainComp;
}
