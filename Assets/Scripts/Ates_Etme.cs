using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ates_Etme : MonoBehaviour
{
    public bool Fire;

    public float fireWait;

    private float fireTime;

    public GameObject Mermi;

    void Update ()
    {
        Kontroller ();
    }

    void FixedUpdate ()
    {
        Ates_Et ();
    }

    void Kontroller ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Fire = true;
        }
        else
        {
            Fire = false;
        }
    }

    void Ates_Et()
    {
        if (fireTime < fireWait)
        {
            fireTime++;
        }
        else if (Fire)
        {
            Instantiate(Mermi, transform.position, Quaternion.identity);
            fireTime = 0f;
        }
    }
}
