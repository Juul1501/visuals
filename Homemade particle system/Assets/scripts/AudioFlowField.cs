using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NoiseFlowField))]
public class AudioFlowField : MonoBehaviour {

	NoiseFlowField _noiseFlowField;
	public AudioPeer _audioPeer;

	[Header("Speed")]
	public bool _useSpeed;
	public Vector2 _movespeedMinMax, _rotateSpeedMinMax;
	public int band;
	[Header("scale")]
	public bool _useScale;
	public Vector2 _scaleMinMax;
	[Header("Material")]
	public Material _material;
	private Material [] _audioMaterial;
	public bool _useColor1;
	public Gradient _gradient1;
	public Color[] _color1;
	public string _colorName1;
	[Range(0f, 1f)]
	public float _colorTreshold1;
	public float _colorMultiplier1;
	public bool _useColor2;
	public Color[] _color2;
	public string _colorName2;
	[Range(0f, 1f)]
	public float _colortreshold2;
	public float _colorMultiplier2;
	public Gradient _gradient2;


	// Use this for initialization
	void Start () {
		int countBand = 0;
		_noiseFlowField = GetComponent<NoiseFlowField>();
		_audioMaterial = new Material[8];
		_color1 = new Color[8];
		_color2 = new Color[8];
		for (int i = 0; i < 8; i++)
		{
			_color1[i] = _gradient1.Evaluate((1f / 8f) * i);
			_color2[i] = _gradient2.Evaluate((1f / 8f) * i);
			_audioMaterial[i] = new Material (_material);
		}

		for(int i = 0; i < _noiseFlowField._particleAmount; i++)
		{
			int band = countBand % 8;
			_noiseFlowField._particleMeshRenderer[i].material = _audioMaterial[band];
			_noiseFlowField._particles[i].audioBand = band;
			countBand ++;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( _useSpeed)
		{
			_noiseFlowField._particleMoveSpeed = Mathf.Lerp(_movespeedMinMax.x, _movespeedMinMax.y, AudioPeer._audioBandBuffer[0] / 5);
			_noiseFlowField._particleRotateSpeed = Mathf.Lerp(_rotateSpeedMinMax.x, _rotateSpeedMinMax.y, AudioPeer._audioBandBuffer[0] / 5);
			
		}

		for(int i = 0; i < _noiseFlowField._particleAmount; i++)
		{
				
				float scale = Mathf.Lerp(_scaleMinMax.x,_scaleMinMax.y, AudioPeer._audioBandBuffer [_noiseFlowField._particles[i].audioBand] / 5);
				_noiseFlowField._particles[i].transform.localScale = new Vector3 (scale,scale,scale);
			
		}

		for(int i = 0; i < 8; i++)
		{
			if(_useColor1)
			{
				if(AudioPeer._audioBandBuffer [i] > _colorTreshold1) 
				{
					_audioMaterial[i].SetColor(_colorName1, _color1[i] * AudioPeer._audioBandBuffer[i] * _colorMultiplier1);
				} else {
					_audioMaterial[i].SetColor(_colorName1, _color1[i] * 0f);

				}
			}

			if(_useColor2)
			{
				if(AudioPeer._audioBand [i] > _colortreshold2) 
				{
					_audioMaterial[i].SetColor(_colorName2, _color2[i] * AudioPeer._audioBand[i] * _colorMultiplier2);
				} else {
					_audioMaterial[i].SetColor(_colorName2, _color2[i] * 0f);

				}
			}
		}
	}

	
}
