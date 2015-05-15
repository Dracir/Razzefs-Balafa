using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterEtheralEntering : State {
	
    CharacterEtheral Layer {
    	get { return (CharacterEtheral)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public Vector3 targetRotation;
	Vector3 startingRotation;
	
	Vector3 startingScale;
	
	[Disable] public float t;
	public float timeToEnter;
	
	public override void OnEnter() {
		base.OnEnter();
		t = timeToEnter;
		startingScale = transform.localScale;
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		t -= Time.deltaTime;
		transform.rotation = Quaternion.Euler(Interpolation.smoothStep(targetRotation, Vector3.zero, t/timeToEnter));
		transform.localScale = Interpolation.smoothStep(Vector3.zero, startingScale, t/timeToEnter);
		if(t <= 0){
			SwitchState<CharacterEtheralStay>();
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
