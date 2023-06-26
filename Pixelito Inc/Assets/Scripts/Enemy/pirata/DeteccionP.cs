using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionP : MonoBehaviour
{
    [SerializeField] Enemigo enemigo;
    [SerializeField] Patrol patrol;
    Animator mobAnim;
    public bool idle;
    private void Start()
    {
        idle = false;
        mobAnim = GetComponentInParent<Animator>();
        enemigo = GetComponentInParent<Enemigo>();
        patrol = GetComponentInParent<Patrol>();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        Character_Move player = collision.GetComponent<Character_Move>();
        if (player != null)
        {
            enemigo.perseguir = true;
            patrol.enabled = false;
            mobAnim.SetBool("Idle", false);
            mobAnim.SetBool("Patrol", false);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        mobAnim.SetBool("Patrol", true);
        enemigo.perseguir = false;
        patrol.enabled = true;
    }
    public void Attack() { mobAnim.SetBool("Patrol", false); }
}
