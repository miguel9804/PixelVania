using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool abrirPuerta = false;
    [SerializeField] private PickUpCheck pUC;

    private void Update()
    {
        if (!pUC.KeyBool()) abrirPuerta = false;
        if (abrirPuerta && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
            pUC.keyCount = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpCheck manager = collision.GetComponent<PickUpCheck>();
        Health_Cha Player = collision.GetComponent<Health_Cha>();
        if (Player != null && manager.keyCount)
        {
            abrirPuerta = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Health_Cha Player = collision.GetComponent<Health_Cha>();
        if (Player != null) abrirPuerta = false;

    }
}
