using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class MovimentoInimigo : MonoBehaviour // Classe MovimentoInimigo: Gerencia o movimento do inimigo em um caminho.

{
    [SerializeField] private Rigidbody2D corpoRigido;    // Rigidbody2D que controla o movimento do inimigo.

    [SerializeField] private float velocidadeMovimento = 2f;    // Velocidade de movimento.

    private Transform alvo;    // Próximo ponto do caminho para o inimigo seguir.

    private int indiceCaminho = 0;    // Posição do inimigo no caminho.

    private float velocidadeInicial;    // Velocidade original do inimigo.

    private void Start() // Inicializa a velocidade e define o primeiro ponto de destino.
    {
        velocidadeInicial = velocidadeMovimento;
        alvo = LevelManager.instance.path[indiceCaminho];
    }

    private void Update() // Checa se o inimigo chegou ao próximo ponto.
    {
        if (Vector2.Distance(alvo.position, transform.position) <= 0.1f)
        {
            indiceCaminho++;

            if (indiceCaminho == LevelManager.instance.path.Length)
            {
                GeradorInimigos.onInimigoDestruido.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                alvo = LevelManager.instance.path[indiceCaminho];
            }
        }
    }

    private void FixedUpdate() // Movimenta o inimigo na direção do alvo.
    {
        Vector2 direcao = (alvo.position - transform.position).normalized;
        corpoRigido.velocity = direcao * velocidadeMovimento;
    }

    public void AjustarVelocidade(float novaVelocidade) // Método para alterar a velocidade temporariamente.
    {
        velocidadeMovimento = novaVelocidade;
    }

    public void ResetarVelocidade() // Restaura a velocidade original.
    {
        velocidadeMovimento = velocidadeInicial;
    }
}