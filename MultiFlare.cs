using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class MultiFlare : MonoBehaviour
{
	// Token: 0x06000057 RID: 87 RVA: 0x00005F24 File Offset: 0x00004124
	private void Start()
	{
		if (base.transform.childCount > 0)
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		this.GenerateMesh();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00005FB0 File Offset: 0x000041B0
	public void GenerateMesh()
	{
		if (this.Atlas == null)
		{
			return;
		}
		bool flag = Application.isEditor && !Application.isPlaying;
		if (flag && base.transform.childCount != 0)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this._filters[i] == null)
				{
					foreach (object obj in base.transform)
					{
						Transform transform = (Transform)obj;
						if (transform.name == ((MultiFlare.FlareType)i).ToString())
						{
							this._filters[i] = transform.GetComponent<MeshFilter>();
						}
					}
				}
			}
		}
		this._offScreenLights = this.LightsForUpdateInit(MultiFlare.FlareType.OffScreen);
		this._screenLights = this.LightsForUpdateInit(MultiFlare.FlareType.Normal);
		this._shitLights = this.LightsForUpdateInit(MultiFlare.FlareType.Shit);
		for (int j = 0; j < 3; j++)
		{
			this.GenerateMesh((MultiFlare.FlareType)j);
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000060EC File Offset: 0x000042EC
	private void GenerateMesh(MultiFlare.FlareType type)
	{
		bool flag = true;
		foreach (MultiFlareLight multiFlareLight in this.Lights)
		{
			foreach (MultiFlareLight.Flare flare in multiFlareLight.Flares)
			{
				if (flare.Type == type)
				{
					flag = false;
					break;
				}
			}
		}
		if (flag && this._filters[(int)type] != null && this._filters[(int)type].mesh != null && this._filters[(int)type].mesh.vertexCount == 0)
		{
			return;
		}
		if (this._filters[(int)type] == null)
		{
			GameObject gameObject = new GameObject(type.ToString(), new Type[]
			{
				typeof(MeshFilter),
				typeof(MeshRenderer)
			});
			this._filters[(int)type] = gameObject.GetComponent<MeshFilter>();
			switch (type)
			{
			case MultiFlare.FlareType.Normal:
				gameObject.renderer.material = this.NormalMat;
				break;
			case MultiFlare.FlareType.OffScreen:
				gameObject.renderer.material = this.OffScreenMat;
				break;
			case MultiFlare.FlareType.Shit:
				gameObject.renderer.material = this.ShitMat;
				break;
			}
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			gameObject.renderer.castShadows = false;
			gameObject.renderer.receiveShadows = false;
		}
		Mesh mesh = this.GetMesh(type);
		this._filters[(int)type].mesh = mesh;
		if (type == MultiFlare.FlareType.OffScreen)
		{
			this._offScreenMesh = mesh;
			this._offScreenMesh.MarkDynamic();
		}
		else if (type == MultiFlare.FlareType.Normal && this._screenLights.Length != 0)
		{
			this._screenMesh = mesh;
			this._screenMesh.MarkDynamic();
		}
		if (type == MultiFlare.FlareType.Shit && this._shitLights.Length != 0)
		{
			this._shitMesh = mesh;
			this._shitMesh.MarkDynamic();
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00006334 File Offset: 0x00004534
	private void LateUpdate()
	{
		if (this._offScreenMesh != null)
		{
			Camera camera;
			if ((camera = Camera.current) == null)
			{
				camera = (Camera.mainCamera ?? Camera.main);
			}
			Camera camera2 = camera;
			if (camera2 != null)
			{
				Vector3 position = camera2.transform.position;
				int rayCastMask = this.RayCastMask;
				float num = this.Speed * Time.deltaTime;
				num = Mathf.Clamp01(num);
				bool flag = false;
				foreach (MultiFlare.LightForUpdate lightForUpdate in this._offScreenLights)
				{
					flag |= lightForUpdate.ProcessOffScreen(position, rayCastMask, num, this._offScreenColors);
				}
				if (flag)
				{
					this._offScreenMesh.colors32 = this._offScreenColors;
				}
			}
		}
		if (this._screenMesh != null)
		{
			bool flag2 = false;
			foreach (MultiFlare.LightForUpdate lightForUpdate2 in this._screenLights)
			{
				flag2 |= lightForUpdate2.Process(this._screenColors);
			}
			if (flag2)
			{
				this._screenMesh.colors32 = this._screenColors;
			}
		}
		if (this._shitMesh != null)
		{
			bool flag3 = false;
			foreach (MultiFlare.LightForUpdate lightForUpdate3 in this._shitLights)
			{
				flag3 |= lightForUpdate3.Process(this._shitColors);
			}
			if (flag3)
			{
				this._shitMesh.colors32 = this._shitColors;
			}
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000064CC File Offset: 0x000046CC
	private Mesh GetMesh(MultiFlare.FlareType type)
	{
		int num = 0;
		foreach (MultiFlareLight multiFlareLight in this.Lights)
		{
			foreach (MultiFlareLight.Flare flare in multiFlareLight.Flares)
			{
				if (flare.Type == type)
				{
					num++;
				}
			}
		}
		int num2 = num << 2;
		Vector3[] vertices = new Vector3[num2];
		Vector4[] tangents = new Vector4[num2];
		Vector3[] normals = new Vector3[num2];
		Vector2[] array = new Vector2[num2];
		Vector2[] uv = new Vector2[num2];
		Color32[] array2 = new Color32[num2];
		int[] array3 = new int[num * 6];
		int k = 0;
		int num3 = 0;
		while (k < num)
		{
			int num4 = k << 2;
			array3[num3++] = num4;
			array3[num3++] = num4 + 3;
			array3[num3++] = num4 + 2;
			array3[num3++] = num4 + 2;
			array3[num3++] = num4 + 1;
			array3[num3++] = num4;
			k++;
		}
		int l = 0;
		int num5 = 0;
		while (l < this.Lights.Length)
		{
			this.Lights[l].FillMesh(ref num5, type, this.Atlas, vertices, tangents, normals, array, uv, array2);
			l++;
		}
		for (int m = 0; m < num2; m++)
		{
			array2[m].a = byte.MaxValue;
		}
		if (type == MultiFlare.FlareType.OffScreen)
		{
			this._offScreenColors = array2;
		}
		else if (type == MultiFlare.FlareType.Normal)
		{
			this._screenColors = array2;
		}
		else if (type == MultiFlare.FlareType.Shit)
		{
			this._shitColors = array2;
		}
		foreach (MultiFlareLight multiFlareLight2 in this.Lights)
		{
			this.CalcBounds(multiFlareLight2.transform.position);
		}
		return new Mesh
		{
			vertices = vertices,
			normals = normals,
			tangents = tangents,
			uv = array,
			uv1 = uv,
			colors32 = array2,
			triangles = array3,
			bounds = this._bounds
		};
	}

	// Token: 0x0600005C RID: 92 RVA: 0x0000671C File Offset: 0x0000491C
	private void CalcBounds(Vector3 position)
	{
		Vector3 min = this._bounds.min;
		Vector3 max = this._bounds.max;
		if (position.x > min.x && position.y > min.y && position.z > min.z && position.x < max.x && position.y < max.y && position.z < max.z)
		{
			return;
		}
		if (position.x < min.x)
		{
			min.x = position.x - 50f;
		}
		if (position.y < min.y)
		{
			min.y = position.y - 50f;
		}
		if (position.z < min.z)
		{
			min.z = position.z - 50f;
		}
		if (position.x > max.x)
		{
			max.x = position.x + 50f;
		}
		if (position.y > max.y)
		{
			max.y = position.y + 50f;
		}
		if (position.z > max.z)
		{
			max.z = position.z + 50f;
		}
		this._bounds.SetMinMax(min, max);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000068AC File Offset: 0x00004AAC
	private MultiFlare.LightForUpdate[] LightsForUpdateInit(MultiFlare.FlareType type)
	{
		LinkedList<MultiFlare.LightForUpdate> linkedList = new LinkedList<MultiFlare.LightForUpdate>();
		Func<MultiFlareLight, int, int, MultiFlare.LightForUpdate> func = new Func<MultiFlareLight, int, int, MultiFlare.LightForUpdate>(MultiFlare.LightForUpdate.GetLight);
		if (type == MultiFlare.FlareType.OffScreen)
		{
			func = new Func<MultiFlareLight, int, int, MultiFlare.LightForUpdate>(MultiFlare.LightForUpdate.GetLightOffScreen);
		}
		int i = 0;
		int num = 0;
		while (i < this.Lights.Length)
		{
			int arg = num;
			MultiFlareLight.Flare[] flares = this.Lights[i].Flares;
			foreach (MultiFlareLight.Flare flare in flares)
			{
				if (flare.Type == type)
				{
					num += 4;
				}
			}
			MultiFlare.LightForUpdate lightForUpdate = func(this.Lights[i], arg, num);
			if (lightForUpdate != null)
			{
				linkedList.AddLast(lightForUpdate);
			}
			i++;
		}
		MultiFlare.LightForUpdate[] array2 = new MultiFlare.LightForUpdate[linkedList.Count];
		linkedList.CopyTo(array2, 0);
		return array2;
	}

	// Token: 0x040000A3 RID: 163
	private const int TypesCount = 3;

	// Token: 0x040000A4 RID: 164
	public ProFlareAtlas Atlas;

	// Token: 0x040000A5 RID: 165
	public Material NormalMat;

	// Token: 0x040000A6 RID: 166
	public Material OffScreenMat;

	// Token: 0x040000A7 RID: 167
	public Material ShitMat;

	// Token: 0x040000A8 RID: 168
	public LayerMask RayCastMask;

	// Token: 0x040000A9 RID: 169
	public float Speed;

	// Token: 0x040000AA RID: 170
	public MultiFlareLight[] Lights = new MultiFlareLight[0];

	// Token: 0x040000AB RID: 171
	private MeshFilter[] _filters = new MeshFilter[3];

	// Token: 0x040000AC RID: 172
	private Bounds _bounds = default(Bounds);

	// Token: 0x040000AD RID: 173
	private Mesh _offScreenMesh;

	// Token: 0x040000AE RID: 174
	private Mesh _screenMesh;

	// Token: 0x040000AF RID: 175
	private Mesh _shitMesh;

	// Token: 0x040000B0 RID: 176
	private Color32[] _offScreenColors;

	// Token: 0x040000B1 RID: 177
	private Color32[] _screenColors;

	// Token: 0x040000B2 RID: 178
	private Color32[] _shitColors;

	// Token: 0x040000B3 RID: 179
	private MultiFlare.LightForUpdate[] _offScreenLights;

	// Token: 0x040000B4 RID: 180
	private MultiFlare.LightForUpdate[] _screenLights;

	// Token: 0x040000B5 RID: 181
	private MultiFlare.LightForUpdate[] _shitLights;

	// Token: 0x02000015 RID: 21
	private class LightForUpdate
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00006994 File Offset: 0x00004B94
		public bool ProcessOffScreen(Vector3 camPos, int rayCastMask, float delta, Color32[] colors)
		{
			if (this._lightObject != null && this._lightObject.intensity != this._lastIntensity)
			{
				this._lastIntensity = this._lightObject.intensity;
				this._lightI = this._lastIntensity * 0.125f;
			}
			bool flag = !Physics.Linecast(camPos, this._position, rayCastMask);
			if (this._lastAlpha == ((!flag) ? 0f : 1f) && this._lightObject == null)
			{
				return false;
			}
			this._lastAlpha += ((!flag) ? (-delta) : delta);
			if (flag)
			{
				if (this._lastAlpha > 1f)
				{
					this._lastAlpha = 1f;
				}
			}
			else if (this._lastAlpha < 0f)
			{
				this._lastAlpha = 0f;
			}
			byte a = (byte)(this._lastAlpha * this._lightI * 255f);
			for (int i = this._vStart; i < this._vEnd; i++)
			{
				colors[i].a = a;
			}
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00006ACC File Offset: 0x00004CCC
		public static MultiFlare.LightForUpdate GetLightOffScreen(MultiFlareLight light, int start, int end)
		{
			MultiFlareLight.Flare[] flares = light.Flares;
			bool flag = false;
			foreach (MultiFlareLight.Flare flare in flares)
			{
				if (flare.Type == MultiFlare.FlareType.OffScreen)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return null;
			}
			return new MultiFlare.LightForUpdate
			{
				_vStart = start,
				_vEnd = end,
				_position = light.transform.position,
				_lightObject = light.LightObject
			};
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00006B54 File Offset: 0x00004D54
		public bool Process(Color32[] colors)
		{
			if (this._lightObject != null && this._lightObject.intensity != this._lastIntensity)
			{
				this._lastIntensity = this._lightObject.intensity;
				byte a = (byte)(this._lastIntensity * 31.875f);
				for (int i = this._vStart; i < this._vEnd; i++)
				{
					colors[i].a = a;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public static MultiFlare.LightForUpdate GetLight(MultiFlareLight light, int start, int end)
		{
			if (light.LightObject == null)
			{
				return null;
			}
			return new MultiFlare.LightForUpdate
			{
				_vStart = start,
				_vEnd = end,
				_position = light.transform.position,
				_lightObject = light.LightObject
			};
		}

		// Token: 0x040000B6 RID: 182
		private Light _lightObject;

		// Token: 0x040000B7 RID: 183
		private Vector3 _position;

		// Token: 0x040000B8 RID: 184
		private int _vStart;

		// Token: 0x040000B9 RID: 185
		private int _vEnd;

		// Token: 0x040000BA RID: 186
		private float _lastAlpha = 255f;

		// Token: 0x040000BB RID: 187
		private float _lastIntensity;

		// Token: 0x040000BC RID: 188
		private float _lightI;
	}

	// Token: 0x02000016 RID: 22
	public enum FlareType
	{
		// Token: 0x040000BE RID: 190
		Normal,
		// Token: 0x040000BF RID: 191
		OffScreen,
		// Token: 0x040000C0 RID: 192
		Shit
	}

	// Token: 0x02000017 RID: 23
	public enum RotationType
	{
		// Token: 0x040000C2 RID: 194
		None,
		// Token: 0x040000C3 RID: 195
		Normal,
		// Token: 0x040000C4 RID: 196
		Inverse
	}
}
