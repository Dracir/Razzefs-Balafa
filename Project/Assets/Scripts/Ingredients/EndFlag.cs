using UnityEngine;
using System.Collections;
using Magicolo;

public class EndFlag : MonoBehaviour {
	
	Collider2D collider;
	bool checkingForAlive = false;
	bool endAnimnationThing = false;
	
	void Awake(){
		collider = GetComponent<Collider2D>();
		collider.enabled = true;
	}
	
	void OnTriggerStay2D(Collider2D other) {
		CharacterMotion motion = other.GetComponent<CharacterMotion>();
		if(motion != null && motion.Grounded && !checkingForAlive){
			checkingForAlive = true;
			CharacterStatus status = other.GetComponent<CharacterStatus>();
			StartCoroutine(NextLevelIfStillAlive(status));
		
		}else if(endAnimnationThing){
			CharacterDetail detail = other.GetComponent<CharacterDetail>();
			Game.instance.GetComponent<GameLevelEndAnimation>().getToTheFlag(detail);
		}
	}
	
	IEnumerator NextLevelIfStillAlive(CharacterStatus status){
		yield return new WaitForSeconds(1);
		if(status.isAlive()){
			Game.instance.GetComponent<GameLevelEndAnimation>().endFlagLocation = transform.position;
			Game.instance.SwitchState<GameLevelEndAnimation>();
			endAnimnationThing = true;
		}else{
			checkingForAlive = false;
		}
	}
}
