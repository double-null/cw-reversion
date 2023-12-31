using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
internal class FreeMove : MonoBehaviour
{
	// Token: 0x06000532 RID: 1330 RVA: 0x00021124 File Offset: 0x0001F324
	private void Start()
	{
		this._originPosition = base.transform.position;
		this._originalRotation = base.transform.localRotation;
		Screen.lockCursor = true;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0002115C File Offset: 0x0001F35C
	private void Update()
	{
		float num = Input.GetAxis("Mouse X") * this.SensitivityY;
		float num2 = -Input.GetAxis("Mouse Y") * this.SensitivityY;
		float axis = Input.GetAxis("Vertical");
		float axis2 = Input.GetAxis("Mouse ScrollWheel");
		if (Mathf.Abs(axis2) > 0.001f)
		{
			this.MoveSpeed += axis2;
		}
		if (Mathf.Abs(num) > 0.001f || Mathf.Abs(num2) > 0.001f)
		{
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			localEulerAngles.z = 0f;
			localEulerAngles.x += num2;
			localEulerAngles.y += num;
			localEulerAngles.y = FreeMove.ClampAngle(localEulerAngles.y);
			localEulerAngles.x = FreeMove.ClampAngle(localEulerAngles.x);
			base.transform.localEulerAngles = localEulerAngles;
		}
		if (Mathf.Abs(axis) > 0.001f)
		{
			base.transform.Translate(Vector3.forward * this.MoveSpeed * axis);
		}
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00021280 File Offset: 0x0001F480
	public static float ClampAngle(float angle)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return angle;
	}

	// Token: 0x0400049E RID: 1182
	private Vector3 _originPosition = default(Vector3);

	// Token: 0x0400049F RID: 1183
	private Quaternion _originalRotation = default(Quaternion);

	// Token: 0x040004A0 RID: 1184
	private Vector3 _deltaRotation = default(Vector3);

	// Token: 0x040004A1 RID: 1185
	public float SensitivityY = 5f;

	// Token: 0x040004A2 RID: 1186
	public float MoveSpeed = 0.01f;
}
