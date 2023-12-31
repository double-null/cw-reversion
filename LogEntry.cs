using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008D RID: 141
[Serializable]
public class LogEntry
{
	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000322 RID: 802 RVA: 0x00016B4C File Offset: 0x00014D4C
	// (set) Token: 0x06000323 RID: 803 RVA: 0x00016B54 File Offset: 0x00014D54
	public string LastTime
	{
		get
		{
			return this._lastTime;
		}
		set
		{
			this._lastTime = value;
			this.LogText = this._lastTime + " " + this.text;
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00016B7C File Offset: 0x00014D7C
	public void OnScreeenResolutionChanged()
	{
		this.HeadHeight = SingletoneForm<global::Console>.Instance.HeadersStyle.CalcHeight(new GUIContent(this.header), (float)Screen.width - 20f) + 8f;
		this.OpenedHeight = SingletoneForm<global::Console>.Instance.HeadersStyle.CalcHeight(new GUIContent(this.LogText), (float)Screen.width - 20f) + 8f;
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00016BF0 File Offset: 0x00014DF0
	public Dictionary<string, object> ToDict()
	{
		return new Dictionary<string, object>
		{
			{
				"lastTime",
				this.LastTime
			},
			{
				"text",
				this.text
			},
			{
				"count",
				this.count.ToString()
			}
		};
	}

	// Token: 0x04000358 RID: 856
	public string header = "Unknown header";

	// Token: 0x04000359 RID: 857
	public string text = string.Empty;

	// Token: 0x0400035A RID: 858
	private string _lastTime = string.Empty;

	// Token: 0x0400035B RID: 859
	public string LogText = string.Empty;

	// Token: 0x0400035C RID: 860
	public bool opened;

	// Token: 0x0400035D RID: 861
	public bool IsErrorEntry;

	// Token: 0x0400035E RID: 862
	public int count;

	// Token: 0x0400035F RID: 863
	public Color color = Color.gray;

	// Token: 0x04000360 RID: 864
	public float OpenedHeight;

	// Token: 0x04000361 RID: 865
	public float HeadHeight;
}
