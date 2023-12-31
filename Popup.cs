using System;
using UnityEngine;

// Token: 0x0200014A RID: 330
[Serializable]
internal class Popup
{
	// Token: 0x0600082F RID: 2095 RVA: 0x000497FC File Offset: 0x000479FC
	public Popup()
	{
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00049878 File Offset: 0x00047A78
	public Popup(WindowsID ID, string Title, string Desc, Popup.SomeAction action, PopupState pState = PopupState.information, bool isError = false, bool Closable = true, params object[] args)
	{
		this.windowID = (int)ID;
		this.title = Title;
		this.desc = Desc;
		this.popupState = pState;
		this.closable = Closable;
		this.closeMethod = string.Empty;
		this.progressName = string.Empty;
		this.error = isError;
		this.action = action;
		this.args = args;
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x00049948 File Offset: 0x00047B48
	public Popup(WindowsID ID, string Title, string Desc, PopupState pState = PopupState.information, bool isError = false, bool Closable = true, string CloseMethod = "", string inc = "")
	{
		this.windowID = (int)ID;
		this.title = Title;
		this.desc = Desc;
		this.popupState = pState;
		this.closable = Closable;
		this.closeMethod = CloseMethod;
		this.progressName = inc;
		this.error = isError;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00049A00 File Offset: 0x00047C00
	public Popup(Popup i)
	{
		this.windowID = i.windowID;
		this.title = i.title;
		this.desc = i.desc;
		this.popupState = i.popupState;
		this.closable = i.closable;
		this.closeMethod = i.closeMethod;
		this.progressName = i.progressName;
		this.error = i.error;
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000833 RID: 2099 RVA: 0x00049ADC File Offset: 0x00047CDC
	public bool IsQuickGamePopup
	{
		get
		{
			return this.popupState == PopupState.quickGame;
		}
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00049AE8 File Offset: 0x00047CE8
	public void Show(float time = 0.5f)
	{
		this.popupA.Show(time, 0f);
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00049AFC File Offset: 0x00047CFC
	public void Once(float fade = 0.5f, float stay = 0.5f)
	{
		this.popupA.OnceLong(fade, stay);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00049B0C File Offset: 0x00047D0C
	public void Hide(Popup popup, float time = 0.5f, float delay = 0.5f)
	{
		this.popupA.Hide(time, delay);
		if (popup == null)
		{
			return;
		}
		this.desc = popup.desc;
		this.popupState = popup.popupState;
		this.closable = popup.closable;
		this.closable = false;
		this.closeMethod = popup.closeMethod;
		this.progressName = popup.progressName;
	}

	// Token: 0x04000938 RID: 2360
	public int windowID = -1;

	// Token: 0x04000939 RID: 2361
	public string title = string.Empty;

	// Token: 0x0400093A RID: 2362
	public string desc = string.Empty;

	// Token: 0x0400093B RID: 2363
	public bool closable;

	// Token: 0x0400093C RID: 2364
	public string closeMethod = string.Empty;

	// Token: 0x0400093D RID: 2365
	public PopupState popupState = PopupState.information;

	// Token: 0x0400093E RID: 2366
	public string progressName = string.Empty;

	// Token: 0x0400093F RID: 2367
	public bool error;

	// Token: 0x04000940 RID: 2368
	public bool alreadyClicked;

	// Token: 0x04000941 RID: 2369
	public Alpha popupA = new Alpha();

	// Token: 0x04000942 RID: 2370
	public Rect rect;

	// Token: 0x04000943 RID: 2371
	public Popup.SomeAction action = delegate()
	{
	};

	// Token: 0x04000944 RID: 2372
	public object[] args;

	// Token: 0x020003AE RID: 942
	// (Invoke) Token: 0x06001E20 RID: 7712
	public delegate void SomeAction();
}
