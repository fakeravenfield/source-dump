using System;
using UnityEngine;

namespace Lua.Wrapper
{
	// Token: 0x02000964 RID: 2404
	[Wrapper(typeof(CapturePoint), includeBase = true)]
	public static class WCapturePoint
	{
		// Token: 0x06003D5C RID: 15708 RVA: 0x0000961A File Offset: 0x0000781A
		[Getter]
		public static float GetCaptureRange(CapturePoint self)
		{
			return self.captureRange;
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x0002980F File Offset: 0x00027A0F
		[Setter]
		public static void SetCaptureRange(CapturePoint self, float value)
		{
			self.captureRange = value;
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x00029818 File Offset: 0x00027A18
		[Getter]
		public static float GetCaptureFloor(CapturePoint self)
		{
			return self.captureFloor;
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x00029820 File Offset: 0x00027A20
		[Setter]
		public static void SetCaptureFloor(CapturePoint self, float value)
		{
			self.captureFloor = value;
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x00029829 File Offset: 0x00027A29
		[Getter]
		public static float GetCaptureCeiling(CapturePoint self)
		{
			return self.captureCeiling;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x00029831 File Offset: 0x00027A31
		[Setter]
		public static void SetCaptureCeiling(CapturePoint self, float value)
		{
			self.captureCeiling = value;
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x0002983A File Offset: 0x00027A3A
		[Getter]
		public static float GetCaptureRate(CapturePoint self)
		{
			return self.captureRate;
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x00029842 File Offset: 0x00027A42
		[Setter]
		public static void SetCaptureRate(CapturePoint self, float value)
		{
			self.captureRate = value;
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0002984B File Offset: 0x00027A4B
		[Getter]
		[Doc("Returns the renderer of the flag, if available.[..]")]
		public static Renderer GetFlagRenderer(CapturePoint self)
		{
			return self.flagRenderer;
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x00029853 File Offset: 0x00027A53
		[Getter]
		[Doc("The team that is closest to taking over the capture point.[..] This is the same team as the current team color indicated on the flag renderer.")]
		public static WTeam GetPendingOwner(CapturePoint self)
		{
			return (WTeam)self.pendingOwner;
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x0002985B File Offset: 0x00027A5B
		[Getter]
		[Doc("The capture progress of the pending owner, from 0 to 1")]
		public static float GetCaptureProgress(CapturePoint self)
		{
			return self.control;
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x00029863 File Offset: 0x00027A63
		[Getter]
		[Doc("True while any attackers are inside the capture zone.")]
		public static bool GetIsContested(CapturePoint self)
		{
			return self.isContested;
		}
	}
}
