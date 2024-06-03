using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static TurretManager main;

    [Header("Refereces")]
    [SerializeField] private Tower[] towers;

    private int selectedTower = 0;

    private void Awake()
    {
        //Allow other scripts to directly communicate with this one.
        main = this;
    }

    public Tower GetSelectedTurret()
    {
        //Communicate which turret to place.
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        //Define which turret to place.
        selectedTower = _selectedTower;
    }
}
