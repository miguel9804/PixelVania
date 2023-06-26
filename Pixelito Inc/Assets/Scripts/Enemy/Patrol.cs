using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    Transform tr;
    public float speed;
    float waitT;
    public bool patrullar;
    [SerializeField] Transform[] spots;
    private int spot;
    public Animator mobAnim;

    // Start is called before the first frame update
    void Start()
    {
        mobAnim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        patrullar = true;
        waitT = Random.Range(1, 2);
        spot = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (spot == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, spots[spot].position, speed * Time.deltaTime);
            tr.localScale = new Vector3(-0.58f, 0.58f, 0.58f);
            if (Vector2.Distance(transform.position, spots[spot].position) <= 0.16f)
            {
                if (waitT <= 0) { mobAnim.SetBool("Idle", false); spot = 0; waitT = Random.Range(1, 2); }
                else { mobAnim.SetBool("Idle", true); waitT -= Time.deltaTime; }
            }
        }
        if (spot == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, spots[spot].position, speed * Time.deltaTime);
            tr.localScale = new Vector3(0.58f, 0.58f, 0.58f);
            if (Vector2.Distance(transform.position, spots[spot].position) <= 0.16f)
            {
                if (waitT <= 0) { mobAnim.SetBool("Idle", false); spot = 1; waitT = Random.Range(0, 2); }
                else { mobAnim.SetBool("Idle", true); waitT -= Time.deltaTime; }
            }
        }
    }
}
