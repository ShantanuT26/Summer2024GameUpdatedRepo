using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    private PersistentObjects Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
