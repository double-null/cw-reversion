using System;
using UnityEngine;

// Token: 0x020001BF RID: 447
[AddComponentMenu("Scripts/Game/Components/CrossHair")]
internal class CrossHair : MonoBehaviour
{
	// Token: 0x06000F61 RID: 3937 RVA: 0x000B0B18 File Offset: 0x000AED18
	private void OnGUI()
	{
		if (this.gui == null)
		{
			this.gui = GameObject.Find("main").GetComponentInChildren<MainGUI>();
		}
		GUI.color = new Color(1f, 1f, 1f, 0.5f);
		Utility.DrawLine(new Vector2((float)(Screen.width / 2), 0f), new Vector2((float)(Screen.width / 2), (float)Screen.height), this.gui.black, 1);
		Utility.DrawLine(new Vector2(0f, (float)(Screen.height / 2)), new Vector2((float)Screen.width, (float)(Screen.height / 2)), this.gui.black, 1);
	}

	// Token: 0x04000FB0 RID: 4016
	private MainGUI gui;
}
