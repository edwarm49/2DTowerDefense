using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.main.path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //Check the distance between current position and target position. If it is close enough, 
        //cycle to next target position.
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            //Move onto the next point along the path.
            pathIndex++;

            //Destroy enemy if it has reached the end of the path.
            if (pathIndex == GameManager.main.path.Length)
            {
                Spawner.onEnemyDestroy.Invoke();
                GameManager.main.TakeDamage(1);
                Destroy(gameObject);
                return;
            }
            else
            {
                target = GameManager.main.path[pathIndex];
            }
        }
    }
    private void FixedUpdate()
    {
        //Move to the target point along path. (Normalize didn't work correctly here for some reason.)
        Vector2 direction = (target.position - transform.position);
        float distance = direction.magnitude;
        Vector2 velocity = direction * (moveSpeed / distance);

        rb.velocity = velocity;
    }
}
