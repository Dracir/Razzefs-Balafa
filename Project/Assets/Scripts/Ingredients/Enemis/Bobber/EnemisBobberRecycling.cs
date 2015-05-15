using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobberRecycling : State, Recyclable {
	
    EnemisBobber Layer {
    	get { return (EnemisBobber)layer; }
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

	#region Recyclable implementation

	public void recycle() {
		SwitchState<EnemisBobberRecycling>();
	}

	#endregion
}
