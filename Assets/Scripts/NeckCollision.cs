using UnityEngine;
using System.Collections;

public class NeckCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.name == "Sword") {
			Destroy(transform.parent.gameObject);
		}
	}
}
