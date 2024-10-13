using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiZeka : MonoBehaviour {

	public float mesafe;
	public Transform hedef;
	private Animator zombiAnim;
	private NavMeshAgent agent;

	void Start () {
		zombiAnim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent> ();
	}
	void Update () {

	}
	void FixedUpdate(){
		zombiAnim.SetFloat ("hiz",agent.speed);
		mesafe = Vector3.Distance (transform.position,hedef.position);
		Temel_Hareketler ();
	}
	void Temel_Hareketler(){
		if (mesafe <= 20 && mesafe > 3) {
			agent.speed = 5;
			agent.destination = hedef.position;
		} else if (mesafe > 3 && mesafe < 30) {
			agent.speed = 1;
			agent.destination = hedef.position;
		}
		if (mesafe > 30 || mesafe < 3) {
			agent.speed = 0;
			agent.destination = hedef.position;
		}
		if (mesafe < 3) {
			zombiAnim.SetBool ("atak",true);
		}
		else
		{
			zombiAnim.SetBool ("atak",false);
		}
	}
}
