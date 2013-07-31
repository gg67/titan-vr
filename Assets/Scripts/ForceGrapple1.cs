using UnityEngine;
using System.Collections;

public class ForceGrapple1 : MonoBehaviour {
	public Transform tetherPlanePrefab;
	public Transform cam;
	public bool isTethered = false;
	float maximumTetherLength = 1000;
	float tetherLength = 0;
	Vector3 tetherPoint = new Vector3();
	Vector3 ropeVector;
	RaycastHit wallData;
	GameObject tetherPlane;
	public bool passedTetherPlane = false;
	Vector3 forward;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindWithTag("MainCamera").transform;
		forward = transform.FindChild("ForwardDirection").transform.rotation.eulerAngles;
	}

	
	void FixedUpdate() {
		Debug.DrawRay(cam.position, cam.forward*100, Color.red);
	
		if(Input.GetButtonDown("Jump")) {
			Debug.Log ("GetButtonDown");
			shootGrapple();	
			rigidbody.AddForce(((ropeVector.normalized + Vector3.up) * 10f), ForceMode.Impulse);
//			rigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
		} else if (Input.GetButton ("Jump")) {
			Debug.Log ("GetButton");
//			reelGrapple();	
		} else if (Input.GetButtonUp ("Jump")) {
			isTethered = false;
			Destroy(tetherPlane);
			passedTetherPlane = false;
		} else {
			isTethered = false;
			Destroy(tetherPlane);
			passedTetherPlane = false;
		}
		
		if(isTethered) {
//			addRopeForce();			
			handleMovement();
			ropeVector = tetherPoint - transform.position;
		}
	}
	
	void shootGrapple() {
		if(Physics.Raycast( cam.position, forward.normalized*100, out wallData, maximumTetherLength )) {
			isTethered = true;
		    tetherPoint = wallData.point;
		    tetherLength = wallData.distance;
			ropeVector = tetherPoint - transform.position;
//			createPlane();
		} else {
			isTethered = false;
		}			
	}
	
	void addRopeForce() {
		if(tetherLength < 0) { Debug.DrawRay(transform.position, ropeVector, Color.green,10f);
			rigidbody.AddForce(ropeVector.normalized * rigidbody.velocity.magnitude);	
		}
		
	}
	
	void reelGrapple() {
		Debug.DrawRay(transform.position, ropeVector, Color.blue,10f);
		rigidbody.AddForce(ropeVector.normalized * 30f);
		rigidbody.AddForce(Vector3.up * 10f);
		tetherLength = Vector3.Distance(transform.position, tetherPoint);
	}
	
	void handleMovement() {
		if(Input.GetKey("w")) {
			rigidbody.AddForce(Vector3.up * 10f);
		} else if(Input.GetKey("s")) {
			rigidbody.AddForce(Vector3.up * -10f);
		} else if(Input.GetKey("a")) {
			rigidbody.AddForce(Vector3.right * -10f);
		} else if(Input.GetKey("d")) {
			rigidbody.AddForce(Vector3.right * 10f);
		}
	}
	
	void createPlane() {
		Debug.Log ("Creating plane");
		tetherPlane = Instantiate(tetherPlanePrefab) as GameObject;
//		tetherPlane.transform.localScale = new Vector3(50,50,50);
//		tetherPlane.transform.position = tetherPoint; 
//		tetherPlane.transform.rotation = Quaternion.FromToRotation(Vector3.up, wallData.normal);
	}
	
	
}

