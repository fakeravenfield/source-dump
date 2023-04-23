using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x0200059D RID: 1437
	[Serializable]
	public class Serializer
	{
		// Token: 0x0600256B RID: 9579 RVA: 0x000EFBE0 File Offset: 0x000EDDE0
		public int Store(object obj, bool writeProperties = true)
		{
			if (obj == null)
			{
				return -1;
			}
			int count = this.entities.Count;
			for (int i = 0; i < count; i++)
			{
				if (obj == this.entities[i].obj)
				{
					return i;
				}
			}
			Serializer.SerializedObject serializedObject = new Serializer.SerializedObject();
			Type type = obj.GetType();
			serializedObject.typeName = type.AssemblyQualifiedName.ToString();
			serializedObject.obj = obj;
			this.entities.Add(serializedObject);
			int result = this.entities.Count - 1;
			if (type.IsArray)
			{
				Array array = (Array)obj;
				Type elementType = type.GetElementType();
				serializedObject.AddValues(elementType, array, this);
				return result;
			}
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (!fieldInfo.IsLiteral && !fieldInfo.FieldType.IsPointer && !fieldInfo.IsNotSerialized)
				{
					serializedObject.AddValue(fieldInfo.FieldType, fieldInfo.GetValue(obj), fieldInfo.Name, this);
				}
			}
			if (writeProperties)
			{
				foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
				{
					if (propertyInfo.CanWrite && !(propertyInfo.Name == "Item"))
					{
						serializedObject.AddValue(propertyInfo.PropertyType, propertyInfo.GetValue(obj, null), propertyInfo.Name, this);
					}
				}
			}
			return result;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000EFD50 File Offset: 0x000EDF50
		public object Retrieve(int num)
		{
			if (num < 0)
			{
				return null;
			}
			if (this.entities[num].obj != null)
			{
				return this.entities[num].obj;
			}
			Serializer.SerializedObject serializedObject = this.entities[num];
			Type type = Type.GetType(serializedObject.typeName);
			if (type == null)
			{
				type = Type.GetType(serializedObject.typeName.Substring(0, serializedObject.typeName.IndexOf(",")));
			}
			if (type == null)
			{
				return null;
			}
			if (type.IsArray)
			{
				Array values = serializedObject.GetValues(type.GetElementType(), this);
				serializedObject.obj = values;
				return values;
			}
			object obj = Activator.CreateInstance(type);
			serializedObject.obj = obj;
			foreach (FieldInfo fieldInfo in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (!fieldInfo.IsLiteral && !fieldInfo.FieldType.IsPointer && !fieldInfo.IsNotSerialized)
				{
					object value = null;
					try
					{
						value = serializedObject.GetValue(fieldInfo.FieldType, fieldInfo.Name, this);
					}
					catch (Exception ex)
					{
						string str = "Serialization error:\n";
						Exception ex2 = ex;
						Debug.LogError(str + ((ex2 != null) ? ex2.ToString() : null));
					}
					fieldInfo.SetValue(obj, value);
				}
			}
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (propertyInfo.CanWrite && !(propertyInfo.Name == "Item"))
				{
					object obj2 = null;
					try
					{
						obj2 = serializedObject.GetValue(propertyInfo.PropertyType, propertyInfo.Name, this);
					}
					catch (Exception ex3)
					{
						string str2 = "Serialization error:\n";
						Exception ex4 = ex3;
						Debug.LogError(str2 + ((ex4 != null) ? ex4.ToString() : null));
					}
					if (obj2 != null)
					{
						propertyInfo.SetValue(obj, obj2, null);
					}
				}
			}
			return obj;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000EFF40 File Offset: 0x000EE140
		public void ClearLinks()
		{
			for (int i = 0; i < this.entities.Count; i++)
			{
				this.entities[i].obj = null;
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00019F52 File Offset: 0x00018152
		public void Clear()
		{
			this.entities.Clear();
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000EFF78 File Offset: 0x000EE178
		public bool Equals(Serializer ser)
		{
			if (this.entities.Count != ser.entities.Count)
			{
				return false;
			}
			int count = this.entities.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.entities[i].Equals(ser.entities[i]))
				{
					Debug.Log(this.entities[i].typeName);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000EFFF0 File Offset: 0x000EE1F0
		public static object DeepCopy(object obj)
		{
			Serializer serializer = new Serializer();
			serializer.Store(obj, true);
			serializer.ClearLinks();
			object result = serializer.Retrieve(0);
			serializer.ClearLinks();
			return result;
		}

		// Token: 0x040023ED RID: 9197
		public List<Serializer.SerializedObject> entities = new List<Serializer.SerializedObject>();

		// Token: 0x0200059E RID: 1438
		[Serializable]
		public struct BaseValue
		{
			// Token: 0x040023EE RID: 9198
			public object val;

			// Token: 0x040023EF RID: 9199
			public string name;

			// Token: 0x040023F0 RID: 9200
			public bool property;
		}

		// Token: 0x0200059F RID: 1439
		[Serializable]
		public struct ObjectValue
		{
			// Token: 0x040023F1 RID: 9201
			public int link;

			// Token: 0x040023F2 RID: 9202
			public string name;

			// Token: 0x040023F3 RID: 9203
			public bool property;
		}

		// Token: 0x020005A0 RID: 1440
		[Serializable]
		public struct BoolValue
		{
			// Token: 0x040023F4 RID: 9204
			public bool val;

			// Token: 0x040023F5 RID: 9205
			public string name;

			// Token: 0x040023F6 RID: 9206
			public bool property;
		}

		// Token: 0x020005A1 RID: 1441
		[Serializable]
		public struct IntValue
		{
			// Token: 0x040023F7 RID: 9207
			public int val;

			// Token: 0x040023F8 RID: 9208
			public string name;

			// Token: 0x040023F9 RID: 9209
			public bool property;
		}

		// Token: 0x020005A2 RID: 1442
		[Serializable]
		public struct FloatValue
		{
			// Token: 0x040023FA RID: 9210
			public float val;

			// Token: 0x040023FB RID: 9211
			public string name;

			// Token: 0x040023FC RID: 9212
			public bool property;
		}

		// Token: 0x020005A3 RID: 1443
		[Serializable]
		public struct StringValue
		{
			// Token: 0x040023FD RID: 9213
			public string val;

			// Token: 0x040023FE RID: 9214
			public string name;

			// Token: 0x040023FF RID: 9215
			public bool property;
		}

		// Token: 0x020005A4 RID: 1444
		[Serializable]
		public struct CharValue
		{
			// Token: 0x04002400 RID: 9216
			public char val;

			// Token: 0x04002401 RID: 9217
			public string name;

			// Token: 0x04002402 RID: 9218
			public bool property;
		}

		// Token: 0x020005A5 RID: 1445
		[Serializable]
		public struct FloatArray
		{
			// Token: 0x04002403 RID: 9219
			public float[] val;

			// Token: 0x04002404 RID: 9220
			public string name;

			// Token: 0x04002405 RID: 9221
			public bool property;
		}

		// Token: 0x020005A6 RID: 1446
		[Serializable]
		public struct RectValue
		{
			// Token: 0x04002406 RID: 9222
			public Rect val;

			// Token: 0x04002407 RID: 9223
			public string name;

			// Token: 0x04002408 RID: 9224
			public bool property;
		}

		// Token: 0x020005A7 RID: 1447
		[Serializable]
		public struct UnityObjValue
		{
			// Token: 0x04002409 RID: 9225
			public UnityEngine.Object val;

			// Token: 0x0400240A RID: 9226
			public string name;

			// Token: 0x0400240B RID: 9227
			public bool property;
		}

		// Token: 0x020005A8 RID: 1448
		[Serializable]
		public class SerializedObject
		{
			// Token: 0x06002572 RID: 9586 RVA: 0x00019F72 File Offset: 0x00018172
			public IList GetListByType(Type type)
			{
				if (type == typeof(bool))
				{
					return this.bools;
				}
				if (type == typeof(int))
				{
					return this.ints;
				}
				return this.links;
			}

			// Token: 0x06002573 RID: 9587 RVA: 0x000F0020 File Offset: 0x000EE220
			public void AddValue(Type type, object val, string name, Serializer ser)
			{
				if (type == typeof(bool))
				{
					this.bools.Add(new Serializer.BoolValue
					{
						val = (bool)val,
						name = name
					});
					return;
				}
				if (type == typeof(int))
				{
					this.ints.Add(new Serializer.IntValue
					{
						val = (int)val,
						name = name
					});
					return;
				}
				if (type == typeof(float))
				{
					this.floats.Add(new Serializer.FloatValue
					{
						val = (float)val,
						name = name
					});
					return;
				}
				if (type == typeof(string))
				{
					this.strings.Add(new Serializer.StringValue
					{
						val = (string)val,
						name = name,
						property = false
					});
					return;
				}
				if (type == typeof(char))
				{
					this.chars.Add(new Serializer.CharValue
					{
						val = (char)val,
						name = name,
						property = false
					});
					return;
				}
				if (type == typeof(Rect))
				{
					this.rects.Add(new Serializer.RectValue
					{
						val = (Rect)val,
						name = name,
						property = false
					});
					return;
				}
				if (type.IsSubclassOf(typeof(UnityEngine.Object)))
				{
					this.unityObjs.Add(new Serializer.UnityObjValue
					{
						val = (UnityEngine.Object)val,
						name = name,
						property = false
					});
					return;
				}
				if (type == typeof(float[]))
				{
					this.floatArrays.Add(new Serializer.FloatArray
					{
						val = (float[])val,
						name = name,
						property = false
					});
					return;
				}
				if (type == typeof(AnimationCurve))
				{
					int index = ser.Store(val, true);
					ser.entities[index].AddValue(typeof(Keyframe[]), ((AnimationCurve)val).keys, "keys", ser);
					this.links.Add(new Serializer.ObjectValue
					{
						link = ser.Store(val, true),
						name = name
					});
					return;
				}
				if (type == typeof(Keyframe))
				{
					int index2 = ser.Store(val, true);
					Keyframe keyframe = (Keyframe)val;
					ser.entities[index2].AddValue(typeof(float), keyframe.time, "time", ser);
					ser.entities[index2].AddValue(typeof(float), keyframe.value, "value", ser);
					ser.entities[index2].AddValue(typeof(float), keyframe.inTangent, "inTangent", ser);
					ser.entities[index2].AddValue(typeof(float), keyframe.outTangent, "outTangent", ser);
					this.links.Add(new Serializer.ObjectValue
					{
						link = ser.Store(val, true),
						name = name
					});
					return;
				}
				if (type == typeof(Matrix) || type == typeof(CoordRect) || type == typeof(Coord))
				{
					this.links.Add(new Serializer.ObjectValue
					{
						link = ser.Store(val, false),
						name = name
					});
					return;
				}
				this.links.Add(new Serializer.ObjectValue
				{
					link = ser.Store(val, true),
					name = name
				});
			}

			// Token: 0x06002574 RID: 9588 RVA: 0x000F045C File Offset: 0x000EE65C
			public void AddValues(Type type, Array array, Serializer ser)
			{
				if (type == typeof(bool))
				{
					for (int i = 0; i < array.Length; i++)
					{
						this.bools.Add(new Serializer.BoolValue
						{
							val = (bool)array.GetValue(i)
						});
					}
					return;
				}
				if (type == typeof(int))
				{
					for (int j = 0; j < array.Length; j++)
					{
						this.ints.Add(new Serializer.IntValue
						{
							val = (int)array.GetValue(j)
						});
					}
					return;
				}
				if (type == typeof(float))
				{
					for (int k = 0; k < array.Length; k++)
					{
						this.floats.Add(new Serializer.FloatValue
						{
							val = (float)array.GetValue(k)
						});
					}
					return;
				}
				for (int l = 0; l < array.Length; l++)
				{
					this.AddValue(type, array.GetValue(l), "", ser);
				}
			}

			// Token: 0x06002575 RID: 9589 RVA: 0x000F057C File Offset: 0x000EE77C
			public object GetValue(Type type, string name, Serializer ser)
			{
				if (type == typeof(bool))
				{
					for (int i = 0; i < this.bools.Count; i++)
					{
						if (this.bools[i].name == name)
						{
							return this.bools[i].val;
						}
					}
				}
				else if (type == typeof(int))
				{
					for (int j = 0; j < this.ints.Count; j++)
					{
						if (this.ints[j].name == name)
						{
							return this.ints[j].val;
						}
					}
				}
				else if (type == typeof(float))
				{
					for (int k = 0; k < this.floats.Count; k++)
					{
						if (this.floats[k].name == name)
						{
							return this.floats[k].val;
						}
					}
				}
				else if (type == typeof(string))
				{
					for (int l = 0; l < this.strings.Count; l++)
					{
						if (this.strings[l].name == name)
						{
							return this.strings[l].val;
						}
					}
				}
				else if (type == typeof(char))
				{
					for (int m = 0; m < this.chars.Count; m++)
					{
						if (this.chars[m].name == name)
						{
							return this.chars[m].val;
						}
					}
				}
				else if (type == typeof(Rect))
				{
					for (int n = 0; n < this.rects.Count; n++)
					{
						if (this.rects[n].name == name)
						{
							return this.rects[n].val;
						}
					}
				}
				else if (type.IsSubclassOf(typeof(UnityEngine.Object)))
				{
					for (int num = 0; num < this.unityObjs.Count; num++)
					{
						if (this.unityObjs[num].name == name)
						{
							try
							{
								if (this.unityObjs[num].val.GetType() == typeof(UnityEngine.Object))
								{
									return null;
								}
							}
							catch
							{
								return null;
							}
							return this.unityObjs[num].val;
						}
					}
				}
				else if (type == typeof(float[]))
				{
					for (int num2 = 0; num2 < this.floatArrays.Count; num2++)
					{
						if (this.floatArrays[num2].name == name)
						{
							return this.floatArrays[num2].val;
						}
					}
				}
				else
				{
					for (int num3 = 0; num3 < this.links.Count; num3++)
					{
						if (this.links[num3].name == name)
						{
							return ser.Retrieve(this.links[num3].link);
						}
					}
				}
				return null;
			}

			// Token: 0x06002576 RID: 9590 RVA: 0x000F0918 File Offset: 0x000EEB18
			public Array GetValues(Type elementType, Serializer ser)
			{
				IList listByType = this.GetListByType(elementType);
				Array array = Array.CreateInstance(elementType, listByType.Count);
				if (elementType == typeof(bool))
				{
					for (int i = 0; i < this.bools.Count; i++)
					{
						array.SetValue(this.bools[i].val, i);
					}
				}
				else if (elementType == typeof(int))
				{
					for (int j = 0; j < this.ints.Count; j++)
					{
						array.SetValue(this.ints[j].val, j);
					}
				}
				else if (elementType == typeof(float))
				{
					for (int k = 0; k < this.floats.Count; k++)
					{
						array.SetValue(this.floats[k].val, k);
					}
				}
				else if (elementType == typeof(string))
				{
					for (int l = 0; l < this.strings.Count; l++)
					{
						array.SetValue(this.strings[l].val, l);
					}
				}
				else if (elementType == typeof(char))
				{
					for (int m = 0; m < this.chars.Count; m++)
					{
						array.SetValue(this.chars[m].val, m);
					}
				}
				else if (elementType == typeof(Rect))
				{
					for (int n = 0; n < this.rects.Count; n++)
					{
						array.SetValue(this.rects[n].val, n);
					}
				}
				else if (elementType.IsSubclassOf(typeof(UnityEngine.Object)))
				{
					for (int num = 0; num < this.unityObjs.Count; num++)
					{
						array.SetValue(this.unityObjs[num].val, num);
					}
				}
				else if (elementType == typeof(float[]))
				{
					for (int num2 = 0; num2 < this.floatArrays.Count; num2++)
					{
						array.SetValue(this.floatArrays[num2].val, num2);
					}
				}
				else
				{
					for (int num3 = 0; num3 < this.links.Count; num3++)
					{
						array.SetValue(ser.Retrieve(this.links[num3].link), num3);
					}
				}
				return array;
			}

			// Token: 0x06002577 RID: 9591 RVA: 0x000F0BD8 File Offset: 0x000EEDD8
			public bool Equals(Serializer.SerializedObject obj)
			{
				if (this.bools.Count != obj.bools.Count)
				{
					return false;
				}
				for (int i = this.bools.Count - 1; i >= 0; i--)
				{
					if (this.bools[i].val != obj.bools[i].val || this.bools[i].name != obj.bools[i].name)
					{
						return false;
					}
				}
				if (this.ints.Count != obj.ints.Count)
				{
					return false;
				}
				for (int j = this.ints.Count - 1; j >= 0; j--)
				{
					if (this.ints[j].val != obj.ints[j].val || this.ints[j].name != obj.ints[j].name)
					{
						return false;
					}
				}
				if (this.floats.Count != obj.floats.Count)
				{
					return false;
				}
				for (int k = this.floats.Count - 1; k >= 0; k--)
				{
					if (this.floats[k].val != obj.floats[k].val || this.floats[k].name != obj.floats[k].name)
					{
						return false;
					}
				}
				if (this.strings.Count != obj.strings.Count)
				{
					return false;
				}
				for (int l = this.strings.Count - 1; l >= 0; l--)
				{
					if (this.strings[l].val != obj.strings[l].val || this.strings[l].name != obj.strings[l].name)
					{
						return false;
					}
				}
				if (this.chars.Count != obj.chars.Count)
				{
					return false;
				}
				for (int m = this.chars.Count - 1; m >= 0; m--)
				{
					if (this.chars[m].val != obj.chars[m].val || this.chars[m].name != obj.chars[m].name)
					{
						return false;
					}
				}
				if (this.unityObjs.Count != obj.unityObjs.Count)
				{
					return false;
				}
				for (int n = this.unityObjs.Count - 1; n >= 0; n--)
				{
					if (this.unityObjs[n].val != obj.unityObjs[n].val || this.unityObjs[n].name != obj.unityObjs[n].name)
					{
						return false;
					}
				}
				if (this.floatArrays.Count != obj.floatArrays.Count)
				{
					return false;
				}
				for (int num = this.floatArrays.Count - 1; num >= 0; num--)
				{
					if (this.floatArrays[num].name != obj.floatArrays[num].name)
					{
						return false;
					}
					if (this.floatArrays[num].val.Length != obj.floatArrays[num].val.Length)
					{
						return false;
					}
					for (int num2 = 0; num2 < this.floatArrays[num].val.Length; num2++)
					{
						if (this.floatArrays[num].val[num2] != obj.floatArrays[num].val[num2])
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x0400240C RID: 9228
			public object obj;

			// Token: 0x0400240D RID: 9229
			public string typeName;

			// Token: 0x0400240E RID: 9230
			public List<Serializer.ObjectValue> links = new List<Serializer.ObjectValue>();

			// Token: 0x0400240F RID: 9231
			public List<Serializer.BoolValue> bools = new List<Serializer.BoolValue>();

			// Token: 0x04002410 RID: 9232
			public List<Serializer.IntValue> ints = new List<Serializer.IntValue>();

			// Token: 0x04002411 RID: 9233
			public List<Serializer.FloatValue> floats = new List<Serializer.FloatValue>();

			// Token: 0x04002412 RID: 9234
			public List<Serializer.StringValue> strings = new List<Serializer.StringValue>();

			// Token: 0x04002413 RID: 9235
			public List<Serializer.CharValue> chars = new List<Serializer.CharValue>();

			// Token: 0x04002414 RID: 9236
			public List<Serializer.FloatArray> floatArrays = new List<Serializer.FloatArray>();

			// Token: 0x04002415 RID: 9237
			public List<Serializer.RectValue> rects = new List<Serializer.RectValue>();

			// Token: 0x04002416 RID: 9238
			public List<Serializer.UnityObjValue> unityObjs = new List<Serializer.UnityObjValue>();
		}
	}
}
