using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200038D RID: 909
public static class ComponentCopyPaste
{
	// Token: 0x06001D3E RID: 7486 RVA: 0x00100DA4 File Offset: 0x000FEFA4
	public static void CopyTo(GameObject obj, Component component)
	{
		if (component == null)
		{
			return;
		}
		ComponentCopyPaste.Copy(component, obj.AddComponent(component.GetType()));
		UnityEngine.Object.Destroy(component);
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x00100DCC File Offset: 0x000FEFCC
	public static void CopyFromTo(GameObject from, GameObject to, Type[] types)
	{
		HashSet<Type> hashSet = new HashSet<Type>(types);
		Component[] components = from.GetComponents<Component>();
		foreach (Component component in components)
		{
			if (!(component == null))
			{
				if (hashSet.Contains(component.GetType()))
				{
					ComponentCopyPaste.CopyTo(to, component);
				}
			}
		}
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x00100E30 File Offset: 0x000FF030
	public static void Copy(Component fromComponent, Component toComponent)
	{
		if (fromComponent.GetType() != toComponent.GetType())
		{
			return;
		}
		LinkedList<PropertyInfo> properties = ComponentCopyPaste.GetProperties(fromComponent);
		LinkedList<FieldInfo> fields = ComponentCopyPaste.GetFields(fromComponent);
		Dictionary<PropertyInfo, object> dictionary = new Dictionary<PropertyInfo, object>();
		Dictionary<FieldInfo, object> dictionary2 = new Dictionary<FieldInfo, object>();
		foreach (PropertyInfo propertyInfo in properties)
		{
			dictionary[propertyInfo] = propertyInfo.GetValue(fromComponent, null);
		}
		foreach (FieldInfo fieldInfo in fields)
		{
			dictionary2[fieldInfo] = fieldInfo.GetValue(fromComponent);
		}
		properties = ComponentCopyPaste.GetProperties(toComponent);
		fields = ComponentCopyPaste.GetFields(toComponent);
		foreach (PropertyInfo propertyInfo2 in properties)
		{
			propertyInfo2.SetValue(toComponent, dictionary[propertyInfo2], null);
		}
		foreach (FieldInfo fieldInfo2 in fields)
		{
			fieldInfo2.SetValue(toComponent, dictionary2[fieldInfo2]);
		}
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x00100FF0 File Offset: 0x000FF1F0
	private static LinkedList<PropertyInfo> GetProperties(Component component)
	{
		if (ComponentCopyPaste.deniedPropertys == null)
		{
			ComponentCopyPaste.deniedPropertys = new HashSet<string>();
			foreach (PropertyInfo propertyInfo in typeof(Component).GetProperties())
			{
				ComponentCopyPaste.deniedPropertys.Add(propertyInfo.Name);
			}
		}
		LinkedList<PropertyInfo> linkedList = new LinkedList<PropertyInfo>();
		PropertyInfo[] properties2 = component.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo2 in properties2)
		{
			if (propertyInfo2.CanWrite)
			{
				if (propertyInfo2.CanRead)
				{
					if (!propertyInfo2.GetGetMethod().IsStatic)
					{
						if (!ComponentCopyPaste.deniedPropertys.Contains(propertyInfo2.Name))
						{
							linkedList.AddLast(propertyInfo2);
						}
					}
				}
			}
		}
		return linkedList;
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x001010E0 File Offset: 0x000FF2E0
	private static LinkedList<FieldInfo> GetFields(Component component)
	{
		LinkedList<FieldInfo> linkedList = new LinkedList<FieldInfo>();
		FieldInfo[] fields = component.GetType().GetFields();
		foreach (FieldInfo fieldInfo in fields)
		{
			if (fieldInfo.IsPublic)
			{
				if (!fieldInfo.IsStatic)
				{
					linkedList.AddLast(fieldInfo);
				}
			}
		}
		return linkedList;
	}

	// Token: 0x040021EE RID: 8686
	private static HashSet<string> deniedPropertys;
}
