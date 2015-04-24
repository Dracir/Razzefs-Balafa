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
		
		if (Layer.temperatureInfo.IsFreezing){
			SwitchState<CharacterTemperatureFreezing>();
			return;
		}
		
		if (Layer.temperatureInfo.IsBlazing){
			SwitchState<CharacterTemperatureBlazing>();
			return;
		}
		
		Layer.spriteRenderer.color = Layer.spriteRenderer.color.Lerp(Color.white, Time.deltaTime * Layer.fadeSpeed, Channels.RGB);
	}
}
