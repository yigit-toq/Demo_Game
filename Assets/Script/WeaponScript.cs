using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour {

	private RaycastHit Hit;

	public GameObject HitCıkış;
	public GameObject Fener;
	public GameObject EText;
	public GameObject MermiKutu;

	public Text ToplamMermiText;
	public Text MermiText;

	private bool ETextKapan;

	[Header("ReloadSystem")]
	private float ReloadXZaman;
	public float ReloadZaman;
	public float ToplamMermi;
	public float SarjorMermi;
	public float KalanMermi;
	public bool Reload;

	[Header("AtesSistemi")]
	private Animator SilahAnim;
	public GameObject MermiziEfekt;
	public bool KameraAtes;
	public float Mesafe;
	public float SilahBeklemeSuresi;
	private float SilahBekleme;

	public ParticleSystem muzzleFlash;

	[Header("Sesler")]
	private AudioSource SilahSesKaynak;
	public AudioClip SilahSes;

	public CharacterController Karakter;


	void Start () {
		ReloadXZaman = 5;

		MermiKutu.SetActive (true);
		EText.SetActive (false);
		ETextKapan = false;

		SilahAnim = GetComponent<Animator> ();
		SilahSesKaynak = GetComponent<AudioSource> ();
		SilahSesKaynak.clip = SilahSes;

		ToplamMermiText.text = ToplamMermi.ToString ();
		MermiText.text = KalanMermi.ToString ();
	}
	void Update(){
		Kontroller ();
		SilahAnim.SetFloat ("Hiz",Karakter.velocity.magnitude);
		if (Physics.Raycast (HitCıkış.transform.position, HitCıkış.transform.forward, out Hit, 2f) && Hit.collider.tag == "Mermi") {
			EText.SetActive (true);
		}
		else if(Physics.Raycast (HitCıkış.transform.position, HitCıkış.transform.forward, out Hit, Mesafe))
		{
			EText.SetActive (false);
		}
		if(ETextKapan){
			EText.SetActive (false);
		}
		if(Reload){
			if (ReloadZaman > ReloadXZaman) {
				float gerekliMermi = SarjorMermi - KalanMermi;
				if (gerekliMermi > ToplamMermi) {
					KalanMermi += ToplamMermi;
					ToplamMermi = 0;
					ToplamMermiText.text = ToplamMermi.ToString ();
					MermiText.text = KalanMermi.ToString ();
				}
				else
				{
					KalanMermi = SarjorMermi;
					ToplamMermi -= gerekliMermi;
				}
				Reload = false;
			}
			else
			{
				ReloadZaman += Time.deltaTime;
			}
		}
	}


	private void Fire(){
		if(Physics.Raycast(HitCıkış.transform.position,HitCıkış.transform.forward,out Hit,Mesafe)){
			muzzleFlash.Play ();
			SilahSesKaynak.Play ();
			SilahAnim.Play ("Fire", -1, 0f);
			KalanMermi = KalanMermi - 1;
			ToplamMermiText.text = ToplamMermi.ToString ();
			MermiText.text = KalanMermi.ToString ();
			Instantiate (MermiziEfekt,Hit.point,Quaternion.LookRotation(Hit.normal));
		}
	}
	void Kontroller(){
		if(Input.GetKey(KeyCode.Mouse0) && KameraAtes && Time.time > SilahBekleme && !Reload && KalanMermi > 0){
			Fire ();
			SilahBekleme = Time.time + SilahBeklemeSuresi;
		}
		if (Input.GetKeyDown (KeyCode.F) && !Fener.activeSelf) {
			Fener.SetActive (true);
		}
		else if(Input.GetKeyDown (KeyCode.F) && Fener.activeSelf)
		{
			Fener.SetActive (false);
		}
		if(Input.GetKeyDown(KeyCode.R) && !Reload && ToplamMermi > 0){
			Reload = true;
			ReloadZaman = 0;
		}
		if(Input.GetKeyDown(KeyCode.E) && EText.activeSelf){
			ETextKapan = true;
			EText.SetActive (false);
			MermiKutu.SetActive (false);
			ToplamMermi = ToplamMermi + 60;
			ToplamMermiText.text = ToplamMermi.ToString ();
			MermiText.text = KalanMermi.ToString ();
		}
	}
}
