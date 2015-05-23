using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobberExplosing : State {
	
	EnemisBobber Layer {
		get { return (EnemisBobber)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		transform.parent.GetComponent<Collider2D>().enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
		
	 	transform.parent.GetComponent<Rigidbody2D>().isKinematic = true;
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.parent.position, Layer.explosionRadius, Layer.damageLayers.value);
		foreach (var otherCollider in colliders) {
			TemperatureKiller killer = otherCollider.GetComponent<TemperatureKiller>();
			
			if (killer != null) {
				killer.fireDamage(transform.parent.position, Layer.maxHeatDamage, Layer.explosionRadius);
			}
		}
		
		StartCoroutine(DespawnAfterPlaying(Layer.explosion, Layer.audioPlayer.Play("Explosion1"), Layer.audioPlayer.Play("Explosion2"), Layer.audioPlayer.Play("Explosion3")));
	}
	
	IEnumerator DespawnAfterPlaying(ParticleSystem particleFX, params AudioSourceItem[] sources) {
		particleFX.Simulate(0);
		particleFX.Play();
		
		while (particleFX.isPlaying || !System.Array.TrueForAll(sources, source => source.State == AudioStates.Stopped)) {
			yield return new WaitForSeconds(0.1f);
		}
		
		transform.parent.gameObject.Remove();
		
		//dropFXPool.Despawn(particleFX);
	}
}
