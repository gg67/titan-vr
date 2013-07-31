using UnityEngine;
using System.Collections;

public class Roper: MonoBehaviour {
	public GameObject linkPrefab;
	
	public Transform cam;
	public Vector3 velocity;
	public Vector3 oldVelocity;
	public Vector3 acceleration;
	
	public bool isTethered = false;
	float maximumTetherLength = 100;
	float tetherLength = 0;
	Vector3 tetherPoint = new Vector3();
	RaycastHit wallData;
	int numLinks;


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

		
		if(Input.GetButtonDown("Fire1")) {
			Debug.Log("Fire1");
			if(Physics.Raycast( cam.position, cam.forward*100, out wallData, maximumTetherLength )) {
			    isTethered = true;
			    tetherPoint = wallData.point;
			    tetherLength = wallData.distance;
				Vector3 ropeVector = tetherPoint - transform.position;
				
				// create rope
				Debug.Log ("Creating rope");
				GameObject rope = new GameObject();
				numLinks = (int)tetherLength;
				GameObject lastLink = null;
				for (int i=0; i<numLinks; ++i) {
					if(i == numLinks-1) {
						Debug.Log ("Creating last link");
						GameObject link = (GameObject)Instantiate(linkPrefab, tetherPoint-ropeVector.normalized*2, Quaternion.LookRotation(ropeVector));
						link.GetComponent<CharacterJoint>().connectedBody = rigidbody;
//						link.parent = rope.transform;
					} else if(lastLink) {
						GameObject link = (GameObject)Instantiate(linkPrefab, tetherPoint-ropeVector.normalized, lastLink.transform.rotation);
						link.GetComponent<CharacterJoint>().connectedBody = lastLink.rigidbody;
//						link.parent = rope.transform;
					} else if(i==0) {
						Debug.Log ("Creating first link");
						lastLink = (GameObject)Instantiate(linkPrefab, tetherPoint, Quaternion.LookRotation(ropeVector));
					}
				}
			} else {
				isTethered = false;
			}
		}
		
		if(Input.GetButton("Jump") && isTethered) {
			
			Vector3 ropeVector = tetherPoint - transform.position;

		}
		
		if(isTethered) {
			Vector3 ropeVector = tetherPoint - transform.position;
			Debug.DrawRay(transform.position, ropeVector, Color.green,10f);
		    if (ropeVector.magnitude > tetherLength) {

			}
		}
	}
	
}
