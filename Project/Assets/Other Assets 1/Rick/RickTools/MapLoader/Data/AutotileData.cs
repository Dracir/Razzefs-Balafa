using UnityEngine;
using System.Collections;

[System.Serializable]
public class AutotileData{

	public string name = "";
	
	//Source Srite
	public Sprite center;
	public Sprite side;
	public Sprite cornerInside;
	public Sprite cornerOutside;
	
	/* Prefab copie */
	public GameObject basePrefab;
	public string outputAssetFolder;
	
	/* Make Tiled autotile */
	public string autoTileFilePath;
	public string tilesFileName;
}
