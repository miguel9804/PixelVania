using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    private bool puedeComprar = false;
    [SerializeField] private PickUpCheck pUC;
    [SerializeField] private GameObject storeMenu;
    public GameObject pause;

    [SerializeField]
    public AudioSource Tienda;

    private void Update()
    {
        if (!pause.activeInHierarchy) if (Input.GetKeyDown(KeyCode.B) && puedeComprar) LoadStore();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpCheck manager = collision.GetComponent<PickUpCheck>();
        Health_Cha Player = collision.GetComponent<Health_Cha>();
        if (Player != null) {puedeComprar = true;}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Health_Cha Player = collision.GetComponent<Health_Cha>();
        if (Player != null) puedeComprar = false;

    }
    public void LoadStore() 
    {
        storeMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CerrarTienda()
    {
        storeMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ComprarCura()
    {
        if (pUC.coinCount >= 50)
        {
            Tienda.Play();
            pUC.coinCount -= 50;
            pUC.potsCount += 1;
        }
    }
    public void ComprarResistencia()
    {
        if (pUC.coinCount >= 75)
        {
            Tienda.Play();
            pUC.coinCount -= 75;
            pUC.boostCount += 1;
        }
    }
    public void ComprarAtaque()
    {
        if (pUC.coinCount >= 100)
        {
            Tienda.Play();
            pUC.coinCount -= 100;
            pUC.attackBoostCount += 1;
        }
    }
}
