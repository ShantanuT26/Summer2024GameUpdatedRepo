using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePrefabPool : MonoBehaviour
{
    [SerializeField]private GameObject pAfterImagePrefab;
    private Queue<GameObject> prefabPool;
    private int numAvailable;
    public static PlayerAfterImagePrefabPool instance { get; private set; }

    private void Awake()
    {
        numAvailable = 0;
        prefabPool = new Queue<GameObject>();
        instance = this;
    }
    public void fillQueue()
    {
        for(int i = 0; i<10; i++)
        {
            GameObject dashPrefabCopy = Instantiate(pAfterImagePrefab);
            tossPlayerPrefabToPool(dashPrefabCopy);
            UnityEngine.Debug.Log("filledqueue");
        }
    }
    public void tossPlayerPrefabToPool(GameObject dashPrefabCopy)
    {
        dashPrefabCopy.SetActive(false);
        numAvailable--;
        prefabPool.Enqueue(dashPrefabCopy);
        UnityEngine.Debug.Log("Sent back to pool from pool");
    }
    public void retrievePlayerPrefabFromPool()
    {
        UnityEngine.Debug.Log("retrievingfrompool");
        if (prefabPool.Count== 0)
        {
            UnityEngine.Debug.Log("abouttofillqueue");
            fillQueue();
        }
        var dashPrefabCopy = prefabPool.Dequeue();
        dashPrefabCopy.SetActive(true);
        numAvailable++;
    }
}
