using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoJugador 
{
    public static bool ExisteInforGuardada = false;


    public static class InfoPJ
    {
        public static Vector2 posicion;
        public static int vida;
        public static int maxVida;
        public static int level;
        public static float experiencia;
        public static float damage;
        public static float resistencia;
        public static float experienciaNecesaria;
        public static float pociones;
        public static float monedas;
        

    }
    public static class InfoBoss
    {
        public static float vidaBoss;
    }
}
