using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisFlamesFrozen : State {
	
	TemperatureInfo temperatureInfo;
	
    EnemisFlames Layer {
    	get { return (EnemisFlames)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnAwake() {
		base.OnAwake();
		temperatureInfo = GetComponent<TemperatureInfo>();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if(!temperatureInfo.IsFreezing){
			SwitchState<EnemisFlamesAttacking>();
		}
	}
}
