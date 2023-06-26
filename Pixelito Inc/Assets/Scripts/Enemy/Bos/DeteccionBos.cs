using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionBos : MonoBehaviour
{
    [SerializeField] Boss bos;
    [SerializeField] PatrolBos patrolB;
    Animator mobAnim;
    public bool idle;
    private void Start()
    {
        idle = false;
        mobAnim = GetComponentInParent<Animator>();
        bos = GetComponentInParent<Boss>();
        patrolB = GetComponentInParent<PatrolBos>();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        Character_Move player = collision.GetComponent<Character_Move>();
        if (player != null)
        {
            bos.perseguir = true;
            patrolB.enabled = false;
            mobAnim.SetBool("Idle", false);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        mobAnim.SetBool("Patrol", true);
        bos.perseguir = false;
        patrolB.enabled = true;
    }
    public void Attack() { mobAnim.SetBool("Patrol", false); }
    public void NOAttack() { mobAnim.SetBool("Patrol", true); }

}

