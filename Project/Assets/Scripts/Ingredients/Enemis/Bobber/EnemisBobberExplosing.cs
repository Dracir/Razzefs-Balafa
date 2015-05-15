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
		GetComponent<SpriteRenderer>().enabled = false;
		//GetComponent<Animator>().SetTrigger("Activating");
		
		Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.parent.position, Layer.explosionRadius, Layer.damageLayers.value );
		foreach (var otherCollider in colliders) {
			TemperatureKiller killer = otherCollider.GetComponent<TemperatureKiller>();
			if(killer){
				killer.fireDamage(transform.parent.position, Layer.maxHeatDamage, Layer.explosionRadius);
			}
		}
		
		StartCoroutine(DespawnAfterPlaying(Layer.explosion));
	}
	
	IEnumerator DespawnAfterPlaying(ParticleSystem particleFX) {
		particleFX.Simulate(0);
		particleFX.Play();
		while(particleFX.isPlaying){
			yield return new WaitForSeconds(0.1f);
		}
		
		transform.parent.gameObject.Remove();
		
		//dropFXPool.Despawn(particleFX);
	}
}
