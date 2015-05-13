using UnityEngine;
using System.Collections;
using Magicolo;

public class EndFlag : MonoBehaviour {

	bool _collider2DCached;
	Collider2D _collider2D;
	new public Collider2D collider2D { 
		get { 
			_collider2D = _collider2DCached ? _collider2D : GetComponent<Collider2D>();
			_collider2DCached = true;
			return _collider2D;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Game.instance.SwitchState<GameNextLevel>();
			collider2D.Remove();
		}
	}
}
