using UnityEngine;
using System.Collections;

public class ForceGrapple : MonoBehaviour {
	public Transform cam;
	public Vector3 velocity;
	public Vector3 oldVelocity;
	public Vector3 acceleration;
	
	public bool isTethered = false;
	float maximumTetherLength = 1000;
	float tetherLength = 0;
	Vector3 tetherPoint = new Vector3();
	Vector3 ropeVector;
	RaycastHit wallData;


	// Use this for initialization
	void Start () {
		cam = GameObject.FindWithTag("MainCamera").transform;
	}
	
	void FixedUpdate() {
		Debug.DrawRay(cam.position, cam.forward*100, Color.red);
	
		if(Input.GetButtonDown("Jump")) {
			shootGrapple();	
		} else if (Input.GetButton ("Jump")) {
			reelGrapple();	
		} else if (Input.GetButtonUp ("Jump")) {
			isTethered = false;
		}
		
		if(isTethered) {
			addRopeForce();			
		}
	}
	
	void shootGrapple() {
		if(Physics.Raycast( cam.position, cam.forward*100, out wallData, maximumTetherLength )) {
			isTethered = true;
		    tetherPoint = wallData.point;
		    tetherLength = wallData.distance;
		} else {
			isTethered = false;
		}			
	}
	
	void addRopeForce() {
		ropeVector = tetherPoint - transform.position;
		Debug.DrawRay(transform.position, ropeVector, Color.green,10f);
	    if (ropeVector.magnitude > tetherLength) {
			rigidbody.AddForce((ropeVector.normalized - rigidbody.velocity.normalized) * rigidbody.velocity.magnitude);
		}	
	}
	
	void reelGrapple() {
		rigidbody.AddForce(ropeVector.normalized * 50f);
		if(tetherLength > 0)
			tetherLength--;	
		else
			tetherLength = 0;
	}
	
	
}

