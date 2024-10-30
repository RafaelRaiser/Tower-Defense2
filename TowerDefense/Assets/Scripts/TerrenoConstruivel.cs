using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrenoConstruivel : MonoBehaviour // Classe TerrenoConstruivel: Representa uma �rea onde torres podem ser constru�das no jogo.
{
    [SerializeField] private SpriteRenderer renderizadorSprite; // Componente SpriteRenderer usado para mudar a apar�ncia do terreno.

    [SerializeField] private Color corDestaque; // Cor que ser� aplicada quando o mouse passar sobre o terreno.

    private GameObject torreConstruida; // Refer�ncia ao objeto da torre constru�da neste terreno.

    private Color corInicial; // Cor inicial do terreno.

    // M�todo chamado ao iniciar o jogo. Inicializa a cor inicial do terreno.
    private void Start()
    {
        corInicial = renderizadorSprite.color; // Armazena a cor inicial do SpriteRenderer.
    }

    private void OnMouseEnter() // M�todo chamado quando o mouse entra na �rea do terreno.
    {
        renderizadorSprite.color = corDestaque; // Muda a cor do terreno para a cor de destaque.
    }

    private void OnMouseExit() // M�todo chamado quando o mouse sai da �rea do terreno.
    {
        renderizadorSprite.color = corInicial; // Restaura a cor inicial do terreno.
    }

    private void OnMouseDown() // M�todo chamado quando o mouse clica no terreno.
    {
        if (torreConstruida != null) return; // Se j� houver uma torre constru�da, n�o faz nada.

        Torre torreParaConstruir = GerenciadorConstrucao.instancia.ObterTorreSelecionada(); // Obt�m a torre selecionada do GerenciadorConstrucao.

        torreConstruida = Instantiate(torreParaConstruir.prefab, transform.position, Quaternion.identity); // Instancia a torre na posi��o do terreno.
    }
}
