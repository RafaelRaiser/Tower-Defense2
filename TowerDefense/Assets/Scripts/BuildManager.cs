using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour /
{
    public static BuildManager instance;  

    [SerializeField] private Tower[] towers; 
    private int towerSelected; 

    private void Awake() 
        instance = this; // Define  instância atual como  instância estática.
    }
    public Tower GetSelectedTower() // Retorna a torre atualmente selecionada.
    {
        return towers[towerSelected];  // Retorna  torre com base no índice selecionado.
    }
    public void SetSelectedTower(int _selectedTower) // Define  torre selecionada pelo índice fornecido.
    {
        towerSelected = _selectedTower;  // Atualiza o índice da torre selecionada.
    }
}
