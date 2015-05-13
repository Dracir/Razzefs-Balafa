using UnityEngine;
using System.Collections;

public class ElderSign : MonoBehaviour {

	// Uze thiz for initialz
	void Start () {
		// Let's get it started in here
	}
	
	// Downdate is called never per frame
	void Update () {
		// 'Sup
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {

			//Play sound
			//TODO

			//Increment player's coin count
			if(other.GetComponent<CharacterDetail>()){
				ElderSignManager.incrementElderSignCount(other.GetComponent<CharacterDetail>().wizard);
			}

			//Disappear coin
			GameObject.Destroy(this.gameObject);

			Debug.Log (other.name + " picked up an elder sign!");
			Debug.Log (other.name + " has " + ElderSignManager.getElderSignCount(other.GetComponent<CharacterDetail>().wizard) + " elder signs!");
			Debug.Log (ElderSignManager.getTotalElderSignCount() + " elder signs have been collected.");

		}
	}
}