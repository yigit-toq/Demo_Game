using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour {

	private static Demon demonOrnek;
	public static Demon DemonCode{
		get{
			if(demonOrnek == null){
				demonOrnek = GameObject.FindObjectOfType<Demon>();
			}
			return demonOrnek;
		}	
	}
	public GameObject DemonKonusmaKutusu;
	public GameObject DemonKonusma1;
	public GameObject DemonKonusma2;
	public GameObject DemonKonusma3;
	public GameObject KarakterKonusma3;
	public GameObject KarakterKonusma4;
	public GameObject DemonEngel;
	public Transform rayCast;
	public BoxCollider2D KarakterCollider;
	public BoxCollider2D DemonCollider;
	public bool demonGordü;
	public bool atesPüskürt;
	public bool degiyormu;
	public float bekle;
	public float demonCan;
	private Animator demonAnim;
	private RaycastHit2D demonHit;

	void Start () {
		gameObject.SetActive (true);
		demonCan = 600;
		degiyormu = false;
		DemonEngel.SetActive (true);
		demonAnim = GetComponent<Animator> ();
		atesPüskürt = false;

	}
	void Update(){
		if (DemonEngel.activeSelf && Karakter.PlayerCode.demonGeldimi) {
			DemonEngel.SetActive (false);
			StartCoroutine (Demon_Konusma ());
		}
		if(bekle > 5.2f){
			bekle = Random.Range(0,2);
		}
	}
	void FixedUpdate () {
		demonGordü = Demon_Gordumu ();
		if(demonGordü && demonCan > 0){
			bekle = bekle + Random.Range (0.04f,0.06f);
			demonAnim.SetFloat("DemonAtak",bekle);
		}
		if(degiyormu && (Karakter.PlayerCode.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("atak") || Karakter.PlayerCode.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("egilipatak") || Karakter.PlayerCode.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("ziplatak"))){
			demonCan = demonCan - 8;
		}
		if(demonCan <= 0 && !demonAnim.GetCurrentAnimatorStateInfo(0).IsName("DemonAttack")){
			demonCan = 0;
			transform.position += new Vector3 (0, 8 * Time.deltaTime, 0);
		}
	}
	private bool Demon_Gordumu(){
		demonHit = Physics2D.Raycast (rayCast.position,Vector2.left,8);
		if (demonHit.collider == KarakterCollider) {
			return true;
		}
		return false;
	}
	IEnumerator Demon_Konusma(){
		DemonKonusmaKutusu.SetActive (true);
		DemonKonusma1.SetActive (true);
		yield return new WaitForSeconds (2);
		DemonKonusma1.SetActive (false);
		KarakterKonusma3.SetActive (true);
		yield return new WaitForSeconds (2);
		KarakterKonusma3.SetActive (false);
		DemonKonusma2.SetActive (true);
		yield return new WaitForSeconds (2);
		KarakterKonusma4.SetActive (true);
		DemonKonusma2.SetActive (false);
		yield return new WaitForSeconds (2);
		KarakterKonusma4.SetActive (false);
		DemonKonusma3.SetActive (true);
		yield return new WaitForSeconds (2);
		DemonKonusma3.SetActive (false);
		DemonKonusmaKutusu.SetActive (false);
		Karakter.PlayerCode.demonGeldimi = false;
	}
	public void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			degiyormu = true;
		}
	}
	public void OnCollisionExit2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			degiyormu = false;
		}
	}
	public void Hasar_Ver(){
		if (degiyormu) {
			Karakter.PlayerCode.can = Karakter.PlayerCode.can - Random.Range (30, 35);
		}
	}
}
