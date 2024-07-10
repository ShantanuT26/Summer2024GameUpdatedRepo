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
    private bool startCalled;
    public void ButtonPressSceneSwitch()
    {
        StartGame();
        
    }
    private void Awake()
    {
        startCalled = false;
    }
    public void StartGame()
    {
        //UnloadMainMenuObjects();
        //StartCoroutine(FirstGameLoad());
        startCalled = true;
        loadScene1 = SceneManager.LoadSceneAsync(scene1);
        
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //playerController.FreezePlayer(true);
        SceneManager.LoadScene(scene2, LoadSceneMode.Additive);

        //playerController.FreezePlayer(false);
    }
    public void Update()
    {
        if(startCalled)
        {
            Debug.Log("startcalled");
            if (loadScene1.isDone)
            {
                Debug.Log("scene1loaded");
                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                playerController.FreezePlayer(true);
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
        //canvasGroup.alpha = 0f;
    }
}
