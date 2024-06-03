using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attirbutes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int reward = 50;


    private bool isDestroyed = false;
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        //Damagable entities take damage here:
        //I think this is where the main issue is coming from. 
        //The issue might be happening when 2 enemies die from the same bullet?
        //Causes only 1 instance of damage therefore, only 1 enemy is killed?
        //but theres 2 instances of this script if theres 2 enemies right?
        if (hitPoints <= 0 && !isDestroyed)
        {
            Spawner.onEnemyDestroy.Invoke();
            GameManager.main.IncreaseCurrency(reward);
            //Spawner.main.enemiesAlive--;
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
