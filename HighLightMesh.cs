using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
[ExecuteInEditMode]
public class HighLightMesh : MonoBehaviour
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000046 RID: 70 RVA: 0x000054E4 File Offset: 0x000036E4
	private Material Mat
	{
		get
		{
			if (this._material == null)
			{
				this._material = new Material(this.TheShader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
				this._material.SetColor("_Color", this.Color);
			}
			return this._material;
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000553C File Offset: 0x0000373C
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.TheShader == null)
		{
			string name = base.GetType().Name;
			Debug.Log(name + " shaders are not set up! Disabling " + name + " effect.");
			base.enabled = false;
		}
		else if (!this.TheShader.isSupported)
		{
			string name2 = base.GetType().Name;
			Debug.Log(name2 + " shaders are not supported! Disabling " + name2 + " effect.");
			base.enabled = false;
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000055D4 File Offset: 0x000037D4
	protected void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
		this._material = null;
		this._lastColor = Color.black;
		this._lastWidth = 0;
		this._lastTarget = null;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00005614 File Offset: 0x00003814
	private void OnPostRender()
	{
		Material mat = this.Mat;
		if (this._lastColor != this.Color)
		{
			this._lastColor = this.Color;
			mat.SetColor("_Color", this.Color);
		}
		if (this._lastWidth != (int)base.camera.pixelWidth || this._lastHeight != (int)base.camera.pixelHeight || this._lastLineWidth != this.LineWidth)
		{
			this._lastWidth = (int)base.camera.pixelWidth;
			this._lastHeight = (int)base.camera.pixelHeight;
			this._lastLineWidth = this.LineWidth;
			mat.SetVector("_Offset", new Vector4(this.LineWidth / base.camera.pixelWidth, this.LineWidth / (float)((int)base.camera.pixelHeight), 0f, 0f));
		}
		if (this._lastTarget != this.Target)
		{
			this._lastTarget = this.Target;
			Renderer[] componentsInChildren = this.Target.GetComponentsInChildren<Renderer>(false);
			this._targetParts = new HighLightMesh.Part[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this._targetParts[i] = new HighLightMesh.Part(componentsInChildren[i].GetComponent<MeshFilter>());
			}
		}
		this.DrawOnScreen(mat, 0, false);
		if (this.Always)
		{
			mat.SetPass(3);
			this.DrawMeshes();
		}
		else
		{
			mat.SetPass(1);
			this.DrawMeshes();
			mat.SetPass(2);
			this.DrawMeshes();
		}
		this.DrawOnScreen(mat, 5, false);
		this.DrawOnScreen(mat, 6, base.camera.renderingPath == RenderingPath.DeferredLighting);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000057D4 File Offset: 0x000039D4
	private void DrawOnScreen(Material material, int pass, bool flipY = false)
	{
		GL.PushMatrix();
		material.SetPass(pass);
		GL.LoadOrtho();
		GL.Begin(7);
		if (flipY)
		{
			GL.TexCoord(new Vector3(0f, 1f));
			GL.Vertex3(0f, 1f, 0f);
			GL.TexCoord(new Vector3(0f, 0f));
			GL.Vertex3(0f, 0f, 0f);
			GL.TexCoord(new Vector3(1f, 0f));
			GL.Vertex3(1f, 0f, 0f);
			GL.TexCoord(new Vector3(1f, 1f));
			GL.Vertex3(1f, 1f, 0f);
		}
		else
		{
			GL.TexCoord(new Vector3(0f, 0f));
			GL.Vertex3(0f, 1f, 0f);
			GL.TexCoord(new Vector3(0f, 1f));
			GL.Vertex3(0f, 0f, 0f);
			GL.TexCoord(new Vector3(1f, 1f));
			GL.Vertex3(1f, 0f, 0f);
			GL.TexCoord(new Vector3(1f, 0f));
			GL.Vertex3(1f, 1f, 0f);
		}
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00005950 File Offset: 0x00003B50
	private void DrawMeshes()
	{
		if (this._targetParts == null)
		{
			return;
		}
		foreach (HighLightMesh.Part part in this._targetParts)
		{
			part.Draw();
		}
	}

	// Token: 0x0400007C RID: 124
	public Shader TheShader;

	// Token: 0x0400007D RID: 125
	public Color Color;

	// Token: 0x0400007E RID: 126
	public float LineWidth;

	// Token: 0x0400007F RID: 127
	public bool Always;

	// Token: 0x04000080 RID: 128
	public Transform Target;

	// Token: 0x04000081 RID: 129
	private Transform _lastTarget;

	// Token: 0x04000082 RID: 130
	private float _lastLineWidth;

	// Token: 0x04000083 RID: 131
	private Material _material;

	// Token: 0x04000084 RID: 132
	private Color _lastColor;

	// Token: 0x04000085 RID: 133
	private HighLightMesh.Part[] _targetParts;

	// Token: 0x04000086 RID: 134
	private int _lastWidth;

	// Token: 0x04000087 RID: 135
	private int _lastHeight;

	// Token: 0x0200000F RID: 15
	public class Part
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00005990 File Offset: 0x00003B90
		public Part(MeshFilter meshFilter)
		{
			this._mesh = meshFilter.sharedMesh;
			this._transform = meshFilter.transform;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000059B0 File Offset: 0x00003BB0
		public void Draw()
		{
			Graphics.DrawMeshNow(this._mesh, this._transform.localToWorldMatrix);
		}

		// Token: 0x04000088 RID: 136
		private Mesh _mesh;

		// Token: 0x04000089 RID: 137
		private Transform _transform;
	}
}
