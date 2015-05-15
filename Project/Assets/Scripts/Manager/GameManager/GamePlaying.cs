using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GamePlaying : State {
	
	
    Game Layer {
    	get { return ((Game)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	GameHUD gameHud;
	
	public override void OnEnter() {
		base.OnEnter();
		gameHud = Layer.guiGameObject.GetComponent<GameHUD>();
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		gameHud.Refresh();
	}
}
