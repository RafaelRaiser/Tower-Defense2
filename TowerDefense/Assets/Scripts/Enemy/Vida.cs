using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour // Classe Vida: Controla os pontos de saúde e destruição de um objeto.

{
    [SerializeField] protected float pontosDeVida = 2;    // Quantidade inicial de pontos de vida do objeto.

    protected bool destruido = false;    // Indica se o objeto já foi eliminado.

    public virtual void ReceberDano(float dano)    // Método para causar dano ao objeto.

    {
        pontosDeVida -= dano; // Subtrai o dano recebido dos pontos de vida.
        if (pontosDeVida <= 0 && !destruido)        // Verifica se o objeto atingiu zero de vida e ainda não foi destruído.

        {
            GeradorInimigos.onInimigoDestruido.Invoke(); // Notifica o gerador sobre a destruição de um inimigo.
            destruido = true; // Marca o objeto como destruído.
            Destroy(gameObject); // Remove o objeto da cena.
        }
    }
}
