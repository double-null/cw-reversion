using System;
using UnityEngine;

// Token: 0x020003A1 RID: 929
[AddComponentMenu("Scripts/Engine/Components/TimeScaler")]
internal class TimeScaler : MonoBehaviour
{
	// Token: 0x06001D98 RID: 7576 RVA: 0x001035DC File Offset: 0x001017DC
	public void Awake()
	{
		Time.timeScale = this.StartScale;
	}

	// Token: 0x06001D99 RID: 7577 RVA: 0x001035EC File Offset: 0x001017EC
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			Time.timeScale = 1f;
		}
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			Time.timeScale = 0.5f;
		}
		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			Time.timeScale = 0.1f;
		}
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			Time.timeScale = 0.05f;
		}
		if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			Time.timeScale = 0.01f;
		}
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			Time.timeScale = 1E-08f;
		}
	}

	// Token: 0x0400224D RID: 8781
	public float StartScale = 1f;
}
