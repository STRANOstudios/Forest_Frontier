using UnityEngine;
using System.Collections;

public class AutoDisable : MonoBehaviour
{
    [SerializeField, Range(5f, 60f)] float disableDelay = 5f;

    void OnEnable()
    {
        StartCoroutine(DisableAfterDelay());
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(5f, disableDelay));

        gameObject.SetActive(false);
    }
}
