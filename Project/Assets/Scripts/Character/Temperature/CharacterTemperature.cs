using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperature : StateLayer {
	
	[Range(-1, 0)] public float freezingThreshold = -0.75F;
	[Range(0, 1)] public float blazingThreshold = 0.75F;
	
	bool _temperatureCached;
	Temperature _temperature;
	public Temperature temperature { 
		get { 
			_temperature = _temperatureCached ? _temperature : GetComponent<Temperature>();
			_temperatureCached = true;
			return _temperature;
		}
	}
	
	bool _spriteRendererCached;
	SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { 
		get { 
			_spriteRenderer = _spriteRendererCached ? _spriteRenderer : GetComponentInChildren<SpriteRenderer>();
			_spriteRendererCached = true;
			return _spriteRenderer;
		}
	}
	
	public float CurrentTemperature {
		get {
			return temperature.temperature;
		}
	}
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
}
