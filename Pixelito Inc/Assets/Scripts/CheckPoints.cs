using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoints : MonoBehaviour
{
    private Health_Cha Vidapj;
    private Level NivelPj;
    private AttackP DamagePj;
    private CheckeaCheckPoints check;
    private bool Guardado;
    private PickUpCheck inventario;
    private EnemyChecker enemigos;
    private Spawn2 spawn;
    private Character_Move alive;
    public Boss bossStas;
    public GameObject boss;
    public GameObject bossVida;
    public List<GameObject> almacenEnemigos = new List<GameObject>();
    public Scriptchekpoints scripchek;

    [SerializeField]
    private AudioSource confirmacion;

    void Awake()
    {
        alive = GetComponent<Character_Move>();
        Vidapj = GetComponent<Health_Cha>();
        NivelPj = GetComponent<Level>();
        DamagePj = GetComponentInChildren<AttackP>();
        check = GetComponentInParent<CheckeaCheckPoints>();
        inventario = GetComponent<PickUpCheck>();
        enemigos = GameObject.FindGameObjectWithTag("Master").GetComponent<EnemyChecker>();
        spawn = GameObject.FindGameObjectWithTag("Master").GetComponent<Spawn2>();
        Guardado = false;
    }

    void Update()
    {
        InfoJugador.InfoBoss.vidaBoss = bossStas.maxL;
    }

    public void guardarPartida()
    {
        if (Guardado == true)
        {
            almacenEnemigos.Clear();
            enemigos.OnLoad();
            almacenEnemigos.AddRange(enemigos.listaEnemigosVivos);
            InfoJugador.InfoPJ.vida = Vidapj.lp;
            InfoJugador.InfoPJ.maxVida = Vidapj.maxLp;
            InfoJugador.InfoPJ.resistencia = Vidapj.res;
            InfoJugador.InfoPJ.damage = DamagePj.dps;
            InfoJugador.InfoPJ.posicion = transform.position;
            InfoJugador.InfoPJ.experiencia = NivelPj.exp;
            InfoJugador.InfoPJ.level = NivelPj.l;
            InfoJugador.InfoPJ.experienciaNecesaria = NivelPj.rexp;
            InfoJugador.InfoPJ.pociones = inventario.potsCount;
            InfoJugador.InfoPJ.monedas = inventario.coinCount;
            InfoJugador.ExisteInforGuardada = true;
            enemigos.CleanListaEnemigosVivos();
        }

    }

    public void cargarPartida()
    {

        foreach (GameObject Enemy in almacenEnemigos)
        {
            Enemy.GetComponent<Enemigo>().Alive();

            Debug.Log("Recorrio");
        }
        alive.Dead(false);
        alive.anim.SetBool("Dead", false);
        Vidapj.lp = InfoJugador.InfoPJ.maxVida;
        Vidapj.maxLp = InfoJugador.InfoPJ.maxVida;
        Vidapj.res = InfoJugador.InfoPJ.resistencia;
        DamagePj.dps = InfoJugador.InfoPJ.damage;
        NivelPj.exp = InfoJugador.InfoPJ.experiencia;
        NivelPj.l = InfoJugador.InfoPJ.level;
        NivelPj.rexp = InfoJugador.InfoPJ.experienciaNecesaria;
        inventario.potsCount = InfoJugador.InfoPJ.pociones;
        inventario.coinCount = InfoJugador.InfoPJ.monedas;
        //transform.position = InfoJugador.InfoPJ.posicion;
        bossStas.LP = InfoJugador.InfoBoss.vidaBoss;
        boss.SetActive(false);
        bossVida.SetActive(false);
        this.transform.position = scripchek.posicion;

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            confirmacion.Play();
            Guardado = true;
            guardarPartida();
            Debug.Log("Guardado Exitoso");
        }

        else { Guardado = false; }
    }


}
