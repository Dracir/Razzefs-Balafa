using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollowMany : MonoBehaviour {
	
	public List<Transform> follow;
	
	GameObject flag;
	
	Vector3 init;

	float lerpAmount = 0.1f;
	
	float zoomOutAmount = 0.1f;
	
	//how much of the screen do I want between the players and the flag?
	float zoomMarginPercentage = 0.7f;
	
	void Start () {
		init = transform.position;
		
	}
	
	void Update () {
		Vector3 average = Vector3.zero;
		if (follow == null || follow.Count == 0)
			return;
		
		//determine new position based on follow things
		float smallestX = Mathf.Infinity;
		float largestX = Mathf.NegativeInfinity;
		float smallestY = Mathf.Infinity;
		float largestY = Mathf.NegativeInfinity;
		
		for (int i = 0; i < follow.Count; i ++) {
			if (follow[i] == null){
				follow.Remove(follow[i]);
				continue;
			}
			average += follow[i].position;
			
			smallestX = Mathf.Min(smallestX, Camera.main.WorldToScreenPoint(follow[i].position).x);
			largestX = Mathf.Max(largestX, Camera.main.WorldToScreenPoint(follow[i].position).x);
			smallestY = Mathf.Min(smallestY, Camera.main.WorldToScreenPoint(follow[i].position).y);
			largestY = Mathf.Max(largestY, Camera.main.WorldToScreenPoint(follow[i].position).y);
			
		}
		
		if (largestX - smallestX > Screen.width * zoomMarginPercentage || largestY - smallestY > Screen.height * zoomMarginPercentage){
			init -= Vector3.forward * zoomOutAmount;
		}
		average /= follow.Count;

		transform.position = Vector3.Lerp(transform.position, new Vector3(average.x, average.y, init.z), lerpAmount);
	}

	public void SetFollowing(GameObject[] players) {
		List<Transform> dudes = new List<Transform>();
		foreach(GameObject go in players){
			if (go == null)
				continue;
			dudes.Add(go.transform);
		}
		if (flag){
			dudes.Add(flag.transform);
		}
		follow = dudes;
		
	}
	
}
