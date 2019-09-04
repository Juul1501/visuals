﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKankerveelCubes : MonoBehaviour {

	public GameObject _sampleCubePrefab;
	public float heighMultiplier;
	GameObject[] _sampleCube = new GameObject [512];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 512; i++)
		{
			GameObject _instanceSampleCube = (GameObject) Instantiate (_sampleCubePrefab);
			_instanceSampleCube.transform.position = this.transform.position;
			_instanceSampleCube.transform.parent = this.transform;
			_instanceSampleCube.name = "SampleCube" + i;
			this.transform.eulerAngles = new Vector3 (0, -0.703125f * i, 0);
			_instanceSampleCube.transform.position = Vector3.forward * 100;
			_sampleCube[i] = _instanceSampleCube;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 512; i++)
		{
			if (_sampleCube != null)
			{
				_sampleCube[i].transform.localScale = new Vector3 (1,(AudioPeer._samples[i] * heighMultiplier) + 2,1);

			}
		}
	}
}
