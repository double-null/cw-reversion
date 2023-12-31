using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[Serializable]
internal class eNetworkView : PoolableBehaviour
{
	// Token: 0x17000079 RID: 121
	// (get) Token: 0x060003DB RID: 987 RVA: 0x0001AED0 File Offset: 0x000190D0
	public eObserved Observed
	{
		get
		{
			return this.observed as eObserved;
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060003DC RID: 988 RVA: 0x0001AEE0 File Offset: 0x000190E0
	// (set) Token: 0x060003DD RID: 989 RVA: 0x0001AEE8 File Offset: 0x000190E8
	public eNetworkPlayer owner
	{
		get
		{
			return this.player;
		}
		set
		{
			this.player = value;
			if (!this.player.IsVirtual)
			{
				this.cached_view = base.gameObject.AddComponent<NetworkView>();
			}
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060003DE RID: 990 RVA: 0x0001AF20 File Offset: 0x00019120
	public bool IsVirtual
	{
		get
		{
			return this.player.IsVirtual;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060003DF RID: 991 RVA: 0x0001AF30 File Offset: 0x00019130
	public NetworkView view
	{
		get
		{
			return this.cached_view;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001AF38 File Offset: 0x00019138
	// (set) Token: 0x060003E1 RID: 993 RVA: 0x0001AF58 File Offset: 0x00019158
	public NetworkViewID viewID
	{
		get
		{
			if (this.IsVirtual)
			{
				return this.cached_viewID;
			}
			return this.view.viewID;
		}
		set
		{
			if (this.IsVirtual)
			{
				this.cached_viewID = value;
			}
			else
			{
				this.view.viewID = value;
			}
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001AF80 File Offset: 0x00019180
	// (set) Token: 0x060003E3 RID: 995 RVA: 0x0001AFB0 File Offset: 0x000191B0
	public int group
	{
		get
		{
			if (this.player.IsVirtual)
			{
				return this.cached_group;
			}
			return this.cached_view.group;
		}
		set
		{
			if (this.player.IsVirtual)
			{
				this.cached_group = value;
			}
			else
			{
				this.cached_view.group = value;
			}
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060003E4 RID: 996 RVA: 0x0001AFE8 File Offset: 0x000191E8
	// (set) Token: 0x060003E5 RID: 997 RVA: 0x0001B008 File Offset: 0x00019208
	public NetworkStateSynchronization stateSynchronization
	{
		get
		{
			if (this.IsVirtual)
			{
				return this.cached_stateSynchronization;
			}
			return this.cached_view.stateSynchronization;
		}
		set
		{
			if (this.IsVirtual)
			{
				this.cached_stateSynchronization = value;
			}
			else
			{
				this.cached_view.stateSynchronization = value;
			}
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x060003E6 RID: 998 RVA: 0x0001B030 File Offset: 0x00019230
	// (set) Token: 0x060003E7 RID: 999 RVA: 0x0001B050 File Offset: 0x00019250
	public Component observed
	{
		get
		{
			if (this.IsVirtual)
			{
				return this.cached_observed;
			}
			return this.cached_view.observed;
		}
		set
		{
			if (this.IsVirtual)
			{
				this.cached_observed = value;
			}
			else
			{
				this.cached_view.observed = value;
			}
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001B078 File Offset: 0x00019278
	// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0001B098 File Offset: 0x00019298
	public bool isMine
	{
		get
		{
			if (this.IsVirtual)
			{
				return this.cached_isMine;
			}
			return this.view.isMine;
		}
		set
		{
			if (this.IsVirtual)
			{
				this.cached_isMine = value;
				this.OnEnable();
			}
		}
	}

	// Token: 0x17000082 RID: 130
	// (set) Token: 0x060003EA RID: 1002 RVA: 0x0001B0B8 File Offset: 0x000192B8
	public float sendRate
	{
		set
		{
			if (this.IsVirtual)
			{
				this.cached_sendRate = value;
			}
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001B0CC File Offset: 0x000192CC
	public eNetworkView Destiny
	{
		get
		{
			if (this.cached_destiny == null)
			{
				eNetworkView[] array = (eNetworkView[])UnityEngine.Object.FindObjectsOfType(typeof(eNetworkView));
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].viewID == this.viewID && array[i].isMine)
					{
						this.cached_destiny = array[i];
						break;
					}
				}
			}
			return this.cached_destiny;
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001B14C File Offset: 0x0001934C
	public void RPC(string name, NetworkViewID reciever, params object[] args)
	{
		if (this.IsVirtual)
		{
			base.StartCoroutine(this.RPC_loopback(name, reciever, args));
		}
		else
		{
			for (int i = 0; i < Network.connections.Length; i++)
			{
				if (Network.connections[i] == reciever.owner)
				{
					this.cached_view.RPC(name, reciever.owner, args);
				}
			}
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0001B1C8 File Offset: 0x000193C8
	private IEnumerator RPC_loopback(string name, NetworkViewID reciever, params object[] args)
	{
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(CVars.n_ping / 2f));
		try
		{
			MethodInfo method = this.Destiny.observed.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			method.Invoke(this.Destiny.observed, args);
			yield break;
		}
		catch (Exception ex)
		{
			Exception e = ex;
			global::Console.exception(new Exception("failed method: " + name, e));
		}
		Debug.LogWarning("No virtual recievers (" + name + ")");
		yield break;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0001B200 File Offset: 0x00019400
	public void Disable()
	{
		if (!this.IsVirtual)
		{
			for (int i = 0; i < Network.connections.Length; i++)
			{
				this.view.SetScope(Network.connections[i], false);
			}
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x0001B250 File Offset: 0x00019450
	public void Enable(NetworkPlayer reciever)
	{
		if (!this.IsVirtual)
		{
			for (int i = 0; i < Network.connections.Length; i++)
			{
				if (Network.connections[i] == this.owner.owner)
				{
					this.cached_view.SetScope(this.owner.owner, true);
				}
				if (Network.connections[i] == reciever)
				{
					this.cached_view.SetScope(reciever, true);
				}
			}
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0001B2E8 File Offset: 0x000194E8
	private ListStream Send(ListStream packet)
	{
		packet.timestamp = Network.time;
		this.packets.Add(packet);
		return packet;
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0001B304 File Offset: 0x00019504
	private IEnumerator SendLoop()
	{
		for (;;)
		{
			yield return base.StartCoroutine(this.WaitForRealTimeSeconds(1f / this.cached_sendRate));
			this.Observed.OnNetworkEvent(new eNetworkStream(this.Observed.GetUpdateType(), this.Send(new ListStream(false))));
		}
		yield break;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0001B320 File Offset: 0x00019520
	private ListStream Recieve()
	{
		for (int i = 0; i < this.Destiny.packets.Count; i++)
		{
			if (Network.time - this.Destiny.packets[i].timestamp >= (double)CVars.n_ping / 2.0)
			{
				ListStream listStream = this.Destiny.packets[i];
				listStream.isReading = true;
				this.Destiny.packets.RemoveAt(i);
				i = -1;
				if (CVars.n_lossage == 0f || UnityEngine.Random.value <= 1f - CVars.n_lossage)
				{
					return listStream;
				}
			}
		}
		return null;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0001B3D8 File Offset: 0x000195D8
	private IEnumerator RecieveLoop()
	{
		for (;;)
		{
			yield return null;
			while (this.Destiny == null)
			{
				yield return new WaitForSeconds(1f);
			}
			for (ListStream packet = this.Recieve(); packet != null; packet = this.Recieve())
			{
				this.Observed.OnNetworkEvent(new eNetworkStream(packet));
			}
		}
		yield break;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0001B3F4 File Offset: 0x000195F4
	private void OnEnable()
	{
		if (this.player != null && this.IsVirtual)
		{
			base.StopAllCoroutines();
			if (this.cached_isMine)
			{
				base.StartCoroutine(this.SendLoop());
			}
			else
			{
				base.StartCoroutine(this.RecieveLoop());
			}
		}
	}

	// Token: 0x040003B1 RID: 945
	private eNetworkPlayer player;

	// Token: 0x040003B2 RID: 946
	private NetworkView cached_view;

	// Token: 0x040003B3 RID: 947
	private eNetworkView cached_destiny;

	// Token: 0x040003B4 RID: 948
	private NetworkViewID cached_viewID;

	// Token: 0x040003B5 RID: 949
	private NetworkStateSynchronization cached_stateSynchronization;

	// Token: 0x040003B6 RID: 950
	private int cached_group;

	// Token: 0x040003B7 RID: 951
	private Component cached_observed;

	// Token: 0x040003B8 RID: 952
	private bool cached_isMine;

	// Token: 0x040003B9 RID: 953
	private float cached_sendRate;

	// Token: 0x040003BA RID: 954
	[NonSerialized]
	private List<ListStream> packets = new List<ListStream>();
}
