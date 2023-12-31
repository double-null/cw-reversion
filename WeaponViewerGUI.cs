using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x0200019C RID: 412
[AddComponentMenu("Scripts/GUI/WeaponViewerGUI")]
internal class WeaponViewerGUI : Form
{
	// Token: 0x06000BAA RID: 2986 RVA: 0x0009126C File Offset: 0x0008F46C
	[Obfuscation(Exclude = true)]
	private void ShowWeaponViewerGUI(object[] obj)
	{
		this.weaponType = (Weapons)((int)Crypt.ResolveVariable(obj, Weapons.none, 0));
		this.weaponMod = (bool)Crypt.ResolveVariable(obj, false, 1);
		this.weaponName = this.weaponType.ToString();
		Dictionary<int, WeaponMods> currentWeaponsMods = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods;
		if (currentWeaponsMods.ContainsKey((int)this.weaponType))
		{
			this.mods = currentWeaponsMods[(int)this.weaponType].Mods;
		}
		base.CancelInvoke();
		this.Clear();
		this.Show(0.5f, 0f);
		if (this.cameraObject == null)
		{
			this.cameraObject = SingletoneForm<PoolManager>.Instance["weapon_wv"].Spawn();
			this.cameraObject.transform.parent = base.transform;
			this.cameraObject.GetComponent<Camera>().targetTexture = MainGUI.Instance.weaponViewer;
		}
		Light[] componentsInChildren = this.gui.gameObject.GetComponentsInChildren<Light>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = true;
		}
		CameraListener.Enable(this.cameraObject);
		this.isUpdating = true;
		this._stateName = Loader.DownloadLodWV((int)this.weaponType, new StateDownloaderFinishedCallback(this.PrefabDownloaded));
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x000913E0 File Offset: 0x0008F5E0
	[Obfuscation(Exclude = true)]
	private void HideWeaponViewerGUI(object obj)
	{
		base.Invoke("DelayedHide", (float)obj);
		this.Hide((float)obj);
		this.isUpdating = false;
		this.gui.IsWeaponViewerClicked = false;
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00091420 File Offset: 0x0008F620
	[Obfuscation(Exclude = true)]
	private void DelayedHide()
	{
		this.Clear();
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00091428 File Offset: 0x0008F628
	[Obfuscation(Exclude = true)]
	private void PrefabDownloaded()
	{
		Debug.Log("WVGUI.PrefabDownloaded " + this.weaponType);
		PrefabFactory.GetHolderByName("preweapon").LoadAllAsync();
		this.inactivity.Start();
		if (this.weaponObject)
		{
			Utility.SetLayerRecursively(this.weaponObject, 0);
			PoolManager.Despawn(this.weaponObject);
			this.weaponObject = null;
		}
		this.weaponObject = PoolManager.Spawn("wv_weapon");
		this.cWeapon = this.weaponObject.GetComponent<ClientWeapon>();
		this.cWeapon.GetComponent<BaseWeapon>().LOD = true;
		this.cWeapon.state.isMod = this.weaponMod;
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(WWWUtil.lodsUrl(this.weaponName));
		GameObject gameObject = PrefabFactory.GetHolderByName(fileNameWithoutExtension).Generate();
		this.cWeapon.Copy(gameObject.GetComponent<BaseWeapon>());
		if (this.mods != null)
		{
			foreach (KeyValuePair<ModType, int> keyValuePair in this.mods)
			{
				MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
				if (modById != null && !modById.IsBasic && !modById.IsCamo && !modById.IsAmmo)
				{
					modById.Prefab = PrefabFactory.GenerateLodModWithoutCreating(modById);
				}
			}
		}
		this.cWeapon.Init(null, (int)this.weaponType);
		this.cWeapon.AfterInit(Main.UserInfo.weaponsStates[(int)this.cWeapon.type].repair_info);
		this.cWeapon.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		Utility.SetLayerRecursively(this.weaponObject, LayerMask.NameToLayer("weaponViewer"));
		this.weaponObject.transform.parent = base.transform;
		Bounds bounds = default(Bounds);
		for (int i = 0; i < this.cWeapon.RenderersCurrent.Count; i++)
		{
			bounds.Encapsulate(this.cWeapon.RenderersCurrent[i].bounds);
		}
		this.center = bounds.center;
		if (Main.UserInfo.weaponsStates[(int)this.weaponType].CurrentWeapon.IsPrimary)
		{
			this.min = bounds.size.magnitude * 1.25f;
			this.max = bounds.size.magnitude * 1.75f;
			this.camCurrentRad = bounds.size.magnitude * 1.5f;
			this.camTargetRad = bounds.size.magnitude * 1.5f;
		}
		else
		{
			this.camCurrentRad = bounds.size.magnitude * 3f;
			this.camTargetRad = bounds.size.magnitude * 3f;
			this.min = bounds.size.magnitude * 2.5f;
			this.max = bounds.size.magnitude * 4f;
		}
		this.camEuler.x = 270f;
		this.camEuler.y = 90f;
	}

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000917C0 File Offset: 0x0008F9C0
	public override Rect Rect
	{
		get
		{
			return new Rect((float)((Screen.width - this.gui.Width) / 2), (float)((Screen.height - this.gui.Height) / 2), (float)this.gui.Width, (float)this.gui.Height);
		}
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00091814 File Offset: 0x0008FA14
	public override void MainInitialize()
	{
		this.isUpdating = true;
		this.isRendering = true;
		base.MainInitialize();
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x0009182C File Offset: 0x0008FA2C
	public override void Clear()
	{
		base.Clear();
		if (this.cameraObject)
		{
			CameraListener.Disable(this.cameraObject);
			PoolManager.Despawn(this.cameraObject);
			this.cameraObject = null;
		}
		if (this.weaponObject)
		{
			Utility.SetLayerRecursively(this.weaponObject, 0);
			PoolManager.Despawn(this.weaponObject);
			this.weaponObject = null;
		}
		Light[] componentsInChildren = this.gui.gameObject.GetComponentsInChildren<Light>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000918C8 File Offset: 0x0008FAC8
	public override void Register()
	{
		EventFactory.Register("ShowWeaponViewerGUI", this);
		EventFactory.Register("HideWeaponViewerGUI", this);
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x000918E0 File Offset: 0x0008FAE0
	public override void OnUpdate()
	{
		if (!base.Visible)
		{
			return;
		}
		if (Input.GetMouseButton(0) && this.isInMouseRect)
		{
			this.inactivity.Start();
			float axis = Input.GetAxis("Mouse X");
			float num = Input.GetAxis("Mouse Y");
			if (Main.UserInfo.settings.binds.invertMouse)
			{
				num *= -1f;
			}
			this.camEuler.y = this.camEuler.y + 4f * axis * 270f * Time.deltaTime;
			this.camEuler.x = this.camEuler.x - 4f * num * 270f * Time.deltaTime;
			if (this.camEuler.x > 359.5f)
			{
				this.camEuler.x = 359.5f;
			}
			if (this.camEuler.x < 180.5f)
			{
				this.camEuler.x = 180.5f;
			}
		}
		if (this.isInMouseRect)
		{
			if (this.camTargetRad < this.min)
			{
				this.camTargetRad = this.min;
			}
			if (this.camTargetRad > this.max)
			{
				this.camTargetRad = this.max;
			}
			if (this.camCurrentRad > this.camTargetRad)
			{
				this.camCurrentRad = this.camTargetRad;
			}
			if (this.camCurrentRad < this.camTargetRad)
			{
				this.camCurrentRad += Time.deltaTime * 10f;
			}
			this.camTargetRad -= Input.GetAxis("Mouse ScrollWheel") * 100f * Time.deltaTime;
		}
		if (this.inactivity.Elapsed > 5f)
		{
			this.camEuler.y = this.camEuler.y + 30f * Time.deltaTime;
		}
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00091AC8 File Offset: 0x0008FCC8
	public override void OnLateUpdate()
	{
		if (!base.Visible)
		{
			return;
		}
		if (this.cameraObject)
		{
			this.cameraObject.transform.position = this.center + Quaternion.Euler(this.camEuler) * Vector3.up * this.camCurrentRad;
			this.cameraObject.transform.LookAt(this.center);
		}
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x00091B44 File Offset: 0x0008FD44
	public override void OnConnected()
	{
		this.Clear();
		base.OnConnected();
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x00091B54 File Offset: 0x0008FD54
	public override void InterfaceGUI()
	{
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		this.gui.PictureSized(new Vector2(0f, 0f), MainGUI.Instance.black, new Vector2((float)Screen.width, (float)Screen.height));
		if ((double)base.visibility > 0.75)
		{
			this.gui.BeginGroup(this.Rect, this.windowID != this.gui.FocusedWindow);
			this.gui.PictureSizedNoBlend(new Vector2((this.Rect.width - (float)this.gui.weaponViewer.width) / 2f, this.Rect.height / 8f), this.gui.weaponViewer, new Vector2((float)this.gui.weaponViewer.width, (float)this.gui.weaponViewer.height), false);
			if (Input.GetMouseButtonDown(0) && this.gui.TextButton(new Rect(0f, 0f, (float)this.gui.weaponViewer.width, (float)this.gui.weaponViewer.height), string.Empty, 24, "#ffffff", "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Hover)
			{
				this.isInMouseRect = true;
			}
			else if (Input.GetMouseButtonDown(0) && !this.gui.TextButton(new Rect(0f, 0f, (float)this.gui.weaponViewer.width, (float)this.gui.weaponViewer.height), string.Empty, 24, "#ffffff", "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Hover)
			{
				this.isInMouseRect = false;
			}
			if (Loader.Progress(this._stateName) < 1f)
			{
				int num = (int)(Loader.Progress(this._stateName) * 100f);
				this.gui.TextField(new Rect(0f, 0f, (float)this.gui.weaponViewer.width, (float)this.gui.weaponViewer.height), num + "%", 11, "#b1b0b1_Micra", TextAnchor.MiddleCenter, false, false);
				Vector2 vector = new Vector2((float)(this.gui.weaponViewer.width / 2), (float)(this.gui.weaponViewer.height / 2));
				float angle = 180f * Time.realtimeSinceStartup * 1.5f;
				this.gui.RotateGUI(angle, new Vector2(vector.x + (float)(this.krutilka_small.width / 2) - 10f, vector.y + (float)(this.krutilka_small.height / 2) + 10f));
				this.gui.Picture(new Vector2(vector.x - 10f, vector.y + 10f), this.krutilka_small);
				this.gui.RotateGUI(0f, Vector2.zero);
			}
			string str = Main.UserInfo.weaponsStates[(int)this.weaponType].CurrentWeapon.InterfaceName;
			string info = Main.UserInfo.weaponsStates[(int)this.weaponType].CurrentWeapon.Info;
			if (this.weaponMod)
			{
				str = Main.UserInfo.weaponsStates[(int)this.weaponType].CurrentWeapon.ModInterfaceName;
			}
			this.gui.Picture(new Vector2((float)(this.gui.weaponViewer.width - this.icon_3d.width), (float)(this.gui.weaponViewer.height - this.icon_3d.height)), this.icon_3d);
			int width = CWGUI.p.closeButton.normal.background.width;
			int height = CWGUI.p.closeButton.normal.background.height;
			if (GUI.Button(new Rect(this.Rect.width - (float)width - 10f, 5f, (float)width, (float)height), string.Empty, CWGUI.p.closeButton) && !PopupGUI.IsAnyPopupShow)
			{
				Loader.Cancel(this._stateName);
				this.HideWeaponViewerGUI(0.35f);
				EventFactory.Call("ShowInterface", null);
			}
			this.gui.TextLabel(new Rect((this.Rect.width - (float)this.gui.weaponViewer.width) / 2f, 0f, (float)this.gui.weaponViewer.width, 40f), Language.WeaponView + ": " + str, 18, "#FFFFFF", TextAnchor.UpperLeft, true);
			this.gui.TextLabel(new Rect((this.Rect.width - (float)this.gui.weaponViewer.width) / 2f, this.Rect.height / 8f + (float)this.gui.weaponViewer.height + 5f, (float)this.gui.weaponViewer.width, 400f), info, 18, "#C1C1C1", TextAnchor.UpperLeft, true);
			this.gui.EndGroup();
		}
	}

	// Token: 0x04000D83 RID: 3459
	public Texture2D krutilka_small;

	// Token: 0x04000D84 RID: 3460
	public Texture2D background;

	// Token: 0x04000D85 RID: 3461
	public Texture icon_3d;

	// Token: 0x04000D86 RID: 3462
	private float min;

	// Token: 0x04000D87 RID: 3463
	private float max;

	// Token: 0x04000D88 RID: 3464
	private float camCurrentRad;

	// Token: 0x04000D89 RID: 3465
	private float camTargetRad;

	// Token: 0x04000D8A RID: 3466
	private bool isInMouseRect = true;

	// Token: 0x04000D8B RID: 3467
	private GameObject weaponObject;

	// Token: 0x04000D8C RID: 3468
	private GameObject cameraObject;

	// Token: 0x04000D8D RID: 3469
	private string weaponName = string.Empty;

	// Token: 0x04000D8E RID: 3470
	private Weapons weaponType = Weapons.none;

	// Token: 0x04000D8F RID: 3471
	private bool weaponMod;

	// Token: 0x04000D90 RID: 3472
	private Vector3 center = Vector3.zero;

	// Token: 0x04000D91 RID: 3473
	private Vector3 camEuler = new Vector3(280f, -280f, 0f);

	// Token: 0x04000D92 RID: 3474
	private eTimer inactivity = new eTimer();

	// Token: 0x04000D93 RID: 3475
	private Dictionary<ModType, int> mods;

	// Token: 0x04000D94 RID: 3476
	private ClientWeapon cWeapon;

	// Token: 0x04000D95 RID: 3477
	private string _stateName;
}
