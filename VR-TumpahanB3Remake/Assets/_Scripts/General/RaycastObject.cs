using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastObject : MonoBehaviour
{
    public LayerMask layerMask;
    public float distance = 10.0f;

    private RaycastHit raycastHit;
    public void CheckRaycast(System.Action onHit = null, System.Action onMiss = null)
    {
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, distance, layerMask))
        {
            onHit?.Invoke();
        }
        else
        {
            onMiss?.Invoke();
        }
    }

    public RaycastHit GetRaycastHit()
    {
        return raycastHit;
    }

    private void OnDrawGizmos()
    {
        CheckRaycast(() =>
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, raycastHit.point);
        }, () =>
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        });
    }
}
