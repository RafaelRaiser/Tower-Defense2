using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoRegenerador : Vida 

{
    [SerializeField] private float taxaRegeneracao = 1f;   // Taxa de regeneração de saúde.
    [SerializeField] private float vidaMaxima = 5f;   // Limite máximo de saúde do inimigo.

    private void Start()     // Inicializa a saúde e inicia o processo de regeneração.

    {
        pontosDeVida = vidaMaxima;   // Define a saúde inicial para o máximo permitido.
        StartCoroutine(RegenerarSaude());  // Começa a regeneração da saúde.
    }

    private IEnumerator RegenerarSaude()     // Corrotina que controla a regeneração de saúde.

    {
        while (!destruido)         // Continua regenerando enquanto o objeto não foi destruído.

        {
            if (pontosDeVida < vidaMaxima)             // Se a saúde estiver abaixo do limite máximo, regenera pontos.

            {
                pontosDeVida += taxaRegeneracao * Time.deltaTime;  // Adiciona pontos de acordo com a taxa.
                pontosDeVida = Mathf.Min(pontosDeVida, vidaMaxima);   // Limita a saúde ao valor máximo.
            }
            yield return null;  // Aguarda até o próximo quadro.
        }
    }

    public override void ReceberDano(float dano)  // Recebe dano e chama o método da classe base.
    {
        base.ReceberDano(dano);
    }
}