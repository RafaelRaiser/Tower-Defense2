using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projeteis : MonoBehaviour // Classe Projeteis: Representa um proj�til que causa dano a inimigos.
{
    [SerializeField] private Rigidbody2D rigido2D; // Componente Rigidbody2D usado para movimentar o proj�til.
    [SerializeField] private float velocidadeProjeteis = 5f; // Velocidade do proj�til.
    [SerializeField] private int danoProjeteis = 1; // Dano causado pelo proj�til ao colidir com um inimigo.

    private Transform alvo; // Refer�ncia ao alvo que o proj�til deve seguir.

    public void DefinirAlvo(Transform _alvo) // M�todo para definir o alvo do proj�til.
    {
        alvo = _alvo; // Define o alvo do proj�til.
    }

    private void FixedUpdate() // M�todo chamado a cada quadro de f�sica.
    {
        if (!alvo) return; // Se n�o houver um alvo, sai do m�todo.

        Vector2 direcao = (alvo.position - transform.position).normalized; // Calcula a dire��o do proj�til em dire��o ao alvo e normaliza a dire��o.
        rigido2D.velocity = direcao * velocidadeProjeteis; // Define a velocidade do Rigidbody2D na dire��o calculada.

        if (Vector2.Distance(alvo.position, transform.position) < 0.1f) // Verifica se o proj�til atingiu o alvo.
        {
            Vida vidaInimiga = alvo.GetComponent<Vida>(); // Obt�m o componente de vida do inimigo.

            if (vidaInimiga != null)
            {
                vidaInimiga.ReceberDano(danoProjeteis); // Aplica o dano ao inimigo.
            }
            Destroy(gameObject); // Destroi o proj�til.
        }
    }

    private void OnCollisionEnter2D(Collision2D colisao) // M�todo chamado quando o proj�til colide com outro objeto.
    {
        Vida vidaInimiga = colisao.gameObject.GetComponent<Vida>(); // Obt�m o componente de vida do inimigo.

        if (vidaInimiga != null)
        {
            vidaInimiga.ReceberDano(danoProjeteis); // Aplica o dano ao inimigo.
        }
        Destroy(gameObject); // Destroi o proj�til.
    }
}
