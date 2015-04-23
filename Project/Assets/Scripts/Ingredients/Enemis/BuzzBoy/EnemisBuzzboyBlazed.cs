using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBuzzboyBlazed : State {
	
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
		if( Layer.temperature.IsBlazing ){
			if(Layer.temperature.wasBlazed){
				SwitchState<EnemisBuzzboyDying>();
			}
		}else{
			SwitchState(Layer.lastStateTypeName);
		}
	}
}
