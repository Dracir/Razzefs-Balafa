using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisSofter : StateLayer {
	
	public float chargingTime;
	public LayerMask lazerLayerMask;
	public float deltaTemperatureChangePerSeconde;
	public float bounceDeltaTemperatureLost;
	
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
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
