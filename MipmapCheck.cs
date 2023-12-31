using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B3 RID: 179
internal class MipmapCheck : MonoBehaviour
{
	// Token: 0x06000471 RID: 1137 RVA: 0x0001DF24 File Offset: 0x0001C124
	private void Awake()
	{
		if (MipmapCheck.I == null)
		{
			MipmapCheck.I = this;
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0001DF3C File Offset: 0x0001C13C
	public static void Check()
	{
		if (MipmapCheck.I && Environment.OSVersion.Platform != PlatformID.Unix)
		{
			MipmapCheck.I.StartCoroutine(MipmapCheck.I.StartCheck());
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001DF80 File Offset: 0x0001C180
	private bool IsEuqal(Color col1, Color col2)
	{
		Color color = col1 - col2;
		return Mathf.Abs(color.r) <= this.minColorAccuracy.r && Mathf.Abs(color.b) <= this.minColorAccuracy.r && Mathf.Abs(color.g) <= this.minColorAccuracy.r;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001DFEC File Offset: 0x0001C1EC
	private IEnumerator StartCheck()
	{
		yield return new WaitForSeconds(1f);
		if (this._realTexture == null)
		{
			this._realTexture = new Texture2D(16, 16, TextureFormat.ARGB32, false);
		}
		this._genTexture = MipmapCheck.GenerateSuperTex(6, Color.blue, Color.yellow);
		this.BoardmeshRenderer.materials[0].mainTexture = this._genTexture;
		yield return new WaitForEndOfFrame();
		RenderTexture.active = base.camera.targetTexture;
		base.camera.Render();
		this._realTexture.ReadPixels(new Rect(0f, 0f, (float)this._realTexture.width, (float)this._realTexture.height), 0, 0);
		this._realTexture.Apply();
		yield return new WaitForEndOfFrame();
		if (this.IsEuqal(this._realTexture.GetPixel(1, 1), Color.yellow))
		{
			if (CVars.CaptureMipMapScreen && Main.IsGameLoaded)
			{
				base.StartCoroutine(this.SendScreenShot());
			}
			this.SetViolation();
		}
		yield return 0;
		yield break;
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0001E008 File Offset: 0x0001C208
	private void SetViolation()
	{
		Main.UserInfo.Violation.Value |= 2;
		HtmlLayer.RequestCompressed("?action=awh&user_id=" + Main.UserInfo.userID, delegate(string text, string url)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.id0, Language.MipmapCheckFailCaption, Language.MipmapCheckFailDescription, PopupState.information, false, true, "ExitGame", string.Empty));
		}, null, string.Empty, string.Empty);
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0001E070 File Offset: 0x0001C270
	private IEnumerator SendScreenShot()
	{
		yield return new WaitForSeconds(10f);
		Texture2D capturedTexture = Utility.CaptureCustomScreenshot(Screen.width, Screen.height);
		yield return new WaitForEndOfFrame();
		HtmlLayer.SendFile("awh/?user_id=" + Main.UserInfo.userID + "&type=screen", capturedTexture.EncodeToPNG(), null, null);
		yield return new WaitForEndOfFrame();
		HtmlLayer.SendFile("awh/?user_id=" + Main.UserInfo.userID + "&type=prefab", this._realTexture.EncodeToPNG(), null, null);
		yield return new WaitForSeconds(10f);
		yield break;
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0001E08C File Offset: 0x0001C28C
	private static Texture2D GenerateSuperTex(int mips, Color32 ok, Color32 notOk)
	{
		int num = 1 << mips;
		Texture2D texture2D = new Texture2D(num, num, TextureFormat.RGB24, true);
		texture2D.filterMode = FilterMode.Point;
		for (int i = 0; i <= mips; i++)
		{
			int num2 = num >> i;
			Color32[] array = new Color32[num2 * num2];
			if (i < 4)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = ok;
				}
			}
			else
			{
				for (int k = 0; k < array.Length; k++)
				{
					array[k] = notOk;
				}
			}
			texture2D.SetPixels32(array, i);
		}
		texture2D.Apply(false, true);
		return texture2D;
	}

	// Token: 0x04000438 RID: 1080
	private Texture2D _genTexture;

	// Token: 0x04000439 RID: 1081
	private Texture2D _realTexture;

	// Token: 0x0400043A RID: 1082
	private Texture2D _drawTexture;

	// Token: 0x0400043B RID: 1083
	private float _timer;

	// Token: 0x0400043C RID: 1084
	private Color minColorAccuracy = new Color(0.05f, 0.05f, 0.05f, 1f);

	// Token: 0x0400043D RID: 1085
	public MeshRenderer BoardmeshRenderer;

	// Token: 0x0400043E RID: 1086
	private static MipmapCheck I;
}
