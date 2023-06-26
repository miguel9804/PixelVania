using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health_Cha : MonoBehaviour
{
    private Character_Move cha;

    [SerializeField]
    private Color color_dam, color_nor;
    private SpriteRenderer rend;

    private bool dam;
    public float res;
    private float tim, res_value;
    public int lp, maxLp;

    [SerializeField]
    private AudioSource DamagePJ;

    [SerializeField]
    private AudioSource MuertePj;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        color_nor = new Color(1f, 1f, 1f);
        color_dam = new Color(1f, 0.6f, 0.6f, 0.7f);
        res = 0f;
        lp = 50;
        maxLp = lp;
        cha = GetComponent<Character_Move>();
    }

    private void Update()
    {
        if (dam) //Si recibe daño el personaje cambiara de color por un momento para dar el efecto de daño
        {
            rend.color = color_dam;
            tim += 1f * Time.deltaTime;
            Debug.Log(dam);
            if(tim>1f)
            {
                Debug.Log("Dam");
                rend.color = Color.Lerp(color_dam, color_nor, 1f);
                dam = false;
                tim = 0f;
            }
        }
        lp = Mathf.Clamp(lp, 0, maxLp);
    }

    //vida del personaje y funcion cuando recibe daño, cualquier cambio me avisan, es igual a la funcion del enemigo que recibe daño.
    public void TakeDamage(float damage) 
    {
        res_value = 100f /(100f + res); // resistencia, cuando la res_value valga 1, significa que no tendra resistencia alguna. Al aumentar de nivel aumentara la res.
        lp -= (int)(damage * res_value);
        dam = true;
        DamagePJ.Play();

        if (lp <= 0) // Muerte
        {
            cha.anim.SetBool("Dead", true);
            cha.Dead(true);
            MuertePj.Play();
            Time.timeScale = 0;
        }
    }
    public void DanoCaida(float damage) //Se hizo una nueva funcion para el daño de caida, asi la resistencia no afectara a este -Miguel
    {
        lp -= (int)damage; 
        dam = true;
        DamagePJ.Play();

        if (lp <= 0) // Muerte
        {
            cha.anim.SetBool("Dead", true);
            cha.Dead(true);
            MuertePj.Play();
            Time.timeScale = 0;
        }
    }

    public float LPValue()
    {
        return lp;
    }
    public float MaxLPValue()
    {
        return maxLp;
    }
}
