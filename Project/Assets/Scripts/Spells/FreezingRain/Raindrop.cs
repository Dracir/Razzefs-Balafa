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
	
	void OnCollisionEnter2D(Collision2D collision) {
		/*if(collision.gameObject.layer == LayerMask.NameToLayer("Trigger")) {
			Debug.Log("Dont die");
			return;
		} 
		Debug.Log(collision.gameObject.layer);*/
		TemperatureInfo temperature = collision.collider.FindComponent<TemperatureInfo>();
		
//		if (collision.collider.GetComponent<Effector2D>() == null && collision.collider.GetComponent<MirrorBall>() == null) {
//		}
		
		if (temperature != null) {
			temperature.Temperature -= FreezingRain.Coldness;
			FreezingRain.DespawnRaindrop(this);
		}
	}
	
	//	void OnTriggerEnter2D(Collider2D collision) {
	//		TemperatureInfo temperature = collision.FindComponent<TemperatureInfo>();
	//		Effector2D effector = collision.GetComponent<Effector2D>();
	//
	//		if (temperature != null) {
	//			temperature.Temperature -= FreezingRain.Coldness;
	//		}
	//
	//		if (effector == null) {
	//			FreezingRain.DespawnRaindrop(this);
	//		}
	//	}
}

