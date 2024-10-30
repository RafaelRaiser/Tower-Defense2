using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorConstrucao : MonoBehaviour // Classe GerenciadorConstrucao: Gerencia a sele��o e o acesso a torres para constru��o.
{
    public static GerenciadorConstrucao instancia; // Inst�ncia est�tica da classe GerenciadorConstrucao, usada para acesso global.

    [SerializeField] private Torre[] torresDisponiveis; // Array de torres dispon�veis para constru��o.
    private int torreSelecionada; // �ndice da torre atualmente selecionada.

    private void Awake() // M�todo chamado quando o objeto � instanciado. Inicializa a inst�ncia est�tica.
    {
        instancia = this; // Define a inst�ncia atual como a inst�ncia est�tica.
    }

    public Torre ObterTorreSelecionada() // Retorna a torre atualmente selecionada.
    {
        return torresDisponiveis[torreSelecionada]; // Retorna a torre com base no �ndice selecionado.
    }

    public void DefinirTorreSelecionada(int indiceTorre) // Define a torre selecionada pelo �ndice fornecido.
    {
        torreSelecionada = indiceTorre; // Atualiza o �ndice da torre selecionada.
    }
}
