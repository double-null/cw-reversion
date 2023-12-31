using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034F RID: 847
internal class MasteringInfo
{
	// Token: 0x06001C35 RID: 7221 RVA: 0x000FB7A4 File Offset: 0x000F99A4
	public void Initialize(string json)
	{
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(json, string.Empty);
		this.WearStates = new Dictionary<int, object>
		{
			{
				0,
				null
			}
		};
		this.Camouflages = new Dictionary<int, int>
		{
			{
				0,
				0
			},
			{
				1,
				0
			},
			{
				2,
				0
			},
			{
				3,
				0
			},
			{
				4,
				0
			}
		};
		this.ExpToNextPoint = Globals.I.MasteringExpToNextPoint;
		object obj;
		if (dictionary.TryGetValue("mp", out obj))
		{
			this.MasteringPoints = int.Parse((string)obj);
		}
		object obj2;
		if (dictionary.TryGetValue("mp_exp", out obj2))
		{
			this.CurrentExp = int.Parse((string)obj2);
			Main.UserInfo.CurrentMpExp = this.CurrentExp;
		}
		object obj3;
		if (dictionary.TryGetValue("mod_states", out obj3))
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj3;
			this.WeaponsStats = new Dictionary<int, MasteringInfo.MasteringWeaponStats>();
			foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
			{
				this.WeaponsStats.Add(int.Parse(keyValuePair.Key), new MasteringInfo.MasteringWeaponStats((Dictionary<string, object>)keyValuePair.Value));
			}
		}
		object wearStates;
		if (dictionary.TryGetValue("wear_states", out wearStates))
		{
			this.InitWearStates(wearStates);
		}
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x000FB930 File Offset: 0x000F9B30
	private void InitWearStates(object wearStates)
	{
		Dictionary<string, object> dictionary = (Dictionary<string, object>)wearStates;
		if (dictionary == null)
		{
			return;
		}
		object obj;
		if (dictionary.TryGetValue("camos", out obj))
		{
			foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)obj))
			{
				this.WearStates.Add(int.Parse(keyValuePair.Key), keyValuePair.Value);
			}
		}
		object obj2;
		if (dictionary.TryGetValue("wearing", out obj2))
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj2;
			if (dictionary2.TryGetValue("camos", out obj))
			{
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)obj;
				foreach (KeyValuePair<string, object> keyValuePair2 in dictionary3)
				{
					string[] array = keyValuePair2.Key.Split(new char[]
					{
						'_'
					});
					if (this.Camouflages.ContainsKey(int.Parse(array[1])))
					{
						this.Camouflages[int.Parse(array[1])] = int.Parse(keyValuePair2.Value.ToString());
					}
				}
			}
		}
	}

	// Token: 0x040020F9 RID: 8441
	public static int BaseCamoIndex = 60;

	// Token: 0x040020FA RID: 8442
	public int MasteringPoints;

	// Token: 0x040020FB RID: 8443
	public int CurrentExp;

	// Token: 0x040020FC RID: 8444
	public int ExpToNextPoint;

	// Token: 0x040020FD RID: 8445
	public Dictionary<int, MasteringInfo.MasteringWeaponStats> WeaponsStats;

	// Token: 0x040020FE RID: 8446
	public Dictionary<int, object> WearStates;

	// Token: 0x040020FF RID: 8447
	public Dictionary<int, int> Camouflages;

	// Token: 0x02000350 RID: 848
	internal class MasteringWeaponStats
	{
		// Token: 0x06001C37 RID: 7223 RVA: 0x000FBAA8 File Offset: 0x000F9CA8
		public MasteringWeaponStats(Dictionary<string, object> dict)
		{
			this.Initialize(dict);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000FBAB8 File Offset: 0x000F9CB8
		public bool MetaUnlocked(int meta)
		{
			return this._unlocked.ContainsKey(meta);
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x000FBAC8 File Offset: 0x000F9CC8
		public bool WtaskMetaUnlocked
		{
			get
			{
				return this._unlocked.ContainsKey(0);
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000FBAD8 File Offset: 0x000F9CD8
		public bool ModUnlocked(int level, int index)
		{
			return this._unlocked.ContainsKey(level) && this._unlocked[level].ContainsKey(index.ToString()) && (bool)this._unlocked[level][index.ToString()];
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000FBB34 File Offset: 0x000F9D34
		public bool MetaInProgress(int meta, bool premium = false)
		{
			if (premium && !this._unlocked.ContainsKey(0))
			{
				return meta == this._unlocked.Count;
			}
			return meta == this._unlocked.Count - 1;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000FBB78 File Offset: 0x000F9D78
		public void UnlockWtaskMeta()
		{
			if (this.WtaskMetaUnlocked)
			{
				return;
			}
			this._unlocked.Add(0, new Dictionary<string, object>
			{
				{
					"1",
					true
				},
				{
					"2",
					true
				},
				{
					"3",
					true
				},
				{
					"4",
					true
				}
			});
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000FBBE8 File Offset: 0x000F9DE8
		public void Initialize(Dictionary<string, object> dict)
		{
			object obj;
			if (dict.TryGetValue("total_exp", out obj))
			{
				this.WeaponExp = (int)obj;
			}
			object obj2;
			if (dict.TryGetValue("exp", out obj2))
			{
				this.MetaExp = (int)obj2;
			}
			object obj3;
			if (dict.TryGetValue("meta", out obj3))
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)obj3;
				this._unlocked = new Dictionary<int, Dictionary<string, object>>();
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					this._unlocked.Add(int.Parse(keyValuePair.Key), (Dictionary<string, object>)keyValuePair.Value);
				}
			}
			object obj4;
			if (dict.TryGetValue("camo", out obj4))
			{
				this.Camouflages = new List<int>
				{
					MasteringInfo.BaseCamoIndex
				};
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj4;
				foreach (KeyValuePair<string, object> keyValuePair2 in dictionary2)
				{
					int item = int.Parse(keyValuePair2.Key);
					if (!this.Camouflages.Contains(item))
					{
						this.Camouflages.Add(item);
					}
				}
			}
		}

		// Token: 0x04002100 RID: 8448
		public int WeaponExp;

		// Token: 0x04002101 RID: 8449
		public int MetaExp;

		// Token: 0x04002102 RID: 8450
		public Texture BaseCamouflage;

		// Token: 0x04002103 RID: 8451
		private Dictionary<int, Dictionary<string, object>> _unlocked;

		// Token: 0x04002104 RID: 8452
		public List<int> Camouflages;
	}
}
