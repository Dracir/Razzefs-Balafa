using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Magicolo.EditorTools {
	public class CustomAttributePropertyDrawerBase : CustomPropertyDrawerBase {
	
		public string prefixLabel;
		public bool noFieldLabel;
		public bool noPrefixLabel;
		public bool noIndex;
		public bool disableOnPlay;
		public bool disableOnStop;
		public string disableBool;
		public bool beforeSeparator;
		public bool afterSeparator;
		public int indent;
		public int index;
	
		public bool drawPrefixLabel = true;
		public bool isArray;
		public float scrollbarThreshold;
		public bool boolDisabled;
		public GUIContent currentLabel = GUIContent.none;
	
		public SerializedProperty arrayProperty;
	
		static MethodInfo getPropertyDrawerMethod;
		public static MethodInfo GetPropertyDrawerMethod {
			get {
				if (getPropertyDrawerMethod == null) {
					foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
						foreach (Type type in assembly.GetTypes()) {
							if (type.Name == "ScriptAttributeUtility") {
								getPropertyDrawerMethod = type.GetMethod("GetDrawerTypeForType", ObjectExtensions.AllFlags);
							}
						}
					}
				}
				return getPropertyDrawerMethod;
			}
		}
	
		public override void Begin(Rect position, SerializedProperty property, GUIContent label) {
			base.Begin(position, property, label);
			
			scrollbarThreshold = Screen.width - position.width > 19 ? 298 : 313;
			
			if (beforeSeparator) {
				position.y += 4;
				EditorGUI.LabelField(position, GUIContent.none, new GUIStyle("RL DragHandle"));
				position.y += 12;
			}
			
			EditorGUI.BeginDisabledGroup((Application.isPlaying && disableOnPlay) || (!Application.isPlaying && disableOnStop) || boolDisabled);
			EditorGUI.indentLevel += indent;
			
			if (isArray) {
				if (noIndex) {
					if (string.IsNullOrEmpty(prefixLabel)) {
						label.text = label.text.Substring(0, label.text.Length - 2);
					}
				}
				else if (!string.IsNullOrEmpty(prefixLabel)) {
					prefixLabel += " " + index;
				}
			}
		
			if (drawPrefixLabel) {
				if (!noPrefixLabel) {
					if (!string.IsNullOrEmpty(prefixLabel)) {
						label.text = prefixLabel;
					}
					
					position = EditorGUI.PrefixLabel(position, label);
				}
			}
			else {
				if (noPrefixLabel) {
					label.text = "";
				}
				else if (!string.IsNullOrEmpty(prefixLabel)) {
					label.text = prefixLabel;
				}
			}
			
			currentPosition = position;
			currentLabel = label;
		}
	
		public override void End() {
			base.End();
			
			EditorGUI.indentLevel -= indent;
			EditorGUI.EndDisabledGroup();
			
			if (afterSeparator) {
				currentPosition.y += 4;
				EditorGUI.LabelField(currentPosition, "", new GUIStyle("RL DragHandle"));
				currentPosition.y += 12;
			}
		}
	
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			InitializeParameters(property, label);
			
			return EditorGUI.GetPropertyHeight(property, label, true) + (beforeSeparator ? 16 : 0) + (afterSeparator ? 16 : 0);
		}
	
		public PropertyDrawer GetPropertyDrawer(Type propertyAttributeType, params object[] arguments) {
			Type propertyDrawerType = GetPropertyDrawerMethod.Invoke(null, new object[] { propertyAttributeType }) as Type;
			
			if (propertyDrawerType != null) {
				PropertyAttribute propertyAttribute = Activator.CreateInstance(propertyAttributeType, arguments) as PropertyAttribute;
				PropertyDrawer propertyDrawer = Activator.CreateInstance(propertyDrawerType) as PropertyDrawer;
				propertyDrawer.SetValueToMember("m_Attribute", propertyAttribute);
				propertyDrawer.SetValueToMember("m_FieldInfo", fieldInfo);
				return propertyDrawer;
			}
			
			return null;
		}
	
		public PropertyDrawer GetPropertyDrawer(Attribute propertyAttribute, params object[] arguments) {
			return GetPropertyDrawer(propertyAttribute.GetType(), arguments);
		}
		
		void InitializeParameters(SerializedProperty property, GUIContent label) {
			CustomAttributeBase customAttribute = (CustomAttributeBase)attribute;
			
			noFieldLabel = customAttribute.NoFieldLabel;
			noPrefixLabel = customAttribute.NoPrefixLabel;
			noIndex = customAttribute.NoIndex;
			prefixLabel = customAttribute.PrefixLabel;
			disableOnPlay = customAttribute.DisableOnPlay;
			disableOnStop = customAttribute.DisableOnStop;
			disableBool = customAttribute.DisableBool;
			indent = customAttribute.Indent;
			beforeSeparator = customAttribute.BeforeSeparator;
			afterSeparator = customAttribute.AfterSeparator;
			isArray = typeof(IList).IsAssignableFrom(fieldInfo.FieldType);
			
			if (isArray) {
				index = AttributeUtility.GetIndexFromLabel(label);
				arrayProperty = property.GetParent();
			}
			
			bool inverseBool = !string.IsNullOrEmpty(disableBool) && disableBool.StartsWith("!");
			boolDisabled = !string.IsNullOrEmpty(disableBool) && property.serializedObject.targetObject.GetValueFromMemberAtPath<bool>(inverseBool ? disableBool.Substring(1) : disableBool);
			boolDisabled = inverseBool ? !boolDisabled : boolDisabled;
		}
	}
}