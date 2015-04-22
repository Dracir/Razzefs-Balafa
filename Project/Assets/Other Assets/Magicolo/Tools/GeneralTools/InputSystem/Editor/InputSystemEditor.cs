using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Magicolo.EditorTools;

namespace Magicolo.GeneralTools {
	[CustomEditor(typeof(InputSystem)), CanEditMultipleObjects]
	public class InputSystemEditor : CustomEditorBase {
		
		InputSystem inputSystem;
		SerializedProperty keyInfosProperty;
		KeyInfo currentKeyInfo;
		SerializedProperty currentKeyInfoProperty;
		SerializedProperty keysProperty;
		SerializedProperty currentKeyProperty;
		SerializedProperty keyInfoListenersProperty;
		SerializedProperty currentKeyInfoListenerProperty;
		SerializedProperty axisInfosProperty;
		AxisInfo currentAxisInfo;
		SerializedProperty currentAxisInfoProperty;
		SerializedProperty axisInfoListenersProperty;
		SerializedProperty currentAxisInfoListenerProperty;
		
		public override void OnEnable() {
			base.OnEnable();
			
			inputSystem = (InputSystem)target;
		}

		public override void OnInspectorGUI() {
			Begin();
			
			ShowKeyInfos();
			ShowAxisInfos();
			
			End();
		}

		void ShowKeyInfos() {
			keyInfosProperty = serializedObject.FindProperty("keyInfos");
			
			if (AddFoldOut(keyInfosProperty, "Keys".ToGUIContent())) {
				KeyInfo[] keyInfos = inputSystem.GetKeyInfos();
				KeyInfo keyInfo = keyInfos.Last();
				
				keyInfo.SetUniqueName("default", "", keyInfos);
				keyInfo.SetKeys(new []{ KeyCode.None });
				
				serializedObject.Update();
			}
			
			if (keyInfosProperty.isExpanded) {
				EditorGUI.indentLevel += 1;
				
				for (int i = 0; i < keyInfosProperty.arraySize; i++) {
					currentKeyInfo = inputSystem.GetKeyInfos()[i];
					currentKeyInfoProperty = keyInfosProperty.GetArrayElementAtIndex(i);
				
					BeginBox();
					
					if (DeleteFoldOut(keyInfosProperty, i, currentKeyInfo.Name.ToGUIContent(), CustomEditorStyles.BoldFoldout)) {
						break;
					}
				
					ShowKeyInfo();
					
					EndBox();
				}
				
				EditorGUI.indentLevel -= 1;
			}
		}
		
		void ShowKeyInfo() {
			keysProperty = currentKeyInfoProperty.FindPropertyRelative("inputKeys");
			
			if (currentKeyInfoProperty.isExpanded) {
				EditorGUI.indentLevel += 1;
				
				UniqueNameField(currentKeyInfo, inputSystem.GetKeyInfos());

				BeginBox();
				
				for (int i = 0; i < keysProperty.arraySize; i++) {
					currentKeyProperty = keysProperty.GetArrayElementAtIndex(i);
					
					EditorGUILayout.BeginHorizontal();
						
					EditorGUILayout.PrefixLabel("Key " + i);
					
					int indent = EditorGUI.indentLevel;
					EditorGUI.indentLevel = 0;
					
					EditorGUILayout.PropertyField(currentKeyProperty, GUIContent.none);
					
					EditorGUI.indentLevel = indent;
						
					if (i == 0) {
						SmallAddButton(keysProperty);
					}
					else {
						if (DeleteButton(keysProperty, i) && keysProperty.arraySize == 0) {
							AddToArray(keysProperty);
						}
					}
					
					EditorGUILayout.EndHorizontal();
					
					Reorderable(keysProperty, i, true);
				}
				
				EndBox();
				Separator();
				
				ShowKeyInfoListeners();
				
				Separator();
				EditorGUI.indentLevel -= 1;
			}
		}
	
		void ShowKeyInfoListeners() {
			keyInfoListenersProperty = currentKeyInfoProperty.FindPropertyRelative("listenerReferences");
			
			BeginBox();
			
			AddFoldOut(keyInfoListenersProperty, "Listeners".ToGUIContent());

			if (keyInfoListenersProperty.isExpanded) {
				EditorGUI.indentLevel += 1;
				
				
				for (int i = 0; i < keyInfoListenersProperty.arraySize; i++) {
					currentKeyInfoListenerProperty = keyInfoListenersProperty.GetArrayElementAtIndex(i);
					
					EditorGUI.BeginDisabledGroup(Application.isPlaying);
					EditorGUILayout.BeginHorizontal();
					
					ShowAddKeyListenerPopup();
					
					if (DeleteButton(keyInfoListenersProperty, i)) {
						break;
					}
					
					EditorGUILayout.EndHorizontal();
					EditorGUI.EndDisabledGroup();
					
					Reorderable(keyInfoListenersProperty, i, true);
				}
					
				Separator();
				EditorGUI.indentLevel -= 1;
			}
			
			EndBox();
		}
		
		void ShowAddKeyListenerPopup() {
			List<MonoBehaviour> listeners = new List<MonoBehaviour>();
			List<string> options = new List<string>{ " " };
			MonoBehaviour currentListener = currentKeyInfoListenerProperty.GetValue<MonoBehaviour>();
			
			foreach (MonoBehaviour listener in inputSystem.GetComponents<MonoBehaviour>()) {
				if (listener is IInputKeyListener && (currentListener == listener || !keyInfoListenersProperty.Contains(listener))) {
					listeners.Add(listener);
					options.Add(InputSystemUtility.FormatListener(listener));
				}
			}
			
			foreach (MonoBehaviour listener in FindObjectsOfType<MonoBehaviour>()) {
				if (listener is IInputKeyListener && (currentListener == listener || !keyInfoListenersProperty.Contains(listener))) {
					listeners.Add(listener);
					options.Add(InputSystemUtility.FormatListener(listener));
				}
			}
			
			int index = listeners.IndexOf(currentKeyInfoListenerProperty.GetValue<MonoBehaviour>()) + 1;
			
			index = EditorGUILayout.Popup(index, options.ToArray()) - 1;
			
			currentKeyInfoListenerProperty.SetValue(index == -1 ? null : listeners[index]);
		}
		
		void ShowAxisInfos() {
			axisInfosProperty = serializedObject.FindProperty("axisInfos");
			
			if (AddFoldOut(axisInfosProperty, "Axes".ToGUIContent())) {
				AxisInfo[] axisInfos = inputSystem.GetAxisInfos();
				AxisInfo axisInfo = axisInfos.Last();
				
				axisInfo.SetUniqueName("default", "", axisInfos);
				
				serializedObject.Update();
			}
			
			if (axisInfosProperty.isExpanded) {
				EditorGUI.indentLevel += 1;
				
				for (int i = 0; i < axisInfosProperty.arraySize; i++) {
					currentAxisInfo = inputSystem.GetAxisInfos()[i];
					currentAxisInfoProperty = axisInfosProperty.GetArrayElementAtIndex(i);
				
					BeginBox();
					
					if (DeleteFoldOut(axisInfosProperty, i, currentAxisInfo.Name.ToGUIContent(), CustomEditorStyles.BoldFoldout)) {
						break;
					}
				
					ShowAxisInfo();
					
					EndBox();
				}
				
				EditorGUI.indentLevel -= 1;
			}
		}
		
		void ShowAxisInfo() {
			if (currentAxisInfoProperty.isExpanded) {
				EditorGUI.indentLevel += 1;
				
				UniqueNameField(currentAxisInfo, inputSystem.GetAxisInfos());
				
				ShowAxisPopup();
				EditorGUILayout.PropertyField(currentAxisInfoProperty.FindPropertyRelative("threshold"));
				
				Separator();
				
				ShowAxisInfoListeners();
				
				Separator();
				
				EditorGUI.indentLevel -= 1;
			}
		}
		
		void ShowAxisPopup() {
			List<string> options = new List<string>{ " " };
			
			SerializedObject inputManagerSerialized = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset"));
			SerializedProperty inputManagerAxisProperty = inputManagerSerialized.FindProperty("m_Axes");
			SerializedProperty currentAxisProperty = currentAxisInfoProperty.FindPropertyRelative("inputAxis");
		
			for (int i = 0; i < inputManagerAxisProperty.arraySize; i++) {
				string axisName = inputManagerAxisProperty.GetArrayElementAtIndex(i).FindPropertyRelative("m_Name").GetValue<string>();
				
				if (!options.Contains(axisName)) {
					options.Add(axisName);
				}
			}
			
			Popup(currentAxisProperty, options.ToArray(), "Axis".ToGUIContent());
		}
		
		void ShowAxisInfoListeners() {
			axisInfoListenersProperty = currentAxisInfoProperty.FindPropertyRelative("listenerReferences");
			
			BeginBox();
			
			AddFoldOut(axisInfoListenersProperty, "Listeners".ToGUIContent());

			if (axisInfoListenersProperty.isExpanded) {
				EditorGUI.indentLevel += 1;
				
				
				for (int i = 0; i < axisInfoListenersProperty.arraySize; i++) {
					currentAxisInfoListenerProperty = axisInfoListenersProperty.GetArrayElementAtIndex(i);
					
					EditorGUI.BeginDisabledGroup(Application.isPlaying);
					EditorGUILayout.BeginHorizontal();
					
					ShowAddAxisListenerPopup();
					
					if (DeleteButton(axisInfoListenersProperty, i)) {
						break;
					}
					
					EditorGUILayout.EndHorizontal();
					EditorGUI.EndDisabledGroup();
					
					Reorderable(axisInfoListenersProperty, i, true);
				}
					
				Separator();
				EditorGUI.indentLevel -= 1;
			}
			
			EndBox();
		}
		
		void ShowAddAxisListenerPopup() {
			List<MonoBehaviour> listeners = new List<MonoBehaviour>();
			List<string> options = new List<string>{ " " };
			MonoBehaviour currentListener = currentAxisInfoListenerProperty.GetValue<MonoBehaviour>();
			
			foreach (MonoBehaviour listener in inputSystem.GetComponents<MonoBehaviour>()) {
				if (listener is IInputAxisListener && (currentListener == listener || !axisInfoListenersProperty.Contains(listener))) {
					listeners.Add(listener);
					options.Add(InputSystemUtility.FormatListener(listener));
				}
			}
			
			foreach (MonoBehaviour listener in FindObjectsOfType<MonoBehaviour>()) {
				if (listener is IInputAxisListener && (currentListener == listener || !axisInfoListenersProperty.Contains(listener))) {
					listeners.Add(listener);
					options.Add(InputSystemUtility.FormatListener(listener));
				}
			}
			
			int index = listeners.IndexOf(currentAxisInfoListenerProperty.GetValue<MonoBehaviour>()) + 1;
			
			index = EditorGUILayout.Popup(index, options.ToArray()) - 1;
			
			currentAxisInfoListenerProperty.SetValue(index == -1 ? null : listeners[index]);
		}
	}
}
