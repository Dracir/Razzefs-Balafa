using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class HarryCastIdle : State {
	
    HarryCast Layer {
    	get { return ((HarryCast)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
}
