using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayController : MonoBehaviour
{
    public bool oneway = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpCheck manager = collision.GetComponent<PickUpCheck>();
        if (manager)
        {
            oneway = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PickUpCheck manager = collision.GetComponent<PickUpCheck>();
        if (manager)
        {
            oneway = false;
        }
    }
}
