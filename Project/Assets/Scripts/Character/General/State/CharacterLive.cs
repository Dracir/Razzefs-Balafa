using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using RickTools.MapLoader;

public class CharacterLive : StateLayer {
	
    CharacterStatus Layer {
    	get { return ((CharacterStatus)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	MapData mapData;
	
	public override void OnEnter() {
		base.OnEnter();
		mapData = Game.instance.MapData;
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		dieIfOutSideMap();
		
	}

	void dieIfOutSideMap(){
		if(mapData != null && !mapData.isWithinMap(transform.position)){
			Layer.Die();
		}
	}
}
