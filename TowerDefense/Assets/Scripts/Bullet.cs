using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private Rigidbody2D rb;            // Componente Rigidbody2D usado para movimentar o proj�til.
    [SerializeField] private float bulletSpeed = 5f;    // Velocidade do proj�til.
    [SerializeField] private int bulletDamage = 1;      // Dano causado pelo proj�til ao colidir com um inimigo.

    private Transform target;    // Refer�ncia ao alvo que o proj�til deve seguir.

    public void SetTarget(Transform _target)    // M�todo para definir o alvo do proj�til.
    {
        target = _target;
    }

    private void FixedUpdate()    // M�todo chamado a cada quadro de f�sica para movimentar o proj�til.
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;  // Dire��o normalizada em dire��o ao alvo.
        rb.velocity = direction * bulletSpeed;                                  // Aplica a velocidade ao Rigidbody2D.
    }

    private void OnCollisionEnter2D(Collision2D collision)    // M�todo chamado quando o proj�til colide com outro objeto.
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)    // Aplica dano apenas se o alvo tiver componente de sa�de.
        {
            health.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);    // Destroi o proj�til ap�s a colis�o.
    }
}
