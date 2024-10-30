using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour, IAtaque // Classe Torre: Representa uma estrutura que ataca inimigos dentro de um determinado alcance.
{
    [SerializeField] protected float alcanceDeAlvo = 5f;    // Raio de alcance da torre para identificar inimigos.
    [SerializeField] protected LayerMask mascaraInimigo;    // M�scara de camada para identificar quais objetos s�o inimigos.
    [SerializeField] protected GameObject prefabProjetil;   // Prefab do proj�til que a torre ir� disparar.
    [SerializeField] protected Transform pontoDeDisparo;    // Ponto de onde os proj�teis ser�o disparados.
    [SerializeField] private float frequenciaDeDisparo = 1f;    // Dano por segundo (disparos por segundo).

    protected Transform alvo;    // Refer�ncia ao inimigo alvo.
    protected float tempoParaDisparo;    // Tempo at� o pr�ximo disparo.

    public virtual void ExecutarAtaque()    // M�todo virtual para atacar. Pode ser sobrescrito em classes derivadas.
    {
        //implementa��o nas classes derivadas.
    }

    private void Update()    // M�todo chamado a cada quadro para verificar e atacar inimigos.
    {
        if (alvo == null) // Se n�o houver alvo, procura um.
        {
            ProcurarAlvo();
            return;
        }

        if (!VerificarAlcanceDoAlvo())
        {
            alvo = null;        // Verifica se o alvo est� fora do alcance.
        }
        else
        {
            tempoParaDisparo += Time.deltaTime; // Aumenta o tempo at� o pr�ximo disparo.

            if (tempoParaDisparo >= 1f / frequenciaDeDisparo) // Verifica se � hora de disparar.
            {
                Disparar(); // Realiza o disparo.
                tempoParaDisparo = 0f; // Reseta o tempo at� o pr�ximo disparo.
            }
        }
    }

    protected virtual void Disparar()    // M�todo protegido para disparar um proj�til.
    {
        GameObject objetoProjetil = Instantiate(prefabProjetil, pontoDeDisparo.position, Quaternion.identity); // Instancia um objeto de bala na posi��o do ponto de disparo.

        // Obt�m o script do proj�til e define o alvo.
        Projeteis scriptDoProjetil = objetoProjetil.GetComponent<Projeteis>();
        scriptDoProjetil.DefinirAlvo(alvo);
    }

    private bool VerificarAlcanceDoAlvo()    // Verifica se o alvo est� dentro do alcance da torre.
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