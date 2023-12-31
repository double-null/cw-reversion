using System;
using UnityEngine;

// Token: 0x02000146 RID: 326
[AddComponentMenu("Scripts/GUI/PageManager")]
public class PageManager : SingletoneForm<PageManager>
{
	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000822 RID: 2082 RVA: 0x00049490 File Offset: 0x00047690
	public override Rect Rect
	{
		get
		{
			return new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x000494B0 File Offset: 0x000476B0
	public override void MainInitialize()
	{
		this.isRendering = true;
		base.MainInitialize();
		this.Show(0.5f, 0f);
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x000494D0 File Offset: 0x000476D0
	public override void LateGUI()
	{
		this.gui.color = new Color(1f, 1f, 1f, 1f);
		this.gui.BeginGroup(this.Rect, true);
		for (int i = 0; i < this.pages.Length; i++)
		{
			this.pages[i].Paint(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y), true);
		}
		this.gui.EndGroup();
	}

	// Token: 0x04000926 RID: 2342
	public Page editPage;

	// Token: 0x04000927 RID: 2343
	public Page[] pages;

	// Token: 0x04000928 RID: 2344
	public GUIStyle[] styles;
}
