using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace MapMagic
{
	// Token: 0x02000559 RID: 1369
	public static class CustomSerialization
	{
		// Token: 0x06002293 RID: 8851 RVA: 0x000DD49C File Offset: 0x000DB69C
		private static Type GetStandardAssembliesType(string s)
		{
			if (s.StartsWith("Plugins."))
			{
				s = s.Replace("Plugins", typeof(CustomSerialization).Namespace);
			}
			Type type = Type.GetType(s);
			if (type == null)
			{
				type = Type.GetType(s + ", UnityEngine");
			}
			if (type == null)
			{
				type = Type.GetType(s + ", Assembly-CSharp-Editor");
			}
			if (type == null)
			{
				type = Type.GetType(s + ", Assembly-CSharp");
			}
			return type;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0001839B File Offset: 0x0001659B
		private static IEnumerable<CustomSerialization.Value> Values(object obj)
		{
			Type objType = obj.GetType();
			if (objType.IsArray)
			{
				Type elementType = objType.GetElementType();
				Array array = (Array)obj;
				if (elementType.IsPrimitive)
				{
					yield return new CustomSerialization.Value
					{
						name = "items",
						type = objType,
						obj = array
					};
				}
				else
				{
					int num;
					for (int i = 0; i < array.Length; i = num + 1)
					{
						yield return new CustomSerialization.Value
						{
							name = "item" + i.ToString(),
							type = elementType,
							obj = array.GetValue(i)
						};
						num = i;
					}
				}
				elementType = null;
				array = null;
			}
			else if (objType.IsSubclassOf(typeof(UnityEngine.Object)))
			{
				yield return new CustomSerialization.Value
				{
					name = "Object",
					type = objType,
					obj = obj
				};
			}
			else
			{
				foreach (FieldInfo fieldInfo in objType.UsableFields(false))
				{
					yield return new CustomSerialization.Value
					{
						name = fieldInfo.Name,
						type = fieldInfo.FieldType,
						obj = fieldInfo.GetValue(obj)
					};
				}
				IEnumerator<FieldInfo> enumerator = null;
				foreach (PropertyInfo propertyInfo in objType.UsableProperties(false, true))
				{
					yield return new CustomSerialization.Value
					{
						name = propertyInfo.Name,
						type = propertyInfo.PropertyType,
						obj = propertyInfo.GetValue(obj, null)
					};
				}
				IEnumerator<PropertyInfo> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000DD528 File Offset: 0x000DB728
		public static int WriteClass(object obj, List<string> classes, List<UnityEngine.Object> objects, List<float> floats, List<object> references)
		{
			if (references.Contains(obj))
			{
				return references.IndexOf(obj);
			}
			Type type = obj.GetType();
			Type type2 = type.IsArray ? type.GetElementType() : null;
			string str = type.ToString();
			int count = classes.Count;
			classes.Add(null);
			for (int i = references.Count; i < classes.Count; i++)
			{
				references.Add(null);
			}
			references[count] = obj;
			StringWriter stringWriter = new StringWriter();
			stringWriter.Write("<" + str);
			if (type.IsArray)
			{
				stringWriter.Write(" length=" + ((Array)obj).Length.ToString());
			}
			stringWriter.WriteLine(">");
			Func<object, int> <>9__0;
			foreach (CustomSerialization.Value value in CustomSerialization.Values(obj))
			{
				if (value.type.IsPrimitive)
				{
					TextWriter textWriter = stringWriter;
					string[] array = new string[7];
					array[0] = "\t<";
					array[1] = value.name;
					array[2] = " type=";
					int num = 3;
					Type type3 = value.type;
					array[num] = ((type3 != null) ? type3.ToString() : null);
					array[4] = " value=";
					int num2 = 5;
					object obj2 = value.obj;
					array[num2] = ((obj2 != null) ? obj2.ToString() : null);
					array[6] = "/>";
					textWriter.WriteLine(string.Concat(array));
				}
				else if (value.obj == null)
				{
					TextWriter textWriter2 = stringWriter;
					string[] array2 = new string[5];
					array2[0] = "\t<";
					array2[1] = value.name;
					array2[2] = " type=";
					int num3 = 3;
					Type type4 = value.type;
					array2[num3] = ((type4 != null) ? type4.ToString() : null);
					array2[4] = " null/>";
					textWriter2.WriteLine(string.Concat(array2));
				}
				else if (value.type == typeof(string))
				{
					string text = (string)value.obj;
					text = text.Replace("\n", "\\n");
					TextWriter textWriter3 = stringWriter;
					string[] array3 = new string[7];
					array3[0] = "\t<";
					array3[1] = value.name;
					array3[2] = " type=";
					int num4 = 3;
					Type type5 = value.type;
					array3[num4] = ((type5 != null) ? type5.ToString() : null);
					array3[4] = " value=";
					array3[5] = text;
					array3[6] = "/>";
					textWriter3.WriteLine(string.Concat(array3));
				}
				else if (typeof(CustomSerialization.IStruct).IsAssignableFrom(value.type))
				{
					TextWriter textWriter4 = stringWriter;
					string[] array4 = new string[7];
					array4[0] = "\t<";
					array4[1] = value.name;
					array4[2] = " type=";
					int num5 = 3;
					Type type6 = value.type;
					array4[num5] = ((type6 != null) ? type6.ToString() : null);
					array4[4] = " ";
					array4[5] = ((CustomSerialization.IStruct)value.obj).Encode();
					array4[6] = "/>";
					textWriter4.WriteLine(string.Concat(array4));
				}
				else if (typeof(CustomSerialization.IStructLink).IsAssignableFrom(value.type))
				{
					Func<object, int> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = ((object linkObj) => CustomSerialization.WriteClass(linkObj, classes, objects, floats, references)));
					}
					Func<object, int> writeClass = func;
					TextWriter textWriter5 = stringWriter;
					string[] array5 = new string[7];
					array5[0] = "\t<";
					array5[1] = value.name;
					array5[2] = " type=";
					int num6 = 3;
					Type type7 = value.type;
					array5[num6] = ((type7 != null) ? type7.ToString() : null);
					array5[4] = " ";
					array5[5] = ((CustomSerialization.IStructLink)value.obj).Encode(writeClass);
					array5[6] = "/>";
					textWriter5.WriteLine(string.Concat(array5));
				}
				else if (type == typeof(float[]))
				{
					float[] array6 = (float[])obj;
					TextWriter textWriter6 = stringWriter;
					string[] array7 = new string[7];
					array7[0] = "\t<items type=";
					int num7 = 1;
					Type type8 = value.type;
					array7[num7] = ((type8 != null) ? type8.ToString() : null);
					array7[2] = " start=";
					array7[3] = floats.Count.ToString();
					array7[4] = " length=";
					array7[5] = array6.Length.ToString();
					array7[6] = "/>";
					textWriter6.WriteLine(string.Concat(array7));
					floats.AddRange(array6);
				}
				else if (type.IsArray && type2.IsPrimitive)
				{
					TextWriter textWriter7 = stringWriter;
					string str2 = "\t<items type=";
					Type type9 = value.type;
					textWriter7.Write(str2 + ((type9 != null) ? type9.ToString() : null) + " values=");
					Array array8 = (Array)obj;
					for (int j = 0; j < array8.Length; j++)
					{
						stringWriter.Write(array8.GetValue(j));
						if (j != array8.Length - 1)
						{
							stringWriter.Write(',');
						}
					}
					stringWriter.WriteLine("/>");
				}
				else if (value.type.IsSubclassOf(typeof(UnityEngine.Object)))
				{
					TextWriter textWriter8 = stringWriter;
					string[] array9 = new string[7];
					array9[0] = "\t<";
					array9[1] = value.name;
					array9[2] = " type=";
					int num8 = 3;
					Type type10 = value.type;
					array9[num8] = ((type10 != null) ? type10.ToString() : null);
					array9[4] = " object=";
					array9[5] = objects.Count.ToString();
					array9[6] = "/>";
					textWriter8.WriteLine(string.Concat(array9));
					objects.Add((UnityEngine.Object)value.obj);
				}
				else if (value.obj == null)
				{
					TextWriter textWriter9 = stringWriter;
					string[] array10 = new string[5];
					array10[0] = "\t<";
					array10[1] = value.name;
					array10[2] = " type=";
					int num9 = 3;
					Type type11 = value.type;
					array10[num9] = ((type11 != null) ? type11.ToString() : null);
					array10[4] = " link=-1/>";
					textWriter9.WriteLine(string.Concat(array10));
				}
				else if (value.type.IsClass && !value.type.IsValueType)
				{
					TextWriter textWriter10 = stringWriter;
					string[] array11 = new string[7];
					array11[0] = "\t<";
					array11[1] = value.name;
					array11[2] = " type=";
					int num10 = 3;
					Type type12 = value.type;
					array11[num10] = ((type12 != null) ? type12.ToString() : null);
					array11[4] = " link=";
					array11[5] = CustomSerialization.WriteClass(value.obj, classes, objects, floats, references).ToString();
					array11[6] = "/>";
					textWriter10.WriteLine(string.Concat(array11));
				}
				else if (value.type == typeof(Vector2))
				{
					Vector2 vector = (Vector2)value.obj;
					TextWriter textWriter11 = stringWriter;
					string[] array12 = new string[9];
					array12[0] = "\t<";
					array12[1] = value.name;
					array12[2] = " type=";
					int num11 = 3;
					Type type13 = value.type;
					array12[num11] = ((type13 != null) ? type13.ToString() : null);
					array12[4] = " x=";
					array12[5] = vector.x.ToString();
					array12[6] = " y=";
					array12[7] = vector.y.ToString();
					array12[8] = "/>";
					textWriter11.WriteLine(string.Concat(array12));
				}
				else if (value.type == typeof(Vector3))
				{
					Vector3 vector2 = (Vector3)value.obj;
					TextWriter textWriter12 = stringWriter;
					string[] array13 = new string[11];
					array13[0] = "\t<";
					array13[1] = value.name;
					array13[2] = " type=";
					int num12 = 3;
					Type type14 = value.type;
					array13[num12] = ((type14 != null) ? type14.ToString() : null);
					array13[4] = " x=";
					array13[5] = vector2.x.ToString();
					array13[6] = " y=";
					array13[7] = vector2.y.ToString();
					array13[8] = " z=";
					array13[9] = vector2.z.ToString();
					array13[10] = "/>";
					textWriter12.WriteLine(string.Concat(array13));
				}
				else if (value.type == typeof(Rect))
				{
					Rect rect = (Rect)value.obj;
					TextWriter textWriter13 = stringWriter;
					string[] array14 = new string[13];
					array14[0] = "\t<";
					array14[1] = value.name;
					array14[2] = " type=";
					int num13 = 3;
					Type type15 = value.type;
					array14[num13] = ((type15 != null) ? type15.ToString() : null);
					array14[4] = " x=";
					array14[5] = rect.x.ToString();
					array14[6] = " y=";
					array14[7] = rect.y.ToString();
					array14[8] = " width=";
					array14[9] = rect.width.ToString();
					array14[10] = " height=";
					array14[11] = rect.height.ToString();
					array14[12] = "/>";
					textWriter13.WriteLine(string.Concat(array14));
				}
				else if (value.type == typeof(Color))
				{
					Color color = (Color)value.obj;
					TextWriter textWriter14 = stringWriter;
					string[] array15 = new string[13];
					array15[0] = "\t<";
					array15[1] = value.name;
					array15[2] = " type=";
					int num14 = 3;
					Type type16 = value.type;
					array15[num14] = ((type16 != null) ? type16.ToString() : null);
					array15[4] = " r=";
					array15[5] = color.r.ToString();
					array15[6] = " g=";
					array15[7] = color.g.ToString();
					array15[8] = " b=";
					array15[9] = color.b.ToString();
					array15[10] = " a=";
					array15[11] = color.a.ToString();
					array15[12] = "/>";
					textWriter14.WriteLine(string.Concat(array15));
				}
				else if (value.type == typeof(Vector4))
				{
					Vector4 vector3 = (Vector4)value.obj;
					TextWriter textWriter15 = stringWriter;
					string[] array16 = new string[13];
					array16[0] = "\t<";
					array16[1] = value.name;
					array16[2] = " type=";
					int num15 = 3;
					Type type17 = value.type;
					array16[num15] = ((type17 != null) ? type17.ToString() : null);
					array16[4] = " x=";
					array16[5] = vector3.x.ToString();
					array16[6] = " y=";
					array16[7] = vector3.y.ToString();
					array16[8] = " z=";
					array16[9] = vector3.z.ToString();
					array16[10] = " w=";
					array16[11] = vector3.w.ToString();
					array16[12] = "/>";
					textWriter15.WriteLine(string.Concat(array16));
				}
				else if (value.type == typeof(Quaternion))
				{
					Quaternion quaternion = (Quaternion)value.obj;
					TextWriter textWriter16 = stringWriter;
					string[] array17 = new string[13];
					array17[0] = "\t<";
					array17[1] = value.name;
					array17[2] = " type=";
					int num16 = 3;
					Type type18 = value.type;
					array17[num16] = ((type18 != null) ? type18.ToString() : null);
					array17[4] = " x=";
					array17[5] = quaternion.x.ToString();
					array17[6] = " y=";
					array17[7] = quaternion.y.ToString();
					array17[8] = " z=";
					array17[9] = quaternion.z.ToString();
					array17[10] = " w=";
					array17[11] = quaternion.w.ToString();
					array17[12] = "/>";
					textWriter16.WriteLine(string.Concat(array17));
				}
				else if (value.type.IsEnum)
				{
					TextWriter textWriter17 = stringWriter;
					string[] array18 = new string[7];
					array18[0] = "\t<";
					array18[1] = value.name;
					array18[2] = " type=";
					int num17 = 3;
					Type type19 = value.type;
					array18[num17] = ((type19 != null) ? type19.ToString() : null);
					array18[4] = " value=";
					array18[5] = ((int)value.obj).ToString();
					array18[6] = "/>";
					textWriter17.WriteLine(string.Concat(array18));
				}
				else if (value.type == typeof(Keyframe))
				{
					Keyframe keyframe = (Keyframe)value.obj;
					TextWriter textWriter18 = stringWriter;
					string[] array19 = new string[15];
					array19[0] = "\t<";
					array19[1] = value.name;
					array19[2] = " type=";
					int num18 = 3;
					Type type20 = value.type;
					array19[num18] = ((type20 != null) ? type20.ToString() : null);
					array19[4] = " time=";
					array19[5] = keyframe.time.ToString();
					array19[6] = " value=";
					array19[7] = keyframe.value.ToString();
					array19[8] = " in=";
					array19[9] = keyframe.inTangent.ToString();
					array19[10] = " out=";
					array19[11] = keyframe.outTangent.ToString();
					array19[12] = " mode=";
					array19[13] = keyframe.tangentMode.ToString();
					array19[14] = "/>";
					textWriter18.WriteLine(string.Concat(array19));
				}
				else
				{
					TextWriter textWriter19 = stringWriter;
					string[] array20 = new string[7];
					array20[0] = "\t<";
					array20[1] = value.name;
					array20[2] = " type=";
					int num19 = 3;
					Type type21 = value.type;
					array20[num19] = ((type21 != null) ? type21.ToString() : null);
					array20[4] = " link=";
					array20[5] = CustomSerialization.WriteClass(value.obj, classes, objects, floats, references).ToString();
					array20[6] = "/>";
					textWriter19.WriteLine(string.Concat(array20));
				}
			}
			stringWriter.WriteLine("</" + str + ">");
			stringWriter.Close();
			classes[count] = stringWriter.ToString();
			return count;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000DE308 File Offset: 0x000DC508
		public static object ReadClass(int slotNum, List<string> classes, List<UnityEngine.Object> objects, List<float> floats, List<object> references)
		{
			for (int i = references.Count; i < classes.Count; i++)
			{
				references.Add(null);
			}
			if (references[slotNum] != null)
			{
				return references[slotNum];
			}
			object obj = null;
			StringReader stringReader = new StringReader(classes[slotNum]);
			string text = stringReader.ReadLine();
			text = text.Substring(1, text.Length - 2);
			int num = 0;
			if (text.Contains(" length="))
			{
				string[] array = text.Split(new char[]
				{
					' '
				});
				num = (int)array[1].Parse(typeof(int));
				text = array[0];
			}
			Type standardAssembliesType = CustomSerialization.GetStandardAssembliesType(text);
			if (standardAssembliesType == null)
			{
				Debug.LogError("Could not load " + text + " as this type does not exists anymore");
				return null;
			}
			Type type = standardAssembliesType.IsArray ? standardAssembliesType.GetElementType() : null;
			if (standardAssembliesType.IsArray)
			{
				obj = Activator.CreateInstance(standardAssembliesType, new object[]
				{
					num
				});
			}
			else
			{
				obj = Activator.CreateInstance(standardAssembliesType);
			}
			references[slotNum] = obj;
			List<CustomSerialization.Value> list = new List<CustomSerialization.Value>();
			Func<int, object> <>9__0;
			string text3;
			for (;;)
			{
				string text2 = stringReader.ReadLine();
				if (text2 == null || text2.StartsWith("</"))
				{
					goto IL_8CD;
				}
				text2 = text2.Substring(2, text2.Length - 4);
				string[] array2 = text2.Split(new char[]
				{
					' ',
					','
				});
				CustomSerialization.Value value = default(CustomSerialization.Value);
				value.name = array2[0];
				text3 = array2[1].Remove(0, 5);
				value.type = CustomSerialization.GetStandardAssembliesType(text3);
				if (value.type == null)
				{
					break;
				}
				if (value.type.IsArray && value.name == "items")
				{
					if (value.type == typeof(float[]))
					{
						int num2 = (int)array2[2].Parse(typeof(int));
						for (int j = num2; j < num2 + num; j++)
						{
							list.Add(new CustomSerialization.Value
							{
								name = "item",
								type = type,
								obj = floats[j]
							});
						}
					}
					else
					{
						for (int k = 2; k < array2.Length; k++)
						{
							list.Add(new CustomSerialization.Value
							{
								name = "item",
								type = type,
								obj = array2[k].Parse(type)
							});
						}
					}
				}
				else
				{
					if (array2[2] == "null")
					{
						value.obj = null;
					}
					else if (typeof(CustomSerialization.IStruct).IsAssignableFrom(value.type))
					{
						value.obj = Activator.CreateInstance(value.type);
						((CustomSerialization.IStruct)value.obj).Decode(array2);
					}
					else if (typeof(CustomSerialization.IStructLink).IsAssignableFrom(value.type))
					{
						Func<int, object> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = ((int link) => CustomSerialization.ReadClass(link, classes, objects, floats, references)));
						}
						Func<int, object> readClass = func;
						value.obj = Activator.CreateInstance(value.type);
						((CustomSerialization.IStructLink)value.obj).Decode(array2, readClass);
					}
					else if (array2[2].StartsWith("link"))
					{
						value.obj = CustomSerialization.ReadClass(int.Parse(array2[2].Remove(0, 5)), classes, objects, floats, references);
					}
					else if (value.type.IsPrimitive)
					{
						value.obj = array2[2].Parse(value.type);
					}
					else if (value.type.IsSubclassOf(typeof(UnityEngine.Object)))
					{
						value.obj = objects[(int)array2[2].Parse(typeof(int))];
					}
					else if (value.type == typeof(string))
					{
						string text4 = (string)array2[2].Parse(value.type);
						text4 = text4.Replace("\\n", "\n");
						value.obj = text4;
					}
					else if (value.type == typeof(Vector2))
					{
						value.obj = new Vector2((float)array2[2].Parse(typeof(float)), (float)array2[3].Parse(typeof(float)));
					}
					else if (value.type == typeof(Vector3))
					{
						value.obj = new Vector3((float)array2[2].Parse(typeof(float)), (float)array2[3].Parse(typeof(float)), (float)array2[4].Parse(typeof(float)));
					}
					else if (value.type == typeof(Rect))
					{
						value.obj = new Rect((float)array2[2].Parse(typeof(float)), (float)array2[3].Parse(typeof(float)), (float)array2[4].Parse(typeof(float)), (float)array2[5].Parse(typeof(float)));
					}
					else if (value.type == typeof(Color))
					{
						value.obj = new Color((float)array2[2].Parse(typeof(float)), (float)array2[3].Parse(typeof(float)), (float)array2[4].Parse(typeof(float)), (float)array2[5].Parse(typeof(float)));
					}
					else if (value.type == typeof(Vector4))
					{
						value.obj = new Vector4((float)array2[2].Parse(typeof(float)), (float)array2[3].Parse(typeof(float)), (float)array2[4].Parse(typeof(float)), (float)array2[5].Parse(typeof(float)));
					}
					else if (value.type == typeof(Quaternion))
					{
						value.obj = new Quaternion((float)array2[2].Parse(typeof(float)), (float)array2[3].Parse(typeof(float)), (float)array2[4].Parse(typeof(float)), (float)array2[5].Parse(typeof(float)));
					}
					else if (value.type == typeof(Keyframe))
					{
						value.obj = new Keyframe((float)array2[2].Parse(typeof(float)), (float)array2[3].Parse(typeof(float)), (float)array2[4].Parse(typeof(float)), (float)array2[5].Parse(typeof(float)))
						{
							tangentMode = (int)array2[6].Parse(typeof(int))
						};
					}
					else if (value.type.IsEnum)
					{
						value.obj = Enum.ToObject(value.type, (int)array2[2].Parse(typeof(int)));
					}
					list.Add(value);
				}
			}
			Debug.LogError("Could not load " + text3 + " as this type does not exists anymore");
			return null;
			IL_8CD:
			int count = list.Count;
			if (standardAssembliesType.IsArray)
			{
				Array array3 = (Array)obj;
				for (int l = 0; l < array3.Length; l++)
				{
					array3.SetValue(list[l].obj, l);
				}
			}
			else
			{
				foreach (FieldInfo fieldInfo in standardAssembliesType.UsableFields(false))
				{
					string name = fieldInfo.Name;
					Type fieldType = fieldInfo.FieldType;
					for (int m = 0; m < count; m++)
					{
						if (list[m].name == name && list[m].type == fieldType)
						{
							fieldInfo.SetValue(obj, list[m].obj);
						}
					}
				}
				foreach (PropertyInfo propertyInfo in standardAssembliesType.UsableProperties(false, true))
				{
					string name2 = propertyInfo.Name;
					Type propertyType = propertyInfo.PropertyType;
					for (int n = 0; n < count; n++)
					{
						if (list[n].name == name2 && list[n].type == propertyType)
						{
							propertyInfo.SetValue(obj, list[n].obj, null);
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000DED80 File Offset: 0x000DCF80
		public static object DeepCopy(object src)
		{
			List<string> classes = new List<string>();
			List<UnityEngine.Object> objects = new List<UnityEngine.Object>();
			List<float> floats = new List<float>();
			List<object> references = new List<object>();
			List<object> references2 = new List<object>();
			return CustomSerialization.ReadClass(CustomSerialization.WriteClass(src, classes, objects, floats, references), classes, objects, floats, references2);
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000DEDC0 File Offset: 0x000DCFC0
		public static string ExportXML(List<string> classes, List<UnityEngine.Object> objects, List<float> floats)
		{
			StringWriter stringWriter = new StringWriter();
			for (int i = 0; i < classes.Count; i++)
			{
				stringWriter.Write(classes[i]);
			}
			stringWriter.Write("<Floats values=");
			int count = floats.Count;
			for (int j = 0; j < count; j++)
			{
				stringWriter.Write(floats[j].ToString());
				if (j != count - 1)
				{
					stringWriter.Write(",");
				}
			}
			stringWriter.WriteLine("/>");
			stringWriter.Close();
			return stringWriter.ToString();
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000DEE50 File Offset: 0x000DD050
		public static void ImportXML(string xml, out List<string> classes, out List<UnityEngine.Object> objects, out List<float> floats)
		{
			StringReader stringReader = new StringReader(xml);
			classes = new List<string>();
			objects = new List<UnityEngine.Object>();
			floats = new List<float>();
			StringWriter stringWriter = null;
			for (;;)
			{
				string text = stringReader.ReadLine();
				if (text == null)
				{
					break;
				}
				if (!text.StartsWith("<Object"))
				{
					if (text.StartsWith("<Floats"))
					{
						text = text.Replace("<Floats values=", "");
						text = text.Replace("/>", "");
						if (text.Length != 0)
						{
							string[] array = text.Split(new char[]
							{
								','
							});
							for (int i = 0; i < array.Length; i++)
							{
								floats.Add(float.Parse(array[i]));
							}
						}
					}
					else
					{
						if (!text.Contains("/>") && !text.Contains("</"))
						{
							if (stringWriter != null)
							{
								classes.Add(stringWriter.ToString());
							}
							stringWriter = new StringWriter();
						}
						stringWriter.WriteLine(text);
					}
				}
			}
			classes.Add(stringWriter.ToString());
		}

		// Token: 0x0200055A RID: 1370
		public interface IStruct
		{
			// Token: 0x0600229A RID: 8858
			string Encode();

			// Token: 0x0600229B RID: 8859
			void Decode(string[] lineMembers);
		}

		// Token: 0x0200055B RID: 1371
		public interface IStructLink
		{
			// Token: 0x0600229C RID: 8860
			string Encode(Func<object, int> writeClass);

			// Token: 0x0600229D RID: 8861
			void Decode(string[] lineMembers, Func<int, object> readClass);
		}

		// Token: 0x0200055C RID: 1372
		private struct Value
		{
			// Token: 0x04002261 RID: 8801
			public string name;

			// Token: 0x04002262 RID: 8802
			public Type type;

			// Token: 0x04002263 RID: 8803
			public object obj;
		}
	}
}
