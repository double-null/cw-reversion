using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class ProFlareAtlas : MonoBehaviour
{
	// Token: 0x0600004F RID: 79 RVA: 0x000059DC File Offset: 0x00003BDC
	public void UpdateElementNameList()
	{
		this.elementNameList = new string[this.elementsList.Count];
		for (int i = 0; i < this.elementNameList.Length; i++)
		{
			this.elementNameList[i] = this.elementsList[i].name;
		}
	}

	// Token: 0x0400008A RID: 138
	public Texture2D texture;

	// Token: 0x0400008B RID: 139
	public int elementNumber;

	// Token: 0x0400008C RID: 140
	public bool editElements;

	// Token: 0x0400008D RID: 141
	[SerializeField]
	public List<ProFlareAtlas.Element> elementsList = new List<ProFlareAtlas.Element>();

	// Token: 0x0400008E RID: 142
	public string[] elementNameList;

	// Token: 0x02000011 RID: 17
	[Serializable]
	public class Element
	{
		// Token: 0x0400008F RID: 143
		public string name = "Flare Element";

		// Token: 0x04000090 RID: 144
		public Rect UV = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000091 RID: 145
		public bool Imported;
	}
}
