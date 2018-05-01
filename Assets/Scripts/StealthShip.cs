using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthShip : Ship
{

	public Path myPatrolPath;
	
	
	// Use this for initialization
	public override void Start ()
	{
		base.Start();
		MyMovementMachine.ChangeState(new StatePatrol(this, myPatrolPath));
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();
	}
	
	
}
