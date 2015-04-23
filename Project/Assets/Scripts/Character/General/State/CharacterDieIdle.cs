using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDieIdle : State {
	
    CharacterDie Layer {
    	get { return ((CharacterDie)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
}
