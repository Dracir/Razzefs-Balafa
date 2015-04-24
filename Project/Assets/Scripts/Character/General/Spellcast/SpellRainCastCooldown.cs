using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellRainCastCooldown : State {
	
	public float cooldown;
	[Disable] public float counter;
	
    SpellRainCast Layer {
    	get { return (SpellRainCast)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
		counter = cooldown;
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		counter -= Time.deltaTime;
		
		if (counter <= 0) {
			SwitchState<SpellRainCastIdle>();
		}
	}
}
