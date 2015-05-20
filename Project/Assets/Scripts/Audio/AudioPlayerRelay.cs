using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class AudioPlayerRelay : MonoBehaviourExtended {

	bool _audioPlayerCached;
	AudioPlayer _audioPlayer;
	public AudioPlayer audioPlayer { 
		get { 
			_audioPlayer = _audioPlayerCached ? _audioPlayer : this.FindComponent<AudioPlayer>();
			_audioPlayerCached = true;
			return _audioPlayer;
		}
	}
    
	public void Play(string sourceName) {
		audioPlayer.Play(sourceName);
	}
}

