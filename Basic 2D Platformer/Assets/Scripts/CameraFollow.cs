using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	//this script will follow the average point between all the objects in the array
	public Transform[] toFollow;

	//'init' should be used if we want to start zooming the camera in and out between the different points
	//private Vector3 init;

	private float lerpAmount = 0.1f;
	public Vector3 offset = Vector3.zero;

	// Use this for initialization
	void Start () {
		//init = transform.position;

		if (toFollow == null){
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			toFollow = new Transform[players.Length];
			int i = 0;
			foreach (var item in players) {
				toFollow[i] = item.transform;
				i++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 averagePos = VectorFunctions.Midpoint(toFollow);

		//take the average pos and lerp towards it; add the offset afterward
		transform.position = Vector3.Lerp(transform.position, new Vector3(averagePos.x, averagePos.y, transform.position.z), lerpAmount) + offset;
	}
}
