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
	
	public LayerMask rotatingLayerToInteractWithGenre;

	public override void OnUpdate() {
		base.OnUpdate();
		
		if (!Layer.stationnary) {
			transform.parent.position += transform.parent.right * Time.deltaTime * Layer.movementSpeed;
		}
		
	}
	
	public override void TriggerEnter2D(Collider2D collision){
		base.TriggerEnter2D(collision);
		CharacterStatus status = collision.gameObject.GetComponent<CharacterStatus>();
		if (status != null) {
			status.Die();			
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		if (rotatingLayerToInteractWithGenre.Contains(collision.gameObject.layer)) {
			transform.parent.Rotate(0, 0, 180);
		}
	}
	
}
