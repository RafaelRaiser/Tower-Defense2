using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrenoConstruivel : MonoBehaviour // Classe TerrenoConstruivel: Representa uma área onde torres podem ser construídas no jogo.
{
    [SerializeField] private SpriteRenderer renderizadorSprite; // Componente SpriteRenderer usado para mudar a aparência do terreno.

    [SerializeField] private Color corDestaque; // Cor que será aplicada quando o mouse passar sobre o terreno.

    private GameObject torreConstruida; // Referência ao objeto da torre construída neste terreno.

    private Color corInicial; // Cor inicial do terreno.

    // Método chamado ao iniciar o jogo. Inicializa a cor inicial do terreno.
    private void Start()
    {
        corInicial = renderizadorSprite.color; // Armazena a cor inicial do SpriteRenderer.
    }

    private void OnMouseEnter() // Método chamado quando o mouse entra na área do terreno.
    {
        renderizadorSprite.color = corDestaque; // Muda a cor do terreno para a cor de destaque.
    }

    private void OnMouseExit() // Método chamado quando o mouse sai da área do terreno.
    {
        renderizadorSprite.color = corInicial; // Restaura a cor inicial do terreno.
    }

    private void OnMouseDown() // Método chamado quando o mouse clica no terreno.
    {
        if (torreConstruida != null) return; // Se já houver uma torre construída, não faz nada.

        Torre torreParaConstruir = GerenciadorConstrucao.instancia.ObterTorreSelecionada(); // Obtém a torre selecionada do GerenciadorConstrucao.

        torreConstruida = Instantiate(torreParaConstruir.prefab, transform.position, Quaternion.identity); // Instancia a torre na posição do terreno.
    }
}
