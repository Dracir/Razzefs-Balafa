using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperature : StateLayer {
	
	[Min] public float fadeSpeed = 3;
	[Min(0.001F)] public float frozenMassModifier = 10;

	//To calculate temperature increase from collisions, we find the force of collision, subtract the forceTemperatureThreshold (to a minimum of 0), then divide by forceToTemperatureRatio.
	public float forceTemperatureThreshold = 15f;
	public float forceToTemperatureRatio = 50f;

	
	[SerializeField, Disable] bool frozen;
	public bool Frozen {
		get {
			return frozen;
		}
		set {
			if (frozen != value) {
				frozen = value;
				
				if (frozen) {
					Freeze();
				}
				else {
					Unfreeze();
				}
			}
		}
	}
	bool _animatorCached;
	Animator _animator;
	public Animator animator { 
		get { 
			_animator = _animatorCached ? _animator : GetComponent<Animator>();
			_animatorCached = true;
			return _animator;
		}
	}
	
	bool _rigidbodyCached;
	Rigidbody2D _rigidbody;
	new public Rigidbody2D rigidbody { 
		get { 
			_rigidbody = _rigidbodyCached ? _rigidbody : GetComponent<Rigidbody2D>();
			_rigidbodyCached = true;
			return _rigidbody;
		}
	}

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
		Layer.GetState<CharacterMotion>().Disable();
		Layer.GetState<CharacterCast>().Disable();
		
		animator.enabled = false;
		rigidbody.mass *= frozenMassModifier;
	}
	
	public void Unfreeze() {
		Layer.GetState<CharacterMotion>().Enable();
		Layer.GetState<CharacterCast>().Enable();
		
		animator.enabled = true;
		rigidbody.mass /= frozenMassModifier;
	}

	void OnCollisionEnter2D(Collision2D coll){
		temperatureInfo.Temperature += Mathf.Max(coll.relativeVelocity.magnitude - forceTemperatureThreshold, 0f) / forceToTemperatureRatio; 
		Debug.Log (this.gameObject.name + " has collided with force " + coll.relativeVelocity.magnitude + " and has reached temperature " + temperatureInfo.Temperature);
	}
}
