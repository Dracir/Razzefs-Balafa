using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.AudioTools;

namespace Magicolo {
	[System.Serializable]
	public class AudioSourceItem : AudioItem {
		
		public override AudioStates State {
			get {
				return setup.State;
			}
		}
		
		readonly AudioSetup setup;

		public AudioSourceItem(AudioSetup setup, AudioPlayer audioPlayer)
			: base(audioPlayer) {
			
			this.setup = setup;
		}

		public override AudioItem Play(params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.Play();
			
			return this;
		}

		public override AudioItem Play(float delay, params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.Play(delay);
			
			return this;
		}

		public override AudioItem Pause(params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.Pause();
			
			return this;
		}

		public override AudioItem Pause(float delay, params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.Pause(delay);
			
			return this;
		}

		public override AudioItem UnPause(params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.UnPause();
			
			return this;
		}

		public override AudioItem UnPause(float delay, params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.UnPause(delay);
			
			return this;
		}

		public override AudioItem Stop(params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.Stop();
			
			return this;
		}
		
		public override AudioItem Stop(float delay, params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.Stop(delay);
			
			return this;
		}
		
		public override AudioItem StopImmediate(params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.StopImmediate();
			
			return this;
		}
		
		public override AudioItem StopImmediate(float delay, params AudioOption[] options) {
			ApplyOptions(options);
			
			setup.StopImmediate(delay);
			
			return this;
		}
		
		public override AudioItem ApplyOptions(params AudioOption[] options) {
			foreach (AudioOption option in options) {
				option.Apply(setup);
			}
			
			return this;
		}

		public override AudioItem OnPlay(AudioItemCallback onPlayCallback) {
			setup.OnPlay(onPlayCallback);
			
			return this;
		}

		public override AudioItem OnStop(AudioItemCallback onStopCallback) {
			setup.OnStop(onStopCallback);
			
			return this;
		}

		public override AudioItem OnPause(AudioItemCallback onPauseCallback) {
			setup.OnPause(onPauseCallback);
			
			return this;
		}

		public override AudioItem OnUnPause(AudioItemCallback onUnPauseCallback) {
			setup.OnUnPause(onUnPauseCallback);
			
			return this;
		}
	}
}