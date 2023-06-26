using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreditMenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject obj, obj2;

    public void LoadCredits()
    {
        if (obj2.activeInHierarchy && !obj.activeInHierarchy)
        {
            obj.SetActive(true);
            obj2.SetActive(false);
        }
        else if (obj.activeInHierarchy && !obj2.activeInHierarchy)
        {
            obj2.SetActive(true);
            obj.SetActive(false);
        }

    }
}
