using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float timeLimt = 5f;

    private float activeTime;
    private Transform target;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    //Destroy stray bullets
    private void Update()
    {
        activeTime += Time.deltaTime;
        if(activeTime > timeLimt)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        //Follow and soft-lock onto a target.
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;

        if (!target) return;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If it hits something with health, take that health away and delete the bullet.
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
