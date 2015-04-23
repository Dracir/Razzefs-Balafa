using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterLiveIdle : State {
	
    CharacterLive Layer {
    	get { return ((CharacterLive)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
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
