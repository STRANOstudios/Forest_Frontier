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
            SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
        }
    }
}
