using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Character Karakter;

    public Transform BulletTransform;

    private Transform PlayerTransform;

    private Rigidbody2D EnemyRigidbody;

    private AudioSource EnemyAudioSource;

    public GameObject BulletPrefab;

    private Vector2 Movement;

    private Vector3 Hedef, Difference;

    public float Süre, MaxSüre;
    private float Distance, AttackDistance;
    private float MoveSpeed;
    private float BulletSpeed;
    private float EnemyHeal;

    private bool Fire;

    private void Awake()
    {
        Karakter = FindObjectOfType<Character>();

        PlayerTransform = GameObject.Find("Karakter").GetComponent<Transform>();

        EnemyRigidbody = GetComponent<Rigidbody2D>();
        EnemyAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BulletSpeed = 30;
        EnemyHeal = 100;
        MaxSüre = 1;

        MoveSpeed = Random.Range(4, 8);
        AttackDistance = Random.Range(10, 12);

        Fire = false;
    }

    private void Update()
    {
        Vector3 Direction = PlayerTransform.transform.position - transform.position;

        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        Difference = Hedef - transform.position;

        EnemyRigidbody.rotation = angle - 135;

        Direction.Normalize();

        Movement = Direction;
    }

    private void FixedUpdate()
    {
        MoveEnemy(Movement);
    }

    void MoveEnemy(Vector2 Direction)
    {
        Distance = Vector3.Distance(PlayerTransform.transform.position, transform.position);

        if (Distance >= AttackDistance)
        {
            EnemyRigidbody.MovePosition((Vector2) transform.position + (MoveSpeed * Time.deltaTime * Direction));
        }
        else
        {
            float bulletRotation = EnemyRigidbody.rotation - 45;

            Vector2 BulletDif = Difference / Distance;

            BulletDif.Normalize();

            FireBullet(BulletDif, bulletRotation);
        }
    }

    private void FireBullet(Vector2 Direction, float BulletRotation)
    {
        if (MaxSüre > Süre)
        {
            Süre += Time.deltaTime;
        }
        else
        {
            Fire = true;

            MaxSüre = Random.Range(0.5f, 1f);

            Süre = 0;
        }

        if (Fire)
        {
            GameObject bullet = Instantiate(BulletPrefab) as GameObject;

            EnemyAudioSource.Play();

            bullet.transform.SetPositionAndRotation(BulletTransform.position, Quaternion.Euler(0, 0, BulletRotation));
            bullet.GetComponent<Rigidbody2D>().velocity = Direction * BulletSpeed;

            Destroy(bullet, 3);

            Fire = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            EnemyHeal -= Random.Range(50, 20);

            if (EnemyHeal <= 0)
            {
                Karakter.Skor++;

                Karakter.SkorText.text = Karakter.Skor.ToString();

                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
        }
    }
}
