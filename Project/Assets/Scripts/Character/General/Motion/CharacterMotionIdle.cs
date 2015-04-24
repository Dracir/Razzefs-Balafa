using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterMotionIdle : State {
	
    CharacterMotion Layer {
    	get { return ((CharacterMotion)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
}
