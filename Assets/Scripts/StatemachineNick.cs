using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatemachineNick
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

	public void Update()
	{
		_currentRunningState.Execute();
	}
}
