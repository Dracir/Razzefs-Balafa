using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GameLevelEndAnimation : State {
	
    Game Layer {
    	get { return (Game)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }

	public GameObject wisardEnterFXPrefab;
	
	public Vector3 endFlagLocation;
	[Disable] public bool[] gotToTheFlag = new bool[4];
	bool switchingLevel;
	
	public override void OnEnter() {
		base.OnEnter();
		switchingLevel = false;
		int index = 0;
		foreach (var player in Layer.playersGameObject) {
			if(player != null){
				gotToTheFlag[index] = false;
				player.GetComponent<CharacterEtheralMoving>().target = endFlagLocation;
				player.GetComponent<CharacterStatus>().SwitchState<CharacterEtheral>();
				player.GetComponent<CharacterEtheral>().SwitchState<CharacterEtheralTransforming>();
			}else{
				gotToTheFlag[index] = true;
			}
			index++;
		}
	}

	public void getToTheFlag(CharacterDetail detail){
		gotToTheFlag[detail.Id] = true;
		GameObjectExtend.createClone(wisardEnterFXPrefab, null, endFlagLocation);
		if( allGotThrough() && !switchingLevel){
			switchingLevel = true;
			StartCoroutine(switchTentot());
		}
	}

	IEnumerator switchTentot(){
		yield return new WaitForSeconds(2);
		SwitchState<GameNextLevel>();
	}
	bool allGotThrough(){
		foreach (var playerGotTought in gotToTheFlag) {
			if(!playerGotTought) return false;
		}	
		return true;
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		for (int i = 0; i < 4; i++) {
			if(!gotToTheFlag[i]){
				
			}
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
