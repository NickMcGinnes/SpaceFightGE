using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTarget : IState 
{
	public StateTarget(Ship owningShip) : base(owningShip)
	{
	}

	public override void Enter()
	{
		throw new System.NotImplementedException();
	}

	public override void Exit()
	{
		throw new System.NotImplementedException();
	}

	public override void Execute()
	{
		throw new System.NotImplementedException();
	}
	
	
	void PredictTargetLocation()
	{
		//Vector3 tposVector3 = MyShip.PrimaryTargetObject.transform.position;
		Vector3 tposVector3 = MyShip.TargetPosition;
		Vector3 toTarget = tposVector3 - MyShip.transform.position;

		float timer = (toTarget.magnitude/ MyShip.CurrentVelocity.magnitude);
		
		//Debug.Log(timer);
	}
}
