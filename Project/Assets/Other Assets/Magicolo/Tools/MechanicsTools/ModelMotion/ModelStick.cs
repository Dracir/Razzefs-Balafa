using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelStick : StateLayer {
	
	public Force2 Gravity {
		get {
			return Layer.Gravity;
		}
	}
	
	public bool Jumping {
		get {
			return Layer.Jumping;
		}
	}
	
	new public Rigidbody rigidbody {
		get {
			return Layer.rigidbody;
		}
	}
	
	ModelMotion Layer {
		get { return ((ModelMotion)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
}
