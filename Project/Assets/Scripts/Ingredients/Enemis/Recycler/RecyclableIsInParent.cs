using UnityEngine;
using System.Collections;

public class RecyclableIsInParent : MonoBehaviour, Recyclable {

	public void recycle(){
		transform.parent.GetComponent<Recyclable>().recycle();
	}
}
