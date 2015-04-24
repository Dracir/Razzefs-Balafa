using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellRainCastCooldown : State {
	
    SpellRainCast Layer {
    	get { return (SpellRainCast)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
}
