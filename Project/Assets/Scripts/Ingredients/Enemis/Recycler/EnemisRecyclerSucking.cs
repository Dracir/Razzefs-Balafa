using UnityEditor;
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
	
	
	public override void OnAwake(){
		base.OnAwake();
	
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		//Layer.succerPointEffector.forceDirection = 180 + transform.parent.rotation.z;
	}

	public override void TriggerEnter2D(Collider2D collision) {
		base.TriggerEnter2D(collision);
		Recyclable recycable = collision.gameObject.GetComponentInChildren<Recyclable>();
		if(recycable != null){
			//Debug.Log(collision.transform.gameObject.layer);
			recycable.recycle();
		}
	}
}
