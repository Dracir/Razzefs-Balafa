using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.AudioTools;

// Enumerator container that plays his sources one at a time sequentially with each play event
// Random container that plays one of his sources randomly
// Switch container that plays one of his sources based on the state of an enum field
// Blend container that plays all his sources and crossfades between them based on a float field
// Sequence container that plays all his sources one after the other
// Mix container that plays all his sources
// RTPCs that will control sources parameters based on a float value and AnimationCurve
// Allow tweening of other parameters (stereo pan, spatial blend, etc.)
// DelayOptions enum for AudioOption to decide if delays will continue counting while the source is stopped or paused

namespace Magicolo {
	[AddComponentMenu("Magicolo/Audio/Audio Player")]
	public class AudioPlayer : MonoBehaviourExtended {

		[SerializeField] AudioPlayerSetupManager setupManager;
		public AudioPlayerSetupManager SetupManager {
			get {
				if (setupManager == null) {
					setupManager = new AudioPlayerSetupManager(this);
				}
				
				return setupManager;
			}
		}
		
		[SerializeField] AudioPlayerItemManager itemManager;
		public AudioPlayerItemManager ItemManager {
			get {
				if (itemManager == null) {
					itemManager = new AudioPlayerItemManager(this);
				}
				
				return itemManager;
			}
		}
		
		public AudioSourceItem Play(string sourceName, float delay, params AudioOption[] options) {
			AudioSourceItem sourceItem = ItemManager.GetSourceItem(sourceName);
			
			sourceItem.Play(delay, options);
			
			return sourceItem;
		}
		
		public AudioSourceItem Play(string sourceName, params AudioOption[] options) {
			return Play(sourceName, 0, options);
		}
		
		void Reset() {
			SetupManager.Initialize(this);
			ItemManager.Initialize(this);
			
			foreach (AudioSetup setup in this.GetComponentsInChildrenExclusive<AudioSetup>()) {
				setup.gameObject.Remove();
			}
		}
	}
}
