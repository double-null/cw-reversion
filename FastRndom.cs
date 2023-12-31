using System;
using UnityEngine;

// Token: 0x0200038F RID: 911
public static class FastRndom
{
	// Token: 0x06001D44 RID: 7492 RVA: 0x001011E0 File Offset: 0x000FF3E0
	static FastRndom()
	{
		FastRndom._vectors = FastRndom.RandomsV(256, () => UnityEngine.Random.onUnitSphere);
		FastRndom._hemisphereVectors = FastRndom.RandomsV(256, delegate
		{
			Vector3 insideUnitSphere = UnityEngine.Random.insideUnitSphere;
			if (insideUnitSphere.y < 0f)
			{
				insideUnitSphere.y = -insideUnitSphere.y;
			}
			insideUnitSphere.y += 0.3f;
			return insideUnitSphere.normalized;
		});
		FastRndom._circleVectors = FastRndom.RandomsV(256, delegate
		{
			Vector2 vector = new Vector2(0.5f - UnityEngine.Random.value, 0.5f - UnityEngine.Random.value);
			Vector2 normalized = vector.normalized;
			return new Vector3(normalized.x, 0f, normalized.y);
		});
	}

	// Token: 0x06001D45 RID: 7493 RVA: 0x001012B0 File Offset: 0x000FF4B0
	public static int Int()
	{
		FastRndom._rnd1 ^= FastRndom._rnd1 << 7;
		FastRndom._rnd1 ^= FastRndom._rnd1 >> 3;
		return FastRndom._rnd1;
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x001012DC File Offset: 0x000FF4DC
	public static int Int(int max)
	{
		if (max == 0)
		{
			return 0;
		}
		FastRndom._rnd1 ^= FastRndom._rnd1 << 7;
		FastRndom._rnd1 ^= FastRndom._rnd1 >> 3;
		return ((FastRndom._rnd1 >= 0) ? FastRndom._rnd1 : (-FastRndom._rnd1)) % max;
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x00101334 File Offset: 0x000FF534
	public static int Int(int start, int diapazone)
	{
		FastRndom._rnd1 ^= FastRndom._rnd1 << 7;
		FastRndom._rnd1 ^= FastRndom._rnd1 >> 3;
		return start + ((FastRndom._rnd1 >= 0) ? FastRndom._rnd1 : (-FastRndom._rnd1)) % diapazone;
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x00101384 File Offset: 0x000FF584
	public static uint Uint()
	{
		FastRndom._rnd2 ^= FastRndom._rnd2 << 7;
		FastRndom._rnd2 ^= FastRndom._rnd2 >> 3;
		return FastRndom._rnd2;
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x001013B0 File Offset: 0x000FF5B0
	public static float Float()
	{
		if (--FastRndom._rndI < 0)
		{
			FastRndom._rndI = 255;
		}
		return FastRndom._floats[FastRndom._rndI];
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x001013E8 File Offset: 0x000FF5E8
	public static float FloatRotation()
	{
		if (--FastRndom._rndI < 0)
		{
			FastRndom._rndI = 255;
		}
		return FastRndom._fRotations[FastRndom._rndI];
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x00101420 File Offset: 0x000FF620
	public static Vector3 VectorNormalized()
	{
		if (--FastRndom._rndI < 0)
		{
			FastRndom._rndI = 255;
		}
		return FastRndom._vectors[FastRndom._rndI];
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x00101454 File Offset: 0x000FF654
	public static Vector3 VectorHemisphere()
	{
		if (--FastRndom._rndI < 0)
		{
			FastRndom._rndI = 255;
		}
		return FastRndom._hemisphereVectors[FastRndom._rndI];
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x00101488 File Offset: 0x000FF688
	public static Vector3 VectorCircle()
	{
		if (--FastRndom._rndI < 0)
		{
			FastRndom._rndI = 255;
		}
		return FastRndom._circleVectors[FastRndom._rndI];
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x001014BC File Offset: 0x000FF6BC
	private static float[] RandomsF(int length, float mult = 1f)
	{
		float[] array = new float[length];
		if (mult == 1f)
		{
			for (int i = 0; i < length; i++)
			{
				array[i] = UnityEngine.Random.value;
			}
		}
		else
		{
			for (int j = 0; j < length; j++)
			{
				array[j] = UnityEngine.Random.value * mult;
			}
		}
		return array;
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x00101518 File Offset: 0x000FF718
	private static Vector3[] RandomsV(int length, Func<Vector3> func)
	{
		Vector3[] array = new Vector3[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = func();
		}
		return array;
	}

	// Token: 0x040021EF RID: 8687
	private const int Count = 256;

	// Token: 0x040021F0 RID: 8688
	private static int _rnd1 = 242313459;

	// Token: 0x040021F1 RID: 8689
	private static uint _rnd2 = 242313459U;

	// Token: 0x040021F2 RID: 8690
	private static int _rndI;

	// Token: 0x040021F3 RID: 8691
	private static float[] _floats = FastRndom.RandomsF(256, 1f);

	// Token: 0x040021F4 RID: 8692
	private static Vector3[] _vectors;

	// Token: 0x040021F5 RID: 8693
	private static Vector3[] _hemisphereVectors;

	// Token: 0x040021F6 RID: 8694
	private static Vector3[] _circleVectors;

	// Token: 0x040021F7 RID: 8695
	private static float[] _fRotations = FastRndom.RandomsF(256, 360f);

	// Token: 0x02000390 RID: 912
	internal class Unrepeated
	{
		// Token: 0x06001D53 RID: 7507 RVA: 0x001015F8 File Offset: 0x000FF7F8
		public Unrepeated(int maxRandom)
		{
			this._maxRandom = maxRandom;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00101608 File Offset: 0x000FF808
		public int Get()
		{
			FastRndom._rnd1 ^= FastRndom._rnd1 << 7;
			FastRndom._rnd1 ^= FastRndom._rnd1 >> 3;
			int num = ((FastRndom._rnd1 >= 0) ? FastRndom._rnd1 : (-FastRndom._rnd1)) % this._maxRandom;
			if (num == this._lastRandom)
			{
				num++;
			}
			this._lastRandom = num;
			return num % this._maxRandom;
		}

		// Token: 0x040021FB RID: 8699
		private int _lastRandom;

		// Token: 0x040021FC RID: 8700
		private int _maxRandom;
	}
}
