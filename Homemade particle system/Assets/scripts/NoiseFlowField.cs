using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFlowField : MonoBehaviour {

	public float cubesize;
	FastNoise _fastNoise;
	public Vector3Int _gridSize;
	public Vector3[,,] _flowfieldDirection;
	public float _increment;
	public Vector3 _offset, _offsetSpeed;

	//particles

	public GameObject _particlePrefab;
	public int _particleAmount;
	public List<FlowFieldParticle> _particles;
	public List <MeshRenderer> _particleMeshRenderer;
	public float _spawnRadius;
	public float _particleScale, _particleMoveSpeed, _particleRotateSpeed;




	public bool _particleSpawnValidation (Vector3 position)
	{
		bool valid = true;
		foreach(FlowFieldParticle particle in _particles)
		{
			if (Vector3.Distance (position, particle.transform.transform.position) < _spawnRadius)
			{
				valid = false;
				break;
			}
		}
		if (valid) {
			return true;
		
		} else {
			return false;
		}

	}


	// Use this for initialization
	void Awake () {
		_flowfieldDirection = new Vector3 [_gridSize.x, _gridSize.y, _gridSize.z];
		_fastNoise = new FastNoise();
		_particles = new List <FlowFieldParticle>();
		_particleMeshRenderer = new List <MeshRenderer>();
		for(int i = 0; i < _particleAmount; i++)
			{
				int attempt = 0;

				while (attempt < 100)
				{
					Vector3 randomPos = new Vector3(
						Random.Range(this.transform.position.x, this.transform.position.x + (_gridSize.x * cubesize)),
						Random.Range(this.transform.position.y, this.transform.position.y + (_gridSize.y * cubesize)),
						Random.Range(this.transform.position.z, this.transform.position.z + (_gridSize.z * cubesize))
					
					);

					bool isValid = _particleSpawnValidation(randomPos);

					if (isValid)
					{
						GameObject particleInstance = (GameObject)Instantiate(_particlePrefab);
						particleInstance.transform.position = randomPos;
						particleInstance.transform.parent = this.transform;
						particleInstance.transform.localScale = new Vector3 (_particleScale,_particleScale,_particleScale);
						_particles.Add (particleInstance.GetComponent<FlowFieldParticle>());
						_particleMeshRenderer.Add(particleInstance.GetComponent<MeshRenderer>());
						Debug.Log(_particleMeshRenderer);
						break;	
					} 

					if (!isValid)
					{
						attempt ++;
					}
				}
			}

			

	}
	
	// Update is called once per frame
	void Update () {
		CalculateFlowFieldDirections();
		ParticleBehaviour();
	}

	void CalculateFlowFieldDirections()
	{

		_offset = new Vector3(_offset.x + (_offsetSpeed.x * Time.deltaTime), _offset.y + (_offsetSpeed.y * Time.deltaTime), _offset.z + (_offsetSpeed.z * Time.deltaTime));

	_fastNoise = new FastNoise();

		float xoff = 0;
		for(int x = 0; x < _gridSize.x; x++)
		{
			float yoff = 0;
			for(int y = 0; y < _gridSize.y; y++)
			{
				float zoff = 0;
				for(int z = 0; z < _gridSize.z; z++)
				{
					float noise = _fastNoise.GetSimplex(xoff + _offset.x, yoff + _offset.y, zoff + _offset.z) + 1;
					Vector3 _noiseDirection = new Vector3(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI), Mathf.Cos(noise * Mathf.PI));
					_flowfieldDirection[x,y,z] = Vector3.Normalize(_noiseDirection);

					zoff += _increment;
				}

				yoff += _increment;
			}

			xoff += _increment;
		}
	}

	void ParticleBehaviour()
	{
		foreach(FlowFieldParticle p in _particles)
		{

			//x edges
			if (p.transform.position.x > this.transform.position.x + (_gridSize.x * cubesize))
			{
				p.transform.position = new Vector3(this.transform.position.x, p.transform.position.y, p.transform.position.z);
			}

			if (p.transform.position.x < this.transform.position.x)
			{
				p.transform.position = new Vector3(this.transform.position.x + (_gridSize.x * cubesize), p.transform.position.y, p.transform.position.z);
			}


			//y edges
			if (p.transform.position.y > this.transform.position.y + (_gridSize.y * cubesize))
			{
				p.transform.position = new Vector3(p.transform.position.x, this.transform.position.y, p.transform.position.z);
			}

			if (p.transform.position.y < this.transform.position.y)
			{
				p.transform.position = new Vector3(p.transform.position.x, this.transform.position.y  + (_gridSize.y * cubesize), p.transform.position.z);
			}


			//z edges
			if (p.transform.position.z > this.transform.position.z + (_gridSize.z * cubesize))
			{
				p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, this.transform.position.z);
			}

			if (p.transform.position.z < this.transform.position.z)
			{
				p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, this.transform.position.z  + (_gridSize.z * cubesize));
			}


			Vector3Int particlePos = new Vector3Int (
				Mathf.FloorToInt( Mathf.Clamp((p.transform.position.x - this.transform.position.x) / cubesize, 0, _gridSize.x - 1)),
				Mathf.FloorToInt( Mathf.Clamp((p.transform.position.y - this.transform.position.y) / cubesize, 0, _gridSize.y - 1)),
				Mathf.FloorToInt( Mathf.Clamp((p.transform.position.z - this.transform.position.z) / cubesize, 0, _gridSize.z - 1)));
		p.ApplyRotation(_flowfieldDirection[particlePos.x, particlePos.y, particlePos.z], _particleRotateSpeed);
		p._moveSpeed = _particleMoveSpeed;
		//p.transform.localScale = new Vector3(_particleScale, _particleScale, _particleScale);
		
		}
		

	}

	private void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(this.transform.position + new Vector3((_gridSize.x *cubesize) * 0.5f, (_gridSize.y *cubesize) * 0.5f, (_gridSize.z *cubesize) * 0.5f ),
		new Vector3(_gridSize.x * cubesize, _gridSize.y * cubesize, _gridSize.z * cubesize ) );
	
	}
}
