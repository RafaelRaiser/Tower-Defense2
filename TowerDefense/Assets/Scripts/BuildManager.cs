using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }  // Singleton seguro para acessar globalmente.

    [Header("Tower Settings")]
    [SerializeField] private Tower[] towers;    // Array de torres dispon�veis para constru��o.
    private int selectedTowerIndex = 0;         // �ndice da torre atualmente selecionada.

    #region Singleton
    private void Awake()
    {
        // Implementa��o do padr�o Singleton para garantir uma �nica inst�ncia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destr�i a nova inst�ncia se uma j� existe
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste o BuildManager entre cenas.
    }
    #endregion
    public Tower GetSelectedTower()
    {
        if (towers == null || towers.Length == 0) return null; // Verifica se h� torres dispon�veis
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
