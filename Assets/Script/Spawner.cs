using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;

    private Vector2 SpawnPosition;

    private float SpawnRadius;

    public float Duration;
    public float MaxDuration;

    private void Start()
    {
        Duration = 0;
        MaxDuration = 2;
        SpawnRadius = 20;
    }

    private void Update()
    {
        if (Duration < MaxDuration)
        {
            Duration += Time.deltaTime;
        }
        else
        {
            MaxDuration = Random.Range(2, 4);

            SpawnPosition = transform.position;

            SpawnPosition += Random.insideUnitCircle.normalized * SpawnRadius;

            Instantiate(Enemy, SpawnPosition, Enemy.transform.rotation);

            Duration = 0;
        }
    }
}
