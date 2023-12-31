using System;
using UnityEngine;

// Token: 0x02000143 RID: 323
[Serializable]
public class Page : PageTexture
{
	// Token: 0x17000106 RID: 262
	// (get) Token: 0x0600080C RID: 2060 RVA: 0x00049038 File Offset: 0x00047238
	public int Length
	{
		get
		{
			return this.types.Length;
		}
	}

	// Token: 0x17000107 RID: 263
	public PageElement this[int index]
	{
		get
		{
			if (this.types[index] == PageElementType.Text)
			{
				return this.texts[this.Index(index)];
			}
			if (this.types[index] == PageElementType.Texture)
			{
				return this.textures[this.Index(index)];
			}
			return null;
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x0004908C File Offset: 0x0004728C
	public int Index(int index)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.types.Length; i++)
		{
			if (this.types[i] == PageElementType.Text && index == i)
			{
				return num;
			}
			if (this.types[i] == PageElementType.Texture && index == i)
			{
				return num2;
			}
			if (this.types[i] == PageElementType.Text)
			{
				num++;
			}
			else if (this.types[i] == PageElementType.Texture)
			{
				num2++;
			}
		}
		return -1;
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x0004910C File Offset: 0x0004730C
	public void Swap(int index1, int index2)
	{
		if (this.types[index1] == this.types[index2])
		{
			if (this.types[index1] == PageElementType.Text)
			{
				Debug.LogWarning(this.Index(index1));
				Debug.LogWarning(this.Index(index2));
				PageText pageText = this.texts[this.Index(index1)];
				this.texts[this.Index(index1)] = this.texts[this.Index(index2)];
				this.texts[this.Index(index2)] = pageText;
			}
			else if (this.types[index1] == PageElementType.Texture)
			{
				PageTexture pageTexture = this.textures[this.Index(index1)];
				this.textures[this.Index(index1)] = this.textures[this.Index(index2)];
				this.textures[this.Index(index2)] = pageTexture;
			}
		}
		else
		{
			PageElementType pageElementType = this.types[index1];
			this.types[index1] = this.types[index2];
			this.types[index2] = pageElementType;
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0004920C File Offset: 0x0004740C
	public void RemoveAt(int index)
	{
		if (this.types[index] == PageElementType.Text)
		{
			ArrayUtility.RemoveAt<PageText>(ref this.texts, this.Index(index));
			ArrayUtility.RemoveAt<PageElementType>(ref this.types, index);
		}
		else if (this.types[index] == PageElementType.Texture)
		{
			ArrayUtility.RemoveAt<PageTexture>(ref this.textures, this.Index(index));
			ArrayUtility.RemoveAt<PageElementType>(ref this.types, index);
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00049278 File Offset: 0x00047478
	public void Add(PageElement element)
	{
		if (element is PageText)
		{
			ArrayUtility.Add<PageText>(ref this.texts, element as PageText);
			ArrayUtility.Add<PageElementType>(ref this.types, PageElementType.Text);
		}
		else if (element is PageTexture)
		{
			ArrayUtility.Add<PageTexture>(ref this.textures, element as PageTexture);
			ArrayUtility.Add<PageElementType>(ref this.types, PageElementType.Texture);
		}
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x000492DC File Offset: 0x000474DC
	public void Clear()
	{
		this.types = new PageElementType[0];
		this.texts = new PageText[0];
		this.textures = new PageTexture[0];
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00049310 File Offset: 0x00047510
	public override void Draw(Vector2 mousePosition, bool enabled)
	{
		base.Draw(mousePosition, enabled);
		GUI.BeginGroup(this.Rect);
		for (int i = 0; i < this.Length; i++)
		{
			this[i].Paint(mousePosition - new Vector2(this.Rect.x, this.Rect.y), enabled);
		}
		GUI.EndGroup();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00049380 File Offset: 0x00047580
	public override void Paint(Vector2 mousePosition, bool enabled)
	{
		base.Paint(mousePosition, enabled);
	}

	// Token: 0x0400091E RID: 2334
	public string name = "unknown";

	// Token: 0x0400091F RID: 2335
	public PageElementType[] types = new PageElementType[0];

	// Token: 0x04000920 RID: 2336
	public PageText[] texts = new PageText[0];

	// Token: 0x04000921 RID: 2337
	public PageTexture[] textures = new PageTexture[0];
}
