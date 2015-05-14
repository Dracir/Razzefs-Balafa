using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class BobberSpawner : StateLayer {
	
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public float reloadTime;
	public Pool bobberPool;
	public TemperatureInfo temperature;
	
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
