using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour {

	public Transform Karakter;

	void LateUpdate () 
	{
		transform.position = Karakter.position;
	}
}
