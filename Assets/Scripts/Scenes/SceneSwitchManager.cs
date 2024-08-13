using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : MonoBehaviour
{
    private bool startCalled;
    private bool menuRemoved;
    [SerializeField]private Canvas menuCanvas;
    private Player player;
    public void ButtonPressSceneSwitch()
    {
        StartGame(); 
    }
    private void Awake()
    {
        menuRemoved = false;
        startCalled = false;
    }
    private void OnEnable()
    {
       /* DoorTrigger.LoadSceneAction += LoadScene;
        DoorTrigger.UnloadScenesAction += UnloadScenes;
        DoorTrigger.SpawnPlayerAtNewDoorAction += SpawnPlayerAtNewDoor;*/
        DoorTrigger.LoadScenesAndSpawnPlayerAction += SceneLoadAndPlayerSpawn;
    }
    public void StartGame()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        startCalled = true;
    }   
    public void Update()
    {
        if(startCalled)
        {
            if(!menuRemoved)
            {
                UnloadMainMenuObjects();
            }
        }
    }
    /*private void SpawnPlayerAtNewDoor(DoorTrigger.DoorToSpawnAt doorToSpawnAt)
    {
        DoorTrigger[] possibleDoors = GameObject.FindObjectsOfType<DoorTrigger>();
        foreach(DoorTrigger door in possibleDoors)
        {
            if(door.currentDoorPosition == doorToSpawnAt)
            {
                player.AdjustPositionAfteSceneSwitch(door.gameObject.transform.position);
                break;
            }
        }
    }*/
    public void SceneLoadAndPlayerSpawn(SceneField sceneToLoad, SceneField[] scenesToUnload, 
        DoorTrigger.DoorToSpawnAt doorToSpawnAt)
    {
        StartCoroutine(LoadScenesSpawnPlayerInOrder(sceneToLoad, scenesToUnload, doorToSpawnAt));
    }

    public IEnumerator LoadScenesSpawnPlayerInOrder(SceneField sceneToLoad, SceneField[] scenesToUnload, DoorTrigger.DoorToSpawnAt doorToSpawnAt)
    {
        
        //LOADING SCENE
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        loadingScene.allowSceneActivation = true;
        while(!loadingScene.isDone)
        {
            yield return null;
        }
        Camera[] allCameras = GameObject.FindObjectsOfType<Camera>();

        //MAINTAINING CAMERA
        foreach (Camera camera in allCameras)
        {
            if (camera.tag != "MainCamera")
            {
                camera.gameObject.SetActive(false);
            }
        }

        //STARTING FADE OUT CYCLE
        SceneFadeManager.Instance.StartFadeOutCycle();
        player.CompletelyFreezePlayer(true);
        Debug.Log("Playerfrozentrue");

        while (SceneFadeManager.Instance.GetIsFadingOut() == true)
        {
            yield return null;
        }
        //SPAWNING PLAYER

        DoorTrigger[] possibleDoors = GameObject.FindObjectsOfType<DoorTrigger>();  
        foreach (DoorTrigger door in possibleDoors)
        {
            if (door.currentDoorPosition == doorToSpawnAt)
            {
                player.AdjustPositionAfteSceneSwitch(door.gameObject.transform.position);
                player.CompletelyFreezePlayer(false);
                Debug.Log("Playerfrozenfalse");
                break;
            }
        }

        //STARTING FADE IN CYCLE 
        SceneFadeManager.Instance.StartFadeInCycle();

        //UNLOADING SCENES
        for (int i = 0; i < scenesToUnload.Length; i++)
        {
            SceneManager.UnloadSceneAsync(scenesToUnload[i]);
        }
    }
              
    public void UnloadMainMenuObjects()
    {
        menuCanvas.gameObject.SetActive(false);
        menuRemoved = true;
    }
   /* private void LoadScene(SceneField sceneToLoad)
    {
        StartCoroutine(InOrderSceneLoader(sceneToLoad));
    }
    private IEnumerator InOrderSceneLoader(SceneField sceneToLoad)
    {
        AsyncOperation currentOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        currentOperation.allowSceneActivation = true;

        while (!currentOperation.isDone)
        {
            yield return null;
        }
        Camera[] allCameras = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera camera in allCameras)
        {
            if (camera.tag != "MainCamera")
            {
                camera.gameObject.SetActive(false);
            }
        }
    }
    private void UnloadScenes(SceneField[] scenesToUnload)
    {
        for (int i = 0; i < scenesToUnload.Length; i++)
        {
            SceneManager.UnloadSceneAsync(scenesToUnload[i]);
        }
    }*/
}