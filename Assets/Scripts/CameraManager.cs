using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public GameObject cam;
	public GameObject tacCam;
	public List<GameObject> objects;
	public int index = 0;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0))
		{
			index++;
			if (index == objects.Count)
				index = 0;
		}

		if (Input.GetKeyDown(KeyCode.Space))
			tacCam.SetActive(! tacCam.activeSelf);
		
		cam.transform.position = objects[index].transform.position;
	}
}
