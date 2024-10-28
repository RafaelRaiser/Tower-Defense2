using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } // Instância singleton segura da classe LevelManager.

    public Transform startPoint;   // Ponto inicial onde os inimigos são gerados.
    public Transform[] path;       // Pontos que definem o caminho que os inimigos devem seguir.
#region singleton
    private void Awake()
    {
        // Implementação do padrão Singleton para garantir uma única instância
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroi a nova instância se uma já existe
            return;
        }
    }
#endregion
}
