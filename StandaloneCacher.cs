using System;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x0200009B RID: 155
internal static class StandaloneCacher
{
	// Token: 0x17000067 RID: 103
	// (get) Token: 0x0600037C RID: 892 RVA: 0x0001943C File Offset: 0x0001763C
	internal static bool ShouldUseCache
	{
		get
		{
			return CVars.IsStandaloneRealm && Main.ExternalContent;
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00019450 File Offset: 0x00017650
	internal static AssetBundleCreateRequest GetCreateRequest(string url, int version)
	{
		if (!Directory.Exists(StandaloneCacher.CachePath))
		{
			Directory.CreateDirectory(StandaloneCacher.CachePath);
			return null;
		}
		string fileName = Path.GetFileName(url);
		string[] files = Directory.GetFiles(StandaloneCacher.CachePath);
		if (!files.Any((string file) => Path.GetFileName(file) == fileName))
		{
			return null;
		}
		byte[] array = null;
		string path = StandaloneCacher.CachePath + fileName;
		if (File.Exists(path))
		{
			array = File.ReadAllBytes(path);
		}
		if (array == null)
		{
			Debug.LogWarning("cant cache " + fileName + ", from " + url);
			return null;
		}
		byte[] array2 = new byte[4];
		Array.Copy(array, array2, 4);
		int num = BitConverter.ToInt32(array2, 0);
		if (version != num)
		{
			return null;
		}
		int num2 = array.Length - 4;
		byte[] array3 = new byte[num2];
		Array.Copy(array, 4, array3, 0, num2);
		return AssetBundle.CreateFromMemory(array3);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00019548 File Offset: 0x00017748
	internal static void Cache(byte[] bytes, string url, int version)
	{
		if (!Directory.Exists(StandaloneCacher.CachePath))
		{
			Directory.CreateDirectory(StandaloneCacher.CachePath);
		}
		byte[] array = BitConverter.GetBytes(version);
		array = array.Concat(bytes).ToArray<byte>();
		string path = StandaloneCacher.CachePath + Path.GetFileName(url);
		using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create)))
		{
			binaryWriter.Write(array);
		}
	}

	// Token: 0x04000399 RID: 921
	private static readonly string CachePath = Application.dataPath + "/../Cache/";
}
