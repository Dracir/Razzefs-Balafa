using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellRainCast : StateLayer {
	
    CharacterCast Layer {
    	get { return (CharacterCast)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
}
