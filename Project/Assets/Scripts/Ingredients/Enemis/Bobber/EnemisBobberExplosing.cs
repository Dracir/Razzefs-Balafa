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
		
		Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.parent.position, Layer.explosionRadius, Layer.activationLayers.value );
		foreach (var otherCollider in colliders) {
			Transform otherTranform = otherCollider.transform.parent;
			TemperatureInfo otherTemperature = otherTranform.GetComponent<TemperatureInfo>();
			if(otherTemperature){
				float distance = (otherTranform.position - transform.parent.position).magnitude;
				if(distance <= Layer.explosionRadius){
					float t = distance / Layer.explosionRadius;
					float damage = Mathf.Lerp(Layer.maxHeatDamage, 0 , t*t*(3f-2f*t));
					otherTemperature.Temperature = damage;
				} 
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
