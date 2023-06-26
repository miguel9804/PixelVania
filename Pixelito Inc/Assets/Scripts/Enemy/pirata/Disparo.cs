using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [SerializeField] GameObject bala;
    [SerializeField] Transform pistola;
    [SerializeField] AudioSource EnemyAttack;
    public void Disparar() { EnemyAttack.Play(); Instantiate(bala, pistola.position, transform.rotation); }
}
