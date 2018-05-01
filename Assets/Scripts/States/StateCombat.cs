using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCombat : IState
{
	private bool LevelingOut;
	private Vector3 randomVector3 = Vector3.zero;
	public StateCombat(Ship owningShip) : base(owningShip)
	{
	}

	public override void Enter()
	{
		LevelOutSpeed();
	}

	public override void Exit()
	{	
	}

	public override void Execute()
	{
		if (LevelingOut)
		{
			LevelOutSpeed();
			return;
		}
		
		//dostuff
		
		
		RCS();
	}

	

	void RCS()
	{
		GotoLoc();
	}

	void GotoLoc()
	{
		if ( randomVector3 == Vector3.zero)
		{
			randomVector3 = MyShip.transform.position + new Vector3(Random.Range(-20,20),Random.Range(-20,20),Random.Range(-20,20));
		}
		else if ((MyShip.transform.position - randomVector3).magnitude < 3)
		{
			randomVector3 = MyShip.transform.position + new Vector3(Random.Range(-20,20),Random.Range(-20,20),Random.Range(-20,20));
		}

		Vector3 toTarget = randomVector3 - MyShip.transform.position;
		float distance = toTarget.magnitude;
		
		MyShip.DesiredVector3 = toTarget / (distance);
		MyShip.SteeringVector3 = MyShip.DesiredVector3 - MyShip.CurrentVelocity;
		
		float angle = Vector3.Angle(MyShip.CurrentVelocity, MyShip.SteeringVector3.normalized);
		MyShip.RcsPower = Mathf.Min(MyShip.SteeringVector3.magnitude * angle, 3);
		
		float actualForce = MyShip.RcsPower * MyShip.ForceNeededForOneG;
		MyShip.RcsAcceleration = (actualForce / MyShip.ShipMass);
		
		Vector3 accelerationVector3 = MyShip.RcsAcceleration * MyShip.SteeringVector3;
		
		MyShip.CurrentVelocity += accelerationVector3 * Time.deltaTime;
		
		RotatetoTarget();
		
	}
	
	
	
	
	void LevelOutSpeed()
	{
		if (!LevelingOut)
			LevelingOut = true;

		Vector3 slowdown = MyShip.CurrentVelocity * -1;
		MyShip.SteeringVector3 = slowdown.normalized;
		RotateToSteering();
		
		float angle = Vector3.Angle(MyShip.transform.forward, MyShip.SteeringVector3.normalized);
		if (angle < 3)
		{
			MyShip.MainEnginePower = MyShip.GForceLimit;
			float actualForce = MyShip.MainEnginePower * MyShip.ForceNeededForOneG;
			MyShip.MainDriveAcceleration = actualForce / MyShip.ShipMass;
		
			Vector3 accelerationVector3 = MyShip.MainDriveAcceleration * MyShip.transform.forward;
		
			MyShip.CurrentVelocity += accelerationVector3 * Time.deltaTime;
		}
		
		if (MyShip.CurrentVelocity.magnitude < 0.3f)
		{
			MyShip.SteeringVector3 = MyShip.CurrentVelocity;
			LevelingOut = false;
		}
	}
	
	void RotateToSteering()
	{
		MyShip.RotQuat = Quaternion.LookRotation(MyShip.SteeringVector3);
		MyShip.transform.rotation = Quaternion.Slerp(MyShip.transform.rotation, MyShip.RotQuat, MyShip.RotationSpeed);
	}

	void RotatetoTarget()
	{
		MyShip.RotQuat = Quaternion.LookRotation(MyShip.PrimaryTargetObject.transform.position - MyShip.transform.position);
		MyShip.transform.rotation = Quaternion.Slerp(MyShip.transform.rotation, MyShip.RotQuat, MyShip.RotationSpeed);
	}
	
	
}
