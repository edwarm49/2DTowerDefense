using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Transform startPos;
    public Transform[] path;

    public int currency;
    public int health;

    private void Awake()
    {
        //Allow other scripts to directly communicate with this one.
        main = this;
    }

    private void Start()
    {
        currency = 300;
        health = 10;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        //Upon placing a new turret, snatch precious money away.
        if(amount <= currency)
        {
            //buy item
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("youre too broke for that item");
            return false;
        }
    }
}
