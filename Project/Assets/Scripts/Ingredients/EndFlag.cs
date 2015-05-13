using UnityEngine;
using System.Collections;

public class EndFlag : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			Game.instance.SwitchState<GameNextLevel>();
			enabled = false;
		}
    }
}
