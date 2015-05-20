using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterStatus : StateLayer {
	
	public bool invincible;
	public float RespawnTime = 4f;
	
	bool _spriteRendererCached;
	SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { 
		get { 
			_spriteRenderer = _spriteRendererCached ? _spriteRenderer : this.FindComponent<SpriteRenderer>();
			_spriteRendererCached = true;
			return _spriteRenderer;
		}
	}
	
	bool _boxColliderCached;
	BoxCollider2D _boxCollider;
	public BoxCollider2D boxCollider { 
		get { 
			_boxCollider = _boxColliderCached ? _boxCollider : this.FindComponent<BoxCollider2D>();
			_boxColliderCached = true;
			return _boxCollider;
		}
	}
		
	bool _circleColliderCached;
	CircleCollider2D _circleCollider;
	public CircleCollider2D circleCollider { 
		get { 
			_circleCollider = _circleColliderCached ? _circleCollider : this.FindComponent<CircleCollider2D>();
			_circleColliderCached = true;
			return _circleCollider;
		}
	}
	
	bool _rigidBodyCached;
	Rigidbody2D _rigidBody;
	public Rigidbody2D rigidBody { 
		get { 
			_rigidBody = _rigidBodyCached ? _rigidBody : this.FindComponent<Rigidbody2D>();
			_rigidBodyCached = true;
			return _rigidBody;
		}
	}
	
	bool _AnimatorCached;
	Animator _animator;
	public Animator animator { 
		get { 
			_animator = _AnimatorCached ? _animator : this.FindComponent<Animator>();
			_AnimatorCached = true;
			return _animator;
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
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public CharacterDetail detail;
	public Sprite holdFlyingSprite;
	
	public override void OnAwake(){
		base.OnAwake();
		detail = GetComponent<CharacterDetail>();
	}
	
	
	public void Die() {
		Logger.Log("WHY ME!!!");
		
		if (!invincible) {
			SwitchState<CharacterDie>().SwitchState<CharacterDieDying>();
			audioPlayer.Play("Death1");
			audioPlayer.Play("Death2");
		}
	}
	
	public void setColliders(bool active){
		circleCollider.enabled = active;
		boxCollider.enabled = active;
	}

	public bool isAlive(){
		return GetState<CharacterLive>().IsActive;
	}
}
