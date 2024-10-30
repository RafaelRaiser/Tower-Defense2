using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour /
{
    public static BuildManager instance;  

    [SerializeField] private Tower[] towers; 
    private int towerSelected; 

    private void Awake() 
        instance = this; // Define  inst�ncia atual como  inst�ncia est�tica.
    }
    public Tower GetSelectedTower() // Retorna a torre atualmente selecionada.
    {
        return towers[towerSelected];  // Retorna  torre com base no �ndice selecionado.
    }
    public void SetSelectedTower(int _selectedTower) // Define  torre selecionada pelo �ndice fornecido.
    {
        towerSelected = _selectedTower;  // Atualiza o �ndice da torre selecionada.
    }
}
