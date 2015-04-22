using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GameNextLevel : State {
	
    Game Layer {
    	get { return ((Game)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	[Disable] GameObject player1;
	[Disable] GameObject player2;
	[Disable] GameObject player3;
	[Disable] GameObject player4;
	
	LevelCycleMenager levelCycle;
	
	public override void OnAwake() {
		base.OnAwake();
		levelCycle = GetComponent<LevelCycleMenager>();
		levelCycle.loadMapPack();
		DontDestroyOnLoad(this);
		checkPlayerExist();
	}

	void checkPlayerExist() {
		if(player1 == null){
			player1 = GameObjectExtend.createClone(Layer.player1Prefab);
		}
	}
	
	public override void OnEnter() {
		base.OnEnter();
		if(levelCycle.hasNextMap()){
			levelCycle.nextMap();
			setPlayerPosition();
			SwitchState<GamePlaying>();
		}
		
		
	}

	void setPlayerPosition() {
		if(player1 != null){
			player1.transform.position = levelCycle.currentMapGO.FindChildRecursive("P1Start").transform.position;
		}
		if(player2 != null){
			player2.transform.position = levelCycle.currentMapGO.FindChildRecursive("P2Start").transform.position;
		}
		if(player3 != null){
			player3.transform.position = levelCycle.currentMapGO.FindChildRecursive("P3Start").transform.position;
		}
		if(player4 != null){
			player4.transform.position = levelCycle.currentMapGO.FindChildRecursive("P4Start").transform.position;
		}
	}
	
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
