using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    private static PersistentObjects Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(Instance);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //PRESERVE
            //Destroy(gameObject);
        }
    }
}
