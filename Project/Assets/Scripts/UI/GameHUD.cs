using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameHUD : MonoBehaviour {
	List<TemperatureDisplay> tempDisplays;
	public RectTransform[] profiles = new RectTransform[4];
	
	CharacterDetail[] players;
	
	void Start () {
		tempDisplays = new List<TemperatureDisplay>(GetComponentsInChildren<TemperatureDisplay>());
		
		
		players = FindObjectsOfType<CharacterDetail>();
		
		for (int i = tempDisplays.Count - 1; i >= 0; i--){
			bool playerExists = players.Length >= i + 1;
			//Debug.Log("PLayer exists?" + playerExists);
			if (!playerExists){
				//tempDisplays[i].gameObject.SetActive(false);
				Destroy(tempDisplays[i].gameObject);
				tempDisplays.Remove(tempDisplays[i]);
				
				
				//TODO replace the portrait with instruction: "Press [cyclebutton] to change to [picture of spell]" kind of thing
			}
		}
		
		
	}
	
	void Update () {
		
	}
	
	public void Refresh () {
		if (players == null)
			return;
		for (int i = 0; i < players.Length; i ++){
			tempDisplays[i].ShowTemp(players[i].Temperature);
		}
		
		//TODO put the hud in the corners
		float xOffset = (float)Screen.width / 2f;
		float yOffset = (float)Screen.height / 2f;
		
		for (int i = 0; i < profiles.Length; i++){
			int xMultiplier = i % 2 == 0? 1 : -1;
			int yMultiplier = i < 2? -1 : 1;
			float x = (i % 2 == 0? 0 : Screen.width) + profiles[i].rect.width / 2f * xMultiplier;
			float y = (i < 2? Screen.height : 0) + profiles[i].rect.height / 2f * yMultiplier;
			
//			float x = xOffset * xMultiplier + (profiles[i].rect.width / 2f * xMultiplier) + (float)Screen.width / 2f;
//			float y = yOffset * yMultiplier + (profiles[i].rect.height / 2f * yMultiplier) + (float)Screen.height / 2f;
			profiles[i].position = new Vector3(x, y, 0);
		}
	}
	
}
