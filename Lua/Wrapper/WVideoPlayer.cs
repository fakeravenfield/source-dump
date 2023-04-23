using System;
using UnityEngine.Video;

namespace Lua.Wrapper
{
	// Token: 0x0200099F RID: 2463
	[Wrapper(typeof(VideoPlayer), includeBase = true, includeTarget = true)]
	public static class WVideoPlayer
	{
		// Token: 0x06003EAD RID: 16045 RVA: 0x0002A570 File Offset: 0x00028770
		[Doc("Sets the VideoPlayer url to a file in the mod content folder.[..] This is the same as setting the VideoPlayer url to a local file path via the default Unity API. For example, ``cool_video.mov`` will load the file cool_video.mov next to your mutator rfc file.")]
		public static void SetModContentFileURL(VideoPlayer self, string localPath)
		{
			self.url = RavenscriptManager.ResolveInvokingSourceModContentPath(localPath);
		}
	}
}
