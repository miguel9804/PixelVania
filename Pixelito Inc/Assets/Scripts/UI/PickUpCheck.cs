using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PickUpCheck : MonoBehaviour
{
    [SerializeField]
    public float coinCount;
    public float potsCount = 0, boostCount = 0, attackBoostCount = 0;
    [SerializeField]
    public bool keyCount = false;
    [SerializeField] GameObject botonVictoria;
    [SerializeField] UIManager controlMisiones;
    private Health_Cha lifeCheck;
    public Load vidas;

    GameObject puertaBoss;// jesus

    private void Awake()
    {
        lifeCheck = GetComponent<Health_Cha>();
        puertaBoss = GameObject.FindGameObjectWithTag("PuertaBoss");// Jesus
    }

    public void PickUpItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "CoinAssasin":
                coinCount += Random.Range(20, 40);
                
                break;
            case "CoinElectric":
                coinCount += Random.Range(50, 75);
                break;
            case "CoinPirata":
                coinCount += Random.Range(75, 90);
                break;
            case "Gem":
                coinCount += Random.Range(150, 200);
                lifeCheck.lp = lifeCheck.maxLp;
                puertaBoss.SetActive(false);
                break;
            case "End":
                Time.timeScale = 0;
                botonVictoria.SetActive(true);
                break;
            case "Key":
                keyCount = true;
                break;
            case "Treasure1":
                controlMisiones.misionChecker[0] = true;
                break;
            case "Treasure2":
                controlMisiones.misionChecker[1] = true;
                break;
            case "Treasure3":
                controlMisiones.misionChecker[2] = true;
                break;
            case "vidaextra":
                vidas.vidas += 1;
                break;

        }
    }
    public float CoinValue()
    {
        return coinCount;
    }
    public bool KeyBool()
    {
        return keyCount;
    }
    public float PotsValue()
    {
        return potsCount;
    }
    public float PotsValueM()
    {
        potsCount -= 1;
        lifeCheck.lp += 15;
        return potsCount;
    }
    public float BoostValue()
    {
        return boostCount;
    }
    public float BoostValueM()
    {
        boostCount -= 1;

        return boostCount;
    }
    public float AttackBoostValue()
    {
        return attackBoostCount;
    }
    public float AttackBoostValueM()
    {
        attackBoostCount -= 1;
        return attackBoostCount;
    }
}
