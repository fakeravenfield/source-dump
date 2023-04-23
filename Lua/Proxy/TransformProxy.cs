using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Proxy
{
	// Token: 0x02000A0D RID: 2573
	[Proxy(typeof(Transform))]
	public class TransformProxy : IProxy
	{
		// Token: 0x06005052 RID: 20562 RVA: 0x0003A675 File Offset: 0x00038875
		[MoonSharpHidden]
		public TransformProxy(Transform value)
		{
			this._value = value;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06005053 RID: 20563 RVA: 0x0003A684 File Offset: 0x00038884
		public int childCount
		{
			get
			{
				return this._value.childCount;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06005054 RID: 20564 RVA: 0x0003A691 File Offset: 0x00038891
		// (set) Token: 0x06005055 RID: 20565 RVA: 0x0003A6A3 File Offset: 0x000388A3
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

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x0003A6C4 File Offset: 0x000388C4
		// (set) Token: 0x06005057 RID: 20567 RVA: 0x0003A6D6 File Offset: 0x000388D6
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

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x0003A6F7 File Offset: 0x000388F7
		// (set) Token: 0x06005059 RID: 20569 RVA: 0x0003A704 File Offset: 0x00038904
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

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600505A RID: 20570 RVA: 0x0003A712 File Offset: 0x00038912
		// (set) Token: 0x0600505B RID: 20571 RVA: 0x0003A71F File Offset: 0x0003891F
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

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600505C RID: 20572 RVA: 0x0003A72D File Offset: 0x0003892D
		public int hierarchyCount
		{
			get
			{
				return this._value.hierarchyCount;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600505D RID: 20573 RVA: 0x0003A73A File Offset: 0x0003893A
		// (set) Token: 0x0600505E RID: 20574 RVA: 0x0003A74C File Offset: 0x0003894C
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

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600505F RID: 20575 RVA: 0x0003A76D File Offset: 0x0003896D
		// (set) Token: 0x06005060 RID: 20576 RVA: 0x0003A77F File Offset: 0x0003897F
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

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x0003A7A0 File Offset: 0x000389A0
		// (set) Token: 0x06005062 RID: 20578 RVA: 0x0003A7B2 File Offset: 0x000389B2
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

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x0003A7D3 File Offset: 0x000389D3
		// (set) Token: 0x06005064 RID: 20580 RVA: 0x0003A7E5 File Offset: 0x000389E5
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

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06005065 RID: 20581 RVA: 0x0003A806 File Offset: 0x00038A06
		public Matrix4x4Proxy localToWorldMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.localToWorldMatrix);
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06005066 RID: 20582 RVA: 0x0003A818 File Offset: 0x00038A18
		public Vector3Proxy lossyScale
		{
			get
			{
				return Vector3Proxy.New(this._value.lossyScale);
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06005067 RID: 20583 RVA: 0x0003A82A File Offset: 0x00038A2A
		// (set) Token: 0x06005068 RID: 20584 RVA: 0x001387D4 File Offset: 0x001369D4
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

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06005069 RID: 20585 RVA: 0x0003A83C File Offset: 0x00038A3C
		// (set) Token: 0x0600506A RID: 20586 RVA: 0x0003A84E File Offset: 0x00038A4E
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

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600506B RID: 20587 RVA: 0x0003A86F File Offset: 0x00038A6F
		// (set) Token: 0x0600506C RID: 20588 RVA: 0x0003A881 File Offset: 0x00038A81
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

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600506D RID: 20589 RVA: 0x0003A8A2 File Offset: 0x00038AA2
		public TransformProxy root
		{
			get
			{
				return TransformProxy.New(this._value.root);
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		// (set) Token: 0x0600506F RID: 20591 RVA: 0x0003A8C6 File Offset: 0x00038AC6
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

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06005070 RID: 20592 RVA: 0x0003A8E7 File Offset: 0x00038AE7
		// (set) Token: 0x06005071 RID: 20593 RVA: 0x0003A8F9 File Offset: 0x00038AF9
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

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x0003A91A File Offset: 0x00038B1A
		public Matrix4x4Proxy worldToLocalMatrix
		{
			get
			{
				return Matrix4x4Proxy.New(this._value.worldToLocalMatrix);
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06005073 RID: 20595 RVA: 0x0003A92C File Offset: 0x00038B2C
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(this._value.gameObject);
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x0003A93E File Offset: 0x00038B3E
		// (set) Token: 0x06005075 RID: 20597 RVA: 0x0003A94B File Offset: 0x00038B4B
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

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06005076 RID: 20598 RVA: 0x0003A959 File Offset: 0x00038B59
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(this._value.transform);
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06005077 RID: 20599 RVA: 0x0003A96B File Offset: 0x00038B6B
		// (set) Token: 0x06005078 RID: 20600 RVA: 0x0003A978 File Offset: 0x00038B78
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

		// Token: 0x06005079 RID: 20601 RVA: 0x0003A986 File Offset: 0x00038B86
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x001387FC File Offset: 0x001369FC
		[MoonSharpHidden]
		public static TransformProxy New(Transform value)
		{
			if (value == null)
			{
				return null;
			}
			TransformProxy transformProxy = (TransformProxy)ObjectCache.Get(typeof(TransformProxy), value);
			if (transformProxy == null)
			{
				transformProxy = new TransformProxy(value);
				ObjectCache.Add(typeof(TransformProxy), value, transformProxy);
			}
			return transformProxy;
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x0003A98E File Offset: 0x00038B8E
		public void DetachChildren()
		{
			this._value.DetachChildren();
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x0003A99B File Offset: 0x00038B9B
		public TransformProxy Find(string n)
		{
			return TransformProxy.New(this._value.Find(n));
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x0003A9AE File Offset: 0x00038BAE
		public TransformProxy GetChild(int index)
		{
			return TransformProxy.New(this._value.GetChild(index));
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x0003A9C1 File Offset: 0x00038BC1
		public int GetSiblingIndex()
		{
			return this._value.GetSiblingIndex();
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x0003A9CE File Offset: 0x00038BCE
		public Vector3Proxy InverseTransformDirection(Vector3Proxy direction)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			return Vector3Proxy.New(this._value.InverseTransformDirection(direction._value));
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x0003A9F4 File Offset: 0x00038BF4
		public Vector3Proxy InverseTransformDirection(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.InverseTransformDirection(x, y, z));
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x0003AA09 File Offset: 0x00038C09
		public Vector3Proxy InverseTransformPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.InverseTransformPoint(position._value));
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x0003AA2F File Offset: 0x00038C2F
		public Vector3Proxy InverseTransformPoint(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.InverseTransformPoint(x, y, z));
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x0003AA44 File Offset: 0x00038C44
		public Vector3Proxy InverseTransformVector(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector3Proxy.New(this._value.InverseTransformVector(vector._value));
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0003AA6A File Offset: 0x00038C6A
		public Vector3Proxy InverseTransformVector(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.InverseTransformVector(x, y, z));
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x00138848 File Offset: 0x00136A48
		public bool IsChildOf(TransformProxy parent)
		{
			Transform parent2 = null;
			if (parent != null)
			{
				parent2 = parent._value;
			}
			return this._value.IsChildOf(parent2);
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00138870 File Offset: 0x00136A70
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

		// Token: 0x06005087 RID: 20615 RVA: 0x001388AC File Offset: 0x00136AAC
		public void LookAt(TransformProxy target)
		{
			Transform target2 = null;
			if (target != null)
			{
				target2 = target._value;
			}
			this._value.LookAt(target2);
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x0003AA7F File Offset: 0x00038C7F
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

		// Token: 0x06005089 RID: 20617 RVA: 0x0003AAB4 File Offset: 0x00038CB4
		public void LookAt(Vector3Proxy worldPosition)
		{
			if (worldPosition == null)
			{
				throw new ScriptRuntimeException("argument 'worldPosition' is nil");
			}
			this._value.LookAt(worldPosition._value);
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x0003AAD5 File Offset: 0x00038CD5
		public void Rotate(Vector3Proxy eulers, Space relativeTo)
		{
			if (eulers == null)
			{
				throw new ScriptRuntimeException("argument 'eulers' is nil");
			}
			this._value.Rotate(eulers._value, relativeTo);
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x0003AAF7 File Offset: 0x00038CF7
		public void Rotate(Vector3Proxy eulers)
		{
			if (eulers == null)
			{
				throw new ScriptRuntimeException("argument 'eulers' is nil");
			}
			this._value.Rotate(eulers._value);
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x0003AB18 File Offset: 0x00038D18
		public void Rotate(float xAngle, float yAngle, float zAngle, Space relativeTo)
		{
			this._value.Rotate(xAngle, yAngle, zAngle, relativeTo);
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x0003AB2A File Offset: 0x00038D2A
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			this._value.Rotate(xAngle, yAngle, zAngle);
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x0003AB3A File Offset: 0x00038D3A
		public void Rotate(Vector3Proxy axis, float angle, Space relativeTo)
		{
			if (axis == null)
			{
				throw new ScriptRuntimeException("argument 'axis' is nil");
			}
			this._value.Rotate(axis._value, angle, relativeTo);
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x0003AB5D File Offset: 0x00038D5D
		public void Rotate(Vector3Proxy axis, float angle)
		{
			if (axis == null)
			{
				throw new ScriptRuntimeException("argument 'axis' is nil");
			}
			this._value.Rotate(axis._value, angle);
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0003AB7F File Offset: 0x00038D7F
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

		// Token: 0x06005091 RID: 20625 RVA: 0x0003ABB5 File Offset: 0x00038DB5
		public void SetAsFirstSibling()
		{
			this._value.SetAsFirstSibling();
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x0003ABC2 File Offset: 0x00038DC2
		public void SetAsLastSibling()
		{
			this._value.SetAsLastSibling();
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x001388D4 File Offset: 0x00136AD4
		public void SetParent(TransformProxy p)
		{
			Transform parent = null;
			if (p != null)
			{
				parent = p._value;
			}
			this._value.SetParent(parent);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x001388FC File Offset: 0x00136AFC
		public void SetParent(TransformProxy parent, bool worldPositionStays)
		{
			Transform parent2 = null;
			if (parent != null)
			{
				parent2 = parent._value;
			}
			this._value.SetParent(parent2, worldPositionStays);
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x0003ABCF File Offset: 0x00038DCF
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

		// Token: 0x06005096 RID: 20630 RVA: 0x0003AC04 File Offset: 0x00038E04
		public void SetSiblingIndex(int index)
		{
			this._value.SetSiblingIndex(index);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x0003AC12 File Offset: 0x00038E12
		public Vector3Proxy TransformDirection(Vector3Proxy direction)
		{
			if (direction == null)
			{
				throw new ScriptRuntimeException("argument 'direction' is nil");
			}
			return Vector3Proxy.New(this._value.TransformDirection(direction._value));
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x0003AC38 File Offset: 0x00038E38
		public Vector3Proxy TransformDirection(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.TransformDirection(x, y, z));
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x0003AC4D File Offset: 0x00038E4D
		public Vector3Proxy TransformPoint(Vector3Proxy position)
		{
			if (position == null)
			{
				throw new ScriptRuntimeException("argument 'position' is nil");
			}
			return Vector3Proxy.New(this._value.TransformPoint(position._value));
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x0003AC73 File Offset: 0x00038E73
		public Vector3Proxy TransformPoint(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.TransformPoint(x, y, z));
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x0003AC88 File Offset: 0x00038E88
		public Vector3Proxy TransformVector(Vector3Proxy vector)
		{
			if (vector == null)
			{
				throw new ScriptRuntimeException("argument 'vector' is nil");
			}
			return Vector3Proxy.New(this._value.TransformVector(vector._value));
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x0003ACAE File Offset: 0x00038EAE
		public Vector3Proxy TransformVector(float x, float y, float z)
		{
			return Vector3Proxy.New(this._value.TransformVector(x, y, z));
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x0003ACC3 File Offset: 0x00038EC3
		public void Translate(Vector3Proxy translation, Space relativeTo)
		{
			if (translation == null)
			{
				throw new ScriptRuntimeException("argument 'translation' is nil");
			}
			this._value.Translate(translation._value, relativeTo);
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x0003ACE5 File Offset: 0x00038EE5
		public void Translate(Vector3Proxy translation)
		{
			if (translation == null)
			{
				throw new ScriptRuntimeException("argument 'translation' is nil");
			}
			this._value.Translate(translation._value);
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x0003AD06 File Offset: 0x00038F06
		public void Translate(float x, float y, float z, Space relativeTo)
		{
			this._value.Translate(x, y, z, relativeTo);
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x0003AD18 File Offset: 0x00038F18
		public void Translate(float x, float y, float z)
		{
			this._value.Translate(x, y, z);
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x00138924 File Offset: 0x00136B24
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

		// Token: 0x060050A2 RID: 20642 RVA: 0x00138960 File Offset: 0x00136B60
		public void Translate(float x, float y, float z, TransformProxy relativeTo)
		{
			Transform relativeTo2 = null;
			if (relativeTo != null)
			{
				relativeTo2 = relativeTo._value;
			}
			this._value.Translate(x, y, z, relativeTo2);
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x0003AD28 File Offset: 0x00038F28
		public bool CompareTag(string tag)
		{
			return this._value.CompareTag(tag);
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x0003AD36 File Offset: 0x00038F36
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x0003AD43 File Offset: 0x00038F43
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400329C RID: 12956
		[MoonSharpHidden]
		public Transform _value;
	}
}
