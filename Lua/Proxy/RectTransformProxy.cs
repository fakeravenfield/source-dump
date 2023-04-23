using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x020009EC RID: 2540
	[Proxy(typeof(RectTransform))]
	public class RectTransformProxy : IProxy
	{
		// Token: 0x06004BA9 RID: 19369 RVA: 0x00036202 File Offset: 0x00034402
		[MoonSharpHidden]
		public RectTransformProxy(RectTransform value)
		{
			this._value = value;
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x00036211 File Offset: 0x00034411
		public RectTransformProxy()
		{
			this._value = new RectTransform();
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06004BAB RID: 19371 RVA: 0x00036224 File Offset: 0x00034424
		// (set) Token: 0x06004BAC RID: 19372 RVA: 0x00036236 File Offset: 0x00034436
		public Vector2Proxy anchoredPosition
		{
			get
			{
				return Vector2Proxy.New(this._value.anchoredPosition);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.anchoredPosition = value._value;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06004BAD RID: 19373 RVA: 0x00036257 File Offset: 0x00034457
		// (set) Token: 0x06004BAE RID: 19374 RVA: 0x00036269 File Offset: 0x00034469
		public Vector3Proxy anchoredPosition3D
		{
			get
			{
				return Vector3Proxy.New(this._value.anchoredPosition3D);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.anchoredPosition3D = value._value;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06004BAF RID: 19375 RVA: 0x0003628A File Offset: 0x0003448A
		// (set) Token: 0x06004BB0 RID: 19376 RVA: 0x0003629C File Offset: 0x0003449C
		public Vector2Proxy anchorMax
		{
			get
			{
				return Vector2Proxy.New(this._value.anchorMax);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.anchorMax = value._value;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06004BB1 RID: 19377 RVA: 0x000362BD File Offset: 0x000344BD
		// (set) Token: 0x06004BB2 RID: 19378 RVA: 0x000362CF File Offset: 0x000344CF
		public Vector2Proxy anchorMin
		{
			get
			{
				return Vector2Proxy.New(this._value.anchorMin);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.anchorMin = value._value;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x000362F0 File Offset: 0x000344F0
		// (set) Token: 0x06004BB4 RID: 19380 RVA: 0x00036302 File Offset: 0x00034502
		public Vector2Proxy offsetMax
		{
			get
			{
				return Vector2Proxy.New(this._value.offsetMax);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.offsetMax = value._value;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06004BB5 RID: 19381 RVA: 0x00036323 File Offset: 0x00034523
		// (set) Token: 0x06004BB6 RID: 19382 RVA: 0x00036335 File Offset: 0x00034535
		public Vector2Proxy offsetMin
		{
			get
			{
				return Vector2Proxy.New(this._value.offsetMin);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.offsetMin = value._value;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06004BB7 RID: 19383 RVA: 0x00036356 File Offset: 0x00034556
		// (set) Token: 0x06004BB8 RID: 19384 RVA: 0x00036368 File Offset: 0x00034568
		public Vector2Proxy pivot
		{
			get
			{
				return Vector2Proxy.New(this._value.pivot);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.pivot = value._value;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x00036389 File Offset: 0x00034589
		public RectProxy rect
		{
			get
			{
				return RectProxy.New(this._value.rect);
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06004BBA RID: 19386 RVA: 0x0003639B File Offset: 0x0003459B
		// (set) Token: 0x06004BBB RID: 19387 RVA: 0x000363AD File Offset: 0x000345AD
		public Vector2Proxy sizeDelta
		{
			get
			{
				return Vector2Proxy.New(this._value.sizeDelta);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.sizeDelta = value._value;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06004BBC RID: 19388 RVA: 0x000363CE File Offset: 0x000345CE
		public int childCount
		{
			get
			{
				return this._value.childCount;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06004BBD RID: 19389 RVA: 0x000363DB File Offset: 0x000345DB
		// (set) Token: 0x06004BBE RID: 19390 RVA: 0x000363ED File Offset: 0x000345ED
		public Vector3Proxy eulerAngles
		{
			get
			{
				return Vector3Proxy.New(this._value.eulerAngles);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.eulerAngles = value._value;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06004BBF RID: 19391 RVA: 0x0003640E File Offset: 0x0003460E
		// (set) Token: 0x06004BC0 RID: 19392 RVA: 0x00036420 File Offset: 0x00034620
		public Vector3Proxy forward
		{
			get
			{
				return Vector3Proxy.New(this._value.forward);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.forward = value._value;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x00036441 File Offset: 0x00034641
		// (set) Token: 0x06004BC2 RID: 19394 RVA: 0x0003644E File Offset: 0x0003464E
		public bool hasChanged
		{
			get
			{
				return this._value.hasChanged;
			}
			set
			{
				this._value.hasChanged = value;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x0003645C File Offset: 0x0003465C
		// (set) Token: 0x06004BC4 RID: 19396 RVA: 0x00036469 File Offset: 0x00034669
		public int hierarchyCapacity
		{
			get
			{
				return this._value.hierarchyCapacity;
			}
			set
			{
				this._value.hierarchyCapacity = value;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06004BC5 RID: 19397 RVA: 0x00036477 File Offset: 0x00034677
		public int hierarchyCount
		{
			get
			{
				return this._value.hierarchyCount;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x00036484 File Offset: 0x00034684
		// (set) Token: 0x06004BC7 RID: 19399 RVA: 0x00036496 File Offset: 0x00034696
		public Vector3Proxy localEulerAngles
		{
			get
			{
				return Vector3Proxy.New(this._value.localEulerAngles);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.localEulerAngles = value._value;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x000364B7 File Offset: 0x000346B7
		// (set) Token: 0x06004BC9 RID: 19401 RVA: 0x000364C9 File Offset: 0x000346C9
		public Vector3Proxy localPosition
		{
			get
			{
				return Vector3Proxy.New(this._value.localPosition);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.localPosition = value._value;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06004BCA RID: 19402 RVA: 0x000364EA File Offset: 0x000346EA
		// (set) Token: 0x06004BCB RID: 19403 RVA: 0x000364FC File Offset: 0x000346FC
		public QuaternionProxy localRotation
		{
			get
			{
				return QuaternionProxy.New(this._value.localRotation);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.localRotation = value._value;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06004BCC RID: 19404 RVA: 0x0003651D File Offset: 0x0003471D
		// (set) Token: 0x06004BCD RID: 19405 RVA: 0x0003652F File Offset: 0x0003472F
		public Vector3Proxy localScale
		{
			get
			{
				return Vector3Proxy.New(this._value.localScale);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.localScale = value._value;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06004BCE RID: 19406 RVA: 0x00036550 File Offset: 0x00034750
		public Matrix4x4Proxy localToWorldMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.localToWorldMatrix);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x00036562 File Offset: 0x00034762
		public Vector3Proxy lossyScale
		{
			get
			{
				return Vector3Proxy.New(this._value.lossyScale);
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06004BD0 RID: 19408 RVA: 0x00036574 File Offset: 0x00034774
		// (set) Token: 0x06004BD1 RID: 19409 RVA: 0x00131E68 File Offset: 0x00130068
		public TransformProxy parent
		{
			get
			{
				return TransformProxy.New(this._value.parent);
			}
			set
			{
				Transform parent = null;
				if (value != null)
				{
					parent = value._value;
				}
				this._value.parent = parent;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06004BD2 RID: 19410 RVA: 0x00036586 File Offset: 0x00034786
		// (set) Token: 0x06004BD3 RID: 19411 RVA: 0x00036598 File Offset: 0x00034798
		public Vector3Proxy position
		{
			get
			{
				return Vector3Proxy.New(this._value.position);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.position = value._value;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x000365B9 File Offset: 0x000347B9
		// (set) Token: 0x06004BD5 RID: 19413 RVA: 0x000365CB File Offset: 0x000347CB
		public Vector3Proxy right
		{
			get
			{
				return Vector3Proxy.New(this._value.right);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.right = value._value;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06004BD6 RID: 19414 RVA: 0x000365EC File Offset: 0x000347EC
		public TransformProxy root
		{
			get
			{
				return TransformProxy.New(this._value.root);
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06004BD7 RID: 19415 RVA: 0x000365FE File Offset: 0x000347FE
		// (set) Token: 0x06004BD8 RID: 19416 RVA: 0x00036610 File Offset: 0x00034810
		public QuaternionProxy rotation
		{
			get
			{
				return QuaternionProxy.New(this._value.rotation);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.rotation = value._value;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06004BD9 RID: 19417 RVA: 0x00036631 File Offset: 0x00034831
		// (set) Token: 0x06004BDA RID: 19418 RVA: 0x00036643 File Offset: 0x00034843
		public Vector3Proxy up
		{
			get
			{
				return Vector3Proxy.New(this._value.up);
			}
			set
			{
				if (value == null)
				{
					throw new ScriptRuntimeException("argument 'value' is nil");
				}
				this._value.up = value._value;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06004BDB RID: 19419 RVA: 0x00036664 File Offset: 0x00034864
		public Matrix4x4Proxy worldToLocalMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.worldToLocalMatrix);
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x00036676 File Offset: 0x00034876
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x00036688 File Offset: 0x00034888
		// (set) Token: 0x06004BDE RID: 19422 RVA: 0x00036695 File Offset: 0x00034895
		public string tag
		{
			get
			{
				return this._value.tag;
			}
			set
			{
				this._value.tag = value;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06004BDF RID: 19423 RVA: 0x000366A3 File Offset: 0x000348A3
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x000366B5 File Offset: 0x000348B5
		// (set) Token: 0x06004BE1 RID: 19425 RVA: 0x000366C2 File Offset: 0x000348C2
		public string name
		{
			get
			{
				return this._value.name;
			}
			set
			{
				this._value.name = value;
			}
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x000366D0 File Offset: 0x000348D0
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x00131E90 File Offset: 0x00130090
		[MoonSharpHidden]
		public static RectTransformProxy New(RectTransform value)
		{
			if (value == null)
			{
				return null;
			}
			RectTransformProxy rectTransformProxy = (RectTransformProxy)ObjectCache.Get(typeof(RectTransformProxy), value);
			if (rectTransformProxy == null)
			{
				rectTransformProxy = new RectTransformProxy(value);
				ObjectCache.Add(typeof(RectTransformProxy), value, rectTransformProxy);
			}
			return rectTransformProxy;
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x000366D8 File Offset: 0x000348D8
		[MoonSharpUserDataMetamethod("__call")]
		public static RectTransformProxy Call(DynValue _)
		{
			return new RectTransformProxy();
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x000366DF File Offset: 0x000348DF
		public void ForceUpdateRectTransforms()
		{
			this._value.ForceUpdateRectTransforms();
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x000366EC File Offset: 0x000348EC
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			this._value.GetLocalCorners(fourCornersArray);
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x000366FA File Offset: 0x000348FA
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			this._value.GetWorldCorners(fourCornersArray);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x00036708 File Offset: 0x00034908
		public void DetachChildren()
		{
			this._value.DetachChildren();
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00036715 File Offset: 0x00034915
		public TransformProxy Find(string n)
		{
			return TransformProxy.New(this._value.Find(n));
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00036728 File Offset: 0x00034928
		public TransformProxy GetChild(int index)
		{
			return TransformProxy.New(this._value.GetChild(index));
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0003673B File Offset: 0x0003493B
		public int GetSiblingIndex()
		{
			return this._value.GetSiblingIndex();
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x00036748 File Offset: 0x00034948
		public Vector3Proxy InverseTransformDirection(Vector3Proxy direction)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			return Vector3Proxy.New(this._value.InverseTransformDirection(direction._value));
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0003676E File Offset: 0x0003496E
		public Vector3Proxy InverseTransformDirection(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.InverseTransformDirection(x, y, z));
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x00036783 File Offset: 0x00034983
		public Vector3Proxy InverseTransformPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.InverseTransformPoint(position._value));
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x000367A9 File Offset: 0x000349A9
		public Vector3Proxy InverseTransformPoint(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.InverseTransformPoint(x, y, z));
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x000367BE File Offset: 0x000349BE
		public Vector3Proxy InverseTransformVector(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector3Proxy.New(this._value.InverseTransformVector(vector._value));
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x000367E4 File Offset: 0x000349E4
		public Vector3Proxy InverseTransformVector(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.InverseTransformVector(x, y, z));
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x00131EDC File Offset: 0x001300DC
		public bool IsChildOf(TransformProxy parent)
		{
			Transform parent2 = null;
			if (parent != null)
			{
				parent2 = parent._value;
			}
			return this._value.IsChildOf(parent2);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x00131F04 File Offset: 0x00130104
		public void LookAt(TransformProxy target, Vector3Proxy worldUp)
		{
			Transform target2 = null;
			if (target != null)
			{
				target2 = target._value;
			}
			if (worldUp == null)
			{
				throw new ScriptRuntimeException("argument 'worldUp' is nil");
			}
			this._value.LookAt(target2, worldUp._value);
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x00131F40 File Offset: 0x00130140
		public void LookAt(TransformProxy target)
		{
			Transform target2 = null;
			if (target != null)
			{
				target2 = target._value;
			}
			this._value.LookAt(target2);
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x000367F9 File Offset: 0x000349F9
		public void LookAt(Vector3Proxy worldPosition, Vector3Proxy worldUp)
		{
			if (worldPosition == null)
			{
				throw new ScriptRuntimeException("argument 'worldPosition' is nil");
			}
			if (worldUp == null)
			{
				throw new ScriptRuntimeException("argument 'worldUp' is nil");
			}
			this._value.LookAt(worldPosition._value, worldUp._value);
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0003682E File Offset: 0x00034A2E
		public void LookAt(Vector3Proxy worldPosition)
		{
			if (worldPosition == null)
			{
				throw new ScriptRuntimeException("argument 'worldPosition' is nil");
			}
			this._value.LookAt(worldPosition._value);
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0003684F File Offset: 0x00034A4F
		public void Rotate(Vector3Proxy eulers, Space relativeTo)
		{
			if (eulers == null)
			{
				throw new ScriptRuntimeException("argument 'eulers' is nil");
			}
			this._value.Rotate(eulers._value, relativeTo);
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x00036871 File Offset: 0x00034A71
		public void Rotate(Vector3Proxy eulers)
		{
			if (eulers == null)
			{
				throw new ScriptRuntimeException("argument 'eulers' is nil");
			}
			this._value.Rotate(eulers._value);
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x00036892 File Offset: 0x00034A92
		public void Rotate(float xAngle, float yAngle, float zAngle, Space relativeTo)
		{
			this._value.Rotate(xAngle, yAngle, zAngle, relativeTo);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x000368A4 File Offset: 0x00034AA4
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			this._value.Rotate(xAngle, yAngle, zAngle);
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x000368B4 File Offset: 0x00034AB4
		public void Rotate(Vector3Proxy axis, float angle, Space relativeTo)
		{
			if (axis == null)
			{
				throw new ScriptRuntimeException("argument 'axis' is nil");
			}
			this._value.Rotate(axis._value, angle, relativeTo);
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x000368D7 File Offset: 0x00034AD7
		public void Rotate(Vector3Proxy axis, float angle)
		{
			if (axis == null)
			{
				throw new ScriptRuntimeException("argument 'axis' is nil");
			}
			this._value.Rotate(axis._value, angle);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x000368F9 File Offset: 0x00034AF9
		public void RotateAround(Vector3Proxy point, Vector3Proxy axis, float angle)
		{
			if (point == null)
			{
				throw new ScriptRuntimeException("argument 'point' is nil");
			}
			if (axis == null)
			{
				throw new ScriptRuntimeException("argument 'axis' is nil");
			}
			this._value.RotateAround(point._value, axis._value, angle);
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0003692F File Offset: 0x00034B2F
		public void SetAsFirstSibling()
		{
			this._value.SetAsFirstSibling();
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0003693C File Offset: 0x00034B3C
		public void SetAsLastSibling()
		{
			this._value.SetAsLastSibling();
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x00131F68 File Offset: 0x00130168
		public void SetParent(TransformProxy p)
		{
			Transform parent = null;
			if (p != null)
			{
				parent = p._value;
			}
			this._value.SetParent(parent);
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x00131F90 File Offset: 0x00130190
		public void SetParent(TransformProxy parent, bool worldPositionStays)
		{
			Transform parent2 = null;
			if (parent != null)
			{
				parent2 = parent._value;
			}
			this._value.SetParent(parent2, worldPositionStays);
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x00036949 File Offset: 0x00034B49
		public void SetPositionAndRotation(Vector3Proxy position, QuaternionProxy rotation)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			if (rotation == null)
			{
				throw new ScriptRuntimeException("argument 'rotation' is nil");
			}
			this._value.SetPositionAndRotation(position._value, rotation._value);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0003697E File Offset: 0x00034B7E
		public void SetSiblingIndex(int index)
		{
			this._value.SetSiblingIndex(index);
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0003698C File Offset: 0x00034B8C
		public Vector3Proxy TransformDirection(Vector3Proxy direction)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			return Vector3Proxy.New(this._value.TransformDirection(direction._value));
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x000369B2 File Offset: 0x00034BB2
		public Vector3Proxy TransformDirection(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.TransformDirection(x, y, z));
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x000369C7 File Offset: 0x00034BC7
		public Vector3Proxy TransformPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.TransformPoint(position._value));
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x000369ED File Offset: 0x00034BED
		public Vector3Proxy TransformPoint(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.TransformPoint(x, y, z));
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x00036A02 File Offset: 0x00034C02
		public Vector3Proxy TransformVector(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector3Proxy.New(this._value.TransformVector(vector._value));
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x00036A28 File Offset: 0x00034C28
		public Vector3Proxy TransformVector(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.TransformVector(x, y, z));
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x00036A3D File Offset: 0x00034C3D
		public void Translate(Vector3Proxy translation, Space relativeTo)
		{
			if (translation == null)
			{
				throw new ScriptRuntimeException("argument 'translation' is nil");
			}
			this._value.Translate(translation._value, relativeTo);
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x00036A5F File Offset: 0x00034C5F
		public void Translate(Vector3Proxy translation)
		{
			if (translation == null)
			{
				throw new ScriptRuntimeException("argument 'translation' is nil");
			}
			this._value.Translate(translation._value);
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x00036A80 File Offset: 0x00034C80
		public void Translate(float x, float y, float z, Space relativeTo)
		{
			this._value.Translate(x, y, z, relativeTo);
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x00036A92 File Offset: 0x00034C92
		public void Translate(float x, float y, float z)
		{
			this._value.Translate(x, y, z);
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x00131FB8 File Offset: 0x001301B8
		public void Translate(Vector3Proxy translation, TransformProxy relativeTo)
		{
			if (translation == null)
			{
				throw new ScriptRuntimeException("argument 'translation' is nil");
			}
			Transform relativeTo2 = null;
			if (relativeTo != null)
			{
				relativeTo2 = relativeTo._value;
			}
			this._value.Translate(translation._value, relativeTo2);
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x00131FF4 File Offset: 0x001301F4
		public void Translate(float x, float y, float z, TransformProxy relativeTo)
		{
			Transform relativeTo2 = null;
			if (relativeTo != null)
			{
				relativeTo2 = relativeTo._value;
			}
			this._value.Translate(x, y, z, relativeTo2);
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x00036AA2 File Offset: 0x00034CA2
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x00036AB0 File Offset: 0x00034CB0
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x00036ABD File Offset: 0x00034CBD
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04003183 RID: 12675
		[MoonSharpHidden]
		public RectTransform _value;
	}
}
