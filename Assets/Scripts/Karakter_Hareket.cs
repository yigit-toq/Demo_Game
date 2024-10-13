using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Karakter_Hareket : MonoBehaviour
{
    private static Karakter_Hareket karakterKod;
    public static Karakter_Hareket karakter
    {
        get
        {
            if (karakterKod == null)
            {
                karakterKod = FindObjectOfType<Karakter_Hareket>();
            }
            return karakterKod;
        }
    }

    //[Header("ReloadSystem")]
    //private float ReloadXZaman;
    //public float ReloadZaman;
    //public float ToplamMermi;
    //public float SarjorMermi;
    //public float KalanMermi;
    //public bool Reload;

    //public Text ToplamMermiText;
    //public Text MermiText;

    public bool Zipla;

    public bool zeminde;

    public bool havadaKontrol;

    public bool sagaBak;

    public float hiz;

    public float ZiplamaKuvveti;

    public float TemasCapi;

    public int can;

    private float yatay;

    public LayerMask HangiZemin;

    public Transform[] Temas_Noktalari;

    public Rigidbody2D karakterRigid { get; set; }
    public Animator karakterAnimator { get; set; }

    public Animator canBarAnim;
    

    void Start ()
    {
        karakterRigid = GetComponent<Rigidbody2D> ();
        karakterAnimator = GetComponent<Animator>();

        //ToplamMermiText.text = ToplamMermi.ToString();
        //MermiText.text = KalanMermi.ToString();

        //ReloadXZaman = 3;

        can = 3;

        sagaBak = true;
    }

    void Update()
    {
        Kontroller ();

        //Reload_İslemi ();
    }

    void FixedUpdate ()
    {
        yatay = Input.GetAxis("Horizontal");

        zeminde = Zeminde ();

        Temel_Hareketler (yatay);

        Yon_Cevir (yatay);
    }

    private void Temel_Hareketler (float yatay)
    {
        if (Zipla)
        {
            zeminde = false;

            karakterRigid.AddForce(new Vector2(0, ZiplamaKuvveti * 100));

            Zipla = false;
        }

        karakterRigid.velocity = new Vector2 (yatay * hiz, karakterRigid.velocity.y);

        karakterAnimator.SetFloat("KarakterHiz", Mathf.Abs(yatay));
        canBarAnim.SetInteger("Can", can);
    }

    private void Yon_Cevir (float yatay)
    {
        if (yatay > 0 && !sagaBak || yatay < 0 && sagaBak)
        {
            sagaBak = !sagaBak;

            Vector3 yon = transform.localScale;

            yon.x *= -1;

            transform.localScale = yon;
        }
    }

    private void Kontroller ()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !Zipla && zeminde)
        {
            Zipla = true;
        }

        //if (Input.GetKeyDown(KeyCode.R) && !Reload && ToplamMermi > 0 && KalanMermi < 30)
        //{
        //    Reload = true;
        //    ReloadZaman = 0;
        //}
    }

    private bool Zeminde()
    {
        if (karakterRigid.velocity.y <= 0)
        {
            foreach (Transform nokta in Temas_Noktalari)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(nokta.position, TemasCapi, HangiZemin);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    //void Reload_İslemi ()
    //{
    //    if (Reload)
    //    {
    //        if (ReloadZaman > ReloadXZaman)
    //        {
    //            float gerekliMermi = SarjorMermi - KalanMermi;
    //            if (gerekliMermi > ToplamMermi)
    //            {
    //                KalanMermi += ToplamMermi;
    //                ToplamMermi = 0;
    //                ToplamMermiText.text = ToplamMermi.ToString();
    //                MermiText.text = KalanMermi.ToString();
    //            }
    //            else
    //            {
    //                KalanMermi = SarjorMermi;
    //                ToplamMermi -= gerekliMermi;
    //            }
    //            Reload = false;
    //        }
    //        else
    //        {
    //            ReloadZaman += Time.deltaTime;
    //        }
    //    }
    //}
}
