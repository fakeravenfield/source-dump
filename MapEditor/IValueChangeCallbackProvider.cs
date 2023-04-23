using System;

namespace MapEditor
{
	// Token: 0x020006A6 RID: 1702
	public interface IValueChangeCallbackProvider
	{
		// Token: 0x06002B1C RID: 11036
		void RegisterOnValueChangeCallback(DelOnValueChangedCallback callback);
	}
}
