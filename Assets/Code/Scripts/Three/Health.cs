using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask layersToDamage;
    [SerializeField] private int health = 3;
    [Space]
    [SerializeField, Range(0f, 2f)] float yOffset = 0.4f;

    [Header("References")]
    [SerializeField] private GameObject deathObject;

    private int healthBackup;
    private DOTweenAnimation dotweenAnimation;

    private void Awake()
    {
        healthBackup = health;
    }

    private void Start()
    {
        dotweenAnimation = GetComponent<DOTweenAnimation>();
    }

    private void OnEnable()
    {
        health = healthBackup;

        if (dotweenAnimation == null)
        {
            if (!TryGetComponent<DOTweenAnimation>(out dotweenAnimation))
            {
                Debug.LogWarning("DOTweenAnimation component missing on " + gameObject.name);
            }
        }
    }

    public void TakeDamage()
    {
        if (dotweenAnimation != null)
        {
            dotweenAnimation.DORestart();
        }

        health--;

        if (health <= 0)
        {
            if (deathObject != null)
            {
                Vector3 vector = transform.position;

                vector.y += yOffset;

                ObjectPoolerManager.SpawnObject(deathObject, vector, Quaternion.identity);
            }
            ObjectPoolerManager.ReturnObjectToPool(gameObject);
            gameObject.SetActive(false);
        }
    }
}
