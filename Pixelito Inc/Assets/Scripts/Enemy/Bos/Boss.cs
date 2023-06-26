using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float speed;
    public bool perseguir;
    private Transform target;
    public GameObject lootDrop;
    [SerializeField]BossBarController boss_bar;

    [SerializeField] GameObject expDrop; // Hecho por Miguel para obtener la exp 

    [SerializeField] float stop;
    public float LP, maxL;
    [SerializeField] GameObject Vision;
    [SerializeField] BoxCollider2D coliderMuerto;
    [SerializeField] GameObject Puntodeaparicion;

    Rigidbody2D rb2d;
    Animator mobAnim;
    BoxCollider2D coliderVivo;

    //codigo
    PatrolBos patrol;
    DeteccionBos deteccion;
    AttackBos attack;

    [SerializeField]
    private AudioSource Damagejefe;

    [SerializeField]
    private AudioSource Muertejefe;

    //UI
    [SerializeField] Slider slideBossHP;
    public Text bossLifeText;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coliderVivo = GetComponent<BoxCollider2D>();
        patrol = GetComponent<PatrolBos>();
        attack = GetComponentInChildren<AttackBos>();
        mobAnim = GetComponentInParent<Animator>();
        mobAnim.SetBool("Patrol", true);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        deteccion = GetComponentInChildren<DeteccionBos>();
    }

    // Update is called once per frame
    void Update()
    {
         bossLifeText.text = LP.ToString() + "/" + maxL;
        slideBossHP.value = LP / maxL;
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
        LP = Mathf.Clamp(LP, 0, maxL);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Killzone"))
        {
            Destroy(gameObject);
        }
    }

    public void DealDamage() { attack.colider.enabled = true; }
    public void StopDamage() { attack.colider.enabled = false; }
    public void TakeDamage(float damage) { LP -= damage; Damagejefe.Play(); if (LP <= 0) { mobAnim.SetBool("Muerto", true); } }
    public void Death() { boss_bar.dead = true; Muertejefe.Play(); Destroy(Vision); patrol.enabled = false; mobAnim.enabled = false; attack.colider.enabled = false; coliderMuerto.enabled = true; coliderVivo.enabled = false; Instantiate(lootDrop,Puntodeaparicion.transform.position, Quaternion.identity); Instantiate(expDrop, transform.position, Quaternion.identity); }
}
