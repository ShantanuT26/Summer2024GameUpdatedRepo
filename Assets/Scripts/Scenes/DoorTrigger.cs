using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour, IInteractible
{
    public static event Action<SceneField> LoadSceneAction;
    public static event Action<SceneField[]> UnloadScenesAction;
    public static event Action<DoorToSpawnAt> SpawnPlayerAtNewDoorAction;
    public static event Action<SceneField, SceneField[], DoorToSpawnAt> LoadScenesAndSpawnPlayerAction;
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public enum DoorToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four,
    }

    [Header("Spawn TO")]
    [SerializeField] private DoorToSpawnAt doorToSpawnTo;
    [SerializeField] private SceneField sceneToLoad;
    [SerializeField] private SceneField[] scenesToUnload;

    [Space(10f)]
    [Header("THIS Door")]
    public DoorToSpawnAt currentDoorPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!player.spawnedIntoDoor)
            {
                Interact();
            }
        }
    }
    
    public void Interact()
    {
       /* LoadSceneAction.Invoke(sceneToLoad);
        UnloadScenesAction.Invoke(scenesToUnload);
        SpawnPlayerAtNewDoorAction.Invoke(doorToSpawnTo);*/
        LoadScenesAndSpawnPlayerAction.Invoke(sceneToLoad, scenesToUnload, doorToSpawnTo);
    }
}
