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
		
		Layer.spriteRenderer.color = Layer.spriteRenderer.color.Lerp(new Color(1, 1 - Layer.temperatureInfo.Hotness, 1 - Layer.temperatureInfo.Coldness), Time.deltaTime * Layer.fadeSpeed, Channels.RGB);
		CharacterStatus characterStatus = (CharacterStatus) Layer.layer.layer;
		characterStatus.Die();
	}
}
