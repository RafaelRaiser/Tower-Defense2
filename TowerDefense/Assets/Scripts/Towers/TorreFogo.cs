using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFogo : Torre // Classe TorreFogo: Representa uma torre que causa dano por queimadura a inimigos.
{
    [SerializeField] private float duracaoQueimadura = 3f; // Duração da queimadura em segundos.
    [SerializeField] private float danoPorSegundoQueimadura = 1f; // Dano causado por segundo pela queimadura.

    public override void ExecutarAtaque() // Método chamado para atacar o alvo.
    {
        if (alvo != null) // Verifica se há um alvo definido.
        {
            Vida vidaInimiga = alvo.GetComponent<Vida>(); // Obtém o componente de vida do inimigo.

            if (vidaInimiga != null) // Se o inimigo tiver um componente de vida, inicia a aplicação de dano por queimadura.
            {
                StartCoroutine(AplicarDanoDeQueimadura(vidaInimiga));
            }
        }
    }

    private IEnumerator AplicarDanoDeQueimadura(Vida vidaInimiga) // Corrotina que aplica dano por queimadura ao inimigo ao longo do tempo.
    {
        float tempoDecorrido = 0f; // Tempo decorrido.

        while (tempoDecorrido < duracaoQueimadura) // Enquanto o tempo decorrido for menor que a duração da queimadura.
        {
            vidaInimiga.ReceberDano(danoPorSegundoQueimadura * Time.deltaTime); // Aplica dano ao inimigo baseado no dano por segundo.
            tempoDecorrido += Time.deltaTime; // Atualiza o tempo decorrido.
            yield return null; // Espera o próximo quadro antes de continuar.
        }
    }

    protected override void Disparar() // Método protegido para atirar um projétil.
    {
        GameObject objetoProjetil = Instantiate(prefabProjetil, pontoDeDisparo.position, Quaternion.identity); // Instancia o objeto da bala na posição do ponto de disparo.

        // Obtém o script do projétil e define o alvo.
        Projeteis scriptDoProjetil = objetoProjetil.GetComponent<Projeteis>();
        scriptDoProjetil.DefinirAlvo(alvo);

        ExecutarAtaque(); // Chama o método de ataque para aplicar queimadura.
    }
}
