using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellGravityCastCooldown : State {
	
	public float cooldown;
	[Disable] public float counter;
	
	SpellGravityCast Layer {
		get { return (SpellGravityCast)layer; }
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
			SwitchState<SpellGravityCastIdle>();
		}
	}
}
