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
		ReachTarget();
		Rotate();
		//SimpleRotate();
	}
	
	public void ReachTarget()
	{
		Vector3 toTarget = MyShip.TargetPosition - MyShip.transform.position;
		float distance = toTarget.magnitude;
		
		MyShip.DesiredVector3 = toTarget / distance;
		MyShip.SteeringVector3 = MyShip.DesiredVector3 - MyShip.CurrentVelocity;
		
		float angle = Vector3.Angle(MyShip.CurrentVelocity, MyShip.SteeringVector3.normalized);
		MyShip.MainEnginePower = Mathf.Min(MyShip.SteeringVector3.magnitude * angle,MyShip.GForceLimit);
		
		float actualForce = MyShip.MainEnginePower * MyShip.ForceNeededForOneG;
		MyShip.MainDriveAcceleration = (actualForce / MyShip.ShipMass);
		
		Vector3 accelerationVector3 = MyShip.MainDriveAcceleration * MyShip.transform.forward;
		
		MyShip.CurrentVelocity += accelerationVector3 * Time.deltaTime;
		
		MyShip.transform.position += MyShip.CurrentVelocity;
		
	}

	public void Rotate()
	{
		Quaternion myLookRot = Quaternion.LookRotation(MyShip.SteeringVector3);
		MyShip.transform.rotation = Quaternion.Slerp(MyShip.transform.rotation, myLookRot, MyShip.RotationSpeed);
	}


	void SimpleRotate()
	{
		Vector3 x = MyShip.SteeringVector3.normalized;

		MyShip.transform.forward = x;
		
		float angle = Vector3.Angle(MyShip.transform.forward, MyShip.SteeringVector3.normalized);
		Debug.Log(angle);
	}
}
