using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {
	public GameObject[] floorTile = new GameObject[5];
	public float floorTileLength;
	public Transform respawnPoint;
	public Transform spawnPoint;
	public Transform deletePoint;
	//public List <GameObject> activeGroundTiles;
	public float startingSpeed;
	public float startingSpeedMultiplier;
	public int band;



	// Use this for initialization
	void Awake () {
			respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform;
			spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
			deletePoint = GameObject.FindGameObjectWithTag("DeletePoint").transform;

			
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0,0,startingSpeed + (AudioPeer._audioBandBuffer[band]  * startingSpeedMultiplier)));
		
		if(transform.position.z < respawnPoint.position.z && GameObject.FindGameObjectsWithTag("FloorTile").Length < 3){
			GameObject tile = Instantiate(floorTile[Random.Range(0,floorTile.Length)] );
			tile.transform.position = spawnPoint.position;
		}

		if (transform.position.z < deletePoint.position.z){
			Destroy(gameObject);
		
		}
			
		
		
	}
}
