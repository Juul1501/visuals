using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochGenerator : MonoBehaviour {
	protected enum _axis
	{
		XAxis,
		YAxis,
		ZAxis
	};

	[SerializeField]
	protected _axis axis = new _axis();

	protected enum _initiator
	{
		Triangle,
		Square,
		Pentagon,
		Hexagon,
		Heptagon,
		Octagon
	};

	public struct LineSegment
	{
		public Vector3 StartPosition {get; set;}
		public Vector3 EndPosition {get; set;}
		public Vector3 Direction {get; set;}
		public float Length {get; set;}
	}

	[SerializeField]
	protected _initiator initiator = new _initiator();
	[SerializeField] 
	protected AnimationCurve _generator;
	protected Keyframe[] _keys;

	protected int _generationCount;

	protected int _initiatorPointAmount;
	private Vector3[] _initiatorPoint;
	private Vector3 _rotateVector;
	private Vector3 _rotateAxis;
	protected float _initialRotation;
	[SerializeField]
	protected float _initiatorSize;


	protected Vector3[] _position;
	protected Vector3[] _targetPosition;
	private List <LineSegment> _lineSegment;


	void Awake()
	{
		GetInitiatorPoints();
		//assign lists and arrays

		_position = new Vector3[_initiatorPointAmount +1];
		_targetPosition = new Vector3[_initiatorPointAmount + 1];
		_keys = _generator.keys;
		_lineSegment = new List<LineSegment>();

		_initiatorPoint = new Vector3 [_initiatorPointAmount];
		_rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector; 
		
		for (int i = 0; i < _initiatorPointAmount; i++)
		{
			_position[i] = _rotateVector * _initiatorSize ;
			_rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;  
		}	

		_position [_initiatorPointAmount] = _position[0];
	}

	protected void KochGeneration(Vector3 [] positions, bool outwards, float generatorMultiplier)
	{
		//creat line segments
		_lineSegment.Clear();
		for (int i = 0; i < positions.Length - 1; i++)
		{
			LineSegment line = new LineSegment();
			line.StartPosition = positions[i];
			if (i == positions.Length - 1)
			{
				line.EndPosition = positions[0];
			} else {
				line.EndPosition = positions[i + 1];
			}

			line.Direction = (line.EndPosition - line.StartPosition).normalized;
			line.Length = Vector3.Distance(line.EndPosition,line.StartPosition);
			_lineSegment.Add(line);

		}
		// add the line segment points to a point array

		List<Vector3> newPos = new List<Vector3>();
		List<Vector3> targetPos = new List<Vector3>();

		for (int i = 0; i < _lineSegment.Count; i++)
		{
			newPos.Add(_lineSegment[i].StartPosition);
			targetPos.Add(_lineSegment[i].StartPosition);

			for (int j = 0; j < _keys.Length - 1; j++)
			{
				float moveAmount = _lineSegment[i].Length * _keys[j].time;
				float heightAmount = (_lineSegment[i].Length * _keys[j].value) * generatorMultiplier;
				Vector3 movePos = _lineSegment[i].StartPosition + (_lineSegment[i].Direction * moveAmount);
				Vector3 dir;
				if (outwards)
				{
					dir = Quaternion.AngleAxis(-90, _rotateAxis) * _lineSegment[i].Direction;
				} else {
					dir = Quaternion.AngleAxis(90, _rotateAxis) * _lineSegment[i].Direction;
				}

			}
		}

	}
	
	

	private void OnDrawGizmos()
	{
		GetInitiatorPoints();
		_initiatorPoint = new Vector3 [_initiatorPointAmount];
		_rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector; 

		for (int i = 0; i < _initiatorPointAmount; i++)
		{
			_initiatorPoint[i] = _rotateVector * _initiatorSize ;
			_rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;  
		}

		for (int i = 0; i < _initiatorPointAmount; i++)
		{
			Gizmos.color = Color.red;
			Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
			Gizmos.matrix = rotationMatrix;
			if (i < _initiatorPointAmount - 1)
			{
				Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[i + 1]);
			} else {
				Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[0]);

			}
		}
		
	}
	
	
	private void GetInitiatorPoints()
	{
		switch(initiator){
		
			case _initiator.Triangle:
				_initiatorPointAmount = 3;
				_initialRotation = (360 / _initiatorPointAmount) / 2 ;
				break;

			case _initiator.Square:
				_initiatorPointAmount = 4;
				_initialRotation = (360 / _initiatorPointAmount) / 2 ;
				break;

			case _initiator.Pentagon:
				_initiatorPointAmount = 5;
				_initialRotation = (360 / _initiatorPointAmount) / 2 ;
				break;

			case _initiator.Hexagon:
				_initiatorPointAmount = 6;
				_initialRotation = (360 / _initiatorPointAmount) / 2 ;
				break;

			case _initiator.Heptagon:
				_initiatorPointAmount = 7;
				_initialRotation = (360 / _initiatorPointAmount) / 2 ;
				break;

			case _initiator.Octagon:
				_initiatorPointAmount = 8;
				_initialRotation = (360 / _initiatorPointAmount) / 2 ;
				break;

			default:
				_initiatorPointAmount = 3;
				_initialRotation = (360 / _initiatorPointAmount) / 2 ;
				break;
		}

		switch(axis){

			case _axis.XAxis:
				_rotateVector = new Vector3 (1,0,0);
				_rotateAxis = new Vector3 (0,0,1);
				break;

			case _axis.YAxis:
				_rotateVector = new Vector3 (0,1,0);
				_rotateAxis = new Vector3 (1,0,0);
				break;

			case _axis.ZAxis:
				_rotateVector = new Vector3 (0,0,1);
				_rotateAxis = new Vector3 (0,1,0);
				break;

			default:
				_rotateVector = new Vector3 (0,1,0);
				_rotateAxis = new Vector3 (1,0,0);
				break;

		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (_initiatorPointAmount);
		OnDrawGizmos();
	}
}
