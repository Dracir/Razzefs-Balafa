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
	
	public override void TriggerEnter2D(Collider2D collision){
		base.TriggerEnter2D(collision);
		Raindrop rd = collision.gameObject.GetComponent<Raindrop>();
		//Debug.Log("Je ne sais pas ce qui es passe.");
		if(rd != null){
			rd.hitWith(gameObject);
		}
	}
	
	public override void TriggerStay2D(Collider2D collision) {
		base.TriggerStay2D(collision);
		TemperatureInfo otherTemperature = collision.GetComponentInChildren<TemperatureInfo>();
		if(otherTemperature != null){
			otherTemperature.Heat(Time.deltaTime * Layer.HeatingPerSecond);
		}
	}
}
