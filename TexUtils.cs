using System;
using UnityEngine;

// Token: 0x0200038A RID: 906
public static class TexUtils
{
	// Token: 0x06001D25 RID: 7461 RVA: 0x001006D4 File Offset: 0x000FE8D4
	public static void FuncGraph(ref Texture2D texture, ref Color32[] colors, Func<float, float> func, Vector2 gSize, int width, int height, float start, float softness)
	{
		if (texture == null)
		{
			texture = new Texture2D(width, height, TextureFormat.RGB565, false);
			colors = texture.GetPixels32();
		}
		Color32 color = new Color32(0, 0, 0, byte.MaxValue);
		Color32 color2 = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		for (int i = 0; i < colors.Length; i++)
		{
			colors[i] = color;
		}
		float num = gSize.x / (float)width;
		float num2 = 1f / gSize.y;
		for (int j = 0; j < width; j++)
		{
			float arg = (float)j * num;
			float num3 = Mathf.Clamp01(func(arg) * num2);
			colors[j + width * (int)(num3 * (float)(height - 1))] = color2;
		}
		texture.SetPixels32(colors);
		texture.Apply();
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x001007C8 File Offset: 0x000FE9C8
	public static Texture2D GetTexture(int width, int heigth, params Color[] colors)
	{
		Texture2D texture2D = new Texture2D(width, heigth, TextureFormat.RGBA32, false);
		texture2D.SetPixels(colors);
		texture2D.filterMode = FilterMode.Point;
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x001007F4 File Offset: 0x000FE9F4
	public static Color[] GetColors(Texture texture)
	{
		return ((Texture2D)texture).GetPixels();
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x00100804 File Offset: 0x000FEA04
	public static Texture GetTexture(Color[] colors)
	{
		int num = (int)Mathf.Sqrt((float)colors.Length);
		Texture2D texture2D = new Texture2D(num, num);
		texture2D.SetPixels(colors);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x00100834 File Offset: 0x000FEA34
	public static Color[] ColorSelect(Color[] colors, Color color, float contrast)
	{
		float num = color.r / color.g;
		float num2 = color.r / color.b;
		float num3 = color.r + color.g + color.b;
		for (int i = 0; i < colors.Length; i++)
		{
			Color color2 = colors[i];
			float num4 = num - color2.r / color2.g;
			float num5 = num2 - color2.r / color2.b;
			float num6 = num3 - (color2.r + color2.g + color2.b);
			if (num4 < 0f)
			{
				num4 = -num4;
			}
			if (num5 < 0f)
			{
				num5 = -num5;
			}
			if (num6 < 0f)
			{
				num6 = -num5;
			}
			float num7 = 1f - (num4 + num5 + num6) * contrast;
			colors[i] = new Color(num7, num7, num7, 1f);
		}
		return colors;
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x00100940 File Offset: 0x000FEB40
	public static Color[] GetMipMap(Color[] inColors, int level)
	{
		int num = 1 << level;
		int num2 = num * num;
		int num3 = (int)Mathf.Sqrt((float)inColors.Length);
		int num4 = num3 >> level;
		Color[] array = new Color[num4 * num4];
		float num5 = 1f / (float)num2;
		int i = 0;
		int num6 = -1;
		int num7 = -1;
		while (i < inColors.Length)
		{
			if (i % num == 0)
			{
				num6++;
				if (num6 % num4 == 0)
				{
					num7++;
					if (num7 % num != 0)
					{
						num6 -= num4;
					}
				}
			}
			Color[] array2 = array;
			int num8 = num6;
			array2[num8].r = array2[num8].r + inColors[i].r * num5;
			Color[] array3 = array;
			int num9 = num6;
			array3[num9].g = array3[num9].g + inColors[i].g * num5;
			Color[] array4 = array;
			int num10 = num6;
			array4[num10].b = array4[num10].b + inColors[i].b * num5;
			i++;
		}
		return array;
	}
}
