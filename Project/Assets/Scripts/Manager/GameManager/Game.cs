using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using RickTools.MapLoader;

public class Game : StateLayer {
	
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	public GameObject[] playersPrefab = new GameObject[4];
	public GameObject[] playersGameObject = new GameObject[4];
	
	public Vector3[] playerPositions = new Vector3[4];
	
	public static Game instance;
	
	public override void OnAwake(){
		base.OnAwake();
		
		Game.instance = this;
	}
	
	public MapData MapData{
		get{return GetComponent<GameNextLevel>().levelCycle.currentMapData;}
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
