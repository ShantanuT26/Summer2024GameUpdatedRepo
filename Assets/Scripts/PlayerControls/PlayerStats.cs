using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private float maxHealth;

    private float currentHealth;

    private GameManager gameManager;

    

    [SerializeField] private GameObject deathChunkParticle, deathBloodParticle;

    private void Awake()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }
    private void Damage(float[] damageInfo)
    {
        int direction;
        if(damageInfo[1]>transform.position.x)
        {
            direction = -1;
        }
        else
        {
            direction=1;
        }
        currentHealth -= damageInfo[0];
        if(currentHealth>0)
        {
            playerController.KnockedBack(direction);
        }
        else
        {
            Die();
        }
    }
    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        gameManager.Respawn(playerController.GetStartingPosition());
        Destroy(gameObject);
    }
}
