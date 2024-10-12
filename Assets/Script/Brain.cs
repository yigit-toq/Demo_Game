using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public Character karakter;

    private Vector3 Hedef;
    private Vector3 Difference;

    public GameObject Crosshair;
    public GameObject Player;
    public GameObject BulletPrefab;

    public AudioSource Shot;

    public float RotationZ;
    public float BulletSpeed;

    private void Start()
    {
        karakter = FindObjectOfType<Character>();

        Cursor.visible = false;
        BulletSpeed = 50;
    }

    private void Update()
    {
        Kontroller();
    }

    private void LateUpdate()
    {
        Hedef = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Crosshair.transform.position = new Vector2(Hedef.x, Hedef.y);

        Difference = Hedef - Player.transform.position;

        RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;

        Player.transform.rotation = Quaternion.Euler(0, 0, RotationZ + 45);
    }

    private void Kontroller()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float Distance = Difference.magnitude;

            Vector2 Direction = Difference / Distance;

            Direction.Normalize();

            FireBullet(Direction, RotationZ);
        }
    }

    private void FireBullet(Vector2 Direction, float BulletRotation)
    {
        foreach (Transform transform in karakter.bulletTrans)
        {
            for (int i = 0; i < karakter.bulletTrans.Length - 1; i++)
            {
                GameObject bullet = Instantiate(BulletPrefab) as GameObject;

                Shot.Play();

                bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, BulletRotation));
                bullet.GetComponent<Rigidbody2D>().velocity = Direction * BulletSpeed;

                Destroy(bullet, 3);
            }
        }
    }
}
