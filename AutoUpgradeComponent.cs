using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x0200031B RID: 795
public static class AutoUpgradeComponent
{
	// Token: 0x060014A0 RID: 5280 RVA: 0x0009814C File Offset: 0x0009634C
	static AutoUpgradeComponent()
	{
		Type interfaceType = typeof(IAutoUpgradeReference);
		AutoUpgradeComponent.UPGRADE_REF_COMPONENT_TYPES = new List<Type>(from t in typeof(AutoUpgradeComponent).Assembly.GetTypes()
		where interfaceType.IsAssignableFrom(t)
		select t);
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x000981A8 File Offset: 0x000963A8
	public static Component Upgrade(Component oldComponent)
	{
		Type type = oldComponent.GetType();
		Type upgradeType = ((IAutoUpgradeComponent)oldComponent).GetUpgradeType();
		AutoUpgradeComponent.UpgradeInfo upgradeInfo;
		if (AutoUpgradeComponent.CACHED_UPGRADE_INFO.ContainsKey(type))
		{
			upgradeInfo = AutoUpgradeComponent.CACHED_UPGRADE_INFO[type];
		}
		else
		{
			upgradeInfo = new AutoUpgradeComponent.UpgradeInfo(type, upgradeType);
			AutoUpgradeComponent.CACHED_UPGRADE_INFO.Add(type, upgradeInfo);
		}
		Debug.LogFormat("Upgrading {0}.{1} to {2}", new object[]
		{
			oldComponent.gameObject.name,
			type.Name,
			upgradeType.Name
		});
		Component component = oldComponent.gameObject.AddComponent(upgradeType);
		upgradeInfo.ApplyFields(oldComponent, component);
		AutoUpgradeComponent.UpdateComponentReferences(oldComponent, component);
		UnityEngine.Object.DestroyImmediate(oldComponent, true);
		return component;
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x0009824C File Offset: 0x0009644C
	private static void UpdateComponentReferences(Component oldComponent, Component newComponent)
	{
		foreach (Type type in AutoUpgradeComponent.UPGRADE_REF_COMPONENT_TYPES)
		{
			foreach (Component component in newComponent.gameObject.GetComponentsInChildren(type, true))
			{
				try
				{
					((IAutoUpgradeReference)component).UpgradeReference(oldComponent, newComponent);
				}
				catch
				{
				}
			}
		}
	}

	// Token: 0x0400164B RID: 5707
	public static Dictionary<Type, AutoUpgradeComponent.UpgradeInfo> CACHED_UPGRADE_INFO = new Dictionary<Type, AutoUpgradeComponent.UpgradeInfo>();

	// Token: 0x0400164C RID: 5708
	public static List<Type> UPGRADE_REF_COMPONENT_TYPES;

	// Token: 0x0200031C RID: 796
	public class UpgradeInfo
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x000982D8 File Offset: 0x000964D8
		public UpgradeInfo(Type sourceType, Type destinationType)
		{
			this.sourceType = sourceType;
			this.destinationType = destinationType;
			FieldInfo[] array = (from f in this.sourceType.GetFields(BindingFlags.Instance | BindingFlags.Public)
			where !f.IsNotSerialized
			select f).ToArray<FieldInfo>();
			this.fieldMap = new Dictionary<FieldInfo, FieldInfo>();
			foreach (FieldInfo fieldInfo in array)
			{
				FieldInfo field = destinationType.GetField(fieldInfo.Name);
				if (destinationType != null)
				{
					this.fieldMap.Add(fieldInfo, field);
				}
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00098370 File Offset: 0x00096570
		public void ApplyFields(Component oldComponent, Component newComponent)
		{
			foreach (FieldInfo fieldInfo in this.fieldMap.Keys)
			{
				FieldInfo fieldInfo2 = this.fieldMap[fieldInfo];
				object value = fieldInfo.GetValue(oldComponent);
				fieldInfo2.SetValue(newComponent, value);
			}
		}

		// Token: 0x0400164D RID: 5709
		public Type sourceType;

		// Token: 0x0400164E RID: 5710
		public Type destinationType;

		// Token: 0x0400164F RID: 5711
		public Dictionary<FieldInfo, FieldInfo> fieldMap;
	}
}
