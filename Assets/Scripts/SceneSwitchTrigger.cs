using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchTrigger : MonoBehaviour
{
    [SerializeField]private SceneField[] scenesToLoad;
    [SerializeField]private SceneField[] scenesToUnload;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            LoadScenes();
            UnloadScenes();
            //load scenestoload
            //unload scenestounload
        }
    }
    private void LoadScenes()
    {
        StartCoroutine(InOrderSceneLoader());
    }
    private IEnumerator InOrderSceneLoader()
    {
        for (int i = 0; i < scenesToLoad.Length; i++)
        {
            AsyncOperation currentOperation = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);
            currentOperation.allowSceneActivation = true;

            while(!currentOperation.isDone)
            {
                yield return null;
            }
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
    private void UnloadScenes()
    {
        for(int i = 0; i<scenesToUnload.Length; i++)
        {
            SceneManager.UnloadSceneAsync(scenesToUnload[i]);
        }
    }
}
