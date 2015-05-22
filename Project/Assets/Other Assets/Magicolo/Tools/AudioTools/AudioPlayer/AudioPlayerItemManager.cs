using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo.AudioTools {
	[System.Serializable] 
	public class AudioPlayerItemManager {

		public AudioPlayer audioPlayer;
    	
		public AudioPlayerItemManager(AudioPlayer audioPlayer) {
			this.audioPlayer = audioPlayer;
		}
		
		public void Initialize(AudioPlayer audioPlayer) {
			this.audioPlayer = audioPlayer;
		}

		public AudioSourceItem GetSourceItem(string sourceName) {
			AudioSourceItem sourceItem = new AudioSourceItem(audioPlayer.SetupManager.GetSetup(sourceName), audioPlayer);
			
			return sourceItem;
		}
	}
}