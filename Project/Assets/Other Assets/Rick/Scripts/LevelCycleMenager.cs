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
	public int currentMapIndex = -1;
	
	[Disable] public GameObject currentMapGO;
	[Disable] public MapData currentMapData;
	
	public static LevelCycleMenager instance;
	void Awake(){
		LevelCycleMenager.instance = this;
		DontDestroyOnLoad(this);
	}
	
	[Button("Load Map Pack", "loadMapPack")]
	public bool loadMapPackBtn;
	
	public void loadMapPack(){
		currentMapPack = Resources.LoadAll<MapData>(mapPrefabFolder);
	}
	
	
	
	/*
	  This is made to be sure that the level is loaded before doing stuff.
	  Used when your not in the right Level
	  Because the loadLevel is not a blocking method :(.
	  
	 **/
	bool onLevelWasLoaded_loadNextMap;
    void OnLevelWasLoaded(int iLevel){
		if(onLevelWasLoaded_loadNextMap){
			nextMap();
			onLevelWasLoaded_loadNextMap = false;
		}
    }
	
	
	
	[Button("Load Next Map", "nextMap")]
	public bool loadNextMapBtn;
	
	public void nextMap(){
		if(Application.loadedLevelName != inGameMapName){
			onLevelWasLoaded_loadNextMap = true;
			Application.LoadLevel(inGameMapName);
		}else{
			loadNextMap();
		}
	}

	void loadNextMap() {
		if(currentMapPack.Length == 0) {
			endMapPack();
			
		}else{
			currentMapIndex++;
			
			setCurrentMapIndexInPlayerPref();
			
			if(currentMapIndex == currentMapPack.Length){
				endMapPack();
			}else{
				loadMap(currentMapPack[currentMapIndex]);
			}
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
		clearCurrentMap();
		Application.LoadLevel(endOfCycleMapName);
	}

	
	void loadMap(MapData mapData) {
		if(currentMapGO != null){
			clearCurrentMap();
		}
		
		currentMapGO = GameObjectExtend.createClone(mapData.gameObject);
		currentMapData = currentMapGO.GetComponent<MapData>();
		
	}
	
	void clearCurrentMap() {
		currentMapGO.Remove();
		currentMapData = null;
	}
}
