using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiencia : MonoBehaviour
{
    [SerializeField]
    private float exp;

    private Level obtener;

    private void Awake()
    {
        obtener = GameObject.FindGameObjectWithTag("Player").GetComponent<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        obtener.LevelUp(exp);
        Destroy(this.gameObject);
    }
}
