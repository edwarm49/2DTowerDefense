using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
      
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rps = 1f; //Rounds per Second
    [SerializeField] private AudioSource src;

    private Transform target;
    private float timeUntilFire;
    public bool firing = false;
    public static Turret main;

    private void OnDrawGizmosSelected()
    {
        //Just for the editor to visualize the range of a turret.
        //Handles.color = Color.cyan;
        //Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
    private void Awake()
    {
        main = this;
    }
    // Update is called once per frame
    void Update()
    {
        //This is all pretty self explanitory based on function names.
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
            firing = false;
        }
        else
        {
            //Shoot
            timeUntilFire += Time.deltaTime;
            firing = true;
            if(timeUntilFire >= 1f/ rps)
            {
                Shoot();
                timeUntilFire = 0;
            }
        }
    }
    private void Shoot()
    {
        //Instantiate a bullet at the firing point.
        Debug.Log("Shoot");
        src.Play();
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    private void FindTarget()
    {
        //Send a raycast in a circle that sets the turret's target to the first enemy spotted.
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
            transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        //Rotate the turret to face the targeted enemy.
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x -
            transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);
    }
}
