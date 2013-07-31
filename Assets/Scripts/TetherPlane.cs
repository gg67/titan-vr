using UnityEngine;
using System.Collections;

public class TetherPlane : MonoBehaviour {
	public ForceGrapple1 fg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collision) {
		fg = collision.gameObject.GetComponent<ForceGrapple1>();	
		fg.passedTetherPlane = true;
	}
}
