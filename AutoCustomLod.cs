using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000067 RID: 103
[AddComponentMenu("Scripts/Engine/Components/AutoCustomLod")]
internal class AutoCustomLod : MonoBehaviour
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000E288 File Offset: 0x0000C488
	private int Index
	{
		get
		{
			float magnitude;
			if (Application.isPlaying)
			{
				magnitude = (CameraListener.Camera.transform.position - base.transform.position).magnitude;
			}
			else
			{
				if (this.LODs == null)
				{
					this.Awake();
				}
				magnitude = (CameraListener.Camera.transform.position - base.transform.position).magnitude;
			}
			if (magnitude < CVars.g_lod1Distance * this.k1)
			{
				return 0;
			}
			if (magnitude > CVars.g_lod2Distance * this.k1)
			{
				return this.LODs.Length - 1;
			}
			return (int)((float)(this.LODs.Length - 1) * (magnitude - CVars.g_lod1Distance * this.k1) / (CVars.g_lod2Distance * this.k1 - CVars.g_lod1Distance * this.k1));
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000E374 File Offset: 0x0000C574
	private void Awake()
	{
		int num = 0;
		while (base.transform.FindChild("LOD" + num) != null)
		{
			base.transform.FindChild("LOD" + num).renderer.enabled = false;
			num++;
		}
		this.LODs = new Renderer[num];
		int num2 = 0;
		num = 0;
		while (base.transform.FindChild("LOD" + num) != null)
		{
			this.LODs[num2++] = base.transform.FindChild("LOD" + num).renderer;
			base.transform.FindChild("LOD" + num).renderer.enabled = false;
			num++;
		}
		this.Show();
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000E470 File Offset: 0x0000C670
	public void Hide()
	{
		base.StopAllCoroutines();
		for (int i = 0; i < this.LODs.Length; i++)
		{
			this.LODs[i].enabled = false;
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000E4AC File Offset: 0x0000C6AC
	public void Show()
	{
		if (this.locked || Peer.Dedicated)
		{
			return;
		}
		base.StopAllCoroutines();
		base.StartCoroutine(this.UpdateLods());
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
	public void ShowLQ()
	{
		if (this.locked || Peer.Dedicated)
		{
			return;
		}
		base.StopAllCoroutines();
		int num = this.LODs.Length - 1;
		for (int i = 0; i < this.LODs.Length; i++)
		{
			if (i == num)
			{
				this.LODs[i].enabled = true;
			}
			else
			{
				this.LODs[i].enabled = false;
			}
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000E550 File Offset: 0x0000C750
	private IEnumerator UpdateLods()
	{
		for (;;)
		{
			int index = this.Index;
			for (int i = 0; i < this.LODs.Length; i++)
			{
				if (i == index)
				{
					this.LODs[i].enabled = true;
				}
				else
				{
					this.LODs[i].enabled = false;
				}
			}
			yield return new WaitForSeconds(1f);
		}
		yield break;
	}

	// Token: 0x0400020B RID: 523
	public bool locked;

	// Token: 0x0400020C RID: 524
	public float k1 = 1f;

	// Token: 0x0400020D RID: 525
	private Renderer[] LODs;
}
