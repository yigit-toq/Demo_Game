using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Kontrol : MonoBehaviour {

	private float x_hareket;
	private float z_hareket;

	public bool zeminde;
	public bool zipla;

	public float Hiz;
	public float Damp;
	[Range (1,20)]
	public float rotationSpeed;
	public float ziplamaKuvveti;

	public Transform Karakter;
	public Transform hitNokta;

	public LayerMask zeminLayer;

	public Rigidbody FlashRigidbody { get; set;}
	public Animator FlashAnimator { get; set;}

	private Camera mainCamera;
	private Vector3 StickDirection;
	private RaycastHit ziplaHit;

	void Start () 
	{
		FlashRigidbody = GetComponent<Rigidbody> ();
		FlashAnimator = GetComponent<Animator> ();

		mainCamera = Camera.main;

		zipla = false;
	}

	void LateUpdate()
	{
		x_hareket = Input.GetAxis("Horizontal");
		z_hareket = Input.GetAxis("Vertical");

		StickDirection = new Vector3 (x_hareket, 0, z_hareket).normalized;

		InputMove ();
		InputRotation ();
	}

	void Update ()
	{
		if (this.FlashAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Punching")) {
			FlashAnimator.speed = 5f;
			Hiz = 50f;
		}
		else
		{
			FlashAnimator.speed = 1f;
			Hiz = 200f;
		}
	}

	void FixedUpdate ()
	{
		Zipla_Mekanik ();

		if (Input.GetKeyDown(KeyCode.Space) && zeminde)
		{
			FlashRigidbody.velocity = new Vector3 (0,ziplamaKuvveti,0);
		}

		Kontroller ();
	}

	void InputMove()
	{
		FlashAnimator.SetFloat ("Hiz", Vector3.ClampMagnitude(StickDirection, 1).magnitude, Damp, Time.deltaTime * 50);

		if (x_hareket > 0 || z_hareket > 0)
		{
			transform.position += transform.forward * Time.deltaTime * Hiz;
		}

		if (x_hareket < 0 || z_hareket < 0)
		{
			transform.position += transform.forward * Time.deltaTime * Hiz;
		}
	}

	void InputRotation()
	{
		Vector3 rotationOfset = mainCamera.transform.TransformDirection (StickDirection);
		rotationOfset.y = 0;

		Karakter.forward = Vector3.Slerp (Karakter.forward, rotationOfset, Time.deltaTime * rotationSpeed);
	}

	void Zipla_Mekanik ()
	{
		if (Physics.Raycast (hitNokta.position, hitNokta.TransformDirection (Vector3.down), 2f, zeminLayer)) 
	    {
			zeminde = true;
		}
		else
		{
			zeminde = false;
		}
	}

	void Kontroller ()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			FlashAnimator.SetTrigger ("Atak");
		}
	}
}
