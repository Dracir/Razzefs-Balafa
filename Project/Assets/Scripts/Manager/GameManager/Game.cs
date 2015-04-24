using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Game : StateLayer {
	
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	public GameObject player1Prefab;
	public GameObject player2Prefab;
	public GameObject player3Prefab;
	public GameObject player4Prefab;
	
	public static Game instance;
	
	public override void OnAwake(){
		base.OnAwake();
		
		Game.instance = this;
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
