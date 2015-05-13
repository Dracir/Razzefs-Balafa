using UnityEngine;
using System.Collections;

public class LaserTrigger : MonoBehaviour {

	public float deltaTemperaturePerS;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnTriggerStay2D(Collider2D other) {
		TemperatureInfo temperatureInfo = other.GetComponentInChildren<TemperatureInfo>();
		if(temperatureInfo != null){
			//Debug.Log(temperatureInfo.gameObject.name);
			temperatureInfo.Temperature += deltaTemperaturePerS * Time.deltaTime;
		}
    }
}
