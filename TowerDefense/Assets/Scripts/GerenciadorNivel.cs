using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorNivel : MonoBehaviour // Classe GerenciadorNivel: Gerencia informações do nível, como pontos de início e caminho dos inimigos.
{
    public static GerenciadorNivel instancia; // Instância singleton da classe GerenciadorNivel.

    public Transform pontoInicial; // Ponto de início para onde os inimigos devem ser gerados.

    public Transform[] caminho; // Array de pontos que define o caminho que os inimigos devem seguir.

    #region Singleton
    private void Awake() // Método chamado quando o objeto é inicializado.
    {
        instancia = this; // Inicializa a instância singleton.
    }
    #endregion
}
