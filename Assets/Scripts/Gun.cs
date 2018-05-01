using UnityEngine;

public class Gun : MonoBehaviour
{

	public GameObject bullet;
	public GameObject spawnPoint;
	public bool canFire;
	public Vector3 currentVelocity = Vector3.zero;

	private void Start()
	{
			InvokeRepeating("Fire",0.0f,0.05f); 
	}

	void Fire()
	{
		if (!canFire) return;
		var mybull = Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
		mybull.GetComponent<Rigidbody>().AddForce((spawnPoint.transform.forward * 70) + currentVelocity, ForceMode.VelocityChange);
	}

	public void SetVel(Vector3 newVel)
	{
		currentVelocity = newVel;
	}
}
