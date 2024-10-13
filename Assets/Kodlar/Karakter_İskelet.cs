using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter_İskelet : MonoBehaviour
{
	private static Karakter_İskelet karakterİskelet;
	public static Karakter_İskelet karakterİskeletKod
    {
        get
        {
            if (karakterİskelet == null)
            {
				karakterİskelet = GameObject.FindObjectOfType<Karakter_İskelet> ();
            }
			return karakterİskelet;
        }
    }

	public Transform ZeminHit;
	public LayerMask ZeminLayer;

	public float Hiz;
	public float ZiplamaKuvveti;

	private float yatayHareket;

	public bool Atak;
	public bool Zipla;
	public bool Zeminde;

	private bool SagaBak;
	private bool HavadaKontrol;

	public Rigidbody2D MyRigidbody { get; set; }
	public Animator MyAnimator { get; set; }

    void Start()
    {
		MyRigidbody = GetComponent<Rigidbody2D> ();
		MyAnimator = GetComponent<Animator> ();

		HavadaKontrol = false;
		SagaBak = true;
    }
		
    void Update()
    {
		Kontroller ();
	}

	void FixedUpdate ()
    {
		yatayHareket = Input.GetAxis("Horizontal");

		Hareket_Katmanlari ();

		Zemin_Ustunde ();

		Yon_Cevir (yatayHareket);

		TemelHareketler (yatayHareket);
	}

	void TemelHareketler(float yatay) 
	{
        if (MyRigidbody.velocity.y < 0)
        {
			MyAnimator.SetBool ("Dusme",true);
        }

        if (Zeminde && Zipla)
        {
			MyRigidbody.velocity += new Vector2 (0, ZiplamaKuvveti);
			Zeminde = false;
		}

        if (!MyAnimator.GetCurrentAnimatorStateInfo(0).IsName ("Karakter_Atak"))
        {
			MyRigidbody.velocity = new Vector2(yatay * Hiz, MyRigidbody.velocity.y);
			MyAnimator.SetFloat("Yatay", Mathf.Abs(yatayHareket));
		}
	}

	void Yon_Cevir(float yatay)
	{
		if (yatay > 0 && !SagaBak || yatay < 0 && SagaBak)
        {
			SagaBak = !SagaBak;
			Vector3 yon = gameObject.transform.localScale;
			yon.x *= -1;
			gameObject.transform.localScale = yon;
        }
    }

	void Kontroller ()
    {
		if (Input.GetKeyDown(KeyCode.W)) 
		{
			MyAnimator.SetTrigger("Zipla");
        }

        if (Input.GetMouseButton(0))
        { 
				MyAnimator.SetTrigger("Atak");
        }
    }

	void Zemin_Ustunde ()
    {
		RaycastHit2D ZeminRaycast = Physics2D.Raycast(ZeminHit.position, Vector2.down, 0.1f, ZeminLayer);

        if (ZeminRaycast.collider != null)
        {
			Zeminde = true;
        }
        else
        {
			Zeminde = false;
        }
    }

	void Hareket_Katmanlari ()
    {
        if (!Zeminde)
        {
			MyAnimator.SetLayerWeight (1, 1);
			HavadaKontrol = true;
        }
        else
        {
			MyAnimator.SetLayerWeight(1, 0);
			HavadaKontrol = false;
		}
    }
}
