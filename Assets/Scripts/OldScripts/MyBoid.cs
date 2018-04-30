using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using Random = UnityEngine.Random;

public class MyBoid : MonoBehaviour
{

	#region Variables
	
	public Vector3 MyPosition;
	public Vector3 Target;
	public Vector3 CurrentVelocity;
	public Vector3 DesiredVelocity;
	public Vector3 SteeringForce;

	
	public float Mass = 50;
	public float MaxForce = 30;
	public float MaxSpeed = 20;
	public float SlowingRadius = 500;

	private float _wanderTimer = 0.0f;
	private float _circleRadius = 10.0f;

	private Vector3 _circlePos;
	private float _angle = 60;
	private float _angleChange = 30;
	
	#endregion
	
	void Update ()
	{	
		GetRandomTarget();
		SeekToTarget();

		PersueTarget();
		
		transform.position = MyPosition + CurrentVelocity * Time.deltaTime;
		
		if (CurrentVelocity.magnitude > float.Epsilon) {
			transform.forward = SteeringForce;
		}
	}

	void PersueTarget()
	{
		
	}

	void SeekToTarget()
	{
		//check distance to target and leave if close enough
		// not the cleanest setup to jump in the method and return but keeps update clean while I learn
		if ((Target - transform.position).magnitude < 0.05)
		{
			transform.position = Target;
			CurrentVelocity = Vector3.zero;
			return;
		}
		
		//get vector to target
		MyPosition = transform.position;
		Vector3 toTarget = Target - MyPosition;
		
		// get desired Velocity Vector
		float distance = toTarget.magnitude;
		float ramped = MaxSpeed * (distance / SlowingRadius);
		float clamped = Mathf.Min(ramped, MaxSpeed);
		DesiredVelocity = clamped * (toTarget / distance);

		//apply Desired Velocity to Steering force
		SteeringForce = DesiredVelocity - CurrentVelocity;
		SteeringForce = Vector3.ClampMagnitude(SteeringForce, MaxForce);
		SteeringForce = SteeringForce / Mass;
		
		//add steering force to Current Velocity
		CurrentVelocity = Vector3.ClampMagnitude(CurrentVelocity + SteeringForce, MaxSpeed);
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

	Vector3 CircleWander()
	{
		//get Circle Location from Current Velocity
		_circlePos = CurrentVelocity.normalized;
		_circlePos = transform.position + (_circlePos * _circleRadius);

		//set up direction for random force
		Vector3 displacementVector3 = Vector3.forward;

		displacementVector3 *= _circleRadius;
		
		SetAngle(displacementVector3, _angle);

		_angle += (Random.value * _angleChange) - (_angleChange * 0.5f);

		Vector3 wanderForce = displacementVector3 + _circlePos;
		return wanderForce;
	}

	void SetAngle(Vector3 vec, float num)
	{
		float mag = vec.magnitude;
		vec.x = (float)Math.Cos(num) * mag;
		vec.y = (float)Math.Sin(num) * mag;
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

	void GetRandomTarget()
	{
		if (Time.time < _wanderTimer) return;
		
		Target = new Vector3(Random.Range(-50,50),0.0f,Random.Range(-50,50));

		_wanderTimer = Time.time + Random.Range(1.0f, 7.0f);
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + (DesiredVelocity * 5));
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (transform.position, transform.position + (SteeringForce * 100));
		
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + (CurrentVelocity * 5));
		
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(Target,_circleRadius);
		
	}
}
