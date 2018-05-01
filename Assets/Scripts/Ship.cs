using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Timeline;

public class Ship : MonoBehaviour
{

	#region Variables
	
	[Header("General Values")]
	//public List<Engine> MyEngines;
	protected const float OneG = .0098f;
	public GameObject Sphere;
	public bool HasPeople = false;
	protected const float GforceForPeople = 16;

	[Header("StateMachines")]
	public MyStateMachine MyMovementMachine;
	public MyStateMachine MyCombatMachine;
	public MyStateMachine MyTargetingMachine;
	
	public float ShipMass = 240.0f; // the mass of the ship
	public float ForceNeededForOneG = 2.352f; 
	
	[Header("Main Drive Values")]
	public float MainEnginePower = 1.0f; // 1 = 1G of acceleration
	public float MainDriveAcceleration = 0.0f;
	public float GForceLimit = 100.0f;

	[Header("RCS Values")] 
	public float RcsPower = 1.0f;
	public float RcsAcceleration = 0.0f;
	public Vector3 RcsVector3 = Vector3.zero;
	
	
	[Header("My Vector Values")]
	public Vector3 CurrentVelocity = Vector3.zero;
	public Vector3 CurrentRotation = Vector3.zero;
	public Quaternion RotQuat = Quaternion.identity;
	public Vector3 GoalPosition = Vector3.zero;
	public Vector3 DesiredVector3 = Vector3.zero;
	public Vector3 SteeringVector3 = Vector3.zero;
	public float Speed = 0.0f;
	public float RotationSpeed = 0.1f;

	[Header("Targets")] 
	public LayerMask TargetLayerMask;
	public float ScannerRange = 1000;
	public Vector3 AimPosition;
	public GameObject PrimaryTargetObject;
	public List<GameObject> TargetObjects;
	
	
	

	protected float _findRandomTargetTimer = 0.0f;
	#endregion
	
	// Use this for initialization
	public virtual void Start ()
	{
		if (HasPeople)
			GForceLimit = GforceForPeople;
		
		MakeMachines();
		ForceNeededForOneG = ShipMass * OneG;
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		MyMovementMachine.Update();
		MyCombatMachine.Update();
		MyTargetingMachine.Update();

		transform.position += CurrentVelocity;
	}

	public virtual void MakeMachines()
	{
		MyMovementMachine = new MyStateMachine();
		MyCombatMachine = new MyStateMachine();
		MyTargetingMachine = new MyStateMachine();
	}
	
	public void FindRandomTarget()
	{
		if (Time.time < _findRandomTargetTimer) return;
		
		GoalPosition = new Vector3(Random.Range(-400,400),Random.Range(-400,400),Random.Range(-400,400));

		_findRandomTargetTimer = Time.time + Random.Range(10.0f, 25.0f);
		Sphere.transform.position = GoalPosition;
	}

	
	void CalcRCS() //unfinished
	{
		
		float actualForce = RcsPower * ForceNeededForOneG;
		RcsAcceleration = actualForce / ShipMass;
		
		Vector3 accelerationVector3 = RcsAcceleration * RcsVector3;

		CurrentVelocity += accelerationVector3 * Time.deltaTime;
	}

	
	public virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + (DesiredVector3 * 100));
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, transform.position + (SteeringVector3 * 100));
		
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + (CurrentVelocity * 100));
	}
	
}
