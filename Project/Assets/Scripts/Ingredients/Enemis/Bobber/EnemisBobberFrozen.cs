using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobberFrozen : State {
	
    EnemisBobber Layer {
    	get { return (EnemisBobber)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if(!Layer.temperature.IsFreezing){
			SwitchState<EnemisBobberFalling>();
		}
	}
}
