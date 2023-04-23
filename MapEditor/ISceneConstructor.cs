using System;
using System.Collections.Generic;

namespace MapEditor
{
	// Token: 0x020005DB RID: 1499
	public interface ISceneConstructor
	{
		// Token: 0x060026C1 RID: 9921
		void StartSceneConstruction();

		// Token: 0x060026C2 RID: 9922
		IEnumerable<SceneConstructionProgress> ConstructSceneAsync();

		// Token: 0x060026C3 RID: 9923
		void EndSceneConstruction();
	}
}
