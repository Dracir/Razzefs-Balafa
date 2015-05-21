using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class MirrorBall : MonoBehaviourExtended {

	float minBounce;
	public float MinBounce {
		get { 
			return minBounce; 
		}
		set { 
			minBounce = value; 
		}
	}
	
	float maxBounce;
	public float MaxBounce {
		get {
			return maxBounce;
		}
		set {
			maxBounce = value;
		}
	}
	
	float velocityInherit;
	public float VelocityInherit {
		get {
			return velocityInherit;
		}
		set {
			velocityInherit = value;
		}
	}
	
	float hotness;
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
			BounceCollider(collisionRigidbody);
		}
		
		if (collisionTemperature != null) {
			collisionTemperature.Heat(Hotness);
		}
		
		if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
			Bounces -= 1;
		}
		
		if (Bounces == 0) {
			Explode();
		}
	}
	
	void OnTriggerEnter2D(Collider2D collision) {
		Raindrop raindrop = collision.GetComponent<Raindrop>();
		
		if (raindrop != null) {
			BounceTrigger(raindrop.rigidbody2D);
		}
	}
	
	void BounceCollider(Rigidbody2D colliderRigidbody) {
		colliderRigidbody.SetVelocity(colliderRigidbody.velocity.normalized * Mathf.Clamp(colliderRigidbody.velocity.magnitude + rigidbody2D.velocity.magnitude * VelocityInherit, MinBounce, MaxBounce));
	}
	
	void BounceTrigger(Rigidbody2D triggerRigidbody) {
		triggerRigidbody.SetVelocity(Vector3.Reflect(triggerRigidbody.velocity, (transform.position - triggerRigidbody.transform.position).normalized));
		
		BounceCollider(triggerRigidbody);
	}
}

