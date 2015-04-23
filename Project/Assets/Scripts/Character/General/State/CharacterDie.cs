using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDie : StateLayer {
	
    CharacterStatus Layer {
    	get { return ((CharacterStatus)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
}
