using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterSelectIdle : State {
	
	CharacterSelect Layer {
		get { return (CharacterSelect)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}

	public override void OnEnter() {
		base.OnEnter();
		
		Layer.wizardSelect.skipUpdate = true;
		Layer.box.SetActive(false);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.box.SetActive(true);
	}
}
