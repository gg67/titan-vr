using UnityEngine;
using System.Collections;

public class ManeuverGearMovement : MonoBehaviour {
	GameObject player;
	Camera cam;
	public GameObject grapplePrefab;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")) {
			player.rigidbody.AddForce(Vector3.up *15, ForceMode.Impulse);
		} else if(Input.GetButtonDown("Fire1")) {
			Debug.Log ("Fire1 pressed");
			GameObject grapple = (GameObject)Instantiate(grapplePrefab, transform.position, transform.rotation);
			Vector3 v = Input.mousePosition;
//			Vector3 worldPoint = cam.ScreenToWorldPoint(v);
			Ray ray = cam.ScreenPointToRay(v);
	        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
			grapple.rigidbody.AddForce(ray.direction *50, ForceMode.Impulse);
		}
		
	}
}
