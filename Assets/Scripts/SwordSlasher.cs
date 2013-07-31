using UnityEngine;
using System.Collections;

public class SwordSlasher : MonoBehaviour {
	Transform lSword, rSword;
	Vector3 initPos;
	Transform rotationPoint, init;
	Quaternion initRotation, lastRotation;
	public Transform to;
	public float speed = 5f;
	bool slashing = false;
	float rotationAmount = 0f;

	// Use this for initialization
	void Start () {
		lSword = transform.Find("LeftSword");
		rSword = transform.Find("RightSword");
		rotationPoint = transform.Find("Rotation Point");
		
		initPos = transform.position;
		initRotation = transform.localRotation;
		lastRotation = initRotation;
		
		Debug.Log("rotPoint: " + rotationPoint);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			slash();
		}
		
		Debug.Log ("rotation amount: " + rotationAmount);
		
		if(slashing) {
	        lSword.RotateAround(rotationPoint.position, rotationPoint.right, 500 * Time.deltaTime);
	        rSword.RotateAround(rotationPoint.position, rotationPoint.right, 500 * Time.deltaTime);
			
			rotationAmount += Quaternion.Angle(lastRotation, lSword.transform.rotation);
			
			if(rotationAmount >= 360) {
				slashing = false;
				rotationAmount = 0;
			}
			lastRotation = lSword.transform.rotation;
		}
		
	}
	
	void slash() {
		Debug.Log ("Slashing");
		slashing = true;
	}
	
}
