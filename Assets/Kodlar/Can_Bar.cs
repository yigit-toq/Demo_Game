using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can_Bar : MonoBehaviour
{
    public Transform Karakter;

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;

    void LateUpdate () 
    {
        transform.position = new Vector2 (Mathf.Clamp (Karakter.position.x, xMin, xMax), Mathf.Clamp (Karakter.position.y, yMin, yMax));
    }
}
