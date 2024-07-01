using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent, RequireComponent(typeof(SceneInitializer))]
public class SceneInitializer : MonoBehaviour
{
    [Header("Scene Manager")]
    [SerializeField] private List<SceneAsset> scenes = new();

    private void Awake()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        foreach (var scene in scenes)
        {
            if (!IsSceneLoaded(scene.name))
            {
                SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
            }
        }
    }

    private bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
