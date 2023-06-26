using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Enemigo : MonoBehaviour
{
    public float speed;
    public bool perseguir;
    private Transform target;
    [SerializeField] public Transform posicionInicial;
    public GameObject lootDrop;
    float forceRetroseso = 1.5f;

    [SerializeField] GameObject expDrop; // Hecho por Miguel para obtener la exp 

    [SerializeField] float stop;
    [SerializeField] public float LP, maxLP; // Lo puse publico para poder obtener la xp al morir el enemigo
    [SerializeField] GameObject Vision;
    [SerializeField] BoxCollider2D coliderMuerto;

    public Rigidbody2D rb2d;
    public Animator mobAnim;
    BoxCollider2D coliderVivo;

    //codigo
    Patrol patrol;
    Deteccion deteccion;
    AttackE attack;
    EnemyChecker listaEnemigos;
    Listaenemigos ListaEnemigos;

    [SerializeField]
    private AudioSource DamageEnemy;

    [SerializeField]
    private AudioSource MuerteEnemy;

    void Awake()
    {
        LP = maxLP;
    }
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coliderVivo = GetComponent<BoxCollider2D>();
        patrol = GetComponent<Patrol>();
        attack = GetComponentInChildren<AttackE>();
        mobAnim = GetComponentInParent<Animator>();
        mobAnim.SetBool("Patrol", true);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        deteccion = GetComponentInChildren<Deteccion>();
        listaEnemigos = GameObject.FindGameObjectWithTag("Master").GetComponent<EnemyChecker>();
        ListaEnemigos = GameObject.FindGameObjectWithTag("Master").GetComponent<Listaenemigos>();

    }


    // Update is called once per frame
    void Update()
    {
        if (perseguir == true)
        {
            if (Vector2.Distance(transform.position, target.position) > stop)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, target.position) <= stop)
                {
                    deteccion.Attack();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Killzone"))
        {
            Destroy(gameObject);
        }
    }
    public void Alive()
    {
        this.gameObject.SetActive(true);
        LP = maxLP;
        coliderVivo.enabled = true;
        coliderMuerto.enabled = false;
        transform.position = posicionInicial.position;
        mobAnim.SetBool("Muerto", false);

    }
    public void DealDamage() { attack.colider.enabled = true; }
    public void StopDamage() { attack.colider.enabled = false; }
    public void TakeDamage(float damage)
    {
        Debug.LogError("Auchis");
        attack.colider.enabled = false;
        LP -= damage;
        // Determina dirección del golpe
        Vector2 knockbackDirection = transform.position - target.position;
        knockbackDirection = knockbackDirection.normalized;
        rb2d.AddForce(knockbackDirection * forceRetroseso, ForceMode2D.Impulse);

        DamageEnemy.Play();
        if (LP <= 0) { mobAnim.SetBool("Muerto", true); rb2d.AddForce(transform.up * forceRetroseso, ForceMode2D.Impulse); MuerteEnemy.Play(); listaEnemigos.Check(gameObject);  }
    }
    public void Death() { Instantiate(lootDrop, transform.position, Quaternion.identity); this.gameObject.SetActive(false); Instantiate(expDrop, transform.position, Quaternion.identity); }
}
