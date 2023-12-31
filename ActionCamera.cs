using System;
using UnityEngine;

// Token: 0x020001B4 RID: 436
internal class ActionCamera : MonoBehaviour
{
	// Token: 0x06000EF3 RID: 3827 RVA: 0x000ADAD4 File Offset: 0x000ABCD4
	private void Start()
	{
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
	private void Update()
	{
		if (this.Enable)
		{
			if (Input.GetKey(KeyCode.Keypad5))
			{
			}
			if (Input.GetKey(KeyCode.Keypad2))
			{
				MonoBehaviour.print("On sit");
				base.transform.Translate(0f, 0f, 0f);
				base.transform.Rotate(0f, 0f, this.sitCameraRotate.Evaluate(Time.time));
			}
			if (Input.GetKey(KeyCode.Keypad8))
			{
				MonoBehaviour.print("On jump");
				base.transform.Translate(0f, 0f, 0f);
				base.transform.Rotate(this.jumpCameraRotate.Evaluate(Time.time), 0f, 0f);
			}
		}
	}

	// Token: 0x04000F51 RID: 3921
	public bool Enable = true;

	// Token: 0x04000F52 RID: 3922
	public AnimationCurve jumpCameraRotate = new AnimationCurve();

	// Token: 0x04000F53 RID: 3923
	public AnimationCurve jumpCameraTranslate = new AnimationCurve();

	// Token: 0x04000F54 RID: 3924
	public AnimationCurve sitCameraRotate = new AnimationCurve();

	// Token: 0x04000F55 RID: 3925
	public AnimationCurve sitCameraTranslate = new AnimationCurve();
}
