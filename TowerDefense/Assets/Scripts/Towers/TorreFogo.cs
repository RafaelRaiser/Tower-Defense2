using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFogo : Torre // Classe TorreFogo: Representa uma torre que causa dano por queimadura a inimigos.
{
    [SerializeField] private float duracaoQueimadura = 3f; // Dura��o da queimadura em segundos.
    [SerializeField] private float danoPorSegundoQueimadura = 1f; // Dano causado por segundo pela queimadura.

    public override void ExecutarAtaque() // M�todo chamado para atacar o alvo.
    {
        if (alvo != null) // Verifica se h� um alvo definido.
        {
            Vida vidaInimiga = alvo.GetComponent<Vida>(); // Obt�m o componente de vida do inimigo.

            if (vidaInimiga != null) // Se o inimigo tiver um componente de vida, inicia a aplica��o de dano por queimadura.
            {
                StartCoroutine(AplicarDanoDeQueimadura(vidaInimiga));
            }
        }
    }

    private IEnumerator AplicarDanoDeQueimadura(Vida vidaInimiga) // Corrotina que aplica dano por queimadura ao inimigo ao longo do tempo.
    {
        float tempoDecorrido = 0f; // Tempo decorrido.

        while (tempoDecorrido < duracaoQueimadura) // Enquanto o tempo decorrido for menor que a dura��o da queimadura.
        {
            vidaInimiga.ReceberDano(danoPorSegundoQueimadura * Time.deltaTime); // Aplica dano ao inimigo baseado no dano por segundo.
            tempoDecorrido += Time.deltaTime; // Atualiza o tempo decorrido.
            yield return null; // Espera o pr�ximo quadro antes de continuar.
        }
    }

    protected override void Disparar() // M�todo protegido para atirar um proj�til.
    {
        GameObject objetoProjetil = Instantiate(prefabProjetil, pontoDeDisparo.position, Quaternion.identity); // Instancia o objeto da bala na posi��o do ponto de disparo.

        // Obt�m o script do proj�til e define o alvo.
        Projeteis scriptDoProjetil = objetoProjetil.GetComponent<Projeteis>();
        scriptDoProjetil.DefinirAlvo(alvo);

        ExecutarAtaque(); // Chama o m�todo de ataque para aplicar queimadura.
    }
}
