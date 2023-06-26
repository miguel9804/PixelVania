using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZone : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            rb2d.velocity.Equals(0);
            rb2d.bodyType = RigidbodyType2D.Static;
        }
    }
}
