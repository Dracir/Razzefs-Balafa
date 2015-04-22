using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperatureIdle : State {
	
    CharacterTemperature Layer {
    	get { return ((CharacterTemperature)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.CurrentTemperature <= Layer.freezingThreshold){
			SwitchState<CharacterTemperatureFreezing>();
			return;
		}
		
		if (Layer.CurrentTemperature >= Layer.blazingThreshold){
			SwitchState<CharacterTemperatureBlazing>();
			return;
		}
	}
}
