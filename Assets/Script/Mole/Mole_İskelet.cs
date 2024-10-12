using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole_İskelet : MonoBehaviour {

	public float Hiz;
	public float Yatay;
	public float Dikey;
	public float Yon;
	public float yürümeSınır;
	public float yürümeSüre;

	public Vector2 Hareket;

	private Animator moleAnimator;
	private Rigidbody2D moleRigidbody;

	void Start ()
	{
		moleAnimator = GetComponent<Animator> ();
		moleRigidbody = GetComponent<Rigidbody2D> ();

		Hiz = 2.5f;
		yürümeSınır = Random.Range (100, 300);
		StartCoroutine (HareketEtme());
	}
		
	void FixedUpdate ()
	{
		Hareket = new Vector2 (Yatay, Dikey);

		if (yürümeSüre < yürümeSınır)
		{
			yürümeSüre++;

			moleRigidbody.MovePosition (moleRigidbody.position + Hareket * Hiz * Time.fixedDeltaTime);

			moleAnimator.SetFloat ("Horizontal", Yatay);
			moleAnimator.SetFloat ("Vertical", Dikey);
			moleAnimator.SetFloat ("Hiz", Hareket.sqrMagnitude);
		}
		else
		{
			StartCoroutine (HareketEtme());
			yürümeSınır = Random.Range (100, 300);
			yürümeSüre = 0;
		}
	}

	IEnumerator HareketEtme ()
	{
		Yon = Random.Range (-2, 2);

		moleAnimator.SetFloat ("Yon", Yon);

		if (Yon <= -1.5f && Yon >= -2f)
		{
			Yatay = -1;
			Dikey = 0;
		}

		if (Yon > -1.5f && Yon < 0f)
		{
			Dikey = -1;
			Yatay = 0;
		}

		if (Yon > 0f && Yon < 1.5f)
		{
			Dikey = 1;
			Yatay = 0;
		}

		if (Yon >= 1.5f && Yon <= 2f)
		{
			Yatay = 1;
			Dikey = 0;
		}

		if (Yon ==  0f)
		{
			Yatay = 0;
			Dikey = 0;
		}

		yield return null;
	}
}
