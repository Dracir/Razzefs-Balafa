using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class BobberSpawnerReloading : State {
	
    BobberSpawner Layer {
    	get { return (BobberSpawner)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public float cooldown;
	
	public override void OnEnter() {
		base.OnEnter();
		cooldown = Layer.reloadTime;
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		cooldown-= Time.deltaTime;
		if(cooldown <= 0){
			SwitchState<BobberSpawnerSpawning>();
		}
		
		if(Layer.temperature.IsFreezing){
			SwitchState<BobberSpawnerFrozen>();
		}
	}
}
