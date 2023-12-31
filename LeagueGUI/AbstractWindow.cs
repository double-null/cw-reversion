using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200030E RID: 782
	public abstract class AbstractWindow : IWindow
	{
		// Token: 0x06001AAB RID: 6827 RVA: 0x000F1BE8 File Offset: 0x000EFDE8
		public virtual void SetRect(Rect rect)
		{
			this.rect = rect;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000F1BF4 File Offset: 0x000EFDF4
		public virtual void OnDrawWindow()
		{
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000F1BF8 File Offset: 0x000EFDF8
		public virtual void OnStart()
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000F1BFC File Offset: 0x000EFDFC
		public virtual void OnGUI()
		{
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x000F1C00 File Offset: 0x000EFE00
		public virtual void OnUpdate()
		{
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000F1C04 File Offset: 0x000EFE04
		public virtual void OnReadyGame()
		{
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000F1C08 File Offset: 0x000EFE08
		public virtual void OnJoinGame()
		{
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000F1C0C File Offset: 0x000EFE0C
		public virtual void OnCancelGame()
		{
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000F1C10 File Offset: 0x000EFE10
		public virtual void OnWaitingPlayers()
		{
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000F1C14 File Offset: 0x000EFE14
		public virtual void OnMapLoading()
		{
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000F1C18 File Offset: 0x000EFE18
		public virtual void OnMatchStarting()
		{
		}

		// Token: 0x04001FCC RID: 8140
		protected Rect rect;
	}
}
