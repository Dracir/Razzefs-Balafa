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
		
		if (!Layer.temperatureInfo.IsBlazing) {
			SwitchState<CharacterTemperatureIdle>();
			return;
		}
		
		Layer.spriteRenderer.color = Layer.spriteRenderer.color.Lerp(new Color(1, 1 - Layer.temperatureInfo.Hotness, 1 - Layer.temperatureInfo.Coldness), Time.deltaTime * Layer.fadeSpeed, Channels.RGB);
		CharacterStatus characterStatus = (CharacterStatus)Layer.layer.layer;
		Layer.characterMoveMoving.speed = Layer.initialMoveSpeed + Layer.initialMoveSpeed * Layer.temperatureInfo.Hotness * Layer.heatSpeedBoost;
		
		if (Layer.temperatureInfo.wasBlazed) {
			Layer.temperatureInfo.wasBlazed = false;
			Layer.temperatureInfo.ResetTemperature();
			
			SwitchState<CharacterTemperatureIdle>();
			characterStatus.Die();
		}
	}
}
