using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class ShowPathLine : MonoBehaviour
{
    public Transform target;
    public float offsetHeight = 0.5f;
    public float updateTime = 0.25f;
    public float minDistance = 0.5f;

    private LineRenderer pathRenderer;
    private WaitForSeconds updateTimeYield;

    private void Awake()
    {
        pathRenderer = GetComponent<LineRenderer>();
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        updateTimeYield = new WaitForSeconds(updateTime);
        NavMeshPath path = new NavMeshPath();

        while (true)
        {
#if UNITY_EDITOR
            updateTimeYield = new WaitForSeconds(updateTime);
#endif
            if (target == null)
            {
                pathRenderer.positionCount = 0;
                yield return updateTimeYield;
                continue;
            }

            if (Vector3.Distance(transform.position, target.position) < minDistance)
            {
                pathRenderer.positionCount = 0;
                yield return updateTimeYield;
                continue;
            }

            if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
            {
                pathRenderer.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    pathRenderer.SetPosition(i, path.corners[i] + Vector3.up * offsetHeight);
                }

                yield return updateTimeYield;
                continue;
            }

            pathRenderer.positionCount = 0;
            yield return updateTimeYield;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetNoTarget()
    {
        target = null;
    }
}
