using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterEtheralIdle : State {
	
    CharacterEtheral Layer {
    	get { return (CharacterEtheral)layer; }
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
}
