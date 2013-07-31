using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {
	public int waveNumber = 0;
	public int enemiesLeft = 0;
	private GameObject player;
	private GameObject menu;
	private Spawner spawner;
	private List<GameObject> enemiesInWave;

	void Awake() {
		spawner = GetComponent<Spawner>();
		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Use this for initialization
	void Start () {
		enemiesInWave = new List<GameObject>();
		startWave();
	}
	
	// Update is called once per frame
	void Update () {
		if (enemiesLeft == 0 && enemiesInWave.Count > 0) {
			++waveNumber;
			enemiesLeft = enemiesInWave.Count;
			spawner.spawn();
		}
	}
	
	public void startWave() {
		spawner.spawn();
	}
	
}
