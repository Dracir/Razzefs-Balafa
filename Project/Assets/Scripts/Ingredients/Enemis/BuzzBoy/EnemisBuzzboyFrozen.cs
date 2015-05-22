using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBuzzboyFrozen : State {
	
    EnemisBuzzboy Layer {
    	get { return ((EnemisBuzzboy)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		if( Layer.temperature.IsFreezing ){
			if(Layer.temperature.wasFrozen){
				Layer.temperature.wasFrozen = false;
				SwitchState<EnemisBuzzboyDying>();
			}
		}else{
			SwitchState(Layer.lastStateTypeName);
		}
	}
}
