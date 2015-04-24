using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobber : StateLayer {

    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public float movementSpeed = 5;
	public float activationTime = 1;
	
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
