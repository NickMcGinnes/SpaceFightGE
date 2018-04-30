using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class StateReach : IState {

	public StateReach(Ship owningShip) : base(owningShip)
	{
		MyShip = owningShip;
	}

	public override void Enter()
	{
	}

	public override void Exit()
	{
	}

	public override void Execute()
	{
		
		Rotate();
		ReachTarget();
	}

	void PredictTargetLocation()
	{
		//Vector3 tposVector3 = MyShip.PrimaryTargetObject.transform.position;
		Vector3 tposVector3 = MyShip.TargetPosition;
		Vector3 toTarget = tposVector3 - MyShip.transform.position;

		float timer = (toTarget.magnitude/ MyShip.CurrentVelocity.magnitude);
		
		//Debug.Log(timer);
		
		
	}
	
	public void ReachTarget()
	{
		Vector3 toTarget = MyShip.TargetPosition - MyShip.transform.position;
		float distance = toTarget.magnitude;
		
		MyShip.DesiredVector3 = toTarget / distance;
		MyShip.SteeringVector3 = MyShip.DesiredVector3 - MyShip.CurrentVelocity;
		
		float actualForce = MyShip.MainEnginePower * MyShip.ForceNeededForOneG;
		MyShip.MainDriveAcceleration = (actualForce / MyShip.ShipMass);
		
		Vector3 accelerationVector3 = MyShip.MainDriveAcceleration * MyShip.transform.forward;
		
		MyShip.CurrentVelocity += accelerationVector3 * Time.deltaTime;
		
		MyShip.transform.position += MyShip.CurrentVelocity;
		
	}

	public void Rotate()
	{
		Vector3 cross = Vector3.Cross(MyShip.transform.forward, MyShip.SteeringVector3.normalized);
		float angle = Vector3.Angle(MyShip.transform.forward, MyShip.SteeringVector3.normalized);
		Quaternion q = Quaternion.Euler(cross);
		
		Debug.Log(cross);
		
		
		//Debug.Log(q);
		
		MyShip.transform.rotation *= q;
		
		if (angle < 1)
		{
			MyShip.MainEnginePower = 3;
		}
		else
		{
			
			MyShip.MainEnginePower = 0;
		}
		
	}


	void SimpleRotate()
	{
		Vector3 x = MyShip.SteeringVector3.normalized;

		MyShip.transform.forward = x;
		
		float angle = Vector3.Angle(MyShip.transform.forward, MyShip.SteeringVector3.normalized);
		Debug.Log(angle);
	}
}
