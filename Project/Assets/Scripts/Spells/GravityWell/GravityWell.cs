using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GravityWell : MonoBehaviourExtended {

	bool _boxCollider2DCached;
	BoxCollider2D _boxCollider2D;
	public BoxCollider2D boxCollider2D { 
		get { 
			_boxCollider2D = _boxCollider2DCached ? _boxCollider2D : GetComponent<BoxCollider2D>();
			_boxCollider2DCached = true;
			return _boxCollider2D;
		}
	}
	
	bool _areaEffector2DCached;
	AreaEffector2D _areaEffector2D;
	public AreaEffector2D areaEffector2D { 
		get { 
			_areaEffector2D = _areaEffector2DCached ? _areaEffector2D : GetComponent<AreaEffector2D>();
			_areaEffector2DCached = true;
			return _areaEffector2D;
		}
	}
	
	bool _particleSystemCached;
	ParticleSystem _particleSystem;
	new public ParticleSystem particleSystem { 
		get { 
			_particleSystem = _particleSystemCached ? _particleSystem : GetComponentInChildren<ParticleSystem>();
			_particleSystemCached = true;
			return _particleSystem;
		}
	}
	
	public float Angle {
		set {
			transform.SetLocalEulerAngles(value, Axes.Z);
			areaEffector2D.forceDirection = value;
			particleSystem.startRotation = (-value + 90) * Mathf.Deg2Rad;
		}
	}
	
	public float Length {
		set {
			boxCollider2D.size = new Vector2(value, 1);
			boxCollider2D.offset = new Vector2(value / 2 - 0.5F, 0);
			particleSystem.startLifetime = value / 8;
		}
	}
	
	public void Explode() {
		gameObject.Remove();
	}
}

