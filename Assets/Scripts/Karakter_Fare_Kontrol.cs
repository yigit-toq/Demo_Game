using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter_Fare_Kontrol : MonoBehaviour
{
    private float yRotasyon;

    public float minY;
    public float maxY;
    public float fareHassasiyet;

    public Transform karakterVücut;

    void Start()
    {
        yRotasyon = 0;
    }

    void Update()
    {
        float fareX = Input.GetAxis("Mouse X") * fareHassasiyet * Time.deltaTime;
        float fareY = Input.GetAxis("Mouse Y") * fareHassasiyet * Time.deltaTime;

        yRotasyon -= fareY;

        yRotasyon = Mathf.Clamp(yRotasyon, minY, maxY);
        transform.localRotation = Quaternion.Euler(yRotasyon, 0f, 0f);
        karakterVücut.Rotate(Vector3.up * fareX);
    }
}
