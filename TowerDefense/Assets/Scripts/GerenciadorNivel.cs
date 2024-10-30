using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorNivel : MonoBehaviour // Classe GerenciadorNivel: Gerencia informa��es do n�vel, como pontos de in�cio e caminho dos inimigos.
{
    public static GerenciadorNivel instancia; // Inst�ncia singleton da classe GerenciadorNivel.

    public Transform pontoInicial; // Ponto de in�cio para onde os inimigos devem ser gerados.

    public Transform[] caminho; // Array de pontos que define o caminho que os inimigos devem seguir.

    #region Singleton
    private void Awake() // M�todo chamado quando o objeto � inicializado.
    {
        instancia = this; // Inicializa a inst�ncia singleton.
    }
    #endregion
}
