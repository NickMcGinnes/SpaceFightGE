using UnityEngine;

public class StateSimpleMove : IState {

	public StateSimpleMove(Ship owningShip) : base(owningShip)
	{
	}

	public override void Enter()
	{
	}

	public override void Exit()
	{	
	}

	public override void Execute()
	{
		MoveForward();
	}

	void MoveForward()
	{
		MyShip.CurrentVelocity += MyShip.transform.forward *0.1f *Time.deltaTime;
	}
}
