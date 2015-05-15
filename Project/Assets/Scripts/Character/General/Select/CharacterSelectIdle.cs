using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Magicolo;

public class CharacterSelectIdle : State {
	
	CharacterSelect Layer {
		get { return (CharacterSelect)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnAwake()
	{
		base.OnAwake();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		Layer.selectMenu.skipUpdate = true;
		Layer.box.SetActive(false);
		Layer.joinText.gameObject.SetActive(true);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.box.SetActive(true);
		Layer.joinText.gameObject.SetActive(false);
	}
}
