using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class MusicPlayer : MonoBehaviourExtended {

	bool _audioPlayerCached;
	AudioPlayer _audioPlayer;
	public AudioPlayer audioPlayer { 
		get { 
			_audioPlayer = _audioPlayerCached ? _audioPlayer : this.FindComponent<AudioPlayer>();
			_audioPlayerCached = true;
			return _audioPlayer;
		}
	}
    
	void Start() {
		audioPlayer.Play("level1");
	}
}

