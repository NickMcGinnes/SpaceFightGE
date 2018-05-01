public class MyStateMachine
{
	//private List<IState> _listStates;
	private IState _currentRunningState;
	private IState _prevoiusState;
	
	public void ChangeState(IState myNewState)
	{
		if (_currentRunningState != null)
		{
			_currentRunningState.Exit();
		}

		_currentRunningState = myNewState;

		if (_currentRunningState != null)
		{
			_currentRunningState.Enter();
		}
	}
	
	public IState GetCurrentState()
	{
		return _currentRunningState;
	}

	public void Update()
	{
		if (_currentRunningState != null)
		_currentRunningState.Execute();
	}
}
