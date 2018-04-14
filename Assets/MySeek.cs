using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySeek : MonoBehaviour
{
	public Vector3 Position;
	public Vector3 Target;
	//public Vector3 Force;
	public Vector3 CurrentVelocity;
	public Vector3 DesiredVelocity;
	public Vector3 SteeringForce;

	
	public float Mass = 5;
	public float MaxForce = 8;
	public float MaxSpeed = 10;

	private Rigidbody _myRigidbody;
	// Use this for initialization
	void Start ()
	{
		_myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetTarget();
		Position = transform.position;


		DesiredVelocity = (Target - Position).normalized * MaxSpeed;
		SteeringForce = DesiredVelocity - CurrentVelocity;

		SteeringForce = Vector3.ClampMagnitude(SteeringForce, MaxForce);
		SteeringForce = SteeringForce / Mass;
		
		CurrentVelocity += SteeringForce;
		transform.position = Position + CurrentVelocity * Time.deltaTime;
		
		
		if (CurrentVelocity.magnitude > float.Epsilon) {
			transform.forward = CurrentVelocity;
		}
	}

	void GetTarget()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			Target = hit.point;
		}
		
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + (DesiredVelocity * 10));
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, transform.position + (SteeringForce * 10));
		
		//current Velocity
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + (CurrentVelocity * 10));
		
	}
}
