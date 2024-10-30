using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoRegenerador : Vida 

{
    [SerializeField] private float taxaRegeneracao = 1f;   // Taxa de regenera��o de sa�de.
    [SerializeField] private float vidaMaxima = 5f;   // Limite m�ximo de sa�de do inimigo.

    private void Start()     // Inicializa a sa�de e inicia o processo de regenera��o.

    {
        pontosDeVida = vidaMaxima;   // Define a sa�de inicial para o m�ximo permitido.
        StartCoroutine(RegenerarSaude());  // Come�a a regenera��o da sa�de.
    }

    private IEnumerator RegenerarSaude()     // Corrotina que controla a regenera��o de sa�de.

    {
        while (!destruido)         // Continua regenerando enquanto o objeto n�o foi destru�do.

        {
            if (pontosDeVida < vidaMaxima)             // Se a sa�de estiver abaixo do limite m�ximo, regenera pontos.

            {
                pontosDeVida += taxaRegeneracao * Time.deltaTime;  // Adiciona pontos de acordo com a taxa.
                pontosDeVida = Mathf.Min(pontosDeVida, vidaMaxima);   // Limita a sa�de ao valor m�ximo.
            }
            yield return null;  // Aguarda at� o pr�ximo quadro.
        }
    }

    public override void ReceberDano(float dano)  // Recebe dano e chama o m�todo da classe base.
    {
        base.ReceberDano(dano);
    }
}