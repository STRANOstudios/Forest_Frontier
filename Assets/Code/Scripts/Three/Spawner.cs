using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Min(0)] float spawnRadius = 1.0f; // Radius for raycast to check for obstacles
    [SerializeField, Min(0), Tooltip("the tolerance for the raycast")] float tolerance = 0.5f;
    [SerializeField, Min(0), Tooltip("the area for the raycast")] float spawnArea = 10;
    [Space]
    [SerializeField, Min(0), Tooltip("the amount of objects to spawn at start")] int spawnAmountInit = 10;
    [SerializeField, Min(0), Tooltip("the amount of objects to spawn")] int spawnAmount = 10;
    [Space]
    [SerializeField, Range(1, 24)] float spawnHoursMin = 5;
    [SerializeField, Range(1, 24)] float spawnHoursMax = 23;
    [Space]
    [SerializeField, Range(0f, 2f)] float yOffset = 0.5f;

    [Header("References")]
    [SerializeField] List<GameObject> objectsToSpawn;
    [SerializeField] Transform defaultSpawnPosition;
    [SerializeField, Tooltip("the surface on which the object will be spawned")] NavMeshSurface navMeshSurface;
    [SerializeField] Camera mainCamera;

    [Header("Debug")]
    [SerializeField] bool debug = false;

    private bool spawning = false;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!defaultSpawnPosition) Debug.LogWarning("DefaultSpawnPosition not assigned");
        if (objectsToSpawn.Count == 0) Debug.LogWarning("ObjectsToSpawn not assigned");
        if (!navMeshSurface) Debug.LogWarning("NavMeshSurface not assigned");
        //if (!mainCamera) debug.LogWarning("MainCamera not assigned");
    }

#endif

    private void Awake()
    {
        Spawn(spawnAmountInit);
    }

    private void Update()
    {
        if (TimeManager.time >= spawnHoursMin && TimeManager.time <= spawnHoursMax)
        {
            spawning = false;
            return;
        }

        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnWithDelay(0.5f));
        }
    }

    private IEnumerator SpawnWithDelay(float delay)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            yield return new WaitForSeconds(delay);

            Vector3 vector3 = GetValidSpawnPoint();

            if (vector3 == Vector3.zero) continue;

            vector3.y -= yOffset;

            ObjectPoolerManager.SpawnObject(objectsToSpawn[Random.Range(0, objectsToSpawn.Count)], vector3, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }

    void Spawn(int amount = 10)
    {
        for (int attempt = 0; attempt < amount; attempt++)
        {
            Vector3 vector3 = GetValidSpawnPoint();

            if (vector3 == Vector3.zero) continue;

            vector3.y -= yOffset;

            ObjectPoolerManager.SpawnObject(objectsToSpawn[Random.Range(0, objectsToSpawn.Count)], vector3, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }

    Vector3 GetValidSpawnPoint()
    {
        for (int attempt = 0; attempt < 100; attempt++) // Try 100 times to find a valid point
        {
            Vector3 randomPoint = GetRandomPointOnNavMesh();

            if (randomPoint != Vector3.zero /*&& !IsVisibleFrom(randomPoint, mainCamera)*/ && !IsObstructed(randomPoint))
            {
                return randomPoint;
            }
        }
        return Vector3.zero; // Return zero vector if no valid point is found
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomPoint = Random.insideUnitSphere * spawnArea;
        randomPoint += navMeshSurface.transform.position;

        Debug.DrawRay(randomPoint, Vector3.up, Color.red, 5.0f);

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, tolerance, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }

    bool IsVisibleFrom(Vector3 point, Camera camera) // to be bugfixed
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(point);

        if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0)
        {
            Ray ray = new(camera.transform.position, point - camera.transform.position);
            RaycastHit[] hits = Physics.RaycastAll(ray, (point - camera.transform.position).magnitude);

            Debug.DrawRay(camera.transform.position, point - camera.transform.position, Color.cyan, 5.0f); // Draw ray from camera to point

            return !(hits.Length > 0);
        }

        return false;
    }

    bool IsObstructed(Vector3 point)
    {
        int obstacleLayer = LayerMask.NameToLayer("Obstacles");
        int obstacleLayerMask = 1 << obstacleLayer;

        Collider[] hitColliders = Physics.OverlapSphere(point, spawnRadius, obstacleLayerMask);

        Debug.DrawRay(point, Vector3.up * 2, Color.green, 5.0f); // Draw ray at the spawn position

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == obstacleLayer)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(navMeshSurface.transform.position, spawnArea);
    }
}
