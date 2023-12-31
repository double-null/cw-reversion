using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
[AddComponentMenu("Scripts/GUI/Sprite/cwSprite")]
internal class cwSprite : PoolableBehaviour
{
	// Token: 0x0600083D RID: 2109 RVA: 0x00049BD4 File Offset: 0x00047DD4
	protected override void Awake()
	{
		this.mesh = new Mesh();
		this.mesh.name = "gui_mesh";
		this.indices[0] = 0;
		this.indices[1] = 1;
		this.indices[2] = 2;
		this.indices[3] = 0;
		this.indices[4] = 2;
		this.indices[5] = 3;
		this.uvs[0] = new Vector2(0f, 0f);
		this.uvs[1] = new Vector2(0f, 1f);
		this.uvs[2] = new Vector2(1f, 1f);
		this.uvs[3] = new Vector2(1f, 0f);
		this.mesh.vertices = this.vertices;
		this.mesh.triangles = this.indices;
		this.mesh.uv = this.uvs;
		this.filter = base.gameObject.AddComponent<MeshFilter>();
		base.gameObject.AddComponent<MeshRenderer>();
		base.renderer.material = this.material;
		base.renderer.material.mainTexture = this.picture;
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00049D2C File Offset: 0x00047F2C
	private void Update()
	{
		if (Camera.mainCamera != null)
		{
			Vector3 vector = this.pos;
			this.vertices[0] = new Vector3(vector.x, vector.z, Camera.mainCamera.nearClipPlane);
			this.vertices[1] = new Vector3(vector.x, vector.z + (float)this.picture.height, Camera.mainCamera.nearClipPlane);
			this.vertices[2] = new Vector3(vector.x + (float)this.picture.width, vector.z + (float)this.picture.height, Camera.mainCamera.nearClipPlane);
			this.vertices[3] = new Vector3(vector.x + (float)this.picture.width, vector.z, Camera.mainCamera.nearClipPlane);
			for (int i = 0; i < 4; i++)
			{
				this.vertices[i] = Quaternion.Euler(0f, Time.realtimeSinceStartup * 90f, 0f) * Camera.mainCamera.ScreenToWorldPoint(this.vertices[i]);
			}
			this.mesh.vertices = this.vertices;
			this.filter.mesh = this.mesh;
		}
	}

	// Token: 0x04000949 RID: 2377
	public Vector3 pos;

	// Token: 0x0400094A RID: 2378
	public Texture2D picture;

	// Token: 0x0400094B RID: 2379
	public Material material;

	// Token: 0x0400094C RID: 2380
	private Mesh mesh;

	// Token: 0x0400094D RID: 2381
	private MeshFilter filter;

	// Token: 0x0400094E RID: 2382
	private Vector3[] vertices = new Vector3[4];

	// Token: 0x0400094F RID: 2383
	private Vector2[] uvs = new Vector2[4];

	// Token: 0x04000950 RID: 2384
	private int[] indices = new int[6];
}
