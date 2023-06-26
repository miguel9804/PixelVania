using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public static EnemyChecker instance;
    //private Enemigo enemigos;

    public List<GameObject> listaEnemigos = new List<GameObject>();
    public List<GameObject> listaEnemigosVivos = new List<GameObject>();

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        //enemigos = GetComponent<Enemigo>();
        listaEnemigos.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        //print(listaEnemigos.Count);
    }

    void Update()
    {
        
    }

    public void OnLoad()
    {
        foreach (GameObject Enemy in listaEnemigos) 
        {           
            if(Enemy.activeInHierarchy)
            {
                listaEnemigosVivos.Add(Enemy);
            }            
        }

    }
    public void CleanListaEnemigosVivos()
    {
        listaEnemigosVivos.Clear();
    }

    public void Check(GameObject Enemy)
    {
        if (listaEnemigos.Contains(Enemy))
        {
            listaEnemigos.Remove(Enemy);
        }
        print(listaEnemigos.Count);
    }
    public void CargarLista()
    {
        listaEnemigos.Clear();
        listaEnemigos.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }


}
