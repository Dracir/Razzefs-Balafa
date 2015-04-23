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

	public override void OnUpdate() {
		base.OnUpdate();
		
		if (!Layer.stationnary) {
			transform.parent.position += transform.parent.right * Time.deltaTime * Layer.movementSpeed;
		}
		
	}
	
	public override void TriggerEnter2D(Collider2D collision) {
		base.TriggerEnter2D(collision);
		
		CharacterStatus status = collision.gameObject.GetComponent<CharacterStatus>();
		
		if (status != null) {
			status.Die();
			
		}else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
			transform.parent.Rotate(0, 0, 180);
		}
	}
	
}
