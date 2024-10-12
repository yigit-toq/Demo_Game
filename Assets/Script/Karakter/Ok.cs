using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ok : MonoBehaviour {

	public float okHiz;

	private Rigidbody2D okRigid;

	void Start ()
	{
		okRigid = GetComponent<Rigidbody2D> ();
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Karakter_İskelet.karakterCode.okYön);

		Destroy (gameObject, 3f);
	}

	void FixedUpdate () 
	{
		if (transform.eulerAngles.z == 0f)
		{
			transform.position += new Vector3 (0, okHiz * Time.fixedDeltaTime, 0);
		}

		if (transform.eulerAngles.z == 180f)
		{
			transform.position += new Vector3 (0, okHiz * -1 * Time.fixedDeltaTime, 0);
		}

		if (transform.eulerAngles.z == 90f)
		{
			transform.position += new Vector3 (okHiz * -1 * Time.fixedDeltaTime, 0, 0);
		}

		if (transform.eulerAngles.z == (270f))
		{
			transform.position += new Vector3 (okHiz * Time.fixedDeltaTime, 0, 0);
		}
	}
}
