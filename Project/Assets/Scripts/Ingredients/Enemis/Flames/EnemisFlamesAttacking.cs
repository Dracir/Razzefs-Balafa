using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisFlamesAttacking : State {
	
	Animator animator;
	TemperatureInfo temperatureInfo;
	
    EnemisFlames Layer {
    	get { return (EnemisFlames)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnAwake() {
		base.OnAwake();
		animator = GetComponent<Animator>();
		temperatureInfo = GetComponent<TemperatureInfo>();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		animator.enabled = true;
	}
	
	public override void OnExit() {
		base.OnExit();
		animator.enabled = false;
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		if(temperatureInfo.IsFreezing){
			SwitchState<EnemisFlamesFrozen>();
		}
		
	}
	
	public override void TriggerStay2D(Collider2D collision) {
		base.TriggerStay2D(collision);
		
		TemperatureInfo otherTemperature = collision.GetComponentInChildren<TemperatureInfo>();
		if(otherTemperature != null){
			otherTemperature.Temperature += Time.deltaTime * Layer.HeatingPerSecond;
		}
	}
}
