using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Karakter0 : MonoBehaviour {

	private static Karakter0 karakterOrn;
	public static Karakter0 karakter 
	{
		get{
			if(karakterOrn == null)
			{
				karakterOrn = FindObjectOfType<Karakter0> ();
			}
			return karakterOrn;
		}
	}

	public static BGMusic bgMusic;

	public Rigidbody2D myRigidbody 
	{
		get;
		set;
	}
	public Animator myAnimator 
	{
		get;
		set;
	}

	[Header ("Float")]
	public float hiz;
	public float yatay;
	public float TemasCapi;
	public float ZiplamaKuvveti;

	[Header ("Bool")]
	public bool HavadaKontrol;
	public bool sagaBak;
	public bool zeminde;
	public bool Atak;
	public bool Zipla;
	public bool Kayma;
	public bool kunaiAtak;
	public bool öldün;
	public bool pause;

	[Header ("Maskeler")]
	public LayerMask HangiZemin;

	[Header ("Konumlar")]
	public Transform[] Temas_Noktalari;
	public Transform kunaiMachine;
	public Transform kunaiMachine2;

	private Vector3 kunaiYon;

	[Header ("Objeler")]
	public GameObject bitis_ekrani;
	public GameObject KapiAcik;
	public GameObject Kunai;

	[Header ("Sesler")]
	public AudioSource KapiAcikSes;
	public AudioSource KapiKapaliSes;

	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();

		bgMusic = FindObjectOfType<BGMusic> ();

		bitis_ekrani.SetActive (false);

		bgMusic.hangiSahne = 2;

		sagaBak = true;
		kunaiAtak = false;
		Atak = false;
		öldün = false;
		pause = false;
	}

	//void Update () 
	//{
	//    Kontroller ();
	//}

	void FixedUpdate () 
	{
		//if (!Kayma)
		//{
		//	yatay = Input.GetAxis ("Horizontal");
		//}

		zeminde = Zeminde ();

		Temel_Hareketler (yatay);

		Yon_Cevir (yatay);

		Hareket_Katmanlari ();
	}

	private void Temel_Hareketler (float yatay)
	{
		if (myRigidbody.velocity.y < -2f) 
		{
			myAnimator.SetBool ("Düsme",true);
		}

		if ((Atak || kunaiAtak) && zeminde) 
		{
			myRigidbody.velocity = Vector2.zero;
		}
		else if (!Kayma && !öldün)
		{
			if (zeminde || HavadaKontrol) 
			{
				myRigidbody.velocity = new Vector2 (yatay * hiz, myRigidbody.velocity.y);
			}
		}

		if (zeminde && Zipla)
		{
			zeminde = false;
			myRigidbody.AddForce (new Vector2(0,ZiplamaKuvveti * 100));
		}

		myAnimator.SetFloat ("KarakterHiz", Mathf.Abs (yatay));
	}

	private void Yon_Cevir(float yatay)
	{
		if (yatay > 0 && !sagaBak || yatay < 0 && sagaBak && !Atak && !Kayma && !kunaiAtak && !öldün) // Kayma Animasyonu False Değerinde Eklenicek. 
		{
			sagaBak = !sagaBak;

			Vector3 yon = transform.localScale;

			yon.x *= -1;

			transform.localScale = yon;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "acikKapi")
		{
			KapiAcikSes.Play ();
			SceneManager.LoadScene (3);
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "öldün")
		{
			öldün = true;
			StartCoroutine (Death());
		}

		if (other.gameObject.tag == "aniÖlüm")
		{
			öldün = true;
			transform.position = new Vector3 (-58.8f, -7.8f, 0);
			öldün = false;
		}
	}

	//private void Kontroller ()
	//{
	//if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && !Zipla)
	//{
	//	myAnimator.SetTrigger ("Zipla");
	//}

	//if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.DownArrow)) && zeminde && yatay != 0 && !Atak && !kunaiAtak) 
	//{
	//	myAnimator.SetTrigger ("Kayma");
	//}

	//if(Input.GetKeyDown(KeyCode.Mouse0) && !Kayma && !kunaiAtak && !Atak)
	//{
	//	myAnimator.SetTrigger ("Atak");
	//}

	//if (Input.GetKeyDown (KeyCode.Mouse1) && !Kayma && !Atak && !kunaiAtak) 
	//{
	//	myAnimator.SetTrigger ("kunaiAtak");
	//}
	//}

	private bool Zeminde ()
	{
		if (myRigidbody.velocity.y <= 0) 
		{
			foreach (Transform nokta in Temas_Noktalari) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (nokta.position, TemasCapi, HangiZemin);
				for (int i = 0; i < colliders.Length; i++) 
				{
					if (colliders [i].gameObject != gameObject) 
					{
						myAnimator.ResetTrigger ("Zipla");
						myAnimator.SetBool ("Düsme",false);
						return true;
					}
				}
			}
		}
		return false;
	}

	private void Hareket_Katmanlari () 
	{
		if (zeminde) 
		{
			myAnimator.SetLayerWeight (1,1);
			HavadaKontrol = false;
		}
		else
		{
			myAnimator.SetLayerWeight (1,1);
			HavadaKontrol = true;
		}
	}

	public void Kunai_Atma ()
	{
		if (transform.localScale.x < 0)
		{
			kunaiYon = Kunai.transform.localScale;
			kunaiYon.x = -1.25f;
			Kunai.transform.localScale = kunaiYon;
		}
		else
		{
			kunaiYon = Kunai.transform.localScale;
			kunaiYon.x = 1.25f;
			Kunai.transform.localScale = kunaiYon;
		}

		if(zeminde)
		{
			Instantiate (Kunai, kunaiMachine2.position, Quaternion.identity);
		}
		else if (HavadaKontrol)
		{
			Instantiate (Kunai, kunaiMachine.position, Quaternion.identity);
		}
	}

	IEnumerator Death ()
	{
		yield return new WaitForSeconds (0.5f);
		transform.position = new Vector3 (-58.8f, -7.8f, 0);
		öldün = false;
	}

	public void Saga_Git ()
	{
		if (!Kayma) 
		{
			yatay = 1;
		}
	}

	public void Sola_Git ()
	{
		if (!Kayma)
		{
			yatay = -1;
		}
	}

	public void Dur ()
	{
		yatay = 0;
	}

	public void Zipla_Buton ()
	{
		if (!Zipla && !öldün)
		{
			myAnimator.SetTrigger ("Zipla");
		}
	}

	public void Atak_Buton ()
	{
		if (!Atak)
		{
			myAnimator.SetTrigger ("Atak");
		}
	}

	public void Kunai_Atak_Buton ()
	{
		if(!Atak && !kunaiAtak && !Kayma)
		{
			myAnimator.SetTrigger ("kunaiAtak");
		}
	}

	public void Kayma_Buton ()
	{
		if (zeminde && yatay != 0 && !Atak && !Kayma)
		{
			myAnimator.SetTrigger ("Kayma");
		}
	}

	public void Replay ()
	{
		SceneManager.LoadScene (2);
	}

	public void MaınMenu ()
	{
		SceneManager.LoadScene (1);
	}

	public void Pause ()
	{
		if (!pause) 
		{
			pause = true;
			bitis_ekrani.SetActive (true);
		}
		else
		{
			pause = false;
			bitis_ekrani.SetActive (false);
		}
	}
}
