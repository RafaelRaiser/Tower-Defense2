using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Tower  
{
//Propriedades da torre:
    public string name;            // Nome da torre.
    public GameObject prefab;       // Prefab da torre, usado para instanciar no jogo.

    public Tower(string name, GameObject prefab)   // Construtor para inicializar os atributos da torre.
    {
        this.name = name;
        this.prefab = prefab;
    }
}

