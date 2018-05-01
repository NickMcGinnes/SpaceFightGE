using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("killme",10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void killme()
	{
		Destroy(gameObject);
	}
}
