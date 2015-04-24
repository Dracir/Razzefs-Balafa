using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterCastIdle : State {
	
    CharacterCast Layer {
    	get { return (CharacterCast)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
}
