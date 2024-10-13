using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helibot : MonoBehaviour
{
    public Transform ateslemeBolgesi;

    public GameObject Mermi;

    public float fireTime;
    public float fireWait;
    
    void Start ()
    {

    }

    void FixedUpdate ()
    {
        Ates_Et ();
    }

    void Ates_Et()
    {
        if (fireTime < fireWait)
        {
            fireTime++;
        }
        else
        {
            Instantiate(Mermi, ateslemeBolgesi.position, Quaternion.identity);
            fireTime = 0f;
        }
    }
}
