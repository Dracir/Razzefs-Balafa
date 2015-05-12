using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisSofterCharging : State {
	
    EnemisSofter Layer {
    	get { return (EnemisSofter)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
