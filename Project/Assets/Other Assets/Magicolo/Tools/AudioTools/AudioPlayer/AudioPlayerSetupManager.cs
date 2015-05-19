using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo.AudioTools {
	[System.Serializable]   
	public class AudioPlayerSetupManager {

		public List<AudioSetup> setups = new List<AudioSetup>();
		public AudioPlayer audioPlayer;
		
		Dictionary<string, AudioSetup> nameSetupDict;
		Dictionary<string, AudioSetup> NameSetupDict {
			get {
				if (nameSetupDict == null) {
					BuildSourceDict();
				}
				
				return nameSetupDict;
			}
		}
		
		public AudioPlayerSetupManager(AudioPlayer audioPlayer) {
			this.audioPlayer = audioPlayer;
		}
		
		public void Initialize(AudioPlayer audioPlayer) {
			this.audioPlayer = audioPlayer;
		}
		
		public AudioSetup GetSetup(string sourceName) {
			AudioSetup setup = null;
			
			try {
				setup = NameSetupDict[sourceName];
			}
			catch {
				Logger.LogError(string.Format("Audio setup named {0} was not found.", sourceName));
			}
			
			return setup;
		}
		
		public AudioSetup[] GetSetups() {
			return setups.ToArray();
		}
		
		public void AddSetup(AudioSetup setup) {
			if (!setups.Contains(setup)) {
				setups.Add(setup);
				
				NameSetupDict[setup.name] = setup;
			}
		}
		
		public void RemoveSetup(AudioSetup setup) {
			setups.Remove(setup);
			NameSetupDict.Remove(setup.name);
		}
		
		void BuildSourceDict() {
			nameSetupDict = new Dictionary<string, AudioSetup>();
			
			foreach (AudioSetup setup in setups) {
				nameSetupDict[setup.name] = setup;
			}
		}
	}
}