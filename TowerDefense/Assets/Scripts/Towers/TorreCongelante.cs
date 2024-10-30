using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreCongelante : Torre // Classe TorreCongelante: Representa uma torre que reduz a velocidade dos inimigos.
{
    [SerializeField] private float taxaDeAtaque = 4f; // Taxa de ataques por segundo da torre.
    [SerializeField] private float tempoCongelamento = 1f; // Dura��o do efeito de congelamento em segundos.

    private void Update() // M�todo chamado a cada quadro para verificar se deve congelar inimigos.
    {
        tempoParaDisparo += Time.deltaTime; // Aumenta o tempo at� o pr�ximo disparo.

        if (tempoParaDisparo >= 1f / taxaDeAtaque) // Verifica se � hora de aplicar o efeito de congelamento.
        {
            CongelarInimigos(); // Aplica o efeito de congelamento aos inimigos.
            tempoParaDisparo = 0f; // Reseta o tempo at� o pr�ximo disparo.
        }
    }

    private void CongelarInimigos() // M�todo que congela inimigos dentro do alcance da torre.
    {
        RaycastHit2D[] impactos = Physics2D.CircleCastAll(transform.position, alcanceDeAlvo, (Vector2)transform.position, 0f, mascaraInimigo); // Realiza um CircleCast para detectar inimigos dentro do alcance.

        if (impactos.Length > 0) // Se houver inimigos detectados, aplica o efeito de congelamento.
        {
            foreach (var impacto in impactos)
            {
                MovimentoInimigo movimentoInimigo = impacto.transform.GetComponent<MovimentoInimigo>(); // Obt�m o componente de movimento do inimigo.

                movimentoInimigo.AtualizarVelocidade(0.5f); // Reduz a velocidade do inimigo.
                StartCoroutine(ResetarVelocidadeInimigo(movimentoInimigo)); // Inicia a corrotina para resetar a velocidade ap�s o tempo de congelamento.
            }
        }
    }

    private IEnumerator ResetarVelocidadeInimigo(MovimentoInimigo movimentoInimigo) // Reseta a velocidade do inimigo ap�s o tempo de congelamento.
    {
        yield return new WaitForSeconds(tempoCongelamento); // Espera pelo tempo de congelamento.
        movimentoInimigo.ResetarVelocidade(); // Reseta a velocidade do inimigo ao seu valor original.
    }
}
