using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000070 RID: 112
[AddComponentMenu("Scripts/Engine/EventFactory")]
internal class EventFactory : SingletoneComponent<EventFactory>
{
	// Token: 0x060001F2 RID: 498 RVA: 0x000113E4 File Offset: 0x0000F5E4
	public static void Register(string name, PoolableBehaviour invoker)
	{
		if (SingletoneComponent<EventFactory>.Instance._names.IndexOf(name) == -1)
		{
			SingletoneComponent<EventFactory>.Instance._objects.Add(invoker);
			SingletoneComponent<EventFactory>.Instance._names.Add(name);
		}
		else
		{
			SingletoneComponent<EventFactory>.Instance._objects[SingletoneComponent<EventFactory>.Instance._names.IndexOf(name)] = invoker;
		}
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0001144C File Offset: 0x0000F64C
	public static void Call(string name, object values = null)
	{
		if (SingletoneComponent<EventFactory>.Instance._objects.Count == 0)
		{
			Debug.LogError("no no no???");
			Debug.Break();
		}
		int num = SingletoneComponent<EventFactory>.Instance._names.IndexOf(name);
		if (num != -1)
		{
			MethodInfo method = SingletoneComponent<EventFactory>.Instance._objects[num].GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			try
			{
				method.Invoke(SingletoneComponent<EventFactory>.Instance._objects[num], new object[]
				{
					values
				});
			}
			catch (Exception e)
			{
				global::Console.exception(e);
			}
		}
		else if (!Peer.Dedicated)
		{
			Debug.LogWarning("Function not found: " + name);
		}
	}

	// Token: 0x04000269 RID: 617
	private List<string> _names = new List<string>();

	// Token: 0x0400026A RID: 618
	private List<PoolableBehaviour> _objects = new List<PoolableBehaviour>();
}
