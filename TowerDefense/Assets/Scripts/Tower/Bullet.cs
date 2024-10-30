using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 

{
    [SerializeField] private Rigidbody2D rb;    

    [SerializeField] private float bulletSpeed = 5f;   

    [SerializeField] private int bulletDamage = 1;    

    private Transform target;    

    public void SetTarget(Transform _target)    

    {
        target = _target;// Define o alvo do projétil.
    }
    private void FixedUpdate()    // Método chamado a cada quadro de física.

    {
        if (!target) return;        // Se não houver um alvo, sai do método.

        Vector2 direction = (target.position - transform.position).normalized;        

        rb.velocity = direction * bulletSpeed;        // Define a velocidade do Rigidbody2D para mover o projétil em direção ao alvo.

    }
    private void OnCollisionEnter2D(Collision2D other)     // Método chamado quando o projétil colide com outro objeto.

    {
        other.gameObject.GetComponent<Health>().Damaged(bulletDamage);        
        Destroy(gameObject);        // Destrói o objeto do projétil após a colisão.

    }
}
