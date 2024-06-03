using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    private GameObject turret;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        Debug.Log("Build Turret Here: " + name);
        if (turret != null) return;

        Tower towerToBuild = TurretManager.main.GetSelectedTurret();
        //Check to make sure the player isn't broke.
        if(towerToBuild.cost > GameManager.main.currency)
        {
            Debug.Log("!Insufficient Credits!");
            return;
        }
        //Snatch money away :(
        GameManager.main.SpendCurrency(towerToBuild.cost);
        //Then build a turret at the location of the clicked plot.
        turret = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        
    }
}
