using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kutu : MonoBehaviour {

	void OnCollisionStay2D (Collision2D other)
	{
		if (other.gameObject.tag == "kutuGitleft")
		{
			transform.position -= new Vector3 (3, 0, 0);
		}

		if (other.gameObject.tag == "kutuGitright")
		{
			transform.position += new Vector3 (3, 0, 0);
		}
	}
}
