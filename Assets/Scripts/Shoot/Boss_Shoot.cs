using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot : MonoBehaviour {

	[SerializeField]
	public GameObject bullet;
	public Transform fp;
	float fireRate;
	float nextFire;

	Boss_Movement bm;


	// Use this for initialization
	void Start () {
		fireRate = 0.7f;
		nextFire = Time.time;
		bm = gameObject.GetComponent<Boss_Movement>();

	}



	
	// Update is called once per frame
	void Update () {
		CheckIfTimeToFire ();
	}

	void CheckIfTimeToFire()
	{
		if (Time.time > nextFire) {
			float angle = bm.moveRight ? 0f : 180f;
			Instantiate(bullet, fp.position,Quaternion.Euler(new Vector3(0f, 0f, angle)));
			nextFire = Time.time + fireRate;
		}
		
	}

}
