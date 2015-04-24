﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraDudes : MonoBehaviour {
	
	public List<Transform> follow;
	
	GameObject flag;
	float border = 5;
	
	Vector3 init;
	float ignoreDistance = 5f;

	float lerpAmount = 0.1f;
	
	float zoomOutAmount = 0.1f;
	
	//how much of the screen do I want between the players and the flag?
	float zoomMarginPercentage = 0.7f;
	
	void Start () {
		init = transform.position;
		
	}
	
	void Update () {
		Vector3 average = Vector3.zero;
		if (follow == null)
			return;
		
		//see if there's a flag now
		if (flag == null){
			flag = GameObject.FindWithTag("EndFlag");
			if (flag != null){
				follow.Add(flag.transform);
			}
		}
		
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
		Debug.Log(string.Format("My borders? are {0}, {1}, {2}, and {3}", smallestX, largestX, smallestY, largestY));
		
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