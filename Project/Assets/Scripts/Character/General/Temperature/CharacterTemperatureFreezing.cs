using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperatureFreezing : State {
	
	public float freezeDuration = 1;
	
	[Disable] public float freezeCounter;
	
	CharacterTemperature Layer {
		get { return ((CharacterTemperature)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.Frozen = false;
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (!Layer.temperatureInfo.IsFreezing) {
			SwitchState<CharacterTemperatureIdle>();
			return;
		}
		
		Color targetColor = Layer.Frozen ? Color.blue : new Color(1 - Layer.temperatureInfo.Coldness, 1 - Layer.temperatureInfo.Coldness, 1);
		Layer.spriteRenderer.color = Layer.spriteRenderer.color.Lerp(targetColor, Time.deltaTime * Layer.fadeSpeed, Channels.RGB);
	
		if (Layer.temperatureInfo.wasFrozen) {
			Freeze();
		}
	}
	
	void Freeze() {
		Layer.Frozen = true;
		freezeCounter = freezeDuration;
		
		Layer.temperatureInfo.wasFrozen = false;
	}
}
