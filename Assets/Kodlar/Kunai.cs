using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour 
{
	public float kunaiHiz;

	void Start ()
	{
		Destroy (gameObject, 5f);
	}

	void FixedUpdate () 
	{
		transform.position += new Vector3 (kunaiHiz * transform.localScale.x * Time.fixedDeltaTime, 0, 0);
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "zemin" || other.gameObject.tag == "kutuGitright" || other.gameObject.tag == "kutuGitleft")
		{
			Destroy (this.gameObject);
		}
	}
}
