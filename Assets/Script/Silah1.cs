using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Silah1 : MonoBehaviour {

	public Zombi zombieCode;

	private static Silah1 gunGet;
	public static Silah1 gunCode {
		get{
			if(gunGet == null){
				gunGet = GameObject.FindObjectOfType<Silah1> ();
			}
			return gunGet;
		}
	}

	[Header("Bilgi")]
	public string Silah_Adı;

	[Header("Karakter")]
	public float hiz;

	[Header("Mermi")]
	public float toplamMermi;
	public float sarjorMermi;
	public float elimizdekiMermi;
	public float atesHizi;
	private float atesZamani;
	public bool ates;

	public float menzil;

	[Header ("Hasar")]
	public float vucutHasar;
	public float kafadanHasar;
	public float kolHasar;
	public float bacakHasar;

	[Header("Reload")]
	public bool reload;
	public float reloadmaxZaman;
	private float reloadZaman;

	[Header("Prefabs")]
	public GameObject tahtaizi;
	public GameObject kanizi;

	[Header("ParticleSystem")]
	public ParticleSystem muzzleFlash;

	[Header("Fener")]
	public bool isik;
	public GameObject fener;

	[Header("Sesler")]
	public AudioClip atesSes;
	public AudioClip reloadSes;

	[Header("UI")]
	public Text sarjorText;
	public Text toplamermiText;


	private RaycastHit hit;
	public Animator gunAnim {
		get;
		set;
	}
	private AudioSource gunSes;
	private CharacterController charCont;

	void Start () {
		fener.SetActive (false);
		gunAnim = GetComponent<Animator> ();
		gunSes = GetComponent<AudioSource> ();
		charCont = FindObjectOfType<CharacterController> ();
		zombieCode = FindObjectOfType<Zombi> ();
	}

	void Update () {
		Kontroller ();
		Fener ();
		MermiUI ();
	}
	void FixedUpdate () {
		Hareket ();
		Ates_Etme ();
		Reload ();
	}
	void Hareket () {
		gunAnim.SetFloat ("hiz",charCont.velocity.magnitude);
		hiz = charCont.velocity.magnitude;
		if(Aim.aimCode.aim){
			gunAnim.SetFloat ("hiz",0);
		}
	}
	void Kontroller ()
	{
		if (Input.GetMouseButton (0) && elimizdekiMermi > 0 && !reload && !ates) 
		{
			gunAnim.SetTrigger ("ates");
			elimizdekiMermi--;
			gunSes.PlayOneShot (atesSes);
			muzzleFlash.Play ();
			ates = true;
			if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit, menzil))
			{
				if(hit.collider.tag == "Untagged") 
				{
					Instantiate (tahtaizi,hit.point,Quaternion.LookRotation(hit.normal));
				}
				if(hit.collider.tag == "Enemy")
				{
					Instantiate (kanizi,hit.point,Quaternion.LookRotation(hit.normal));
				}
				if(hit.collider.tag == "EnemyHead") 
				{
					zombieCode.zombiCan = zombieCode.zombiCan - kafadanHasar;
				}
				if(hit.collider.tag == "EnemyBody") 
				{
					zombieCode.zombiCan = zombieCode.zombiCan - vucutHasar;
				}
				if(hit.collider.tag == "EnemyLegTop" || hit.collider.tag == "EnemyLegLower")
				{
					zombieCode.zombiCan = zombieCode.zombiCan - bacakHasar;
				}
				if(hit.collider.tag == "EnemyArmTop" || hit.collider.tag == "EnemyArmLower")
				{
					zombieCode.zombiCan = zombieCode.zombiCan - kolHasar;
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.R) && !reload && toplamMermi > 0 && elimizdekiMermi != sarjorMermi && !ates && !Aim.aimCode.aim) 
		{
			reload = true;
			gunAnim.SetTrigger ("reload");
			gunSes.PlayOneShot (reloadSes);
		}

		if (Input.GetKeyDown (KeyCode.F) && !isik) 
		{
			isik = true;
		}
		else if(Input.GetKeyDown (KeyCode.F))
		{
			isik = false;
		}
	}

    void Ates_Etme () 
	{
		if(ates)
		{
			if (atesZamani > atesHizi) 
			{
				atesZamani = 0;
				ates = false;
			}
			else
			{
				atesZamani += Time.deltaTime;
			}
		}
	}

	void Reload ()
	{
		if(reload)
		{
			if (reloadZaman > reloadmaxZaman) 
			{
				float gerekliMermi = sarjorMermi - elimizdekiMermi;
				if (gerekliMermi > toplamMermi) 
				{
					elimizdekiMermi += toplamMermi;
					toplamMermi = 0;
				}
				else
				{
					elimizdekiMermi = sarjorMermi;
					toplamMermi -= gerekliMermi;
				}
				reload = false;
				reloadZaman = 0;
			}
			else
			{
				reloadZaman += Time.deltaTime;
			}
		}
	}

	void Fener () 
	{
		if (isik) 
		{
			fener.SetActive (true);
		}
		else
		{
			fener.SetActive (false);
		}
	}

	void MermiUI()
	{
		sarjorText.text = elimizdekiMermi.ToString ("00");
		toplamermiText.text = toplamMermi.ToString ("000");
	}
}
