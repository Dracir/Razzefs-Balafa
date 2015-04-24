using UnityEngine;
using System.Collections;

public class DieZone : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		CharacterStatus status = other.gameObject.GetComponent<CharacterStatus>();
		
		if(status != null){
			status.Die();
		}
    }
	
	void OnTriggerEnter2D(Collider2D other) {
		CharacterStatus status = other.GetComponent<CharacterStatus>();
		
		if(status != null){
			status.Die();
		}
    }
}
