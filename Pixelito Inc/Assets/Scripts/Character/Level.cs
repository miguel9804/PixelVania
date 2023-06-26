using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int l, in_vit; // l es Level o Nivel y in_vit es incremento de vida
    public float in_damage, in_res, exp, rexp;
    private float expRestante;

    private Health_Cha heal; // Variable de Health o Vida pa los que no hablan ingles
    private AttackP at;

    // Start is called before the first frame update
    private void Awake()
    {
        in_damage = 10f;
        in_vit = 25;
        in_res = 15f;
        l = 1;
        exp = 0f;
        rexp = 50f;
        heal = GetComponent<Health_Cha>();
        at = GetComponentInChildren<AttackP>();
    }

    public void LevelUp(float obtained_exp) // Funcion de nivel
    {
        Debug.Log(obtained_exp);
        exp += obtained_exp; // La xp se obtiene al matar enemigos, si mata se le suma la xp que tiene con la xp obtenida del enemigo
        if(l>=2)
        {
            if (exp >= rexp) // Cuando la xp sea mayor o igual a la xp necesaria para subir (rexp) este aumentara su nivel y sus caracteristicas
            {
                expRestante = exp - rexp;
                exp = expRestante;
                l++; // aumento de nivel
                heal.maxLp += in_vit; // Aunmento de vida maxima
                heal.lp += in_vit; // Aumento de vida
                heal.res += in_res; // Aumento de resistencia
                at.dps += in_damage; // Aumento de daño
                rexp *= 1.5f; // la xp necesaria se multiplica para que sea mas dificil subir
            }
        }
        else if (l < 2)
        {
            if (exp >= rexp) // Cuando la xp sea mayor o igual a la xp necesaria para subir (rexp) este aumentara su nivel y sus caracteristicas
            {
                expRestante = exp - rexp;
                exp = expRestante;
                l++; // aumento de nivel
                heal.maxLp += in_vit; // Aunmento de vida maxima
                heal.lp += in_vit; // Aumento de vida
                heal.res += in_res; // Aumento de resistencia
                at.dps += in_damage; // Aumento de daño
                rexp *= 2f; // la xp necesaria se multiplica para que sea mas dificil subir
            }
        }
       
        
        //if (exp < rexp)
        
    }

    public float ShowExpVal()
    {
        return exp / rexp; //El UI lo coje de esta manera
    }
    public int ShowRexp()
    {
        return (int)rexp;
    }
    public float ShowExp()
    {
        return exp;
    }
    public float ShowLevel()
    {
        return l;
    }
    
}
