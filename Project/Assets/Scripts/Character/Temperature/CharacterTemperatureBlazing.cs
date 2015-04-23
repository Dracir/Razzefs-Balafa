using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperatureBlazing : State {
	
	CharacterTemperature Layer {
		get { return ((CharacterTemperature)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (!Layer.temperature.IsBlazing){
			SwitchState<CharacterTemperatureIdle>();
			return;
		}
	}
}
