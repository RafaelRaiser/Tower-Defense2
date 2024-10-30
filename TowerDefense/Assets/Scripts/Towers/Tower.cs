using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tower
{
    public string name; // Nome da torre.
    public GameObject prefab; // Prefab da torre, usado para instanciar a torre no jogo.

    // Construtor da classe Tower, que inicializa os atributos da torre.
    public Tower(string _name, GameObject _prefab)
    {
        name = _name; // Inicializa o nome da torre.
        prefab = _prefab; // Inicializa o prefab da torre.
    }
}