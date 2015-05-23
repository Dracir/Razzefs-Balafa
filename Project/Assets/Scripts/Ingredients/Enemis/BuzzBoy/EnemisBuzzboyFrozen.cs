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
		Layer.Rotating = false;
	}
	
	public override void OnExit() {
		base.OnExit();
		Layer.Rotating = true;
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		if( ! Layer.temperature.IsFreezing ){
			SwitchState<EnemisBuzzboyActive>();
			
		}else{
			/*if(Layer.temperature.wasFrozen){
				Layer.temperature.wasFrozen = false;
				SwitchState<EnemisBuzzboyDying>();
			}*/
		}
	}
}
