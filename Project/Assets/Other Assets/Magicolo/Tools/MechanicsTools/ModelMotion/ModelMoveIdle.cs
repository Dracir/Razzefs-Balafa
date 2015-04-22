using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelMoveIdle : State {
	
    ModelMove Layer {
    	get { return ((ModelMove)layer); }
    }
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.AbsHorizontalAxis > Layer.moveThreshold) {
			SwitchState("Moving");
			return;
		}
	}
}
