using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelStickIdle : State {
	
	ModelStick Layer {
		get { return ((ModelStick)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
}
