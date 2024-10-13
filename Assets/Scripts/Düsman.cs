using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Düsman : MonoBehaviour {

	private static Düsman dusmanAcik;
	public static Düsman EntityCode {
		get{
			if(dusmanAcik == null){
				dusmanAcik = GameObject.FindObjectOfType<Düsman> ();
			}
			return dusmanAcik;
		}
	}
	public LayerMask KarakterLayer;
	public float hiz;
	public float alevlihiz;
	public float sonhiz;
	public float düsmanCan;
	public float itmeKuvveti;
	public bool gordunmu;
	public bool düsmanÖldü;
	public bool Alevlen;
	public bool Firla;
	public bool temas_halinde;
	private Animator kuruKafaAnimator;
	public Vector2 yon;

	void Start () {
		Firla = false;
		düsmanÖldü = false;
		düsmanCan = 100;
		temas_halinde = false;
		kuruKafaAnimator = GetComponent<Animator> ();
		sonhiz = hiz;
		if (transform.localScale.x > 0) {
			hiz = hiz * -1;
		}
	}
	void Update(){
		if(düsmanCan <= 0){
			düsmanCan = 0;
			Karakter.PlayerCode.ölüSayisi = Karakter.PlayerCode.ölüSayisi + 1;
			gameObject.SetActive (false);
		}
		if(Karakter.PlayerCode.ittirme && Karakter.PlayerCode.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("atak")){
			transform.position += new Vector3 ((sonhiz * itmeKuvveti * -1) * Time.deltaTime, 0, 0);
		}
	}
	void FixedUpdate(){
		if((!temas_halinde || !gordunmu) && !Firla && !Karakter.PlayerCode.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("hasarAlma")){
		transform.position += new Vector3 (sonhiz * Time.deltaTime,0,0);
		}
		gordunmu = Bizi_Goruyormu();
		Dusman_Hizlimi ();
	}
	bool Bizi_Goruyormu(){
		if (transform.localScale.x > 0) {
			yon = Vector2.left;
		} else {
			yon = Vector2.right;
		}
		RaycastHit2D kurukafaHit = Physics2D.Raycast (transform.position,yon,10,KarakterLayer);
		if(kurukafaHit.collider == null){
			return false;
		}
		return true;
	}
	private void Dusman_Hizlimi(){
		if (gordunmu) {
			sonhiz = hiz * alevlihiz;
			kuruKafaAnimator.SetBool ("alevlen", true);
		} else {
			sonhiz = hiz;
			kuruKafaAnimator.SetBool ("alevlen", false);
		}
	}
	public void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			temas_halinde = true;
		}
		if (other.gameObject.tag == "Player" && Karakter.PlayerCode.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("atak") && !Karakter.PlayerCode.ittirme) {
			Firla = true;
		}
		if (other.gameObject.tag == "Player" && Firla) {
			transform.position += new Vector3 ((sonhiz * itmeKuvveti) * Time.deltaTime, 0, 0);
		}
		if (other.gameObject.tag == "Player" && Karakter.PlayerCode.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("atak")) {
			düsmanCan = düsmanCan - 20;
		}
	}
	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			temas_halinde = false;
			Firla = false;
		}
	}
}