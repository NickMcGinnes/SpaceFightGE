using UnityEngine;

public class StateIdle : IState {

	// Use this for initialization
	public StateIdle(Ship owningShip) : base(owningShip)
	{
	}

	public override void Enter()
	{
		Debug.Log("entered Idle");
	}

	public override void Exit()
	{
		Debug.Log("leaving Idle");
	}

	public override void Execute()
	{
		Debug.Log("Idling");
	}
}
