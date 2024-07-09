using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Jobs;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public string tag;
        public int quantity;
    }
    public Dictionary<string, Queue<GameObject>> poolDict;

    public List<Pool> pools;

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for(int i = 0; i<pool.quantity; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }
            poolDict.Add(pool.tag, objPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        GameObject temp = poolDict[tag].Dequeue();
        temp.SetActive(true);
        temp.transform.position = position;
        temp.transform.rotation = rotation;
        return temp;
    }
    public void SendToQueue(GameObject obj, string tag) 
    {
        obj.SetActive(false);
        poolDict[tag].Enqueue(obj);
    }



}
