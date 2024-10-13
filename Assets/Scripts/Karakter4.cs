using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Karakter4 : MonoBehaviour {

	private static Karakter4 ornek4;
	public static Karakter4 PlayerCode4 {
		get{
			if(ornek4 == null){
				ornek4 = GameObject.FindObjectOfType<Karakter4> ();
			}
			return ornek4;
		}
	}

	private RectTransform rect;
	private RectTransform rectwo;

	[Header("Transform")]
	public Transform[] temasNoktasi;

	[Header("LayerMask")]
	public LayerMask Zeminler;

	[Header("GameObject")]
	public GameObject varil;
	public GameObject bitis_ekrani;

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

	public bool Egilme {get; set;}
	public bool Zipla {get; set;}
	public bool Atak {get; set;}

	private bool sagaBak;
	private bool öldün_mü;

	public Rigidbody2D MyRigidbody {get; set;}
	public Animator MyAnimator {get; set;}

	[Header("Int")]
	public int coin;

	void Start () {
		float canVeri = PlayerPrefs.GetFloat ("sonCan3");
		float hasarVeri= PlayerPrefs.GetFloat ("sonHasar3");
		float hizVeri= PlayerPrefs.GetFloat ("sonHiz3");
		int coinVeri= PlayerPrefs.GetInt ("sonCoin3");

		can = canVeri;
		hiz = hizVeri;
		coin = coinVeri;
		Hasar_Vurma = hasarVeri;

		coinText.text = coin.ToString ();

		stamina = 275;

		rect = canBar.rectTransform;
		rectwo = staminaBar.rectTransform;

		sagaBak = true;

		MyRigidbody = GetComponent<Rigidbody2D> ();
		MyAnimator = GetComponent<Animator> ();

		bitis_ekrani.SetActive (false);
	}

	void Update () 
	{
		//Kontroller ();

		rect.sizeDelta = new Vector2 (can,rect.sizeDelta.y);
		rectwo.sizeDelta = new Vector2 (stamina,rectwo.sizeDelta.y);

		if (stamina < 0)
		{ 
			stamina = 0;
		}

		if (stamina <= 275 && !Atak && !Zipla)
		{
			stamina = stamina + 1;
		}
			
		if (can <= 0)
		{
			can = 0;
			öldün_mü = true;
		}

		if (can > 400)
		{
			can = 400;
		}

		if (öldün_mü)
		{
			bitis_ekrani.SetActive (true);
		}
	}

	void FixedUpdate ()
	{
		//float yatay = Input.GetAxis ("Horizontal");

		Temel_Hareketler (yatay);

		ZeminÜstünde = Zeminde ();

		AnimasyonKatmanlari ();

		Yon_Cevir (yatay);

		if (ZeminÜstünde)
		{
			MyAnimator.SetBool ("düsme",false);	
		}
	}

	private void Temel_Hareketler (float yatay)
	{
		if (MyRigidbody.velocity.y < 0) 
		{
			MyAnimator.SetBool ("düsme",true);	
		}

		if (Zipla && ZeminÜstünde && stamina >= 50 && !öldün_mü && !Atak)
		{
			ZeminÜstünde = false;
			jumpSes.Play ();
			MyRigidbody.AddForce (new Vector2 (0, ZiplamaKuvveti));
		}

		if ((ZeminÜstünde || HavadaKontrol) && !Atak && !Egilme && !öldün_mü)
		{
			MyRigidbody.velocity = new Vector2 (yatay * hiz,MyRigidbody.velocity.y);
		}

		if (!öldün_mü)
		{
			MyAnimator.SetFloat ("KarakterHizi",Mathf.Abs(yatay));
		}
	}

	//void Kontroller ()
	//{
	//	if (Input.GetKeyDown(KeyCode.Mouse0) && stamina >= 10)
	//	{
	//		MyAnimator.SetTrigger ("atak");
	//   	atakSes.Play ();
	//		stamina = stamina - 10;
	//	}
	//
	//	if (Input.GetKeyDown(KeyCode.W) && stamina >= 15) 
	//	{
	//		MyAnimator.SetTrigger ("zipla");
	//		stamina = stamina - 15;
	//	}
	//
	//	if (Input.GetKey (KeyCode.S)) 
	//	{
	//		MyAnimator.SetBool ("egilme",true);
	//	}
	//	else
	//	{
	//		MyAnimator.SetBool ("egilme",false);
	//	}
	//}

	void Yon_Cevir (float yatay)
	{
		if ((yatay > 0 && !sagaBak || yatay < 0 && sagaBak) && !öldün_mü)
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

	private bool Zeminde ()
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

		if (other.gameObject.tag == "Salyangoz") 
		{
			other.gameObject.SetActive (false);
			StartCoroutine (Yavaslama ());
			canSes.Play ();
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

		if (other.gameObject.tag == "varilCoin")
		{
			if (!varil.activeSelf)
			{
				coin = coin + 100;
				SkorAyarla (coin);
				coinSes.Play ();
				other.gameObject.SetActive (false);
			}
		}

		if (other.gameObject.tag == "yasliYürü")
		{
			Dayi.dayiCode.yürü = true;
		}

		if (other.gameObject.tag == "bitiş")
		{
			sahneGecis.ornek.LoadLevel (7);
			sahneGecis.ornek.level = 8;
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

	IEnumerator Yavaslama ()
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
		MyAnimator.SetTrigger ("zipla");
	}

	public void EgilButon ()
	{
		MyAnimator.SetBool ("egilme",true);
	}

	public void Kalk ()
	{
		MyAnimator.SetBool ("egilme",false);
	}

	public void AtakButon ()
	{
		if (stamina > 50)
		{
			stamina = stamina - 10;
			atakSes.Play ();
			MyAnimator.SetTrigger ("atak");
		}
	}

	public void Butona_Basildi ()
	{
		SceneManager.LoadScene (5);
	}

	public void Butona_Basildi_2 ()
	{
		SceneManager.LoadScene (6);
	}
}
