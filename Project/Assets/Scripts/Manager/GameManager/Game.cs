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
	
	public LevelCycleMenager levelCycle;
	
	public GameObject GuiPrefab;
	[Disable] public GameObject guiGameObject;
	//[Disable] public 
	
	public override void OnAwake(){
		base.OnAwake();
		if(instance == null){
			levelCycle = GetComponent<LevelCycleMenager>();
			Game.instance = this;
		}
	}
	
	public MapData MapData{
		get{return levelCycle.currentMapData;}
	}
	
	[Button("Load Next Map", "nextMap")]
	public bool loadNextMapBtn;
	
	public void nextMap(){
		SwitchState<GameNextLevel>();
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
