using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    [SerializeField] Health_Cha health;
    [SerializeField] GameObject botonDerrota;
    [SerializeField] EnemyChecker enemigos;
    private CheckPoints checkPoints;
    public GameObject jugador;
    public short vidas;
    public Scriptchekpoints scripchek;



    void Awake()
    {
        checkPoints = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckPoints>();

        vidas = 2;
    }

    private void Update()
    {
        if (health.lp <= 0)
        {
            botonDerrota.SetActive(true);
        }
    }


    public void CheckpointLoad()
    {
        if (vidas <= 0) LoadL("MainMenu");



        if (vidas > 0)
        {

            Time.timeScale = 1;
            vidas -= 1;
            checkPoints.cargarPartida();
            botonDerrota.SetActive(false);
        }
        /* if (InfoJugador.ExisteInforGuardada == false)
           {
               LoadL("Hielo");  
           }*/
    }
    public void LoadL(string level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public short Vidas()
    {
        return vidas;
    }
}
