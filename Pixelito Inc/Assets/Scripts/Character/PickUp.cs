using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField]
    private AudioSource PickUpSound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpSound.Play();
        PickUpCheck manager = collision.GetComponent<PickUpCheck>();
        if (manager)
        {
            manager.PickUpItem(gameObject);
            Destroy(gameObject);
        }
    }
}
