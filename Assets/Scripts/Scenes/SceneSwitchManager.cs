using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : MonoBehaviour
{
    //[SerializeField] private CanvasGroup canvasGroup;
    //private string scene1 = "SampleScene";
   // private string scene2 = "SampleSceneCont";
    [SerializeField] private SceneField scene1;
    [SerializeField] private SceneField scene2;
    private PlayerController playerController;
    private AsyncOperation loadScene2;
    private bool startCalled;
    private bool sceneLoaded;
    private bool menuRemoved;
    private Scene backgroundScene;
    private Camera backgroundSceneCam;
    [SerializeField]private Canvas menuCanvas;
    public void ButtonPressSceneSwitch()
    {
        StartGame();
        
    }
    private void Awake()
    {
        menuRemoved = false;
        startCalled = false;
        sceneLoaded = false;
        
    }
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.FreezePlayer(true);
    }
    public void StartGame()
    {
        //UnloadMainMenuObjects();
        //StartCoroutine(FirstGameLoad());
        startCalled = true;
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
            if (loadScene2.progress >= 0.9f && !sceneLoaded)
            {
                loadScene2.allowSceneActivation = true;
                if(loadScene2.isDone)
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
                    backgroundScene = SceneManager.GetSceneByName(scene2);
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene1));   
                    sceneLoaded = true;
                    playerController.FreezePlayer(false);

                }
            }
        }
    }
    /*public IEnumerator FirstGameLoad()
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
    }*/
    public void UnloadMainMenuObjects()
    {
        menuCanvas.gameObject.SetActive(false);
        menuRemoved = true;
    }
}
