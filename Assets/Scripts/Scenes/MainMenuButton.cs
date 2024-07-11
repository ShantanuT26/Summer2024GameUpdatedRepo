using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    //[SerializeField] private CanvasGroup canvasGroup;
    private string scene1 = "SampleScene";
    private string scene2 = "SampleSceneCont";
    private PlayerController playerController;
    private AsyncOperation loadScene1;
    private AsyncOperation loadScene2;
    private bool startCalled;
    private bool sceneLoaded;
    private bool menuRemoved;
    private Scene backgroundScene;
    private Scene menuScene;
    private Camera menuSceneCam;
    private Camera backgroundSceneCam;
    [SerializeField]private CanvasGroup menuCanvasGroup;
    public void ButtonPressSceneSwitch()
    {
        StartGame();
        
    }
    private void Awake()
    {
        menuRemoved = false;
        startCalled = false;
        sceneLoaded = false;
        menuScene = SceneManager.GetSceneByName("MainMenu");
        GameObject[] gObjects = menuScene.GetRootGameObjects();
        for(int i = 0; i < gObjects.Length; i++) 
        {
            if (gObjects[i].tag == "MainCamera")
            {
                menuSceneCam = gObjects[i].GetComponent<Camera>();
            }
        }
        
    }
    public void StartGame()
    {
        //UnloadMainMenuObjects();
        //StartCoroutine(FirstGameLoad());
        startCalled = true;
        loadScene1 = SceneManager.LoadSceneAsync(scene1, LoadSceneMode.Additive);
        loadScene1.allowSceneActivation = false;
        
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //playerController.FreezePlayer(true);
        loadScene2 = SceneManager.LoadSceneAsync(scene2, LoadSceneMode.Additive);
        loadScene2.allowSceneActivation = false;

        //playerController.FreezePlayer(false);
    }
    public void Update()
    {
        if(startCalled)
        {
            if(!menuRemoved)
            {
                UnloadMainMenuObjects();
            }
            Debug.Log("startcalled");
            if (loadScene1.progress >=0.9f&& loadScene2.progress >= 0.9f && !sceneLoaded)
            {
                Debug.Log("Progress above 90");
                loadScene1.allowSceneActivation = true;
                loadScene2.allowSceneActivation = true;
                if(loadScene1.isDone)
                {
                    menuSceneCam.gameObject.SetActive(false);
                    playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                    playerController.FreezePlayer(true);
                }
                if(loadScene1.isDone && loadScene2.isDone)
                {
                    backgroundScene = SceneManager.GetSceneByName(scene2);
                    GameObject[] gameObjects = backgroundScene.GetRootGameObjects();
                    for (int i = 0; i < gameObjects.Length; i++)
                    {
                        if (gameObjects[i].tag == "MainCamera")
                        {
                            backgroundSceneCam = gameObjects[i].GetComponent<Camera>();
                        }
                    }
                    backgroundSceneCam.gameObject.SetActive(false);
                    Debug.Log("Progress complete");
                    backgroundScene = SceneManager.GetSceneByName(scene2);
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene1));   
                    sceneLoaded = true;
                    playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                    playerController.FreezePlayer(false);

                }
            }
        }
    }
    public IEnumerator FirstGameLoad()
    {
        Debug.Log("coroutinestarted");
        AsyncOperation loadScene1 = SceneManager.LoadSceneAsync(scene1);

        while (!loadScene1.isDone);
        {
            Debug.Log("loadscene1: " + loadScene1.isDone);
            yield return null;
        }
        Debug.Log("Loadedscene1");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.FreezePlayer(true);
        Debug.Log("Playerfrozen!"); 
        AsyncOperation loadScene2 = SceneManager.LoadSceneAsync(scene2, LoadSceneMode.Additive);
        while(!loadScene2.isDone) 
        {
            yield return null;
        }
        playerController.FreezePlayer(false);
    }
    public void UnloadMainMenuObjects()
    {
        menuCanvasGroup.alpha = 0f;
    }
}
