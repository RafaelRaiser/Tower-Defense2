using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }  // Singleton seguro para acessar globalmente.

    [Header("Tower Settings")]
    [SerializeField] private Tower[] towers;    // Array de torres disponíveis para construção.
    private int selectedTowerIndex = 0;         // Índice da torre atualmente selecionada.

    #region Singleton
    private void Awake()
    {
        // Implementação do padrão Singleton para garantir uma única instância
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destrói a nova instância se uma já existe
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste o BuildManager entre cenas.
    }
    #endregion
    public Tower GetSelectedTower()
    {
        if (towers == null || towers.Length == 0) return null; // Verifica se há torres disponíveis
        return towers[selectedTowerIndex];
    }
    public void SetSelectedTower(int towerIndex)
    {
        if (towerIndex >= 0 && towerIndex < towers.Length)
        {
            selectedTowerIndex = towerIndex;
        }
    }
}
