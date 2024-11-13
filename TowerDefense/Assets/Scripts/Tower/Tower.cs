using System;
using UnityEngine;

[Serializable]
public class Tower
{
    public string name; // Nome da torre.
    public GameObject prefab; // Prefab da torre.

    public Tower(string _name, GameObject _prefab)
    {
        name = _name;
        prefab = _prefab;
    }
}
