using UnityEngine;
using System.Collections;
using Magicolo;

public class TemperatureKiller : MonoBehaviour {

	TemperatureInfo info;
	public bool removeIfBlazed;
	public bool removeIfFrozen;
	
	void Start () {
		info = GetComponent<TemperatureInfo>();
	}
	
	
	void Update () {
		if(removeIfBlazed && info.wasBlazed){
			info.wasBlazed = false;
			gameObject.Remove();
		}else if(removeIfFrozen && info.wasFrozen){
			info.wasFrozen = false;
			gameObject.Remove();
		}
	}
}

