using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombi : MonoBehaviour {

	public float zombiCan;
	public float mesafe;
	private float zombiölümSüre;
	public Transform hedef;
	private NavMeshAgent agent;
	private Animator zombiAnim;

	void Start () {
		zombiAnim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		hedef = GameObject.FindGameObjectWithTag ("Player").transform;
		zombiCan = 100f;
		zombiölümSüre = 100;
	}
	void Update () {

	}

	void FixedUpdate () {
		Hareket ();
		zombiAnim.SetFloat ("can",zombiCan);
	}
	void Hareket () {
		mesafe = Vector3.Distance (transform.position,hedef.position);
		zombiAnim.SetFloat ("speed",agent.speed);
		if (zombiCan > 0) {
			if (mesafe > 50) {
				agent.speed = 0;
			}
			if (!zombiAnim.GetCurrentAnimatorStateInfo (0).IsTag ("atak")) {
				if (mesafe < 40 && mesafe > 20) {
					agent.speed = 3;
					agent.destination = hedef.position;
				}
				if (mesafe < 20 && mesafe > 3) {
					agent.speed = 6;
					agent.destination = hedef.position;
				}
			}
			if (mesafe < 3) {
				agent.speed = 0;
				zombiAnim.SetTrigger ("attack");
			} 
			else
			{
				zombiAnim.ResetTrigger ("attack");
			}
	    }
	    else if (zombiCan <= 0)
	    {
		    zombiCan = 0;
		    zombiölümSüre = zombiölümSüre - 0.05f;
		    if(zombiölümSüre <= 0){
		    Destroy (gameObject);
		    }
	    }
    }
}