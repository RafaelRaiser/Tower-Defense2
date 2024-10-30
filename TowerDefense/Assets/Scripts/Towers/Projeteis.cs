using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projeteis : MonoBehaviour // Classe Projeteis: Representa um projétil que causa dano a inimigos.
{
    [SerializeField] private Rigidbody2D rigido2D; // Componente Rigidbody2D usado para movimentar o projétil.
    [SerializeField] private float velocidadeProjeteis = 5f; // Velocidade do projétil.
    [SerializeField] private int danoProjeteis = 1; // Dano causado pelo projétil ao colidir com um inimigo.

    private Transform alvo; // Referência ao alvo que o projétil deve seguir.

    public void DefinirAlvo(Transform _alvo) // Método para definir o alvo do projétil.
    {
        alvo = _alvo; // Define o alvo do projétil.
    }

    private void FixedUpdate() // Método chamado a cada quadro de física.
    {
        if (!alvo) return; // Se não houver um alvo, sai do método.

        Vector2 direcao = (alvo.position - transform.position).normalized; // Calcula a direção do projétil em direção ao alvo e normaliza a direção.
        rigido2D.velocity = direcao * velocidadeProjeteis; // Define a velocidade do Rigidbody2D na direção calculada.

        if (Vector2.Distance(alvo.position, transform.position) < 0.1f) // Verifica se o projétil atingiu o alvo.
        {
            Vida vidaInimiga = alvo.GetComponent<Vida>(); // Obtém o componente de vida do inimigo.

            if (vidaInimiga != null)
            {
                vidaInimiga.ReceberDano(danoProjeteis); // Aplica o dano ao inimigo.
            }
            Destroy(gameObject); // Destroi o projétil.
        }
    }

    private void OnCollisionEnter2D(Collision2D colisao) // Método chamado quando o projétil colide com outro objeto.
    {
        Vida vidaInimiga = colisao.gameObject.GetComponent<Vida>(); // Obtém o componente de vida do inimigo.

        if (vidaInimiga != null)
        {
            vidaInimiga.ReceberDano(danoProjeteis); // Aplica o dano ao inimigo.
        }
        Destroy(gameObject); // Destroi o projétil.
    }
}
