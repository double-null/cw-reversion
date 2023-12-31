using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
[ExecuteInEditMode]
public class LocalDustParticles : MonoBehaviour
{
	// Token: 0x06000033 RID: 51 RVA: 0x000046CC File Offset: 0x000028CC
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Color particlesColor = this.ParticlesColor;
		particlesColor.a *= 0.1f;
		Gizmos.color = particlesColor;
		Gizmos.DrawCube(new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
		particlesColor.a = 0.5f;
		Gizmos.color = particlesColor;
		Gizmos.DrawWireCube(new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
		if (this.PurticlesParent == null)
		{
			return;
		}
		if (this._lastMatrix != base.transform.localToWorldMatrix || this._lastCount != this.Count || this._lastSideSpeed != this.SideSpeed || this._lastParticlesColor != this.ParticlesColor || this._lastAlphaRandomness != this.AlphaRandomness || this._lastSizeRandomness != this.SizeRandomness)
		{
			this._lastParticlesColor = this.ParticlesColor;
			this._lastSideSpeed = this.SideSpeed;
			this._lastCount = this.Count;
			this._lastMatrix = base.transform.localToWorldMatrix;
			this._lastAlphaRandomness = this.AlphaRandomness;
			this._lastSizeRandomness = this.SizeRandomness;
			this.PurticlesParent.GenerateMesh();
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004854 File Offset: 0x00002A54
	private void OnEnable()
	{
	}

	// Token: 0x04000066 RID: 102
	public LocalDustParticlesParent PurticlesParent;

	// Token: 0x04000067 RID: 103
	public int Count = 100;

	// Token: 0x04000068 RID: 104
	public Color ParticlesColor = Color.white;

	// Token: 0x04000069 RID: 105
	public float AlphaRandomness;

	// Token: 0x0400006A RID: 106
	public float SizeRandomness;

	// Token: 0x0400006B RID: 107
	public float SideSpeed;

	// Token: 0x0400006C RID: 108
	private Matrix4x4 _lastMatrix;

	// Token: 0x0400006D RID: 109
	private int _lastCount;

	// Token: 0x0400006E RID: 110
	private float _lastSideSpeed;

	// Token: 0x0400006F RID: 111
	private Color _lastParticlesColor;

	// Token: 0x04000070 RID: 112
	private float _lastAlphaRandomness;

	// Token: 0x04000071 RID: 113
	private float _lastSizeRandomness;
}
