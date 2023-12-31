using System;
using UnityEngine;

// Token: 0x02000148 RID: 328
[Serializable]
public class PageText : PageElement
{
	// Token: 0x06000826 RID: 2086 RVA: 0x000495B4 File Offset: 0x000477B4
	public override void Draw(Vector2 mousePosition, bool enabled)
	{
		if (this.style == null)
		{
			GUIStyle[] styles = SingletoneForm<PageManager>.Instance.styles;
			this.style = styles[0];
			for (int i = 0; i < styles.Length; i++)
			{
				if (this.styleName == styles[i].name)
				{
					this.style = styles[i];
					break;
				}
			}
		}
		base.Draw(mousePosition, enabled);
		if (this.text != string.Empty)
		{
			if (this.textEdit == TextEditType.editable)
			{
				this.text = GUI.TextArea(this.Rect, this.text, this.maxLenght, this.style);
			}
			else if (this.textEdit == TextEditType.selectable)
			{
				GUI.TextArea(this.Rect, this.text, this.maxLenght, this.style);
			}
			else
			{
				this.content.text = this.text;
				this.content.tooltip = this.tooltip;
				GUI.Label(this.rect, this.content, this.style);
			}
		}
	}

	// Token: 0x0400092D RID: 2349
	public string text = string.Empty;

	// Token: 0x0400092E RID: 2350
	public int maxLenght = 100;

	// Token: 0x0400092F RID: 2351
	public string tooltip = string.Empty;

	// Token: 0x04000930 RID: 2352
	public TextEditType textEdit;

	// Token: 0x04000931 RID: 2353
	public string styleName = "unknown";

	// Token: 0x04000932 RID: 2354
	private GUIStyle style;

	// Token: 0x04000933 RID: 2355
	private GUIContent content = new GUIContent();
}
