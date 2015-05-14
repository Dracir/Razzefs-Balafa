using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterStatus : StateLayer {
	
	public bool invincible;
	public float timeBeforeSpiritLeaveCorpse;
	public float minRespawnTime = 0f;
	public float maxRespawnTime = 4f;
	public float maxDistance = 15;
	
	
	bool _spriteRendererCached;
	SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { 
		get { 
			_spriteRenderer = _spriteRendererCached ? _spriteRenderer : GetComponentInChildren<SpriteRenderer>();
			_spriteRendererCached = true;
			return _spriteRenderer;
		}
	}
	
	bool _boxColliderCached;
	BoxCollider2D _boxCollider;
	public BoxCollider2D boxCollider { 
		get { 
			_boxCollider = _boxColliderCached ? _boxCollider : GetComponentInChildren<BoxCollider2D>();
			_boxColliderCached = true;
			return _boxCollider;
		}
	}
		
	bool _circleColliderCached;
	CircleCollider2D _circleCollider;
	public CircleCollider2D circleCollider { 
		get { 
			_circleCollider = _circleColliderCached ? _circleCollider : GetComponentInChildren<CircleCollider2D>();
			_circleColliderCached = true;
			return _circleCollider;
		}
	}
	
	bool _rigidBodyCached;
	Rigidbody2D _rigidBody;
	public Rigidbody2D rigidBody { 
		get { 
			_rigidBody = _rigidBodyCached ? _rigidBody : GetComponentInChildren<Rigidbody2D>();
			_rigidBodyCached = true;
			return _rigidBody;
		}
	}
	
	bool _AnimatorCached;
	Animator _animator;
	public Animator animator { 
		get { 
			_animator = _AnimatorCached ? _animator : GetComponentInChildren<Animator>();
			_AnimatorCached = true;
			return _animator;
		}
	}
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public void Die() {
		if (!invincible) {
			SwitchState<CharacterDie>().SwitchState<CharacterDieDying>();
		}
	}
	
	
	
	public void setColliders(bool active){
		circleCollider.enabled = active;
		boxCollider.enabled = active;
	}
}
