public abstract class IState 
{
	protected Ship MyShip;

	protected IState(Ship owningShip)
	{
		this.MyShip = owningShip;
	}
	
	public abstract void Enter();
	public abstract void Exit();
	public abstract void Execute();

	

}
