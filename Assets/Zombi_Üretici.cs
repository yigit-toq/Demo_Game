using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_Üretici : MonoBehaviour {

	public GameObject zombi_model;

	public float olusmaSüre;
	public float süre;

	public bool olus;

	void Start () 
	{
		olus = false;
		süre = 1000f;
	}

	void Update () 
	{
		if (olusmaSüre < süre) 
		{
			olusmaSüre = olusmaSüre + Random.Range (0.2f, 2f);
			olus = false;
		}
		else
		{
			float random_x = Random.Range (130,170);
			float random_z = Random.Range (160,190);
			olus = true;
			olusmaSüre = 0f;
			transform.position = new Vector3 (random_x,2,random_z);
			süre = Random.Range (500f,2000f);
		}

		if(olus)
		{
			Instantiate (zombi_model,transform.position,Quaternion.identity);
		}
	}
}
