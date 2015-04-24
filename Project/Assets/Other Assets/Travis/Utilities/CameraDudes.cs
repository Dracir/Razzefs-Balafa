using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraDudes : MonoBehaviour {
	
	public Transform[] follow;
	
	float border = 2;
	
	Vector3 init;
	
	void Start () {
		init = transform.position;
		
	}
	
	void Update () {
		Vector3 average = Vector3.zero;
		foreach (Transform t in follow) {
			average += t.position;
		}
		average /= follow.Length;
		
		transform.position = new Vector3(average.x, average.y, init.z);
	}

	public void AddPlayers(GameObject[] players) {
		List<Transform> dudes = new List<Transform>();
		foreach(GameObject go in players){
			if (go == null)
				continue;
			dudes.Add(go.transform);
		}
		follow = dudes.ToArray();
		
	}
}
