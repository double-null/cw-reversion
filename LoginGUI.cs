using System;
using UnityEngine;

// Token: 0x02000157 RID: 343
[AddComponentMenu("Scripts/GUI/LoginGUI")]
internal class LoginGUI : MonoBehaviour
{
	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000880 RID: 2176 RVA: 0x0004D648 File Offset: 0x0004B848
	public int Width
	{
		get
		{
			return 800;
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06000881 RID: 2177 RVA: 0x0004D650 File Offset: 0x0004B850
	public int Height
	{
		get
		{
			return 600;
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0004D658 File Offset: 0x0004B858
	private void Awake()
	{
		Debug.Log("Awake Login splash!");
		this._pCurrentGUI = new LoginGUI.CurrentGUI(this.LoginScreen);
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0004D678 File Offset: 0x0004B878
	private void OnGUI()
	{
		if (this._pCurrentGUI != null)
		{
			this._pCurrentGUI();
		}
		if (this._pModalGUI != null)
		{
			this._pModalGUI();
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
	private void LoginScreen()
	{
		GUILayout.BeginVertical("box", new GUILayoutOption[0]);
		GUILayout.Button("push me", new GUILayoutOption[0]);
		GUILayout.Button("or me!", new GUILayoutOption[0]);
		GUILayout.Button("or me!", new GUILayoutOption[0]);
		GUILayout.Button("or me!", new GUILayoutOption[0]);
		GUILayout.EndVertical();
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0004D720 File Offset: 0x0004B920
	private void RegisterScreen()
	{
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0004D724 File Offset: 0x0004B924
	private void CodeAttempt()
	{
	}

	// Token: 0x0400099D RID: 2461
	public static bool Login = true;

	// Token: 0x0400099E RID: 2462
	private string _username;

	// Token: 0x0400099F RID: 2463
	private string _password;

	// Token: 0x040009A0 RID: 2464
	private LoginGUI.CurrentGUI _pCurrentGUI;

	// Token: 0x040009A1 RID: 2465
	private LoginGUI.ModalGUI _pModalGUI;

	// Token: 0x020003AF RID: 943
	// (Invoke) Token: 0x06001E24 RID: 7716
	private delegate void CurrentGUI();

	// Token: 0x020003B0 RID: 944
	// (Invoke) Token: 0x06001E28 RID: 7720
	private delegate void ModalGUI();
}
