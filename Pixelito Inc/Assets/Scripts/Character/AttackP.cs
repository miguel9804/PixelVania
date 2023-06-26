using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackP : MonoBehaviour
{
    public float dps; //Dps es el daño que realiza

    private Character_Move cha;
    private Level lvl;



    private void Awake()
    {
        cha = GetComponentInParent<Character_Move>(); //Esta InParent porque este es el hijo del ataque
        lvl = GetComponentInParent<Level>();
    }

    //Atake (Echo por Jesus) si se ba a modificar tener en cuenta la compatibilidad con el de enemigo :D
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemigo enemigo = collision.GetComponent<Enemigo>(); // Al colisionar con algo el buscara los scripts del enemigo y del boss
        Boss bos = collision.GetComponent<Boss>();
        
        if (enemigo!=null) // Si encuentra el script entonces significa que es el enemigo
        {
            
            enemigo.TakeDamage(dps); // daño que recibe el enemigo
            cha.MoveAttack(); // Aca al atacar al enemigo el personaje se movera
        }
        if (bos != null)
        {
            bos.TakeDamage(dps);
            cha.MoveAttack();
        }
        if(collision.gameObject.tag.Equals("Piso"))
        {
            cha.MoveAttackW();
        }
    }
}
