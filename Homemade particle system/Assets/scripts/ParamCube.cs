using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour {
	public int _band;
	public float _startScale, _scaleMultiplier;
    public bool _useBuffer;
    public float noiseSpeedMultiplier;
    public Renderer rend;



	// Use this for initialization
	void Start () {
        rend = gameObject.GetComponent<Renderer>();
	}

    // Update is called once per frame
    void Update() {
        if (_useBuffer)
        {
            transform.localScale = new Vector3((AudioPeer._amplitudeBuffer * _scaleMultiplier) + _startScale, (AudioPeer._amplitudeBuffer * _scaleMultiplier) + _startScale, transform.localScale.z);
            rend.material.SetFloat("noise speed", AudioPeer._amplitudeBuffer * noiseSpeedMultiplier);
        } else
        {
            transform.localScale = new Vector3((AudioPeer._freqBand[_band] * _scaleMultiplier )+ _startScale, (AudioPeer._amplitudeBuffer * _scaleMultiplier) + _startScale,( AudioPeer._bandBuffer[_band] * _scaleMultiplier) +_startScale);
            rend.material.SetFloat("noise speed", AudioPeer._amplitudeBuffer * noiseSpeedMultiplier);
        }
	}
}
