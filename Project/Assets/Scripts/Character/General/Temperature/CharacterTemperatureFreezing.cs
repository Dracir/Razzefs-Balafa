using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterTemperatureFreezing : State {
	
	public float freezeDuration = 1;
	
	[Disable] public bool frozen;
	[Disable] public float freezeCounter;
	
	CharacterTemperature Layer {
		get { return ((CharacterTemperature)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Unfreeze();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (!Layer.temperatureInfo.IsFreezing) {
			SwitchState<CharacterTemperatureIdle>();
			return;
		}
		
		Layer.spriteRenderer.color = Layer.spriteRenderer.color.Lerp(new Color(1 - Layer.temperatureInfo.Coldness, 1 - Layer.temperatureInfo.Coldness, 1), Time.deltaTime * Layer.fadeSpeed, Channels.RGB);
	
		freezeCounter -= frozen ? Time.deltaTime : 0;
			
		if (freezeCounter <= 0) {
			Unfreeze();
		}
		
		if (Layer.temperatureInfo.wasFrozen) {
			Freeze();
		}
	}
	
	void Freeze() {
		if (!frozen) {
			Layer.Freeze();
		}
			
		frozen = true;
		freezeCounter = freezeDuration;
		
		Layer.temperatureInfo.wasFrozen = false;
	}
	
	void Unfreeze() {
		frozen = false;
		
		if (frozen) {
			Layer.Unfreeze();
		}
	}
}
