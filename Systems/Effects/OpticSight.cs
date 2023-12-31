using System;
using UnityEngine;

namespace Systems.Effects
{
	// Token: 0x020003A7 RID: 935
	public class OpticSight : MonoBehaviour
	{
		// Token: 0x06001E06 RID: 7686 RVA: 0x00105CB4 File Offset: 0x00103EB4
		private void Awake()
		{
			this._mesh = OpticSight.GetMesh();
			this.UpdateMinDot();
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00105CC8 File Offset: 0x00103EC8
		private static Mesh GetMesh()
		{
			Vector3[] vertices = new Vector3[]
			{
				new Vector3(-1f, -1f, 0f),
				new Vector3(-1f, 1f, 0f),
				new Vector3(1f, -1f, 0f),
				new Vector3(1f, 1f, 0f)
			};
			Vector2[] uv = new Vector2[]
			{
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 0f),
				new Vector2(1f, 1f)
			};
			int[] triangles = new int[]
			{
				0,
				2,
				3,
				3,
				1,
				0
			};
			return new Mesh
			{
				vertices = vertices,
				uv = uv,
				triangles = triangles
			};
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00105E04 File Offset: 0x00104004
		private void Update()
		{
			if (this.OpticCamera == null)
			{
				return;
			}
			if (this.OpticCamera.enabled)
			{
				Graphics.DrawMesh(this._mesh, Vector3.zero, Quaternion.identity, this.CameraFrontWindow, 0, this.OpticCamera, 0, null, false, true);
			}
			if (Camera.current == null)
			{
				return;
			}
			float dot = this.GetDot();
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00105E74 File Offset: 0x00104074
		private void UpdateMinDot()
		{
			this._minDot = Mathf.Cos(this.Angle * 0.017453292f);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00105E90 File Offset: 0x00104090
		private float GetDot()
		{
			Transform transform = Camera.current.transform;
			Transform transform2 = base.transform;
			Vector3 normalized = (transform.position - transform2.position).normalized;
			Vector3 forward = transform2.forward;
			switch (this.SightDirection)
			{
			case OpticSight.Direction.X_negative:
				forward = new Vector3(-1f, 0f, 0f);
				break;
			case OpticSight.Direction.Y_negative:
				forward = new Vector3(0f, -1f, 0f);
				break;
			case OpticSight.Direction.Z_negative:
				forward = new Vector3(0f, 0f, -1f);
				break;
			case OpticSight.Direction.X_positive:
				forward = new Vector3(1f, 0f, 0f);
				break;
			case OpticSight.Direction.Y_positive:
				forward = new Vector3(0f, 1f, 0f);
				break;
			case OpticSight.Direction.Z_positive:
				forward = new Vector3(0f, 0f, 1f);
				break;
			}
			return Vector3.Dot(normalized, transform2.TransformDirection(forward));
		}

		// Token: 0x04002268 RID: 8808
		public Material CameraFrontWindow;

		// Token: 0x04002269 RID: 8809
		public Camera OpticCamera;

		// Token: 0x0400226A RID: 8810
		public OpticSight.Direction SightDirection;

		// Token: 0x0400226B RID: 8811
		private float _minDot;

		// Token: 0x0400226C RID: 8812
		public float Angle;

		// Token: 0x0400226D RID: 8813
		private Mesh _mesh;

		// Token: 0x020003A8 RID: 936
		public enum Direction
		{
			// Token: 0x0400226F RID: 8815
			X_negative,
			// Token: 0x04002270 RID: 8816
			Y_negative,
			// Token: 0x04002271 RID: 8817
			Z_negative,
			// Token: 0x04002272 RID: 8818
			X_positive,
			// Token: 0x04002273 RID: 8819
			Y_positive,
			// Token: 0x04002274 RID: 8820
			Z_positive
		}
	}
}
