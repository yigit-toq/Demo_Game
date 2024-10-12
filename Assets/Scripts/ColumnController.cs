using System.Collections;
using UnityEngine;

public class ColumnController : MonoBehaviour
{
    public Character Data { get; set; }

    public GameObject ColumnObject;
    public Animator groundAnimator;

    public float time;

    private void Start()
    {
        Data = FindObjectOfType<Character>();
    }

    private void Update()
    {
        if (Data.IsDead)
        {
            groundAnimator.SetTrigger("Stop");
        }
    }

    public IEnumerator ColumnSpawner(float time)
    {
        while (!Data.IsDead)
        {
            Instantiate (ColumnObject, new Vector3(10, Random.Range(-1.5f, 3f), 0), Quaternion.identity);

            yield return new WaitForSeconds(time);
        }
    }
}
