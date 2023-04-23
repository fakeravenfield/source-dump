using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MapEditor
{
	// Token: 0x020006F4 RID: 1780
	public class PropertiesSidebarUI : SidebarBase
	{
		// Token: 0x06002CBA RID: 11450 RVA: 0x0001ECCD File Offset: 0x0001CECD
		protected override void DoInitialize()
		{
			base.DoInitialize();
			this.editor.onSelectionChanged.AddListener(new UnityAction(this.SelectionChanged));
			this.SelectionChanged();
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x001042C8 File Offset: 0x001024C8
		private void SelectionChanged()
		{
			this.ClearProperties();
			Selection selection = this.editor.GetSelection();
			if (selection.GetLength() == 1)
			{
				SelectableObject selectableObject = selection.GetObjects().First<SelectableObject>();
				Transform transform = selectableObject.transform;
				PropertyBinding property = new PropertyBinding(typeof(Vector3), "Position", delegate(object v)
				{
					transform.position = (Vector3)v;
				}, () => transform.position, new ShowInMapEditorAttribute(-3));
				PropertyBinding property2 = new PropertyBinding(typeof(Vector3), "Rotation", delegate(object v)
				{
					transform.localEulerAngles = (Vector3)v;
				}, () => transform.localEulerAngles, new ShowInMapEditorAttribute(-2));
				PropertyBinding property3 = new PropertyBinding(typeof(Vector3), "Scale", delegate(object v)
				{
					transform.localScale = (Vector3)v;
				}, () => transform.localScale, new ShowInMapEditorAttribute(-1));
				this.editorsToUpdate.Add(this.AddPropertyEditor(property));
				this.editorsToUpdate.Add(this.AddPropertyEditor(property2));
				this.editorsToUpdate.Add(this.AddPropertyEditor(property3));
				foreach (PropertyBinding propertyBinding in selectableObject.GetOrCreateComponent<PropertyProvider>().GetBindings())
				{
					if (!propertyBinding.hide)
					{
						this.editorsToUpdate.Add(this.AddPropertyEditor(propertyBinding));
					}
				}
				return;
			}
			if (selection.GetLength() > 1)
			{
				Dictionary<string, PropertyBinding[]> dictionary = (from obj in selection.GetObjects()
				select obj.GetOrCreateComponent<PropertyProvider>()).SelectMany((PropertyProvider provider) => provider.GetBindings()).GroupBy((PropertyBinding p) => p.name, (PropertyBinding p) => p, (string name, IEnumerable<PropertyBinding> bindings) => new
				{
					name,
					bindings
				}).ToDictionary(a => a.name, a => a.bindings.ToArray<PropertyBinding>());
				foreach (string key in dictionary.Keys)
				{
					PropertyBinding[] array2 = dictionary[key];
					if (array2.Length != 0)
					{
						bool flag = true;
						Type type = array2.First<PropertyBinding>().type;
						PropertyBinding[] array = array2;
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].type != type)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							PropertyBinding property4 = new PropertyBinding(array2);
							this.AddPropertyEditor(property4);
						}
					}
				}
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x0001ECF7 File Offset: 0x0001CEF7
		private void ClearProperties()
		{
			this.editorsToUpdate.Clear();
			Utils.DestroyChildren(this.layoutPanel.gameObject);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x001045E0 File Offset: 0x001027E0
		private PropertiesSidebarUI.EditorInfo AddPropertyEditor(PropertyBinding property)
		{
			switch (this.SystemTypeToEditorType(property.type))
			{
			case PropertiesSidebarUI.EditorType.Enum:
				return this.AddEnumEditor(property);
			case PropertiesSidebarUI.EditorType.Float:
				return this.AddFloatEditor(property);
			case PropertiesSidebarUI.EditorType.Bool:
				return this.AddBoolEditor(property);
			case PropertiesSidebarUI.EditorType.String:
				return this.AddStringEditor(property);
			case PropertiesSidebarUI.EditorType.Vector3:
				return this.AddVector3Editor(property);
			case PropertiesSidebarUI.EditorType.Material:
				return this.AddMaterialPicker(property);
			case PropertiesSidebarUI.EditorType.Color:
				return this.AddColorPicker(property);
			case PropertiesSidebarUI.EditorType.AudioAsset:
				return this.AddAudioAssetPicker(property);
			default:
			{
				PropertiesSidebarUI.EditorInfo result = default(PropertiesSidebarUI.EditorInfo);
				result.updateValue = delegate()
				{
				};
				result.canUpdate = (() => false);
				return result;
			}
			}
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x001046BC File Offset: 0x001028BC
		private PropertiesSidebarUI.EditorType SystemTypeToEditorType(Type type)
		{
			if (type.IsEnum)
			{
				return PropertiesSidebarUI.EditorType.Enum;
			}
			if (type.IsAssignableFrom(typeof(float)))
			{
				return PropertiesSidebarUI.EditorType.Float;
			}
			if (type.IsAssignableFrom(typeof(bool)))
			{
				return PropertiesSidebarUI.EditorType.Bool;
			}
			if (type.IsAssignableFrom(typeof(string)))
			{
				return PropertiesSidebarUI.EditorType.String;
			}
			if (type.IsAssignableFrom(typeof(Vector3)))
			{
				return PropertiesSidebarUI.EditorType.Vector3;
			}
			if (type.IsAssignableFrom(typeof(MapEditorMaterial)))
			{
				return PropertiesSidebarUI.EditorType.Material;
			}
			if (type.IsAssignableFrom(typeof(Color)))
			{
				return PropertiesSidebarUI.EditorType.Color;
			}
			if (type.IsAssignableFrom(typeof(AudioAsset)))
			{
				return PropertiesSidebarUI.EditorType.AudioAsset;
			}
			Debug.LogWarningFormat("Type {0} is not supported by the property sidebar", new object[]
			{
				type.Name
			});
			return PropertiesSidebarUI.EditorType.None;
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x0010477C File Offset: 0x0010297C
		private PropertiesSidebarUI.EditorInfo AddEnumEditor(PropertyBinding property)
		{
			DropdownWithText dropdown = UnityEngine.Object.Instantiate<DropdownWithText>(this.dropdownPrefab);
			dropdown.transform.SetParent(this.layoutPanel, false);
			dropdown.SetDescription(property.name);
			dropdown.SetOptions(Enum.GetNames(property.type));
			Action action = delegate()
			{
				dropdown.SetSelectedValue(property.GetValue().ToString());
			};
			action();
			dropdown.onSelectedValueChanged.AddListener(delegate(string v)
			{
				object value = Enum.Parse(property.type, v);
				this.PropertyChanged<object>(property, value);
			});
			PropertiesSidebarUI.EditorInfo result = default(PropertiesSidebarUI.EditorInfo);
			result.updateValue = action;
			result.canUpdate = (() => true);
			return result;
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x0010485C File Offset: 0x00102A5C
		private PropertiesSidebarUI.EditorInfo AddFloatEditor(PropertyBinding property)
		{
			Func<bool> canUpdate;
			Action action;
			if (property.range != null)
			{
				SliderWithInput slider = UnityEngine.Object.Instantiate<SliderWithInput>(this.sliderPrefab);
				slider.transform.SetParent(this.layoutPanel, false);
				slider.SetDescription(property.name);
				slider.SetRange(property.range.min, property.range.max);
				canUpdate = (() => !slider.isFocused);
				action = delegate()
				{
					slider.SetValue((float)property.GetValue());
				};
				action();
				slider.onValueChanged.AddListener(delegate(float v)
				{
					this.PropertyChanged<float>(property, v);
				});
			}
			else
			{
				FloatInput input = UnityEngine.Object.Instantiate<FloatInput>(this.floatInputPrefab);
				input.transform.SetParent(this.layoutPanel, false);
				input.SetDescription(property.name);
				canUpdate = (() => !input.isFocuesed);
				action = delegate()
				{
					input.SetValue((float)property.GetValue());
				};
				action();
				input.onValueChanged.AddListener(delegate(float v)
				{
					this.PropertyChanged<float>(property, v);
				});
			}
			return new PropertiesSidebarUI.EditorInfo
			{
				updateValue = action,
				canUpdate = canUpdate
			};
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x00104A14 File Offset: 0x00102C14
		private PropertiesSidebarUI.EditorInfo AddBoolEditor(PropertyBinding property)
		{
			Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(this.togglePrefab);
			toggle.transform.SetParent(this.layoutPanel, false);
			toggle.SetText(property.name);
			Action action = delegate()
			{
				toggle.isOn = (bool)property.GetValue();
			};
			action();
			toggle.onValueChanged.AddListener(delegate(bool v)
			{
				this.PropertyChanged<bool>(property, v);
			});
			PropertiesSidebarUI.EditorInfo result = default(PropertiesSidebarUI.EditorInfo);
			result.updateValue = action;
			result.canUpdate = (() => true);
			return result;
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x00104ADC File Offset: 0x00102CDC
		private PropertiesSidebarUI.EditorInfo AddStringEditor(PropertyBinding property)
		{
			InputWithText input = UnityEngine.Object.Instantiate<InputWithText>(this.inputPrefab);
			input.transform.SetParent(this.layoutPanel, false);
			input.SetDescription(property.name);
			Action action = delegate()
			{
				input.SetText(property.GetValue().ToString());
			};
			action();
			input.onEndEdit.AddListener(delegate(string v)
			{
				this.PropertyChanged<string>(property, v);
			});
			return new PropertiesSidebarUI.EditorInfo
			{
				updateValue = action,
				canUpdate = (() => !input.isFocused)
			};
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x00104B90 File Offset: 0x00102D90
		private PropertiesSidebarUI.EditorInfo AddVector3Editor(PropertyBinding property)
		{
			Vector3Editor input = UnityEngine.Object.Instantiate<Vector3Editor>(this.vector3Prefab);
			input.transform.SetParent(this.layoutPanel, false);
			input.SetDescription(property.name);
			Action action = delegate()
			{
				input.SetValue((Vector3)property.GetValue());
			};
			action();
			input.onValueChanged.AddListener(delegate(Vector3 v)
			{
				this.PropertyChanged<Vector3>(property, v);
			});
			return new PropertiesSidebarUI.EditorInfo
			{
				updateValue = action,
				canUpdate = (() => !input.isFocused)
			};
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x00104C44 File Offset: 0x00102E44
		private PropertiesSidebarUI.EditorInfo AddMaterialPicker(PropertyBinding property)
		{
			MaterialPicker input = UnityEngine.Object.Instantiate<MaterialPicker>(this.materialPickerPrefab);
			input.transform.SetParent(this.layoutPanel, false);
			input.SetDescription(property.name);
			Action action = delegate()
			{
				input.SetMaterial((MapEditorMaterial)property.GetValue());
			};
			action();
			input.onMaterialChanged.AddListener(delegate(MapEditorMaterial v)
			{
				this.PropertyChanged<MapEditorMaterial>(property, v);
			});
			PropertiesSidebarUI.EditorInfo result = default(PropertiesSidebarUI.EditorInfo);
			result.updateValue = action;
			result.canUpdate = (() => true);
			return result;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x00104D0C File Offset: 0x00102F0C
		private PropertiesSidebarUI.EditorInfo AddColorPicker(PropertyBinding property)
		{
			ColorPicker input = UnityEngine.Object.Instantiate<ColorPicker>(this.colorPickerPrefab);
			input.transform.SetParent(this.layoutPanel, false);
			input.SetDescription(property.name);
			Action action = delegate()
			{
				input.SetColor((Color)property.GetValue());
			};
			action();
			input.onColorChanged.AddListener(delegate(Color v)
			{
				this.PropertyChanged<Color>(property, v);
			});
			PropertiesSidebarUI.EditorInfo result = default(PropertiesSidebarUI.EditorInfo);
			result.updateValue = action;
			result.canUpdate = (() => true);
			return result;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x00104DD4 File Offset: 0x00102FD4
		private PropertiesSidebarUI.EditorInfo AddAudioAssetPicker(PropertyBinding property)
		{
			string[] options = new string[]
			{
				"(None)"
			}.Concat(from a in AssetTable.GetAllAudioClips()
			select a.audioClip.name).ToArray<string>();
			DropdownWithText dropdown = UnityEngine.Object.Instantiate<DropdownWithText>(this.dropdownPrefab);
			dropdown.transform.SetParent(this.layoutPanel, false);
			dropdown.SetDescription(property.name);
			dropdown.SetOptions(options);
			Action action = delegate()
			{
				AudioAsset audioAsset = (AudioAsset)property.GetValue();
				if (audioAsset.HasValue())
				{
					dropdown.SetSelectedValue(audioAsset.audioClip.name);
					return;
				}
				dropdown.SetSelectedIndex(0);
			};
			action();
			dropdown.onSelectedValueChanged.AddListener(delegate(string v)
			{
				AudioAsset value = AssetTable.GetAllAudioClips().FirstOrDefault((AudioAsset a) => a.audioClip.name == v);
				this.PropertyChanged<AudioAsset>(property, value);
			});
			PropertiesSidebarUI.EditorInfo result = default(PropertiesSidebarUI.EditorInfo);
			result.updateValue = action;
			result.canUpdate = (() => true);
			return result;
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x00104EE8 File Offset: 0x001030E8
		private void PropertyChanged<T>(PropertyBinding property, T value)
		{
			if (!EqualityComparer<T>.Default.Equals(value, (T)((object)property.GetValue())))
			{
				SetPropertyAction setPropertyAction = this.editor.GetActionHistory().TopOfUndoStack<SetPropertyAction>();
				if (setPropertyAction != null && setPropertyAction.property == property)
				{
					setPropertyAction.newValue = value;
				}
				else
				{
					this.editor.AddUndoableAction(new SetPropertyAction(property, value));
				}
				property.SetValue(value);
			}
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x00104F64 File Offset: 0x00103164
		protected override void Update()
		{
			base.Update();
			foreach (PropertiesSidebarUI.EditorInfo editorInfo in this.editorsToUpdate)
			{
				if (editorInfo.canUpdate())
				{
					editorInfo.updateValue();
				}
			}
		}

		// Token: 0x04002939 RID: 10553
		public RectTransform layoutPanel;

		// Token: 0x0400293A RID: 10554
		public SliderWithInput sliderPrefab;

		// Token: 0x0400293B RID: 10555
		public FloatInput floatInputPrefab;

		// Token: 0x0400293C RID: 10556
		public DropdownWithText dropdownPrefab;

		// Token: 0x0400293D RID: 10557
		public InputWithText inputPrefab;

		// Token: 0x0400293E RID: 10558
		public Toggle togglePrefab;

		// Token: 0x0400293F RID: 10559
		public Vector3Editor vector3Prefab;

		// Token: 0x04002940 RID: 10560
		public MaterialPicker materialPickerPrefab;

		// Token: 0x04002941 RID: 10561
		public ColorPicker colorPickerPrefab;

		// Token: 0x04002942 RID: 10562
		private List<PropertiesSidebarUI.EditorInfo> editorsToUpdate = new List<PropertiesSidebarUI.EditorInfo>();

		// Token: 0x020006F5 RID: 1781
		private enum EditorType
		{
			// Token: 0x04002944 RID: 10564
			None,
			// Token: 0x04002945 RID: 10565
			Enum,
			// Token: 0x04002946 RID: 10566
			Float,
			// Token: 0x04002947 RID: 10567
			Bool,
			// Token: 0x04002948 RID: 10568
			String,
			// Token: 0x04002949 RID: 10569
			Vector3,
			// Token: 0x0400294A RID: 10570
			Material,
			// Token: 0x0400294B RID: 10571
			Color,
			// Token: 0x0400294C RID: 10572
			AudioAsset
		}

		// Token: 0x020006F6 RID: 1782
		private struct EditorInfo
		{
			// Token: 0x0400294D RID: 10573
			public Action updateValue;

			// Token: 0x0400294E RID: 10574
			public Func<bool> canUpdate;
		}
	}
}
