using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterEtheral : StateLayer {
	
    public CharacterStatus Layer {
    	get { return (CharacterStatus)layer; }
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
