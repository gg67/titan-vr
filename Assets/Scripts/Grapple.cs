using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {
	public GameObject usedGrapplePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag != "Player") {
			Instantiate(usedGrapplePrefab, collision.transform.position, collision.transform.rotation);
			Destroy(gameObject);
		}
	
	}
}
