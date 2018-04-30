using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocinante : Ship {

	// Use this for initialization
	public override void Start ()
	{
		base.Start();
		MyMovementMachine.ChangeState(new StateReach(this));
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();
		base.FindRandomTarget();
	}
	
}
