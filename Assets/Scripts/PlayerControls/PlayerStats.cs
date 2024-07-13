using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //IMPORTANT KEEP
    //private PlayerController playerController;

    [SerializeField] private float maxHealth;

    private float currentHealth;

    private GameManager gameManager;

    private delegate void OnPlayerTakeDamage(AttackDetails attackDetails);

    private event OnPlayerTakeDamage damageHandler;

    [SerializeField] private GameObject deathChunkParticle, deathBloodParticle;

    private void Awake()
    {
        currentHealth = maxHealth;
        //IMPORTANT KEEP
        // playerController = GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        damageHandler += HandleDamage;
    }
    private void OnDisable()
    {
        damageHandler -= HandleDamage;
    }
    private void HandleDamage(AttackDetails attackDetails)
    {
        Debug.Log("HandlingDamage");
        int direction;
        if (attackDetails.position.x > transform.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        currentHealth -= attackDetails.damage;
        Debug.Log("HandlingDamage current health: " + currentHealth);
        if (currentHealth > 0)
        {
            //IMPORTANT KEEP
            //playerController.KnockedBack(direction);
        }
        else
        {
            Die();
        }
    }
    /*private void Damage(float[] damageInfo)
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
    }*/
    public void TakeDamage(AttackDetails attackDetails)
    {
        Debug.Log("playertakingdamage");
        damageHandler.Invoke(attackDetails);
    }
    private void Die()
    {
        Debug.Log("Playerdeath");
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        //IMPORTANT KEEP
        //gameManager.Respawn(playerController.GetStartingPosition());
        Destroy(gameObject);
    }
}
