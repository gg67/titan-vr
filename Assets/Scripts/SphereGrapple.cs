using UnityEngine;
using System.Collections;

public class SphereGrapple : MonoBehaviour {
	public Transform cam;
	RaycastHit wallData;
	Vector3 hitPoint;
	float maximumTetherLength = 1000f;
	float tetherLength = 0;
	bool isTethered = false;
	Vector3 ropeVector;
	GameObject ropeSphere;
	
	void Start() {
		cam = GameObject.FindWithTag("MainCamera").transform;
	}
	
	void FixedUpdate() {
		Debug.DrawRay(cam.position, cam.forward*100, Color.red);
		if(Input.GetButtonDown("Fire1")) {
			fireGrapple();
		} else if(Input.GetButtonUp("Fire1")) {
			isTethered = false;
			Destroy(ropeSphere);
		}
		
		if(isTethered) {
			grappleMeToPoint();
		}
	}
	
	void fireGrapple () {
		if(Physics.Raycast( cam.position, cam.forward*100, out wallData, maximumTetherLength )) {
		    isTethered = true;
		    hitPoint = wallData.point;
			ropeVector = wallData.point - transform.position;
			tetherLength = wallData.distance;
			createSphere();
		} else {
			isTethered = false;
		}

	}
	
	void grappleMeToPoint () {
		if(!ropeSphere.collider.enabled) {
			ropeSphere.collider.enabled = true;	
		}
		
		if(tetherLength > 0) {
			rigidbody.AddForce(ropeVector.normalized * 50f);
		}
		ropeVector = wallData.point - transform.position;
		tetherLength = Mathf.Abs(ropeVector.magnitude);
	}
	
	void applyRopeForce() {
		ropeVector = wallData.point - transform.position;
	}
	
	void createSphere() {
		ropeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		ropeSphere.transform.localScale = new Vector3(tetherLength*2, tetherLength*2, tetherLength*2);
		ropeSphere.transform.position = hitPoint;
		ropeSphere.collider.enabled = false;
	}
}
