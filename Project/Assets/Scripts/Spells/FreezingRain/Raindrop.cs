using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Raindrop : MonoBehaviourExtended {

	[SerializeField, Disable] float lifeCounter;
	public float LifeCounter {
		get {
			return lifeCounter;
		}
		set {
			lifeCounter = value;
		}
	}
	
	Color color;
	public Color Color {
		get {
			return color;
		}
		set {
			color = value;
			spriteRenderer.color = color;
		}
	}
	
	FreezingRain freezingRain;
	public FreezingRain FreezingRain {
		get {
			return freezingRain;
		}
		set {
			freezingRain = value;
		}
	}
	
	bool _spriteRendererCached;
	SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { 
		get { 
			_spriteRenderer = _spriteRendererCached ? _spriteRenderer : GetComponent<SpriteRenderer>();
			_spriteRendererCached = true;
			return _spriteRenderer;
		}
	}
	
	bool _rigidbody2DCached;
	Rigidbody2D _rigidbody2D;
	new public Rigidbody2D rigidbody2D { 
		get { 
			_rigidbody2D = _rigidbody2DCached ? _rigidbody2D : this.FindComponent<Rigidbody2D>();
			_rigidbody2DCached = true;
			return _rigidbody2D;
		}
	}
	
	void Update() {
		lifeCounter -= Time.deltaTime;
		
		if (lifeCounter <= 0) {
			FreezingRain.DespawnRaindrop(this);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.layer == LayerMask.NameToLayer("Trigger")){
		   	return;	
		}
		
		hitWith(collision.gameObject);
	}
	
	public void hitWith(GameObject hit){
		TemperatureInfo temperature = hit.FindComponent<TemperatureInfo>();
		Effector2D effector = hit.GetComponent<Effector2D>();
		MirrorBall mirror = hit.GetComponent<MirrorBall>();
	
		if (temperature != null) {
			temperature.Temperature -= FreezingRain.Coldness;
		}
	
		if (effector == null && mirror == null) {
			FreezingRain.DespawnRaindrop(this);
		}
	}
}

