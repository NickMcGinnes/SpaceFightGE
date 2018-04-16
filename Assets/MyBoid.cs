using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class MyBoid : MonoBehaviour
{
	public Vector3 MyPosition;
	public Vector3 Target;
	public Vector3 CurrentVelocity;
	public Vector3 DesiredVelocity;
	public Vector3 SteeringForce;

	
	public float Mass = 50;
	public float MaxForce = 30;
	public float MaxSpeed = 20;
	public float SlowingRadius = 500;
	//private Rigidbody _myRigidbody;
	// Use this for initialization
	void Start ()
	{
		//_myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetMousePositionAsTarget();

		if ((transform.position - Target).magnitude > 0.05f)
		{
			SeekToTarget();
		}
		else {
			transform.position = Target;
			CurrentVelocity = Vector3.zero;
		}
		
		
		if (CurrentVelocity.magnitude > float.Epsilon) {
			transform.forward = DesiredVelocity;
		}
	}

	void SeekToTarget()
	{
		MyPosition = transform.position;
		Vector3 toTarget = Target - MyPosition;
		
		
		float distance = toTarget.magnitude;
		float ramped = MaxSpeed * (distance / SlowingRadius);
		float clamped = Mathf.Min(ramped, MaxSpeed);
		
		DesiredVelocity = clamped * (toTarget / distance);
		
		//DesiredVelocity = (Target - myPosition).normalized * MaxSpeed;
		
		SteeringForce = DesiredVelocity - CurrentVelocity;
		
		SteeringForce = Vector3.ClampMagnitude(SteeringForce, MaxForce);
		SteeringForce = SteeringForce / Mass;

		CurrentVelocity = Vector3.ClampMagnitude(CurrentVelocity + SteeringForce, MaxSpeed);
		
		transform.position = MyPosition + CurrentVelocity * Time.deltaTime;
		
		
		
	}

	void FleeFromTarget()
	{
		MyPosition = transform.position;

		Vector3 fromTarget =  MyPosition - Target;
		
		DesiredVelocity = fromTarget.normalized * MaxSpeed;
		
		SteeringForce = DesiredVelocity - CurrentVelocity;
		
		SteeringForce = Vector3.ClampMagnitude(SteeringForce, MaxForce);
		SteeringForce = SteeringForce / Mass;

		CurrentVelocity = Vector3.ClampMagnitude(CurrentVelocity + SteeringForce, MaxSpeed);
		
		transform.position = MyPosition + CurrentVelocity * Time.deltaTime;
	}
	
	
	void GetMousePositionAsTarget()
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
		Gizmos.DrawLine (transform.position, transform.position + (DesiredVelocity * 5));
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, transform.position + (SteeringForce * 100));
		
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + (CurrentVelocity * 5));
		
	}
}
