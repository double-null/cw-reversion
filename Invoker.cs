using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000089 RID: 137
[Serializable]
internal class Invoker : Convertible, ReusableClass<Invoker>
{
	// Token: 0x060002F7 RID: 759 RVA: 0x000157B4 File Offset: 0x000139B4
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		this.state.Convert(dict, isWrite);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x000157C4 File Offset: 0x000139C4
	public void Init(MonoBehaviour mono)
	{
		this.mono = mono;
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x000157D0 File Offset: 0x000139D0
	public void Tick(float dt)
	{
		for (int i = 0; i < this.state.keys.Length; i++)
		{
			this.state.keys[i].elapsed -= dt;
		}
		for (int j = 0; j < this.state.keys.Length; j++)
		{
			if (this.state.keys[j].elapsed < 0f)
			{
				if (this.mono)
				{
					string name = this.state.keys[j].name;
					MethodInfo method = this.mono.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					method.Invoke(this.mono, null);
				}
				this.state.keys.RemoveAt(j);
				j = -1;
			}
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x000158C0 File Offset: 0x00013AC0
	public void Invoke(string name, float time, bool cancel = false)
	{
		if (cancel)
		{
			this.CancelInvoke(name);
		}
		eCache.InvokerStateKey.Clear();
		eCache.InvokerStateKey.name = name;
		eCache.InvokerStateKey.elapsed = time;
		this.state.keys.Add(eCache.InvokerStateKey);
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00015910 File Offset: 0x00013B10
	public bool IsInvoking(string name)
	{
		for (int i = 0; i < this.state.keys.Length; i++)
		{
			if (this.state.keys[i].name == name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00015964 File Offset: 0x00013B64
	public float InvokeTime(string name)
	{
		for (int i = 0; i < this.state.keys.Length; i++)
		{
			if (this.state.keys[i].name == name)
			{
				return this.state.keys[i].elapsed;
			}
		}
		return -1f;
	}

	// Token: 0x060002FD RID: 765 RVA: 0x000159D0 File Offset: 0x00013BD0
	public void CancelInvoke()
	{
		this.state.keys.Clear();
	}

	// Token: 0x060002FE RID: 766 RVA: 0x000159E4 File Offset: 0x00013BE4
	public void CancelInvoke(string name)
	{
		for (int i = 0; i < this.state.keys.Length; i++)
		{
			if (this.state.keys[i].name == name)
			{
				this.state.keys.RemoveAt(i);
				i = -1;
			}
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00015A4C File Offset: 0x00013C4C
	public void ListOfInvocation()
	{
		for (int i = 0; i < this.state.keys.Length; i++)
		{
			Debug.Log(this.state.keys[i].name);
		}
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00015A98 File Offset: 0x00013C98
	public void Clone(Invoker i)
	{
		this.state.Clone(i.state);
		this.mono = i.mono;
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00015AB8 File Offset: 0x00013CB8
	public void Clear()
	{
		this.state.Clear();
		this.mono = null;
	}

	// Token: 0x04000356 RID: 854
	public InvokerState state = new InvokerState();

	// Token: 0x04000357 RID: 855
	public MonoBehaviour mono;
}
