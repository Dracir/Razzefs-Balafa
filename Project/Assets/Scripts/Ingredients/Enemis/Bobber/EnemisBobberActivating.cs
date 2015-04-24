using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobberActivating : State {
	
    EnemisBobber Layer {
    	get { return (EnemisBobber)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public float t;
	
	public override void OnEnter() {
		base.OnEnter();
		t = Layer.activationTime;
		GetComponent<Animator>().SetTrigger("Activation");
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		t -= Time.deltaTime;
		if(t<=0){
			SwitchState<EnemisBobberExplosing>();
		}
	}
}
