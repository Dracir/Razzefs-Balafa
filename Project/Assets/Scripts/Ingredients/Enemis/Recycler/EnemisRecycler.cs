using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisRecycler : StateLayer {
	
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	
	public AreaEffector2D succerAreaEffector;
	public BoxCollider2D succerCollider;
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
