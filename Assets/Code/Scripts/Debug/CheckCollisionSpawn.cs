using UnityEngine;

[ExecuteAlways]
public class CheckCollisionSpawn : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask layerMask;

    private bool validPosition = true;

    private void Update()
    {
        CheckValidePosition();
    }

    private void CheckValidePosition()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, layerMask);

        if (colliders.Length > 0)
        {
            Debug.Log("Spawn is invalid");
            validPosition = false;
            return;
        }

        Debug.Log("Spawn is valid");
        validPosition = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (!validPosition)
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
