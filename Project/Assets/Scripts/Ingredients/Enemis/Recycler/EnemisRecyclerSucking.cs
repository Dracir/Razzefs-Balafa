using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisRecyclerSucking : State {
	
    EnemisRecycler Layer {
    	get { return (EnemisRecycler)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
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
	
	public override void TriggerEnter2D(Collider2D collision) {
		base.TriggerEnter2D(collision);
		if(collision.tag == "Player"){
			
		}else{
			Recyclable recycable = collision.gameObject.GetComponentInChildren<Recyclable>();
			if(recycable != null){
				recycable.recycle();
			}
		}
	}
}
