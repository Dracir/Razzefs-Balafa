using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterStateDying : State {
	
    CharacterState Layer {
    	get { return ((CharacterState)layer); }
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
