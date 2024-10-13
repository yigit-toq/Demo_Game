using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Karakter0 : MonoBehaviour {

	private static Karakter0 ornek0;
	public static Karakter0 PlayerCode0 {
		get{
			if(ornek0 == null){
				ornek0 = GameObject.FindObjectOfType<Karakter0> ();
			}
			return ornek0;
		}
	}

	[Header("Transform")]
	public Transform[] temasNoktasi;

	[Header("GameObject")]
	public GameObject bitis_ekrani;

	[Header("LayerMask")]
	public LayerMask Zeminler;

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

	public bool Egilme;
	public bool Zipla;
	public bool Atak;

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
		sagaBak = true;

		Hasar_Vurma = 10;
		stamina = 275;
		can = 400;
		coin = 0;
		hiz = 7;

		SkorAyarla (coin);
		UISkorAyarla (coin);

		rect = canBar.rectTransform;
		rectwo = staminaBar.rectTransform;

		bitis_ekrani.SetActive (false);

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
			stamina = stamina + 1;
		}

		if (öldün_mü)
		{
			bitis_ekrani.SetActive (true);
		}
	}

	void FixedUpdate ()
	{
		//float yatay = Input.GetAxis ("Horizontal");

		Temel_Hareketler2 (yatay);
		ZeminÜstünde = Zeminde2 ();
		AnimasyonKatmanlari ();
		Yon_Cevir (yatay);

		if (can >= 400)
		{
			can = 400;
		}
	}

	private void Temel_Hareketler2 (float yatay)
	{
		if (MyRigidbody.velocity.y < 0) 
		{
			MyAnimator.SetBool ("dusme", true);	
		}

		if (Zipla && ZeminÜstünde && !Egilme && stamina >= 50 && !öldün_mü && !Atak)
		{
			ZeminÜstünde = false;
			jumpSes.Play ();
			MyRigidbody.AddForce (new Vector2 (0, ZiplamaKuvveti));
			stamina = stamina - 20;
		}

		if ((ZeminÜstünde || HavadaKontrol) && !Atak && !Egilme && !öldün_mü)
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
		//if (Input.GetKeyDown(KeyCode.Mouse0) && stamina >= 10)
		//{
		//	MyAnimator.SetTrigger ("atak");
		//  atakSes.Play ();
		//	stamina = stamina - 10;
		//}

		//if (Input.GetKeyDown(KeyCode.W))
		//{
		//	MyAnimator.SetTrigger ("zipla");
		//}

		//if (Input.GetKey (KeyCode.S) && ZeminÜstünde) 
		//{
		//	MyAnimator.SetBool ("egilme",true);
		//}
		//else
		//{
		//	MyAnimator.SetBool ("egilme",false);
		//}
	//}

	void Yon_Cevir(float yatay) {
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

	private bool Zeminde2()
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

		if(other.gameObject.tag == "bitiş")
		{
			PlayerPrefs.SetInt ("sonSkor",coin);
			PlayerPrefs.SetFloat ("sonCan",can);
			sahneGecis.ornek.LoadLevel (7);
			sahneGecis.ornek.level = 2;
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if(other.gameObject.tag == "düşerekÖldün")
		{
			öldün_mü = true;
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
		if(stamina > 50)
		{
			stamina -= 10;
			atakSes.Play ();
			MyAnimator.SetTrigger ("atak");
		}
	}

	public void Butona_Basildi ()
	{
		SceneManager.LoadScene (1);
	}
}
