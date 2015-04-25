using UnityEngine;
using System.Collections;

public class AnimatorRandomFloatSender : MonoBehaviour {

	Animator animator;
	
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	
	void Update () {
		animator.SetFloat("Random", Random.Range(0f,1f));
	}
}
