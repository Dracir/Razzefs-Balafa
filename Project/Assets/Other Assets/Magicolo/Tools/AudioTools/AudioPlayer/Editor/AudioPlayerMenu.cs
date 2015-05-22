using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Magicolo.AudioTools {
	public static class AudioPlayerMenus {
	
		[MenuItem("Magicolo's Tools/Create/Audio Player")]
		static void CreateAudioPlayer() {
			GameObject activeGameObject = Selection.activeGameObject;
			AudioPlayer existingAudioPlayer = activeGameObject == null ? null : activeGameObject.GetComponentInChildren<AudioPlayer>();
			
			if (existingAudioPlayer == null) {
				GameObject gameObject;
		
				gameObject = new GameObject("Audio");
				gameObject.AddComponent<AudioPlayer>();
				Undo.RegisterCreatedObjectUndo(gameObject, "Audio Player Created");
				
				gameObject.transform.parent = activeGameObject == null ? null : activeGameObject.transform;
				activeGameObject = gameObject;
			}
			else {
				activeGameObject = existingAudioPlayer.gameObject;
			}
			
			Selection.activeGameObject = activeGameObject;
		}
	}
}