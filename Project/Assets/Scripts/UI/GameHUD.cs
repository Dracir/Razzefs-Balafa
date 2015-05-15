using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameHUD : MonoBehaviour {
	List<TemperatureDisplay> tempDisplays;
	
	CharacterDetail[] players;
	
	void Start () {
		tempDisplays = new List<TemperatureDisplay>(GetComponentsInChildren<TemperatureDisplay>());
		players = FindObjectsOfType<CharacterDetail>();
		
		for (int i = tempDisplays.Count - 1; i >= 0; i--){
			bool playerExists = players.Length >= i + 1;
			Debug.Log("PLayer exists?" + playerExists);
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
	}
	
}
