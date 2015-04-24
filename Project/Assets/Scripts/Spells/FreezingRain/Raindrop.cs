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
	
	void Update() {
		lifeCounter -= Time.deltaTime;
		
		if (lifeCounter <= 0) {
			FreezingRain.DespawnRaindrop(this);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collision) {
		Effector2D effector = collision.GetComponent<Effector2D>();
		
		if (effector == null) {
			FreezingRain.DespawnRaindrop(this);
		}
	}
}

