using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Timeline;

public class Ship : MonoBehaviour
{


	//public List<Engine> MyEngines;
	public GameObject sphere;
	public bool HasPeople = false;
	
	public float EnginePower = 1.0f; // 1 should equal 1g of acceleration at 0.1 scale
	
	public float MaxEngineForce = 6000.0f; // multiplied by 1000 later
	public float ShipMass = 2400000.0f; // the mass of the ship

	public float Acceleration = 0.0f;
	public float MaxGForce = 100.0f;
	private const float OneG = 0.98f;

	public Vector3 CurrentVelocity = Vector3.zero;
	public Vector3 Target = Vector3.zero;

	private Vector3 _currentRotation = Vector3.zero;

	private float _wanderTimer = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
		if (HasPeople)
			MaxGForce = 6.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ControlShip();
		FindTarget();
		PanToTarget();
		Accel();	
	}
	
	void FindTarget()
	{
		if (Time.time < _wanderTimer) return;
		
		Target = new Vector3(Random.Range(-400,400),Random.Range(-400,400),Random.Range(-400,400));

		_wanderTimer = Time.time + Random.Range(1.0f, 7.0f);
		sphere.transform.position = Target;
	}

	void PanToTarget()
	{

		Vector3 v3ToTarget = (Target - transform.position).normalized;
		Vector3 myLook = transform.forward;
		Vector3 desiredVector3 = v3ToTarget;
		
		print(Vector3.Angle(myLook, desiredVector3) - 90);
		
		_currentRotation += desiredVector3 * Mathf.Min(Vector3.Angle(myLook,desiredVector3) - 90.0f,0.25f)* Time.deltaTime;
		
		myLook += _currentRotation;

		transform.forward = myLook;
	}

	void Accel()
	{
		
		float actualForce = EnginePower * (MaxEngineForce *100);
		Acceleration = (actualForce / ShipMass);
		
		Vector3 accelerationVector3 = Acceleration * transform.forward;
		
		CurrentVelocity += accelerationVector3 * Time.deltaTime;

		transform.position += CurrentVelocity;
	}
	
	private void ControlShip()
	{
		if (Input.GetKey(KeyCode.W))
		{
			EnginePower += 0.5f * Time.deltaTime;
			EnginePower = Mathf.Min(EnginePower, MaxGForce);
		}

		if (Input.GetKey(KeyCode.S))
		{
			EnginePower -= 0.5f * Time.deltaTime;
			EnginePower = Mathf.Max(EnginePower, 0.0f);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(Target, 30.0f);
	}
}
