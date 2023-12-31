using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class Flicker : MonoBehaviour
{
	// Token: 0x06000052 RID: 82 RVA: 0x00005A94 File Offset: 0x00003C94
	private void Awake()
	{
		if (this.RandomTimeShift)
		{
			this.TimeShift = UnityEngine.Random.value * 300f;
		}
		this._light = base.light;
		this._rndSeed = UnityEngine.Random.value * 10f;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00005AD0 File Offset: 0x00003CD0
	private void OnValidate()
	{
		if (this.Generate != Flicker.CurveType.SelectTypeForGenerate)
		{
			this.Curve = this.GetCurve(this.Generate);
			AnimationCurve curve = this.Curve;
			WrapMode wrapMode = WrapMode.Loop;
			this.Curve.preWrapMode = wrapMode;
			curve.postWrapMode = wrapMode;
			this.Generate = Flicker.CurveType.SelectTypeForGenerate;
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00005B1C File Offset: 0x00003D1C
	private AnimationCurve GetCurve(Flicker.CurveType type)
	{
		if (type == Flicker.CurveType.Sin)
		{
			return new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0.5f, 4f, 4f),
				new Keyframe(0.5f, 0.5f, -4f, -4f),
				new Keyframe(1f, 0.5f, 4f, 4f)
			});
		}
		if (type == Flicker.CurveType.Random)
		{
			Keyframe[] array = new Keyframe[24];
			float num;
			for (int i = 2; i < array.Length; i++)
			{
				num = UnityEngine.Random.value;
				num = num * num * 30f;
				array[i] = new Keyframe(UnityEngine.Random.value, 0.5f + UnityEngine.Random.value * 0.5f, num, num);
			}
			float value = UnityEngine.Random.value;
			num = UnityEngine.Random.value;
			num = num * num * 90f;
			array[0] = new Keyframe(0f, value, num, num);
			array[1] = new Keyframe(1f, value, num, num);
			return new AnimationCurve(array);
		}
		if (type == Flicker.CurveType.Saw)
		{
			Keyframe[] array2 = new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			};
			array2[0].tangentMode = 10;
			array2[1].tangentMode = 10;
			return new AnimationCurve(array2);
		}
		if (type == Flicker.CurveType.Square)
		{
			Keyframe[] array3 = new Keyframe[]
			{
				new Keyframe(0f, 0f, float.PositiveInfinity, float.PositiveInfinity),
				new Keyframe(0.5f, 1f, float.PositiveInfinity, float.PositiveInfinity),
				new Keyframe(1f, 1f, float.PositiveInfinity, float.PositiveInfinity)
			};
			array3[0].tangentMode = 31;
			array3[1].tangentMode = 31;
			array3[2].tangentMode = 31;
			return new AnimationCurve(array3);
		}
		if (type == Flicker.CurveType.Triangle)
		{
			Keyframe[] array4 = new Keyframe[]
			{
				new Keyframe(0f, 0f, -2f, 2f),
				new Keyframe(0.5f, 1f, 2f, -2f),
				new Keyframe(1f, 0f, -2f, 2f)
			};
			array4[0].tangentMode = 10;
			array4[1].tangentMode = 10;
			array4[2].tangentMode = 10;
			return new AnimationCurve(array4);
		}
		return new AnimationCurve();
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00005E6C File Offset: 0x0000406C
	private void Update()
	{
		float num = this.TimeShift + Time.time * this.Frequency;
		if (this.FullRandomCurve)
		{
			this._light.intensity = this.IntensityShift + Mathf.PerlinNoise(num, this._rndSeed) * this.Intensity;
		}
		else
		{
			this._light.intensity = this.IntensityShift + this.Curve.Evaluate(num) * this.Intensity;
		}
	}

	// Token: 0x04000092 RID: 146
	public float Frequency = 1f;

	// Token: 0x04000093 RID: 147
	public float Intensity = 1f;

	// Token: 0x04000094 RID: 148
	public float IntensityShift;

	// Token: 0x04000095 RID: 149
	public float TimeShift;

	// Token: 0x04000096 RID: 150
	public AnimationCurve Curve;

	// Token: 0x04000097 RID: 151
	public Flicker.CurveType Generate;

	// Token: 0x04000098 RID: 152
	public bool RandomTimeShift;

	// Token: 0x04000099 RID: 153
	public bool FullRandomCurve;

	// Token: 0x0400009A RID: 154
	private Light _light;

	// Token: 0x0400009B RID: 155
	private float _rndSeed;

	// Token: 0x02000013 RID: 19
	public enum CurveType
	{
		// Token: 0x0400009D RID: 157
		SelectTypeForGenerate,
		// Token: 0x0400009E RID: 158
		Random,
		// Token: 0x0400009F RID: 159
		Sin,
		// Token: 0x040000A0 RID: 160
		Triangle,
		// Token: 0x040000A1 RID: 161
		Saw,
		// Token: 0x040000A2 RID: 162
		Square
	}
}
