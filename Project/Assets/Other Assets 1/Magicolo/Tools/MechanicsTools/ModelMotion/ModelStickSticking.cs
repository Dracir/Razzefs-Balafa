using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelStickSticking : State {
	
	[Layer] public int stickyLayer;
	public Collider[] stickyTriggers;
	[Min] public float cooldown = 0.5F;
	
	[Disable] public int colliderCounter;
	[Disable] public float cooldownCounter;
	[Disable] public Vector3 initialGravity;
	[Disable] public Collider currentCollider;
	[Disable] public Collider lastCollider;
    
	ModelStick Layer {
		get { return ((ModelStick)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnAwake() {
		base.OnAwake();
	
		initialGravity = Layer.Gravity;
		
		foreach (Collider stickyTrigger in stickyTriggers) {
			stickyTrigger.gameObject.layer = stickyLayer;
		}
	}

	public override void OnEnter() {
		base.OnEnter();
		
		foreach (Collider stickyTrigger in stickyTriggers) {
			stickyTrigger.enabled = true;
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
		foreach (Collider stickyTrigger in stickyTriggers) {
			stickyTrigger.enabled = false;
		}
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		cooldownCounter -= cooldownCounter <= 0 ? 0 : Time.deltaTime;
		
		if (currentCollider == null) {
			if (cooldownCounter <= 0 && Layer.Gravity.Direction != (Vector2)initialGravity) {
				Layer.Gravity.Direction = initialGravity;
			}
		}
		else if (Layer.Jumping) {
			currentCollider = null;
			cooldownCounter = cooldown;
			Layer.Gravity.Direction = initialGravity;
		}
		else if (currentCollider == lastCollider) {
			Stick();
		}
		else if (lastCollider == null) {
			Stick();
			cooldownCounter = cooldown;
		}
		else if (currentCollider != lastCollider && cooldownCounter <= 0) {
			Stick();
			cooldownCounter = cooldown;
		}
		
		lastCollider = currentCollider;
	}
	
	public override void TriggerEnter(Collider collision) {
		base.TriggerEnter(collision);
		
		if (stickyLayer == collision.gameObject.layer) {
			colliderCounter += 1;
			
			currentCollider = collision.transform.parent.GetComponent<Collider>();
		}
	}
	
	public override void TriggerExit(Collider collision) {
		base.TriggerExit(collision);
		
		if (stickyLayer == collision.gameObject.layer) {
			colliderCounter -= 1;
			
			if (colliderCounter <= 0) {
				currentCollider = null;
			}
		}
	}
	
	void Stick() {
		Vector3 adjustedPosition = transform.position + new Vector3(0, 0.3F, 0).Rotate(Layer.Gravity.Angle - 90);
			
		Layer.Gravity.Direction = (currentCollider.ClosestPointOnBounds(adjustedPosition) - adjustedPosition);
	}
}
