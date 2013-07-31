using UnityEngine;
using System.Collections;

public class HingeSwinger : MonoBehaviour {
	public Transform cam;
	public bool isTethered = false;
	float maximumTetherLength = 1000;
	float tetherLength = 0;
	Vector3 tetherPoint = new Vector3();
	Vector3 ropeVector;
	RaycastHit wallData;
	GameObject lastHingedObject;


	// Use this for initialization
	void Start () {
		cam = GameObject.FindWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void FixedUpdate() {
		Debug.DrawRay(cam.position, cam.forward*100, Color.red);
		
		if(Input.GetButtonDown("Fire1")) {
			if(Physics.Raycast( cam.position, cam.forward*100, out wallData, maximumTetherLength )) {
			    isTethered = true;
			    tetherPoint = wallData.point;
			    tetherLength = wallData.distance;
				lastHingedObject = wallData.transform.gameObject;
				lastHingedObject.AddComponent<HingeJoint>();
				lastHingedObject.GetComponent<HingeJoint>().connectedBody = rigidbody;
				lastHingedObject.rigidbody.freezeRotation = true;
			} else {
				isTethered = false;
				Destroy(lastHingedObject.GetComponent<HingeJoint>());
			}
		} else if (Input.GetButton ("Fire1")) {
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
		
	}
	
	
}

