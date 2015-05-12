using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterStatus : StateLayer {
	
	public bool invincible;
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public void Die() {
		if (!invincible) {
			SwitchState<CharacterDie>();
		}
	}
}
