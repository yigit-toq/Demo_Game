using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour {

	public bool Zeminde;

	public float x { get; set; }
	public float z { get; set; }

	public float Hiz;
	public float Damp;
	public float yerCekimi;
	public float ziplamaHizi;

	[Range (1,20)]
	public float rotationSpeed;

	public Transform karakter;

	public Animator MyAnimator { get; set; }
	public Rigidbody MyRigidbody { get; set; }

	private Camera mainCamera;
	private Vector3 StickDirection;
	private Vector3 Hareket;

	void Start () 
	{
		MyAnimator = GetComponent<Animator> ();
		MyRigidbody = GetComponent<Rigidbody> ();

		mainCamera = Camera.main;

		Hareket = Vector3.zero;
	}

	void Update()
    {
		x = Input.GetAxis("Horizontal");
		z = Input.GetAxis("Vertical");

        if (Zeminde)
        {
			Hareket = transform.right * x + transform.forward * z;

			if (Input.GetButton("Jump"))
			{
				Hareket.y = ziplamaHizi;
			}
		}

		Hareket.y -= yerCekimi * Time.deltaTime;

		MyRigidbody.velocity = Hareket * Hiz;
	}
		
	void LateUpdate() 
	{
		StickDirection = new Vector3 (x, 0, z).normalized;

		InputMove ();
		InputRotation ();
	}

	void InputMove ()
	{
		MyAnimator.SetFloat ("Hiz", Vector3.ClampMagnitude(StickDirection, 1).magnitude, Damp, Time.deltaTime * 10);
	}

	
	void InputRotation ()
	{
		Vector3 rotOfset = mainCamera.transform.TransformDirection (StickDirection);
		rotOfset.y = 0;

		karakter.forward = Vector3.Slerp (karakter.forward, rotOfset, Time.deltaTime * rotationSpeed);
	}

	void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Zemin")
        {
			Zeminde = true;
        }
    }

	void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Zemin")
        {
			Zeminde = false;
        }
    }
}
