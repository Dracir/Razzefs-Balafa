using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterEtheralMoving : State {
	
    CharacterEtheral Layer {
    	get { return (CharacterEtheral)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	[Disable] public Vector3 target;
	Vector3 startingPosition;
	
	[Disable] public float t;
	[Disable] public float timeToTravel;
	public float speed;
	
	public override void OnEnter() {
		base.OnEnter();
		timeToTravel = (target - transform.position).magnitude / speed;
		startingPosition = transform.position;
		t = timeToTravel;
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		t -= Time.deltaTime;
		transform.position = Interpolation.smoothStep(target, startingPosition, t/timeToTravel);
		if(t <= 0){
			SwitchState<CharacterEtheralEntering>();
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
