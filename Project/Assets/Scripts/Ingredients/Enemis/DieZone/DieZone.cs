using UnityEngine;
using System.Collections;

public class DieZone : MonoBehaviour {

	
	void OnTriggerEnter2D(Collider2D other) {
		CharacterStatus status = other.GetComponent<CharacterStatus>();
		
		if(status != null){
			status.Die();
		}
    }
}
