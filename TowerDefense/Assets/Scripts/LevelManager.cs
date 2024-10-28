using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } // Inst�ncia singleton segura da classe LevelManager.

    public Transform startPoint;   // Ponto inicial onde os inimigos s�o gerados.
    public Transform[] path;       // Pontos que definem o caminho que os inimigos devem seguir.
#region singleton
    private void Awake()
    {
        // Implementa��o do padr�o Singleton para garantir uma �nica inst�ncia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroi a nova inst�ncia se uma j� existe
            return;
        }
    }
#endregion
}
