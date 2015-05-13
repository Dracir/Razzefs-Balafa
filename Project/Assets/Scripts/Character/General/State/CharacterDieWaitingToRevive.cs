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
		Layer.Layer.spriteRenderer.color = new Color(1,1,1,1f);
		Layer.Layer.rigidBody.isKinematic = false;
		Layer.Layer.setColliders(true);
		SwitchState<CharacterDieIdle>();
		Layer.Layer.SwitchState<CharacterLive>();
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
