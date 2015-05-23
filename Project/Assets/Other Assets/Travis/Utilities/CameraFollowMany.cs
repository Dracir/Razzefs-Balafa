using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CameraFollowMany : MonoBehaviour {
	
	public List<Transform> follow;
	
	//how much of the screen do I want between the players and the flag?
	[Disable] public float zoom;
	
	float minZoomMargin = 0.65F;
	float maxZoomMargin = 0.75F;
	float zoomThreshold = 50;
	float minZoom = 15;
	float maxZoom = 25;
	float lerpAmount = 3;
	float zoomOutAmount = 0.5F;
	
	void Start() {
		zoom = transform.position.z;
	}
	
	void Update() {
		if (follow == null || follow.Count == 0) {
			return;
		}
		
		//determine new position based on follow things
		Vector3 average = Vector3.zero;
		float smallestX = Mathf.Infinity;
		float largestX = Mathf.NegativeInfinity;
		float smallestY = Mathf.Infinity;
		float largestY = Mathf.NegativeInfinity;
		
		for (int i = 0; i < follow.Count; i++) {
			if (follow[i] == null) {
				follow.Remove(follow[i]);
				continue;
			}
			
			average += follow[i].position;
			
			smallestX = Mathf.Min(smallestX, Camera.main.WorldToScreenPoint(follow[i].position).x);
			largestX = Mathf.Max(largestX, Camera.main.WorldToScreenPoint(follow[i].position).x);
			smallestY = Mathf.Min(smallestY, Camera.main.WorldToScreenPoint(follow[i].position).y);
			largestY = Mathf.Max(largestY, Camera.main.WorldToScreenPoint(follow[i].position).y);
			
		}
		
		average /= follow.Count;
		
		float maxDifferenceX = (largestX - smallestX) - (Screen.width / 2) * maxZoomMargin;
		float maxDifferenceY = (largestY - smallestY) - (Screen.height / 2) * maxZoomMargin;
		float minDifferenceX = (largestX - smallestX) - (Screen.width / 2) * minZoomMargin;
		float minDifferenceY = (largestY - smallestY) - (Screen.height / 2) * minZoomMargin;
		
		if (maxDifferenceX > zoomThreshold || maxDifferenceY > zoomThreshold) {
			zoom -= zoomOutAmount;
		}
		else if (minDifferenceX < -zoomThreshold || minDifferenceY < -zoomThreshold) {
			zoom += zoomOutAmount;
		}
		
		zoom = Mathf.Clamp(zoom, -maxZoom, -minZoom);
		transform.TranslateTowards(average, lerpAmount, Axes.XY);
		transform.TranslateTowards(zoom, lerpAmount, Axes.Z);
	}

	public void SetFollowing(GameObject[] players) {
		List<Transform> dudes = new List<Transform>();
		
		foreach (GameObject go in players) {
			if (go == null) {
				continue;
			}
			
			dudes.Add(go.transform);
		}

		follow = dudes;
	}
	
}
