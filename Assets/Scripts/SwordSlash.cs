using UnityEngine;
using System.Collections;

public class SwordSlash : MonoBehaviour {
	Vector3 initPos;
	Transform rotationPoint, init;
	Quaternion initRotation, lastRotation;
	public Transform to;
	public float speed = 5f;
	bool slashing = false;
	float rotationAmount = 0f;

	// Use this for initialization
	void Start () {
		init = transform;
		initPos = transform.position;
		initRotation = transform.localRotation;
		lastRotation = initRotation;
		rotationPoint = transform.FindChild("Rotation Point");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			slash();
		}
		
		if(slashing) {
			transform.Rotate(Vector3.right, 800 * Time.deltaTime);
//			transform.Rotate(rotationPoint.right, 800 * Time.deltaTime);
			rotationAmount += Quaternion.Angle(lastRotation, transform.rotation);
			if(rotationAmount >= 360) {
				slashing = false;
				rotationAmount = 0;
				transform.localRotation = initRotation;
			}
			lastRotation = transform.rotation;
		}
		
	}
	
	
	void slash() {
		Debug.Log ("Slashing");
		slashing = true;
	}
	
}
