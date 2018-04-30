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
	private const float OneG = .0098f;
	public GameObject Sphere;
	public bool HasPeople = false;

	[Header("StateMachines")]
	public StatemachineNick MyMovementMachine;
	public StatemachineNick MyCombatMachine;
	public StatemachineNick MyTargetingMachine;
	
	public float ShipMass = 240.0f; // the mass of the ship
	public float ForceNeededForOneG = 2.352f; 
	
	[Header("Main Drive Values")]
	public float MainEnginePower = 1.0f; // 1 = 1G of acceleration
	public float MainDriveAcceleration = 0.0f;
	public float MaxGForceAllowed = 100.0f;

	[Header("RCS Values")] 
	public float RcsPower = 1.0f;
	public float RcsAcceleration = 0.0f;
	public Vector3 RcsVector3 = Vector3.zero;
	
	
	[Header("My Vector Values")]
	public Vector3 CurrentVelocity = Vector3.zero;
	public Vector3 CurrentRotation = Vector3.zero;
	public Quaternion RotQuat = Quaternion.identity;
	public Vector3 DesiredVector3 = Vector3.zero;
	public Vector3 SteeringVector3 = Vector3.zero;
	public float Speed = 0.0f;
	
	[Header("Targets")]
	public Vector3 TargetPosition = Vector3.zero;
	public Vector3 TargetVelocity = Vector3.zero;
	public GameObject PrimaryTargetObject;
	public List<GameObject> TargetObjects;
	

	private float _wanderTimer = 0.0f;
	
	#endregion
	
	// Use this for initialization
	void Start ()
	{
		MyMovementMachine = new StatemachineNick();
		MyMovementMachine.ChangeState(new StateReach(this));
	}
	
	// Update is called once per frame
	void Update ()
	{
		FindRandomTarget();
		MyMovementMachine.Update();
		//CalcForwardVelocity();
		//Traversal();
	}
	
	private void ControlShip()
	{
		if (Input.GetKey(KeyCode.W))
		{
			MainEnginePower += 0.5f * Time.deltaTime;
			MainEnginePower = Mathf.Min(MainEnginePower, MaxGForceAllowed);
		}

		if (Input.GetKey(KeyCode.S))
		{
			MainEnginePower -= 0.5f * Time.deltaTime;
			MainEnginePower = Mathf.Max(MainEnginePower, 0.0f);
		}
	}
	
	void FindRandomTarget()
	{
		if (Time.time < _wanderTimer) return;
		
		TargetPosition = new Vector3(Random.Range(-400,400),Random.Range(-400,400),Random.Range(-400,400));

		_wanderTimer = Time.time + Random.Range(10.0f, 25.0f);
		Sphere.transform.position = TargetPosition;
	}

	void PanToTarget()
	{
		
		CurrentRotation = Vector3.RotateTowards(CurrentRotation, SteeringVector3, 0.05f, 1);
	}
	
	void CalcForwardVelocity()
	{
		
		float actualForce = MainEnginePower * ForceNeededForOneG;
		MainDriveAcceleration = (actualForce / ShipMass);
		
		Vector3 accelerationVector3 = MainDriveAcceleration * transform.forward;
		
		CurrentVelocity += accelerationVector3 * Time.deltaTime;
	}

	void CalcRCS()
	{
		float actualForce = RcsPower * ForceNeededForOneG;
		RcsAcceleration = actualForce / ShipMass;
		
		Vector3 accelerationVector3 = RcsAcceleration * RcsVector3;

		CurrentVelocity += accelerationVector3 * Time.deltaTime;
	}
	
	void Traversal()
	{
		transform.rotation = RotQuat;
		//transform.forward += CurrentRotation;
		transform.position += CurrentVelocity;
	}

	
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + (DesiredVector3 * 100));
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, transform.position + (SteeringVector3 * 100));
		
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + (CurrentVelocity * 100));
		
		
	}
	
}
