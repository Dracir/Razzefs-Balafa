using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.AudioTools;

namespace Magicolo {
	[System.Serializable]
	public abstract class AudioItem {

		readonly AudioPlayer audioPlayer;
		public AudioPlayer AudioPlayer {
			get {
				return audioPlayer;
			}
		}
		
		public bool IsPlaying {
			get {
				return State == AudioStates.FadingIn || State == AudioStates.FadingOut || State == AudioStates.Playing;
			}
		}
		
		public abstract AudioStates State { get; }
		
		protected AudioItem(AudioPlayer audioPlayer) {
			this.audioPlayer = audioPlayer;
		}
		
		public abstract AudioItem Play(params AudioOption[] options);
		
		public abstract AudioItem Play(float delay, params AudioOption[] options);
		
		public abstract AudioItem UnPause(params AudioOption[] options);
		
		public abstract AudioItem UnPause(float delay, params AudioOption[] options);
		
		public abstract AudioItem Pause(params AudioOption[] options);
		
		public abstract AudioItem Pause(float delay, params AudioOption[] options);
		
		public abstract AudioItem Stop(params AudioOption[] options);
		
		public abstract AudioItem Stop(float delay, params AudioOption[] options);
		
		public abstract AudioItem StopImmediate(params AudioOption[] options);
		
		public abstract AudioItem StopImmediate(float delay, params AudioOption[] options);
		
		public abstract AudioItem ApplyOptions(params AudioOption[] options);
		
		public abstract AudioItem OnPlay(AudioItemCallback onPlayCallback);
		
		public abstract AudioItem OnStop(AudioItemCallback onStopCallback);
		
		public abstract AudioItem OnPause(AudioItemCallback onPauseCallback);
		
		public abstract AudioItem OnUnPause(AudioItemCallback onUnPauseCallback);
	}
}