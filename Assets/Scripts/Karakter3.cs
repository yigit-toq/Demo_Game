using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Karakter3 : MonoBehaviour {

	private static Karakter3 ornek3;
	public static Karakter3 PlayerCode3 {
		get{
			if(ornek3 == null){
				ornek3 = GameObject.FindObjectOfType<Karakter3>();
			}
			return ornek3;
		}
	}

	private Rigidbody2D rigid;
	private Animator anim;

	[Header("GameObject")]
	public GameObject karakterKonusma1;
	public GameObject karakterKonusma2;
	public GameObject karakterKonusma3;
	public GameObject Konusma_Kutusu;
	public GameObject tüccarKonusma1;
	public GameObject Guc;
	public GameObject Hiz;
	public GameObject Can;
	public GameObject SohbetEt;
	public GameObject SohbetCik;

	[Header("Text")]
	public Text coinText;

	[Header("Image")]
	public Image canBar;

	private RectTransform rect;

	[Header("Int")]
	public int coin;

	[Header("Float")]
	public float can;
	public float hiz;
	public float hasar;

	private float yatay;

	private bool sagaBak;
	private bool konus;

	void Start () {
		int coinVeri = PlayerPrefs.GetInt ("sonCoin");
		float canVeri = PlayerPrefs.GetFloat ("sonCan");
		float hizVeri = PlayerPrefs.GetFloat ("sonHiz");
		float hasarVeri = PlayerPrefs.GetFloat ("sonHasar");

		coin = coinVeri;
		can = canVeri;
		hiz = hizVeri;
		hasar = hasarVeri;

		SkorAyarla (coin);

		sagaBak = true;
		konus = false;

		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();

		SohbetEt.SetActive (false);
		SohbetCik.SetActive (false);

		rect = canBar.rectTransform;
	}

	void Update ()
	{
		rect.sizeDelta = new Vector2(can,rect.sizeDelta.y);

		if (konus)
		{
			SohbetEt.SetActive (false);
			anim.SetFloat ("HareketHizi",0);
		}

		if (Can.activeSelf && Hiz.activeSelf && Guc.activeSelf)
		{
			SohbetCik.SetActive (true);
		}

		if (can <= 0)
		{
			can = 0;
		}
	}

	void FixedUpdate () 
	{
		//float yatay = Input.GetAxis ("Horizontal");

		Temel_Hareketler (yatay);
		Yon_Cevir (yatay);

		if (can >= 400)
		{
			can = 400;
		}
	}

	void Temel_Hareketler (float yatay)
	{
		if (!konus)
		{
		    rigid.velocity = new Vector2 (yatay * hiz,rigid.velocity.y);
		}

		anim.SetFloat ("HareketHizi",Mathf.Abs(yatay));
	}

	void Yon_Cevir (float yatay)
	{
		if ((sagaBak && yatay < 0 || !sagaBak && yatay > 0) && !konus)
		{
			sagaBak = !sagaBak;
			Vector3 yon = transform.localScale;
			yon.x *= -1;
			transform.localScale = yon;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "dukkancik")
		{
			PlayerPrefs.SetFloat ("sonCan3",can);
			PlayerPrefs.SetFloat ("sonHasar3",hasar);
			PlayerPrefs.SetFloat ("sonHiz3",hiz);
			PlayerPrefs.SetInt ("sonCoin3",coin);
			sahneGecis.ornek.LoadLevel (7);
			sahneGecis.ornek.level = 6;
		}

		if (other.gameObject.tag == "konus")
		{
			SohbetEt.SetActive (true);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "konus")
		{
			SohbetEt.SetActive (false);
			konus = false;
		}
	}

	public void Hiz_Al () 
	{
		if (coin >= 100) 
		{
			hiz = hiz + 1;
			coin = coin - 100;
			SkorAyarla (coin);
			StartCoroutine (Esya_Aldin ());
		}
	}

	public void Güc_Al ()
	{
		if (coin >= 200) 
		{
			hasar = hasar + 1;
			coin = coin - 200;
			SkorAyarla (coin);
			StartCoroutine (Esya_Aldin ());
		}
	}

	public void Can_Al ()
	{
		if(coin >= 200)
		{
			can = 400;
			coin = coin - 200;
			SkorAyarla (coin);
			StartCoroutine (Esya_Aldin ());
		}
	}

	IEnumerator Konusma ()
	{
		Konusma_Kutusu.SetActive (true);
		tüccarKonusma1.SetActive (true);
		yield return new WaitForSeconds (2);
		tüccarKonusma1.SetActive (false);
		karakterKonusma1.SetActive (true);
		yield return new WaitForSeconds (2);
		karakterKonusma1.SetActive (false);
		Can.SetActive (true);
		Hiz.SetActive (true);
		Guc.SetActive (true);
	}

	IEnumerator Esya_Aldin ()
	{
		Can.SetActive (false);
		Hiz.SetActive (false);
		Guc.SetActive (false);
		SohbetCik.SetActive (false);
		karakterKonusma2.gameObject.SetActive (true);
		yield return new WaitForSeconds (2);
		karakterKonusma2.gameObject.SetActive (false);
		Konusma_Kutusu.SetActive (false);
		konus = false;
	}

	IEnumerator Almiyim ()
	{
		Can.SetActive (false);
		Hiz.SetActive (false);
		Guc.SetActive (false);
		karakterKonusma3.SetActive (true);
		yield return new WaitForSeconds (2);
		karakterKonusma3.SetActive (false);
		Konusma_Kutusu.SetActive (false);
		konus = false;
	}

	void SkorAyarla (int count) 
	{
		coinText.text = count.ToString ();
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

	public void SohbetEdelim ()
	{
		konus = true;
		StartCoroutine (Konusma());
	}

	public void SohbetiBitirelim ()
	{
		StartCoroutine (Almiyim());
		SohbetCik.SetActive (false);
	}
}
