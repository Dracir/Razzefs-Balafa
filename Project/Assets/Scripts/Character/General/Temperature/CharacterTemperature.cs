using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperature : StateLayer {
	
	[Min] public float fadeSpeed = 3;
	[Min(0.001F)] public float frozenMassModifier = 10;

	//To calculate temperature increase from collisions, we find the force of collision, subtract the forceTemperatureThreshold (to a minimum of 0), then divide by forceToTemperatureRatio.
	public float forceTemperatureThreshold = 25f;
	public float forceToTemperatureRatio = 75f;

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
	
	[Disable] public float initialMoveSpeed;
	
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
	
	bool _characterMoveMovingCached;
	CharacterMoveMoving _characterMoveMoving;
	public CharacterMoveMoving characterMoveMoving { 
		get { 
			_characterMoveMoving = _characterMoveMovingCached ? _characterMoveMoving : this.FindComponent<CharacterMoveMoving>();
			_characterMoveMovingCached = true;
			return _characterMoveMoving;
		}
	}
	
	CharacterLive Layer {
		get { return ((CharacterLive)layer); }
	}
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnAwake() {
		base.OnAwake();
		
		initialMoveSpeed = characterMoveMoving.speed;
	}
	
	public override void CollisionEnter2D(Collision2D collision) {
		base.CollisionEnter2D(collision);
		Debug.Log("Should b gettin hottah");
		temperatureInfo.Temperature += Mathf.Max(collision.relativeVelocity.magnitude - forceTemperatureThreshold, 0f) / forceToTemperatureRatio; 
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
