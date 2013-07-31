using UnityEngine;
using System.Collections;

public class LerpGrapple : MonoBehaviour {
	public Transform cam;
	RaycastHit wallData;
	Vector3 hitPoint;
	float maximumTetherLength = 1000f;
	bool isTethered = false;
	
	void Start() {
		cam = GameObject.FindWithTag("MainCamera").transform;
	}
	
	void Update() {
		Debug.DrawRay(cam.position, cam.forward*100, Color.red);
		if(Input.GetButtonDown("Fire1")) {
			fireGrapple();
		} else if(Input.GetButtonUp("Fire1")) {
			isTethered = false;
		}
		
		if(isTethered) {
			grappleMeToPoint();
		}
	}
	
	void FixedUpdate() {
	
	}
	
	// Raycast up to 100 meters forward
	void fireGrapple () {
		if(Physics.Raycast( cam.position, cam.forward*100, out wallData, maximumTetherLength )) {
		    isTethered = true;
		    hitPoint = wallData.point;

		} else {
			isTethered = false;
		}

	}
	
	//Pulls Me to the target position like a spring

	float smooth = 1.0f;
	void grappleMeToPoint () {
		transform.position = Vector3.Lerp (transform.position, hitPoint, Time.deltaTime * smooth);
	}
}
