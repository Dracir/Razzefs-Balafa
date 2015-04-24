using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobberFalling : State {
	
    EnemisBobber Layer {
    	get { return (EnemisBobber)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		transform.parent.GetComponent<Rigidbody2D>().gravityScale = 1;
	}
	
	public override void OnExit() {
		base.OnExit();
		transform.parent.GetComponent<Rigidbody2D>().gravityScale = 0;
	}
	
	public override void OnUpdate() {
		if(Layer.temperature.IsFreezing){
			SwitchState<EnemisBobberFrozen>();
		}else if(Layer.temperature.IsFreezing){
			SwitchState<EnemisBobberExplosing>();
		}
	}
	
	public override void TriggerEnter2D(Collider2D collision) {
		base.TriggerEnter2D(collision);
		
		SwitchState<EnemisBobberActivating>();
	}
}
