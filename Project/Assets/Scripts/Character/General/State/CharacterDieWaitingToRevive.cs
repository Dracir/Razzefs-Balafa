using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDieWaitingToRevive : State {
	
    CharacterDie Layer {
    	get { return (CharacterDie)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	
	
	public override void OnEnter() {
		base.OnEnter();
		SwitchState<CharacterDieEnteringPortal>();
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
