using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperature : StateLayer {
	
	[Min] public float fadeSpeed = 3;

	//To calculate temperature increase from collisions, we find the force of collision, subtract the forceTemperatureThreshold (to a minimum of 0), then divide by forceToTemperatureRatio.
	public float forceTemperatureThreshold = 25;
	public float forceToTemperatureRatio = 75;

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
			_animator = _animatorCached ? _animator : this.FindComponent<Animator>();
			_animatorCached = true;
			return _animator;
		}
	}
	
	bool _rigidbodyCached;
	Rigidbody2D _rigidbody;
	new public Rigidbody2D rigidbody { 
		get { 
			_rigidbody = _rigidbodyCached ? _rigidbody : this.FindComponent<Rigidbody2D>();
			_rigidbodyCached = true;
			return _rigidbody;
		}
	}

	bool _temperatureInfoCached;
	TemperatureInfo _temperatureInfo;
	public TemperatureInfo temperatureInfo { 
		get { 
			_temperatureInfo = _temperatureInfoCached ? _temperatureInfo : this.FindComponent<TemperatureInfo>();
			_temperatureInfoCached = true;
			return _temperatureInfo;
		}
	}
	
	bool _spriteRendererCached;
	SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { 
		get { 
			_spriteRenderer = _spriteRendererCached ? _spriteRenderer : this.FindComponent<SpriteRenderer>();
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
	
	bool _audioPlayerCached;
	AudioPlayer _audioPlayer;
	public AudioPlayer audioPlayer { 
		get { 
			_audioPlayer = _audioPlayerCached ? _audioPlayer : this.FindComponent<AudioPlayer>();
			_audioPlayerCached = true;
			return _audioPlayer;
		}
	}
	
	CharacterLive Layer {
		get { return ((CharacterLive)layer); }
	}
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	bool hasCollidedThisFrame = false;
	
	public override void OnAwake() {
		base.OnAwake();
		
		initialMoveSpeed = characterMoveMoving.speed;
	}
	
	public override void CollisionEnter2D(Collision2D collision) {
		base.CollisionEnter2D(collision);
		CollisionHeat(collision);
	}
	
	void CollisionHeat(Collision2D collision) {
		if (collision.gameObject.tag == "MirrorBall" || hasCollidedThisFrame) {
			return;
		}
		
		float otherMass = collision.rigidbody == null ? 1 : collision.rigidbody.mass;
		float force = collision.relativeVelocity.magnitude * otherMass;
		
		if (force < forceTemperatureThreshold) {
			return;
		}
		
		float damages = force / forceToTemperatureRatio;
		
		if (damages > 0) {
			Debug.Log("Damages: " + damages);
			Debug.Log("velocity before subtraction: " + collision.relativeVelocity.magnitude * otherMass);
		}
		
		temperatureInfo.Temperature += damages; 
		hasCollidedThisFrame = true;
	}
	
	public void Freeze() {
		Layer.GetState<CharacterMotion>().Disable();
		
		animator.enabled = false;
		rigidbody.isKinematic = true;
		audioPlayer.Play("Freeze");
	}
	
	public void Unfreeze() {
		Layer.GetState<CharacterMotion>().Enable();
		
		animator.enabled = true;
		rigidbody.isKinematic = false;
		audioPlayer.Play("UnFreeze");
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		hasCollidedThisFrame = false;
	}
}
