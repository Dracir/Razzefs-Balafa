using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

 public class CharacterStatusDying : State {
	
    CharacterStatus Layer {
    	get { return ((CharacterStatus)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		Debug.Log("DIE !!!");
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
