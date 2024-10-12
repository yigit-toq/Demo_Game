using UnityEngine;

public class ColumnMovoment : MonoBehaviour
{
    public Character Data { get; set; }

    public float Time;

    public float Speed;

    void Start()
    {
        Data = FindObjectOfType<Character>();

        Destroy(gameObject, Time);
    }

    void FixedUpdate()
    {
        if (!Data.IsDead)
        {
            transform.position += Vector3.left * Speed * UnityEngine.Time.deltaTime;
        }
    }
}
