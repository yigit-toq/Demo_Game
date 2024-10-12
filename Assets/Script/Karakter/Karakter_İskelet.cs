using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Karakter_İskelet : MonoBehaviour {

	private static Karakter_İskelet karakter;
	public static Karakter_İskelet karakterCode
	{
		get 
		{
			if(karakter == null) 
			{
				karakter = GameObject.FindObjectOfType<Karakter_İskelet> ();
			}
			return karakter;
		}
	}

	public Karakter_Fonksiyon karakterFonksiyon { get; set; }

	public bool Atak;
	public bool SilahAtak;
	public bool Reload;

	public float Hiz;
	public float okYön;

	public float ElimizdekiMermi;
	public float ToplamMermi;
	public float GerekliMermi;
	public float SabitMermi;

	public Text elimizdekiMermiText;
	public Text toplamMermiText;

	public Joystick joystick;

	public Transform okCikis;

	public GameObject Ok;

	public Animator karakterAnim { get; set; }

	public Rigidbody2D karakterRigid { get; set; }

	public AudioSource karakterSource { get; set; }

	public Vector2 Hareket;

	void Start () 
	{
		karakterFonksiyon = FindObjectOfType<Karakter_Fonksiyon> ();

		karakterAnim = GetComponent<Animator> ();
		karakterRigid = GetComponent<Rigidbody2D> ();
		karakterSource = GetComponent<AudioSource> ();

		Reload = false;

		ElimizdekiMermi = 10;
		SabitMermi = 10;
		ToplamMermi = 90;
		GerekliMermi = 0;

		elimizdekiMermiText.text = ElimizdekiMermi.ToString ();
		toplamMermiText.text = ToplamMermi.ToString ();

		okYön = 180f;
	}

	void Update ()
	{
		//Hareket = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		Hareket = new Vector2 (joystick.Horizontal, joystick.Vertical);

		karakterAnim.SetFloat ("Horizontal",Hareket.x);
	    karakterAnim.SetFloat ("Vertical",Hareket.y);
		karakterAnim.SetFloat ("Hiz",Hareket.sqrMagnitude);

		if (Hareket.x > 0.5f) 
		{
			karakterAnim.SetFloat ("Yon",2f);
			okYön = 270f;
		}
		else if (Hareket.x < -0.5f)
		{
			karakterAnim.SetFloat ("Yon",-2f);
			okYön = 90f;
		}

		if (Hareket.y > 0.5f) 
		{
			karakterAnim.SetFloat ("Yon",1f);
			okYön = 0f;
		}
		else if (Hareket.y < -0.5f)
		{
			karakterAnim.SetFloat ("Yon",-1f);
			okYön = 180f;
		}

		if (Mathf.Abs(Hareket.x) + Mathf.Abs(Hareket.y) == 2f)
		{
			Hiz = 3f;
		} 
		else
		{
			Hiz = 6f;
		}

		//Kontroller ();

		if (ToplamMermi <= 0)
		{
			toplamMermiText.text = ("000");
		}

		if (ElimizdekiMermi <= 0)
		{
			elimizdekiMermiText.text = ("00");
		}
	}

	void FixedUpdate ()
	{
		if (!Atak && !SilahAtak) 
		{
			karakterRigid.MovePosition (karakterRigid.position + Hareket * Hiz * Time.fixedDeltaTime);
		}
	}

	//void Kontroller () 
	//{
		//if (Input.GetMouseButtonDown(1) && !Atak)
		//{
		//	karakterAnim.SetTrigger ("Atak");
		//}

		//if (Input.GetMouseButtonDown(0) && !SilahAtak)
		//{
		//	karakterAnim.SetTrigger ("SilahAtak");
		//}

		//if (Input.GetKeyDown(KeyCode.R)) 
		//{
		//	if (!Reload && ToplamMermi > 0 && ElimizdekiMermi != SabitMermi && !SilahAtak)
		//	{
		//		Reload = true;
		//		GerekliMermi = SabitMermi - ElimizdekiMermi;
		//
		//		if (GerekliMermi > ToplamMermi) 
		//		{
		//			ElimizdekiMermi += ToplamMermi;
		//			ToplamMermi = 0;
		//			elimizdekiMermiText.text = ElimizdekiMermi.ToString ();
		//			toplamMermiText.text = ToplamMermi.ToString ();
		//		}
		//		else
		//		{
		//			ElimizdekiMermi = SabitMermi;
		//			ToplamMermi -= GerekliMermi;
		//			elimizdekiMermiText.text = ElimizdekiMermi.ToString ();
		//			toplamMermiText.text = ToplamMermi.ToString ();
		//		}
		//
		//		Reload = false;
		//	}
		//}
	//}

	public void Atak_Yapma () 
	{
		if (!Atak)
		{
			karakterAnim.SetTrigger ("Atak");
		}
	}

	public void SilahAtak_Yapma ()
	{
		if (!SilahAtak && ElimizdekiMermi > 0 && !Reload) 
		{
			karakterAnim.SetTrigger ("SilahAtak");

			ElimizdekiMermi -= 1;
			elimizdekiMermiText.text = ElimizdekiMermi.ToString ();
		}
	}

	public void Ok_Atma ()
	{
		Instantiate (Ok, okCikis.position, Quaternion.identity);
	}

	public void Reloaded ()
	{
		if (!Reload && ToplamMermi > 0 && ElimizdekiMermi != SabitMermi && !SilahAtak)
		{
			Reload = true;
			GerekliMermi = SabitMermi - ElimizdekiMermi;

			if (GerekliMermi > ToplamMermi) 
			{
				ElimizdekiMermi += ToplamMermi;
				ToplamMermi = 0;
				elimizdekiMermiText.text = ElimizdekiMermi.ToString ();
				toplamMermiText.text = ToplamMermi.ToString ();
			}
			else
			{
				ElimizdekiMermi = SabitMermi;
				ToplamMermi -= GerekliMermi;
				elimizdekiMermiText.text = ElimizdekiMermi.ToString ();
				toplamMermiText.text = ToplamMermi.ToString ();
			}

			Reload = false;
		}
	}
}
