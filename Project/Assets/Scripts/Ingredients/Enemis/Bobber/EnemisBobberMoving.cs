using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobberMoving : State {
	
    EnemisBobber Layer {
    	get { return (EnemisBobber)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		if(Layer.temperature.IsFreezing){
			SwitchState<EnemisBobberFrozen>();
		}else if(Layer.temperature.IsFreezing){
			SwitchState<EnemisBobberExplosing>();
		}else{
			transform.parent.position += Layer.movementSpeed * Time.deltaTime * transform.parent.right;	
		}
	}
	
	public override void TriggerEnter2D(Collider2D collision) {
		base.TriggerEnter2D(collision);
	
		if(collision.tag == "Player"){
			SwitchState<EnemisBobberActivating>();
		}
		
	}
	
	
}
