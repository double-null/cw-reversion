using System;
using UnityEngine;

// Token: 0x020001D2 RID: 466
[AddComponentMenu("Scripts/Game/Components/ShowCross")]
internal class ShowCross : MonoBehaviour
{
	// Token: 0x06000FA0 RID: 4000 RVA: 0x000B25DC File Offset: 0x000B07DC
	private void OnGUI()
	{
		if (this.gui == null)
		{
			this.gui = GameObject.Find("main").GetComponentInChildren<MainGUI>();
		}
		GUI.color = new Color(1f, 1f, 1f, 0.5f);
		Utility.DrawLine(new Vector2((float)(Screen.width / 2), 0f), new Vector2((float)(Screen.width / 2), (float)Screen.height), this.gui.black, 2);
		Utility.DrawLine(new Vector2(0f, (float)(Screen.height / 2)), new Vector2((float)Screen.width, (float)(Screen.height / 2)), this.gui.black, 2);
	}

	// Token: 0x04001012 RID: 4114
	private MainGUI gui;
}
