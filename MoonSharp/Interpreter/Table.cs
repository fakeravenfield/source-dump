using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter
{
	// Token: 0x020007BA RID: 1978
	public class Table : RefIdObject, IScriptPrivateResource
	{
		// Token: 0x0600310D RID: 12557 RVA: 0x0010ED30 File Offset: 0x0010CF30
		public Table(Script owner)
		{
			this.m_Values = new LinkedList<TablePair>();
			this.m_StringMap = new LinkedListIndex<string, TablePair>(this.m_Values);
			this.m_ArrayMap = new LinkedListIndex<int, TablePair>(this.m_Values);
			this.m_ValueMap = new LinkedListIndex<DynValue, TablePair>(this.m_Values);
			this.m_Owner = owner;
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x0010ED90 File Offset: 0x0010CF90
		public Table(Script owner, params DynValue[] arrayValues) : this(owner)
		{
			for (int i = 0; i < arrayValues.Length; i++)
			{
				this.Set(DynValue.NewNumber((double)(i + 1)), arrayValues[i]);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x00021C92 File Offset: 0x0001FE92
		public Script OwnerScript
		{
			get
			{
				return this.m_Owner;
			}
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x00021C9A File Offset: 0x0001FE9A
		public void Clear()
		{
			this.m_Values.Clear();
			this.m_StringMap.Clear();
			this.m_ArrayMap.Clear();
			this.m_ValueMap.Clear();
			this.m_CachedLength = -1;
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0010EDC4 File Offset: 0x0010CFC4
		private int GetIntegralKey(double d)
		{
			int num = (int)d;
			if (d >= 1.0 && d == (double)num)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x170003E8 RID: 1000
		public object this[params object[] keys]
		{
			get
			{
				return this.Get(keys).ToObject();
			}
			set
			{
				this.Set(keys, DynValue.FromObject(this.OwnerScript, value));
			}
		}

		// Token: 0x170003E9 RID: 1001
		public object this[object key]
		{
			get
			{
				return this.Get(key).ToObject();
			}
			set
			{
				this.Set(key, DynValue.FromObject(this.OwnerScript, value));
			}
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0010EDE8 File Offset: 0x0010CFE8
		private Table ResolveMultipleKeys(object[] keys, out object key)
		{
			Table table = this;
			key = ((keys.Length != 0) ? keys[0] : null);
			for (int i = 1; i < keys.Length; i++)
			{
				DynValue dynValue = table.RawGet(key);
				if (dynValue == null)
				{
					throw new ScriptRuntimeException("Key '{0}' did not point to anything");
				}
				if (dynValue.Type != DataType.Table)
				{
					throw new ScriptRuntimeException("Key '{0}' did not point to a table");
				}
				table = dynValue.Table;
				key = keys[i];
			}
			return table;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x00021D15 File Offset: 0x0001FF15
		public void Append(DynValue value)
		{
			this.CheckScriptOwnership(value);
			this.PerformTableSet<int>(this.m_ArrayMap, this.Length + 1, DynValue.NewNumber((double)(this.Length + 1)), value, true, this.Length + 1);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x0010EE48 File Offset: 0x0010D048
		private void PerformTableSet<T>(LinkedListIndex<T, TablePair> listIndex, T key, DynValue keyDynValue, DynValue value, bool isNumber, int appendKey)
		{
			TablePair tablePair = listIndex.Set(key, new TablePair(keyDynValue, value));
			if (this.m_ContainsNilEntries && value.IsNotNil() && (tablePair.Value == null || tablePair.Value.IsNil()))
			{
				this.CollectDeadKeys();
				return;
			}
			if (value.IsNil())
			{
				this.m_ContainsNilEntries = true;
				if (isNumber)
				{
					this.m_CachedLength = -1;
					return;
				}
			}
			else if (isNumber && (tablePair.Value == null || tablePair.Value.IsNilOrNan()))
			{
				if (appendKey >= 0)
				{
					LinkedListNode<TablePair> linkedListNode = this.m_ArrayMap.Find(appendKey + 1);
					if (linkedListNode == null || linkedListNode.Value.Value == null || linkedListNode.Value.Value.IsNil())
					{
						this.m_CachedLength++;
						return;
					}
					this.m_CachedLength = -1;
					return;
				}
				else
				{
					this.m_CachedLength = -1;
				}
			}
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x00021D4A File Offset: 0x0001FF4A
		public void Set(string key, DynValue value)
		{
			if (key == null)
			{
				throw ScriptRuntimeException.TableIndexIsNil();
			}
			this.CheckScriptOwnership(value);
			this.PerformTableSet<string>(this.m_StringMap, key, DynValue.NewString(key), value, false, -1);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x00021D72 File Offset: 0x0001FF72
		public void Set(int key, DynValue value)
		{
			this.CheckScriptOwnership(value);
			this.PerformTableSet<int>(this.m_ArrayMap, key, DynValue.NewNumber((double)key), value, true, -1);
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x0010EF28 File Offset: 0x0010D128
		public void Set(DynValue key, DynValue value)
		{
			if (key.IsNilOrNan())
			{
				if (key.IsNil())
				{
					throw ScriptRuntimeException.TableIndexIsNil();
				}
				throw ScriptRuntimeException.TableIndexIsNaN();
			}
			else
			{
				if (key.Type == DataType.String)
				{
					this.Set(key.String, value);
					return;
				}
				if (key.Type == DataType.Number)
				{
					int integralKey = this.GetIntegralKey(key.Number);
					if (integralKey > 0)
					{
						this.Set(integralKey, value);
						return;
					}
				}
				this.CheckScriptOwnership(key);
				this.CheckScriptOwnership(value);
				this.PerformTableSet<DynValue>(this.m_ValueMap, key, key, value, false, -1);
				return;
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x0010EFAC File Offset: 0x0010D1AC
		public void Set(object key, DynValue value)
		{
			if (key == null)
			{
				throw ScriptRuntimeException.TableIndexIsNil();
			}
			if (key is string)
			{
				this.Set((string)key, value);
				return;
			}
			if (key is int)
			{
				this.Set((int)key, value);
				return;
			}
			this.Set(DynValue.FromObject(this.OwnerScript, key), value);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x0010F004 File Offset: 0x0010D204
		public void Set(object[] keys, DynValue value)
		{
			if (keys == null || keys.Length == 0)
			{
				throw ScriptRuntimeException.TableIndexIsNil();
			}
			object key;
			this.ResolveMultipleKeys(keys, out key).Set(key, value);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x00021D92 File Offset: 0x0001FF92
		public DynValue Get(string key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x00021DA4 File Offset: 0x0001FFA4
		public DynValue Get(int key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x00021DB6 File Offset: 0x0001FFB6
		public DynValue Get(DynValue key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		public DynValue Get(object key)
		{
			return this.RawGet(key) ?? DynValue.Nil;
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x00021DDA File Offset: 0x0001FFDA
		public DynValue Get(params object[] keys)
		{
			return this.RawGet(keys) ?? DynValue.Nil;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x0010F030 File Offset: 0x0010D230
		private static DynValue RawGetValue(LinkedListNode<TablePair> linkedListNode)
		{
			if (linkedListNode == null)
			{
				return null;
			}
			return linkedListNode.Value.Value;
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x00021DEC File Offset: 0x0001FFEC
		public DynValue RawGet(string key)
		{
			return Table.RawGetValue(this.m_StringMap.Find(key));
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x00021DFF File Offset: 0x0001FFFF
		public DynValue RawGet(int key)
		{
			return Table.RawGetValue(this.m_ArrayMap.Find(key));
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x0010F050 File Offset: 0x0010D250
		public DynValue RawGet(DynValue key)
		{
			if (key.Type == DataType.String)
			{
				return this.RawGet(key.String);
			}
			if (key.Type == DataType.Number)
			{
				int integralKey = this.GetIntegralKey(key.Number);
				if (integralKey > 0)
				{
					return this.RawGet(integralKey);
				}
			}
			return Table.RawGetValue(this.m_ValueMap.Find(key));
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x0010F0A8 File Offset: 0x0010D2A8
		public DynValue RawGet(object key)
		{
			if (key == null)
			{
				return null;
			}
			if (key is string)
			{
				return this.RawGet((string)key);
			}
			if (key is int)
			{
				return this.RawGet((int)key);
			}
			return this.RawGet(DynValue.FromObject(this.OwnerScript, key));
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x0010F0F8 File Offset: 0x0010D2F8
		public DynValue RawGet(params object[] keys)
		{
			if (keys == null || keys.Length == 0)
			{
				return null;
			}
			object key;
			return this.ResolveMultipleKeys(keys, out key).RawGet(key);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x00021E12 File Offset: 0x00020012
		private bool PerformTableRemove<T>(LinkedListIndex<T, TablePair> listIndex, T key, bool isNumber)
		{
			bool flag = listIndex.Remove(key);
			if (flag && isNumber)
			{
				this.m_CachedLength = -1;
			}
			return flag;
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x00021E27 File Offset: 0x00020027
		public bool Remove(string key)
		{
			return this.PerformTableRemove<string>(this.m_StringMap, key, false);
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x00021E37 File Offset: 0x00020037
		public bool Remove(int key)
		{
			return this.PerformTableRemove<int>(this.m_ArrayMap, key, true);
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x0010F120 File Offset: 0x0010D320
		public bool Remove(DynValue key)
		{
			if (key.Type == DataType.String)
			{
				return this.Remove(key.String);
			}
			if (key.Type == DataType.Number)
			{
				int integralKey = this.GetIntegralKey(key.Number);
				if (integralKey > 0)
				{
					return this.Remove(integralKey);
				}
			}
			return this.PerformTableRemove<DynValue>(this.m_ValueMap, key, false);
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x00021E47 File Offset: 0x00020047
		public bool Remove(object key)
		{
			if (key is string)
			{
				return this.Remove((string)key);
			}
			if (key is int)
			{
				return this.Remove((int)key);
			}
			return this.Remove(DynValue.FromObject(this.OwnerScript, key));
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x0010F174 File Offset: 0x0010D374
		public bool Remove(params object[] keys)
		{
			object key;
			return keys != null && keys.Length != 0 && this.ResolveMultipleKeys(keys, out key).Remove(key);
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x0010F19C File Offset: 0x0010D39C
		public void CollectDeadKeys()
		{
			for (LinkedListNode<TablePair> linkedListNode = this.m_Values.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value.Value.IsNil())
				{
					this.Remove(linkedListNode.Value.Key);
				}
			}
			this.m_ContainsNilEntries = false;
			this.m_CachedLength = -1;
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x0010F1FC File Offset: 0x0010D3FC
		public TablePair? NextKey(DynValue v)
		{
			if (v.IsNil())
			{
				LinkedListNode<TablePair> first = this.m_Values.First;
				if (first == null)
				{
					return new TablePair?(TablePair.Nil);
				}
				if (first.Value.Value.IsNil())
				{
					return this.NextKey(first.Value.Key);
				}
				return new TablePair?(first.Value);
			}
			else
			{
				if (v.Type == DataType.String)
				{
					return this.GetNextOf(this.m_StringMap.Find(v.String));
				}
				if (v.Type == DataType.Number)
				{
					int integralKey = this.GetIntegralKey(v.Number);
					if (integralKey > 0)
					{
						return this.GetNextOf(this.m_ArrayMap.Find(integralKey));
					}
				}
				return this.GetNextOf(this.m_ValueMap.Find(v));
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x0010F2C4 File Offset: 0x0010D4C4
		private TablePair? GetNextOf(LinkedListNode<TablePair> linkedListNode)
		{
			while (linkedListNode != null)
			{
				if (linkedListNode.Next == null)
				{
					return new TablePair?(TablePair.Nil);
				}
				linkedListNode = linkedListNode.Next;
				if (!linkedListNode.Value.Value.IsNil())
				{
					return new TablePair?(linkedListNode.Value);
				}
			}
			return null;
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x0010F31C File Offset: 0x0010D51C
		public int Length
		{
			get
			{
				if (this.m_CachedLength < 0)
				{
					this.m_CachedLength = 0;
					int num = 1;
					while (this.m_ArrayMap.ContainsKey(num) && !this.m_ArrayMap.Find(num).Value.Value.IsNil())
					{
						this.m_CachedLength = num;
						num++;
					}
				}
				return this.m_CachedLength;
			}
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x0010F37C File Offset: 0x0010D57C
		internal void InitNextArrayKeys(DynValue val, bool lastpos)
		{
			if (val.Type == DataType.Tuple && lastpos)
			{
				foreach (DynValue val2 in val.Tuple)
				{
					this.InitNextArrayKeys(val2, true);
				}
				return;
			}
			int i = this.m_InitArray + 1;
			this.m_InitArray = i;
			this.Set(i, val.ToScalar());
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x00021E85 File Offset: 0x00020085
		// (set) Token: 0x06003135 RID: 12597 RVA: 0x00021E8D File Offset: 0x0002008D
		public Table MetaTable
		{
			get
			{
				return this.m_MetaTable;
			}
			set
			{
				this.CheckScriptOwnership(this.m_MetaTable);
				this.m_MetaTable = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x00021EA2 File Offset: 0x000200A2
		public IEnumerable<TablePair> Pairs
		{
			get
			{
				return from n in this.m_Values
				select new TablePair(n.Key, n.Value);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x00021ECE File Offset: 0x000200CE
		public IEnumerable<DynValue> Keys
		{
			get
			{
				return from n in this.m_Values
				select n.Key;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x00021EFA File Offset: 0x000200FA
		public IEnumerable<DynValue> Values
		{
			get
			{
				return from n in this.m_Values
				select n.Value;
			}
		}

		// Token: 0x04002C0A RID: 11274
		private readonly LinkedList<TablePair> m_Values;

		// Token: 0x04002C0B RID: 11275
		private readonly LinkedListIndex<DynValue, TablePair> m_ValueMap;

		// Token: 0x04002C0C RID: 11276
		private readonly LinkedListIndex<string, TablePair> m_StringMap;

		// Token: 0x04002C0D RID: 11277
		private readonly LinkedListIndex<int, TablePair> m_ArrayMap;

		// Token: 0x04002C0E RID: 11278
		private readonly Script m_Owner;

		// Token: 0x04002C0F RID: 11279
		private int m_InitArray;

		// Token: 0x04002C10 RID: 11280
		private int m_CachedLength = -1;

		// Token: 0x04002C11 RID: 11281
		private bool m_ContainsNilEntries;

		// Token: 0x04002C12 RID: 11282
		private Table m_MetaTable;
	}
}
