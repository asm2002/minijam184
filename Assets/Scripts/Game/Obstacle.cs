using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] float minForce = 15;
    [SerializeField] float maxForce = 20;
    [SerializeField] float lifetime = 3;
    [SerializeField] float destroyTime = 0.5f;

    Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        Vector2 force = new Vector2(1, Random.Range(0.1f, 0.4f)).normalized;
        if (transform.position.x == Mathf.Abs(transform.position.x)) force.x *= -1;
        rb.AddForce(force * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

        StartCoroutine(DestroyObstacle());

    }

    public IEnumerator DestroyObstacle()
    {
        yield return new WaitForSeconds(lifetime);
        transform.DOScale(0, destroyTime);
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

}
