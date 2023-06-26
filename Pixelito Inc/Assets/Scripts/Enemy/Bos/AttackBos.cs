using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBos : MonoBehaviour
{
    [SerializeField] float dps; //Cambie algunas variables que tenias en mayuscula ya que es mejor trabajar las variables minusculas y las funciones mayusculas.
    private float tiempoinicial;
    private float tiempo;
    public BoxCollider2D colider;

    [SerializeField]
    private AudioSource AttackJefe;

    // Start is called before the first frame update
    private void Awake()
    {
        colider.enabled = true;
        colider = GetComponent<BoxCollider2D>();
        tiempoinicial = 5f;
        tiempo = 0f;
    }

    void Update()
    {
        Debug.Log(tiempo);
    }
    //Atake (Echo por Jesus) si se ba a modificar tener en cuenta la compativilidad con el de enemigo :D
    public void OnTriggerEnter2D(Collider2D collision)
    {

        Health_Cha personaje = collision.GetComponent<Health_Cha>(); //Vida de personaje lista, el codigo que controla la vida del personaje es Heatlh_Cha, ahi lo cambie
        AttackJefe.Play();

        if (personaje != null) //Agregue esto para que solamente aplicara el daño al personaje, ya que sin esto saltaba un error
        {
            tiempo = tiempoinicial;
            personaje.TakeDamage(dps);
        }

    }
}
