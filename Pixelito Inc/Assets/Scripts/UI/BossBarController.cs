using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBarController : MonoBehaviour
{
    [SerializeField] private GameObject bossLifeBar;
    [SerializeField] GameObject boss;
    public bool dead = false;

    private void Update()
    {
        if(dead == true) { bossLifeBar.SetActive(false); }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        boss.SetActive(true);
        bossLifeBar.SetActive(true);
    }
}
