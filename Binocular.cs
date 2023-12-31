using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
internal class Binocular : MonoBehaviour
{
	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000F1B RID: 3867 RVA: 0x000AF1A0 File Offset: 0x000AD3A0
	// (set) Token: 0x06000F1A RID: 3866 RVA: 0x000AF198 File Offset: 0x000AD398
	public static Binocular Instance { get; private set; }

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000F1C RID: 3868 RVA: 0x000AF1A8 File Offset: 0x000AD3A8
	public static bool Exist
	{
		get
		{
			return Binocular.Instance != null;
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000F1D RID: 3869 RVA: 0x000AF1B8 File Offset: 0x000AD3B8
	// (set) Token: 0x06000F1E RID: 3870 RVA: 0x000AF1CC File Offset: 0x000AD3CC
	public static bool HighLight
	{
		get
		{
			return Binocular.Instance._highLighting.Enable;
		}
		set
		{
			Binocular.Instance._highLighting.Enable = value;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000AF1E0 File Offset: 0x000AD3E0
	public float FOV
	{
		get
		{
			return (!this._on) ? 60f : this._fov;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000F20 RID: 3872 RVA: 0x000AF200 File Offset: 0x000AD400
	public bool On
	{
		get
		{
			return this._on;
		}
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x000AF208 File Offset: 0x000AD408
	private void Awake()
	{
		Binocular.Instance = this;
		base.enabled = false;
		this._on = false;
		this._mask = new GUIMaskTex(MainGUI.Instance.binoculars, false);
		this._highLighting = new BinocularHighLighting();
		this.Vals = new float[]
		{
			1.5f,
			2f,
			4f,
			8f
		};
		this._switcher = new StateSwitcher();
		this._switcher.SwitchingOn = StartData.binocular.blackFlashGoingToOn;
		this._switcher.SwitchingOff = StartData.binocular.blackFlashGoingToOff;
		this._switcher.AddCase(StartData.binocular.GoingToOnTimeMax, delegate
		{
			this.Switch(true);
		}, true);
		this._switcher.AddCase(StartData.binocular.GoingToOffTimeMax, delegate
		{
			this.Switch(false);
		}, false);
		this._flash = new GUIScreenTex(TexUtils.GetTexture(1, 1, new Color[]
		{
			Color.black
		}));
		this._layerMask = StartData.binocular.LayerMask.value;
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x000AF318 File Offset: 0x000AD518
	private void Start()
	{
		this._camera = base.camera;
		this.baseTangent = Mathf.Tan(this._camera.fieldOfView * 0.017453292f);
		this.OnActivate();
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x000AF354 File Offset: 0x000AD554
	private void OnActivate()
	{
		if (this._camera == null)
		{
			return;
		}
		this._currentMultiplicity = this.Vals[0];
		this._fov = (this._targetFov = this.MultiplicityToFov(this._currentMultiplicity));
		this.SetSensitivity();
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x000AF3A4 File Offset: 0x000AD5A4
	private void Update()
	{
		this._highLighting.Update();
		this._switcher.Process(Time.deltaTime);
		if (!this._on)
		{
			return;
		}
		if (Input.GetMouseButtonDown(1))
		{
			this._highLighting.Start(1.5f);
		}
		if (Input.GetMouseButtonUp(1))
		{
			this._highLighting.Stop();
		}
		float axisRaw = Input.GetAxisRaw("Mouse ScrollWheel");
		if (axisRaw != 0f)
		{
			this._i += (int)(axisRaw * 10f);
			if (this._i < 0)
			{
				this._i = 0;
			}
			else if (this._i >= this.Vals.Length)
			{
				this._i = this.Vals.Length - 1;
			}
			this._currentMultiplicity = this.Vals[this._i];
			this._targetFov = this.MultiplicityToFov(this._currentMultiplicity);
			this.SetSensitivity();
		}
		if (this._targetFov != this._fov)
		{
			float num = this._targetFov - this._fov;
			bool flag = num < 0f;
			float num2 = this.Speed * Time.deltaTime * this._fov;
			if (num2 < ((!flag) ? num : (-num)))
			{
				this._fov += ((!flag) ? num2 : (-num2));
			}
			else
			{
				this._fov = this._targetFov;
			}
		}
		this._highLighting.DistanceText = this.GetDistance(this._camera);
		this._highLighting.MultiplicityText = string.Format("{0:0.0}X", this.FovToMultiplicity(this._fov));
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x000AF554 File Offset: 0x000AD754
	private void OnGUI()
	{
		if (this._switcher.InProcess)
		{
			this._flash.Draw(this._switcher.Value);
		}
		if (!this._on)
		{
			return;
		}
		this._mask.Draw();
		this._highLighting.Draw();
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x000AF5AC File Offset: 0x000AD7AC
	private float MultiplicityToFov(float multiplicity)
	{
		return 57.29578f * Mathf.Atan(this.baseTangent / multiplicity);
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x000AF5C4 File Offset: 0x000AD7C4
	private float FovToMultiplicity(float fov)
	{
		return this.baseTangent / Mathf.Tan(fov * 0.017453292f);
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x000AF5DC File Offset: 0x000AD7DC
	private void SetSensitivity()
	{
		this._sensitivity = 1f / this._currentMultiplicity;
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x000AF5F0 File Offset: 0x000AD7F0
	public static float GetSensitivity()
	{
		return Binocular.Instance._sensitivity;
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x000AF5FC File Offset: 0x000AD7FC
	private string GetDistance(Camera camera)
	{
		Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 1000f, this._layerMask))
		{
			float distance = raycastHit.distance;
			return string.Format((distance <= 20f) ? "{0:0.0}M" : "{0:0}M", distance);
		}
		return "Infinity";
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x000AF66C File Offset: 0x000AD86C
	public static Binocular AddTo(GameObject gameObject)
	{
		Binocular[] array = (Binocular[])UnityEngine.Object.FindObjectsOfType(typeof(Binocular));
		foreach (Binocular obj in array)
		{
			UnityEngine.Object.Destroy(obj);
		}
		return gameObject.AddComponent<Binocular>();
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x000AF6B4 File Offset: 0x000AD8B4
	private void Switch(bool on)
	{
		this._on = on;
		if (on)
		{
			this.OnActivate();
		}
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x000AF6CC File Offset: 0x000AD8CC
	public static void StartSwitch(bool on)
	{
		if (Binocular.Instance == null)
		{
			return;
		}
		if (Binocular.Instance._switcher.InProcess)
		{
			return;
		}
		if (on)
		{
			Binocular.Instance.enabled = true;
		}
		if (!Binocular.Instance._switcher.Switch(on))
		{
			return;
		}
		Audio2DPlayer.Play((!on) ? StartData.binocular.SwitchOff : StartData.binocular.SwitchOn);
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x000AF74C File Offset: 0x000AD94C
	public static void ForceSwitchOff()
	{
		if (Binocular.Instance == null)
		{
			return;
		}
		Binocular.Instance._switcher.ForceSwitch(false);
		Binocular.Instance.Switch(false);
	}

	// Token: 0x04000F69 RID: 3945
	public static bool UnLocked;

	// Token: 0x04000F6A RID: 3946
	public float Speed = 1f;

	// Token: 0x04000F6B RID: 3947
	public float[] Vals;

	// Token: 0x04000F6C RID: 3948
	private GUIMaskTex _mask;

	// Token: 0x04000F6D RID: 3949
	private BinocularHighLighting _highLighting;

	// Token: 0x04000F6E RID: 3950
	private StateSwitcher _switcher;

	// Token: 0x04000F6F RID: 3951
	private GUIScreenTex _flash;

	// Token: 0x04000F70 RID: 3952
	private int _layerMask;

	// Token: 0x04000F71 RID: 3953
	private bool _on;

	// Token: 0x04000F72 RID: 3954
	private int _i;

	// Token: 0x04000F73 RID: 3955
	private float _targetFov;

	// Token: 0x04000F74 RID: 3956
	private float _fov;

	// Token: 0x04000F75 RID: 3957
	private float _currentMultiplicity;

	// Token: 0x04000F76 RID: 3958
	private float _sensitivity = 1f;

	// Token: 0x04000F77 RID: 3959
	private Camera _camera;

	// Token: 0x04000F78 RID: 3960
	private float baseTangent;
}
