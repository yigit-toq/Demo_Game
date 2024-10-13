using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Baslangic : MonoBehaviour
{
    public AudioSource GecisSesi;

    public void Sahne_Gecis ()
    {
        SceneManager.LoadScene(1);
    }

    public void GecisSes ()
    {
        GecisSesi.Play();
    }
}
