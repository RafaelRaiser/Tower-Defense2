using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour // Classe Vida: Controla os pontos de sa�de e destrui��o de um objeto.

{
    [SerializeField] protected float pontosDeVida = 2;    // Quantidade inicial de pontos de vida do objeto.

    protected bool destruido = false;    // Indica se o objeto j� foi eliminado.

    public virtual void ReceberDano(float dano)    // M�todo para causar dano ao objeto.

    {
        pontosDeVida -= dano; // Subtrai o dano recebido dos pontos de vida.
        if (pontosDeVida <= 0 && !destruido)        // Verifica se o objeto atingiu zero de vida e ainda n�o foi destru�do.

        {
            GeradorInimigos.onInimigoDestruido.Invoke(); // Notifica o gerador sobre a destrui��o de um inimigo.
            destruido = true; // Marca o objeto como destru�do.
            Destroy(gameObject); // Remove o objeto da cena.
        }
    }
}
