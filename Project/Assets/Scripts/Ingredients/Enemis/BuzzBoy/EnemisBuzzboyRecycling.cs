using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBuzzboyRecycling : State, Recyclable {
	
    EnemisBuzzboy Layer {
    	get { return (EnemisBuzzboy)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		transform.parent.gameObject.Remove();
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}


	void Recyclable.recycle() {
		Layer.SwitchState<EnemisBuzzboyRecycling>();
	}

}
