using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tower  

{
    public string name;  

    public GameObject prefab;  

    public Tower(string _name, int _cost, GameObject _prefab)    

    {
        name = _name;// Inicializa o nome da torre.

        prefab = _prefab; // Inicializa o prefab da torre.

    }
}