using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class BobberSpawnerSpawning : State {
	
    BobberSpawner Layer {
    	get { return (BobberSpawner)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.bobberPool.Spawn();
		SwitchState<BobberSpawnerReloading>();
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
