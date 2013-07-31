using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
 
public class HydraCharacterControls : MonoBehaviour {
	public bool hydraEnabled = false;
 
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;

	public float sensitivityX = 1F;
	public float sensitivityY = 1F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationY = 0F;
 
	void Awake () {
	    rigidbody.freezeRotation = true;
	    rigidbody.useGravity = false;
	}
	
	void Start() {
//		hydraEnabled = SixenseInput.IsBaseConnected( 0 );
	}
	
 
	void FixedUpdate () {
		
		if(hydraEnabled) 
			getHydraInput();
		else
			getKeyboardMouseInput();
		
		// We apply gravity manually for more tuning control
	    rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
	    grounded = false;
	}
 
	void OnCollisionStay () {
	    grounded = true;    
	}
	
	void getKeyboardMouseInput() {
		if (grounded) {
	        // Calculate how fast we should be moving
	        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis ("Vertical"));
	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= speed;
 
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
 
	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
	        }
	    }
		
		// Mouse Head Turning IF OCULUS RIFT IS CONNECTED
//		float rotationX = Input.GetAxis("Mouse X") * sensitivityX;
//		transform.Rotate(Vector3.up, rotationX);

		
	}
	
	void getHydraInput() {
		for ( uint i = 0; i < 2; i++ ) {
			if ( SixenseInput.Controllers[i] != null ) {
				uint controllerNumber = i + 1;
				
			    if (grounded) {
			        // Calculate how fast we should be moving
			        Vector3 targetVelocity = new Vector3(SixenseInput.Controllers[0].JoystickX, 0, SixenseInput.Controllers[0].JoystickY);
			        targetVelocity = transform.TransformDirection(targetVelocity);
			        targetVelocity *= speed;
		 
			        // Apply a force that attempts to reach our target velocity
			        Vector3 velocity = rigidbody.velocity;
			        Vector3 velocityChange = (targetVelocity - velocity);
			        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			        velocityChange.y = 0;
			        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		 
			        // Jump
			        if (canJump && Input.GetButton("Jump")) {
			            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			        }
			    }
				
				// Head turning
				float rotationX = SixenseInput.Controllers[1].JoystickX * sensitivityX;
//				transform.Rotate(0, rotationX * sensitivityX, 0);
				transform.Rotate(Vector3.up, rotationX);
			}
		}	
	}
	
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}