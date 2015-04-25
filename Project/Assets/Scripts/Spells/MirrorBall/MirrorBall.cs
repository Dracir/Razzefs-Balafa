using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class MirrorBall : MonoBehaviourExtended {

	[SerializeField, PropertyField] float bounce = 25;
	public float Bounce {
		get { 
			return bounce; 
		}
		set { 
			bounce = value; 
		}
	}
	
	[SerializeField, PropertyField] float hotness = 0.1F;
	public float Hotness {
		get { 
			return hotness; 
		}
		set { 
			hotness = value; 
		}
	}
	
	Vector2 direction;
	public Vector2 Direction {
		get { 
			return direction; 
		}
		set { 
			direction = value; 
		}
	}
	
	float speed;
	public float Speed {
		get { 
			return speed; 
		}
		set { 
			speed = value; 
		}
	}
	
	int bounces;
	public int Bounces {
		get { 
			return bounces; 
		}
		set { 
			bounces = value; 
		}
	}
	
	public Color Color {
		set {
			spriteRenderer.color = value;
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
	
	bool _rigidbody2DCached;
	Rigidbody2D _rigidbody2D;
	new public Rigidbody2D rigidbody2D { 
		get { 
			_rigidbody2D = _rigidbody2DCached ? _rigidbody2D : GetComponent<Rigidbody2D>();
			_rigidbody2DCached = true;
			return _rigidbody2D;
		}
	}
	
	public void Explode() {
		gameObject.Remove();
	}
	
	void Start() {
		rigidbody2D.SetVelocity(Direction * Speed);
	}
	
	void FixedUpdate() {
		rigidbody2D.SetVelocity(rigidbody2D.velocity.normalized * Speed);
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		Rigidbody2D collisionRigidbody = collision.gameObject.FindComponent<Rigidbody2D>();
		TemperatureInfo collisionTemperature = collision.gameObject.FindComponent<TemperatureInfo>();
		
		if (collisionRigidbody != null) {
			collisionRigidbody.SetVelocity((collision.transform.position - transform.position).normalized * Bounce);
		}
		
		if (collisionTemperature != null) {
			collisionTemperature.Temperature += Hotness;
		}
	}
}

