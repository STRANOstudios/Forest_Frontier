using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask layersToDamage;
    [SerializeField] int health = 3;

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
            gameObject.SetActive(false);
        }
    }
}
