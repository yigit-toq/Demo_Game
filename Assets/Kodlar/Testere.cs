using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testere : MonoBehaviour {

	void FixedUpdate () 
	{
		transform.eulerAngles += new Vector3 (0, 0, 3);
	}
}
