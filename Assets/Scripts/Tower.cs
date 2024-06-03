using System;
using UnityEngine;

[Serializable]
public class Tower
{
    //This is just to store stats for a turret.
    public string name;
    public int cost;
    public GameObject prefab;

    public Tower (string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}
