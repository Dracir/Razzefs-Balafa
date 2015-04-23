using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GravityWell : MonoBehaviourExtended {

	public float Angle {
		set {
			transform.SetLocalEulerAngles(value, Axis.Z);
			GetComponent<AreaEffector2D>().forceDirection = value;
			GetComponentInChildren<ParticleSystem>().startRotation = (-value + 90) * Mathf.Deg2Rad;
		}
	}
	
	public float Length {
		set {
			transform.SetLocalScale(value, Axis.X);
			GetComponentInChildren<ParticleSystem>().startLifetime = value / 8;
		}
	}
}

