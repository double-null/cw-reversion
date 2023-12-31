using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class RainColliderHighlighter : MonoBehaviour
{
	// Token: 0x060000DD RID: 221 RVA: 0x0000B1B4 File Offset: 0x000093B4
	private void OnDrawGizmos()
	{
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.color = new Color(0f, 0.3f, 0.7f, 0.2f);
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawCube(Vector3.zero, Vector3.one);
		Gizmos.matrix = matrix;
	}
}
