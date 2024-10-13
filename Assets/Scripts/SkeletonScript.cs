using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour {

	public Transform solLimit;
	public Transform sagLimit;
	public Transform rayCast;
	public LayerMask rayCastMask;
	public float rayCastUzunluk;
	public float atakMesafe;
	public float hareketHizi;
	public float timer;
	public bool menzilde;

	private RaycastHit2D hit;
	private Transform hedef;
	private Animator iskeletAnim;
	private float mesafe;
	private bool atakModu;
	private bool cooling;
	private float intTimer;

	void Awake(){
		menzilde = false;
		HedefSecme ();
		intTimer = timer;
		iskeletAnim = GetComponent<Animator> ();
	}
	void Update () {
		if(!atakModu){
			Hareket ();
		}
		if(!Sınırların_içinde() && !menzilde && !iskeletAnim.GetCurrentAnimatorStateInfo(0).IsTag("attack")){
			HedefSecme ();
		}
		if (menzilde) {
			hit = Physics2D.Raycast (rayCast.position, transform.right, rayCastUzunluk, rayCastMask);
			RaycastDebugger ();
		}
		else
		{
			AtakDur ();
		}
		if (hit.collider != null) {
			iskeletMantik ();
		}
		else if (hit.collider == null)
		{
			menzilde = false;
		}
	}
	void RaycastDebugger(){
		if (mesafe > atakMesafe) {
			Debug.DrawRay (rayCast.position, transform.right * rayCastUzunluk,Color.red);
		}
		else
		{
			Debug.DrawRay (rayCast.position, transform.right * rayCastUzunluk,Color.green);
		}
	}
	void iskeletMantik(){
		mesafe = Vector2.Distance (transform.position,hedef.transform.position);
		if (mesafe > atakMesafe) {
			AtakDur ();
		}
		else if (atakMesafe >= mesafe && !cooling)
		{
			Atak ();
		}
		if(cooling){
			Cooldown ();
			iskeletAnim.ResetTrigger ("atak");
		}
	}
	void Hareket(){
		iskeletAnim.SetBool ("yürü",true);
		if(!iskeletAnim.GetCurrentAnimatorStateInfo(0).IsTag("attack")){
			Vector2 hedefPozisyon = new Vector2 (hedef.transform.position.x,transform.position.y);
			transform.position = Vector2.MoveTowards (transform.position,hedefPozisyon,hareketHizi * Time.deltaTime);
		}
	}
	void Atak(){
		timer = intTimer;
		atakModu = true;
		iskeletAnim.SetBool ("yürü",false);
		iskeletAnim.SetTrigger ("atak");
	}
	void AtakDur(){
		cooling = false;
		atakModu = false;
		iskeletAnim.ResetTrigger ("atak");
	}
	public void TriggerCooling(){
		cooling = true;
	}
	void Cooldown ()
	{
		timer -= Time.deltaTime;

		if (timer <= 0 && cooling && atakModu) {
			cooling = false;
			timer = intTimer;
		}
	}
	private bool Sınırların_içinde(){
		return transform.position.x > solLimit.position.x && transform.position.x < sagLimit.position.x;
	}
	private void HedefSecme(){
		float mesafe_sol = Vector2.Distance (transform.position,solLimit.position);
		float mesafe_sag = Vector2.Distance (transform.position,sagLimit.position);

		if (mesafe_sol > mesafe_sag) {
			hedef = solLimit;
		}
		else
		{
			hedef = sagLimit;
		}
		Yon_Cevirme ();
	}
	private void Yon_Cevirme(){
		Vector3 yon = transform.eulerAngles;
		if (transform.position.x > hedef.position.x) {
			yon.y = 180f;
		}
		else
		{
			yon.y = 0f;
		}
		transform.eulerAngles = yon;
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			hedef = other.transform;
			menzilde = true;
			Yon_Cevirme ();
		}
	}
}
