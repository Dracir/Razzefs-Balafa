using UnityEngine;
using System.Collections;

public class HideSpriteOnAwake : MonoBehaviour {

	
	void Awake () {
		if(Application.isPlaying){
			GetComponentInChildren<SpriteRenderer>().enabled = false;
		}
	}
	
}
