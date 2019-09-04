using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (LineRenderer))]
public class KochLine : KochGenerator {


	LineRenderer _lineRenderer;


	// Use this for initialization
	void Start () {
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.enabled = true;
		_lineRenderer.useWorldSpace = false;
		_lineRenderer.loop = true;
		_lineRenderer.positionCount = _position.Length;
		_lineRenderer.SetPositions(_position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

