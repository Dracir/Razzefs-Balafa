using UnityEngine;
using System.Collections;
using Magicolo;

public class EndFlagEtheral : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		CharacterMotion motion = other.FindComponent<CharacterMotion>();
		if(motion != null){
			CharacterDetail detail = other.FindComponent<CharacterDetail>();
			Game.instance.GetComponent<GameLevelEndAnimation>().getToTheFlag(detail);
		}
		
	}
}
