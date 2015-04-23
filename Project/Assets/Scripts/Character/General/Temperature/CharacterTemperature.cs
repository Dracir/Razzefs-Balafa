using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperature : StateLayer {
	
	[Min] public float fadeSpeed = 5;
	
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
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
}
