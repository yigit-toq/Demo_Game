using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oyun_Mekanizmasi : MonoBehaviour
{
    public Karakter_Hareket karakter { get; set; }

    public bool Basla;

    public bool YokEt;

    public bool YokEdildi;

    public bool Basladi { get; set; }

    public AudioSource puppaSound;
    public AudioSource shotSound;

    private Animator puppaAnimator;

    void Start()
    {
        karakter = FindObjectOfType<Karakter_Hareket>();

        puppaAnimator = GetComponent<Animator>();

        YokEdildi = false;
    }

    void Update()
    {
        if (Basla == true)
        {
            StartCoroutine(GreenLightRedLight());

            Basla = false;
            Basladi = true;
        }

        //Yok_Etme_Ýþlemi ();
    }

    IEnumerator GreenLightRedLight()
    {
        puppaAnimator.SetTrigger("Turn Back");

        puppaSound.Play();

        yield return new WaitForSeconds(5);

        puppaAnimator.SetTrigger("Turn Forward");

        StartCoroutine(Hareket_Eden_Ölür());
    }

    IEnumerator Hareket_Eden_Ölür()
    {
        YokEt = true;

        yield return new WaitForSeconds(10);

        YokEt = false;

        StartCoroutine(GreenLightRedLight());
    }

    void Yok_Etme_Ýþlemi()
    {
        if (YokEt == true)
        {
            if (karakter.karakterRigid.velocity.x > 0 || karakter.karakterRigid.velocity.z > 0)
            {
                YokEdildi = true;
            }
        }
    }
}
