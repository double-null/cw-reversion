using System;
using System.Security.Cryptography;
using System.Text;

// Token: 0x02000379 RID: 889
internal class Crypt
{
	// Token: 0x06001CD7 RID: 7383 RVA: 0x000FF2B0 File Offset: 0x000FD4B0
	public static ulong Int32ToInt64(int buttonsH, int buttonsL)
	{
		return (ulong)buttonsH << 32 | (ulong)buttonsL;
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x000FF2CC File Offset: 0x000FD4CC
	public static void Int64ToInt32(ulong buttons, out int buttonsH, out int buttonsL)
	{
		buttonsH = (int)((uint)(buttons >> 32));
		buttonsL = (int)((uint)(buttons & (ulong)-1));
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x000FF2DC File Offset: 0x000FD4DC
	public static object ResolveVariable(object[] args, object Default, int n)
	{
		if (args.Length > n)
		{
			return args[n];
		}
		return Default;
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x000FF2EC File Offset: 0x000FD4EC
	public static string getMD5Hash(string input)
	{
		MD5 md = new MD5CryptoServiceProvider();
		byte[] array = md.ComputeHash(Encoding.UTF8.GetBytes(input));
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("x2"));
		}
		return stringBuilder.ToString();
	}
}
