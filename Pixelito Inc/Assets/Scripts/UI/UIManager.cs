using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider slideHP, slideXP;
    [SerializeField] private AudioSource potTaken;
    [SerializeField]
    private Color color_dam, color_nor;
    private SpriteRenderer rend;
    private Health_Cha lifeCheck;
    private Character_Move characterMove;
    private PickUpCheck pickUpCheck;
    private AttackP attackPlayer;
    [SerializeField]
    private Load vidasCheck;
    private Level levelCheck;
    public Text[] uIText ;
    public bool[] misionChecker;
    public Image KeyCheck;
    private float atktemp, restemp, time = 0;
    private bool h = false, z = false, x = false;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        characterMove = GetComponent<Character_Move>();
        lifeCheck = GetComponent<Health_Cha>();
        attackPlayer = GetComponentInChildren<AttackP>();
        pickUpCheck = GetComponent<PickUpCheck>();
        levelCheck = GetComponent<Level>();
        rend.color = color_nor;
    }

    public void Update()
    {
        uIText[0].text = lifeCheck.LPValue().ToString() + "/" + lifeCheck.MaxLPValue().ToString();
        uIText[5].text = vidasCheck.Vidas().ToString();
        uIText[1].text = pickUpCheck.CoinValue().ToString();
        uIText[2].text = levelCheck.ShowExp().ToString() + "/" + levelCheck.ShowRexp().ToString();
        uIText[3].text = "Lv. " + levelCheck.ShowLevel().ToString();
        uIText[4].text = pickUpCheck.PotsValue().ToString();
        uIText[10].text = pickUpCheck.BoostValue().ToString();
        uIText[9].text = pickUpCheck.AttackBoostValue().ToString();
        //uIText[11].text = attackPlayer.dps.ToString();
        //uIText[11].text = lifeCheck.res.ToString();
        time = Mathf.Clamp(time, 0, 1);

        if (characterMove.Vive())
        {
            if (Input.GetKeyDown(KeyCode.H) && pickUpCheck.PotsValue() >= 1)
            {
                pickUpCheck.PotsValueM();
                potTaken.Play();
                time = 1;
                h = true;
            }
            if (Input.GetKeyDown(KeyCode.Z) && pickUpCheck.BoostValue() >= 1)
            {
                restemp = lifeCheck.res * 2;
                pickUpCheck.BoostValueM();
                potTaken.Play();
                time = 3;
                z = true;
            }
            if (Input.GetKeyDown(KeyCode.X) && pickUpCheck.AttackBoostValue() >= 1)
            {
                atktemp = attackPlayer.dps * 1.5f;
                pickUpCheck.AttackBoostValueM();
                potTaken.Play();
                time = 2.5f;
                x = true;
            }
        }
        
        if (h)// Solamente cambiara de color cuando el h osea cura sea verdadero, para que asi no interfiera con el cambio de color cuando le hacen daño
        {
            if (time > 0)
            {
                rend.color = color_dam;
                time -= 1*Time.deltaTime;
            }
            else if (time <= 0)
            {
                rend.color = color_nor;
                time = 0;
                h = false;
            }
        }
        if (z)
        {
            if (time > 0)
            {
                rend.color = Color.blue;
                lifeCheck.res = restemp;
                time -= 1* Time.deltaTime;
            }
            else if (time <= 0)
            {
                lifeCheck.res = levelCheck.l * levelCheck.in_res;
                rend.color = color_nor;
                time = 0;
                z = false;
            }
        }
        if (x)
        {
            if (time > 0)
            {
                rend.color = Color.green;
                attackPlayer.dps = atktemp;
                time -= 1* Time.deltaTime;
            }
            else if (time <= 0)
            {
                attackPlayer.dps = (levelCheck.l * levelCheck.in_damage) + 10;
                rend.color = color_nor;
                time = 0;
                x = false;
            }
        }

        if (pickUpCheck.KeyBool()) KeyCheck.enabled = true;
        else KeyCheck.enabled = false;

        slideHP.value = lifeCheck.LPValue() / lifeCheck.MaxLPValue();
        slideXP.value = levelCheck.ShowExpVal();
        verificarmisiones();
    }
    public void verificarmisiones()
    {
        if (misionChecker[0] == true)
        {
            uIText[6].color = Color.green;
        }
        if (misionChecker[1] == true)
        {
            uIText[7].color = Color.green;
        }
        if (misionChecker[2] == true)
        {
            uIText[8].color = Color.green;
        }
    }
}
