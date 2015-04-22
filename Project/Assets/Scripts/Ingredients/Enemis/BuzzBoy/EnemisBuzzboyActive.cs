using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBuzzboyActive : State {
	
    EnemisBuzzboy Layer {
    	get { return ((EnemisBuzzboy)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
	
	public override void TriggerEnter2D(Collider2D collision){
		if(collision.tag == "Player"){
			collision.gameObject.GetComponent<StateMachine>().GetLayer<CharacterStatus>().SwitchState<CharacterStatusDying>();
			if(!Layer.stationnary){
				SwitchState<EnemisBuzzboyHitting>();
			}
		}
	}
	
}
