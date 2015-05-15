using UnityEngine;
using System.Collections;

public class GravityChanger : MonoBehaviour {

	float[] gravityForces = new float[4];
	
	void OnTriggerEnter2D(Collider2D collision){
		if(collision.tag == "Player"){
			CharacterMotion motion = collision.transform.parent.GetComponent<CharacterMotion>();
			if(motion != null){
				
				CharacterDetail detail = collision.transform.parent.GetComponent<CharacterDetail>();
				if(motion.gravity.Strength != 0){
					gravityForces[detail.Id] = motion.gravity.Strength;
				}
				
				motion.gravity.Strength = 0;
			}
		}
		
	}
	
	void OnTriggerExit2D(Collider2D collision){
		if(collision.tag == "Player"){
			CharacterMotion motion = collision.transform.parent.GetComponent<CharacterMotion>();
			if(motion != null){
				CharacterDetail detail = collision.transform.parent.GetComponent<CharacterDetail>();
				motion.gravity.Strength = gravityForces[detail.Id];
			}
		}
	}
}
