using System;
using System.Collections.Generic;
using System.Reflection;
using cygwin_x32.ObscuredTypes;
using JsonFx.Json;
using UnityEngine;

// Token: 0x02000375 RID: 885
[Obfuscation(Exclude = true, ApplyToMembers = true)]
public class ArrayUtility
{
	// Token: 0x06001CB3 RID: 7347 RVA: 0x000FE2E0 File Offset: 0x000FC4E0
	public static string[] ToStringArray<T>(T[] array)
	{
		string[] array2 = new string[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i].ToString();
		}
		return array2;
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x000FE320 File Offset: 0x000FC520
	public static string[] ToStringArray<T>(List<T> array)
	{
		string[] array2 = new string[array.Count];
		for (int i = 0; i < array.Count; i++)
		{
			string[] array3 = array2;
			int num = i;
			T t = array[i];
			array3[num] = t.ToString();
		}
		return array2;
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x000FE36C File Offset: 0x000FC56C
	public static void RemoveAt<T>(ref T[] array, int index)
	{
		if (index >= array.Length || array.Length == 0)
		{
			return;
		}
		T[] array2 = new T[array.Length - 1];
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (i != index)
			{
				array2[num++] = array[i];
			}
		}
		array = array2;
	}

	// Token: 0x06001CB6 RID: 7350 RVA: 0x000FE3D4 File Offset: 0x000FC5D4
	public static void Add<T>(ref T[] array, T v)
	{
		T[] array2 = new T[array.Length + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		array2[array.Length] = v;
		array = array2;
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x000FE420 File Offset: 0x000FC620
	public static void AdjustArray(ref List<float> array, double[] copy)
	{
		if (array == null)
		{
			array.Clear();
			for (int i = 0; i < copy.Length; i++)
			{
				array.Add((float)copy[i]);
			}
		}
		else if (array.Count != copy.Length)
		{
			float[] array2 = new float[Math.Max(array.Count, copy.Length)];
			for (int j = 0; j < array2.Length; j++)
			{
				if (copy.Length > j)
				{
					array2[j] = (float)copy[j];
				}
				else
				{
					array2[j] = 0f;
				}
			}
			array = new List<float>(array2);
		}
		else
		{
			for (int k = 0; k < array.Count; k++)
			{
				array[k] = (float)copy[k];
			}
		}
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x000FE4E8 File Offset: 0x000FC6E8
	public static void AdjustArray(ref List<string> array, string[] copy)
	{
		if (array == null)
		{
			array.Clear();
			for (int i = 0; i < copy.Length; i++)
			{
				array.Add(copy[i]);
			}
		}
		else if (array.Count != copy.Length)
		{
			string[] array2 = new string[Math.Max(array.Count, copy.Length)];
			for (int j = 0; j < array2.Length; j++)
			{
				if (copy.Length > j)
				{
					array2[j] = copy[j];
				}
				else
				{
					array2[j] = null;
				}
			}
			array = new List<string>(array2);
		}
		else
		{
			for (int k = 0; k < array.Count; k++)
			{
				array[k] = copy[k];
			}
		}
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x000FE5A8 File Offset: 0x000FC7A8
	public static void AdjustArray(ref float[] array, double[] copy)
	{
		if (array == null)
		{
			array = new float[copy.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (float)copy[i];
			}
		}
		else if (array.Length != copy.Length)
		{
			float[] array2 = new float[Math.Max(array.Length, copy.Length)];
			for (int j = 0; j < array2.Length; j++)
			{
				if (copy.Length > j)
				{
					array2[j] = (float)copy[j];
				}
				else
				{
					array2[j] = 0f;
				}
			}
			array = array2;
		}
		else
		{
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = (float)copy[k];
			}
		}
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x000FE65C File Offset: 0x000FC85C
	public static void AdjustEnumArray<T>(ref T[] array, string[] copy) where T : struct
	{
		if (array == null)
		{
			array = new T[copy.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)((object)Enum.Parse(typeof(T), copy[i]));
			}
		}
		else if (array.Length != copy.Length)
		{
			T[] array2 = new T[Math.Max(array.Length, copy.Length)];
			for (int j = 0; j < array2.Length; j++)
			{
				if (copy.Length > j)
				{
					array2[j] = (T)((object)Enum.Parse(typeof(T), copy[j]));
				}
				else
				{
					array2[j] = default(T);
				}
			}
			array = array2;
		}
		else
		{
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = (T)((object)Enum.Parse(typeof(T), copy[k]));
			}
		}
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x000FE760 File Offset: 0x000FC960
	public static void AdjustEnumArray<T>(ref T[] array, int[] copy) where T : struct
	{
		if (array == null)
		{
			array = new T[copy.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)((object)copy[i]);
			}
		}
		else if (array.Length != copy.Length)
		{
			T[] array2 = new T[Math.Max(array.Length, copy.Length)];
			for (int j = 0; j < array2.Length; j++)
			{
				if (copy.Length > j)
				{
					array2[j] = (T)((object)copy[j]);
				}
				else
				{
					array2[j] = default(T);
				}
			}
			array = array2;
		}
		else
		{
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = (T)((object)copy[k]);
			}
		}
	}

	// Token: 0x06001CBC RID: 7356 RVA: 0x000FE844 File Offset: 0x000FCA44
	public static void AdjustArray<T>(ref T[] array, T[] copy) where T : struct
	{
		if (array == null)
		{
			array = copy;
		}
		else if (array.Length != copy.Length)
		{
			T[] array2 = new T[Math.Max(array.Length, copy.Length)];
			for (int i = 0; i < array2.Length; i++)
			{
				if (copy.Length > i)
				{
					array2[i] = copy[i];
				}
				else
				{
					array2[i] = default(T);
				}
			}
			array = array2;
		}
		else
		{
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = copy[j];
			}
		}
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x000FE8EC File Offset: 0x000FCAEC
	public static void AdjustArray(ref ObscuredInt[] array, int[] copy)
	{
		if (array.Length != copy.Length)
		{
			ObscuredInt[] array2 = new ObscuredInt[Math.Max(array.Length, copy.Length)];
			for (int i = 0; i < array2.Length; i++)
			{
				if (copy.Length > i)
				{
					array2[i] = copy[i];
				}
				else
				{
					array2[i] = default(ObscuredInt);
				}
			}
			array = array2;
		}
		else
		{
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = copy[j];
			}
		}
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x000FE994 File Offset: 0x000FCB94
	public static void AdjustArray(ref string[] array, string[] copy)
	{
		if (array == null)
		{
			array = copy;
		}
		else if (array.Length != copy.Length)
		{
			string[] array2 = new string[Math.Max(array.Length, copy.Length)];
			for (int i = 0; i < array2.Length; i++)
			{
				if (copy.Length > i)
				{
					array2[i] = copy[i];
				}
				else
				{
					array2[i] = null;
				}
			}
			array = array2;
		}
		else
		{
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = copy[j];
			}
		}
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x000FEA20 File Offset: 0x000FCC20
	internal static void AdjustArray<T>(ref T[] array, Dictionary<string, object>[] dict) where T : Convertible, new()
	{
		if (array == null)
		{
			array = new T[dict.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
				array[i].Read(dict[i]);
			}
		}
		else if (array.Length != dict.Length)
		{
			T[] array2 = new T[Math.Max(array.Length, dict.Length)];
			for (int j = 0; j < array2.Length; j++)
			{
				if (array.Length > j && array[j] != null)
				{
					array2[j] = array[j];
				}
				else
				{
					array2[j] = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
				}
				if (dict.Length > j)
				{
					array2[j].Read(dict[j]);
				}
			}
			array = array2;
		}
		else
		{
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k] == null)
				{
					array[k] = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
				}
				array[k].Read(dict[k]);
			}
		}
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x000FEBB8 File Offset: 0x000FCDB8
	public static void AdjustArraySize<T>(ref T[] array, int Length) where T : new()
	{
		if (array == null)
		{
			array = new T[Length];
		}
		else if (array.Length != Length)
		{
			T[] array2 = new T[Length];
			for (int i = 0; i < Math.Min(array.Length, Length); i++)
			{
				array2[i] = array[i];
			}
			array = array2;
		}
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x000FEC1C File Offset: 0x000FCE1C
	public static Dictionary<string, object> FromJSON(string text, string name = "")
	{
		Dictionary<string, object> result;
		try
		{
			if (name == string.Empty)
			{
				result = (Dictionary<string, object>)JsonReader.Deserialize(text);
			}
			else
			{
				result = (Dictionary<string, object>)((Dictionary<string, object>)JsonReader.Deserialize(text))[name];
			}
		}
		catch (Exception innerException)
		{
			throw new Exception("NAME = " + ((name != null) ? name : "NULL"), innerException);
		}
		return result;
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x000FECB0 File Offset: 0x000FCEB0
	public static Dictionary<string, object>[] ArrayFromJSON(string text, string name)
	{
		return (Dictionary<string, object>[])((Dictionary<string, object>)JsonReader.Deserialize(text))[name];
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x000FECC8 File Offset: 0x000FCEC8
	public static string ToJSON<T, V>(Dictionary<T, V> dict)
	{
		return JsonWriter.Serialize(dict);
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x000FECD0 File Offset: 0x000FCED0
	internal static void SerializeList<T>(eNetworkStream stream, List<T> list) where T : cwID, cwEntityType, cwNetworkSerializable
	{
		short num = (short)list.Count;
		stream.Serialize(ref num);
		for (int i = 0; i < list.Count; i++)
		{
			T t = list[i];
			int num2 = t.ID;
			stream.Serialize(ref num2);
			T t2 = list[i];
			EntityType entityType = t2.EntityType;
			stream.Serialize<EntityType>(ref entityType);
			T t3 = list[i];
			t3.Serialize(stream);
		}
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x000FED60 File Offset: 0x000FCF60
	internal static void DeserializeList<T>(eNetworkStream stream, List<T> list, CreateForSyncList<T> New) where T : Component, cwID, cwEntityType, cwNetworkSerializable
	{
		short num = 0;
		stream.Serialize(ref num);
		List<int> list2 = new List<int>();
		for (int i = 0; i < (int)num; i++)
		{
			int num2 = 0;
			stream.Serialize(ref num2);
			EntityType type = EntityType.none;
			stream.Serialize<EntityType>(ref type);
			bool flag = false;
			for (int j = 0; j < list.Count; j++)
			{
				T t = list[j];
				if (t.ID == num2)
				{
					T t2 = list[j];
					t2.Deserialize(stream);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				T item = New(num2, type);
				item.Deserialize(stream);
				list.Add(item);
			}
			list2.Add(num2);
		}
		for (int k = 0; k < list.Count; k++)
		{
			T t3 = list[k];
			if (!EntityNetPlayer.IsClientPlayer(t3.ID))
			{
				bool flag2 = false;
				for (int l = 0; l < list2.Count; l++)
				{
					T t4 = list[k];
					if (t4.ID == list2[l])
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					PoolManager instance = SingletoneForm<PoolManager>.Instance;
					T t5 = list[k];
					Pool pool = instance[t5.name];
					T t6 = list[k];
					pool.Despawn(t6.GetComponent<PoolItem>());
					list.RemoveAt(k);
					k = -1;
				}
			}
		}
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x000FEF18 File Offset: 0x000FD118
	internal static void Interpolate<T>(ClassArray<T> times, out T child, out T father, float interpolateTime, out float k) where T : class, ReusableClass<T>, cwTime, new()
	{
		father = times[times.Length - 1];
		child = times[times.Length - 1];
		k = 1f;
		if (times.Length < 2)
		{
			return;
		}
		for (int i = 1; i < times.Length; i++)
		{
			T t = times[i];
			if (t.Time >= interpolateTime)
			{
				T t2 = times[i - 1];
				if (t2.Time <= interpolateTime)
				{
					father = times[i];
					child = times[i - 1];
					int index = i;
					int index2 = i - 1;
					T t3 = times[index2];
					float num = interpolateTime - t3.Time;
					T t4 = times[index];
					float time = t4.Time;
					T t5 = times[index2];
					k = num / (time - t5.Time);
					if (!float.IsNaN(k))
					{
						T t6 = times[index];
						float time2 = t6.Time;
						T t7 = times[index2];
						if (time2 != t7.Time)
						{
							return;
						}
					}
					k = 1f;
					return;
				}
			}
		}
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x000FF064 File Offset: 0x000FD264
	public static int IndexOf<T>(List<T> list, T t)
	{
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			if (list[i] == t)
			{
				return i;
			}
		}
		return -1;
	}
}
