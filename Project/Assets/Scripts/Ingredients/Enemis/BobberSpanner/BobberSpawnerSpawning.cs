using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class BobberSpawnerSpawning : State {
	
	public Transform spawningPoint;
	
    BobberSpawner Layer {
    	get { return (BobberSpawner)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
		GameObject boober = Layer.bobberPool.Spawn();
		boober.transform.position = spawningPoint.position;
		boober.transform.localRotation = transform.parent.localRotation;
		SwitchState<BobberSpawnerReloading>();
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
