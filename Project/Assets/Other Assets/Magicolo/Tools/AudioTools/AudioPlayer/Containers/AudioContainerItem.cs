using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.AudioTools;

namespace Magicolo {
	[System.Serializable]
	public class AudioContainerItem : AudioItem {

		public override AudioStates State {
			get {
				throw new System.NotImplementedException();
			}
		}

		public AudioContainerItem(AudioPlayer audioPlayer)
			: base(audioPlayer) {
		}

		public override AudioItem Play(params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem Play(float delay, params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem UnPause(params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem UnPause(float delay, params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem Pause(params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem Pause(float delay, params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem Stop(params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem Stop(float delay, params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem StopImmediate(params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem StopImmediate(float delay, params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem ApplyOptions(params AudioOption[] options) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem OnPlay(AudioItemCallback onPlayCallback) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem OnStop(AudioItemCallback onStopCallback) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem OnPause(AudioItemCallback onPauseCallback) {
			throw new System.NotImplementedException();
		}
		
		public override AudioItem OnUnPause(AudioItemCallback onUnPauseCallback) {
			throw new System.NotImplementedException();
		}
	}
}