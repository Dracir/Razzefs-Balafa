using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterStatus : StateLayer {
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public void Die() {
//		SwitchState<CharacterDie>();
	}
}
