using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisFlamesFrozen : State {
	
	TemperatureInfo temperatureInfo;
	public GameObject[] childFire;
	
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
		foreach (var child in childFire) {
			child.SetActive(false);
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		foreach (var child in childFire) {
			child.SetActive(true);
		}
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if(!temperatureInfo.IsFreezing){
			SwitchState<EnemisFlamesAttacking>();
		}
	}
}
