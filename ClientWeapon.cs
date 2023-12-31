using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020001B1 RID: 433
[AddComponentMenu("Scripts/Game/ClientWeapon")]
public class ClientWeapon : BaseWeapon
{
	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000EBA RID: 3770 RVA: 0x000AA8D8 File Offset: 0x000A8AD8
	// (remove) Token: 0x06000EBB RID: 3771 RVA: 0x000AA8F4 File Offset: 0x000A8AF4
	internal event Action<BaseWeapon, bool> OnPrefabDownloaded = delegate(BaseWeapon w, bool b)
	{
	};

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000AA910 File Offset: 0x000A8B10
	// (set) Token: 0x06000EBD RID: 3773 RVA: 0x000AA918 File Offset: 0x000A8B18
	internal Camera OpticCamera { get; private set; }

	// Token: 0x06000EBE RID: 3774 RVA: 0x000AA924 File Offset: 0x000A8B24
	private void DespawnRoot()
	{
		if (this._root)
		{
			foreach (Renderer renderer in this.RenderersCurrent)
			{
				if (renderer)
				{
					renderer.enabled = false;
				}
			}
			foreach (Renderer renderer2 in this.RenderersAll)
			{
				if (renderer2)
				{
					renderer2.enabled = true;
				}
			}
			this._root.animation.playAutomatically = false;
			this._root.animation.cullingType = AnimationCullingType.BasedOnRenderers;
			this._root.animation.Stop();
			this._root.animation.clip = null;
			PoolManager.Despawn(this._rootType + "_prefab" + ((!this.LOD) ? string.Empty : "_lod"), this._root.gameObject);
			this._root = null;
			this.BulletHolder = null;
			this.ShellHolder = null;
			this._rootType = Weapons.none;
		}
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x000AAAA4 File Offset: 0x000A8CA4
	public override void OnPoolDespawn()
	{
		this.IsWeaponInited = false;
		this.Visible = false;
		base.audio.Stop();
		base.audio.clip = null;
		base.audio.mute = false;
		base.audio.volume = Audio.soundVolume;
		this._rand = new CircleRandom();
		this._sniperLens = null;
		if (this.OpticCamera)
		{
			this.OpticCamera.enabled = false;
			this.OpticCamera = null;
		}
		if (this._rootName != string.Empty)
		{
			Loader.Cancel(this._rootName);
			this._rootName = string.Empty;
		}
		if (Utility.IsModableWeapon((int)this.type))
		{
			this.DespawnMods();
		}
		this.DespawnRoot();
		this.RenderersAll.Clear();
		this.RenderersCurrent.Clear();
		this._shadowsDisabled = false;
		base.OnPoolDespawn();
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x000AAB94 File Offset: 0x000A8D94
	private void DespawnMods()
	{
		int type = (int)this.type;
		if (Main.IsGameLoaded)
		{
			Dictionary<ModType, int> dictionary = Utility.StringToMods(this.state.Mods);
			if (dictionary != null)
			{
				foreach (KeyValuePair<ModType, int> keyValuePair in dictionary)
				{
					MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
					if (modById != null && !modById.IsBasic)
					{
						string name = modById.EngShortName + "_prefab" + ((!this.LOD) ? string.Empty : "_lod");
						if (this.OpticMod != null && modById.Type == ModType.optic)
						{
							PoolManager.Despawn(name, this.OpticMod);
						}
						if (this.SilencerMod != null && modById.Type == ModType.silencer)
						{
							PoolManager.Despawn(name, this.SilencerMod);
						}
						if (this.TacticalMod != null && modById.Type == ModType.tactical)
						{
							PoolManager.Despawn(name, this.TacticalMod);
						}
					}
				}
			}
		}
		else
		{
			if (this.OpticMod != null)
			{
				string name2 = this.OpticMod.name.Replace("optic_", string.Empty).Replace("-lod(Clone)", string.Empty) + "_prefab" + ((!this.LOD) ? string.Empty : "_lod");
				PoolManager.Despawn(name2, this.OpticMod);
			}
			if (this.SilencerMod != null)
			{
				string name3 = this.SilencerMod.name.Replace("sil_", string.Empty).Replace("-lod(Clone)", string.Empty) + "_prefab" + ((!this.LOD) ? string.Empty : "_lod");
				PoolManager.Despawn(name3, this.SilencerMod);
			}
			if (this.TacticalMod != null)
			{
				string name4 = this.TacticalMod.name.Replace("tac_", string.Empty).Replace("-lod(Clone)", string.Empty) + "_prefab" + ((!this.LOD) ? string.Empty : "_lod");
				PoolManager.Despawn(name4, this.TacticalMod);
			}
		}
		this.OpticMod = null;
		this.SilencerMod = null;
		this.TacticalMod = null;
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x000AAE50 File Offset: 0x000A9050
	internal void Show()
	{
		this.Visible = true;
		if (!this.LOD)
		{
		}
		foreach (Renderer renderer in this.RenderersCurrent)
		{
			if (renderer)
			{
				renderer.gameObject.layer = ((!this.LOD) ? LayerMask.NameToLayer("hands_render") : 0);
				renderer.enabled = true;
				renderer.castShadows = false;
			}
		}
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x000AAF08 File Offset: 0x000A9108
	internal void Hide()
	{
		this.Visible = false;
		if (base.audio && base.audio.isPlaying)
		{
			base.audio.Stop();
		}
		if (!this.LOD)
		{
		}
		foreach (Renderer renderer in this.RenderersCurrent)
		{
			if (renderer)
			{
				renderer.enabled = false;
			}
		}
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x000AAFB8 File Offset: 0x000A91B8
	private void EnableRenderer(Renderer r)
	{
		r.enabled = true;
		if (!this.RenderersCurrent.Contains(r))
		{
			this.RenderersCurrent.Add(r);
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x000AAFEC File Offset: 0x000A91EC
	private void DisableRenderer(Renderer r)
	{
		r.enabled = false;
		if (this.RenderersCurrent.Contains(r))
		{
			this.RenderersCurrent.Remove(r);
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x000AB014 File Offset: 0x000A9214
	private void EnableRenderersRecursively(GameObject current, string name)
	{
		if ((name == string.Empty || current.name == name) && current.renderer)
		{
			this.EnableRenderer(current.renderer);
		}
		Renderer[] componentsInChildren = current.GetComponentsInChildren<Renderer>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (name == string.Empty || current.name == componentsInChildren[i].name)
			{
				this.EnableRenderer(componentsInChildren[i]);
			}
		}
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x000AB0AC File Offset: 0x000A92AC
	private void DisableRenderersRecursively(GameObject current, string name)
	{
		if ((name == string.Empty || current.name.Contains(name)) && current.renderer)
		{
			this.DisableRenderer(current.renderer);
		}
		Renderer[] componentsInChildren = current.GetComponentsInChildren<Renderer>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (name == string.Empty || componentsInChildren[i].gameObject.name.Contains(name))
			{
				this.DisableRenderer(componentsInChildren[i]);
			}
		}
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x000AB144 File Offset: 0x000A9344
	internal void DisableShadows()
	{
		if (this._shadowsDisabled)
		{
			return;
		}
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			skinnedMeshRenderer.castShadows = false;
			skinnedMeshRenderer.receiveShadows = false;
		}
		foreach (MeshRenderer meshRenderer in base.gameObject.GetComponentsInChildren<MeshRenderer>())
		{
			meshRenderer.castShadows = false;
			meshRenderer.receiveShadows = false;
		}
		this._shadowsDisabled = true;
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x000AB1D0 File Offset: 0x000A93D0
	internal void EnableShadows()
	{
		if (!this._shadowsDisabled)
		{
			return;
		}
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			skinnedMeshRenderer.castShadows = true;
			skinnedMeshRenderer.receiveShadows = true;
		}
		foreach (MeshRenderer meshRenderer in base.gameObject.GetComponentsInChildren<MeshRenderer>())
		{
			meshRenderer.castShadows = true;
			meshRenderer.receiveShadows = true;
		}
		this._shadowsDisabled = false;
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x000AB25C File Offset: 0x000A945C
	private bool IsLodLoaded()
	{
		bool result = false;
		string weaponPrefabName = Utility.GetWeaponPrefabName((int)this.type, WeaponPrefabType.Lod);
		if (PrefabFactory.IsHolderExist(weaponPrefabName) && this.LOD)
		{
			result = true;
			if (Utility.IsModableWeapon((int)this.type))
			{
				Dictionary<ModType, int> dictionary;
				if (!Main.IsGameLoaded)
				{
					dictionary = MasteringSuitsInfo.Instance.Suits[this.StartSuitIndex].CurrentWeaponsMods[(int)this.type].Mods;
				}
				else
				{
					dictionary = Utility.StringToMods(this.state.Mods);
				}
				foreach (KeyValuePair<ModType, int> keyValuePair in dictionary)
				{
					MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
					if (!modById.IsBasic && !modById.IsCamo && !modById.IsAmmo)
					{
						string modPrefabName = Utility.GetModPrefabName(modById, WeaponPrefabType.Lod);
						if (!PrefabFactory.IsHolderExist(modPrefabName))
						{
							Debug.Log("IsLodLoaded: NOT FOUND " + modPrefabName);
							result = false;
							break;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x000AB3A8 File Offset: 0x000A95A8
	[Obfuscation(Exclude = true)]
	private void PrefabDownloaded()
	{
		base.audio.Stop();
		base.audio.mute = false;
		base.audio.volume = Audio.soundVolume;
		this._rand = new CircleRandom();
		this._sniperLens = null;
		if (this.OpticCamera)
		{
			this.OpticCamera.enabled = false;
			this.OpticCamera = null;
		}
		if (Utility.IsModableWeapon((int)this.type))
		{
			this.DespawnMods();
		}
		this.DespawnRoot();
		this.RenderersAll.Clear();
		this.RenderersCurrent.Clear();
		this._shadowsDisabled = false;
		this._rootType = this.type;
		base.Copy(PrefabFactory.GenerateLodWeaponWithoutCreating((int)this.type).GetComponent<BaseWeapon>());
		Dictionary<ModType, int> dictionary = Utility.StringToMods(this.state.Mods);
		if (dictionary != null)
		{
			foreach (KeyValuePair<ModType, int> keyValuePair in dictionary)
			{
				if (keyValuePair.Value != 0)
				{
					MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
					if (!modById.IsBasic && !modById.IsCamo && !modById.IsAmmo)
					{
						modById.Prefab = PrefabFactory.GenerateLodModWithoutCreating(modById);
					}
				}
			}
		}
		this.Init(this.player, (int)this.type);
		this.Hide();
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x000AB540 File Offset: 0x000A9740
	internal override void Init(BaseNetPlayer player, int index)
	{
		base.Init(player, index);
		this.OpticMod = null;
		this.SilencerMod = null;
		this.TacticalMod = null;
		int num = 127;
		string weaponPrefabName = Utility.GetWeaponPrefabName((int)this.type, WeaponPrefabType.Lod);
		if (player && this.LOD && !PrefabFactory.IsHolderExist(weaponPrefabName))
		{
			if (this.weaponUseType == WeaponUseType.Secondary)
			{
				num = 0;
			}
			if (this.weaponUseType == WeaponUseType.Primary)
			{
				Debug.Log("kac added " + player.UserID);
				num = 5;
			}
		}
		else
		{
			num = index;
		}
		this._rootType = (Weapons)num;
		this._root = PoolManager.Spawn((Weapons)num + "_prefab" + ((!this.LOD) ? string.Empty : "_lod"), this.weaponPrefab, 4);
		Utility.ChangeParent(this._root.transform, base.transform);
		this._root.animation.playAutomatically = false;
		this._root.animation.cullingType = AnimationCullingType.BasedOnRenderers;
		this._root.animation.clip = null;
		this._root.animation.Stop();
		base.transform.localPosition = Vector3.zero;
		base.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		if (Utility.IsModableWeapon(num))
		{
			this.BulletHolder = Utility.FindHierarchy(this._root.transform, "muzzleflash");
			if (this.shellName != string.Empty)
			{
				this.ShellHolder = Utility.FindHierarchy(this._root.transform, "Weapons_shellHolder").transform;
			}
		}
		else
		{
			if (base.SS)
			{
				this.BulletHolder = Utility.FindHierarchy(this._root.transform, "Weapons_holeHolder_soundSuppressor").transform;
			}
			else
			{
				this.BulletHolder = Utility.FindHierarchy(this._root.transform, "Weapons_holeHolder").transform;
			}
			if (this.shellName != string.Empty)
			{
				this.ShellHolder = Utility.FindHierarchy(this._root.transform, "Weapons_shellHolder").transform;
			}
		}
		this.StartSuitIndex = Main.UserInfo.suitNameIndex.Value;
		bool flag = this.IsLodLoaded();
		this.InstallMods(num, flag);
		SkinnedMeshRenderer[] componentsInChildren = this._root.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
		MeshRenderer[] componentsInChildren2 = this._root.transform.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			for (int j = 0; j < componentsInChildren[i].materials.Length; j++)
			{
				if (componentsInChildren[i].materials[j].shader.name.Contains("p0/Transparent/Reflective/Specular"))
				{
					this._sniperLens = componentsInChildren[i].materials[j];
				}
			}
			this.RenderersAll.Add(componentsInChildren[i]);
		}
		for (int k = 0; k < componentsInChildren2.Length; k++)
		{
			for (int l = 0; l < componentsInChildren2[k].materials.Length; l++)
			{
				if (componentsInChildren2[k].materials[l].shader.name.Contains("p0/Transparent/Reflective/Specular"))
				{
					this._sniperLens = componentsInChildren2[k].materials[l];
				}
			}
			this.RenderersAll.Add(componentsInChildren2[k]);
		}
		if (this._sniperLens)
		{
			this.OpticCamera = this._root.GetComponentInChildren<Camera>();
			if (this.OpticCamera)
			{
				CameraListener.Disable(this.OpticCamera.gameObject);
			}
		}
		this.RenderersCurrent.AddRange(this.RenderersAll);
		Transform transform = Utility.FindHierarchy(this._root.transform, "ironSight");
		Transform transform2 = Utility.FindHierarchy(this._root.transform, "optic_root");
		Transform transform3 = Utility.FindHierarchy(this._root.transform, "targetPointer");
		Transform transform4 = Utility.FindHierarchy(this._root.transform, "soundSuppressor");
		Transform transform5 = Utility.FindHierarchy(this._root.transform, "grip");
		Transform transform6 = Utility.FindHierarchy(this._root.transform, "kolimator");
		Transform[] array = null;
		if (transform6)
		{
			array = transform6.GetComponentsInChildren<Transform>();
		}
		if (base.Optic || base.Kolimator)
		{
			if (transform && transform.renderer)
			{
				this.DisableRenderer(transform.renderer);
			}
		}
		else if (transform && transform.renderer)
		{
			this.EnableRenderer(transform.renderer);
		}
		if (base.Optic)
		{
			if (transform2)
			{
				if (transform2.renderer)
				{
					this.EnableRenderer(transform2.renderer);
				}
				if (transform2.FindChild("optic_camera"))
				{
					this.EnableRenderersRecursively(transform2.gameObject, string.Empty);
					transform2.gameObject.SetActiveRecursively(true);
				}
			}
		}
		else if (transform2)
		{
			if (transform2.renderer)
			{
				this.DisableRenderer(transform2.renderer);
			}
			if (transform2.FindChild("optic_camera"))
			{
				this.DisableRenderersRecursively(transform2.gameObject, string.Empty);
				transform2.gameObject.SetActiveRecursively(false);
			}
		}
		if (base.LTP)
		{
			if (transform3 && transform3.renderer)
			{
				this.EnableRenderer(transform3.renderer);
			}
		}
		else if (transform3 && transform3.renderer)
		{
			this.DisableRenderer(transform3.renderer);
		}
		if (base.Kolimator)
		{
			if (transform6)
			{
				if (transform6.renderer)
				{
					this.EnableRenderer(transform6.renderer);
				}
				for (int m = 0; m < array.Length; m++)
				{
					if (array[m].renderer)
					{
						this.EnableRenderer(array[m].renderer);
					}
				}
			}
		}
		else if (transform6)
		{
			if (transform6.renderer)
			{
				this.DisableRenderer(transform6.renderer);
			}
			for (int n = 0; n < array.Length; n++)
			{
				if (array[n].renderer)
				{
					this.DisableRenderer(array[n].renderer);
				}
			}
		}
		if (base.SS)
		{
			if (base.audio)
			{
				base.audio.volume = Audio.soundVolume * 0.7f;
			}
			if (transform4 && transform4.renderer)
			{
				this.EnableRenderer(transform4.renderer);
			}
		}
		else
		{
			if (base.audio)
			{
				base.audio.volume = Audio.soundVolume;
			}
			if (transform4 && transform4.renderer)
			{
				this.DisableRenderer(transform4.renderer);
			}
		}
		if (base.Grip)
		{
			if (transform5 && transform5.renderer)
			{
				this.EnableRenderer(transform5.renderer);
			}
		}
		else if (transform5 && transform5.renderer)
		{
			this.DisableRenderer(transform5.renderer);
		}
		if (player)
		{
		}
		foreach (Renderer renderer in this.RenderersAll)
		{
			if (renderer)
			{
				renderer.enabled = false;
			}
		}
		foreach (Renderer renderer2 in this.RenderersCurrent)
		{
			if (renderer2)
			{
				renderer2.enabled = true;
			}
		}
		if (this.state.isMod && !base.IsAbakan)
		{
			this.fireSounds = this.modFireSounds;
			this.secondSoundScheme = this.modSecondSoundScheme;
			this.singleTime = this.modSingleTime;
			this.loopFire = this.modLoopFire;
			this.tailFire = this.modTailFire;
		}
		this.reloadBools = new bool[this.reloadSounds.Length];
		this._rand.InitNew(this.reloadSounds.Length);
		if (base.audio)
		{
			base.audio.Stop();
			base.audio.mute = false;
			base.audio.minDistance = 15f;
			base.audio.maxDistance = base.HearRadius;
			if (Main.UserInfo.skillUnlocked(Skills.hear1))
			{
				base.audio.minDistance *= 1.3f;
				base.audio.maxDistance *= 1.3f;
			}
		}
		if (!this.LOD)
		{
			if (this.DoubleMagazine)
			{
				this._root.animation.Play((!this.FirstReload) ? "fire_start2" : "fire_start");
			}
			else
			{
				this._root.animation.Play("fire_start");
			}
		}
		if (this.secondSoundScheme)
		{
			base.enabled = true;
		}
		else
		{
			base.enabled = false;
		}
		if (player && this.LOD && !flag)
		{
			this._rootName = Loader.DownloadLod(this, delegate
			{
				this.PrefabDownloaded();
				this.OnPrefabDownloaded(this, true);
			});
		}
		if (player && this.LOD && flag)
		{
			this.IsWeaponInited = true;
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x000AC000 File Offset: 0x000AA200
	private void InstallMods(int prefabIndex, bool isLodLoaded)
	{
		if (!Utility.IsModableWeapon(prefabIndex) || (!isLodLoaded && this.LOD))
		{
			return;
		}
		Dictionary<ModType, int> dictionary;
		if (Main.IsGameLoaded)
		{
			dictionary = Utility.StringToMods(this.state.Mods);
		}
		else
		{
			dictionary = MasteringSuitsInfo.Instance.Suits[this.StartSuitIndex].CurrentWeaponsMods[prefabIndex].Mods;
		}
		if (dictionary == null)
		{
			return;
		}
		foreach (object obj in this._root.transform.FindChild("Weapons_Root"))
		{
			Transform transform = (Transform)obj;
			if (transform.name.Contains("mount"))
			{
				transform.gameObject.SetActive(false);
			}
		}
		foreach (KeyValuePair<ModType, int> keyValuePair in dictionary)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
			if (modById.IsCamo)
			{
				this.InstallCamo(modById);
			}
			else if (modById.IsBasic)
			{
				if (!string.IsNullOrEmpty(modById.WeaponSpecificInfo[(int)this.type].Device))
				{
					Transform transform2 = Utility.FindHierarchy(this._root.transform, modById.WeaponSpecificInfo[(int)this.type].Device);
					transform2.gameObject.SetActive(true);
				}
			}
			else
			{
				string name = modById.EngShortName + "_prefab" + ((!this.LOD) ? string.Empty : "_lod");
				if (modById.Prefab == null)
				{
					if (!modById.IsAmmo)
					{
						Debug.LogError(string.Concat(new object[]
						{
							this.type,
							"\t",
							modById.EngShortName,
							"\t",
							keyValuePair.Value
						}));
					}
				}
				else
				{
					GameObject gameObject = PoolManager.Spawn(name, modById.Prefab, 4);
					Transform transform2;
					if (string.IsNullOrEmpty(modById.WeaponSpecificInfo[(int)this.type].Device))
					{
						transform2 = this._root.transform.FindChild("Weapons_Root").FindChild(keyValuePair.Key.ToString());
					}
					else
					{
						transform2 = Utility.FindHierarchy(this._root.transform, modById.WeaponSpecificInfo[(int)this.type].Device);
						transform2.gameObject.SetActive(true);
						transform2 = Utility.FindHierarchy(transform2, keyValuePair.Key.ToString()).transform;
					}
					if (keyValuePair.Key == ModType.optic)
					{
						this.OpticMod = gameObject;
					}
					else if (keyValuePair.Key == ModType.silencer)
					{
						this.SilencerMod = gameObject;
						ModPrefabConfiguration component = modById.Prefab.GetComponent<ModPrefabConfiguration>();
						this.muzzleFlashName = component.muzzleFlashName;
						if (modById.IsSilencer)
						{
							this.silencedMuzzleFlashName = component.muzzleFlashName;
							this.state.isMod = true;
						}
					}
					else
					{
						this.TacticalMod = gameObject;
					}
					foreach (object obj2 in transform2)
					{
						Transform transform3 = (Transform)obj2;
						UnityEngine.Object.Destroy(transform3.gameObject);
					}
					Utility.ChangeParent(gameObject.transform, transform2);
					if (keyValuePair.Key == ModType.optic && Main.IsGameLoaded && !this.LOD && ModsStorage.Instance().GetModById(keyValuePair.Value).SightType == SightType.Optic && this.OpticCamera == null)
					{
						this.OpticCamera = transform2.GetChild(0).GetChild(0).FindChild("aim_camera").GetChild(0).GetComponent<Camera>();
					}
					if (keyValuePair.Key == ModType.silencer)
					{
						this.BulletHolder = Utility.FindHierarchy(gameObject.transform, "muzzleflash");
					}
					if (modById.WeaponSpecificInfo[(int)this.type].Scale != Vector3.zero)
					{
						gameObject.transform.localScale = modById.WeaponSpecificInfo[(int)this.type].Scale;
					}
					if (modById.WeaponSpecificInfo[(int)this.type].Shift != Vector3.zero)
					{
						gameObject.transform.localPosition = modById.WeaponSpecificInfo[(int)this.type].Shift;
					}
					gameObject.transform.parent.localScale = new Vector3(1f, 1f, 1.001f);
					foreach (string text in modById.WeaponSpecificInfo[(int)this.type].DisabledGameObjects)
					{
						if (text.Length > 0)
						{
							Transform transform4 = Utility.FindHierarchy(this._root.transform, text);
							transform4.gameObject.SetActive(false);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
	private void InstallCamo(MasteringMod masteringMod)
	{
		foreach (MeshRenderer meshRenderer in this._root.GetComponentsInChildren<MeshRenderer>())
		{
			foreach (Material material in meshRenderer.materials)
			{
				if (material.shader.name.ToLower().Contains("bumped specular mask"))
				{
					material.SetTexture("_DetailTex", masteringMod.Texture);
				}
			}
		}
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x000AC674 File Offset: 0x000AA874
	internal override void CallLateUpdate()
	{
		if (this._sniperLens)
		{
			this._sniperLens.color = Colors.alpha(this._sniperLens.color, 1f - this.player.Ammo.AimPos);
		}
		if (!this.LOD && this.G.IsInvoking("PostReload"))
		{
			for (int i = 0; i < this.reloadSounds.Length; i++)
			{
				if (this._root.animation["reload"].time >= this.reloadTimes[i] && this.reloadBools[i])
				{
					Audio.Play(base.GetComponent<PoolItem>(), this.reloadSounds[i], true, base.audio.minDistance, base.audio.maxDistance);
					this.reloadBools[i] = false;
				}
			}
		}
		this.UpdateMuzzleFlashesPosition();
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x000AC768 File Offset: 0x000AA968
	private void UpdateMuzzleFlashesPosition()
	{
		for (int i = 0; i < this._muzzles.Count; i++)
		{
			ClientWeapon.GOWithInitialPosition gowithInitialPosition = this._muzzles[i];
			if (gowithInitialPosition.GO.GetComponent<PoolItem>().itemActive)
			{
				gowithInitialPosition.GO.transform.rotation *= this.BulletHolder.rotation;
				Vector3 vec = this.BulletHolder.position + gowithInitialPosition.InitialLocalPosition;
				gowithInitialPosition.GO.transform.position = StartData.WeaponShaders.Replace.ModifyVec(vec);
			}
			else
			{
				this._toDelete.Add(gowithInitialPosition);
			}
		}
		for (int j = 0; j < this._toDelete.Count; j++)
		{
			this._muzzles.Remove(this._toDelete[j]);
			this._toDelete.Remove(this._toDelete[j]);
		}
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x000AC864 File Offset: 0x000AAA64
	internal override void Recover()
	{
		base.Recover();
		this.StopLoopSound();
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x000AC874 File Offset: 0x000AAA74
	internal void StopLoopSound()
	{
		if (base.audio.clip && base.audio.loop)
		{
			base.audio.Stop();
			base.audio.clip = null;
			base.audio.loop = false;
			base.audio.volume = Audio.soundVolume;
		}
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x000AC8DC File Offset: 0x000AAADC
	internal override void AdvanceSound()
	{
		if (this.secondSoundScheme && this.G.IsInvoking("StopFireLoopSound") && base.audio.clip == this.loopFire && this.state.clips != 0 && !base.Damaged)
		{
			this.G.CancelInvoke("StopFireLoopSound");
			this.G.Invoke("StopFireLoopSound", this.singleTime, false);
		}
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x000AC968 File Offset: 0x000AAB68
	[Obfuscation(Exclude = true)]
	internal override void StopFireLoopSound()
	{
		this.StopLoopSound();
		Audio.Play(base.GetComponent<PoolItem>(), this.tailFire, true, base.audio.minDistance, base.audio.maxDistance);
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x000AC9A4 File Offset: 0x000AABA4
	[Obfuscation(Exclude = true)]
	internal override void ReloadSound()
	{
		Audio.Play(base.GetComponent<PoolItem>(), this.boltReloadSounds, true, base.audio.minDistance, base.audio.maxDistance);
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x000AC9DC File Offset: 0x000AABDC
	internal override void PlayAfterIdleAim()
	{
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x000AC9E0 File Offset: 0x000AABE0
	internal override void PlayAfterAimIdle()
	{
		if (this.OpticCamera)
		{
			CameraListener.Disable(this.OpticCamera.gameObject);
		}
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x000ACA10 File Offset: 0x000AAC10
	internal override void PlayIdleAim()
	{
		if ((base.Optic || base.Kolimator) && this.OpticCamera)
		{
			CameraListener.Enable(this.OpticCamera.gameObject);
		}
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x000ACA54 File Offset: 0x000AAC54
	internal override void PlayAimIdle()
	{
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x000ACA58 File Offset: 0x000AAC58
	internal override void PlayIdleOut()
	{
		base.audio.mute = true;
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x000ACA68 File Offset: 0x000AAC68
	internal override void PlayOutIdle()
	{
		base.audio.mute = false;
		if (this.LOD)
		{
			return;
		}
		this._root.animation.Stop();
		this._root.animation.Rewind();
		if (this._root.animation["fire_start"] != null)
		{
			if (this.DoubleMagazine && this._root.animation["fire_start2"] != null)
			{
				this._root.animation.Play((!this.FirstReload) ? "fire_start2" : "fire_start");
			}
			else
			{
				this._root.animation.Play("fire_start");
			}
		}
		if (this._root.animation["out_to_idle"] != null)
		{
			string name;
			if (base.IsModable && !this.type.ToString().Contains("cs"))
			{
				name = this.type + "_cs_out_to_idle";
			}
			else
			{
				name = this.type + "_out_to_idle";
			}
			AnimationState animationState = (this.player as ClientNetPlayer).Animations.handsAnimation[name];
			this._root.animation["out_to_idle"].speed = animationState.length / base.OutIdle;
			this._root.animation.Play("out_to_idle");
		}
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x000ACC14 File Offset: 0x000AAE14
	internal override void PlayIdle()
	{
		if (this.LOD)
		{
			return;
		}
		this._root.animation.Stop();
		if (this.DoubleMagazine)
		{
			this._root.animation.Play((!this.FirstReload) ? "fire_start2" : "fire_start");
		}
		else
		{
			this._root.animation.Play("fire_start");
		}
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x000ACC90 File Offset: 0x000AAE90
	internal override void Cancel()
	{
		if (this.G.IsInvoking("StopFireLoopSound"))
		{
			this.StopFireLoopSound();
		}
		base.Cancel();
		if (this.secondSoundScheme && base.audio.isPlaying)
		{
			this.StopLoopSound();
		}
		if (!this.LOD)
		{
			this._root.animation.Stop();
			if (base.Damaged)
			{
				return;
			}
		}
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x000ACD08 File Offset: 0x000AAF08
	public void AnimationPlayBolt(float normalizedTime = 0f)
	{
		this._root.animation.Stop();
		this._root.animation["bolt"].normalizedTime = normalizedTime;
		this._root.animation.Play("bolt");
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x000ACD58 File Offset: 0x000AAF58
	protected override void OnFire()
	{
		if (!this.LOD)
		{
			Main.UserInfo.weaponsStates[(int)this.type].repair_info = this.state.repair_info;
		}
		if (this.isBolt && this.state.needReload)
		{
			if (this.LOD)
			{
				return;
			}
			float normalizedTime = (!this.player.Ammo.IsAim && !base.IsModable) ? 0.05f : 0f;
			this.AnimationPlayBolt(normalizedTime);
			Audio.Play(base.GetComponent<PoolItem>(), this.boltReloadSounds, true, base.audio.minDistance, base.audio.maxDistance);
		}
		else
		{
			if ((!this.isBolt || !this.player.Ammo.IsAim || this.state.needReload) && !this.LOD)
			{
				this._root.animation.Stop();
				if (this.state.clips == 1 && this.weaponNature == WeaponNature.pistol)
				{
					this._root.animation.Play("fire_end");
				}
				else if (this.DoubleMagazine)
				{
					this._root.animation.Play((!this.FirstReload) ? "fire2" : "fire");
				}
				else if (base.IsAbakan)
				{
					this._root.animation.Play((!this.state.singleShot && !this.AbakanFirstShot) ? "fire2" : "fire1");
				}
				else
				{
					this._root.animation.Play("fire");
					if (this.isBolt && base.IsModable)
					{
						this._root.animation.CrossFadeQueued("bolt");
					}
				}
				if (this.isBolt)
				{
					this.G.CancelInvoke("ReloadSound");
					this.G.Invoke("ReloadSound", 0.5f, false);
				}
			}
			if (this.secondSoundScheme)
			{
				if (base.audio.clip != this.loopFire)
				{
					base.audio.Stop();
					if (base.IsAbakan)
					{
						if ((this.AbakanFirstShot || this.state.singleShot) && this.state.clips > 1)
						{
							AudioClip[] array = new AudioClip[]
							{
								this.modLoopFire,
								this.modTailFire
							};
							base.audio.loop = false;
							base.audio.clip = array[UnityEngine.Random.Range(0, 2)];
						}
						else
						{
							base.audio.loop = true;
							base.audio.clip = this.loopFire;
						}
					}
					else
					{
						base.audio.loop = true;
						base.audio.clip = this.loopFire;
					}
					base.audio.volume = Audio.soundVolume;
					base.audio.priority = 0;
					base.audio.Play();
				}
				this.G.CancelInvoke("StopFireLoopSound");
				float num = (!this.LOD) ? 0.18f : 0.09f;
				this.G.Invoke("StopFireLoopSound", (!base.IsAbakan || (!this.AbakanFirstShot && (!this.state.singleShot || this.state.clips <= 1))) ? this.singleTime : num, false);
			}
			else if (this.fireSounds.Length == 0)
			{
				Debug.Log("No fire sounds in " + this.type);
			}
			else
			{
				Audio.Play(base.GetComponent<PoolItem>(), this.fireSounds[(int)(UnityEngine.Random.value * (float)this.fireSounds.Length)], true, base.audio.minDistance, base.audio.maxDistance);
			}
			this.SpawnMuzzleFlash();
		}
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x000AD194 File Offset: 0x000AB394
	private void SpawnMuzzleFlash()
	{
		GameObject gameObject = null;
		if (!base.SS && this.muzzleFlashName != string.Empty)
		{
			gameObject = SingletoneForm<PoolManager>.Instance[this.muzzleFlashName].Spawn();
		}
		if (base.SS && this.silencedMuzzleFlashName != string.Empty)
		{
			gameObject = SingletoneForm<PoolManager>.Instance[this.silencedMuzzleFlashName].Spawn();
		}
		if (gameObject == null)
		{
			return;
		}
		if (this.player is ClientNetPlayer)
		{
			gameObject.transform.localScale.Scale(this.BulletHolder.localScale);
			this._muzzles.Add(new ClientWeapon.GOWithInitialPosition(gameObject, gameObject.transform.localPosition));
			gameObject.transform.rotation *= this.BulletHolder.rotation;
			Vector3 vec = this.BulletHolder.position + gameObject.transform.localPosition;
			gameObject.transform.position = StartData.WeaponShaders.Replace.ModifyVec(vec);
		}
		else
		{
			Utility.ChangeParent(gameObject.transform, this.BulletHolder);
			gameObject.transform.position = this.BulletHolder.position;
		}
		gameObject.transform.rotation = this.BulletHolder.rotation * Quaternion.Euler(90f, 0f, 0f);
		if (!this.LOD)
		{
			return;
		}
		gameObject.transform.position += gameObject.transform.forward * 0.05f;
		Utility.SetLayerRecursively(gameObject, 0);
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x000AD350 File Offset: 0x000AB550
	protected override void OnReload()
	{
		BattleScreenGUI.showAmmo = true;
		if (!this.LOD)
		{
			if (this.DoubleMagazine)
			{
				string text = (!this.FirstReload) ? "reload" : "reload2";
				this._root.animation[text].speed = (this.player as ClientNetPlayer).Animations.ReloadLenght() / base.ReloadTime;
				this._root.animation.Play(text);
				for (int i = 0; i < this.reloadSounds.Length; i++)
				{
					this.reloadBools[i] = true;
				}
				if (this.FirstReload)
				{
					Audio.Play(base.GetComponent<PoolItem>(), this.boltReloadSounds, true, base.audio.minDistance, base.audio.maxDistance);
				}
			}
			else
			{
				this._root.animation["reload"].speed = (this.player as ClientNetPlayer).Animations.ReloadLenght() / base.ReloadTime;
				this._root.animation.Play("reload");
				for (int j = 0; j < this.reloadSounds.Length; j++)
				{
					this.reloadBools[j] = true;
				}
			}
		}
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x000AD4A0 File Offset: 0x000AB6A0
	protected override void OnPompovikReloadStart()
	{
		BattleScreenGUI.showAmmo = true;
		if (this.LOD)
		{
			return;
		}
		this._root.animation.Stop();
		this._root.animation["reload_start"].speed = ((!this.UseShotGunReload) ? this.player.Animations.ReloadStartLenght() : 1f);
		this._root.animation.Play("reload_start");
		Audio.Play(base.GetComponent<PoolItem>(), this.reloadSounds[0], true, base.audio.minDistance, base.audio.maxDistance);
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x000AD550 File Offset: 0x000AB750
	protected override void OnReloadStart()
	{
		if (!this.LOD)
		{
			Audio.Play(base.GetComponent<PoolItem>(), this.reloadSounds[1], true, base.audio.minDistance, base.audio.maxDistance);
		}
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x000AD594 File Offset: 0x000AB794
	protected override void OnCanInsertClip(float time)
	{
		if (this.LOD)
		{
			return;
		}
		this._root.animation.Stop();
		this._root.animation["reload_loop"].speed = this._root.animation["reload_loop"].length / time;
		this._root.animation.Play("reload_loop");
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x000AD60C File Offset: 0x000AB80C
	protected override void OnInsertClipSound()
	{
		if (!this.LOD)
		{
			int num = (int)(UnityEngine.Random.value * 3f);
			Audio.Play(base.GetComponent<PoolItem>(), this.reloadSounds[1 + num], true, base.audio.minDistance, base.audio.maxDistance);
		}
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x000AD660 File Offset: 0x000AB860
	protected override void OnReloadEnd()
	{
		if (this.LOD)
		{
			return;
		}
		this._root.animation.Stop();
		this._root.animation["reload_end"].speed = ((!this.UseShotGunReload) ? this.player.Animations.ReloadStartLenght() : 1f);
		float num = this._root.animation["reload_end"].length / 2.25f;
		if (this.weaponNature == WeaponNature.shotgun && this.pompovik && base.IsModable)
		{
			num /= 3f;
		}
		this._root.animation.Play("reload_end");
		base.Invoke("AudioReloadEnd", num);
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x000AD73C File Offset: 0x000AB93C
	protected override void AdditionalMuzzleFlash()
	{
		if (!base.SS && this.muzzleFlashName != string.Empty)
		{
			GameObject gameObject = SingletoneForm<PoolManager>.Instance[this.muzzleFlashName].Spawn();
			Utility.ChangeParent(gameObject.transform, this.BulletHolder);
			if (this.player is ClientNetPlayer)
			{
				gameObject.transform.position = StartData.WeaponShaders.Replace.ModifyVec(this.BulletHolder.position);
			}
			else
			{
				gameObject.transform.position = this.BulletHolder.position;
			}
			gameObject.transform.rotation = this.BulletHolder.rotation * Quaternion.Euler(90f, 0f, 0f);
			if (this.LOD)
			{
				gameObject.transform.position += gameObject.transform.forward * 0.05f;
				Utility.SetLayerRecursively(gameObject, 0);
			}
		}
		if (base.SS && this.silencedMuzzleFlashName != string.Empty)
		{
			GameObject gameObject2 = SingletoneForm<PoolManager>.Instance[this.silencedMuzzleFlashName].Spawn();
			Utility.ChangeParent(gameObject2.transform, this.BulletHolder);
			if (this.player is ClientNetPlayer)
			{
				gameObject2.transform.position = StartData.WeaponShaders.Replace.ModifyVec(this.BulletHolder.position);
			}
			else
			{
				gameObject2.transform.position = this.BulletHolder.position;
			}
			gameObject2.transform.rotation = this.BulletHolder.rotation * Quaternion.Euler(90f, 0f, 0f);
			if (this.LOD)
			{
				gameObject2.transform.position += gameObject2.transform.forward * 0.05f;
				Utility.SetLayerRecursively(gameObject2, 0);
			}
		}
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x000AD93C File Offset: 0x000ABB3C
	[Obfuscation(Exclude = true)]
	private void AudioReloadEnd()
	{
		Audio.Play(base.GetComponent<PoolItem>(), this.reloadSounds[4], true, base.audio.minDistance, base.audio.maxDistance);
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x000AD974 File Offset: 0x000ABB74
	internal float AniReloadEndTime
	{
		get
		{
			return this._root.animation["reload_end"].length;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x000AD990 File Offset: 0x000ABB90
	internal float AniReloadStartTime
	{
		get
		{
			return this._root.animation["reload_start"].length;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000EEA RID: 3818 RVA: 0x000AD9AC File Offset: 0x000ABBAC
	internal float AniReloadLoopTime
	{
		get
		{
			return this._root.animation["reload_loop"].speed;
		}
	}

	// Token: 0x04000F37 RID: 3895
	private CircleRandom _rand = new CircleRandom();

	// Token: 0x04000F38 RID: 3896
	private GameObject _root;

	// Token: 0x04000F39 RID: 3897
	private Material _sniperLens;

	// Token: 0x04000F3A RID: 3898
	private Weapons _rootType = Weapons.none;

	// Token: 0x04000F3B RID: 3899
	private string _rootName = string.Empty;

	// Token: 0x04000F3C RID: 3900
	private bool _shadowsDisabled;

	// Token: 0x04000F3D RID: 3901
	internal Transform BulletHolder;

	// Token: 0x04000F3E RID: 3902
	internal Transform ShellHolder;

	// Token: 0x04000F3F RID: 3903
	internal GameObject TacticalMod;

	// Token: 0x04000F40 RID: 3904
	internal GameObject OpticMod;

	// Token: 0x04000F41 RID: 3905
	internal GameObject SilencerMod;

	// Token: 0x04000F42 RID: 3906
	internal int StartSuitIndex;

	// Token: 0x04000F43 RID: 3907
	internal bool IsWeaponInited;

	// Token: 0x04000F44 RID: 3908
	internal List<Renderer> RenderersAll = new List<Renderer>();

	// Token: 0x04000F45 RID: 3909
	internal List<Renderer> RenderersCurrent = new List<Renderer>();

	// Token: 0x04000F46 RID: 3910
	[HideInInspector]
	internal bool Visible;

	// Token: 0x04000F47 RID: 3911
	private List<ClientWeapon.GOWithInitialPosition> _muzzles = new List<ClientWeapon.GOWithInitialPosition>();

	// Token: 0x04000F48 RID: 3912
	private List<ClientWeapon.GOWithInitialPosition> _toDelete = new List<ClientWeapon.GOWithInitialPosition>();

	// Token: 0x020001B2 RID: 434
	private class GOWithInitialPosition
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x000AD9E4 File Offset: 0x000ABBE4
		public GOWithInitialPosition(GameObject go, Vector3 initialLocalPosition)
		{
			this.GO = go;
			this.InitialLocalPosition = initialLocalPosition;
		}

		// Token: 0x04000F4C RID: 3916
		public GameObject GO;

		// Token: 0x04000F4D RID: 3917
		public Vector3 InitialLocalPosition;
	}
}
