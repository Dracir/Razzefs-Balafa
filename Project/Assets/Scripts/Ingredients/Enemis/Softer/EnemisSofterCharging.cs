using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisSofterCharging : State {
	
	[Disable] public float t;
	public ParticleSystem chargingAnimation;
	
    EnemisSofter Layer {
    	get { return (EnemisSofter)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		chargingAnimation.Play();
		t = Layer.chargingTime;
	}
	
	public override void OnExit() {
		base.OnExit();
		chargingAnimation.Stop();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		t-= Time.deltaTime;
		if(t <= 0){
			SwitchState<EnemisSofterFiring>();
		}
	}
}
