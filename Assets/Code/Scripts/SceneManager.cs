using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
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
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
        }
    }
}
