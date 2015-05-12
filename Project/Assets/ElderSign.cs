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
				//TODO: Use wizardz enum instead of character ID
				ElderSignManager.incrementElderSignCount((Wizardz)other.GetComponent<CharacterDetail>().Id);
			}

			//Increment total coin count
			//Handled by Elder Sign Manager

			//Disappear coin
			GameObject.Destroy(this.gameObject);

			Debug.Log (other.name + " picked up an elder sign!");
			Debug.Log (other.name + " has " + ElderSignManager.getElderSignCount((Wizardz)other.GetComponent<CharacterDetail>().Id) + " elder signs!");

		}
	}
}