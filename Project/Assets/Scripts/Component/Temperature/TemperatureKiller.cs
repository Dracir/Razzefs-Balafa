using UnityEngine;
using System.Collections;
using Magicolo;

public class TemperatureKiller : MonoBehaviour {

	TemperatureInfo info;
	public bool removeIfBlazed;
	public bool removeIfFrozen;
	
	[Tooltip("Position to base fireDamage")]
	public Transform positionTransform; 
	
	void Awake () {
		info = GetComponent<TemperatureInfo>();
	}
	
	
	void Update () {
		if(positionTransform == null) return;
		
		if(removeIfBlazed && info.wasBlazed){
			info.wasBlazed = false;
			gameObject.Remove();
		}else if(removeIfFrozen && info.wasFrozen){
			info.wasFrozen = false;
			gameObject.Remove();
		}
	}
	
	public void fireDamage(Vector3 position, float heatDamage, float explosionRadius){
		if(positionTransform == null) return;
		
		float distance = (position - positionTransform.position).magnitude;
		if(distance <= explosionRadius){
			float t = 1 - (distance / explosionRadius);
			float damage = heatDamage * t.Pow(1.5f);
				
			info.Heat(damage);
		} 
	}
}

