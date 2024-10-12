using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour
{

    private float x_Hareket;
    private float z_Hareket;

    public float hiz;
    public float damp;

    [Range(1, 20)]
    public float dönmeHizi;

    public Transform karakterModel;

    public Vector3 StickDirection;

    public Joystick joystick;

    private Camera kamera;

    public Rigidbody karakterRigid { get; set; }
    public Animator karakterAnim { get; set; }

    void Start ()
    {
        karakterRigid = GetComponent<Rigidbody>();
        karakterAnim = GetComponent<Animator>();
        karakterModel = GetComponent<Transform>();

        kamera = Camera.main;
    }

    void LateUpdate ()
    {
        //x_Hareket = Input.GetAxis("Horizontal");
        //z_Hareket = Input.GetAxis("Vertical");

        x_Hareket = joystick.Horizontal;
        z_Hareket = joystick.Vertical;

        StickDirection = new Vector3(x_Hareket, 0, z_Hareket);

        InputMove();
        InputRotation();
    }

    void InputMove()
    {
        karakterAnim.SetFloat("HareketHizi", Vector3.ClampMagnitude(StickDirection, 1).magnitude, damp, Time.deltaTime * 10);

        transform.position += transform.forward * Mathf.Abs(x_Hareket) * hiz;
        transform.position += transform.forward * Mathf.Abs(z_Hareket) * hiz;

        if (Mathf.Abs(x_Hareket) + Mathf.Abs(z_Hareket) > 1)
        {
            hiz = 0.10f;
        }
        else
        {
            hiz = 0.20f;
        }
    }

    void InputRotation()
    {
        Vector3 rotationOfset = kamera.transform.TransformDirection(StickDirection);
        rotationOfset.y = 0;

        karakterModel.forward = Vector3.Slerp(karakterModel.forward, rotationOfset, Time.deltaTime * dönmeHizi);
    }
}
