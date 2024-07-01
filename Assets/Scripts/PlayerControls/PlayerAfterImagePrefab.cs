using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAfterImagePrefab : MonoBehaviour
{
    private float maxVisibleTime = 0.1f;
    private float timeActivated;
    private float opacityMultiplier;
    private float alpha;
    private float alphaSet = 0.8f;

    private bool isActive;

    private SpriteRenderer playerSR;
    private SpriteRenderer mySR;

    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        opacityMultiplier = 0.85f;
    }
    private void OnEnable()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        alpha = alphaSet;
        Debug.Log("PrefabEnabled");
        Debug.Log("alpha: " + alpha);
        isActive = true;
        mySR = GetComponent<SpriteRenderer>();
        playerSR = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        mySR.sprite = playerSR.sprite;
        transform.position = playerTransform.position;
        timeActivated = Time.time;
        mySR.color = new Color(1, 1, 1, alpha);
    }
    private void OnDisable()
    {
        isActive = false;
    }
    private void Update()
    {
        alpha *= opacityMultiplier;
        mySR.color = new Color(1, 1, 1, alpha);
        if (Time.time >= timeActivated + maxVisibleTime)
        {
            PlayerAfterImagePrefabPool.instance.tossPlayerPrefabToPool(gameObject);
            UnityEngine.Debug.Log("Sending back to pool from prefab");
        }
    }
}
