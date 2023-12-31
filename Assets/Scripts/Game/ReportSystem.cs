using System;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
	// Token: 0x020002E7 RID: 743
	internal class ReportSystem
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x000D9458 File Offset: 0x000D7658
		private ReportSystem()
		{
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x000D9478 File Offset: 0x000D7678
		public static ReportSystem Instance
		{
			get
			{
				ReportSystem result;
				if ((result = ReportSystem._instance) == null)
				{
					result = (ReportSystem._instance = new ReportSystem());
				}
				return result;
			}
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000D9494 File Offset: 0x000D7694
		public bool ClientAddSuspect(int suspectUid, int reporterUid, ReportType type)
		{
			if (!this._clientSuspects.ContainsKey(suspectUid))
			{
				this._clientSuspects.Add(suspectUid, new ReportSystem.SuspectData());
			}
			switch (type)
			{
			case ReportType.Cheating:
				if (this._clientSuspects[suspectUid].CheatReporters.Contains(reporterUid))
				{
					return false;
				}
				this._clientSuspects[suspectUid].CheatReporters.Add(reporterUid);
				break;
			case ReportType.BugUsage:
				if (this._clientSuspects[suspectUid].BugUsageReporters.Contains(reporterUid))
				{
					return false;
				}
				this._clientSuspects[suspectUid].BugUsageReporters.Add(reporterUid);
				break;
			case ReportType.Abuse:
				if (this._clientSuspects[suspectUid].AbuseReporters.Contains(reporterUid))
				{
					return false;
				}
				this._clientSuspects[suspectUid].AbuseReporters.Add(reporterUid);
				break;
			}
			foreach (EntityNetPlayer entityNetPlayer in Peer.ClientGame.AllPlayers)
			{
				if (entityNetPlayer.UserID == suspectUid)
				{
					EventFactory.Call("ChatMessage", new object[]
					{
						ChatInfo.gameflow_message,
						string.Empty,
						Language.SuspectedUser + " " + entityNetPlayer.Nick
					});
				}
			}
			return true;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x000D962C File Offset: 0x000D782C
		public bool ServerAddSuspect(int suspectUid, int reporterUid, ReportType type)
		{
			if (!this._serverSuspects.ContainsKey(suspectUid))
			{
				this._serverSuspects.Add(suspectUid, new ReportSystem.SuspectData());
			}
			switch (type)
			{
			case ReportType.Cheating:
				if (this._serverSuspects[suspectUid].CheatReporters.Contains(reporterUid))
				{
					return false;
				}
				this._serverSuspects[suspectUid].CheatReporters.Add(reporterUid);
				break;
			case ReportType.BugUsage:
				if (this._serverSuspects[suspectUid].BugUsageReporters.Contains(reporterUid))
				{
					return false;
				}
				this._serverSuspects[suspectUid].BugUsageReporters.Add(reporterUid);
				break;
			case ReportType.Abuse:
				if (this._serverSuspects[suspectUid].AbuseReporters.Contains(reporterUid))
				{
					return false;
				}
				this._serverSuspects[suspectUid].AbuseReporters.Add(reporterUid);
				break;
			}
			foreach (ServerNetPlayer serverNetPlayer in Peer.ServerGame.ServerNetPlayers)
			{
				if (serverNetPlayer.UserID == suspectUid)
				{
					this._serverSuspects[suspectUid].Stats = new SuspectStats(serverNetPlayer.Stats);
				}
			}
			return true;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000D97A8 File Offset: 0x000D79A8
		public void ClearReportList(int uid)
		{
			foreach (KeyValuePair<int, ReportSystem.SuspectData> keyValuePair in this._clientSuspects)
			{
				keyValuePair.Value.AbuseReporters.Remove(uid);
				keyValuePair.Value.BugUsageReporters.Remove(uid);
				keyValuePair.Value.CheatReporters.Remove(uid);
			}
			foreach (KeyValuePair<int, ReportSystem.SuspectData> keyValuePair2 in this._serverSuspects)
			{
				keyValuePair2.Value.AbuseReporters.Remove(uid);
				keyValuePair2.Value.BugUsageReporters.Remove(uid);
				keyValuePair2.Value.CheatReporters.Remove(uid);
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000D98C8 File Offset: 0x000D7AC8
		public void SaveSuspects()
		{
			Dictionary<int, object> dictionary = new Dictionary<int, object>();
			foreach (KeyValuePair<int, ReportSystem.SuspectData> keyValuePair in this._serverSuspects)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				keyValuePair.Value.Convert(dictionary2, true);
				dictionary.Add(keyValuePair.Key, dictionary2);
			}
			if (dictionary.Count == 0)
			{
				return;
			}
			string data = ArrayUtility.ToJSON<int, object>(dictionary);
			HtmlLayer.SendCompressed("adm.php?q=suspecter/report", data, delegate
			{
			}, delegate
			{
			});
		}

		// Token: 0x0400194F RID: 6479
		private static ReportSystem _instance;

		// Token: 0x04001950 RID: 6480
		private readonly Dictionary<int, ReportSystem.SuspectData> _clientSuspects = new Dictionary<int, ReportSystem.SuspectData>();

		// Token: 0x04001951 RID: 6481
		private readonly Dictionary<int, ReportSystem.SuspectData> _serverSuspects = new Dictionary<int, ReportSystem.SuspectData>();

		// Token: 0x020002E8 RID: 744
		private class SuspectData : Convertible
		{
			// Token: 0x060014A4 RID: 5284 RVA: 0x000D99E0 File Offset: 0x000D7BE0
			public void Convert(Dictionary<string, object> dict, bool isWrite)
			{
				dict.Add("cheat", this.CheatReporters);
				dict.Add("abuse", this.AbuseReporters);
				dict.Add("bug_usage", this.BugUsageReporters);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				this.Stats.Convert(dictionary, isWrite);
				dict.Add("stats", dictionary);
			}

			// Token: 0x04001954 RID: 6484
			public SuspectStats Stats;

			// Token: 0x04001955 RID: 6485
			public readonly List<int> CheatReporters = new List<int>();

			// Token: 0x04001956 RID: 6486
			public readonly List<int> AbuseReporters = new List<int>();

			// Token: 0x04001957 RID: 6487
			public readonly List<int> BugUsageReporters = new List<int>();
		}
	}
}
