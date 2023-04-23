using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x02000741 RID: 1857
	public abstract class ValidationRule
	{
		// Token: 0x06002E74 RID: 11892
		public abstract bool Validate(out ValidationResult result);

		// Token: 0x06002E75 RID: 11893 RVA: 0x00108DDC File Offset: 0x00106FDC
		public static ValidationRule[] FindAll()
		{
			Assembly assembly = Assembly.GetAssembly(typeof(ValidationRule));
			Type typeFromHandle = typeof(ValidationRule);
			List<ValidationRule> list = new List<ValidationRule>();
			foreach (Type type in assembly.GetTypes())
			{
				if (typeFromHandle.IsAssignableFrom(type) && !type.IsGenericType && type != typeFromHandle)
				{
					try
					{
						ValidationRule validationRule = Activator.CreateInstance(type) as ValidationRule;
						if (validationRule != null)
						{
							list.Add(validationRule);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
				}
			}
			return list.ToArray();
		}
	}
}
