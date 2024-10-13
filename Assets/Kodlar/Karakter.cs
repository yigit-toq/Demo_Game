using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Karakter : MonoBehaviour {

	private static Karakter karakterOrn;
	public static Karakter karakter 
	{
		get{
			if(karakterOrn == null)
			{
				karakterOrn = FindObjectOfType<Karakter> ();
			}
			return karakterOrn;
		}
	}

	public static BGMusic bgMusic;

	private Rigidbody2D myRigidbody;
	private Animator myAnimator;

	private int skor;

	[Header ("Float")]
	public float hiz;
	public float yatay;
	public float TemasCapi;
	public float ZiplamaKuvveti;

	[Header ("Bool")]
	public bool sagaBak;
	public bool zeminde;
	public bool Zipla;
	public bool pause;
	public bool Anahtarvar;
	public bool HavadaKontrol;

	[Header("Yazılar")]
	public Text ToplamSkor;

	[Header ("Objeler")]
	public GameObject bitis_ekrani;
	public GameObject AnahtarGörsel;
	public GameObject KapiAcik;

	[Header ("Konumlar")]
	public Transform[] Temas_Noktalari;


	[Header ("Maskeler")]
	public LayerMask HangiZemin;

	[Header ("Sesler")]
	public AudioSource AltinSes;
	public AudioSource KapiAcikSes;
	public AudioSource KapiKapaliSes;

	void Start () 
	{
		Anahtarvar = false;
		pause = false;
		sagaBak = true;

		AnahtarGörsel.SetActive (false);
		bitis_ekrani.SetActive (false);

		ToplamSkor.text = 0.ToString ();

		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();

		bgMusic = FindObjectOfType<BGMusic> ();

		bgMusic.hangiSahne = 1;
	}

	void Update () 
	{
	    //Kontroller ();
	}

	void FixedUpdate () 
	{
		//yatay = Input.GetAxis ("Horizontal");

		zeminde = Zeminde ();

		Temel_Hareketler (yatay);

		Yon_Cevir (yatay);

	}

	private void Temel_Hareketler(float yatay)
	{
		if (myRigidbody.velocity.y < -2f) 
		{
			myAnimator.SetBool ("Düsme",true);
		}

		if (zeminde && Zipla)
		{
			zeminde = false;
			myRigidbody.AddForce (new Vector2(0,ZiplamaKuvveti * 100));
		}

		myRigidbody.velocity = new Vector2 (yatay * hiz, myRigidbody.velocity.y);

		myAnimator.SetFloat ("KarakterHiz", Mathf.Abs (yatay));
	}

	private void Yon_Cevir(float yatay)
	{
		if (yatay > 0 && !sagaBak || yatay < 0 && sagaBak) 
		{
			sagaBak = !sagaBak;
			Vector3 yon = transform.localScale;
			yon.x *= -1;
			transform.localScale = yon;
		}
	}

	private	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Anahtar") 
		{
			other.gameObject.SetActive (false);
			Anahtarvar = true;
			AnahtarGörsel.SetActive (true);
		}
	}

	private	void SkorAyarla (int count)
	{
		ToplamSkor.text = count.ToString ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Kapi" && Anahtarvar == true) 
		{
			other.gameObject.SetActive (false);
			KapiAcik.SetActive (true);
		}
		if (other.gameObject.tag == "Kapi") 
		{
			KapiKapaliSes.Play ();
		}
		else if (KapiAcik.activeSelf)
		{
			KapiAcikSes.Play ();
		}
		if (other.gameObject.tag == "Altin") 
		{
			other.gameObject.SetActive (false);
			AltinSes.Play();
			skor = skor + 100;
			SkorAyarla (skor);
		}
	}

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
		
	//private void Kontroller ()
	//{
	//    if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && !Zipla)
	//    {
	//		myAnimator.SetTrigger ("Zipla");
	//	}
	//}

	public void Saga_Git ()
	{
		yatay = 1;
	}

	public void Sola_Git ()
	{
		yatay = -1;
	}

	public void Dur ()
	{
		yatay = 0;
	}

	public void Zipla_Buton ()
	{
		if (!Zipla && zeminde)
		{
			myAnimator.SetTrigger ("Zipla");
		}
	}

	public void Replay ()
	{
		SceneManager.LoadScene (3);
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
