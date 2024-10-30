using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorConstrucao : MonoBehaviour // Classe GerenciadorConstrucao: Gerencia a seleção e o acesso a torres para construção.
{
    public static GerenciadorConstrucao instancia; // Instância estática da classe GerenciadorConstrucao, usada para acesso global.

    [SerializeField] private Torre[] torresDisponiveis; // Array de torres disponíveis para construção.
    private int torreSelecionada; // Índice da torre atualmente selecionada.

    private void Awake() // Método chamado quando o objeto é instanciado. Inicializa a instância estática.
    {
        instancia = this; // Define a instância atual como a instância estática.
    }

    public Torre ObterTorreSelecionada() // Retorna a torre atualmente selecionada.
    {
        return torresDisponiveis[torreSelecionada]; // Retorna a torre com base no índice selecionado.
    }

    public void DefinirTorreSelecionada(int indiceTorre) // Define a torre selecionada pelo índice fornecido.
    {
        torreSelecionada = indiceTorre; // Atualiza o índice da torre selecionada.
    }
}
