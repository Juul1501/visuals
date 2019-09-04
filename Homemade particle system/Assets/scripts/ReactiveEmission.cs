using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveEmission : MonoBehaviour {

	public Material _material;
	public float red, green, blue;
	public int band;

	public float intensity;


	// Use this for initialization
	void Start () {
		//_material = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		Color _color = new Color (red * AudioPeer._audioBandBuffer[0] * intensity, green * AudioPeer._audioBandBuffer[band]* intensity, blue * AudioPeer._audioBandBuffer[band]* intensity);
		_material.SetColor ("_EmissiveColor", _color);
	}
}
