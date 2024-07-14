using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnRegion;
    private Vector2 playerPosition;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private float playerDistFromSpawnRegion;
    private bool canSpawn;
    private void Awake()
    {
        canSpawn = true;
    }
    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }
    private void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (Mathf.Abs(playerPosition.x-spawnRegion.position.x)<playerDistFromSpawnRegion  && canSpawn)
        {
            Instantiate(enemy1, spawnRegion.position, Quaternion.Euler(0, 0, 0));
            canSpawn = false;
        }
    }
}
