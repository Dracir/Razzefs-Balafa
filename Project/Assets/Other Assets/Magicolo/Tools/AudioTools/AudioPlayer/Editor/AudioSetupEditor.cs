using System;
using Magicolo.EditorTools;
using Magicolo.GeneralTools;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Magicolo.AudioTools {
	[CustomEditor(typeof(AudioSetup))]
	public class AudioSetupEditor : CustomEditorBase {

		AudioSetup setup;
		AudioSource source;
		AudioClip clip;
		
		float adjustedLength;
		
		public override void OnEnable() {
			base.OnEnable();
			
			setup = (AudioSetup)target;
		}
		
		public override void OnInspectorGUI() {
			source = setup.Source;
			clip = setup.Clip;
			
			if (clip == null) {
				setup.Source.Remove();
				setup.Remove();
				
				return;
			}
			
			Begin();
			
			ShowGeneralSettings();
			Separator();
			ShowClipSettings();

			End();
		}

		void ShowGeneralSettings() {
			adjustedLength = clip.length / source.pitch;
			
			ShowFadeIn();
			ShowFadeOut();
			
			EditorGUILayout.PropertyField(serializedObject.FindProperty("randomVolume"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("randomPitch"));
		}

		void ShowFadeIn() {
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fadeIn"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fadeInEase"), GUIContent.none, GUILayout.MaxWidth(120));
			
			EditorGUILayout.EndHorizontal();
		}
		
		void ShowFadeOut() {
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fadeOut"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fadeOutEase"), GUIContent.none, GUILayout.MaxWidth(120));
			
			EditorGUILayout.EndHorizontal();
		}

		void ShowClipSettings() {
			BeginBox();
			
			EditorGUILayout.LabelField("Clip Info", new GUIStyle("boldLabel"));
			
			EditorGUI.indentLevel += 1;
				
			GUIStyle style = EditorStyles.boldLabel;
			EditorGUILayout.LabelField("Name:", clip.name, style);
			EditorGUILayout.LabelField("Path:", AssetDatabase.GetAssetPath(clip), style);
			EditorGUILayout.LabelField("Channels:", clip.channels.ToString(), style);
			EditorGUILayout.LabelField("Frequency:", string.Format("{0} {1} Hz", clip.frequency.ToString().Substring(0, 2), clip.frequency.ToString().Substring(2, 3)), style);
			EditorGUILayout.LabelField("Length:", string.Format("{0} seconds", adjustedLength), style);
			EditorGUILayout.LabelField("Samples:", clip.samples.ToString(), style);
				
			Separator();
			EditorGUI.indentLevel -= 1;
			
			EndBox();
		}
	}
}

