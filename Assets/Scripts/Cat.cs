using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Rigidbody2D CatRigidbody;
    private Animator CatAnimator;

    public float speed;

    public bool zeminde;

    public bool move;

    //Hit Gönder
    public float distance;

    private float range;

    public Transform forwardHit;
    public Transform bottomHit;
    public Transform characterTransform;

    public LayerMask hitLayer;

    private RaycastHit2D characterHit;
    private RaycastHit2D groundHit;

    //Yön Çevir
    public bool lookingRight;

    private Vector3 rotation;

    private void Awake()
    {
        CatRigidbody = GetComponent<Rigidbody2D>();
        CatAnimator = GetComponent<Animator>();

        lookingRight = true;

        range = 50;
    }

    private void Update()
    {
        groundHit = Physics2D.Raycast(bottomHit.position, Vector2.down, 5);

        forwardHit.position = new Vector2(forwardHit.position.x, characterTransform.position.y);

        if (groundHit.collider != null)
        {
            zeminde = true;
        }
        else
        {
            zeminde = false;
        }

        if (characterTransform.position.x > transform.position.x)
        {
            lookingRight = true;

            range = 50;
        }
        else
        {
            lookingRight = false;

            range = -50;
        }

        characterHit = Physics2D.Raycast(forwardHit.position, Vector2.right, range, hitLayer);

        distance = characterHit.distance;

        if (zeminde)
        {
            CatAnimator.SetFloat("Speed", speed);
        }
        else
        {
            CatAnimator.SetFloat("Speed", 0);
        }

        if (distance > 12)
        {
            speed = 6f;
        }
        else if (distance > 3)
        {
            speed = 3f;

            move = true;
        }
        else
        {
            speed = 0;

            move = false;
        }

        Turn();
    }

    private void FixedUpdate()
    {
            if (zeminde)
            {
                /*
                if (lookingRight)
                {
                    transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
                }
                else
                {
                    transform.position -= new Vector3(speed * Time.fixedDeltaTime, 0, 0);
                }
                */

                transform.position = Vector2.MoveTowards(transform.position, characterTransform.position, speed * Time.fixedDeltaTime);
            }          
    }

    private void Turn()
    {
        rotation = transform.eulerAngles;

        if (lookingRight)
        {
            rotation.y = 0;
        }
        else
        {
            rotation.y = 180;
        }

        transform.eulerAngles = rotation;
    }
}
