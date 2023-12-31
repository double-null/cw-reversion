using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200039C RID: 924
public static class PerformanceMeter
{
	// Token: 0x06001D7D RID: 7549 RVA: 0x00102EDC File Offset: 0x001010DC
	static PerformanceMeter()
	{
		PerformanceMeter.OnFPSChange = delegate()
		{
		};
		StartData.UpdateForStatic += PerformanceMeter.Update;
		PerformanceMeter.Update();
	}

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06001D7E RID: 7550 RVA: 0x00102F70 File Offset: 0x00101170
	// (remove) Token: 0x06001D7F RID: 7551 RVA: 0x00102F88 File Offset: 0x00101188
	public static event Action OnFPSChange;

	// Token: 0x06001D80 RID: 7552 RVA: 0x00102FA0 File Offset: 0x001011A0
	private static void AddLog(string n, object t)
	{
		PerformanceMeter._lines.Add(n, t.ToString());
	}

	// Token: 0x06001D81 RID: 7553 RVA: 0x00102FB4 File Offset: 0x001011B4
	public static Dictionary<string, string> GetSystemInfo()
	{
		PerformanceMeter.AddLog("deviceModel", SystemInfo.deviceModel);
		PerformanceMeter.AddLog("deviceType", SystemInfo.deviceType);
		PerformanceMeter.AddLog("graphicsDeviceName", SystemInfo.graphicsDeviceName);
		PerformanceMeter.AddLog("graphicsDeviceVendor", SystemInfo.graphicsDeviceVendor);
		PerformanceMeter.AddLog("graphicsDeviceVersion", SystemInfo.graphicsDeviceVersion);
		PerformanceMeter.AddLog("graphicsMemorySize", SystemInfo.graphicsMemorySize);
		PerformanceMeter.AddLog("graphicsPixelFillrate", SystemInfo.graphicsPixelFillrate);
		PerformanceMeter.AddLog("graphicsShaderLevel", SystemInfo.graphicsShaderLevel);
		PerformanceMeter.AddLog("maxTextureSize", SystemInfo.maxTextureSize);
		PerformanceMeter.AddLog("processorCount", SystemInfo.processorCount);
		PerformanceMeter.AddLog("processorType", SystemInfo.processorType);
		PerformanceMeter.AddLog("supportedRenderTargetCount", SystemInfo.supportedRenderTargetCount);
		PerformanceMeter.AddLog("supports3DTextures", SystemInfo.supports3DTextures);
		PerformanceMeter.AddLog("supportsImageEffects", SystemInfo.supportsImageEffects);
		PerformanceMeter.AddLog("supportsRenderTextures", SystemInfo.supportsRenderTextures);
		PerformanceMeter.AddLog("supportsShadows", SystemInfo.supportsShadows);
		PerformanceMeter.AddLog("supportsVertexPrograms", SystemInfo.supportsVertexPrograms);
		PerformanceMeter.AddLog("systemMemorySize", SystemInfo.systemMemorySize);
		return PerformanceMeter._lines;
	}

	// Token: 0x1700084F RID: 2127
	// (get) Token: 0x06001D82 RID: 7554 RVA: 0x00103118 File Offset: 0x00101318
	// (set) Token: 0x06001D83 RID: 7555 RVA: 0x00103120 File Offset: 0x00101320
	public static PerformanceMeter.PerfRating PerformanceRating { get; private set; }

	// Token: 0x06001D84 RID: 7556 RVA: 0x00103128 File Offset: 0x00101328
	private static void Update()
	{
		if (Time.time < PerformanceMeter._nextUpdateTime)
		{
			return;
		}
		PerformanceMeter._nextUpdateTime = Time.time + 0.25f;
		PerformanceMeter._fpsSum.AddValue(1f / Time.deltaTime);
		float num = PerformanceMeter._fpsSum.Sum / (float)PerformanceMeter._fpsSum.CurrentLength;
		int num2 = (int)PerformanceMeter.PerformanceRating;
		int num3 = num2;
		float num4 = PerformanceMeter._fpsMin[num2];
		float num5 = PerformanceMeter._fpsMax[num2];
		if (num < num4)
		{
			num2--;
		}
		else if (num > num5)
		{
			num2++;
		}
		if (num3 != num2)
		{
			if (num2 > 0 && num2 < 7)
			{
				PerformanceMeter.PerformanceRating = (PerformanceMeter.PerfRating)num2;
			}
			PerformanceMeter.OnFPSChange();
		}
	}

	// Token: 0x04002232 RID: 8754
	private static Dictionary<string, string> _lines = new Dictionary<string, string>();

	// Token: 0x04002233 RID: 8755
	private static float _nextUpdateTime = float.MinValue;

	// Token: 0x04002234 RID: 8756
	private static SumArray _fpsSum = new SumArray(30);

	// Token: 0x04002235 RID: 8757
	private static float[] _fpsMin = new float[]
	{
		-9999f,
		7f,
		18f,
		28f,
		40f,
		70f,
		130f
	};

	// Token: 0x04002236 RID: 8758
	private static float[] _fpsMax = new float[]
	{
		12f,
		22f,
		32f,
		50f,
		90f,
		170f,
		999999f
	};

	// Token: 0x0200039D RID: 925
	public enum PerfRating
	{
		// Token: 0x0400223B RID: 8763
		FPS4,
		// Token: 0x0400223C RID: 8764
		FPS16,
		// Token: 0x0400223D RID: 8765
		FPS24,
		// Token: 0x0400223E RID: 8766
		FPS36,
		// Token: 0x0400223F RID: 8767
		FPS60,
		// Token: 0x04002240 RID: 8768
		FPS100,
		// Token: 0x04002241 RID: 8769
		FPS200
	}
}
