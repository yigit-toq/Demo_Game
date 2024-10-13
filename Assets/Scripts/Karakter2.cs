using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Karakter2 : MonoBehaviour {

	private static Karakter2 ornek2;
	public static Karakter2 PlayerCode2 {
		get{
			if(ornek2 == null){
				ornek2 = GameObject.FindObjectOfType<Karakter2> ();
			}
			return ornek2;
		}
	}

	[Header("Transform")]
	public Transform[] temasNoktasi;

	[Header("LayerMask")]
	public LayerMask Zeminler;

	[Header("GameObject")]
	public GameObject SohbetKutusu;
	public GameObject SohbetButon;
	public GameObject DukkanButon;
	public GameObject tüccar;

	[Header("Image")]
	public Image canBar;
	public Image staminaBar;

	[Header("Text")]
	public Text coinText;
	public Text toplamSkorUI;

	[Header("AudioSource")]
	public AudioSource coinSes;
	public AudioSource canSes;
	public AudioSource jumpSes;
	public AudioSource atakSes;

	[Header("Float")]
	public float ZiplamaKuvveti;
	public float Hasar_Vurma;
	public float temasCapi;
	public float hiz;
	public float can;

	private float stamina;
	private float yatay;

	[Header("Bool")]
	public bool HavadaKontrol;
	public bool ZeminÜstünde;
	public bool sohbetButon_Basildi;
	public bool sohbetButon_Aktif;

	public bool Egilme {get; set;}
	public bool Zipla {get; set;}
	public bool Atak {get; set;}

	private bool sagaBak;
	private bool öldün_mü;

	[Header("Int")]
	public int coin;

	public Rigidbody2D MyRigidbody {get; set;}
	private Animator MyAnimator;

	private RectTransform rect;
	private RectTransform rectwo;

	void Start () 
	{
		float canVeri = PlayerPrefs.GetFloat ("sonCan");
		int coinVeri = PlayerPrefs.GetInt ("sonSkor");

		coin = coinVeri;
		can = canVeri;

		SkorAyarla (coin);

		sagaBak = true;
		sohbetButon_Basildi = false;
		sohbetButon_Aktif = false;

		Hasar_Vurma = 10;
		hiz = 7;
		stamina = 275;

		SohbetButon.SetActive (false);
		DukkanButon.SetActive (false);

		rect = canBar.rectTransform;
		rectwo = staminaBar.rectTransform;

		MyRigidbody = GetComponent<Rigidbody2D> ();
		MyAnimator = GetComponent<Animator> ();
	}

	void Update () 
	{
		rect.sizeDelta = new Vector2 (can,rect.sizeDelta.y);
		rectwo.sizeDelta = new Vector2 (stamina,rectwo.sizeDelta.y);

		if (can <= 0)
		{
			can = 0;
			öldün_mü = true;
		}

		//Kontroller ();

		if (stamina <= 0)
		{
			stamina = 0;
		}

		if (stamina <= 275 && !Atak && !Zipla)
		{
			stamina += 1;
		}

		if (SohbetKutusu.activeSelf)
		{
			MyAnimator.SetFloat ("KarakterHizi", 0);
		}
	}

	void FixedUpdate ()
	{
		//float yatay = Input.GetAxis ("Horizontal");
	
		Temel_Hareketler2 (yatay);
		ZeminÜstünde = Zeminde2 ();
		AnimasyonKatmanlari ();
		Yon_Cevir (yatay);

		if (ZeminÜstünde)
		{
			MyAnimator.SetBool ("düsme",false);	
		}

		if (can >= 400)
		{
			can = 400;
		}
	}

	private void Temel_Hareketler2 (float yatay)
	{
		if (MyRigidbody.velocity.y < 0) 
		{
			MyAnimator.SetBool ("düsme",true);	
		}

		if (Zipla && ZeminÜstünde && !Egilme && stamina >= 50 && !öldün_mü && !Atak)
		{
			ZeminÜstünde = false;
			jumpSes.Play ();
			MyRigidbody.AddForce (new Vector2 (0, ZiplamaKuvveti));
		}

		if ((ZeminÜstünde || HavadaKontrol) && !öldün_mü && !Atak && !Egilme && !SohbetKutusu.activeSelf)
		{
			MyRigidbody.velocity = new Vector2 (yatay * hiz,MyRigidbody.velocity.y);
		}

		if(!öldün_mü)
		{
			MyAnimator.SetFloat ("KarakterHizi",Mathf.Abs(yatay));
		}
	}

	//void Kontroller() 
	//{
	//	if (Input.GetKeyDown(KeyCode.Mouse0) && stamina >= 10 && !SohbetKutusu.activeSelf)
	//	{
	//		MyAnimator.SetTrigger ("atak");
	//   	atakSes.Play ();
	//		stamina = stamina - 10;
	//	}
	//
	//	if (Input.GetKeyDown(KeyCode.W) && stamina >= 20 && !SohbetKutusu.activeSelf)
	//	{
	//		MyAnimator.SetTrigger ("zipla");
	//		stamina = stamina - 15;
	//	}
	//
	//	if (Input.GetKey (KeyCode.S) && !SohbetKutusu.activeSelf) 
	//	{
	//		MyAnimator.SetBool ("egilme",true);
	//	}
	//	else if (!SohbetKutusu.activeSelf)
	//	{
	//		MyAnimator.SetBool ("egilme",false);
	//	}
	//}

	void Yon_Cevir(float yatay) {
		if ((yatay > 0 && !sagaBak || yatay < 0 && sagaBak) && !öldün_mü && !SohbetKutusu.activeSelf)
		{
			sagaBak = !sagaBak;
			Vector3 yon = transform.localScale;
			yon.x *= -1;
			transform.localScale = yon;
		}
	}

	private void AnimasyonKatmanlari () 
	{
		if (!ZeminÜstünde) 
		{
			MyAnimator.SetLayerWeight (1, 1);
			HavadaKontrol = true;
		} 
		else 
		{
			MyAnimator.SetLayerWeight (1, 0);
			HavadaKontrol = false;
		}
	}
	private bool Zeminde2 () 
	{
		if (MyRigidbody.velocity.y <= 0) 
		{
			foreach (Transform nokta in temasNoktasi) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (nokta.position, temasCapi, Zeminler);
				for (int i = 0; i < colliders.Length; i++) 
				{
					if (colliders [i].gameObject != gameObject) 
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Coin")
		{
			other.gameObject.SetActive (false);
			coinSes.Play ();
			coin = coin + 100;
			SkorAyarla (coin);
			UISkorAyarla (coin);
		}

		if (other.gameObject.tag == "tüccar")
		{
			if (!sohbetButon_Aktif)
			{
				SohbetButon.SetActive (true);
			}
		}

		if (other.gameObject.tag == "Salyangoz") 
		{
			other.gameObject.SetActive (false);
			canSes.Play ();
			StartCoroutine (Yavaslama ());
		}

		if (other.gameObject.tag == "can")
		{
			canSes.Play ();
			if(can < 400)
			{
				can = 400;
			}
			other.gameObject.SetActive (false);
		}

		if (other.gameObject.tag == "tüccarKapi" && !tüccar.activeSelf)
		{
			DukkanButon.SetActive (true);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "tüccar")
		{
			SohbetButon.SetActive (false);
		}

		if (other.gameObject.tag == "tüccarKapi")
		{
			DukkanButon.SetActive (false);
		}
	}

	void SkorAyarla (int count) 
	{
		coinText.text = count.ToString ();
	}

	void UISkorAyarla(int count)
	{
		toplamSkorUI.text = count.ToString ();
	}

	IEnumerator Yavaslama()
	{
		hiz = 3.5f;
		MyAnimator.speed = 0.5f;
		yield return new WaitForSeconds (8);
		MyAnimator.speed = 1;
		hiz = 7;
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
		if (stamina >= 20 && !SohbetKutusu.activeSelf)
		{
			MyAnimator.SetTrigger ("zipla");
			stamina = stamina - 20;
		}
	}

	public void EgilButon ()
	{
		if(!SohbetKutusu.activeSelf)
		{
			MyAnimator.SetBool ("egilme",true);
		}
	}

	public void Kalk ()
	{
		MyAnimator.SetBool ("egilme",false);
	}

	public void AtakButon ()
	{
		if (stamina >= 10 && !SohbetKutusu.activeSelf)
		{
			MyAnimator.SetTrigger ("atak");
			atakSes.Play ();
			stamina = stamina - 10;
		}
	}

	public void Butona_Basildi ()
	{
		SceneManager.LoadScene (3);
	}

	public void SohbetEt ()
	{
		sohbetButon_Basildi = true;
		sohbetButon_Aktif = true;
		SohbetButon.SetActive (false);
	}

	public void DukkanGir ()
	{
		PlayerPrefs.SetInt ("sonCoin",coin);
		PlayerPrefs.SetFloat ("sonCan",can);
		PlayerPrefs.SetFloat ("sonHiz",hiz);
		PlayerPrefs.SetFloat ("sonHasar",Hasar_Vurma);
		DukkanButon.SetActive (false);
		SceneManager.LoadScene (4);
	}
}
