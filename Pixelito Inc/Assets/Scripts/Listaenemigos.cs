using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listaenemigos : MonoBehaviour
{
    //Aqui llamo todo lo que necesito
    public bool buffActivado = false; //Un bool para saber si esta activado el freeze o no
    public float duracionBuff = 10; //un timer para contabilizar el tiempo que lleva prendido el buff
    public List<GameObject> ListaEnemigosVivos = new List<GameObject>(); //una lista para meter solo los enemigos que esten vivos
    public List<GameObject> ListaEnemigos = new List<GameObject>(); //una lista para meter todos los enemigos en general

    private void Awake()
    {
        ListaEnemigos.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); //apenas comience el jeugo necesito que me agregue todos los enemigos a la lista
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && duracionBuff == 10) //esta el la condicion que uso para activar el buff
        {
            ListaEnemigosVivos.Clear(); //para que no se dupliquen las listas de enemigos vivos, uso este metodo de Clear() y asi agrega a la lista de vivos solo los que estan vivos
            foreach (GameObject Enemy in ListaEnemigos) //recorro lista de enemigos general
            {
                if (Enemy.activeInHierarchy) //si esta vivo, esto solo funciona si los enemigos tienen en el metodo Death() que el GameObject se destruya o se apague
                {
                    ListaEnemigosVivos.Add(Enemy); //agrego todos los enemigos vivos 
                }
            }
            ListaEnemigosVivos.RemoveRange(ListaEnemigosVivos.Count / 2, ListaEnemigosVivos.Count / 2); //aqui aplico el hecho de que solo tome la mitad de los vivos
            foreach (GameObject Enemy in ListaEnemigosVivos) //recorro lista de enemigos vivos
            {
                Enemy.GetComponent<Enemigo>().speed = 0; //rapidez a 0 del chase
                Enemy.GetComponent<Patrol>().speed = 0; //rapidez a 0 del patrol
                Enemy.GetComponent<Patrol>().mobAnim.SetBool("Idle", true); //apago animacion
                buffActivado = true; //activa el bool de buff
            }
        }
        if (buffActivado) //activador de contador de tiempo
        {
            duracionBuff -= 1f * Time.deltaTime; //tiempo se resta en funcion del Time.DeltaTime
        }
        if (duracionBuff <= 0) //verifica que todavia este activado el buff con base en el tiempo
        {
            buffActivado = false; //desactiva el buff
            duracionBuff = 10; //retorna el tiempo a 10 para poder activar de neuvo el buff
        }
        if (!buffActivado || duracionBuff == 10) //esto controla el regreso de los valores estandar de los enemigos
        {
            foreach (GameObject Enemy in ListaEnemigosVivos) 
            {
                Enemy.GetComponent<Enemigo>().speed = 0.5f;
                Enemy.GetComponent<Patrol>().speed = 0.3f;
                Enemy.GetComponent<Patrol>().mobAnim.SetBool("Idle", false); //esta condicion no afecta el juego normalmente porque predomina sobre esta la funcion que controla los spots

            }
        }
    }
    public void Check(GameObject Enemy) //Metodo que controla que si un enemigo muere se borre de las listas
    {
        if (ListaEnemigosVivos.Contains(Enemy))
        {
            ListaEnemigos.Remove(Enemy);
            ListaEnemigosVivos.Remove(Enemy);
        }
    }
}
