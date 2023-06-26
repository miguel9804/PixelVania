using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBos : MonoBehaviour
{
    Transform tr;
    public float speed;
    float waitT;
    public bool patrullar;
    [SerializeField] Transform[] spots;
    private int spot;
    Animator mobAnim;
    int spotAnt;

    // Start is called before the first frame update
    void Start()
    {
        mobAnim = GetComponent<Animator>();
        patrullar = true;
        waitT = Random.Range(1, 2);
        spot = Random.Range(0, spots.Length);
        spotAnt = spot;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, spots[spot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, spots[spot].position) <= 0.16f)
        {
            if (waitT <= 0) { mobAnim.SetBool("Idle", false); spot = Random.Range(0, spots.Length); spotAnt = spot; waitT = Random.Range(1, 2); }
            else { mobAnim.SetBool("Idle", true); waitT -= Time.deltaTime; }
        }

        if (spot != spotAnt)
        {
            transform.position = Vector2.MoveTowards(transform.position, spots[spot].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, spots[spot].position) <= 0.16f)
            {
                if (waitT <= 0) { mobAnim.SetBool("Idle", false); spot = Random.Range(0, spots.Length); spotAnt = spot; waitT = Random.Range(0, 2); }
                else { mobAnim.SetBool("Idle", true); waitT -= Time.deltaTime; }
            }
        }
        else { spot = Random.RandomRange(0, spots.Length); }
    }
}
