using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	private RaycastHit chestHit;
	public LayerMask chestLayer;
	public GameObject E_Text;
	public Animator chestAnim;
	public AudioSource chestSound;

	public float hitUzunluk;

	private bool chest;
	private bool chestOpen;

	void Start () 
	{
		chestOpen = false;
	}

	void Update () 
	{
		Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out chestHit, hitUzunluk, chestLayer);

		if (chestHit.collider != null && !chestOpen) 
		{
			chest = true;
		} 
		else 
		{
			chest = false;
		}

		if (chest) 
		{
			E_Text.SetActive (true);

			if(Input.GetKeyDown(KeyCode.E))
			{
				chestAnim.SetTrigger ("open");
				chestSound.Play ();
				chestOpen = true;
			}
		}
		else
		{
			E_Text.SetActive (false);
		}
	}
}
