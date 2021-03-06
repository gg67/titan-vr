using UnityEngine;
using System.Collections;
using Pathfinding;

public class TitanAI : MonoBehaviour {
   //The point to move to
    public Transform target;
	public GameObject[] targets;
    
    private Seeker seeker;
    private CharacterController controller;
 
    //The calculated path
    public Path path;
    
    //The AI's speed per second
    public float speed = 100;
    
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
 
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
 
    public void Start () {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
		targets = GameObject.FindGameObjectsWithTag("Respawn");
		target = targets[Random.Range(0,targets.Length)].transform;
		Debug.Log(target.position);
        
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath (transform.position,target.position, OnPathComplete);
    }
    
    public void OnPathComplete (Path p) {
        Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }
 
    public void FixedUpdate () {
		seeker.StartPath (transform.position,target.position, OnPathComplete);	
        if (path == null) {
			Debug.Log ("Path is null");
            //We have no path to move after yet
            return;
        }
        
        if (currentWaypoint >= path.vectorPath.Count) {
			target = targets[Random.Range(0,targets.Length)].transform;
            currentWaypoint = 0;
			seeker.StartPath (transform.position,target.position, OnPathComplete);
            Debug.Log ("End Of Path Reached");
            return;
        }
        
        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.SimpleMove (dir);
		
        Debug.Log ("currentWaypoint = " + currentWaypoint);
		Debug.Log ("Distance to waypoint = " + Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]));
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
    }
}