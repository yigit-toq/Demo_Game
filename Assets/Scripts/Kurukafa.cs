using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurukafa : MonoBehaviour {

	private static Kurukafa kuruKafa;
	public static Kurukafa kuruKafaCode
	{
		get
		{
			if(kuruKafa == null)
			{
				kuruKafa = FindObjectOfType<Kurukafa> ();
			}
			return kuruKafa;
		}
	}

	public Transform solLimit,sagLimit,rayCast,hedef;
	public LayerMask rayMask;
	public Vector3 yon;
	private RaycastHit2D hit;
	private Animator myAnim;
	private Rigidbody2D myRigid;

	public float hitUzunluk,hareketHizi;
	public bool menzilde;
	public float mesafe;
	public bool atakAcik;
	public bool degiyormu;

	void Awake () {
		degiyormu = false;
		menzilde = false;
		Hedef_Secme ();
		myAnim = GetComponent<Animator> ();
		myRigid = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if(!Sinirler_icinde() && !menzilde && !myAnim.GetCurrentAnimatorStateInfo(0).IsTag("atak"))
		{
			Hedef_Secme ();
		}
		if (menzilde) 
		{
			hit = Physics2D.Raycast (rayCast.position, transform.right, hitUzunluk, rayMask);
			RaycastDebugger ();
		}
		else
		{
			Debug.DrawRay (rayCast.position, transform.right * hitUzunluk, Color.red);
		}
	}

	void FixedUpdate ()
	{
		Hareket ();
		Alevlen ();
	}

	void Alevlen ()
	{
		if (hit.collider != null)
		{
			iskeletMantik ();
			hareketHizi = 4;
			atakAcik = true;
			myAnim.SetBool ("alevlen", true);
		}
		else
		{
			atakAcik = false;
			hareketHizi = 2;
			myAnim.SetBool ("alevlen", false);
		}
	}

	private bool Sinirler_icinde ()
	{
		return transform.position.x > solLimit.position.x && transform.position.x < sagLimit.position.x;
	}

	private void Hedef_Secme () 
	{
		float mesafe_sol = Vector2.Distance (transform.position,solLimit.position);
		float mesafe_sag = Vector2.Distance (transform.position,sagLimit.position);

		if (mesafe_sol > mesafe_sag) {
			hedef = solLimit;
		}
		else
		{
			hedef = sagLimit;
		}
		Yon_Cevirme ();
	}

	void RaycastDebugger ()
	{
		if (menzilde) 
		{
			Debug.DrawRay (rayCast.position, transform.right * hitUzunluk, Color.green);
		}
	}

	void iskeletMantik ()
	{
		mesafe = Vector2.Distance (transform.position, hedef.transform.position);
	}

	void Hareket()
	{
		if (!degiyormu)
		{
			Vector2 hedefPozisyon = new Vector2 (hedef.transform.position.x,transform.position.y);
			transform.position = Vector2.MoveTowards (transform.position,hedefPozisyon,hareketHizi * Time.deltaTime);
		}
	}

	public void Yon_Cevirme ()
	{
		yon = transform.localScale;

		if (transform.position.x > hedef.position.x) 
		{
			yon.x = 2.5f;
		}
		else
		{
			yon.x = -2.5f;
		}
		transform.localScale = yon;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			degiyormu = true;
		}
	}
		
	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			degiyormu = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.tag == "karakterKilic") 
		{
			myAnim.SetTrigger ("hit");
			transform.position += new Vector3 (yon.x * 50 * Time.deltaTime, 0, 0);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if(other.gameObject.tag == "karakterKilic") 
		{
			myAnim.SetTrigger ("bekleme");
			myAnim.ResetTrigger ("hit");
		}
	}
}
