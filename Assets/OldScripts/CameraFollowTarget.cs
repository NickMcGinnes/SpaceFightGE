using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
	public GameObject TargetGameObject;

	private Vector3 _offSetVector3;
	// Use this for initialization
	void Start ()
	{
		_offSetVector3 = transform.position - TargetGameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = TargetGameObject.transform.position + _offSetVector3;
	}
}
