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
	
	public LevelCycleMenager levelCycle;
	
	public override void OnAwake() {
		base.OnAwake();
		DontDestroyOnLoad(this);
		levelCycle = GetComponent<LevelCycleMenager>();
		levelCycle.loadMapPack();
		
		levelCycle.nextMap();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		if(levelCycle.levelLoaded){
			makePlayers();
			makeCamera();
			SwitchState<GamePlaying>();
		}
	}
	
	void makePlayers() {
		//TODO select nb player comme faut
		if(player1 == null){
			player1 = GameObjectExtend.createClone(Layer.player1Prefab);
			player1.transform.position = levelCycle.currentMapGO.FindChildRecursive("P1Start").transform.position;
		}
	}

	void makeCamera() {
		CameraDudes follow = Camera.main.GetOrAddComponent<CameraDudes>();
		GameObject flag = levelCycle.currentMapGO.FindChildRecursive("EndFlag");
		follow.SetFollowing(new [] { flag, player1, player2, player3, player4 });
	}
}
