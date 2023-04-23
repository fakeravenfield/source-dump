using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapEditor.Internal.SanityCheck
{
	// Token: 0x0200074B RID: 1867
	public class WithinProtectRange<T> : ValidationRule where T : MapEditorObject
	{
		// Token: 0x06002E8D RID: 11917 RVA: 0x00020020 File Offset: 0x0001E220
		protected WithinProtectRange(string message)
		{
			this.message = message;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x00109080 File Offset: 0x00107280
		public override bool Validate(out ValidationResult result)
		{
			result = ValidationResult.empty;
			MeoCapturePoint[] array = MapEditor.instance.FindObjectsToSave<MeoCapturePoint>();
			List<T> list = MapEditor.instance.FindObjectsToSave<T>().ToList<T>();
			List<T> notProtected = new List<T>();
			foreach (T t in list)
			{
				bool flag = false;
				foreach (MeoCapturePoint meoCapturePoint in array)
				{
					if (Vector3.Distance(t.transform.position, meoCapturePoint.transform.position) <= meoCapturePoint.protectRange)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					notProtected.Add(t);
				}
			}
			if (!notProtected.Any<T>())
			{
				return true;
			}
			result = new ValidationResult(this.message, delegate()
			{
				SelectableObject[] objects = (from vs in notProtected
				select vs.GetSelectableObject()).ToArray<SelectableObject>();
				MapEditor.instance.SetSelection(new Selection(objects));
				MeTools.instance.SwitchToNoopTool();
			});
			return false;
		}

		// Token: 0x04002A9F RID: 10911
		private readonly string message;
	}
}
