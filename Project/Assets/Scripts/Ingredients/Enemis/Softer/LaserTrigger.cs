using UnityEngine;
using System.Collections;

public class LaserTrigger : MonoBehaviour {

	public EnemisSofter softer;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnTriggerStay2D(Collider2D other) {
		TemperatureInfo temperatureInfo = other.GetComponentInChildren<TemperatureInfo>();
		if(temperatureInfo != null){
			temperatureInfo.Temperature += softer.temperatureChangePerSeconde * Time.deltaTime;
		}else{
		}
    }
}
