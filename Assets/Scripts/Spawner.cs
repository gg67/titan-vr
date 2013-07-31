using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Spawner : MonoBehaviour {
	public Transform[] spawns;
	private Dictionary<int,bool> spawnUseDict;
	private int numSpawns;
	public Transform prefabToSpawn;
	

	// Use this for initialization
	void Start () {
		numSpawns = spawns.Length;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void resetSpawnUseDict() {
		spawnUseDict = new Dictionary<int,bool>();
		for(int i=0; i<numSpawns; ++i) {
			spawnUseDict[i] = false;
		}
	}
	
	public void spawn() {
		foreach (Transform spawn in spawns) {
			Instantiate(prefabToSpawn, spawn.position, spawn.rotation);
		}
	}

	
	public void spawn(GameObject go) {
		foreach (Transform spawn in spawns) {
			Instantiate(go, spawn.position, spawn.rotation);
		}
	}
	
	public void spawn(List<GameObject> gameObjects) {
		int numGameObjects = gameObjects.Count;
		int maxSpawns = Mathf.Min(numGameObjects, numSpawns);
		
		for(int i=0; i<maxSpawns; ++i) {
			Instantiate(gameObjects[i], spawns[i].position, spawns[i].rotation);
		}
		
	}
	
	public void spawn(GameObject go, Transform spawnPoint) {
		Instantiate(go, spawnPoint.position, spawnPoint.rotation);
	}
	
	public int spawnCount() {
		return spawns.Length;
	}
}
