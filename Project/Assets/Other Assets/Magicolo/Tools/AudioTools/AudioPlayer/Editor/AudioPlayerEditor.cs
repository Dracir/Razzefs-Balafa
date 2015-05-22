using System;
using Magicolo.EditorTools;
using Magicolo.GeneralTools;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Magicolo.AudioTools {
	[CustomEditor(typeof(AudioPlayer))]
	public class AudioPlayerEditor : CustomEditorBase {
		
		AudioPlayer audioPlayer;
		
		SerializedProperty setupManagerProperty;
		SerializedProperty setupsProperty;
		
		public override void OnEnable() {
			base.OnEnable();
			
			audioPlayer = (AudioPlayer)target;
			audioPlayer.SetExecutionOrder(-16);
		}
		
		public override void OnInspectorGUI() {
			setupManagerProperty = serializedObject.FindProperty("setupManager");
			setupsProperty = setupManagerProperty.FindPropertyRelative("setups");
			
			CleanUp();
			
			Begin();
			
			ShowAddSourcesBox();
			
			End();
		}

		void ShowAddSourcesBox() {
			EditorGUILayout.HelpBox("Drop audio clips here.", MessageType.Info);
			
			DropArea<AudioClip>(true, OnAudioClipDropped);
		}
			
		void OnAudioClipDropped(AudioClip dropped) {
			setupsProperty.Add(GetSetupFromClip(dropped));
		}

		void CleanUp() {
			for (int i = setupsProperty.arraySize - 1; i >= 0; i--) {
				AudioSetup setup = setupsProperty.GetValue<AudioSetup>(i);
				
				if (setup == null) {
					setupsProperty.RemoveAt(i);
				}
				else if (setup.Source == null || setup.Source.clip == null) {
					setup.gameObject.Remove();
					setupsProperty.RemoveAt(i);
				}
			}
		}
		
		AudioSetup GetSetupFromClip(AudioClip clip) {
			GameObject gameObject = new GameObject(clip.name);
			Transform transform = gameObject.transform;
			AudioSource source = gameObject.AddComponent<AudioSource>();
			AudioSetup setup = gameObject.AddComponent<AudioSetup>();
			ComponentUtility.MoveComponentUp(setup);
			
			setup.SetUniqueName(clip.name, "", clip.name, setupsProperty.GetValues<AudioSetup>());
			setup.Initialize(audioPlayer);
			
			transform.parent = audioPlayer.transform;
			transform.Reset();
			
			source.clip = clip;
			source.loop = false;
			source.playOnAwake = false;
			
			return setup;
		}
	}
}

