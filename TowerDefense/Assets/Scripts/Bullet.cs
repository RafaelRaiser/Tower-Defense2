using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private Rigidbody2D rb;            // Componente Rigidbody2D usado para movimentar o projétil.
    [SerializeField] private float bulletSpeed = 5f;    // Velocidade do projétil.
    [SerializeField] private int bulletDamage = 1;      // Dano causado pelo projétil ao colidir com um inimigo.

    private Transform target;    // Referência ao alvo que o projétil deve seguir.

    public void SetTarget(Transform _target)    // Método para definir o alvo do projétil.
    {
        target = _target;
    }

    private void FixedUpdate()    // Método chamado a cada quadro de física para movimentar o projétil.
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;  // Direção normalizada em direção ao alvo.
        rb.velocity = direction * bulletSpeed;                                  // Aplica a velocidade ao Rigidbody2D.
    }

    private void OnCollisionEnter2D(Collision2D collision)    // Método chamado quando o projétil colide com outro objeto.
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)    // Aplica dano apenas se o alvo tiver componente de saúde.
        {
            health.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);    // Destroi o projétil após a colisão.
    }
}
