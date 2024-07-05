using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask layersToDamage;
    [SerializeField] int health = 3;

    [Header("References")]
    [SerializeField] GameObject deathObject;

    private int healthBackup;

    private void Awake()
    {
        healthBackup = health;
    }

    private void OnEnable()
    {
        health = healthBackup;
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            if (deathObject != null)
            {
                ObjectPoolerManager.SpawnObject(deathObject, transform.position, Quaternion.identity);
            }
            ObjectPoolerManager.ReturnObjectToPool(gameObject);
            gameObject.SetActive(false);
        }
    }
}
