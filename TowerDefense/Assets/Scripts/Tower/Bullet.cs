using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Rigidbody para controle de movimento.
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do projétil.
    [SerializeField] private int bulletDamage = 1; // Dano causado pelo projétil.

    private Transform target; // Alvo do projétil.

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized; // Calcula a direção.
        rb.velocity = direction * bulletSpeed; // Move o projétil.
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().Damaged(bulletDamage); // Aplica dano ao colidir.
        Destroy(gameObject); // Destroi o projétil.
    }
}
