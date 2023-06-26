using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private float dps;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Vector3 puntospawn = InfoJugador.InfoPJ.posicion;
            float truespawnx = puntospawn.x;
            float truespawny = puntospawn.y;


            collision.transform.position = new Vector3(truespawnx, truespawny + 0.2f , 0);            
            Health_Cha personaje = collision.GetComponent<Health_Cha>();
            personaje.DanoCaida(dps);//El daño de caida se cambio para que no afecte la resistencia a este -Miguel
        }
    }

}
