using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{

	public GameObject[] myguns;
	public GameObject enemy;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var gun in myguns)
		{
			gun.transform.LookAt(enemy.transform);
		}	
	}
}
