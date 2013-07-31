using UnityEngine;
using System.Collections;

public class Swinger : MonoBehaviour {
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
	
	}
	
	// Update is called once per frame
	void Update () {
		
//		handleMoving();
//		handleKeybodyInput();
		
	}
	
	void FixedUpdate() {
		Debug.DrawRay(cam.position, cam.forward*100, Color.red);
		
		Debug.Log ("Velocity: " + rigidbody.velocity.magnitude);

		
//		if(rigidbody.velocity.magnitude > 100) {
//			rigidbody.AddForce(-rigidbody.velocity, ForceMode.Force);
//		}

		
		if(Input.GetButtonDown("Fire1")) {
			if(Physics.Raycast( cam.position, cam.forward*100, out wallData, maximumTetherLength )) {
			    isTethered = true;
			    tetherPoint = wallData.point;
			    tetherLength = wallData.distance;
				rigidbody.AddForce(ropeVector.normalized * rigidbody.mass * 10f, ForceMode.Impulse);
			} else {
				isTethered = false;
			}
		} else if (Input.GetButton ("Fire1")) {
			rigidbody.AddForce(ropeVector.normalized * rigidbody.mass * rigidbody.velocity.magnitude);
			if(tetherLength > 0)
				tetherLength--;	
			else
				tetherLength = 0;	
		} else if (Input.GetButtonUp ("Fire1")) {
			isTethered = false;
		}
		
//		if(Input.GetButton("Jump") && isTethered) {
//			Debug.Log ("Decreasing tetherLength");
//			rigidbody.AddForce(ropeVector.normalized * 50f);
//			if(tetherLength > 0)
//				tetherLength--;	
//			else
//				tetherLength = 0;
//		}
		
		if(isTethered) {
			ropeVector = tetherPoint - transform.position;
			Debug.DrawRay(transform.position, ropeVector, Color.green,10f);
		    if (ropeVector.magnitude > tetherLength) {
//				float centripetalForce = (rigidbody.mass * rigidbody.velocity.magnitude) / tetherLength;
//				float ropeForce = (rigidbody.mass * 10f);
				rigidbody.AddForce(Vector3.up * 10f);
			}
			
			if(Input.GetKey("a")) {
				Debug.Log ("Left");
				rigidbody.AddForce(transform.right * -10f);
			} else if (Input.GetKey("d")) {
				Debug.Log ("Right");
				rigidbody.AddForce(transform.right * 10f);
			}
		}
	}
	
	
}
