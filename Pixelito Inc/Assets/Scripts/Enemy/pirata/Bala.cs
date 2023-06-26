using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float tiempo_vida;
    [SerializeField] float dps;
    Transform target;
    Rigidbody2D rb2D;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Invoke("Destruir", tiempo_vida);
        rb2D = GetComponent<Rigidbody2D>();
        Vector2 knockbackDirection = target.position - transform.position;
        knockbackDirection.y = 0;
        knockbackDirection = knockbackDirection.normalized;
        rb2D.AddForce(knockbackDirection * velocidad, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        Health_Cha personaje = collision.GetComponent<Health_Cha>(); //Vida de personaje lista, el codigo que controla la vida del personaje es Heatlh_Cha, ahi lo cambie
        if (personaje != null) //Agregue esto para que solamente aplicara el daño al personaje, ya que sin esto saltaba un error
        {
            personaje.TakeDamage(dps);
            Destruir();
        }

    }
    void Destruir() { Destroy(gameObject); }
    
}
