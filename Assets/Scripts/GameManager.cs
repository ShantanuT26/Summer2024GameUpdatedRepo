using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float playerLastDeathTime;

    private bool isDead;

    //private PlayerController playerController;

    [SerializeField] private float respawnTime;

    [SerializeField] private GameObject player;

    private Vector2 startPosition;

    private CinemachineVirtualCamera playerCam;

    private void Awake()
    {
        playerCam = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        CheckRespawn();
    }
    public void Respawn(Vector2 pos)
    {
        playerLastDeathTime = Time.time;
        startPosition = pos;
        isDead = true;
        
    }
    private void CheckRespawn()
    {
        if(Time.time > playerLastDeathTime + respawnTime && isDead)
        {
            var playerTemp = Instantiate(player, startPosition, Quaternion.Euler(0,0,0));
            playerCam.m_Follow = playerTemp.transform;
            isDead = false;
        }
    }
}
