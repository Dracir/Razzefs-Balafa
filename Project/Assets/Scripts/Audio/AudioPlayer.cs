using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InAudioSystem;
using Magicolo;

public class AudioPlayer : MonoBehaviourExtended {

	public InAudioEvent musicEvent;
	
	void Start() {
		InAudio.PostEvent(gameObject, musicEvent);
	}
}

