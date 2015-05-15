using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class FreezingRain : MonoBehaviourExtended {

	public Pool rainPool;
	public Pool dropFXPool;
	
	[SerializeField, PropertyField] float coldness = 0.01F;
	public float Coldness {
		get { 
			return coldness; 
		}
		set { 
			coldness = value; 
		}
	}
	
	[SerializeField, PropertyField(typeof(MinAttribute))] float lifeTime = 2;
	public float LifeTime {
		get {
			return lifeTime;
		}
		set {
			lifeTime = value;
		}
	}
	
	[SerializeField, PropertyField(typeof(RangeAttribute), 0, 6)] float width = 3;
	public float Width {
		get {
			return width;
		}
		set {
			width = value;
			halfWidth = width / 2;
		}
	}
	
	[SerializeField, PropertyField] float emission = 100;
	public float Emission {
		get {
			return emission;
		}
		set {
			emission = value;
			delay = 1F / emission;
		}
	}
	
	[SerializeField, PropertyField(typeof(MinAttribute))] float minSize = 0.25F;
	public float MinSize {
		get {
			return minSize;
		}
		set {
			minSize = value;
		}
	}
	
	[SerializeField, PropertyField(typeof(MinAttribute))] float maxSize = 0.75F;
	public float MaxSize {
		get {
			return maxSize;
		}
		set {
			maxSize = value;
		}
	}
	
	[SerializeField, PropertyField] Color color1 = Color.white;
	public Color Color1 {
		get {
			return color1;
		}
		set {
			color1 = value;
		}
	}
	
	[SerializeField, PropertyField] Color color2 = Color.white;
	public Color Color2 {
		get {
			return color2;
		}
		set {
			color2 = value;
		}
	}
	
	bool _particleSystemCached;
	ParticleSystem _particleSystem;
	new public ParticleSystem particleSystem { 
		get { 
			_particleSystem = _particleSystemCached ? _particleSystem : this.FindComponent<ParticleSystem>();
			_particleSystemCached = true;
			return _particleSystem;
		}
	}
	
	float delay;
	float counter;
	float halfWidth;
	
	void Awake() {
		delay = 1F / Emission;
		halfWidth = Width / 2;
	}
	
	void Update() {
		counter -= Time.deltaTime;
		
		while (counter <= 0) {
			SpawnRaindrop();
			counter += delay;
		}
	}
	
	void SpawnRaindrop() {
		Raindrop raindrop;
		Vector3 position = new Vector3(Random.Range(-halfWidth, halfWidth), 0, 0);
		float size = Random.Range(minSize, maxSize);
		Vector3 scale = new Vector3(size, size, 1);
		
		raindrop = rainPool.Spawn<Raindrop>(transform, position, Quaternion.identity, scale);
		raindrop.FreezingRain = this;
		raindrop.Color = Color.Lerp(Color1, Color2, Random.value);
		raindrop.LifeCounter = LifeTime;
	}
	
	public void DespawnRaindrop(Raindrop raindrop) {
		ParticleSystem particleFX = dropFXPool.Spawn<ParticleSystem>(transform, raindrop.transform.localPosition);
		
		StartCoroutine(DespawnAfterPlaying(particleFX));
		
		rainPool.Despawn(raindrop);
	}
	
	IEnumerator DespawnAfterPlaying(ParticleSystem particleFX) {
		particleFX.Stop();
		particleFX.Simulate(0);
		particleFX.Play();
		
		while (particleFX.isPlaying) {
			yield return new WaitForSeconds(0);
		}
		
		dropFXPool.Despawn(particleFX);
	}
	
	public void Explode() {
		gameObject.Remove();
	}
}

