using UnityEngine;
using System.Collections;
using RickTools.MapLoader;
public class DynamicBackground : MonoBehaviour {

	MapData mapData;
	
	public void setMapData(MapData mapData){
		this.mapData = mapData;
		refresh();
	}

	
	void refresh(){
		float x = Random.Range(0, mapData.width);
		float y = Random.Range(0, mapData.height);
		transform.position = new Vector3(x,y,0);
	}
	
	
}
