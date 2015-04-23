using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Temperature : MonoBehaviourExtended {

	[Range(-1, 1)] public float temperature;
	[Range(-1, 0)] public float freezingThreshold = -0.75F;
	[Range(0, 1)] public float blazingThreshold = 0.75F;
	[Min] public float resistance = 1;
	[Disable] public bool wasFrozen;
	[Disable] public bool wasBlazed;
	
	public bool IsCool {
		get {
			return temperature < 0 && temperature > freezingThreshold;
		}
	}
	
	public bool IsWarm {
		get {
			return temperature > 0 && temperature < blazingThreshold;
		}
	}
	
	public bool IsFreezing {
		get {
			return temperature <= freezingThreshold;
		}
	}
	
	public bool IsBlazing {
		get {
			return temperature >= blazingThreshold;
		}
	}
	
	public float AmbientTemperature {
		get {
			return 0;
		}
	}
	
	void Update() {
		wasFrozen |= temperature <= -1;
		wasBlazed |= temperature >= 1;
			
		float difference = AmbientTemperature - temperature;
		float increment = difference.Sign() * Time.deltaTime / resistance;
		
		if (Mathf.Abs(difference) > Mathf.Abs(increment)) {
			temperature += increment;
		}
		else {
			temperature = AmbientTemperature;
		}
	}
}

