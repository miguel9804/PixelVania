using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptchekpoints : MonoBehaviour
{
    public Collider2D col;
    public float Posx;
    public float Posy;
    public float Posz;
    public Vector3 posicion;
    public GameObject Starposition;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        Posx = Starposition.transform.position.x;
        Posy = Starposition.transform.position.y;
        Posz = Starposition.transform.position.z;
    }
    private void Update()
    {
        posicion = new Vector3(Posx, Posy, Posz);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            Posx = collision.transform.position.x;
            Posy = collision.transform.position.y+0.1f;
            Posz = collision.transform.position.z;
            posicion = new Vector3(Posx, Posy, Posz);
        }
    }
}
