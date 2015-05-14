using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameHUD : MonoBehaviour {
	List<TemperatureDisplay> tempDisplays;
	
	CharacterDetail[] players;
	
	void Start () {
		tempDisplays = new List<TemperatureDisplay>(GetComponentsInChildren<TemperatureDisplay>());
		players = FindObjectsOfType<CharacterDetail>();
		
		for (int i = tempDisplays.Count; i > 0; i--){
			if (players[i] == null){
				tempDisplays[i].gameObject.SetActive(false);
				tempDisplays.Remove(tempDisplays[i]);
			}
		}
	}
	
	void Update () {
		
	}
	
	public void Refresh () {
		for (int i = 0; i < tempDisplays.Count; i ++){
			tempDisplays[i].ShowTemp(players[i].Temperature);
		}
	}
	
}
