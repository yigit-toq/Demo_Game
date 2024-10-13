using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azrail : MonoBehaviour
{
    private static Azrail azrail;
    public static Azrail azrailScript
    {
        get
        {
            if (azrail == null)
            {
                azrail = GameObject.FindObjectOfType<Azrail>();
            }
            return azrail;
        }
    }

    public Animator EnemyAnimator { get; set; }
    private Rigidbody2D EnemyRigidbody;

    [SerializeField]
    private float EnemyHeal, EnemySpeed, Distance, AttackDistance, timer;

    [SerializeField]
    private bool inRange, cooling, attackMod, lookingRight;

    public bool damaged, die, run, attack;

    [SerializeField]
    private Transform leftLimit, rightLimit, target;

    [SerializeField]
    private float raycastLenght;

    [SerializeField]
    private Transform raycastTransform;

    [SerializeField]
    private LayerMask raycastMask;
    [SerializeField]
    private RaycastHit2D enemyHit;


    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        EnemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        die = false;
        damaged = false;

        SelectTarget();
    }

    private void Update()
    {
        RaycastHit();

        RaycastDebugger();

        EnemyLogic();
    }

    private void FixedUpdate()
    {
        if (!attackMod && !die && !damaged)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.fixedDeltaTime);
    }

    private void RaycastHit()
    {
        enemyHit = Physics2D.Raycast(raycastTransform.position, transform.right, raycastLenght, raycastMask);

        if (enemyHit.collider != null)
        {
            inRange = true;

            EnemySpeed = 8;

            target = enemyHit.collider.transform;

            Flip();
        }
        else
        {
            inRange = false;

            EnemySpeed = 4f;
        }
    }

    private void EnemyLogic()
    {
        Distance = Vector2.Distance(transform.position, target.position);

        if (!InsideOfLimits())
        {
            if (!inRange)
            {
                SelectTarget();
            }
        }

        if (inRange)
        {
            Attack();
        }
        else
        {
            StopAttack();
        }
    }

    private void RaycastDebugger()
    {
        if (!inRange)
        {
            Debug.DrawRay(raycastTransform.position, transform.right * raycastLenght, Color.green);
        }
        else
        {
            Debug.DrawRay(raycastTransform.position, transform.right * raycastLenght, Color.red);
        }
    }

    private void Attack()
    {
        //Cooldown();

        if (Distance < AttackDistance)
        {
            attackMod = true;

            EnemyAnimator.SetTrigger("Attack");
        }
    }

    private void StopAttack()
    {
        attackMod = false;

        EnemyAnimator.ResetTrigger("Attack");
    }

    /*
    private void Cooldown()
    {
        if (timer > Random.Range(3, 5))
        {
            timer = 0;

            cooling = false;

            EnemyAnimator.SetBool("Charge", false);
        }
        else
        {
            timer += Time.deltaTime;

            cooling = true;

            EnemyAnimator.SetBool("Charge", true);
        }
    }
    */

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;

            lookingRight = false;
        }
        else
        {
            rotation.y = 0;

            lookingRight = true;
        }
        transform.eulerAngles = rotation;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            EnemyHeal -= 25;

            if (EnemyHeal <= 0)
            {
                die = true;

                EnemyAnimator.SetTrigger("Death");
            }
            else
            {
                EnemyAnimator.SetTrigger("Damaged");

                if (lookingRight)
                {
                    EnemyRigidbody.velocity -= new Vector2(10, 5);
                }
                else
                {
                    EnemyRigidbody.velocity += new Vector2(10, 5);
                }
            }
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
