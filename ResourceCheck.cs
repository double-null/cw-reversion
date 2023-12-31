using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000BA RID: 186
internal class ResourceCheck : MonoBehaviour
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060004E9 RID: 1257 RVA: 0x00020438 File Offset: 0x0001E638
	// (remove) Token: 0x060004EA RID: 1258 RVA: 0x00020450 File Offset: 0x0001E650
	public static event Action<string> OnResourceCheckFailure;

	// Token: 0x060004EB RID: 1259 RVA: 0x00020468 File Offset: 0x0001E668
	private void Awake()
	{
		this.timer = Time.realtimeSinceStartup + 60f;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0002047C File Offset: 0x0001E67C
	public static void Init()
	{
		ResourceCheck._loadedNames.Clear();
		MonoBehaviour[] array = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)) as MonoBehaviour[];
		if (array != null)
		{
			ResourceCheck._scriptsCount = array.Length;
			foreach (MonoBehaviour monoBehaviour in array)
			{
				if (!ResourceCheck._loadedNames.Contains(Assembly.GetAssembly(monoBehaviour.GetType()).FullName))
				{
					ResourceCheck._loadedNames.Add(Assembly.GetAssembly(monoBehaviour.GetType()).FullName);
				}
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				if (!ResourceCheck._loadedNames.Contains(assembly.FullName))
				{
					ResourceCheck._loadedNames.Add(assembly.FullName);
				}
			}
		}
		AppDomain.CurrentDomain.AssemblyLoad += ResourceCheck.OnNewAssemblyLoaded;
		if (CVars.n_httpDebug)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Found ",
				ResourceCheck._scriptsCount,
				" scripts and ",
				ResourceCheck._loadedNames.Count,
				" assemblyes"
			}));
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x000205C8 File Offset: 0x0001E7C8
	private static void OnNewAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
	{
		if (!ResourceCheck._loadedNames.Contains(args.LoadedAssembly.FullName))
		{
			if (CVars.n_httpDebug)
			{
				Debug.Log("Wrong Assebly = " + args.LoadedAssembly.FullName);
			}
			ResourceCheck.OnResourceCheckFailure("resCheckNewAss");
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00020624 File Offset: 0x0001E824
	public static bool CheckResources()
	{
		bool result = false;
		MonoBehaviour[] array = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)) as MonoBehaviour[];
		if (array != null)
		{
			List<string> list = new List<string>();
			foreach (MonoBehaviour monoBehaviour in array)
			{
				if (!ResourceCheck._loadedNames.Contains(Assembly.GetAssembly(monoBehaviour.GetType()).FullName))
				{
					list.Add(Assembly.GetAssembly(monoBehaviour.GetType()).FullName);
				}
			}
			if (list.Count > 0)
			{
				result = true;
				foreach (string str in list)
				{
					if (CVars.n_httpDebug)
					{
						Debug.Log("Wrong Assebly = " + str);
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00020728 File Offset: 0x0001E928
	private void Update()
	{
		if (this.timer < Time.realtimeSinceStartup)
		{
			this.timer = Time.realtimeSinceStartup + 60f + (float)UnityEngine.Random.Range(0, 15);
			bool flag = ResourceCheck.CheckResources();
			if (flag && CVars.InjCheck > 0 && CVars.InjCheckCount > 0)
			{
				CVars.InjCheckCount--;
			}
		}
	}

	// Token: 0x04000461 RID: 1121
	private static List<string> _loadedNames = new List<string>();

	// Token: 0x04000462 RID: 1122
	private static int _scriptsCount = 0;

	// Token: 0x04000463 RID: 1123
	private float timer;
}
