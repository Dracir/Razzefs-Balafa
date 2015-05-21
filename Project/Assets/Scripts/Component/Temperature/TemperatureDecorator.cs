using UnityEngine;
using System.Collections;
using Magicolo;

public class TemperatureDecorator : MonoBehaviour {

	public SpriteRenderer[] spriteRenderers;
	TemperatureInfo temperatureInfo;
	
	public float fadeSpeed = 3;
	
	// Use this for initialization
	void Start () {
		temperatureInfo = GetComponent<TemperatureInfo>();
	}
	
	// Update is called once per frame
	void Update () {
	
		Color targetColor;
		
		if(temperatureInfo.IsWarm){
			targetColor = temperatureInfo.IsBlazing ? Color.red :  new Color(1, 1 - temperatureInfo.Hotness, 1 - temperatureInfo.Coldness);
		}else{
			targetColor = temperatureInfo.IsFreezing ? Color.blue : new Color(1 - temperatureInfo.Coldness, 1 - temperatureInfo.Coldness, 1);
		}
		
		foreach (var spriteRenderer in spriteRenderers) {
			spriteRenderer.color = spriteRenderer.color.Lerp(targetColor, Time.deltaTime * fadeSpeed, Channels.RGB);
		}
}
}
