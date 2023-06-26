using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckeaCheckPoints : MonoBehaviour
{
    public static CheckeaCheckPoints instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
