using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocinante : Ship {

	// Use this for initialization
	public override void Start ()
	{
		base.Start();
		
		MyMovementMachine.ChangeState(new StateReach(this));
		MyTargetingMachine.ChangeState(new StateTarget(this));
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();
		CheckBehaviour();
	}

	void CheckBehaviour()
	{
		if (PrimaryTargetObject == null)
		{
			MyMovementMachine.ChangeState(new StateIdle(this));
			MyCombatMachine.ChangeState(new StateIdle(this));
			return;
		}
		float distanceToEnemy = (PrimaryTargetObject.transform.position - transform.position).magnitude;
		currentMoveState = MyMovementMachine.GetCurrentState();
		if (currentMoveState is StateReach)
		{
			if (distanceToEnemy < CombatRange)
			{
				MyMovementMachine.ChangeState(new StateCombat(this));
				MyCombatMachine.ChangeState(new StateEngage(this));
			}
		}
		else if (currentMoveState is StateCombat)
		{
			if (distanceToEnemy > CombatRange + 100)
			{
				MyMovementMachine.ChangeState(new StateReach(this));
				MyCombatMachine.ChangeState(new StateIdle(this));
			}
		}
		
	}
	
}
