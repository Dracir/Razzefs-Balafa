using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperature : StateLayer {
	
	[Min] public float fadeSpeed = 3;
	[Min(0.001F)] public float frozenMassModifier = 10;
	
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
		
		animator.enabled = false;
		rigidbody.mass *= frozenMassModifier;
	}
	
	public void Unfreeze() {
		Layer.GetState<CharacterMotion>().Enable();
		
		animator.enabled = true;
		rigidbody.mass /= frozenMassModifier;
	}
}
