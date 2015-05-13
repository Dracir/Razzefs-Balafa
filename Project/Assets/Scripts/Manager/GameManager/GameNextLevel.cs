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
	
	public LevelCycleMenager levelCycle;
	
	public override void OnAwake() {
		base.OnAwake();
		DontDestroyOnLoad(this);
		levelCycle = GetComponent<LevelCycleMenager>();
		levelCycle.loadMapPack();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		levelCycle.nextMap();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		if(levelCycle.levelLoaded){
			findPlayersPosition();
			makePlayers();
			makeCamera();
			SwitchState<GamePlaying>();
		}
	}

	void findPlayersPosition() {
		for (int i = 0; i < Layer.playerPositions.Length; i++) {
			GameObject playerPosition = levelCycle.currentMapGO.FindChildRecursive("P" + (i+1) +"Start");
			if(playerPosition){
				Layer.playerPositions[i] = playerPosition.transform.position;
			}else{
				Debug.LogError("Missing player "  + (i+1) + " starting location in map " + levelCycle.currentMapData.name);
			}
			
		}
	}
	void makePlayers() {
		for (int i = 0; i < Layer.playersPrefab.Length; i++) {
			GameObject playerPrefab = Layer.playersPrefab[i];
			if(playerPrefab != null && Layer.playersGameObject[i] == null){
				Layer.playersGameObject[i] = GameObjectExtend.createClone(playerPrefab);
				Layer.playersGameObject[i].transform.position = Layer.playerPositions[i];
			}
		}
	}

	void makeCamera() {
		CameraDudes follow = Camera.main.GetOrAddComponent<CameraDudes>();
		GameObject flag = levelCycle.currentMapGO.FindChildRecursive("EndFlag");
		follow.SetFollowing(new [] { flag, Layer.playersGameObject[0], Layer.playersGameObject[1], Layer.playersGameObject[2], Layer.playersGameObject[3] });
	}
}
