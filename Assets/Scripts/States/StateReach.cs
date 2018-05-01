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
		SetGoalPosition();
		ReachTarget();
		Rotate();
		RCS();
		//SimpleRotate();
	}

	void SetGoalPosition()
	{
		MyShip.GoalPosition = MyShip.AimPosition;
	}
	
	void ReachTarget()
	{
		Vector3 toTarget = MyShip.GoalPosition - MyShip.transform.position;
		float distance = toTarget.magnitude;
		
		MyShip.DesiredVector3 = toTarget / (distance*0.7f);
		MyShip.SteeringVector3 = MyShip.DesiredVector3 - MyShip.CurrentVelocity;
		
		float angle = Vector3.Angle(MyShip.CurrentVelocity, MyShip.SteeringVector3.normalized);
		MyShip.MainEnginePower = Mathf.Min(MyShip.SteeringVector3.magnitude * angle,MyShip.GForceLimit * 0.5f);
		
		float actualForce = MyShip.MainEnginePower * MyShip.ForceNeededForOneG;
		MyShip.MainDriveAcceleration = (actualForce / MyShip.ShipMass);
		
		Vector3 accelerationVector3 = MyShip.MainDriveAcceleration * MyShip.transform.forward;
		
		MyShip.CurrentVelocity += accelerationVector3 * Time.deltaTime;
		
	}
	
	void RCS()
	{
		Vector3 corrective = MyShip.SteeringVector3 - MyShip.CurrentVelocity;
		float angle = Vector3.Angle(MyShip.CurrentVelocity, MyShip.SteeringVector3);
		MyShip.CurrentVelocity += corrective.normalized * (0.005f * angle) * Time.deltaTime;
	}

	void Rotate()
	{
		
		MyShip.RotQuat = Quaternion.LookRotation(MyShip.SteeringVector3);
		MyShip.transform.rotation = Quaternion.Slerp(MyShip.transform.rotation, MyShip.RotQuat, MyShip.RotationSpeed);
	}


	void SimpleRotate()
	{
		Vector3 x = MyShip.SteeringVector3.normalized;

		MyShip.transform.forward = x;
		
		float angle = Vector3.Angle(MyShip.transform.forward, MyShip.SteeringVector3.normalized);
		Debug.Log(angle);
	}
}
