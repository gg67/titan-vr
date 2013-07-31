using UnityEngine;
using System.Collections;

public class VRForceGrapple : MonoBehaviour {
	public bool hydraEnabled = false;
	public static int sLEFT = 0;
	public static int sRIGHT = 1;
	
	public Transform cam;
    Transform[] hands = new Transform[2];
	
	bool[] isTethered = new bool[2];
	float maximumTetherLength = 1000;
	float[] tetherLengths = new float[2];
	Vector3[] tetherPoints = new Vector3[2];
	Vector3[] ropeVectors = new Vector3[2];
	RaycastHit[] wallDatas = new RaycastHit[2];
	bool[] triggerIsPulled = new bool[2];
	
	float antiGravityForce = 10f;
	float reelingForce = 10f;
	float initialReelForce = 10f;
	bool grounded = false;


	// Use this for initialization
	void Start () {
		cam = GameObject.FindWithTag("MainCamera").transform;
//		hydraEnabled = SixenseInput.IsBaseConnected( 0 );
		hands[0] = transform.FindChild("Left Hand").transform;
		hands[1] = transform.FindChild("Right Hand").transform;
	}
	
	void FixedUpdate() {
		if(hydraEnabled)
			getHyrdraInput();
		else
			getKeyboardMouseInput();
		
		grounded = false;
		collider.enabled = true;
	}
	
	void getKeyboardMouseInput() {
//		Debug.DrawRay(hands[0].position, hands[0].forward*100, Color.red);
		Debug.DrawRay(cam.position, cam.forward*100, Color.red);
		if(Input.GetButtonDown("Fire1")) {
			triggerIsPulled[0] = true;
			shootGrapple(0);	
			
		} else if (Input.GetButton("Fire1")) {
			reelGrapple(0);	
			
		} else {
			triggerIsPulled[0] = false;
			isTethered[0] = false;
		}
		
		if(Input.GetKeyDown(KeyCode.LeftShift)) {
			triggerIsPulled[1] = true;
			shootGrapple(1);	
		} else if (Input.GetKey(KeyCode.LeftShift)) {
			reelGrapple(1);	
		} else {
			triggerIsPulled[1] = false;
			isTethered[1] = false;
		}
	
		for(uint i=0; i<2; ++i) {
			if(isTethered[i]) {
				addRopeForce(i);	
				Debug.DrawRay(transform.position, ropeVectors[i], Color.green,10f);
				ropeVectors[i] = tetherPoints[i] - transform.position;
			}	
		}
		
	}
	
	void getHyrdraInput() {
		// Hydra Controls
		for ( uint i = 0; i < 2; i++ )
		{
//			Debug.DrawRay(hands[i].position, hands[i].forward*100, Color.red);
			Debug.DrawRay(cam.position, cam.forward*100, Color.red);
			if ( SixenseInput.Controllers[i] != null )
			{
				if(SixenseInput.Controllers[i].Trigger > 0 && !triggerIsPulled[i]) {
					triggerIsPulled[i] = true;
					shootGrapple(i);	
				} else if (SixenseInput.Controllers[i].Trigger > 0) {
					reelGrapple(i);	
				} else {
					triggerIsPulled[i] = false;
					isTethered[i] = false;
				}
				
				if(isTethered[i]) {
					addRopeForce(i);	
					Debug.DrawRay(transform.position, ropeVectors[i], Color.green,10f);
					ropeVectors[i] = tetherPoints[i] - transform.position;
				}
			}
		}
	}
	
	void shootGrapple(uint dir) {
//		if(Physics.Raycast( hands[dir].position, hands[dir].forward*100, out wallDatas[dir], maximumTetherLength)) {
		if(Physics.Raycast( cam.position, cam.forward*100, out wallDatas[dir], maximumTetherLength)) {
			isTethered[dir] = true;
		    tetherPoints[dir] = wallDatas[dir].point;
		    tetherLengths[dir] = wallDatas[dir].distance;
			ropeVectors[dir] = tetherPoints[dir] - transform.position;
			if(grounded) {
				collider.enabled = false;
				rigidbody.AddForce(ropeVectors[dir].normalized * initialReelForce, ForceMode.Impulse);
			}
		} else {
			isTethered[dir] = false;
		}			
	}
	
	void addRopeForce(uint dir) {
		ropeVectors[dir] = tetherPoints[dir] - transform.position;
		Debug.DrawRay(transform.position, ropeVectors[dir], Color.green,10f);
	    if (ropeVectors[dir].magnitude > tetherLengths[dir]) {
			rigidbody.AddForce((ropeVectors[dir].normalized - rigidbody.velocity.normalized) * rigidbody.velocity.magnitude);
		}	
	}
	
	void reelGrapple(uint dir) {
		rigidbody.AddForce(ropeVectors[dir].normalized * reelingForce);
		rigidbody.AddForce(Vector3.up * antiGravityForce);
		tetherLengths[dir]--;
		if(tetherLengths[dir] < 0) {
			tetherLengths[dir] = 0;
		}
	}
	
	void OnCollisionStay () {
	    grounded = true;    
	}
	
	
}

