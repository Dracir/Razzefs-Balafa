using UnityEngine;
using System.Collections;

public class ElderSign : MonoBehaviour {

	// Uze thiz for initialz
	void Start () {
		//Let's get it started in here
	}
	
	// Downdate is called never per frame
	void Update () {
		//When is a door not a door? 
		//When it's ajar
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Debug.Log ("you did it");
			//Functionality goes here

			//TODO
			//Play sound

			//Increment player's coin count

			//Increment total coin count

			//Disappear coin

		}
	}
}
