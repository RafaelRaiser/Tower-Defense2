using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour, IAtaque // Classe Torre: Representa uma estrutura que ataca inimigos dentro de um determinado alcance.
{
    [SerializeField] protected float alcanceDeAlvo = 5f;    // Raio de alcance da torre para identificar inimigos.
    [SerializeField] protected LayerMask mascaraInimigo;    // Máscara de camada para identificar quais objetos são inimigos.
    [SerializeField] protected GameObject prefabProjetil;   // Prefab do projétil que a torre irá disparar.
    [SerializeField] protected Transform pontoDeDisparo;    // Ponto de onde os projéteis serão disparados.
    [SerializeField] private float frequenciaDeDisparo = 1f;    // Dano por segundo (disparos por segundo).

    protected Transform alvo;    // Referência ao inimigo alvo.
    protected float tempoParaDisparo;    // Tempo até o próximo disparo.

    public virtual void ExecutarAtaque()    // Método virtual para atacar. Pode ser sobrescrito em classes derivadas.
    {
        //implementação nas classes derivadas.
    }

    private void Update()    // Método chamado a cada quadro para verificar e atacar inimigos.
    {
        if (alvo == null) // Se não houver alvo, procura um.
        {
            ProcurarAlvo();
            return;
        }

        if (!VerificarAlcanceDoAlvo())
        {
            alvo = null;        // Verifica se o alvo está fora do alcance.
        }
        else
        {
            tempoParaDisparo += Time.deltaTime; // Aumenta o tempo até o próximo disparo.

            if (tempoParaDisparo >= 1f / frequenciaDeDisparo) // Verifica se é hora de disparar.
            {
                Disparar(); // Realiza o disparo.
                tempoParaDisparo = 0f; // Reseta o tempo até o próximo disparo.
            }
        }
    }

    protected virtual void Disparar()    // Método protegido para disparar um projétil.
    {
        GameObject objetoProjetil = Instantiate(prefabProjetil, pontoDeDisparo.position, Quaternion.identity); // Instancia um objeto de bala na posição do ponto de disparo.

        // Obtém o script do projétil e define o alvo.
        Projeteis scriptDoProjetil = objetoProjetil.GetComponent<Projeteis>();
        scriptDoProjetil.DefinirAlvo(alvo);
    }

    private bool VerificarAlcanceDoAlvo()    // Verifica se o alvo está dentro do alcance da torre.
    {
        return Vector2.Distance(alvo.position, transform.position) <= alcanceDeAlvo;
    }

    private void ProcurarAlvo()    // Procura por um alvo inimigo dentro do alcance usando um CircleCast.
    {
        RaycastHit2D[] impactos = Physics2D.CircleCastAll(transform.position, alcanceDeAlvo, (Vector2)transform.position, 0f, mascaraInimigo);

        // Se houver inimigos detectados, define o primeiro como alvo.
        if (impactos.Length > 0)
        {
            alvo = impactos[0].transform;
        }
    }
}