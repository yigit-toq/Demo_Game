using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter_Hareket : MonoBehaviour
{
    public Oyun_Mekanizmasi oyunMekanizmasi { get; set; }

    public float Hiz;
    public float yerCekimi;
    public float ziplamaHizi;

    public float x { get; set; }
    public float z { get; set; }

    public Vector3 Hareket;

    public Rigidbody karakterRigid { get; set; }
    private CharacterController characterController;

    private void Start()
    {
        oyunMekanizmasi = FindObjectOfType<Oyun_Mekanizmasi>();

        karakterRigid = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();

        Hareket = Vector3.zero;
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            Hareket = transform.right * x + transform.forward * z;
        }

        Hareket.y -= yerCekimi * Time.deltaTime; 
        characterController.Move(Hareket * Hiz * Time.deltaTime);
    }
}
