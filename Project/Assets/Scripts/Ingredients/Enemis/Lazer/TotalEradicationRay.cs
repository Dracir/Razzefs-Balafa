using UnityEngine;
using System.Collections;
using Magicolo;

public class TotalEradicationRay : MonoBehaviour {

	public LineRenderer[] lineRenderers;
	public BoxCollider2D[] triggers;
	public EnemisSofter softer;
	
	void Start () {
	
	}
	
	public void setLazerActive(bool active){
		foreach (var line in lineRenderers) {
			line.gameObject.SetActive(active);
		}
	}

	void Update () {
		foreach (var trigger in triggers) {
			trigger.enabled = false;
		}

		float temp = softer.deltaTemperatureChangePerSeconde;
		int nbLines = lineRenderers.Length;
		for (int i = 0; i < lineRenderers.Length; i++) {
			LineRenderer line  = lineRenderers[i];	
			if(temp > 0){
				Color color = new Color(temp,0,0, 0.9f - (0.9f * i / nbLines));
				line.SetColors(color, color);
			}else{
				Color color = new Color(0,0, Mathf.Abs(temp), 0.9f - (0.9f * i / nbLines));
				line.SetColors(color, color);
			}
		}
		
		float angle = ((Vector2) transform.right).Angle();
		castLazer(0,transform.position, transform.right, -angle, softer.deltaTemperatureChangePerSeconde);
		setLineRenderPositions(0,transform.position);
	}

	void castLazer(int lazerIndex, Vector3 start, Vector3 direction, float rotation, float deltaTemprature) {
		if(lazerIndex == 10) return;
		Vector3 castPosition = start + direction * 0.1f;
		Debug.DrawLine(castPosition, castPosition + new Vector3(0,0.5f,0), Color.red, 1f);
		RaycastHit2D hit = Physics2D.Raycast(castPosition, direction, float.PositiveInfinity, softer.lazerLayerMask);
		
		float overLaser = 0.2f;
		
		
		if(hit.collider != null){
			Debug.DrawRay(castPosition, direction * hit.distance, new Color(1,0.8f,0,0.4f),0.1f);
			setLineRenderCount(lazerIndex+2);
			setLineRenderPositions(lazerIndex + 1, new Vector3(hit.point.x, hit.point.y,0));
			
			Vector3 reflection = Vector3.Reflect(direction, hit.normal);
			float angle = ((Vector2) direction).Angle();
			
			
			TemperatureInfo temperatureInfo = hit.collider.GetComponentInChildren<TemperatureInfo>();
			if(temperatureInfo != null){
				temperatureInfo.Temperature += deltaTemprature * Time.deltaTime;
			}
			
			//setTrigger(lazerIndex, new Vector2(hit.distance + overLaser ,0.2f), start, rotation, deltaTemprature);
			ReflectiveCollider reflectiveCollider = hit.collider.GetComponent<ReflectiveCollider>();
			if(reflectiveCollider != null || true){
				castLazer(lazerIndex + 1, hit.point, reflection.normalized, rotation + angle, deltaTemprature * softer.bounceDeltaTemperatureLost);
			}
			
			
		}else{
			foreach (var line in lineRenderers) {
				line.SetPosition(1, transform.position + transform.right * 1000000);
			}
			Debug.DrawRay(transform.position + (transform.right * 0.1f), transform.right * float.PositiveInfinity, new Color(1,0,0,1f),0.1f);
		}
	}

	void setTrigger(int lazerIndex, Vector2 size, Vector3 positionTrigger, float rotation, float deltaTemprature) {
		
		BoxCollider2D box = triggers[lazerIndex];
		box.enabled = true;
		box.size = size;
		box.offset = new Vector2(size.x / 2f , 0);
		box.transform.position = positionTrigger;
		box.transform.rotation = Quaternion.Euler(0,0,rotation);
		box.GetComponent<LaserTrigger>().deltaTemperaturePerS = deltaTemprature;
	}
	
	void setLineRenderPositions(int index, Vector3 position){
		foreach (var line in lineRenderers) {
			line.SetPosition(index, position);
		}
	}

	void setLineRenderCount(int count) {
		foreach (var line in lineRenderers) {
			line.SetVertexCount(count);
		}
	}
}
