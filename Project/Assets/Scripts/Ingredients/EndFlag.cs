using UnityEngine;
using System.Collections;
using Magicolo;

public class EndFlag : MonoBehaviour {
	
	Collider2D collider;
	
	void Awake(){
		collider = GetComponent<Collider2D>();
		collider.enabled = true;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Game.instance.SwitchState<GameNextLevel>();
			collider.enabled = false;
		}
	}
}
