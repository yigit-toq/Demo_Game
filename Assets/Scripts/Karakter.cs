using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour {
	
	private static Karakter ornek;
	public static Karakter PlayerCode 
	{
		get
		{
			if(ornek == null){
				ornek = GameObject.FindObjectOfType<Karakter> ();
			}
			return ornek;
		}
	}

	[Header ("Vector")]
	public Vector3 yon;

	[Header ("LayerMask")]
	public LayerMask Zeminler;

	[Header ("Transform")]
	public Transform[] temasNoktasi;

	[Header ("GameObject")]
	public GameObject sohbetButon;
	public GameObject sohbetKutusu;
	public GameObject panel;
	public GameObject demon;

	[Header ("Float")]
	public float hiz;
	public float can;
	public float stamina;
	public float temasCapi;
	public float ZiplamaKuvveti;
	public float Hasar_Vurma;

	private float yatay;

	[Header ("Int")]
	public int ölüSayisi;
	public int skor;

	[Header ("Image")]
	public Image can_bar;
	public Image stamina_bar;

	[Header ("Text")]
	public Text toplamSkor;
	public Text toplamSkorUI;
	public Text kaçÖldürme;

	[Header ("Bool")]
	public bool hasarAlma;
	public bool demonGeldimi;
	public bool ittirme;
	public bool HavadaKontrol;
	public bool sohbetButon_Basildi;
	public bool sohbetButon_Aktif;

	public bool konusma_basladi { get; set; }
	public bool ZeminÜstünde { get; set; }
	public bool Egilme { get; set; }
	public bool Zipla { get; set; }
	public bool Atak { get; set; }
	public bool öldün_mü { get; set; }

	private bool sagaBak;

	[Header ("AudioSource")]
	public AudioSource coinSes;
	public AudioSource canSes;
	public AudioSource jumpSes;
	public AudioSource atakSes;

	public Animator myAnimator { get; set; }
	public Rigidbody2D MyRigidbody { get; set; }

	private RectTransform rect;
	private RectTransform rectwo;

	void Start () {
		int skorVeri = PlayerPrefs.GetInt ("sonSkor");
		float canVeri = PlayerPrefs.GetFloat ("sonCan");

		skor = skorVeri;
		can = canVeri;

		toplamSkor.text = skor.ToString ();

		demonGeldimi = false;
		stamina = 275;
		Hasar_Vurma = 10;
		hiz = 7;
		konusma_basladi = false;
		sagaBak = true;
		hasarAlma = false;
		sohbetButon_Basildi = false;
		MyRigidbody = GetComponent <Rigidbody2D>();
		myAnimator = GetComponent <Animator> ();
		rect = can_bar.rectTransform;
		rectwo = stamina_bar.rectTransform;
		sohbetButon.SetActive (false);
		sohbetButon_Aktif = false;
	}

	void Update () 
	{
		rect.sizeDelta = new Vector2 (can,rect.sizeDelta.y);
		rectwo.sizeDelta = new Vector2 (stamina,rect.sizeDelta.y);

		//Kontroller ();

		if (öldün_mü)
		{
			panel.gameObject.SetActive (true);
		}

		KillerText (ölüSayisi);

		if (stamina <= 275 && !Atak && !Zipla)
		{
			stamina = stamina + 1;
		}
	}

	void FixedUpdate()
	{
		//yatay = Input.GetAxis ("Horizontal");

		Temel_Hareketler (yatay);
		ZeminÜstünde = Zeminde ();
		AnimasyonKatmanlari ();
		Yon_Cevir (yatay);

		if (konusma_basladi || demonGeldimi)
		{
			myAnimator.SetFloat ("KarakterHizi",0);
		}

		if (can >= 400)
		{
			can = 400;
		}

		if (can <= 0)
		{
			can = 0;
			öldün_mü = true;
		}
	}

	private void Temel_Hareketler(float yatay)
	{
		if (MyRigidbody.velocity.y < 0) 
		{
			myAnimator.SetBool ("dusme",true);	
		}

		if ((ZeminÜstünde || HavadaKontrol) && !Egilme && !Atak && !konusma_basladi && !öldün_mü && !demonGeldimi)
		{
			MyRigidbody.velocity = new Vector2 (yatay * hiz, MyRigidbody.velocity.y);
		}

		if (Zipla && ZeminÜstünde && !Egilme && !hasarAlma && stamina >= 50 && !demonGeldimi && !öldün_mü && !konusma_basladi && !Atak) 
		{
			ZeminÜstünde = false;
			jumpSes.Play ();
			MyRigidbody.AddForce (new Vector2 (0, ZiplamaKuvveti));
			stamina -= 20;
		}

		if (!konusma_basladi && !öldün_mü && !demonGeldimi)
		{
		    myAnimator.SetFloat ("KarakterHizi",Mathf.Abs(yatay));
		}
	}

	private void Yon_Cevir(float yatay)
	{
		if ((yatay>0 && !sagaBak || yatay<0 && sagaBak) && !konusma_basladi && !öldün_mü && !demonGeldimi)
		{
			sagaBak = !sagaBak;
			yon = transform.localScale;
			yon.x *= -1;
			transform.localScale = yon;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Ghost") 
		{
			if (!sohbetButon_Aktif)
			{
				sohbetButon.SetActive (true);
			}
		}

		if (other.gameObject.tag == "triggerArea")
		{
			Kurukafa.kuruKafaCode.hedef = gameObject.transform;
			Kurukafa.kuruKafaCode.menzilde = true;
			Kurukafa.kuruKafaCode.Yon_Cevirme ();
		}

		if (other.gameObject.tag == "Coin") 
		{
			other.gameObject.SetActive (false);
			coinSes.Play ();
			skor = skor + 100;
			SkorAyarla (skor);
			UISkorAyarla (skor);
		}

		if (other.gameObject.tag == "Salyangoz") 
		{
			other.gameObject.SetActive (false);
			canSes.Play ();
			StartCoroutine (Yavaslama ());
		}

		if(other.gameObject.tag == "demonengel"){
			demonGeldimi = true;
		}

		if(other.gameObject.tag == "can")
		{
			canSes.Play ();
			if(can < 400)
			{
				can = 400;
			}
			other.gameObject.SetActive (false);
		}

		if(other.gameObject.tag == "bitiş")
		{
			PlayerPrefs.SetInt ("sonSkor",skor);
			PlayerPrefs.SetFloat ("sonCan",can);
			sahneGecis.ornek.LoadLevel (7);
			sahneGecis.ornek.level = 4;
		}

		if(other.gameObject.tag == "arka")
		{
			ittirme = true;
		}

		if(other.gameObject.tag == "üst")
		{
			Zipla = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Ghost") 
		{
			sohbetButon.SetActive (false);
		}

		if(other.gameObject.tag == "üst")
		{
			Zipla = false;
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if(other.gameObject.tag == "düşerekÖldün")
		{
			öldün_mü = true;
		}
	}

	private bool Zeminde()
	{
		if (MyRigidbody.velocity.y <= 0) 
		{
			foreach (Transform nokta in temasNoktasi) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (nokta.position, temasCapi, Zeminler);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) 
					{
						return true;
					}
				}
			}
		}
	    return false;
	}

	private void AnimasyonKatmanlari()
	{
		if (!ZeminÜstünde) 
		{
			myAnimator.SetLayerWeight (1, 1);
			HavadaKontrol = true;
		} 
		else 
		{
			myAnimator.SetLayerWeight (1, 0);
			HavadaKontrol = false;
		}
	}

	//private void Kontroller()
	//{
	//	if(Input.GetKeyDown(KeyCode.W))
	//	{
	//		myAnimator.SetTrigger ("zipla");
	//	}
	//
	//	if(Input.GetKeyDown(KeyCode.Mouse0) && !konusma_basladi && stamina >= 10 && !demonGeldimi)
	//	{
	//		myAnimator.SetTrigger ("atak");
	//  	atakSes.Play ();
	//		stamina = stamina - 10;
	//	}
	//
	//	if (Input.GetKey (KeyCode.S) && ZeminÜstünde && !konusma_basladi && !demonGeldimi)
	//	{
	//		myAnimator.SetBool ("egilme", true);
	//	} 
	//	else
	//	{
	//		myAnimator.SetBool ("egilme", false);
	//	}
	//}

	IEnumerator Yavaslama()
	{
		hiz = 3.5f;
		myAnimator.speed = 0.5f;
		yield return new WaitForSeconds (8);
		myAnimator.speed = 1;
		hiz = 7;
	}

	void SkorAyarla(int count)
	{
		toplamSkor.text = count.ToString ();
	}

	void UISkorAyarla(int count)
	{
		toplamSkorUI.text = count.ToString ();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "KurukafaAgiz" && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("atak"))
		{
			transform.position += new Vector3 ((Düsman.EntityCode.sonhiz * -2) * Time.deltaTime, 0, 0);
			can = can - 5;
		}
	}

	void KillerText(int count)
	{
		kaçÖldürme.text = count.ToString ();
	}

	public void SolButon ()
	{
		yatay = -1f;
	}

	public void SagButon ()
	{
		yatay = 1f;
	}

	public void Dur ()
	{
		yatay = 0f;
	}

	public void ZiplaButon ()
	{
		myAnimator.SetTrigger ("zipla");
	}

	public void EgilButon ()
	{
		if(!konusma_basladi && !demonGeldimi)
		{
			myAnimator.SetBool ("egilme",true);
		}
	}

	public void Kalk ()
	{
		myAnimator.SetBool ("egilme",false);
	}

	public void AtakButon ()
	{
		if (stamina >= 50)
		{
			stamina = stamina - 10;
			atakSes.Play ();
			myAnimator.SetTrigger ("atak");
		}
	}

	public void SohbetEt ()
	{
		sohbetButon_Basildi = true;
		sohbetButon_Aktif = true;
		sohbetButon.SetActive (false);
	}

	public void Butona_Basildi ()
	{
		SceneManager.LoadScene (2);
	}
}
