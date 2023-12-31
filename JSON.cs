using System;
using System.Collections.Generic;
using System.Reflection;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x0200008C RID: 140
[Obfuscation(Exclude = true)]
public class JSON
{
	// Token: 0x06000308 RID: 776 RVA: 0x00015B30 File Offset: 0x00013D30
	public static bool IsValid(object test)
	{
		return test != null && !(test is object[]);
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00015B48 File Offset: 0x00013D48
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref bool[] obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2) && JSON.IsValid(obj2))
				{
					ArrayUtility.AdjustArray<bool>(ref obj, (bool[])obj2);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00015BD0 File Offset: 0x00013DD0
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref string[] obj, bool isWrite)
	{
		try
		{
			object obj2;
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else if (dict.TryGetValue(name, out obj2))
			{
				if (!(obj2 is object[]) || ((object[])obj2).Length != 0)
				{
					ArrayUtility.AdjustArray(ref obj, (string[])obj2);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00015C6C File Offset: 0x00013E6C
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref List<string> obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					if (!(obj2 is object[]) || (obj2 as object[]).Length != 0)
					{
						ArrayUtility.AdjustArray(ref obj, (string[])obj2);
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00015D04 File Offset: 0x00013F04
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref int[] obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					if (!(obj2 is object[]) || (obj2 as object[]).Length != 0)
					{
						ArrayUtility.AdjustArray<int>(ref obj, (int[])obj2);
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00015D9C File Offset: 0x00013F9C
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref ObscuredInt[] obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					if (!(obj2 is object[]) || (obj2 as object[]).Length != 0)
					{
						ArrayUtility.AdjustArray(ref obj, (int[])obj2);
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00015E34 File Offset: 0x00014034
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref float[] obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					if (!(obj2 is object[]))
					{
						if (!(obj2 is object[]) || (obj2 as object[]).Length != 0)
						{
							ArrayUtility.AdjustArray(ref obj, (double[])obj2);
						}
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00015EDC File Offset: 0x000140DC
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref List<float> obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					if (!(obj2 is object[]))
					{
						if (!(obj2 is object[]) || (obj2 as object[]).Length != 0)
						{
							ArrayUtility.AdjustArray(ref obj, (double[])obj2);
						}
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00015F84 File Offset: 0x00014184
	internal static void ReadWrite<T>(Dictionary<string, object> dict, string name, ref T[] obj, bool isWrite) where T : Convertible, new()
	{
		try
		{
			if (isWrite)
			{
				if (obj != null)
				{
					Dictionary<string, object>[] array = new Dictionary<string, object>[obj.Length];
					for (int i = 0; i < obj.Length; i++)
					{
						array[i] = new Dictionary<string, object>();
						obj[i].Write(array[i]);
					}
					dict.Add(name, array);
				}
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					if (!(obj2 is object[]) || (obj2 as object[]).Length != 0)
					{
						ArrayUtility.AdjustArray<T>(ref obj, (Dictionary<string, object>[])obj2);
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00016064 File Offset: 0x00014264
	public static void ReadWriteEnum<T>(Dictionary<string, object> dict, string name, ref T[] obj, bool isWrite) where T : struct, IConvertible
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					if (!(obj2 is object[]) || (obj2 as object[]).Length != 0)
					{
						ArrayUtility.AdjustEnumArray<T>(ref obj, (string[])obj2);
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000312 RID: 786 RVA: 0x000160FC File Offset: 0x000142FC
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref bool obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					obj = (bool)obj2;
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00016180 File Offset: 0x00014380
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref byte obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object value = obj;
				if (dict.TryGetValue(name, out value))
				{
					obj = Convert.ToByte(value);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00016204 File Offset: 0x00014404
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref short obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object value = obj;
				if (dict.TryGetValue(name, out value))
				{
					obj = Convert.ToInt16(value);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000315 RID: 789 RVA: 0x00016288 File Offset: 0x00014488
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref int obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object value = obj;
				if (dict.TryGetValue(name, out value))
				{
					obj = Convert.ToInt32(value);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0001630C File Offset: 0x0001450C
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref ObscuredInt obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object value = obj;
				if (dict.TryGetValue(name, out value))
				{
					obj = Convert.ToInt32(value);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x000163A8 File Offset: 0x000145A8
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref long obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, (int)obj);
			}
			else
			{
				object value = (int)obj;
				if (dict.TryGetValue(name, out value))
				{
					obj = Convert.ToInt64(value);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0001642C File Offset: 0x0001462C
	internal static void ReadWrite(Dictionary<string, object> dict, string name, ref Int obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = 0;
				if (dict.TryGetValue(name, out obj2))
				{
					if (obj == null)
					{
						obj = new Int((int)obj2);
					}
					else
					{
						obj = (int)obj2;
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000319 RID: 793 RVA: 0x000164D0 File Offset: 0x000146D0
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref float obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, (double)obj);
			}
			else
			{
				object value = (double)obj;
				if (dict.TryGetValue(name, out value))
				{
					obj = Convert.ToSingle(value);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00016554 File Offset: 0x00014754
	internal static void ReadWrite(Dictionary<string, object> dict, string name, ref Float obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, (double)obj.Value);
			}
			else
			{
				object value = 0.0;
				if (dict.TryGetValue(name, out value))
				{
					if (obj == null)
					{
						obj = new Float(Convert.ToSingle(value));
					}
					else
					{
						obj = Convert.ToSingle(value);
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00016600 File Offset: 0x00014800
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref string obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, obj);
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					obj = (string)obj2;
					if (obj == null)
					{
						obj = string.Empty;
					}
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name + " value = " + obj, innerException));
		}
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0001668C File Offset: 0x0001488C
	internal static void ReadWrite<T>(Dictionary<string, object> dict, string name, ref T obj, bool isWrite) where T : Convertible, new()
	{
		try
		{
			if (isWrite)
			{
				if (obj != null)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					obj.Write(dictionary);
					dict.Add(name, dictionary);
				}
			}
			else
			{
				object obj2 = obj;
				if (dict.TryGetValue(name, out obj2))
				{
					if (obj == null)
					{
						obj = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
					}
					obj.Read((Dictionary<string, object>)obj2);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600031D RID: 797 RVA: 0x00016780 File Offset: 0x00014980
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref KeyCode obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name, (int)obj);
			}
			else
			{
				object obj2 = (int)obj;
				if (dict.TryGetValue(name, out obj2))
				{
					obj = (KeyCode)((int)Enum.Parse(typeof(KeyCode), obj2.ToString()));
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0001682C File Offset: 0x00014A2C
	public static void ReadWrite(Dictionary<string, object> dict, string name, ref Vector3 obj, bool isWrite)
	{
		try
		{
			if (isWrite)
			{
				dict.Add(name + ".x", (double)obj.x);
				dict.Add(name + ".y", (double)obj.y);
				dict.Add(name + ".z", (double)obj.z);
			}
			else
			{
				object value = (double)obj.x;
				if (dict.TryGetValue(name + ".x", out value))
				{
					obj.x = Convert.ToSingle(value);
				}
				if (dict.TryGetValue(name + ".y", out value))
				{
					obj.y = Convert.ToSingle(value);
				}
				if (dict.TryGetValue(name + ".z", out value))
				{
					obj.z = Convert.ToSingle(value);
				}
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x00016954 File Offset: 0x00014B54
	public static void ReadWriteEnum<T>(Dictionary<string, object> dict, string name, ref T obj, bool isWrite) where T : struct, IConvertible
	{
		try
		{
			object obj2;
			if (isWrite)
			{
				dict.Add(name, (int)((object)obj));
			}
			else if (dict.TryGetValue(name, out obj2))
			{
				obj = (T)((object)Enum.Parse(typeof(T), obj2.ToString()));
			}
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Object Name: " + name, innerException));
		}
	}

	// Token: 0x06000320 RID: 800 RVA: 0x000169F4 File Offset: 0x00014BF4
	internal static void ConvertDict<T>(object obj, ref Dictionary<string, T> dict, Func<object, T> castMethod = null)
	{
		object[] array = obj as object[];
		if (array != null)
		{
			dict = new Dictionary<string, T>();
			if (array.Length == 0)
			{
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				T value = (castMethod != null) ? castMethod(array[i]) : ((T)((object)array[i]));
				dict.Add(i.ToString(), value);
			}
			return;
		}
		else
		{
			Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
			if (dictionary == null)
			{
				Debug.LogError("Can't Convert Dict");
				return;
			}
			dict = new Dictionary<string, T>(dictionary.Count);
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				T value2 = (castMethod != null) ? castMethod(keyValuePair.Value) : ((T)((object)keyValuePair.Value));
				dict.Add(keyValuePair.Key, value2);
			}
			return;
		}
	}
}
