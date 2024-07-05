using UnityEngine;
using System.Collections;

public class AutoDisable : MonoBehaviour
{
    [SerializeField, Min(5f)] float disableDelayMin = 5f;
    [SerializeField, Min(5.1f)] float disableDelayMax = 5f;

    void OnEnable()
    {
        StartCoroutine(DisableAfterDelay());
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(disableDelayMin, disableDelayMax));
        ObjectPoolerManager.ReturnObjectToPool(gameObject);
        gameObject.SetActive(false);
    }
}
