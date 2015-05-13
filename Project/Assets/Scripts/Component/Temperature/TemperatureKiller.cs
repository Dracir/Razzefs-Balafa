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
		if(removeIfBlazed && info.IsBlazing){
			gameObject.Remove();
		}else if(removeIfFrozen && info.IsFreezing){
			gameObject.Remove();
		}
	}
}

