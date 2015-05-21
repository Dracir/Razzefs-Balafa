using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobber : StateLayer {

    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public float movementSpeed = 5;
	public float activationTime = 1;
	public float explosionRadius = 1.5f;
	
	public LayerMask activationLayers;
	public LayerMask damageLayers;
	public float maxHeatDamage = 1;
	
	bool _audioPlayerCached;
	AudioPlayer _audioPlayer;
	public AudioPlayer audioPlayer { 
		get { 
			_audioPlayer = _audioPlayerCached ? _audioPlayer : transform.parent.GetComponentInChildren<AudioPlayer>();
			_audioPlayerCached = true;
			return _audioPlayer;
		}
	}
	
	[HideInInspector] public TemperatureInfo temperature;
	public ParticleSystem explosion;
	
	public override void OnAwake() {
		base.OnAwake();
		temperature = GetComponent<TemperatureInfo>();
	}
}
