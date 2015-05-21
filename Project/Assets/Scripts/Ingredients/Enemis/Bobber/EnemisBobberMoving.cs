using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Rick;

public class EnemisBobberMoving : State {
	
	AudioSourceItem movingSource;
	
	EnemisBobber Layer {
		get { return (EnemisBobber)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		movingSource = Layer.audioPlayer.Play("Moving");
	}
	
	public override void OnExit() {
		base.OnExit();
		
		movingSource.Stop();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.temperature.wasFrozen) {
			Layer.temperature.wasFrozen = false;
			SwitchState<EnemisBobberFrozen>();
		}
		else if (Layer.temperature.IsBlazing) {
			SwitchState<EnemisBobberExplosing>();
		}
		else if (Layer.temperature.IsFreezing) {
			float movementSpeedMod = Interpolation.smoothStep(0, 1, Layer.temperature.Temperature.scale(-1, 1, 0, 1));
			transform.parent.position += Layer.movementSpeed * movementSpeedMod * Time.deltaTime * transform.parent.right;
		}
		else {
			transform.parent.position += Layer.movementSpeed * Time.deltaTime * transform.parent.right;
		}
		
		
	}
	
	public override void TriggerEnter2D(Collider2D collision) {
		base.TriggerEnter2D(collision);
	
		if (collision.tag == "Player") {
			SwitchState<EnemisBobberActivating>();
		}
		
	}
	
	public override void CollisionEnter2D(Collision2D collision) {
		SwitchState<EnemisBobberActivating>();
	}
	
	
}
