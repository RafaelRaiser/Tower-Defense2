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
        target = _target;// Define o alvo do proj�til.
    }
    private void FixedUpdate()    // M�todo chamado a cada quadro de f�sica.

    {
        if (!target) return;        // Se n�o houver um alvo, sai do m�todo.

        Vector2 direction = (target.position - transform.position).normalized;        

        rb.velocity = direction * bulletSpeed;        // Define a velocidade do Rigidbody2D para mover o proj�til em dire��o ao alvo.

    }
    private void OnCollisionEnter2D(Collision2D other)     // M�todo chamado quando o proj�til colide com outro objeto.

    {
        other.gameObject.GetComponent<Health>().Damaged(bulletDamage);        
        Destroy(gameObject);        // Destr�i o objeto do proj�til ap�s a colis�o.

    }
}
