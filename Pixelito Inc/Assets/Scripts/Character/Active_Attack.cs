using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Attack : MonoBehaviour
{
    private Character_Move cha;

    [SerializeField]
    private GameObject att; //Es el Collider del ataque
    private Collider2D col; //Hijo del Collider

    private bool attack;
    private float temp; //Tiempo de la animacion

    [SerializeField]
    private AudioSource attackPJ; //Audio 

    // Start is called before the first frame update
    private void Awake()
    {
        temp = 0f;
        attack = false;
        col = att.GetComponent<Collider2D>();
        cha = GetComponent<Character_Move>();
        col.enabled = false; 
    }

    private void Update() //Aqui se maneja si se puede atacar o no al igual que la animacion
    {
        if(temp==0f) // temp es un timer para los ataques y asi hacer la animacion mas pulida y que quede bien
        {
            if (Input.GetMouseButtonDown(0)) // al oprimir apretar el mouse el personaje atacara
            {
                attack = true;
                cha.anim.SetBool("Ataque", true);
                attackPJ.Play();
            }
        }
        if (attack)
        {
            temp += 1 * Time.deltaTime; // cuando el ataque se esta ejecutando empezara a contrar el timer para que no se pueda atacar varias veces sin que la animacion de ataque acabe
           

            if (temp > 0.5f && cha.grounded) // Yo hice las pruebas para ver mas o menos cuanto tenia que esperar para acabar la animacion, en el caso del suelo mayor a 0.5
            {
                cha.anim.SetBool("Ataque", false);
                attack = false; 
                temp = 0;
            }
            if (temp > 0.3f && !cha.grounded) // Y en el caso del aire mayor a 0.3
            {
                cha.anim.SetBool("Ataque", false);
                attack = false; // el ataque se vuelve falso para que no entre de nuevo al if 
                temp = 0; // El timer se reinicia para que pueda volver a atacar y ejecutar la animacion
            }
            
        }

    }

    public void OnAttack() // Evento que activa el collider del ataque
    {
        col.enabled = true;
    }

    public void OnAttackFinished() // evento que descativa el collider del ataque
    {
        col.enabled = false;
    }
 
}
