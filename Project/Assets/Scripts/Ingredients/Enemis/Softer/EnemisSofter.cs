using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisSofter : StateLayer {
	
	public float chargingTime;
	public float maxLazerLenght; 
	public LayerMask lazerLayerMask;
	public float temperatureChangePerSeconde;
	
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
