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
		GetComponent<Animator>().SetTrigger("Activating");
		
		Collider2D [] colliders = Physics2D.OverlapCircleAll(transform.parent.position, Layer.explosionRadius, Layer.activationLayers.value );
		foreach (var otherCollider in colliders) {
			Temperature otherTemperature = otherCollider.GetComponent<Temperature>();
			if(otherTemperature){
				float distance = (otherCollider.transform.position - transform.parent.position).magnitude;
				Debug.Log(distance);
				if(distance <= Layer.explosionRadius){
					otherTemperature.temperature += ( Layer.explosionRadius - distance ) / Layer.explosionRadius * Layer.maxHeatDamage;
				} 
			}
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
