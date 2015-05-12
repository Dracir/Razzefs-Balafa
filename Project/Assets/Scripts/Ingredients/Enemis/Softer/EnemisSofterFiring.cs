using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisSofterFiring : State {
	
	public LineRenderer[] lineRenderers;
	public BoxCollider2D laserTrigger;
	
    EnemisSofter Layer {
    	get { return (EnemisSofter)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		int nbLines = lineRenderers.Length;
		for (int i = 0; i < lineRenderers.Length; i++) {
			LineRenderer line  = lineRenderers[i];
			
			line.gameObject.SetActive(true);
			line.SetPosition(0, transform.position);
			line.SetPosition(1, transform.position);
			
			float width = 0.1f + 0.15f*i;
			line.SetWidth(width,width);
			line.GetComponent<SmoothLineRendererOscillate>().widthOffset = width;
			
			float temp = Layer.temperatureChangePerSeconde;
			if(temp > 0){
				Color color = new Color(temp,0,0, 0.9f - (0.9f * i / nbLines));
				line.SetColors(color, color);
			}else{
				Color color = new Color(0,0, Mathf.Abs(temp), 0.9f - (0.9f * i / nbLines));
				line.SetColors(color, color);
			}
			
		}
		laserTrigger.gameObject.SetActive(true);
	}
	
	public override void OnExit() {
		base.OnExit();
		foreach (var line in lineRenderers) {
			line.gameObject.SetActive(false);
		}
		laserTrigger.gameObject.SetActive(false);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		Vector3 start = transform.position  + (transform.right * 0.1f);
		
		RaycastHit2D hit = Physics2D.Raycast(start, transform.right, Layer.maxLazerLenght, Layer.lazerLayerMask);
		
		float overLaser = 0.1f;
		
		if(hit.collider != null){
			//Debug.Log(hit.collider.name);
			Debug.DrawRay(start, transform.right * hit.distance, new Color(1,0.8f,0,0.4f),0.1f);
			foreach (var line in lineRenderers) {
				line.SetPosition(1, new Vector2(hit.point.x + overLaser, hit.point.y));
			}
			laserTrigger.size = new Vector2(hit.distance + overLaser ,0.2f);
			laserTrigger.transform.position = transform.position + transform.right * (hit.distance / 2f + 2 * overLaser) ;
		}else{
			Debug.DrawRay(transform.position + (transform.right * 0.1f), transform.right * Layer.maxLazerLenght, new Color(1,0,0,1f),0.1f);
		}
	}
}
