using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200047C RID: 1148
	public class MeshBakerMaterialTexture
	{
		// Token: 0x170001A9 RID: 425
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x000159B9 File Offset: 0x00013BB9
		public Texture2D t
		{
			set
			{
				this._t = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x000159C2 File Offset: 0x00013BC2
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x000159CA File Offset: 0x00013BCA
		public DRect matTilingRect { get; private set; }

		// Token: 0x06001CEA RID: 7402 RVA: 0x0000256A File Offset: 0x0000076A
		public MeshBakerMaterialTexture()
		{
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000BE120 File Offset: 0x000BC320
		public MeshBakerMaterialTexture(Texture tx)
		{
			if (tx is Texture2D)
			{
				this._t = (Texture2D)tx;
				return;
			}
			if (!(tx == null))
			{
				Debug.LogError("An error occured. Texture must be Texture2D " + ((tx != null) ? tx.ToString() : null));
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x000BE170 File Offset: 0x000BC370
		public MeshBakerMaterialTexture(Texture tx, Vector2 matTilingOffset, Vector2 matTilingScale, float texelDens)
		{
			if (tx is Texture2D)
			{
				this._t = (Texture2D)tx;
			}
			else if (!(tx == null))
			{
				Debug.LogError("An error occured. Texture must be Texture2D " + ((tx != null) ? tx.ToString() : null));
			}
			this.matTilingRect = new DRect(matTilingOffset, matTilingScale);
			this.texelDensity = texelDens;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x000159D3 File Offset: 0x00013BD3
		public DRect GetEncapsulatingSamplingRect()
		{
			return this.encapsulatingSamplingRect;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x000159DB File Offset: 0x00013BDB
		public void SetEncapsulatingSamplingRect(MB_TexSet ts, DRect r)
		{
			this.encapsulatingSamplingRect = r;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x000159E4 File Offset: 0x00013BE4
		public Texture2D GetTexture2D()
		{
			if (!MeshBakerMaterialTexture.readyToBuildAtlases)
			{
				Debug.LogError("This function should not be called before Step3. For steps 1 and 2 should always call methods like isNull, width, height");
				throw new Exception("GetTexture2D called before ready to build atlases");
			}
			return this._t;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00015A08 File Offset: 0x00013C08
		public bool isNull
		{
			get
			{
				return this._t == null;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x00015A16 File Offset: 0x00013C16
		public int width
		{
			get
			{
				if (this._t != null)
				{
					return this._t.width;
				}
				throw new Exception("Texture was null. can't get width");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00015A3C File Offset: 0x00013C3C
		public int height
		{
			get
			{
				if (this._t != null)
				{
					return this._t.height;
				}
				throw new Exception("Texture was null. can't get height");
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00015A62 File Offset: 0x00013C62
		public string GetTexName()
		{
			if (this._t != null)
			{
				return this._t.name;
			}
			return "null";
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00015A83 File Offset: 0x00013C83
		public bool AreTexturesEqual(MeshBakerMaterialTexture b)
		{
			return this._t == b._t;
		}

		// Token: 0x04001DB4 RID: 7604
		private Texture2D _t;

		// Token: 0x04001DB5 RID: 7605
		public float texelDensity;

		// Token: 0x04001DB6 RID: 7606
		internal static bool readyToBuildAtlases;

		// Token: 0x04001DB7 RID: 7607
		private DRect encapsulatingSamplingRect;
	}
}
