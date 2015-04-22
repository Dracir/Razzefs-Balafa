using UnityEngine;
using System.Collections;
using Magicolo;
using RickTools.MapLoader;


// TODO SET PLAYERS POSITIONS

[System.Serializable]
public class LevelCycleMenager : MonoBehaviour {

	public string playerPrefMapIndexKey;
	
	
	public string mapPrefabFolder;
	public string endOfCycleMapName;
	public string inGameMapName;
	
	
	[Disable] public MapData[] currentMapPack;
	[Disable] public int currentMapIndex = -1;
	
	[Disable] public GameObject currentMapGO;
	[Disable] public MapData currentMapData;
	
	public void loadMap(){
		currentMapPack = Resources.LoadAll<MapData>(mapPrefabFolder);
	}
	
	
	/*
	  This is made to be sure that the level is loaded before doing stuff.
	  Used when your not in the right Level
	  Because the loadLevel is not a blocking method :(.
	  
	 **/
	bool nextLevelOnLevelWasLoaded = false;
    void OnLevelWasLoaded(int iLevel){
		if(nextLevelOnLevelWasLoaded){
			nextMap();
			nextLevelOnLevelWasLoaded = false;
		}
    }
	
	[Button("Load Next Map", "nextMap")]
	public bool loadNextMapBtn;
	
	public void nextMap(){
		if(Application.loadedLevelName != inGameMapName){
			Application.LoadLevel(inGameMapName);
		}else{
			loadNextMap();
		}
	}

	void loadNextMap() {
		if(currentMapPack.Length == 0) {
			endMapPack();
		}
		
		currentMapIndex++;
		
		setCurrentMapIndexInPlayerPref();
		
		if(currentMapIndex == currentMapPack.Length){
			endMapPack();
		}else{
			loadMap(currentMapPack[currentMapIndex]);
		}
		
	}

	void setCurrentMapIndexInPlayerPref() {
		if( !string.IsNullOrEmpty(playerPrefMapIndexKey) ){
			int maxLevel = PlayerPrefs.GetInt(playerPrefMapIndexKey,1);
			if(maxLevel < currentMapIndex){
				PlayerPrefs.SetInt(playerPrefMapIndexKey,currentMapIndex);
			}
		}
		
	}
	
	
	void endMapPack() {
		Application.LoadLevel(endOfCycleMapName);
	}

	
	void loadMap(MapData mapData) {
		if(currentMapGO != null){
			currentMapGO.Remove();
			currentMapData = null;
		}
		
		currentMapGO = GameObjectExtend.createClone(mapData.gameObject);
		currentMapData = currentMapGO.GetComponent<MapData>();
		
	}
	
}
