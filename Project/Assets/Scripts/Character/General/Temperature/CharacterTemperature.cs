using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperature : StateLayer {
	
	[Min] public float fadeSpeed = 5;
	
	bool _temperatureInfoCached;
	TemperatureInfo _temperatureInfo;
	public TemperatureInfo temperatureInfo { 
		get { 
			_temperatureInfo = _temperatureInfoCached ? _temperatureInfo : GetComponent<TemperatureInfo>();
			_temperatureInfoCached = true;
			return _temperatureInfo;
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
	
	CharacterLive Layer {
		get { return ((CharacterLive)layer); }
	}
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public void Freeze() {
		Layer.SwitchState<CharacterLiveIdle>();
	}
	
	public void Unfreeze() {
		Layer.SwitchState<CharacterMotion>();
	}
}
