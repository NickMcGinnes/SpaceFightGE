using UnityEngine;

public class CameraMovement : MonoBehaviour
{

	public GameObject YSpinner;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(0,Input.GetAxis("Mouse X"),0);
		YSpinner.transform.Rotate(Input.GetAxis("Mouse Y"),0,0);
	}
}
